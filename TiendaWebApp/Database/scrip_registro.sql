INSERT INTO Roles (NombreRol) VALUES
('Admin'),
('Cliente'),
('Vendedor'),
('Invitado'),
('Soporte');

INSERT INTO Usuarios (Email, Contrasena, RolID, FechaAdicion, AdicionadoPor) VALUES
('admin@tienda.com', '123456', 1, GETDATE(), 'Sistema'),
('cliente1@tienda.com', '123456', 2, GETDATE(), 'Sistema'),
('cliente2@tienda.com', '123456', 2, GETDATE(), 'Sistema'),
('vendedor@tienda.com', '123456', 3, GETDATE(), 'Sistema'),
('soporte@tienda.com', '123456', 5, GETDATE(), 'Sistema');

INSERT INTO Clientes (UsuarioID, Nombre, Apellido, Direccion, Telefono, FechaNacimiento, FechaAdicion, AdicionadoPor) VALUES
(2, 'Juan', 'P�rez', 'Calle 1', '123456789', '1990-01-01', GETDATE(), 'Sistema'),
(3, 'Ana', 'G�mez', 'Calle 2', '987654321', '1992-02-02', GETDATE(), 'Sistema'),
(4, 'Luis', 'Rodr�guez', 'Calle 3', '456123789', '1995-03-03', GETDATE(), 'Sistema'),
(5, 'Mar�a', 'L�pez', 'Calle 4', '789123456', '1998-04-04', GETDATE(), 'Sistema'),
(6, 'Carlos', 'Mart�nez', 'Calle 5', '321654987', '2000-05-05', GETDATE(), 'Sistema');

INSERT INTO Categorias (Nombre, Descripcion) VALUES
('Electr�nica', 'Productos relacionados con dispositivos electr�nicos'),
('Ropa', 'Prendas de vestir de diferentes estilos'),
('Muebles', 'Art�culos para el hogar y oficina'),
('Deportes', 'Equipamiento y accesorios deportivos'),
('Juguetes', 'Juguetes y juegos para ni�os y adultos');

INSERT INTO Productos (Nombre, Descripcion, Precio, CategoriaID, Stock, FechaAdicion, AdicionadoPor) VALUES
('Laptop', 'Laptop de �ltima generaci�n', 1000.00, 1, 10, GETDATE(), 'Sistema'),
('Tel�fono', 'Tel�fono inteligente', 500.00, 1, 20, GETDATE(), 'Sistema'),
('Camiseta', 'Camiseta de algod�n', 20.00, 2, 50, GETDATE(), 'Sistema'),
('Mesa', 'Mesa de madera', 200.00, 3, 5, GETDATE(), 'Sistema'),
('Bicicleta', 'Bicicleta de monta�a', 800.00, 4, 8, GETDATE(), 'Sistema');

INSERT INTO Pedidos (ClienteID, Estado, PrecioTotal, FechaAdicion, AdicionadoPor) VALUES
(5, 'Pendiente', 1200.00, GETDATE(), 'Juan'),
(6, 'Enviado', 500.00, GETDATE(), 'Ana'),
(7, 'Completado', 300.00, GETDATE(), 'Juan'),
(5, 'Cancelado', 0.00, GETDATE(), 'Luis'),
(8, 'Pendiente', 150.00, GETDATE(), 'Mar�a');

INSERT INTO PedidoDetalle (PedidoID, ProductoID, Cantidad, PrecioUnitario, FechaAdicion, AdicionadoPor) VALUES
(1, 1, 1, 1000.00, GETDATE(), 'Juan'),
(1, 2, 2, 100.00, GETDATE(), 'Juan'),
(2, 2, 1, 500.00, GETDATE(), 'Ana'),
(3, 3, 1, 300.00, GETDATE(), 'Juan'),
(4, 4, 1, 150.00, GETDATE(), 'Mar�a');

select * from dbo.Roles;
select * from dbo.Usuarios;
select * from dbo.Productos;
select * from dbo.Clientes;
select * from dbo.Pedidos;
select * from dbo.PedidoDetalle;
