# Câu lạc bộ Cầu lông LegendsTeamVN

[![CI/CD Pipeline](https://github.com/LegendsTeamVN/LegendsTeamVN.BadmintonClub/actions/workflows/deploy.yml/badge.svg)](https://github.com/LegendsTeamVN/LegendsTeamVN.BadmintonClub/actions/workflows/deploy.yml)

*[Read this in English](README.md)*

Hệ thống Backend API hiện đại và mạnh mẽ để quản lý Câu lạc bộ Cầu lông LegendsTeamVN. Dự án được xây dựng trên nền tảng **.NET 10**, ứng dụng triệt để **Clean Architecture**, **CQRS** và các nguyên lý **Domain-Driven Design (DDD)** nhằm đảm bảo khả năng mở rộng, bảo trì và dễ dàng test.

## 🚀 Công nghệ sử dụng

- **Framework**: .NET 10, ASP.NET Core Minimal APIs
- **Kiến trúc**: Clean Architecture, CQRS (MediatR)
- **Cơ sở dữ liệu**: SQL Server 2022 (Entity Framework Core)
- **Caching**: Redis
- **Message Broker**: RabbitMQ (MassTransit)
- **Hạ tầng (Infrastructure)**: Docker, Nginx Proxy Manager
- **CI/CD**: GitHub Actions, GitHub Container Registry (GHCR)

## 📁 Cấu trúc Dự án

- `src/BuildingBlocks/`: Chứa các thư viện dùng chung, Utilities và cấu trúc cốt lõi của toàn hệ thống.
- `src/LegendsTeamVN.BadmintonClub.*`: Các tầng Domain, Application, và Persistence tuân thủ chặt chẽ Clean Architecture.
- `src/Hosts/LegendsTeamVN.BadmintonClub.API`: Project chính để chạy API (sử dụng Minimal APIs).
- `src/Hosts/LegendsTeamVN.BadmintonClub.Migrator`: Project Worker độc lập chuyên dùng để quản lý và chạy Entity Framework Migrations cực kỳ an toàn.
- `infrastructure/`: Thư mục chứa `docker-compose.yml` để dựng nhanh các dịch vụ hạ tầng ở máy local (SQL Server, Redis, RabbitMQ, NPM).
- `tests/`: Chứa các bài Unit Test kiến trúc (NetArchTest) để ngăn chặn việc gọi sai tầng Dependency.

## 🛠️ Hướng dẫn Cài đặt & Chạy dự án

### Yêu cầu hệ thống
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker & Docker Compose](https://www.docker.com/)

### Bước 1: Khởi động Hạ tầng (Infrastructure)

Trước khi chạy API, bạn cần khởi động Database, Cache và Message Broker.

1. Clone source code về máy:
   ```bash
   git clone https://github.com/LegendsTeamVN/LegendsTeamVN.BadmintonClub.git
   cd LegendsTeamVN.BadmintonClub
   ```

2. Bật hạ tầng thông qua Docker Compose:
   ```bash
   cd infrastructure
   docker compose up -d
   cd ..
   ```

### Bước 2: Chạy Database Migrations

Dự án này sử dụng một project độc lập có tên là `Migrator` để chuyên quản lý migrations. Bạn bắt buộc phải sinh ra code migration cho lần chạy đầu tiên.

**Cách 1: Sử dụng Package Manager Console (trong Visual Studio)**
- Mở cửa sổ PMC và chọn Default Project là `src\Hosts\LegendsTeamVN.BadmintonClub.Migrator`.
```powershell
# Tạo & Chạy cho BadmintonDbContext
Add-Migration -Context BadmintonDbContext Init -OutputDir Migrations
Update-Database -Context BadmintonDbContext

# Tạo & Chạy cho AppIdentityDbContext
Add-Migration -Context AppIdentityDbContext InitIdentity -OutputDir Migrations
Update-Database -Context AppIdentityDbContext
```

**Cách 2: Sử dụng .NET CLI (Terminal)**
- Chạy các lệnh sau từ thư mục gốc của dự án:
```bash
# Tạo & Chạy cho BadmintonDbContext
dotnet ef migrations add Init -c BadmintonDbContext -p src/Hosts/LegendsTeamVN.BadmintonClub.Migrator -s src/Hosts/LegendsTeamVN.BadmintonClub.Migrator -o Migrations
dotnet ef database update -c BadmintonDbContext -p src/Hosts/LegendsTeamVN.BadmintonClub.Migrator -s src/Hosts/LegendsTeamVN.BadmintonClub.Migrator

# Tạo & Chạy cho AppIdentityDbContext
dotnet ef migrations add InitIdentity -c AppIdentityDbContext -p src/Hosts/LegendsTeamVN.BadmintonClub.Migrator -s src/Hosts/LegendsTeamVN.BadmintonClub.Migrator -o Migrations
dotnet ef database update -c AppIdentityDbContext -p src/Hosts/LegendsTeamVN.BadmintonClub.Migrator -s src/Hosts/LegendsTeamVN.BadmintonClub.Migrator
```

### Bước 3: Khởi chạy API

Sau khi Database đã có đầy đủ Schema, bạn có thể chạy API bằng 1 trong 2 cách sau:

**Chạy trực tiếp qua .NET CLI:**
```bash
dotnet run --project src/Hosts/LegendsTeamVN.BadmintonClub.API
```

**Chạy qua Docker Compose (Ở thư mục gốc):**
*Lưu ý: Bạn phải tự tạo file `.env` chứa các biến môi trường cấu hình Database, Redis, JWT... trước khi chạy lệnh này.*
```bash
docker compose up -d
```

## 📚 Tài liệu API (Swagger)

Khi API đang chạy, bạn có thể xem danh sách các API và test trực tiếp thông qua giao diện Swagger:
- **Đường dẫn Local**: `http://localhost:8080/swagger`
- **Tính năng**: Giao diện trực quan, liệt kê đầy đủ Schema và hỗ trợ test API trực tiếp với hệ thống bảo mật JWT Authentication.

## 🏗️ Hướng dẫn Lập trình (Development Guidelines)

Để đảm bảo mã nguồn luôn gọn gàng và dễ bảo trì, chúng tôi đã viết một bộ tài liệu chi tiết giải thích về Kiến trúc lõi, Cơ sở dữ liệu và Quy tắc thiết kế API.

👉 **[Kiến trúc Hệ thống (Architecture)](docs/ARCHITECTURE.VI.md)**
👉 **[Thiết kế Cơ sở dữ liệu (Database)](docs/DATABASE.VI.md)**
👉 **[Hướng dẫn Lập trình API (API Guidelines)](docs/API_GUIDELINES.VI.md)**

## 📜 Giấy phép

Dự án này được cấp phép theo Giấy phép GPL-3.0 - xem chi tiết tại [LICENSE](LICENSE.txt).
