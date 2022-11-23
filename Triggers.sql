--TRIGGERS

--Ejemplo levantamiento trigger fecha nacimiento 
Insert into Cliente (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Telefono,FechaNacim,Provincia,Canton,Distrito)
Values ('901130289','Kenichi','Hayakawa','Bolaños','KeniHB','1234','72739181','2023-07-10','Alajuela','San Rafalel','No se');

Update Cliente
Set FechaNacim='2023-07-10'


--Trigger function restricción fecha de nacimiento errónea
create or replace function public.FechaNacim_Restric()
	returns trigger
	language plpgsql
as 
$$
begin
	if ((select FechaNacim from newt)>(select current_date)) then
		raise notice 'La fecha está incorrecta %', (select FechaNacim from newt);
		if (TG_OP='INSERT') then
			delete from Cliente where FechaNacim=(select FechaNacim from newt);
		end if;
		if (TG_OP='UPDATE') then
			update Cliente 
			set fechanacim=(select FechaNacim from oldt) where Cedula=(select Cedula from oldt);
		end if;
	end if;
	return null;
end;
$$;

	--Trigger para insert
create trigger FechaNacim_Restric_ins
after insert
ON public.Cliente 
referencing new table as newt 
for each statement
execute procedure public.FechaNacim_Restric();

	--Trigger Para Update
create trigger FechaNacim_Restric_upd
after update
ON public.Cliente 
referencing new table as newt old table as oldt
for each statement
execute procedure public.FechaNacim_Restric();

-- Ejemplo  levantamiento trigger sin pedidos
Insert into carrito (Num_Carrito,Cedula_C)
Values (1234567890,'901130289')

Insert into pedido (ID_Pedido,ID_Carrito,Cedula_R,Estado,Comprobante,Provincia,Canton,Distrito)
Values(123467890,1234567890,'184002109','Pendiente','Something','Cartago','La Union','San Juan');

--Trigger para la facturación de un carrito que esté vacío
create or replace function Carritovacio_Func()
	returns trigger
	language plpgsql
as
$$
begin
if((select count(*) from productoxcarrito join newt on productoxcarrito.id_carrito=(select id_pedido from newt))=0) then
	raise notice 'Se está intentando facturar un carrito sin productos';
	delete from pedido where ID_Pedido=(select ID_Pedido from newt);
end if;
return null;
end;
$$;

	--Trigger en insert
create trigger CarritoVacio
after insert 
on Pedido
referencing new table as newt
for each statement
execute procedure Carritovacio_Func();


--Trigger para la facturación de un carrito que esté vacío
 create or replace function RechazoSolFunct()
 	returns trigger
	language plpgsql
as
$$
declare Cedula_AA varchar(9);
begin
if((select estado from newt)='rejected') then
	raise notice 'Se rechazó la solicitud %',(select numsol from newt);
	
	--Admin Afiliado
	Cedula_AA=(Select Cedula_A from Afiliado Where Cedula_J=(select Cedula_A from newt));
	
	--Borro el Afiliado
	delete from afiliado
	where Cedula_J=(select Cedula_A from newt);
	
	--Borro Afiliado
	delete from Admin_Afiliado
	where Cedula=Cedula_AA;
	
end if;
return null;
end;
$$;

	--Trigger al actualizar la solicitud
create trigger SolicitudRechazada
after update
on Solicitud_Afiliado
referencing new table as newt
for each statement
execute procedure RechazoSolFunct();


--Ejemplo trigger rechazo solicitud
insert into solicitud_Afiliado(Cedula_A,Cedula_E,Comentario,Estado)
values ('987654321','305360018','','Procesando')

update Solicitud_Afiliado
Set Estado='rejected'
where numsol=1
