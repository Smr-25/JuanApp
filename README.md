# Juan E-Commerce Admin Panel

## 📋 Proyekt Haqqında

Tam funksional e-commerce admin panel sistemi - ASP.NET Core MVC, Identity və layered architecture ilə.

## ✨ Əsas Xüsusiyyətlər

### 🔐 Authentication & Authorization
- **3 Role Sistemi**: SuperAdmin, Admin, Member
- Email təsdiqləməsi
- Password reset funksionallığı
- Account lockout (3 səhv cəhd)

### 👨‍💼 Admin Panel
- **Dashboard**: Statistika və quick actions
- **Product Management**: Tam CRUD, şəkil yükləmə, variant (color/size)
- **Category Management**: Kateqoriya CRUD
- **Order Management**: Sifarişlərin görüntülənməsi və status update
- **Slider Management**: Homepage slider CRUD
- **Advantage Management**: Üstünlüklərin idarəsi
- **Subscriber Management**: Email abunələrinin idarəsi
- **Color & Size Management**: Məhsul variantları

### 📧 Email Sistemi
- Qeydiyyatda email təsdiqi
- Yeni məhsul əlavə olunanda abunələrə avtomatik email
- HTML email template-lər

### 🛍️ E-Commerce Features
- Product filtering (category, color, size, price)
- Product sorting
- Product details with related products
- Shopping basket
- Quick view modal

## 🏗️ Arxitektura

```
JuanApp/
├── JuanApp.Core/          # Domain models
├── JuanApp.DLL/           # Data Access Layer (DbContext, Migrations)
├── JuanApp.BLL/           # Business Logic Layer (Services, DTOs)
└── JuanApp.PL/            # Presentation Layer (Controllers, Views)
    ├── Areas/Admin/       # Admin Panel Area
    ├── Controllers/       # Public Controllers
    └── Views/            # Public Views
```

## 🚀 Quraşdırma

### 1. Database Migration

```bash
cd JuanApp
dotnet ef migrations add InitialCreate --project JuanApp.DLL --startup-project JuanApp.PL
dotnet ef database update --project JuanApp.DLL --startup-project JuanApp.PL
```

### 2. Konfiqurasiya

`appsettings.Mac.json` (və ya `appsettings.json`):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING"
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SenderEmail": "your-email@gmail.com",
    "SenderName": "Juan Shop",
    "Username": "your-email@gmail.com",
    "Password": "your-app-password"
  }
}
```

### 3. İşə Salma

```bash
dotnet run --project JuanApp.PL
```

## 👤 Default Admin Hesabları

Application işə düşdükdə avtomatik olaraq aşağıdakı hesablar yaranır:

### SuperAdmin
- **Email**: superadmin@juanshop.com
- **Password**: Admin123!
- **Səlahiyyət**: Tam giriş

### Admin
- **Email**: admin@juanshop.com
- **Password**: Admin123!
- **Səlahiyyət**: Admin panel girişi

## 📱 Admin Panel URL

```
https://localhost:7062/Admin/Auth/Login
```

## 🛠️ Texnologiyalar

- ASP.NET Core 8.0 MVC
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Bootstrap 5
- Font Awesome 6
- MailKit (Email göndərmə)

## 📂 Əsas Service-lər

### Admin Services
- `IAdminProductService` - Məhsul idarəsi
- `IAdminCategoryService` - Kateqoriya idarəsi
- `IAdminSliderService` - Slider idarəsi
- `IAdminAdvantageService` - Üstünlük idarəsi
- `IAdminColorService` - Rəng idarəsi
- `IAdminSizeService` - Ölçü idarəsi
- `IOrderService` - Sifariş idarəsi
- `ISubscriberService` - Abunə idarəsi

### Public Services
- `IProductService` - Məhsul məlumatları
- `IAccountService` - İstifadəçi qeydiyyatı və autentifikasiya
- `IEmailService` - Email göndərmə
- `IBasketService` - Səbət idarəsi

## ✅ TODO

- [ ] Payment integration
- [ ] Advanced filtering
- [ ] Product review sistem
- [ ] Wishlist funksionallığı
- [ ] Real-time notification
- [ ] Excel export

## 📝 Qeydlər

- Email göndərmə üçün Gmail App Password istifadə edin
- Admin panel yalnız Admin və SuperAdmin roluna malik istifadəçilər üçün əlçatandır
- Şəkillər `/wwwroot/uploads/` qovluğunda saxlanılır
- Subscriber email-ləri yeni məhsul əlavə olunanda avtomatik göndərilir

## 🤝 Developer

**Juan E-Commerce Team**

---

⭐ **Uğurlar!**

