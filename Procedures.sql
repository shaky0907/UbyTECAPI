--Manejo del pedido cuando sale del carrito
create or replace procedure hacerpedido(IDCarrito int)
	language plpgsql
	as 
	$$
	--Información del destino viene del cliente
	declare CedulaCliente varchar(9);
	declare ProvinciaAux varchar(15);
	declare DistritoAux varchar(15);
	declare CantonAux varchar(15);
	
	--Motero, se ordenan por disponibilidad y localización
	declare rider varchar(9);
	

	begin
	CedulaCliente=(Select Cedula_C from carrito where Num_Carrito=IDCarrito);
	raise notice 'Cedula %', CedulaCliente;
	ProvinciaAux=(Select Provincia from Cliente Where Cedula=CedulaCliente);
	DistritoAux=(Select Distrito from Cliente Where Cedula=CedulaCliente);
	CantonAux=(Select Canton from Cliente Where Cedula=CedulaCliente);
	raise notice 'P % C % D%',ProvinciaAux,CantonAux,DistritoAux;
	
		
	
	--Primer filtro distrito
	if((select count(*) from Repartidor where (Disponibilidad='Available' and Distrito=DistritoAux))<>0) then
		rider=(select Cedula from Repartidor where (Disponibilidad='Available' and Distrito=DistritoAux) limit 1);
		--Se inserta en la pedidos
		Insert into pedido (ID_Pedido,ID_Carrito,Cedula_R,Estado,Comprobante,Provincia,Canton,Distrito)
		values(IDCarrito,IDCarrito,rider,'Preparandose','Comprpobante de pago',ProvinciaAux,CantonAux,DistritoAux);
		--Se pone como ocupado al rider
		Update repartidor
		set Disponibilidad='Recogiendo pedido'
		where Cedula=rider;
	
	--Segundo Filtro Canton
	elsif((select count(*) from Repartidor where (Disponibilidad='Available' and Canton=CantonAux))<>0) then
		rider=(select Cedula from Repartidor where (Disponibilidad='Available' and Canton=CantonAux) limit 1);
		--Se inserta en la pedidos
		Insert into pedido (ID_Pedido,ID_Carrito,Cedula_R,Estado,Comprobante,Provincia,Canton,Distrito)
		values(IDCarrito,IDCarrito,rider,'Preparandose','Comprobante de pago',ProvinciaAux,CantonAux,DistritoAux);
		--Se pone como ocupado al rider
		Update repartidor
		set Disponibilidad='Recogiendo pedido'
		where Cedula=rider;	
	
	--Tercer filtro Provincia
	elsif((select count(*) from Repartidor where (Disponibilidad='Available' and Provincia=ProvinciaAux))<>0) then
		rider=(select Cedula from Repartidor where (Disponibilidad='Available' and Provincia=ProvinciaAux) limit 1);	
		--Se inserta en la pedidos
		Insert into pedido (ID_Pedido,ID_Carrito,Cedula_R,Estado,Comprobante,Provincia,Canton,Distrito)
		values(IDCarrito,IDCarrito,rider,'Preparandose','Comprobante de pago',ProvinciaAux,CantonAux,DistritoAux);
		--Se pone como ocupado al rider
		Update repartidor
		set Disponibilidad='Recogiendo pedido'
		where Cedula=rider;	
	
	--Último recurso se asigna cualquier repartidor disponible en el país	
	elsif((select count(*) from Repartidor where (Disponibilidad='Available'))<>0) then
		rider=(select Cedula from Repartidor where Disponibilidad='Available' limit 1);	
		--Se inserta en la pedidos
		Insert into pedido (ID_Pedido,ID_Carrito,Cedula_R,Estado,Comprobante,Provincia,Canton,Distrito)
		values(IDCarrito,IDCarrito,rider,'Preparandose','Comprobante de pago',ProvinciaAux,CantonAux,DistritoAux);
		--Se pone como ocupado al rider
		Update repartidor
		set Disponibilidad='Recogiendo pedido'
		where Cedula=rider;	
	
	else
		raise notice 'No se puede efectuar el pedido ya que no hay repartidores disponibles en el país';
	end if;
	end;
	$$;

--Ejemplo Procedure

--insert into Carrito(Cedula_C)
--values ('901130289')

Update Repartidor
Set Disponibilidad='Available';
delete from pedido;

call hacerpedido(1)
				

--Manejo cuando el afiliado termina de preparar el pedido
create or replace procedure pedidoelaborado(IDPedido int)
	language plpgsql
	as 
	$$
	begin
	--Se actualiza el estado del pedido
	update Pedido
	set Estado='En camino'
	where ID_Pedido=IDPedido;
	--Se actualzia el estado del repartidor
	update Repartidor
	set Disponibilidad='Entregando pedido'
	where Cedula=(Select Cedula_R from pedido where ID_Pedido=IDPedido);
	
	end;
	$$;
--LLamada con el ejemplo anterior
call pedidoelaborado(1)

--Manejo cuando el afiliado termina de preparar el pedido
create or replace procedure pedidoentregado(IDPedido int)
	language plpgsql
	as 
	$$
	begin
	--Se actualiza el estado del pedido
	update Pedido
	set Estado='Finalizado'
	where ID_Pedido=IDPedido;
	--Se actualzia el estado del repartidor
	update Repartidor
	set Disponibilidad='Available'
	where Cedula=(Select Cedula_R from pedido where ID_Pedido=IDPedido);
	
	end;
	$$;
call pedidoentregado(1)