# 🏗️ Kiến trúc Hệ thống (Architecture)

Tài liệu này giải thích sâu về kiến trúc **.NET 10** đang được sử dụng trong dự án LegendsTeamVN.BadmintonClub, bao gồm Clean Architecture, cấu trúc thư mục, và luồng chạy của CQRS/MediatR.

*[Read this in English](ARCHITECTURE.md)*

---

## 1. Clean Architecture & Dependency Rule

Dự án tuân thủ nghiêm ngặt **Clean Architecture**. Quy tắc sống còn của kiến trúc này là **Quy tắc Phụ thuộc (Dependency Rule)**:
> *Mã nguồn chỉ được phép phụ thuộc hướng vào trong. Tầng bên trong không được phép biết bất kỳ điều gì về tầng bên ngoài.*

### Các Tầng (Layers) trong hệ thống:
1. **Domain Layer (`Core.Domain`, `BadmintonClub.Domain`)**
   - **Vai trò**: Trái tim của hệ thống. Chứa các Thực thể (Entities), Value Objects, Enums, và Domain Exceptions.
   - **Ràng buộc**: KHÔNG phụ thuộc vào bất kỳ thư viện bên ngoài nào (không Entity Framework, không ASP.NET Core).

2. **Application Layer (`Core.Application`, `BadmintonClub.Application`)**
   - **Vai trò**: Chứa logic nghiệp vụ (Use Cases) dưới dạng CQRS Commands/Queries, DTOs, Validators (FluentValidation), và khai báo các Interfaces (Ví dụ: `IUserRepository`).
   - **Ràng buộc**: Chỉ phụ thuộc vào tầng Domain.

3. **Infrastructure / Persistence Layer (`Core.Infrastructure`, `BadmintonClub.Persistence`)**
   - **Vai trò**: Triển khai (Implement) các Interfaces đã định nghĩa ở tầng Application. Chứa `DbContext`, Repositories, Cấu hình RabbitMQ, Redis, và gọi API bên thứ 3.
   - **Ràng buộc**: Phụ thuộc vào tầng Application.

4. **Presentation Layer (`Core.Presentation`, `Hosts/API`)**
   - **Vai trò**: Điểm giao tiếp với Client (RESTful API). Chứa các Endpoints, Middleware xử lý lỗi, và Authentication.
   - **Ràng buộc**: Phụ thuộc vào Application và Infrastructure.

---

## 2. Cấu trúc Thư mục (Directory Structure)

Dự án được chia làm 3 mảng chính:

```text
src/
├── BuildingBlocks/                 # Chứa mã nguồn dùng chung cho toàn hệ thống
│   ├── LegendsTeamVN.Core.Domain
│   ├── LegendsTeamVN.Core.Application
│   ├── LegendsTeamVN.Core.Persistence
│   └── ...
├── Hosts/                          # Chứa các điểm chạy (Entry points) của ứng dụng
│   ├── LegendsTeamVN.BadmintonClub.API       # Project chạy API chính
│   └── LegendsTeamVN.BadmintonClub.Migrator  # Project chạy EF Core Migrations
└── LegendsTeamVN.BadmintonClub.*   # Chứa mã nguồn nghiệp vụ cụ thể của BadmintonClub
    ├── .Domain
    ├── .Application
    ├── .Infrastructure
    └── .Presentation
```

---

## 3. Luồng chạy của CQRS & MediatR

Hệ thống áp dụng **CQRS (Command Query Responsibility Segregation)** thông qua thư viện **MediatR** để tách biệt luồng Đọc (Query) và luồng Ghi (Command).

### Luồng xử lý một Request:
1. **Client Gửi Request**: Client gọi API (ví dụ: `POST /users`).
2. **Endpoint Nhận Request**: Tầng Presentation nhận request và khởi tạo một `Command` (ví dụ: `CreateUserCommand`).
3. **MediatR Điều hướng**: Endpoint gọi `sender.Send(command)`. MediatR tự động tìm `CreateUserCommandHandler` tương ứng.
4. **Validation Pipeline**: Trước khi vào Handler, request đi qua `ValidationBehavior` (tự động chạy FluentValidation). Nếu lỗi, trả về HTTP 400 Bad Request ngay lập tức.
5. **Xử lý Logic**: Handler nhận Command, gọi Domain Entity, và gọi Repository (ở tầng Persistence) để lưu vào Database.
6. **Trả về Kết quả**: Handler trả về một object `Result<T>` cho Endpoint. Endpoint dùng hàm `.Match()` để map thành HTTP 200 OK hoặc HTTP 400/500 kèm Problem Details.
