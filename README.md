# Tech and Solve - APPWeb

Una aplicación full-stack construida con Angular y ASP.NET Core, implementando principios de Clean Architecture con enfoque en la gestión de Productos.

## 📋 Tabla de Contenidos

- [Descripción General de la Arquitectura](#descripción-general-de-la-arquitectura)
- [Arquitectura del Back-End](#arquitectura-del-back-end)
- [Arquitectura del Front-End](#arquitectura-del-front-end)
- [Stack Tecnológico](#stack-tecnológico)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Primeros Pasos](#primeros-pasos)

## 🏗️ Descripción General de la Arquitectura

Este proyecto sigue un patrón de **Clean Architecture**, separando las responsabilidades entre el front-end (SPA Angular) y el back-end (ASP.NET Core Web API). La arquitectura asegura:

- **Separación de Responsabilidades**: Límites claros entre capas
- **Inversión de Dependencias**: Lógica de negocio independiente de frameworks
- **Facilidad de Pruebas**: Componentes aislados para pruebas unitarias más sencillas
- **Mantenibilidad**: Estructura organizada para desarrollo escalable

```
┌─────────────────────────────────────────────────────────────┐
│                    Front-End Angular                         │
│  (Componentes, Servicios, Enrutamiento, Gestión de Estado)  │
└──────────────────────┬──────────────────────────────────────┘
                       │ HTTP/REST API
┌──────────────────────▼──────────────────────────────────────┐
│                   ASP.NET Core Web API                       │
│  ┌────────────────────────────────────────────────────────┐ │
│  │             Capa de Presentación (API)                 │ │
│  │  Controladores, Filtros, Extensions, Middleware        │ │
│  └────────────────────┬───────────────────────────────────┘ │
│  ┌────────────────────▼───────────────────────────────────┐ │
│  │        Capa de Aplicación (Lógica de Negocio)         │ │
│  │  Servicios, DTOs, Validadores, Mappers                │ │
│  └────────────────────┬───────────────────────────────────┘ │
│  ┌────────────────────▼───────────────────────────────────┐ │
│  │      Capa de Dominio (Reglas de Negocio Core)         │ │
│  │  Entidades, Interfaces, Modelos de Dominio            │ │
│  └────────────────────┬───────────────────────────────────┘ │
│  ┌────────────────────▼───────────────────────────────────┐ │
│  │     Capa de Infraestructura (Acceso a Datos)          │ │
│  │  DbContext, Repositorios, Migraciones                  │ │
│  └────────────────────┬───────────────────────────────────┘ │
└───────────────────────┼─────────────────────────────────────┘
                        │
                ┌───────▼────────┐
                │  SQL Server DB │
                └────────────────┘
```

## 🔧 Arquitectura del Back-End

### Capas de Clean Architecture

El back-end está organizado en cuatro capas distintas, siguiendo los principios SOLID y Clean Architecture:

#### 1. **Capa API** (`TechAndSolve.APPWeb.Products.API`)
**Responsabilidad**: Punto de entrada HTTP y configuración de la API

- **Controladores**: Endpoints RESTful (`ProductsController.cs`)
- **Filtros**: Manejo global de excepciones (`ExceptionFilter`)
- **Extensions**: Registro de servicios e inyección de dependencias
- **Configuración**: CORS, OpenAPI/Swagger, pipeline de middleware

**Características Clave**:
- Documentación OpenAPI
- CORS habilitado para peticiones cross-origin
- Filtro de excepciones global para respuestas de error consistentes
- Configuración de inyección de dependencias

#### 2. **Capa de Aplicación** (`TechAndSolve.APPWeb.Products.Application`)
**Responsabilidad**: Orquestación de lógica de negocio y casos de uso

- **Servicios**: Operaciones de negocio (`ProductsService.cs`, `IProductsService.cs`)
- **DTOs**: Objetos de transferencia de datos
  - `ProductRegisterRequest.cs` - Crear producto
  - `ProductUpdateRequest.cs` - Actualizar producto
  - `ProductResponse.cs` - Respuestas de la API
- **Validadores**: Reglas de FluentValidation
  - `ProductRegisterValidator.cs`
  - `ProductUpdateValidator.cs`
- **Mappers**: Conversiones Entidad-DTO (`ProductsMapperExtensions.cs`)

**Dependencias**: Domain, Infrastructure

#### 3. **Capa de Dominio** (`TechAndSolve.APPWeb.Products.Domain`)
**Responsabilidad**: Entidades de negocio e interfaces principales

- **Entidades**: Modelos de dominio (`Product.cs`)
- **Interfaces**: Contratos de repositorio y Unit of Work
  - `IProductsRepository.cs` - Contrato del repositorio de productos
  - `IRepository.cs` - Repositorio genérico base
  - `IUnitOfWork.cs` - Patrón Unit of Work para transacciones
- **Clases Base**: Abstracciones comunes del dominio
  - `IEntityBase.cs` - Interfaz base para entidades
  - `EntityBase.cs` - Clase base con propiedades comunes

**Dependencias**: Ninguna (Lógica de negocio pura)

**Nota Importante**: La interfaz `IUnitOfWork` reside en la capa de Dominio (no en Infraestructura) para mantener la inversión de dependencias y evitar que la capa de Aplicación dependa de Infraestructura.

#### 4. **Capa de Infraestructura** (`TechAndSolve.APPWeb.Products.Infrastructure`)
**Responsabilidad**: Aspectos externos (base de datos, servicios externos)

- **Persistencia**: DbContext de Entity Framework Core (`ApplicationDbContext.cs`)
- **Repositorios**: Implementaciones de acceso a datos
  - `Repository.cs` - Implementación del repositorio genérico
  - `ProductsRepository.cs` - Implementación específica para productos
- **Unit of Work**: Implementación del patrón Unit of Work (`UnitOfWork.cs`)
- **Migraciones**: Versiones del esquema de base de datos
  - `20251102183109_AddProductTable` - Migración inicial de productos
  - `20251102235150_AddIsDeletedPropertyInProductTable` - Soft delete
- **Base de Datos**: SQL Server con EF Core 9.0

**Dependencias**: Domain

**Principio de Inversión de Dependencias**: Esta capa implementa las interfaces definidas en la capa de Dominio (`IRepository`, `IUnitOfWork`), permitiendo que las capas superiores dependan de abstracciones en lugar de implementaciones concretas.

### Stack Tecnológico (Back-End)

| Tecnología | Versión | Propósito |
|------------|---------|-----------|
| .NET | 9.0 | Framework de ejecución |
| ASP.NET Core | 9.0 | Framework Web API |
| Entity Framework Core | 9.0.10 | ORM para acceso a datos |
| FluentValidation | 12.0.0 | Validación de peticiones |
| Scrutor | 6.1.0 | Escaneo de ensamblados y decoración |
| SQL Server | Latest | Base de datos |

### Patrones de Diseño de la API

- **Patrón Repository**: Abstrae la lógica de acceso a datos con repositorios genéricos y específicos
- **Patrón Unit of Work**: Gestiona transacciones y coordina el guardado de cambios
- **Patrón Service**: Encapsula la lógica de negocio y orquesta operaciones
- **Patrón DTO**: Separa los contratos de API de los modelos de dominio
- **Pipeline de Validación**: FluentValidation para validación de peticiones
- **Manejo de Excepciones**: Manejo centralizado de errores mediante filtros
- **Inversión de Dependencias**: Las interfaces residen en la capa de Dominio, las implementaciones en Infraestructura
- **Soft Delete**: Implementación de borrado lógico mediante propiedad `IsDeleted`

## 🎨 Arquitectura del Front-End

### Arquitectura Angular

Construido con **Angular 20** usando componentes standalone y prácticas modernas de Angular.

#### Estructura Principal

```
src/
├── app/
│   ├── products/              # Módulo de funcionalidad de productos
│   ├── shared/                # Utilidades y helpers compartidos
│   ├── components/            # Componentes reutilizables
│   ├── models/                # Interfaces/tipos TypeScript
│   ├── pages/                 # Componentes a nivel de ruta
│   ├── services/              # Servicios HTTP y de negocio
│   ├── modules/               # Módulos de características
│   ├── resolvers/             # Resolvers de datos de ruta
│   ├── routes/                # Configuración de enrutamiento
│   ├── app.ts                 # Componente raíz
│   ├── app.config.ts          # Configuración de la aplicación
│   └── app.routes.ts          # Definiciones de rutas
├── environments/              # Configuraciones de entorno
│   ├── environment.ts
│   └── environment.development.ts
└── main.ts                    # Bootstrap de la aplicación
```

#### Características Clave

- **Componentes Standalone**: Sin NgModules, usando arquitectura Angular moderna
- **Reactive Forms**: Manejo de formularios con validación
- **HTTP Client**: Comunicación con API RESTful
- **Angular Material**: Biblioteca de componentes UI
- **RxJS**: Programación reactiva para operaciones asíncronas
- **Enrutamiento**: Navegación del lado del cliente con resolvers
- **TypeScript**: Desarrollo con tipado seguro

### Stack Tecnológico (Front-End)

| Tecnología | Versión | Propósito |
|------------|---------|-----------|
| Angular | 20.2.0 | Framework |
| Angular Material | 20.2.11 | Componentes UI |
| Angular Flex Layout | 15.0.0-beta.42 | Layouts responsivos |
| RxJS | 7.8.0 | Programación reactiva |
| TypeScript | 5.9.2 | Lenguaje |
| Karma/Jasmine | Latest | Testing |
| Less | 4.2.0 | Preprocesador CSS |

### Patrones del Front-End

- **Componentes Smart/Dumb**: Componentes contenedores y de presentación
- **Capa de Servicios**: Comunicación con API y gestión de estado
- **Reactive Forms**: Gestión de estado de formularios
- **Guards/Resolvers de Ruta**: Control de navegación y pre-carga de datos
- **Organización Modular**: Estructura basada en características
- **Configuración por Entorno**: Configuraciones específicas por entorno

## 📁 Estructura del Proyecto

```
tech-and-solve-APPWeb/
├── TechandSolve.APPWeb/                          # Solución Back-End
│   ├── TechandSolve.APPWeb.Products.API/         # Capa API
│   │   ├── Controllers/
│   │   │   └── ProductsController.cs
│   │   ├── Extensions/                            # Extensions DI
│   │   │   └── ServiceCollectionExtensions.cs
│   │   ├── Filters/                               # Filtros de Excepción
│   │   │   └── ExceptionFilter.cs
│   │   ├── Program.cs
│   │   └── appsettings.json
│   ├── TechandSolve.APPWeb.Products.Application/ # Capa de Aplicación
│   │   └── Products/
│   │       ├── Mappings/                          # Extensiones de mapeo
│   │       │   └── ProductsMapperExtensions.cs
│   │       ├── Requests/                          # DTOs Request
│   │       │   ├── ProductRegisterRequest.cs
│   │       │   └── ProductUpdateRequest.cs
│   │       ├── Responses/                         # DTOs Response
│   │       │   └── ProductResponse.cs
│   │       ├── Services/                          # Lógica de negocio
│   │       │   ├── IProductsService.cs
│   │       │   └── ProductsService.cs
│   │       └── Validators/                        # FluentValidation
│   │           ├── ProductRegisterValidator.cs
│   │           └── ProductUpdateValidator.cs
│   ├── TechandSolve.APPWeb.Products.Domain/      # Capa de Dominio
│   │   ├── Products/
│   │   │   ├── Product.cs                         # Entidad
│   │   │   └── IProductsRepository.cs             # Interfaz
│   │   ├── Interfaces/                            # Interfaces core
│   │   │   ├── IRepository.cs                     # Repositorio genérico
│   │   │   └── IUnitOfWork.cs                     # Unit of Work
│   │   └── Bases/                                 # Clases base
│   │       ├── IEntityBase.cs
│   │       └── EntityBase.cs
│   └── TechandSolve.APPWeb.Products.Infrastructure/ # Capa de Infraestructura
│       ├── Persistence/
│       │   ├── ApplicationDbContext.cs            # EF Core DbContext
│       │   ├── Repositories/                      # Implementaciones
│       │   │   ├── Repository.cs                  # Repositorio genérico
│       │   │   └── Products/
│       │   │       └── ProductsRepository.cs
│       │   ├── Migrations/                        # Migraciones de BD
│       │   │   ├── 20251102183109_AddProductTable.cs
│       │   │   └── 20251102235150_AddIsDeletedPropertyInProductTable.cs
│       │   └── UnitOfWork/
│       │       └── UnitOfWork.cs                  # Implementación UoW
├── TechandSolve.APPWeb.Frontend/                  # Aplicación Front-End
│   ├── src/
│   │   ├── app/
│   │   │   ├── products/                          # Característica de productos
│   │   │   ├── shared/                            # Utilidades compartidas
│   │   │   ├── components/                        # Componentes reutilizables
│   │   │   ├── services/                          # Servicios de API
│   │   │   ├── models/                            # Modelos TypeScript
│   │   │   ├── pages/                             # Componentes de página
│   │   │   └── app.config.ts                      # Configuración de app
│   │   ├── environments/                          # Configs de entorno
│   │   └── main.ts                                # Bootstrap
│   └── package.json
└── README.md                                       # Este archivo
```

## 🚀 Primeros Pasos

### Prerequisitos

- **.NET SDK 9.0** o superior
- **Node.js 18+** y npm
- **SQL Server** (LocalDB o instancia completa)
- **Angular CLI** 20+

### Configuración del Back-End

1. **Navegar al proyecto API**:
   ```bash
   cd TechandSolve.APPWeb/TechandSolve.APPWeb.Products.API
   ```

2. **Actualizar la cadena de conexión** en `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TechAndSolve;Trusted_Connection=true"
     }
   }
   ```

3. **Aplicar migraciones de base de datos**:
   ```bash
   dotnet ef database update
   ```

4. **Ejecutar la API**:
   ```bash
   dotnet run
   ```

   La API estará disponible en `https://localhost:7133` (o el puerto configurado)

### Configuración del Front-End

1. **Navegar al proyecto frontend**:
   ```bash
   cd TechandSolve.APPWeb.Frontend
   ```

2. **Instalar dependencias**:
   ```bash
   npm install
   ```

3. **Actualizar el endpoint de la API** en `src/environments/environment.ts`:
   ```typescript
   export const environment = {
     baseUrl: 'https://localhost:7133/api'
   };
   ```

4. **Iniciar el servidor de desarrollo**:
   ```bash
   npm start
   ```

   La aplicación estará disponible en `http://localhost:4200`

### Flujo de Trabajo de Desarrollo

**Back-End**:
```bash
# Ejecutar con hot reload
dotnet watch run

# Ejecutar pruebas
dotnet test

# Crear nueva migración
dotnet ef migrations add NombreMigracion
```

**Front-End**:
```bash
# Servidor de desarrollo
npm start

# Build para producción
npm run build

# Ejecutar tests
npm test

# Ejecutar linter
ng lint
```

## 📝 Documentación de la API

Cuando se ejecuta en modo desarrollo, la documentación OpenAPI está disponible en:
- Swagger UI: `https://localhost:7133/openapi`

## 🧪 Testing

- **Back-End**: Usa `dotnet test` para pruebas unitarias e integración
- **Front-End**: Karma + Jasmine para pruebas unitarias (`npm test`)

## 📦 Despliegue

### Back-End
```bash
dotnet publish -c Release -o ./publish
```

### Front-End
```bash
npm run build
# Salida en el directorio dist/
```

## 🔄 Cambios Recientes en la Arquitectura

### Movimiento de IUnitOfWork al Dominio (Nov 2025)

**Cambio**: La interfaz `IUnitOfWork` se movió de la capa de Infraestructura a la capa de Dominio.

**Razón**: 
- Eliminar dependencia circular entre capas de Aplicación e Infraestructura
- Adherencia estricta al Principio de Inversión de Dependencias (SOLID)
- La capa de Aplicación ahora solo depende de Dominio, no de Infraestructura

**Estructura Anterior**:
```
Application → Infrastructure (IUnitOfWork)
Application → Domain
```

**Estructura Actual**:
```
Application → Domain (IUnitOfWork, IRepository)
Infrastructure → Domain (implementa interfaces)
```

**Beneficios**:
- ✅ Sin dependencias circulares
- ✅ Mejor testabilidad (mocking más fácil)
- ✅ Cumplimiento de Clean Architecture
- ✅ Mayor flexibilidad para cambiar implementaciones

## 🤝 Contribución

1. Sigue los principios de Clean Architecture
2. Mantén la separación de responsabilidades entre capas
3. Las interfaces deben residir en la capa de Dominio
4. Las implementaciones van en la capa de Infraestructura
5. Escribe pruebas unitarias para nuevas características
6. Usa FluentValidation para validación de entrada
7. Sigue la guía de estilo de Angular para código front-end
8. Implementa soft delete para operaciones de borrado

## 📄 Licencia

[Agrega tu información de licencia aquí]

---

**Construido con Clean Architecture para mantenibilidad y escalabilidad** 🚀
