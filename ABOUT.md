# About Eshop Razor Page

## Project identity

**Eshop Razor Page** is an ASP.NET Core Razor Pages e-commerce frontend and management portal. It is designed to sit in front of a compatible Eshop API and gives customers, administrators, and sellers focused server-rendered experiences.

## At a glance

- **Author:** Mohammad Hasan Pirayandeh
- **Primary language:** C#
- **UI technologies:** Razor, HTML, CSS, JavaScript
- **Framework:** ASP.NET Core Razor Pages
- **Target framework:** .NET 9
- **Architecture:** Server-rendered presentation layer with feature-oriented services and API contracts
- **Authentication:** JWT Bearer tokens carried through an authentication cookie
- **Deployment:** Local .NET hosting or Docker
- **Repository:** https://github.com/MohammadHasanp/Eshop-RazorPage

## What it contains

- Customer storefront pages
- Registration, login, logout, and protected profile pages
- Product, category, banner, slider, cart, order, and comment models
- Admin CRUD interfaces for catalog and user management
- Role and permission management
- Seller dashboard and inventory management
- Reusable Razor layouts, partial views, editor templates, and Tag Helpers
- Static storefront assets and common frontend plugins

## Intended audience

This repository is useful for developers learning or extending a Razor Pages e-commerce application, teams that need a server-rendered shop client for an existing API, and reviewers looking for a structured example of authentication, role-based areas, service abstractions, and reusable Razor UI components.

## Technical tags

`aspnet-core` `razor-pages` `dotnet-9` `csharp` `razor` `html` `css` `javascript` `jquery` `bootstrap` `ecommerce` `online-store` `shopping-cart` `checkout` `jwt-authentication` `role-based-access-control` `admin-dashboard` `seller-panel` `inventory-management` `automapper` `fluentvalidation` `mediatr` `docker`

## Scope and dependency note

This repository is the web/presentation application. The matching backend API and the referenced `Common.Application` assembly are external dependencies and are not part of this checkout. A compatible backend must be available for most application workflows to function.

## Security note

Configuration values committed to source control must be treated as development placeholders. Production deployments should use strong secret material supplied through User Secrets, environment variables, or a dedicated secret manager.

## Maintenance note

The project is active development software. Review package versions, API compatibility, authorization policies, and deployment settings before using it in production.
