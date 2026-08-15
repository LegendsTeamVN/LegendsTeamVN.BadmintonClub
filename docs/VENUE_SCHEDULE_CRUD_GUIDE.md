# Hướng Dẫn Triển Khai CRUD Bảng VenueSchedules Từ A Đến Z

Tài liệu này hướng dẫn chi tiết toàn bộ quy trình thiết kế, kiến trúc và triển khai chức năng **CRUD (Create, Read, Update, Delete)** cho bảng `VenueSchedules` (Lịch hoạt động của sân/địa điểm) trong dự án **LegendsTeamVN.BadmintonClub**.

---

## 1. Tổng Quan Kiến Trúc & Công Nghệ

Dự án áp dụng **Clean Architecture** kết hợp với **CQRS (Command Query Responsibility Segregation)** và **Vertical Slice Architecture**:

```
                  ┌──────────────────────────────────────────┐
                  │          Presentation (Carter API)       │
                  └────────────────────┬─────────────────────┘
                                       │
                                       ▼
                  ┌──────────────────────────────────────────┐
                  │       Application (CQRS / MediatR)       │
                  └──────────┬────────────────────┬──────────┘
                             │                    │
                             ▼                    ▼
   ┌───────────────────────────────────┐  ┌───────────────────────────────────┐
   │       Domain (Entities, Repos)    │  │   Persistence (EF Core, Repos)    │
   └───────────────────────────────────┘  └───────────────────────────────────┘
```

### Công nghệ sử dụng:
- **Framework**: .NET 9 / C# 13
- **ORM**: Entity Framework Core + PostgreSQL
- **CQRS & Messaging**: MediatR (thông qua `LegendsTeamVN.Core`)
- **Validation**: FluentValidation
- **REST Endpoints**: Carter Minimal API
- **Authorization**: Custom Permission Claims (`VenueSchedules.Create`, `VenueSchedules.Read`, `VenueSchedules.Update`, `VenueSchedules.Delete`)

---

## 2. Thiết Kế Cấu Trúc Dữ Liệu (Database Schema)

Bảng **`VenueSchedules`** dùng để lưu thông tin giờ mở/đóng cửa và trạng thái nghỉ của sân theo từng ngày trong tuần.

| Tên Cột | Kiểu Dữ Liệu | Khóa | Mô Tả |
| :--- | :--- | :--- | :--- |
| `Id` | `uuid` | PK | Mã định danh duy nhất của lịch hoạt động |
| `VenueId` | `uuid` | FK | Khóa ngoại tham chiếu đến bảng `Venues` |
| `DayOfWeek` | `integer` | | Ngày trong tuần (0: Chủ Nhật, 1: Thứ 2, ..., 6: Thứ 7) |
| `OpenTime` | `time` | | Giờ mở cửa (VD: `06:00:00`) |
| `CloseTime` | `time` | | Giờ đóng cửa (VD: `22:00:00`) |
| `IsClosed` | `boolean` | | Trạng thái đóng cửa ngày đó (`true` = nghỉ, `false` = hoạt động) |
| `CreatedOnUtc` | `timestamp` | | Thời điểm tạo bản ghi (UTC) |
| `CreatedBy` | `text` | | Người tạo bản ghi |
| `ModifiedOnUtc` | `timestamp` | | Thời điểm cập nhật cuối (UTC) |
| `ModifiedBy` | `text` | | Người cập nhật |
| `IsDeleted` | `boolean` | | Cờ xóa mềm (Soft delete) |
| `DeletedOnUtc` | `timestamp` | | Thời điểm xóa mềm (UTC) |
| `DeletedBy` | `text` | | Người thực hiện xóa |

---

## 3. Cấu Trúc File Đã Triển Khai

```
src/
├── LegendsTeamVN.BadmintonClub.Domain/
│   ├── Entities/
│   │   └── VenueSchedule.cs                      # Entity đại diện cho bảng VenueSchedules
│   └── Repositories/
│       └── IVenueScheduleRepository.cs           # Interface Repository mở rộng IGenericRepository
│
├── LegendsTeamVN.BadmintonClub.Persistence/
│   ├── Configurations/
│   │   └── VenueScheduleConfiguration.cs         # EF Core Fluent API Mapping
│   ├── Repositories/
│   │   └── VenueScheduleRepository.cs            # Impl Repository kế thừa GenericRepository
│   └── DependencyInjection/Extensions/
│       └── ServiceCollectionExtensions.cs        # Đăng ký IVenueScheduleRepository vào DI Container
│
├── LegendsTeamVN.BadmintonClub.Application/
│   ├── DTOs/VenueSchedules/
│   │   ├── Requests/
│   │   │   ├── CreateVenueScheduleRequest.cs
│   │   │   ├── UpdateVenueScheduleRequest.cs
│   │   │   └── GetVenueSchedulesRequest.cs
│   │   └── Responses/
│   │       └── VenueScheduleResponse.cs
│   └── Features/VenueSchedules/
│       ├── Create/
│       │   ├── CreateVenueScheduleCommand.cs
│       │   ├── CreateVenueScheduleCommandHandler.cs
│       │   └── CreateVenueScheduleCommandValidator.cs
│       ├── Update/
│       │   ├── UpdateVenueScheduleCommand.cs
│       │   ├── UpdateVenueScheduleCommandHandler.cs
│       │   └── UpdateVenueScheduleCommandValidator.cs
│       ├── Delete/
│       │   ├── DeleteVenueScheduleCommand.cs
│       │   └── DeleteVenueScheduleCommandHandler.cs
│       ├── GetById/
│       │   ├── GetVenueScheduleByIdQuery.cs
│       │   └── GetVenueScheduleByIdQueryHandler.cs
│       └── GetList/
│           ├── GetVenueSchedulesQuery.cs
│           ├── GetVenueSchedulesQueryHandler.cs
│           └── GetVenueSchedulesQueryValidator.cs
│
└── LegendsTeamVN.BadmintonClub.Presentation/
    └── Endpoints/
        └── VenueScheduleEndpoint.cs               # Định nghĩa các Minimal API Endpoints (Carter)
```

---

## 4. Chi Tiết Triển Khai Từ A Đến Z

### 4.1. Tầng Domain
- **[VenueSchedule.cs](file:///c:/code/abp_be/LegendsTeamVN.BadmintonClub/src/LegendsTeamVN.BadmintonClub.Domain/Entities/VenueSchedule.cs)**: Kế thừa `SoftDeletableEntity<Guid>`, đóng gói các thuộc tính dạng private setter và các phương thức nghiệp vụ (`UpdateSchedule`).
- **[IVenueScheduleRepository.cs](file:///c:/code/abp_be/LegendsTeamVN.BadmintonClub/src/LegendsTeamVN.BadmintonClub.Domain/Repositories/IVenueScheduleRepository.cs)**: Interface thừa hưởng `IGenericRepository<VenueSchedule, Guid>`.

### 4.2. Tầng Persistence
- **[VenueScheduleRepository.cs](file:///c:/code/abp_be/LegendsTeamVN.BadmintonClub/src/LegendsTeamVN.BadmintonClub.Persistence/Repositories/VenueScheduleRepository.cs)**: Lớp triển khai repository kết nối DbContext `BadmintonDbContext`.
- **[ServiceCollectionExtensions.cs](file:///c:/code/abp_be/LegendsTeamVN.BadmintonClub/src/LegendsTeamVN.BadmintonClub.Persistence/DependencyInjection/Extensions/ServiceCollectionExtensions.cs)**: Đăng ký dịch vụ Scoped:
  ```csharp
  services.AddScoped<IVenueScheduleRepository, VenueScheduleRepository>();
  ```

### 4.3. Tầng Application (CQRS & Validation)
Tầng Application chia theo từng tính năng cụ thể (Vertical Slices):
1. **Create (Tạo mới)**:
   - Kiểm tra trùng lặp `(VenueId, DayOfWeek)` -> Trả về lỗi `VenueSchedule.AlreadyExists` nếu đã tồn tại.
   - Kiểm tra điều kiện `CloseTime > OpenTime` khi `IsClosed == false`.
2. **Update (Cập nhật)**:
   - Cập nhật thông tin `OpenTime`, `CloseTime`, `IsClosed` dựa trên `Id`.
3. **Delete (Xóa)**:
   - Xóa mềm bản ghi trong cơ sở dữ liệu qua `venueScheduleRepository.Remove(schedule)`.
4. **GetById (Lấy thông tin chi tiết theo ID)**:
   - Trả về DTO `VenueScheduleResponse`.
5. **GetList (Lấy danh sách phân trang & lọc)**:
   - Hỗ trợ lọc theo `VenueId`, `DayOfWeek`, `IsClosed` và phân trang (`PageNumber`, `PageSize`).

### 4.4. Tầng Presentation (API Endpoints)
- **[VenueScheduleEndpoint.cs](file:///c:/code/abp_be/LegendsTeamVN.BadmintonClub/src/LegendsTeamVN.BadmintonClub.Presentation/Endpoints/VenueScheduleEndpoint.cs)**: Map các route REST API dưới nhóm `/api/venue-schedules`.

---

## 5. Hướng Dẫn Sử Dụng API (API Specification)

Base URL: `/api/venue-schedules`

### 1. Tạo Mới Lịch Hoạt Động (`POST /api/venue-schedules`)
- **Permission**: `VenueSchedules.Create`
- **Request Body**:
```json
{
  "venueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "dayOfWeek": 1,
  "openTime": "06:00:00",
  "closeTime": "22:00:00",
  "isClosed": false
}
```
- **Response `201 Created`**:
```json
"8f4e2d1a-9b3c-4d5e-8f6a-7b8c9d0e1f2a"
```

---

### 2. Lấy Danh Sách Lịch Hoạt Động (`GET /api/venue-schedules`)
- **Permission**: `VenueSchedules.Read`
- **Query Parameters**:
  - `venueId` (Guid, optional): Lọc theo ID sân
  - `dayOfWeek` (int, optional): Lọc theo ngày trong tuần (0-6)
  - `isClosed` (bool, optional): Lọc theo trạng thái đóng cửa
  - `pageNumber` (int, default = 1)
  - `pageSize` (int, default = 10)
- **Response `200 OK`**:
```json
{
  "items": [
    {
      "id": "8f4e2d1a-9b3c-4d5e-8f6a-7b8c9d0e1f2a",
      "venueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "dayOfWeek": 1,
      "openTime": "06:00:00",
      "closeTime": "22:00:00",
      "isClosed": false,
      "createdOnUtc": "2026-08-15T18:00:00Z",
      "modifiedOnUtc": null
    }
  ],
  "pageNumber": 1,
  "pageSize": 10,
  "totalCount": 1,
  "totalPages": 1,
  "hasPreviousPage": false,
  "hasNextPage": false
}
```

---

### 3. Lấy Chi Tiết Lịch Hoạt Động Theo ID (`GET /api/venue-schedules/{id}`)
- **Permission**: `VenueSchedules.Read`
- **Response `200 OK`**:
```json
{
  "id": "8f4e2d1a-9b3c-4d5e-8f6a-7b8c9d0e1f2a",
  "venueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "dayOfWeek": 1,
  "openTime": "06:00:00",
  "closeTime": "22:00:00",
  "isClosed": false,
  "createdOnUtc": "2026-08-15T18:00:00Z",
  "modifiedOnUtc": null
}
```

---

### 4. Cập Nhật Lịch Hoạt Động (`PUT /api/venue-schedules/{id}`)
- **Permission**: `VenueSchedules.Update`
- **Request Body**:
```json
{
  "openTime": "07:00:00",
  "closeTime": "23:00:00",
  "isClosed": false
}
```
- **Response `24 NoContent`**

---

### 5. Xóa Lịch Hoạt Động (`DELETE /api/venue-schedules/{id}`)
- **Permission**: `VenueSchedules.Delete`
- **Response `204 NoContent`**

---

## 6. Lệnh cURL Mẫu Để Test

### Tạo mới:
```bash
curl -X POST "https://localhost:7192/api/venue-schedules" \
  -H "Content-Type: application/json" \
  -d '{
    "venueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "dayOfWeek": 1,
    "openTime": "06:00:00",
    "closeTime": "22:00:00",
    "isClosed": false
  }'
```

### Lấy danh sách theo VenueId:
```bash
curl -X GET "https://localhost:7192/api/venue-schedules?venueId=3fa85f64-5717-4562-b3fc-2c963f66afa6&pageNumber=1&pageSize=10"
```

### Cập nhật:
```bash
curl -X PUT "https://localhost:7192/api/venue-schedules/8f4e2d1a-9b3c-4d5e-8f6a-7b8c9d0e1f2a" \
  -H "Content-Type: application/json" \
  -d '{
    "openTime": "07:00:00",
    "closeTime": "23:00:00",
    "isClosed": false
  }'
```

### Xóa:
```bash
curl -X DELETE "https://localhost:7192/api/venue-schedules/8f4e2d1a-9b3c-4d5e-8f6a-7b8c9d0e1f2a"
```

---

## 7. Kiểm Tra Chi Tiết Với Swagger / Postman

1. Khởi chạy ứng dụng:
   ```bash
   dotnet run --project src/Hosts/LegendsTeamVN.BadmintonClub.API
   ```
2. Truy cập Swagger UI tại: `https://localhost:7192/swagger` (hoặc port đã cấu hình trong `launchSettings.json`).
3. Đăng nhập / Lấy Bearer Token nếu ứng dụng yêu cầu xác thực và thử nghiệm nhóm API **VenueSchedules**.
