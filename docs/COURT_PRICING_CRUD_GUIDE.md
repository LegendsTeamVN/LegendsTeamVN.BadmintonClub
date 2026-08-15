# Hướng Dẫn Triển Khai CRUD Bảng CourtPricings (Bảng Giá Khung Giờ & Ngày Trong Tuần) Từ A Đến Z

Tài liệu này hướng dẫn chi tiết quy trình thiết kế, cấu trúc và cách cài đặt bảng giá chi tiết theo khung giờ và ngày trong tuần (**`CourtPricings`**) trong hệ thống **LegendsTeamVN.BadmintonClub**.

---

## 1. Mối Quan Hệ Giữa `VenueSchedules` & `CourtPricings`

| Bảng | Mục Đích | Ví Dụ |
| :--- | :--- | :--- |
| **`VenueSchedules`** | Quản lý **lịch hoạt động (mở/đóng cửa)** của sân. | Thứ 2 - Thứ 6: Mở từ `06:00` đến `22:00`. Chủ Nhật: Nghỉ (`IsClosed = true`). |
| **`CourtPricings`** | Quản lý **bảng giá theo khung giờ & ngày trong tuần**. | <ul><li>Từ 06:00 đến 16:00: 80.000đ/giờ (thường).</li><li>Từ 16:00 đến 21:00: 120.000đ/giờ (`IsPeakHour = true`).</li><li>Thứ 7 & Chủ Nhật: 130.000đ/giờ.</li></ul> |

---

## 2. Thiết Kế Bảng Dữ Liệu `CourtPricings`

| Tên Cột | Kiểu Dữ Liệu | Mô Tả |
| :--- | :--- | :--- |
| `Id` | `uuid` (PK) | Mã định danh quy tắc giá |
| `VenueId` | `uuid` (FK) | Tham chiếu đến sân (`Venues`) |
| `StartTime` | `time` | Giờ bắt đầu áp dụng giá (VD: `16:00:00`) |
| `EndTime` | `time` | Giờ kết thúc áp dụng giá (VD: `21:00:00`) |
| `DayOfWeek` | `integer?` | Ngày áp dụng (0: Chủ Nhật, 1: T2, ..., 6: T7). Để `null` = Áp dụng tất cả các ngày |
| `PricePerHour` | `numeric` | Giá tiền / giờ (VD: `120000`) |
| `IsPeakHour` | `boolean` | Đánh dấu giờ cao điểm (`true`/`false`) |
| Audit Fields | | `CreatedOnUtc`, `CreatedBy`, `ModifiedOnUtc`, `ModifiedBy`, `IsDeleted`, `DeletedOnUtc`, `DeletedBy` |

---

## 3. Cấu Trúc File Đã Triển Khai

```
src/
├── LegendsTeamVN.BadmintonClub.Domain/
│   ├── Entities/CourtPricing.cs                  # Entity kế thừa AggregateRoot<Guid>
│   └── Repositories/ICourtPricingRepository.cs   # Interface Repository
│
├── LegendsTeamVN.BadmintonClub.Persistence/
│   ├── Repositories/CourtPricingRepository.cs    # GenericRepository implementation
│   └── DependencyInjection/Extensions/
│       └── ServiceCollectionExtensions.cs        # Đăng ký ICourtPricingRepository (Scoped)
│
├── LegendsTeamVN.BadmintonClub.Application/
│   ├── DTOs/CourtPricings/
│   │   ├── Requests/
│   │   │   ├── CreateCourtPricingRequest.cs
│   │   │   ├── UpdateCourtPricingRequest.cs
│   │   │   └── GetCourtPricingsRequest.cs
│   │   └── Responses/
│   │       └── CourtPricingResponse.cs
│   └── Features/CourtPricings/
│       ├── Create/ (Command, Handler, Validator)
│       ├── Update/ (Command, Handler, Validator)
│       ├── Delete/ (Command, Handler)
│       ├── GetById/ (Query, Handler)
│       └── GetList/ (Query, Handler, Validator)
│
└── LegendsTeamVN.BadmintonClub.Presentation/
    └── Endpoints/CourtPricingEndpoint.cs         # Carter Minimal API Endpoints (/api/court-pricings)
```

---

## 4. Hướng Dẫn Cấu Hình Bảng Giá Mẫu

### Kịch Bản 1: Giờ Thường Ngày Thường (06:00 - 16:00)
```json
POST /api/court-pricings
{
  "venueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "startTime": "06:00:00",
  "endTime": "16:00:00",
  "pricePerHour": 80000,
  "dayOfWeek": null,
  "isPeakHour": false
}
```

### Kịch Bản 2: Giờ Cao Điểm Ngày Thường (16:00 - 21:00)
```json
POST /api/court-pricings
{
  "venueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "startTime": "16:00:00",
  "endTime": "21:00:00",
  "pricePerHour": 120000,
  "dayOfWeek": null,
  "isPeakHour": true
}
```

### Kịch Bản 3: Khung Giá Cuối Tuần (Thứ 7 & Chủ Nhật)
- **Thứ 7 (`dayOfWeek: 6`)**:
```json
POST /api/court-pricings
{
  "venueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "startTime": "06:00:00",
  "endTime": "21:00:00",
  "pricePerHour": 140000,
  "dayOfWeek": 6,
  "isPeakHour": true
}
```

- **Chủ Nhật (`dayOfWeek: 0`)**:
```json
POST /api/court-pricings
{
  "venueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "startTime": "06:00:00",
  "endTime": "21:00:00",
  "pricePerHour": 140000,
  "dayOfWeek": 0,
  "isPeakHour": true
}
```

---

## 5. Danh Sách API Specifications

Base URL: `/api/court-pricings`

### 1. Tạo Quy Tắc Giá (`POST /api/court-pricings`)
- **Permission**: `CourtPricings.Create`
- **Request Body**:
```json
{
  "venueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "startTime": "16:00:00",
  "endTime": "21:00:00",
  "pricePerHour": 120000,
  "dayOfWeek": null,
  "isPeakHour": true
}
```
- **Response `201 Created`**: `"a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"`

### 2. Lấy Danh Sách Bảng Giá (`GET /api/court-pricings`)
- **Permission**: `CourtPricings.Read`
- **Query Parameters**:
  - `venueId` (Guid, optional)
  - `dayOfWeek` (int, optional)
  - `isPeakHour` (bool, optional)
  - `pageNumber` (int, default = 1)
  - `pageSize` (int, default = 10)
- **Response `200 OK`**:
```json
{
  "items": [
    {
      "id": "a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d",
      "venueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "startTime": "16:00:00",
      "endTime": "21:00:00",
      "pricePerHour": 120000,
      "dayOfWeek": null,
      "isPeakHour": true,
      "createdOnUtc": "2026-08-15T18:23:00Z",
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

### 3. Lấy Chi Tiết Giá Theo ID (`GET /api/court-pricings/{id}`)
- **Permission**: `CourtPricings.Read`

### 4. Cập Nhật Quy Tắc Giá (`PUT /api/court-pricings/{id}`)
- **Permission**: `CourtPricings.Update`
- **Request Body**:
```json
{
  "startTime": "17:00:00",
  "endTime": "22:00:00",
  "pricePerHour": 130000,
  "dayOfWeek": null,
  "isPeakHour": true
}
```
- **Response `204 NoContent`**

### 5. Xóa Quy Tắc Giá (`DELETE /api/court-pricings/{id}`)
- **Permission**: `CourtPricings.Delete`
- **Response `204 NoContent`**

---

## 6. Lệnh cURL Mẫu Để Test

### Tạo khung giá cao điểm 16h-21h:
```bash
curl -X POST "https://localhost:7192/api/court-pricings" \
  -H "Content-Type: application/json" \
  -d '{
    "venueId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "startTime": "16:00:00",
    "endTime": "21:00:00",
    "pricePerHour": 120000,
    "dayOfWeek": null,
    "isPeakHour": true
  }'
```

### Lấy danh sách giá theo Sân:
```bash
curl -X GET "https://localhost:7192/api/court-pricings?venueId=3fa85f64-5717-4562-b3fc-2c963f66afa6"
```

### Xóa quy tắc giá:
```bash
curl -X DELETE "https://localhost:7192/api/court-pricings/a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"
```
