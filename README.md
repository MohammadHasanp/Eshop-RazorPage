# Eshop Razor Page

A server-rendered e-commerce web application built with **ASP.NET Core Razor Pages** and **.NET 9**. The project provides a customer storefront together with authenticated profile features, an administrator dashboard, and a seller panel for managing inventory.

> **Project status:** Active development / educational and portfolio project. The application expects a separate backend application layer and API contract assembly that are not included in this repository.

## Overview

Eshop Razor Page is the presentation layer of an online shop. It communicates with the e-commerce API through typed service classes and `HttpClient`, maps API responses into page-specific view models, and renders the UI on the server with Razor Pages.

The application supports:

- Customer registration, login, logout, and JWT-backed authentication
- Product browsing, product details, categories, banners, sliders, and comments
- Shopping cart and checkout/order workflows
- Customer profile, address, and password management
- An admin dashboard for products, categories, banners, sliders, users, roles, and user-role assignments
- A seller panel for seller inventory management
- Reusable layouts, partials, editor templates, validation, and custom Tag Helpers
- Docker-based containerization support

## Feature Areas

### Storefront

- Home page and promotional content
- Product and category presentation
- Product carts and checkout-related interactions
- Order and shipping models
- Comment and comment-status handling
- SEO data support for content entities

### Authentication and account

- Registration and login pages under `Pages/Auth`
- JWT token support using `Microsoft.AspNetCore.Authentication.JwtBearer`
- The token is read from the `token` cookie and forwarded as a Bearer token to API calls
- Profile editing, address management, and password changes under `Pages/Profile`
- The `Account` authorization policy protects profile pages

### Administration

The `Pages/Admin` area includes server-rendered management pages for:

- Products and product galleries
- Categories and child categories
- Banners and sliders
- Users
- Roles and permissions
- User-role assignments
- Shared admin layout, header, sidebar, and reusable editor templates

### Seller panel

The `Pages/SellerPanel` area provides a separate seller experience with seller-specific layout components and inventory pages. Access is designed around authenticated users with a `Seller` role through the `SellerPanel` authorization policy.

## Technology Stack

| Area | Technology |
| --- | --- |
| Runtime | .NET 9 / ASP.NET Core |
| Web framework | Razor Pages (`Microsoft.NET.Sdk.Web`) |
| Backend language | C# |
| View language | Razor / HTML (`.cshtml`) |
| Client-side code | JavaScript, jQuery |
| Styling | CSS, Bootstrap |
| Authentication | JWT Bearer authentication and cookie-carried tokens |
| Validation | FluentValidation, Data Annotations, unobtrusive jQuery validation |
| Object mapping | AutoMapper |
| Application patterns | Service layer, DTOs, commands, filters, MediatR contracts |
| JSON | Newtonsoft.Json |
| Charts and UI plugins | ApexCharts, Swiper, Select2, SweetAlert2, noUiSlider and other bundled plugins |
| Deployment | Docker / ASP.NET Core container tooling |

## Repository Structure

```text
.
├── Eshop-RazorPage.sln
├── Eshop-RazorPage/
│   ├── Infrastructure/       Cross-cutting concerns, JWT, mapping, helpers and DI
│   ├── Models/               DTOs, commands, filters and domain-facing view models
│   ├── Pages/                Razor Pages for storefront, auth, profile, admin and seller UI
│   ├── Services/             API-facing service abstractions and implementations
│   ├── TagHelpers/           Reusable server-side UI Tag Helpers
│   ├── ViewModel/             View-specific models
│   ├── wwwroot/               CSS, JavaScript, fonts, images and third-party assets
│   ├── Program.cs             Application startup and HTTP pipeline
│   ├── appsettings*.json      Application and environment configuration
│   ├── Dockerfile             Container build definition
│   └── *.csproj               Project and NuGet package definition
└── README.md
```

### Important namespaces and folders

- `Infrastructure/`: dependency registration, URL generation, Razor rendering, validation attributes, JWT configuration, mapping profiles, and common utilities.
- `Models/`: feature-oriented contracts grouped by Auth, Banner, Category, Comment, Order, Product, Role, Seller, Slider, User, and UserAddress.
- `Services/`: interfaces and implementations that isolate API calls from Razor Page models.
- `Pages/`: the main application UI. `Shared` contains layouts and partials; `Admin` and `SellerPanel` contain role-specific interfaces.
- `TagHelpers/`: buttons, modal helpers, delete-item actions, active-state handling, and submit controls.
- `wwwroot/`: static frontend resources and the shop theme.

## Request and Authentication Flow

1. A browser requests a Razor Page.
2. The page model calls an interface from `Services/`.
3. The registered `HttpClient` sends the request to the configured API.
4. `HttpClientAuthorizationDelegatingHandlers` and the request pipeline forward the JWT from the `token` cookie as a Bearer token.
5. The API response is deserialized into a DTO or `ApiResult` and rendered by Razor.
6. Unauthorized responses are redirected to `/Auth/Login` with the original path included as `redirectTo`.

## Prerequisites

- .NET SDK 9.0 or later
- Access to the matching Eshop backend/API
- The referenced `Common.Application` assembly, or the corresponding shared project available at the path expected by the `.csproj`
- Docker (optional, for containerized execution)

## Configuration

The default configuration is in `Eshop-RazorPage/appsettings.json`; development overrides belong in `appsettings.Development.json` or user secrets.

Relevant settings include:

```json
{
  "JwtConfig": {
    "SignInKey": "<development-only-signing-key>",
    "Issuer": "Eshop.com",
    "Audience": "Eshop-api"
  }
}
```

Do not commit real signing keys, API credentials, connection strings, or production secrets. Use .NET User Secrets, environment variables, or a deployment secret manager.

> The checked-in signing key is suitable only as a development placeholder. Replace it before any shared, staging, or production deployment.

## Run Locally

```bash
git clone https://github.com/MohammadHasanp/Eshop-RazorPage.git
cd Eshop-RazorPage

dotnet restore
dotnet build
dotnet run --project Eshop-RazorPage/Eshop-RazorPage.csproj
```

Open the HTTPS URL printed by `dotnet run` (commonly `https://localhost:7xxx`). If the API is hosted separately, configure its base URL in the shared settings used by `SiteSettings` and `RegisterDependcyServices`.

For development configuration with User Secrets:

```bash
dotnet user-secrets set "JwtConfig:SignInKey" "a-long-local-development-secret"
```

## Docker

Build and run the included container definition from the repository root:

```bash
docker build -t eshop-razorpage ./Eshop-RazorPage
docker run --rm -p 8080:8080 eshop-razorpage
```

Review the `Dockerfile` and environment-specific configuration before deploying to production.

## Development Notes

- Keep API contracts and the `Common.Application` dependency version-aligned with the backend.
- Add new features by keeping API communication in `Services/`, contracts in `Models/`, and UI behavior in the relevant Razor Page folder.
- Use the existing layouts, partials, editor templates, and Tag Helpers before introducing duplicate markup.
- Validate uploaded images and file sizes with the custom validation attributes under `Infrastructure/CustomValidation`.
- Treat the bundled files under `wwwroot/assets` as frontend dependencies; update them deliberately and document major upgrades.

## Testing and Verification

The repository currently does not include a dedicated test project. Before submitting changes, run:

```bash
dotnet restore
dotnet build
```

Then manually verify the affected storefront, authentication, admin, or seller flows against a running compatible API.

## Author

**Mohammad Hasan Pirayandeh**

- GitHub: [@MohammadHasanp](https://github.com/MohammadHasanp)
- Repository: [MohammadHasanp/Eshop-RazorPage](https://github.com/MohammadHasanp/Eshop-RazorPage)

## License

No license file is currently included in this repository. Until a license is added, all rights are reserved by the copyright holder. Add a `LICENSE` file before redistributing the project.
