--Drop Table Admin_Afiliado,Afiliado,Carrito,Cliente,Empleado,Fotos_Producto,Pedido,Producto,ProductoxCarrito,Repartidor,Solicitud_Afiliado,Telefonos_Admin_Afiliado,Telefonos_Afiliado,Telefonos_Empleado,Telefonos_Repartidor,Tipo_Comercio
-- Tabla cliente
create table Cliente(
	Cedula 		varchar(9) 	primary key,
	Nombre 		varchar(40),
	Apellido1 	varchar(40),
	Apellido2 	varchar(40),
	Usuario 	varchar(40),
	Contra		varchar(30),
	Telefono	varchar(8),
	FechaNacim	date,
	Provincia	varchar(15),
	Canton		varchar(20),
	Distrito	varchar(20)
);

create table Carrito(
	Num_Carrito	serial	primary key,
	Cedula_C	varchar(9)  --Foreign key Cliente
);

--Tabla repartidor
create table Repartidor(
	Cedula			varchar(9)		primary key,
	Nombre			varchar(40),
	Apellido1 		varchar(40),
	Apellido2		varchar(40),
	Usuario			varchar(40),
	Contra			varchar(30),
	Correo			varchar(50),
	Provincia		varchar(15),
	Canton			varchar(20),
	Distrito		varchar(20),
	Disponibilidad	varchar(20),
	Profilepic		text
);

--Tabla pedido
create table Pedido(
	ID_Pedido		int	primary key,
	ID_Carrito		int, --Foreign key Carrito
	Cedula_R		varchar(9), --Foreign Key Repartidor
	Estado			varchar(20),
	Comprobante		varchar(30),
	Provincia		varchar(15),
	Canton			varchar(20),
	Distrito		varchar(20)	
);

--Tabla Telefonos
create table Telefonos_repartidor(
	Cedula_R	 	varchar(9), -- Foreign key Repartidor
	Telefono		varchar(8) primary key
);

--Tabla Admin Afiliado
create table Admin_Afiliado(
	Cedula 		varchar(9) primary key,
	Nombre 		varchar(40),
	Apellido1 	varchar(40),
	Apellido2 	varchar(40),
	Correo		varchar(50),
	Usuario 	varchar(40),
	Contra		varchar(30),
	Provincia	varchar(15),
	Canton		varchar(20),
	Distrito	varchar(20),
	Profilepic	text
);

--Telefonos Admin_Afiliado
create table Telefonos_Admin_Afiliado(
	Cedula_A 	varchar(9) , -- Foreign key Admin_Afiliado
	Telefono	varchar(8) primary key
);

--Tabla tipos de comercio
create table Tipo_Comercio(
	ID_Tipo	serial primary key,
	Nombre	varchar(15)
);

--Tabla Afiliado

create table Afiliado(
	Cedula_J	varchar(9) 	primary key,
	Cedula_A	varchar(9) 	default '123456789',-- foreign key Admin afiliado
	ID_Tipo		int			default 1, -- foreign key Tipo Comercio
	Nombre		varchar(50),
	Correo		varchar(50),
	Sinpe		varchar(8),
	Banner		text, --Revisar tipo de datos	
	Provincia	varchar(15),
	Canton		varchar(20),
	Distrito	varchar(20)
);

--Telefonos Afiliado
create table Telefonos_Afiliado(
	Cedula_J_A 	varchar(9), -- Foreign key Afiliado
	Telefono	varchar(8) primary key
);

-- Tabla Producto
create table Producto(
	ID_Producto	serial	primary key,
	Cedula_A	varchar(9),--Foreign key afiliado
	Nombre		varchar(40),
	Categoria	varchar(40),
	Precio		int
);

-- Fotos productos
create table Fotos_Producto(
	ID_Producto int , --foreign key Producto
	Foto		text primary key	 -- Revisar tipo de dato
);

-- Tabla empleado
create table Empleado (
	Cedula 		varchar(9) 	primary key,
	Nombre 		varchar(40),
	Apellido1 	varchar(40),
	Apellido2 	varchar(40),
	Usuario 	varchar(40),
	Contra		varchar(30),
	Provincia	varchar(15),
	Canton		varchar(20),
	Distrito	varchar(20),
	Profile_pic	text --Revisar tipo de dato
);

--Tabla telefonos empleado

create table Telefonos_Empleado(
	Cedula_E	varchar(9) , -- Foreign key Empleado
	Telefono	varchar(8) primary key
);

create table Solicitud_Afiliado(
	NumSol		serial primary key,
	Cedula_A	varchar(9),-- Foreign key Afiliado
	Cedula_E	varchar(9),-- Foreign key Empleado
	Comentario	varchar(120),
	Estado		varchar(10)
);

create table ProductoxCarrito(
	ID_Producto int not null,
	ID_Carrito	int not null,
	Cantidad	int
);

--Foreign keys Telofonos repartidor
	-- Foreign key Telefonos repartidor repartidor
Alter table Telefonos_repartidor
Add constraint TelRep_Rep_FK
Foreign key (Cedula_R) references Repartidor(Cedula) on delete cascade on update cascade;


--Foreign keys Carrito
	-- Foreign key Carrito cliente
Alter table Carrito
Add constraint Client_Carrito_FK
Foreign key (Cedula_C) references Cliente(Cedula) on delete cascade on update cascade;

--Foreign Keys Pedido
	--Foreign key Pedido Carrito
Alter table Pedido
Add constraint Pedido_Carrito_FK
Foreign key (ID_Carrito) references Carrito(Num_Carrito) on delete set null on update cascade;

	--Foreign key Pedido Repartidor
Alter table Pedido
Add constraint Pedido_Repartidor_FK
Foreign key (Cedula_R) references Repartidor(Cedula) on delete set null on update cascade;

--Foreign Keys ProductosxCarrito
	--Foreign Key ProdxCarr Carrito
Alter table ProductoxCarrito
Add constraint PxC_Carrito_FK
Foreign key (ID_Carrito) references Carrito(Num_Carrito) on delete set null on update cascade;
	--Foreign Key ProdxCarr Producto
Alter table ProductoxCarrito
Add constraint PxC_Producto_FK
Foreign key (ID_Producto) references Producto(ID_Producto) on delete set null on update cascade;

--Foreign Keys Fotos Producto
	--Foreign Key ProdxCarr Carrito
Alter table Fotos_Producto
Add constraint Fotos_Producto_FK
Foreign key (ID_Producto) references Producto(ID_producto) on delete cascade on update cascade;

--Foreign Keys  Producto
	--Foreign Key Producto Afiliado
Alter table Producto
Add constraint Producto_Afiliado_FK
Foreign key (Cedula_A) references Afiliado(Cedula_J) on delete cascade on update cascade;

--Foreign Keys Afiliado
	--Foreign Key Tipo-Afiliado
Alter table Afiliado
Add constraint Afiliado_TipoC_FK
Foreign key (ID_Tipo) references Tipo_Comercio(ID_Tipo) on delete set default on update cascade;

	--Foreign Key Afiliado-Cedula Admin
Alter table Afiliado
Add constraint Afiliado_AdminA_FK
Foreign key (Cedula_A) references Admin_Afiliado(Cedula) on delete set default on update cascade;

--Foreign Keys Telefonos Afiliado
	--Foreign Key Telefonos-Afiliado
Alter table Telefonos_Afiliado
Add constraint TelAfiliado_Afiliado_FK
Foreign key (Cedula_J_A) references Afiliado(Cedula_J) on delete cascade on update cascade;

--Foreign Keys Telefonos Afiliado
	--Foreign Key Telefonos-AdminAfiliado
Alter table Telefonos_Admin_Afiliado
Add constraint TelAAfiliado_AAfiliado_FK
Foreign key (Cedula_A) references Admin_Afiliado(Cedula) on delete cascade on update cascade;

--Foreign Keys Telefonos Empleado
	--Foreign Key Telefono-Empleado
Alter table Telefonos_Empleado
Add constraint TelEmpleado_Empleado_FK
Foreign key (Cedula_E) references Empleado(Cedula) on delete cascade on update cascade;

--Foreign Keys Solicitud Afiliado
	--Foreign Key Solicitud-Afiliado
Alter table Solicitud_Afiliado
Add constraint SolA_Afiliado_FK
Foreign key (Cedula_A) references Afiliado(Cedula_J) on delete set null on update cascade;






	

