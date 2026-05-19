# JuanApp (.NET 10)

JuanApp, **.NET 10** üzərində qurulmuş çoxqatlı (Layered Architecture) ASP.NET Core MVC e-commerce tətbiqidir. Layihə həm istifadəçi tərəfini, həm də admin panelini əhatə edir.

## Məzmun
- [Layihə Haqqında](#layihə-haqqında)
- [Arxitektura](#arxitektura)
- [Əsas Funksionallıqlar](#əsas-funksionallıqlar)
- [Texnologiyalar](#texnologiyalar)
- [Tələblər](#tələblər)
- [Quraşdırma](#quraşdırma)
- [Konfiqurasiya](#konfiqurasiya)
- [Miqrasiya və Verilənlər Bazası](#miqrasiya-və-verilənlər-bazası)
- [Layihəni İşə Salmaq](#layihəni-işə-salmaq)
- [Default İstifadəçilər və Rollar](#default-istifadəçilər-və-rollar)
- [Qovluq Strukturu](#qovluq-strukturu)
- [Faydalı Komandalar](#faydalı-komandalar)
- [Qeydlər](#qeydlər)

## Layihə Haqqında
JuanApp-da məhsul kataloqu, səbət, sifariş axını, istifadəçi autentifikasiyası və admin idarəetmə paneli kimi e-commerce üçün vacib modullar mövcuddur.

## Arxitektura
Layihə 4 əsas qatdan ibarətdir:

- **JuanApp.PL** *(Presentation Layer)*  
  MVC Controller/View, route-lar, UI və tətbiqin giriş nöqtəsi (`Program.cs`).
- **JuanApp.BLL** *(Business Logic Layer)*  
  Servislər, interfeyslər, DTO-lar və biznes qaydaları.
- **JuanApp.DLL** *(Data Access Layer)*  
  EF Core `DbContext`, entity konfiqurasiyaları, migration-lar və seed.
- **JuanApp.Core** *(Domain Layer)*  
  Domain modellər və baza entity-lər.

## Əsas Funksionallıqlar
- İstifadəçi qeydiyyat / giriş / profil əməliyyatları
- ASP.NET Core Identity ilə rol əsaslı icazə idarəsi (`SuperAdmin`, `Admin`, `Member`)
- Google OAuth login inteqrasiyası
- Məhsul, kateqoriya, rəng, ölçü modulları
- Səbət (basket) və checkout axını
- Sifariş idarəetməsi
- Blog və subscriber modulu
- Admin area ilə məhsul və kontent idarəetməsi

## Texnologiyalar
- **.NET 10 / ASP.NET Core MVC**
- **Entity Framework Core 10**
- **SQL Server**
- **ASP.NET Core Identity**
- **MailKit** (email göndərişi)

## Tələblər
- **.NET SDK 10.0**
- **SQL Server** (local və ya remote instance)
- *(Opsional)* `dotnet-ef` CLI aləti:

```bash
dotnet tool install --global dotnet-ef
```

## Quraşdırma
Repo local-a klonlandıqdan sonra kök qovluqda:

```bash
dotnet restore JuanApp.slnx
```

## Konfiqurasiya
Əsas konfiqurasiya faylları:
- `JuanApp.PL/appsettings.json`
- `JuanApp.PL/appsettings.Development.json`

`appsettings.Development.json` içində aşağıdakı dəyərləri doldurun:

- `ConnectionStrings:DefaultConnection`
- `EmailSettings:SmtpServer`
- `EmailSettings:Port`
- `EmailSettings:Username`
- `EmailSettings:From`
- `EmailSettings:Password`
- `Authentication:Google:ClientId`
- `Authentication:Google:ClientSecret`

Nümunə `ConnectionStrings`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=JuanAppDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

## Miqrasiya və Verilənlər Bazası
Layihədə migration-lar mövcuddur (`JuanApp.DLL/Data/Migrations`).

Database-i yaratmaq/yeniləmək üçün:

```bash
dotnet ef database update --project JuanApp.DLL --startup-project JuanApp.PL
```

## Layihəni İşə Salmaq
Kök qovluqdan:

```bash
dotnet run --project JuanApp.PL
```

`launchSettings.json` üzrə default URL:
- `http://localhost:5195`

## Default İstifadəçilər və Rollar
Tətbiq start olanda role və admin seed avtomatik işləyir (`DbSeeder`).

Default hesablar:
- **SuperAdmin**
  - Email: `superadmin@juanapp.com`
  - Şifrə: `SuperAdmin@123`
- **Admin**
  - Email: `admin@juanapp.com`
  - Şifrə: `Admin@123`

> Təhlükəsizlik üçün production mühitində bu hesabları və şifrələri dərhal dəyişin.

## Qovluq Strukturu

```text
JuanApp/
├─ JuanApp.PL/      # MVC (UI, Controllers, Views, Program.cs)
├─ JuanApp.BLL/     # Business logic, DTO, service interfaces/implementations
├─ JuanApp.DLL/     # EF Core DbContext, migrations, seeding
├─ JuanApp.Core/    # Domain models
└─ JuanApp.slnx
```

## Faydalı Komandalar

Build:
```bash
dotnet build JuanApp.slnx -c Release
```

Test:
```bash
dotnet test JuanApp.slnx -c Release
```

## Qeydlər
- Layihə `.NET 10`-a target edir (`net10.0`).
- Google login üçün `ClientId` və `ClientSecret` boş olarsa yalnız warning çıxır və həmin login işləməz.
- Environment əsaslı konfiqurasiya (`appsettings.{Environment}.json`) istifadə olunur.
