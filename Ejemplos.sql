--Insersión Para ejemplos
--Insertar dos Clientes
Insert into Cliente (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Telefono,FechaNacim,Provincia,Canton,Distrito)
Values ('901130289','Kenichi','Hayakawa','Bolaños','KeniHB','1234','72739181','2002-07-10','Alajuela','San Rafael','No se');

Insert into Cliente (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Telefono,FechaNacim,Provincia,Canton,Distrito)
Values ('111111111','Lionel','Messi','Cuccittini','Messi10','1234','10101010','1987-06-24','Puntarenas','Buenos Aires','Buenos Aires');

Insert into Cliente (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Telefono,FechaNacim,Provincia,Canton,Distrito)
Values ('111111112','Cristiano','Ronaldo','Dos Santos','CR7','1234','77777777','1985-02-05','San José','San José','Mata Redonda');


--Insertar Repartidor Ejemplo
Insert into Repartidor (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Correo,Provincia,Canton,Distrito,Disponibilidad)
Values ('184002109','David','De la Hoz','Aguirre','DavHA','1234','Divadp0907@gmail.com','San Jose','Santa Ana','Pozos','Available');

Insert into Repartidor (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Correo,Provincia,Canton,Distrito,Disponibilidad)
Values ('222222220','Marcos','Gonzales','Araya','MGA','1234','MGonzales@gmail.com','Puntarenas','Coto Brus','San Vito','Available');

Insert into Repartidor (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Correo,Provincia,Canton,Distrito,Disponibilidad)
Values ('222222221','Neymar','Da Silva','Santos','Ney11','1234','Neyjr@gmail.com','San Jose','San José','Zapote','Available');

Insert into Repartidor (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Correo,Provincia,Canton,Distrito,Disponibilidad)
Values ('222222222','Kylian','Mbappé','Lottin','PSG18','1234','WCchamp@gmail.com','Puntarenas','Buenos Aires','Buenos Aires','Available');

Insert into Repartidor (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Correo,Provincia,Canton,Distrito,Disponibilidad)
Values ('222222223','Harry','Edward','Kane','Tot','1234','Vivalareina@gmail.com','Puntarenas','Buenos Aires','Boruca','Available');

Insert into Telefonos_Repartidor(Cedula_R,Telefono)
Values ('184002109','84848484'),('222222220','53215321'),('222222220','84568456'),('222222221','12312344'),('222222222','96359635'),('222222223','74157415');


--Insertar Empleado Ejemplo
Insert into Empleado (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Provincia,Canton,Distrito,Profile_Pic)
Values ('305360018','Marcelo','Truque','Montero','MarcTM','1234','Cartago','La Unión','San Juan','Mi foto');

Insert into Empleado (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Provincia,Canton,Distrito,Profile_Pic)
Values ('333333331','San Keylor','Navas','Gamboa','KNavas','1234','San Jose','Tibás','San Juan','Hombre de Fe');

Insert into Empleado (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Provincia,Canton,Distrito,Profile_Pic)
Values ('333333332','Mariano','Nestor','Torres','Sap20','1234','Guanacaste','Nicoya','Nicoya','Argentino123');

Insert into Telefonos_Empleado(Cedula_E,Telefono)
Values ('305360018','60593902'),('333333331','60606060'),('333333332','88888888');


--Insaertar Admin afiliado Ejemplo
Insert into Admin_Afiliado(Cedula,Nombre, Apellido1,Apellido2,Correo,Usuario,Contra,Provincia,Canton,Distrito)
Values ('123456789','Marco','Rivera','Meneses','Profemarcos@tec.cr','MarcRM','1234','Cartago','Central','San Nicolás');

Insert into Admin_Afiliado(Cedula,Nombre, Apellido1,Apellido2,Correo,Usuario,Contra,Provincia,Canton,Distrito)
Values ('444444440','Luisito','Suaréz','Díaz','LuisitoMuerde@tec.cr','MSN','1234','San Jose','San Jose','Carmen');
	--Insertar Telefonos para cada admin

Insert into Telefonos_Admin_Afiliado(Cedula_A,Telefono)
Values('123456789','85858585'),('123456789','86858585'),('444444440','20202020');

--Insertar Afiliados Ejemplo
Insert into Afiliado(Cedula_J,Cedula_A,ID_Tipo,Nombre,Correo,SINPE,Banner,Provincia,Canton,Distrito)
Values ('555555550','123456789',1,'Arcos Dorados','hambur@algo.com','88881111','Arcos Dorados','San Jose','San Jose','Mata Redonda');

Insert into Afiliado(Cedula_J,Cedula_A,ID_Tipo,Nombre,Correo,SINPE,Banner,Provincia,Canton,Distrito)
Values ('555555551','444444440',4,'FANAL','Licor@algo.com','69690420','Something','San Jose','Curridabat','Cipreses');
	
	--SolicitudesAfiliados
Insert into Solicitud_Afiliado(Cedula_A,Cedula_E,Comentario,Estado)
values ('555555550','333333331','','accepted'),('555555551','333333331','','accepted');
	
	--Insertar telefonos a afiliados
Insert into Telefonos_Afiliado(Cedula_J_A,Telefono)
Values ('555555550','70707070'),('555555550','80808080'),('555555551','54545454');

--Insertar Productos
Insert into Producto(Cedula_A,Nombre,Categoria,Precio)
Values ('555555550','Big Mac','Comida',5000);

Insert into Producto(Cedula_A,Nombre,Categoria,Precio)
Values ('555555550','Quesoburguesa','Comida',2500);

Insert into Producto(Cedula_A,Nombre,Categoria,Precio)
Values ('555555550','Papas','Acompañamiento',500);

Insert into Producto(Cedula_A,Nombre,Categoria,Precio)
Values ('555555550','CocaCola','Refresco',500);

Insert into Producto(Cedula_A,Nombre,Categoria,Precio)
Values ('555555551','CocaCola','Refresco',1200);

Insert into Producto(Cedula_A,Nombre,Categoria,Precio)
Values ('555555551','Cacique','Bebida Alcoholica',5000);

Insert into Producto(Cedula_A,Nombre,Categoria,Precio)
Values ('555555551','Imperial','Bebida Alcoholica',1000);
