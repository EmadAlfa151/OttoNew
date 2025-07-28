# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a .NET 9 console application that integrates Otto Marketplace with Odoo ERP system. The application handles two-way synchronization:
- Fetches orders from Otto Marketplace API and creates them in Odoo
- Syncs product inventory quantities from Odoo to Otto Marketplace

## Key Components

### API Clients
- **OttoApiClient**: Handles Otto Marketplace API authentication and operations (products, orders, inventory updates)
- **OdooApiClient**: Manages Odoo JSON-RPC communication for products, orders, and inventory

### Services
- **OdooAccountService**: Manages Odoo account configuration from database

### Database Context
- Uses Entity Framework Core with PostgreSQL
- References external project `CoreSystem.DAL` for data access layer

## Common Development Commands

```bash
# Build the project
dotnet build

# Run the application
dotnet run

# Restore NuGet packages
dotnet restore

# Clean build artifacts
dotnet clean
```

## Configuration

The application uses `appsettings.json` for configuration:
- PostgreSQL connection string
- Otto API base URL (`https://api.otto.market`)

## Architecture Notes

### Otto API Integration
- Uses OAuth2 client credentials flow for authentication
- Implements pagination for large datasets (products/orders)
- Handles batch operations with size limits (100 for products, 200 for inventory updates, 256 for orders)

### Odoo Integration
- Uses JSON-RPC protocol over HTTP
- Handles authentication with username/password stored in database
- Manages product creation, order creation, and inventory queries
- Includes tax calculation and product variant handling

### Error Handling
- Uses `Result<T>` pattern for operation results throughout the codebase
- Extensive debug logging for API interactions

### Data Flow
1. Otto orders are fetched and converted to Odoo format
2. Products are created in Odoo if they don't exist (by SKU or barcode)
3. Inventory quantities from Odoo are pushed to Otto for availability updates

## Dependencies

Key NuGet packages:
- Microsoft.Extensions.Hosting (9.0.0) - for dependency injection
- Microsoft.Extensions.Http (9.0.0) - for HTTP client factory
- Microsoft.EntityFrameworkCore (9.0.0) - for database operations
- Npgsql.EntityFrameworkCore.PostgreSQL (9.0.0) - PostgreSQL provider