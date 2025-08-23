# GoGo.Api

## 📌 Mục đích

**GoGo.Api** là một dự án backend được xây dựng theo mô hình **Clean Architecture**, nhằm cung cấp một nền tảng API có tính **mở rộng, bảo trì dễ dàng và đảm bảo chất lượng code**.
Dự án tập trung vào việc quản lý **Course, Enrollment, User Profile** và các nghiệp vụ liên quan đến ứng dụng học tập.

---

## 🏗️ Kiến trúc

Dự án được thiết kế theo **Clean Architecture** với 4 tầng chính:

* **Api**: Chứa các controllers và cấu hình cho ASP.NET Core. Đây là entry point để client giao tiếp với hệ thống.
* **Application**: Chứa logic nghiệp vụ (business rules), services, DTOs, use cases.
* **Core**: Chứa các domain models, interfaces, và định nghĩa quy tắc cốt lõi của hệ thống.
* **Infrastructure**: Chứa các triển khai cụ thể như **Entity Framework Core**, repositories, database context, logging, v.v.

```text
GoGo.Api.sln
 ┣ GoGo.Api/           → Web API layer
 ┣ GoGo.Application/   → Business logic layer
 ┣ GoGo.Core/          → Domain & Interfaces
 ┗ GoGo.Infrastructure/→ Data access & Infrastructure
```

---

## ⚙️ Cài đặt & chạy

### 1. Clone repo

```bash
git clone https://github.com/Vohoangtuanhdhtdm/GoGo.Api.git
cd GoGo.Api
```

### 2. Cài đặt .NET SDK

Yêu cầu: **.NET 8 SDK**
Tải tại: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

### 3. Build project

```bash
dotnet build
```

### 4. Chạy API

```bash
cd GoGo.Api
dotnet run
```

API sẽ chạy mặc định tại: **[https://localhost:5001](https://localhost:5001)**

### 5. Test

Nếu có test project:

```bash
dotnet test
```

---

## 🚀 Công nghệ sử dụng

* **.NET 8**
* **ASP.NET Core Web API**
* **Entity Framework Core** (Data access)
* **SQL Server** (Database)
* **AutoMapper** (Mapping DTOs / Entities)
* **Dependency Injection** (Built-in)

---

## 📖 Tài liệu API

Dự án hỗ trợ **Swagger** để dễ dàng test API.

Sau khi chạy, truy cập:
👉 [https://localhost:5001/swagger](https://localhost:5001/swagger)

Tại đây có thể xem toàn bộ endpoints (Courses, UserProfiles, Enrollments, v.v.)

---

## 🤝 Đóng góp

1. Fork repo & tạo branch mới:

```bash
git checkout -b feature/your-feature
```

2. Commit thay đổi:

```bash
git commit -m "Add new feature"
```

3. Push lên branch:

```bash
git push origin feature/your-feature
```

4. Mở Pull Request để review và merge.

### Quy tắc code style

* Áp dụng **SOLID principles**
* Tách biệt rõ ràng giữa các tầng (**Api, Application, Core, Infrastructure**)
* Sử dụng `async/await` với các thao tác bất đồng bộ
* Viết Unit Test cho các business logic quan trọng

---

## 📬 Liên hệ

👨‍💻 **Vo Hoang Tuan**
🔗 [LinkedIn](https://www.linkedin.com/in/tuan-vo-hoang-18aa56307/)


