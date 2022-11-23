create view productoview as
select P.id_producto as "ID",P.nombre as "Name", P.cedula_a as "Cedula_a",
X.id_carrito as "ID_Carrito", P.precio as "Price", X.cantidad as "Cantidad"
from producto as P,productoxcarrito as X
where P.id_producto = X.id_producto


drop view productoview


select * from producto

select * from carrito

select * from productoview

insert into productoxcarrito
values(13,2,2)

insert into carrito (cedula_c)
values ('901130289')