create view productoview as
select P.id_producto as "ID",P.nombre as "Name", P.cedula_a as "Cedula_a",
X.id_carrito as "ID_Carrito", P.precio as "Price", X.cantidad as "Cantidad"
from producto as P,productoxcarrito as X
where P.id_producto = X.id_producto


Create view VentasxAfiliados (Afiliado,Compras,Total,Servicio)
As Select Afi.Nombre,COUNT(Distinct Car.Num_Carrito),SUM(Prod.Precio*PxC.cantidad),SUM(Prod.Precio*PxC.cantidad)*0.05
From (Carrito as Car Join Productoxcarrito as PxC on Car.Num_Carrito=PxC.ID_Carrito JOIN Producto as Prod on PxC.ID_Producto=Prod.ID_Producto JOIN Afiliado as Afi on Prod.Cedula_A=Afi.Cedula_J)
group By Afi.Nombre

--Para Ejemplo
Insert Into Cliente (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Telefono,Fechanacim,Provincia,Canton,Distrito)
Values ('98984444','John','Doe','None','JHD','1234','12345678','2000-02-12','Guanacaste','Liberia','CaÃ±as Dulces')

Insert into Producto (Cedula_A,Nombre,Categoria,Precio,Picture)
Values ('123456789','Burgir 1','Comida',2500,'something'),('123456789','Burgir2','Comida',999,'something'),('123456789','Pajilla','Comida',1,'something');

Insert Into Carrito(Cedula_C)
Values('98984444')

Insert into ProductoxCarrito(ID_Producto,ID_Carrito,Cantidad)
Values (23,8,2),(25,8,1)


--Reportes por cliente
Create view VentasxCliente (Cliente,Afiliado,Productos,Total,Servicio)
as Select Cli.Nombre, Afi.Nombre, Count(Car.Num_Carrito),SUM(Prod.Precio*PxC.cantidad),SUM(Prod.Precio*PxC.cantidad)*0.05
From (Cliente as Cli join Carrito as Car on Car.Cedula_C=Cli.Cedula Join Productoxcarrito as PxC on Car.Num_Carrito=PxC.ID_Carrito JOIN Producto as Prod on PxC.ID_Producto=Prod.ID_Producto JOIN Afiliado as Afi on Prod.Cedula_A=Afi.Cedula_J)
Group By Cli.Nombre,Afi.Nombre,Car.Num_Carrito
Order By Cli.Nombre;