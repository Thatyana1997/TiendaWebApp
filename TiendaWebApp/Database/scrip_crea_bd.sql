-- Base de datos para el proyecto
CREATE DATABASE TiendaOnline;
GO

USE TiendaOnline;

-- Tabla Roles
CREATE TABLE Roles (
    RolID INT PRIMARY KEY IDENTITY(1,1),
    NombreRol NVARCHAR(100) NOT NULL
);

-- Tabla Usuarios
CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(255) UNIQUE NOT NULL,
    Contrasena NVARCHAR(255) NOT NULL,
    RolID INT,
    FechaRegistro DATETIME DEFAULT GETDATE(),
    FechaAdicion DATETIME DEFAULT GETDATE(),
    AdicionadoPor NVARCHAR(255),
    FechaModificacion DATETIME NULL,
    ModificadoPor NVARCHAR(255) NULL,
    FOREIGN KEY (RolID) REFERENCES Roles(RolID)
);

-- Tabla Clientes
CREATE TABLE Clientes (
    ClienteID INT PRIMARY KEY IDENTITY(1,1),
    UsuarioID INT UNIQUE,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Direccion NVARCHAR(255),
    Telefono NVARCHAR(50),
    FechaNacimiento DATE,
    FechaAdicion DATETIME DEFAULT GETDATE(),
    AdicionadoPor NVARCHAR(255),
    FechaModificacion DATETIME NULL,
    ModificadoPor NVARCHAR(255) NULL,
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);

-- Tabla Categorias
CREATE TABLE Categorias (
    CategoriaID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255)
);

-- Tabla Productos
CREATE TABLE Productos (
    ProductoID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500),
    Precio DECIMAL(10, 2) NOT NULL,
    CategoriaID INT,
    Stock INT NOT NULL,
    FechaAdicion DATETIME DEFAULT GETDATE(),
    AdicionadoPor NVARCHAR(255),
    FechaModificacion DATETIME NULL,
    ModificadoPor NVARCHAR(255) NULL
    FOREIGN KEY (CategoriaID) REFERENCES Categorias(CategoriaID)
);

-- Tabla Pedidos
CREATE TABLE Pedidos (
    PedidoID INT PRIMARY KEY IDENTITY(1,1),
    ClienteID INT,
    FechaPedido DATETIME DEFAULT GETDATE(),
    Estado NVARCHAR(50) NOT NULL,
    PrecioTotal DECIMAL(10, 2) NOT NULL,
    FechaAdicion DATETIME DEFAULT GETDATE(),
    AdicionadoPor NVARCHAR(255),
    FechaModificacion DATETIME NULL,
    ModificadoPor NVARCHAR(255) NULL,
    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID)
);

-- Tabla PedidoDetalle para relacionar productos y pedidos
CREATE TABLE PedidoDetalle (
    PedidoDetalleID INT PRIMARY KEY IDENTITY(1,1),
    PedidoID INT,
    ProductoID INT,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10, 2) NOT NULL,
    FechaAdicion DATETIME DEFAULT GETDATE(),
    AdicionadoPor NVARCHAR(255),
    FechaModificacion DATETIME NULL,
    ModificadoPor NVARCHAR(255) NULL,
    FOREIGN KEY (PedidoID) REFERENCES Pedidos(PedidoID),
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID)
);
