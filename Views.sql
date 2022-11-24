create view productoview as
select P.id_producto as "ID",P.nombre as "Name", P.cedula_a as "Cedula_a",
X.id_carrito as "ID_Carrito", P.precio as "Price", X.cantidad as "Cantidad"
from producto as P,productoxcarrito as X
where P.id_producto = X.id_producto


Create view VentasxAfiliados (Afiliado,Compras,Total,Servicio)
As Select Afi.Nombre,COUNT(Distinct Car.Num_Carrito),SUM(Prod.Precio*PxC.cantidad),SUM(Prod.Precio*PxC.cantidad)*0.05
From (Carrito as Car Join Productoxcarrito as PxC on Car.Num_Carrito=PxC.ID_Carrito JOIN Producto as Prod on PxC.ID_Producto=Prod.ID_Producto JOIN Afiliado as Afi on Prod.Cedula_A=Afi.Cedula_J)
group By Afi.Nombre




drop view productoview


select * from producto

select * from carrito

select * from productoview

insert into productoxcarrito
values(13,2,2)

insert into carrito (cedula_c)
values ('901130289')