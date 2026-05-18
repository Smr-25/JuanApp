# JuanApp

JuanApp **.NET 10** ilə hazırlanmış, çoxqatlı (Layered Architecture) e-commerce web tətbiqidir. Layihə istifadəçi paneli və admin paneli ilə məhsul idarəetməsi, səbət, sifariş, blog, abunəlik və istifadəçi hesabı funksiyalarını təqdim edir.

## 🚀 Texnologiyalar

- **.NET SDK 10.0** (target framework: `net10.0`)
- **ASP.NET Core MVC**
- **Entity Framework Core 10**
- **SQL Server**
- **ASP.NET Core Identity** (rol əsaslı icazələr)
- **Google OAuth** (xarici giriş)
- **MailKit** (email göndərişləri)

## 🧱 Arxitektura

Layihə 4 əsas qatdan ibarətdir:

- `JuanApp.PL` – Presentation Layer (MVC, controller/view)
- `JuanApp.BLL` – Business Logic Layer (servis və biznes qaydaları)
- `JuanApp.DLL` – Data Access Layer (DbContext, migration, seeding)
- `JuanApp.Core` – Domain modellər və ortaq base strukturlar

## ✨ Əsas funksiyalar

### İstifadəçi tərəfi
- Qeydiyyat, giriş, çıxış, email təsdiqi
- Şifrəni unutdum / şifrə yeniləmə
- Google ilə giriş
- Məhsul siyahısı, filtr, axtarış, sıralama, səhifələmə
- Məhsul detalı və məhsul rəyləri
- Səbətə əlavə etmə, miqdar yeniləmə, silmə, checkout
- Sifariş tarixçəsi və sifariş detalları
- Blog siyahısı və blog detalları
- Newsletter abunəliyi

### Admin tərəfi (`/Admin`)
- Dashboard statistikaları
- Məhsul, kateqoriya, rəng, ölçü CRUD əməliyyatları
- Slider və advantage idarəetməsi
- Blog idarəetməsi
- Sifariş və abunəçi idarəetməsi
- Tənzimləmələr (settings)

## ⚙️ Tələblər

- **.NET 10 SDK**
- **SQL Server** (LocalDB və ya full instance)

Yüklənmiş SDK-nı yoxlamaq üçün:

```bash
dotnet --version
```

## 🛠️ Quraşdırma və işə salma

### 1) Reponu açın

```bash
cd /home/runner/work/JuanApp/JuanApp
```

### 2) Konfiqurasiyanı doldurun

`/home/runner/work/JuanApp/JuanApp/JuanApp.PL/appsettings.Development.json` faylında aşağıdakı dəyərləri doldurun:

- `ConnectionStrings:DefaultConnection`
- `EmailSettings` bölməsi (`SmtpServer`, `Port`, `Username`, `From`, `Password`)
- `Authentication:Google` (`ClientId`, `ClientSecret`) – opsional

### 3) Verilənlər bazasını tətbiq edin

```bash
dotnet ef database update --project /home/runner/work/JuanApp/JuanApp/JuanApp.DLL/JuanApp.DLL.csproj --startup-project /home/runner/work/JuanApp/JuanApp/JuanApp.PL/JuanApp.PL.csproj
```

> Əgər `dotnet ef` tanınmırsa, əvvəlcə EF alətini quraşdırın:
>
> ```bash
> dotnet tool install --global dotnet-ef
> ```

### 4) Layihəni başladın

```bash
dotnet run --project /home/runner/work/JuanApp/JuanApp/JuanApp.PL/JuanApp.PL.csproj
```

Default development URL:

- `http://localhost:5195`

## 🔐 Seed olunan rollar və admin hesabları

Tətbiq ilk açılışda rolları və default admin user-ləri seed edir:

- **SuperAdmin**
  - Email: `superadmin@juanapp.com`
  - Password: `SuperAdmin@123`
- **Admin**
  - Email: `admin@juanapp.com`
  - Password: `Admin@123`

> **Vacib:** Production mühitdə bu şifrələri dərhal dəyişin.

## ✅ Lokal yoxlama komandaları

```bash
dotnet build /home/runner/work/JuanApp/JuanApp/JuanApp.slnx
dotnet test /home/runner/work/JuanApp/JuanApp/JuanApp.slnx
```

## 📁 Qovluq strukturu

```text
JuanApp/
├─ JuanApp.PL/      # MVC UI layer
├─ JuanApp.BLL/     # Business logic and services
├─ JuanApp.DLL/     # Data layer, EF Core, migrations
├─ JuanApp.Core/    # Domain models and shared entities
└─ JuanApp.slnx
```

## 📌 Qeyd

Layihə tam şəkildə **.NET 10** üçün hazırlanıb (`net10.0`). Build və run üçün maşında .NET 10 SDK mütləq olmalıdır.
