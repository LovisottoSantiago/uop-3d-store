# ESPECIFICACIÓN DE REQUISITOS
## VISIÓN GENERAL
**Nombre del proyecto:** `uop-3d-store`  
**Descripción:** Aplicación para gestionar una tienda de productos para impresión 3D. Permite a los usuarios visualizar productos, generar órdenes de compra, y contactarse vía WhatsApp. El administrador podrá gestionar productos, consultar y procesar órdenes.
**Entregas:**
1. Aplicación de consola (Parte 1)
2. API RESTful (Parte 2)
3. Aplicación web con HTML/JS/CSS (Parte 3)
---
# ENTREGABLES GENERALES
**Repositorio Git único:** `uop-3d-store`  
**Estructura esperada:**

```
/uop-3d-store
├── /Part1.ConsoleApp
├── /Part2.WebAPI
├── /Part3.Frontend
├── README.md (explicación general)
```

---
# PARTE 1 – APLICACIÓN DE CONSOLA (EF CORE, CODE FIRST)
### Objetivo
Desarrollar una app de consola que utilice **Entity Framework Core (Code-First)** y una base de datos relacional para gestionar productos y órdenes.

---
### Requisitos funcionales
- Crear, editar y eliminar productos.
- Ver listado de productos.
- Crear órdenes de compra seleccionando productos existentes.
- Ver listado de órdenes con sus detalles.
- Cambiar el estado de una orden (Pendiente, Pagada, Cancelada).
- Al marcar una orden como "Pagada", debe descontarse el stock.
- No debe permitirse generar órdenes con productos sin stock suficiente.
 
---
### Requisitos no funcionales
- La base de datos debe generarse mediante **migraciones Code-First**.
- El proyecto debe usar **EF Core** como ORM.
- La lógica de negocio debe estar **separada de la interfaz de consola**.
- Se debe implementar **una arquitectura basada en capas**:
    - Dominio (Entidades)
    - Infraestructura (DbContext, EF)
    - Aplicación (servicios)
    - UI (Consola)

---
### Calidad del código
- Seguir principios **SOLID**.
- Aplicar **Clean Architecture**:
    - Una clase = una responsabilidad.
    - Separación clara de capas.
- Aplicar **Clean Code**:
    - Nombres descriptivos.
    - Comentarios solo donde sea necesario.
    - Código legible y mantenible.
---
### Entregable Parte 1
- Proyecto funcional en `/Part1.ConsoleApp`.
- Base de datos generada con EF Core y SQL Server.
- Carpeta de migraciones.
- README explicando cómo correrlo, conexión a la DB y funcionalidades implementadas.

---
# PARTE 2 – API RESTFUL CON ASP.NET CORE
### Objetivo
Desarrollar una **API REST** que exponga los datos y operaciones realizadas en la Parte 1 mediante endpoints estandarizados.

---
### Requisitos funcionales
- CRUD completo de productos (`GET`, `POST`, `PUT`, `DELETE`)
- Crear orden de compra (`POST`)
- Consultar órdenes (`GET`)
- Cambiar estado de una orden (`PATCH`)
- Validaciones necesarias: no permitir órdenes sin stock suficiente

---
### Requisitos técnicos
- Debe respetarse el estándar **RESTful**.
- Todos los endpoints deben estar documentados con **Swagger/OpenAPI**.
- Las URLs, métodos HTTP, parámetros y respuestas deben seguir lo definido por la documentación Swagger generada.
- Los endpoints con método `OPTIONS` deben ser reemplazados correctamente por `GET`, `POST`, etc.

- Arquitectura en capas:
    - Core (entidades)
    - Infrastructure (EF)
    - Application (servicios y lógica)        
    - API (controladores)
---
### Calidad del código
- **SOLID** y **Clean Architecture** aplicados correctamente.
- Código desacoplado: controladores finos, lógica en servicios.
- Uso de DTOs para transferir datos entre API y clientes.
- Manejo adecuado de errores, validaciones y respuestas HTTP.
---
### Entregable Parte 2
- Proyecto funcional en `/Part2.WebAPI`.
- Swagger funcionando con definición completa.
- README explicando cómo levantar la API y ejemplos de uso.
- Se puede incluir archivo Postman opcional con ejemplos de requests.
---
# PARTE 3 – FRONTEND CON HTML, CSS Y JS
### Objetivo
Desarrollar una aplicación web estática que consuma la API REST desarrollada en la Parte 2 para mostrar productos, manejar el carrito de compras y generar órdenes.

---
### Requisitos funcionales
- Mostrar productos del catálogo (usando datos de la API).
- Filtros por marca, material, etc.
- Agregar productos a un carrito (localStorage).
- Ver y modificar el carrito.
- Finalizar compra → se genera orden vía API y se redirige al WhatsApp del vendedor con el resumen del pedido y el ID de orden.
- Mostrar página de agradecimiento.
- Vista de administración para consultar las órdenes (simple tabla).
---
### Requisitos técnicos
- HTML, CSS y JS (sin frameworks SPA como React o Angular).
- Bootstrap (u otro framework de diseño) puede usarse para la UI.
- El consumo de la API debe hacerse con `fetch` y `async/await`.
- El carrito debe guardarse en `localStorage`.
- Se debe mostrar feedback al usuario en cada acción.
---
### UX/UI
- Interfaz clara, coherente y visualmente agradable.
- Responsive: debe adaptarse bien a móviles y escritorio.
- Retroalimentación al usuario: mensajes, alertas, errores.
- Accesibilidad: textos legibles, colores adecuados.

---
### Entregable Parte 3
- Carpeta `/Part3.Frontend` con todos los archivos estáticos.
- README explicando cómo abrir la página y qué funcionalidades incluye.
- El sitio debe estar conectado funcionalmente a la API (Parte 2).

---
# CHECKLIST FINAL POR ENTREGA
### Parte 1 (Consola)
-  EF Core instalado y funcionando
-  Migraciones aplicadas
-  CRUD de productos
-  Generación de órdenes
-  Validaciones y cambios de estado
### Parte 2 (API)
-  Swagger funcionando
-  Todos los endpoints REST definidos
-  Servicios y DTOs separados
-  Validaciones y estados de órdenes
### Parte 3 (Frontend)
-  Consumo de la API con `fetch`
-  Carrito funcional (localStorage)
-  Redirección a WhatsApp
-  Vista de administrador
-  UI clara y responsive
---


