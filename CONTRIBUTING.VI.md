# 🤝 Hướng dẫn Đóng góp (Contributing)

Trước tiên, xin cảm ơn bạn đã quan tâm và muốn đóng góp cho dự án LegendsTeamVN.BadmintonClub! Chúng tôi luôn hoan nghênh và trân trọng mọi sự đóng góp từ cộng đồng.

*[Read this in English](CONTRIBUTING.md)*

---

## 🛠️ Quy trình Đóng góp

### 1. Fork & Clone
1. Fork repository này về tài khoản GitHub cá nhân của bạn.
2. Clone bản fork về máy local:
   ```bash
   git clone https://github.com/YOUR-USERNAME/LegendsTeamVN.BadmintonClub.git
   ```
3. Khởi chạy hạ tầng và Database theo hướng dẫn tại [README.vi.md](README.vi.md).

### 2. Quy tắc Đặt tên Nhánh (Branching)
Hãy luôn tạo một nhánh mới từ nhánh `main` trước khi code. Vui lòng tuân thủ quy tắc đặt tên sau:
- `feature/ten-tinh-nang` (Dành cho tính năng mới)
- `bugfix/mo-ta-loi` (Dành cho việc sửa lỗi bug)
- `hotfix/loi-nghiem-trong` (Dành cho các lỗi nghiêm trọng cần sửa gấp)
- `docs/cap-nhat-tai-lieu` (Dành cho việc cập nhật tài liệu/README)
- `chore/bao-tri-he-thong` (Dành cho các tác vụ bảo trì, nâng cấp thư viện)

### 3. Tiêu chuẩn Lập trình (Coding Standards)
Trước khi bắt tay vào code, bạn **bắt buộc** phải đọc qua các bộ tiêu chuẩn kiến trúc của dự án để đảm bảo code của bạn không phá vỡ cấu trúc hệ thống:
- 🏗️ [Clean Architecture & Quy trình Code](docs/ARCHITECTURE.vi.md)
- 🗄️ [Quy tắc Thiết kế Database](docs/DATABASE.vi.md)
- 🌐 [Hướng dẫn Thiết kế API](docs/API_GUIDELINES.vi.md)

**Những Quy tắc Sống còn:**
- Code của bạn phải pass 100% các bài test kiến trúc (`tests/LegendsTeamVN.BadmintonClub.Architecture.Tests`).
- Tuyệt đối không cài thêm thư viện (dependencies) bên ngoài vào tầng `Domain`.
- Format code sạch sẽ, xóa bỏ các dòng `using` dư thừa trước khi commit.

### 4. Quy ước Commit Message
Dự án áp dụng chuẩn [Conventional Commits](https://www.conventionalcommits.org/).
Cấu trúc: `<type>(<scope>): <subject>`

**Ví dụ:**
- `feat(users): them chuc nang cap nhat profile`
- `fix(auth): sua loi token het han qua som`
- `docs: cap nhat tai lieu api swagger`
- `refactor(core): toi uu hoa cau truy van db`

### 5. Tạo Pull Requests (PR)
1. Push nhánh bạn vừa code lên bản fork của bạn.
2. Mở một Pull Request trỏ vào nhánh `main` của repository gốc.
3. Điền đầy đủ thông tin vào PR template. Mô tả rõ ràng bạn đã làm gì và tại sao lại làm vậy.
4. Đợi luồng CI/CD (GitHub Actions) chạy test và báo xanh (Thành công).
5. Sửa đổi code nếu có yêu cầu từ người duyệt code (Code Reviewers).

---

Một lần nữa, xin chân thành cảm ơn thời gian và công sức của bạn dành cho dự án! 🏸
