--CREAR SERIAL
--Insertar tipo comercio 
Insert into Tipo_Comercio (Nombre)
Values ('Resturante'),('Supermercado'),('Farmacia'),('Licoreria');


--Insertar Repartidor Ejemplo
Insert into Repartidor (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Correo,Provincia,Canton,Distrito,Disponibilidad)
Values ('184002109','David','De la Hoz','Aguirre','DavHA','1234','David@gmail.com','San Jose','Santa Ana','Pozos','Available');


--Insertar Cliente Ejemplo
Insert into Cliente (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Telefono,FechaNacim,Provincia,Canton,Distrito)
Values ('901130289','Kenichi','Hayakawa','Bolaños','KeniHB','1234','72739181','2002-07-10','Alajuela','San Rafael','No se');

--Insertar Empleado Ejemplo
Insert into Empleado (Cedula,Nombre,Apellido1,Apellido2,Usuario,Contra,Provincia,Canton,Distrito,Profile_Pic)
Values ('305360018','Marcelo','Truque','Montero','MarcTM','1234','Cartago','La Unión','San Juan','Mi foto');

--Insaertar Admin afiliado Ejemplo
Insert into Admin_Afiliado(Cedula,Nombre, Apellido1,Apellido2,Correo,Usuario,Contra,Provincia,Canton,Distrito)
Values ('123456789','Marco','Rivera','Meneses','Profemarcos@tec.cr','MarcRM','1234','Cartago','Central','San Nicolás');

--Insertar Afiliado Ejemplo
Insert into Afiliado(Cedula_J,Cedula_A,ID_Tipo,Nombre,Correo,SINPE,Banner,Provincia,Canton,Distrito)
Values ('987654321','123456789',4,'Hamburguesas','hambur@algo.com','88881111','Arcos Dorados','Alajuela','San Rafael','No se');
