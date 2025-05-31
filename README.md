# Biblioteca Digital Interactiva – Lectura+

## Descripción

"Biblioteca Digital Interactiva – Lectura+" es una plataforma web para explorar colecciones digitales de libros. El sistema permite a los usuarios buscar libros, calificarlos y explorar los más populares a través de un top 5. Está desarrollado en ASP.NET Core MVC con base de datos en SQL Server.

## Funcionalidades Principales

* **Gestión de Libros:** Agregar, modificar y eliminar libros.
* **Exploración de Libros:** Búsqueda por título y género.
* **Clasificación de Libros:** Permite a los usuarios calificar libros sin necesidad de registro.
* **Top 5 Libros:** Muestra los libros mejor calificados.
* **Video de demostración:** https://youtu.be/eDJRi-VuDho

## Tecnologías Utilizadas

* **Backend:** ASP.NET Core MVC, C#
* **Frontend:** HTML, CSS, Bootstrap, JavaScript
* **Base de Datos:** SQL Server
* **ORM:** Entity Framework Core

## Requisitos

* .NET SDK 7.0+
* SQL Server
* Visual Studio 2022 o superior

## Instalación y Configuración

1. **Clonar este repositorio:**

   ```bash
   git clone https://github.com/Jonixmax/Biblioteca.git
   ```

2. **Crear la base de datos en SQL Server:**

   ```sql
   -- Crear la base de datos
   CREATE DATABASE BibliotecaLectura21;
   GO

   USE BibliotecaLectura21;
   GO

   -- Crear tabla de libros
   CREATE TABLE Libros (
       idLibro INT IDENTITY(1,1) PRIMARY KEY,
       titulo NVARCHAR(255) NOT NULL,
       autor NVARCHAR(255) NOT NULL,
       genero NVARCHAR(100) NOT NULL,
       sinopsis NVARCHAR(MAX) NOT NULL,
       portada_url NVARCHAR(500) NOT NULL
   );
   GO

   -- Crear tabla de calificaciones
   CREATE TABLE Calificaciones (
       idCalificacion INT IDENTITY(1,1) PRIMARY KEY,
       idLibro INT NOT NULL,
       puntuacion INT CHECK (puntuacion BETWEEN 1 AND 5),
       fechaHora DATETIME DEFAULT GETDATE(),
       FOREIGN KEY (idLibro) REFERENCES Libros(idLibro) ON DELETE CASCADE
   );
   GO


   ```

3. **Configurar la cadena de conexión en `appsettings.json`:**

   ```json
   {
       "ConnectionStrings": {
           "DefaultConnection": "Server=DESKTOP-JNP8EEG\\MSSQLSERVER01;Database=BibliotecaLectura21;Trusted_Connection=True;TrustServerCertificate=True;"
       }
   }
   ```

4. **Configurar el contexto de la base de datos en Models-DB `BibliotecaLecturaContext.cs`:**

   ```csharp
   optionsBuilder.UseSqlServer("Server=DESKTOP-JNP8EEG\\MSSQLSERVER01;Database=BibliotecaLectura21;Trusted_Connection=True;TrustServerCertificate=True;");
   ```

5. **Ejecutar las migraciones de Entity Framework (si es necesario):**

   ```bash
   dotnet ef database update
   ```

6. **Iniciar la aplicación:**

   ```bash
   dotnet run
   ```

## Uso

* **Página Principal:** Introducción con botones para explorar, calificar y gestionar libros.
* **Gestión de Libros:** Permite agregar, actualizar y eliminar libros desde una interfaz centralizada.
* **Exploración:** Búsqueda avanzada de libros por título y género.
* **Clasificación:** Calificación rápida de libros del 1 al 5.

## Propuesta de mejorar 
* **Optimizacion de imagenes :** ajustar un tamaño valido en el apartado de filtracion de los libros
* **Ajustar el apartado de actualizacion de ejemplar** 




