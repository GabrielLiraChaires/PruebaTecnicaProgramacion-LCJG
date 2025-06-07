CREATE DATABASE examen_jose_lira
go
USE examen_jose_lira
go

CREATE TABLE t_clientes (
    IdCliente INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50),
    Domicilio VARCHAR(100),
    email VARCHAR(50)
);

CREATE TABLE t_productos (
    IdProducto INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50),
    Marca VARCHAR(50),
    Costo NUMERIC(5,2),
    PrecioVenta NUMERIC(5,2)
);

CREATE TABLE t_facturas (
    IdFactura INT PRIMARY KEY IDENTITY(1,1),
    NumeroFactura VARCHAR(10),
    FechaHora DATETIME,
    IdCliente INT,
    FOREIGN KEY (IdCliente) REFERENCES t_clientes(IdCliente)
);

CREATE TABLE t_detalleFactura (
    IdDetalle INT PRIMARY KEY IDENTITY(1,1),
    IdFactura INT,
    IdProducto INT,
    Cantidad INT,
    FOREIGN KEY (IdFactura) REFERENCES t_facturas(IdFactura),
    FOREIGN KEY (idProducto) REFERENCES t_productos(IdProducto)
);
