# Historias Clinicas 📖

## Objetivos 📋
Desarrollar un sistema, que permita la administración general de un consultorio (de cara a los administradores): Prestaciones, Profesionales, Pacientes, etc., como así también, permitir a los pacientes, realizar reserva sobre turnos ofrecidos.
Utilizar Visual Studio 2019 preferentemente y crear una aplicación utilizando ASP.NET MVC Core (versión a definir por el docente 3.1 o 6.0).

<hr />

## Enunciado 📢
La idea principal de este trabajo práctico, es que Uds. se comporten como un equipo de desarrollo.
Este documento, les acerca, un equivalente al resultado de una primera entrevista entre el cliente y alguien del equipo, el cual relevó e identificó la información aquí contenida. 
A partir de este momento, deberán comprender lo que se está requiriendo y construir dicha aplicación web.

Lo  primero debra ser comprender al detalle, que es lo que se espera y busca del proyecto, para ello, deben recopilar todas las dudas que tengan entre Uds. y evacuarlas con su nexo (el docente) de cara al cliente. De esta manera, él nos ayudará a conseguir la información ya un poco más procesada. 
Es importante destacar, que este proceso no debe esperar a hacerlo en clase; deben ir contemplandolas independientemente, las unifican y hace una puesta comun dentro del equipo, ya sean de índole funcional o técnicas, en lugar de que cada consulta enviarla de forma independiente, se recomienda que las envien de manera conjunta. 

Al inicio del proyecto, las consultas seran realizadas por correo y deben seguir el siguiente formato:

Subject: [NT1-<CURSO LETRA>-GRP-<GRUPO NUMERO>] <Proyecto XXX> | Informativo o Consulta

Body: 

1.<xxxxxxxx>
2.< xxxxxxxx>

# Ejemplo
**Subject:** [NT1-A-GRP-5] Agenda de Turnos | Consulta

**Body:**

1.La relación del paciente con Turno es 1:1 o 1:N?
2.Está bien que encaremos la validación del turno activo, con una propiedad booleana en el Turno?

<hr />

Es sumamente importante que los correos siempre tengan:
1.Subject con la referencia, para agilizar cualquier interaccion entre el docente y el grupo
2. Siempre que envien una duda o consulta, pongan en copia a todos los participantes del equipo. 

Nota: A medida que avancemos en la materia, las dudas seran canalizadas por medio de Github, y alli tendremos las dudas comentadas, accesibles por todos y el avance de las mismas. 

**Crear un Issue o escribir un nuevo comentario sobre el issue** que se requiere asistencia, siempre arrobando al docente, ejemplo: @marianolongoort


### Proceso de ejecución en alto nivel ☑️
 - Crear un nuevo proyecto en [visual studio](https://visualstudio.microsoft.com/en/vs/) utilizando la template de MVC.
 - Crear todos los modelos definidos y/o detectados por ustedes, dentro de la carpeta Models cada uno en un archivo separado (Modelos anemicos).
 - En el proyecto encararemos y permitiremos solo una herencia entre los modelos anemicos. Comforme avancemos, veremos que en este nivel, que estos modelos tengan una herencia, sera visto como una mala practica, pero es la mejor forma de visualizarlo. Esta unica herencia soportada sera PERSONA como clase base y luego diferentes especializaciones, segun sea el proyecto (Cliente, Alumno, Profesional, etc.).  
 - Sobre dichos modelos, definir y aplica las restricciones necesarias y solicitadas para cada una de las entidades. [DataAnnotations](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1).
 - Agregar las propiedades navegacionales, sobre las relaciones entre las entidades (modelos).
 - Agregar las propiedades relacionales, en el modelo donde se quiere alojar la relacion (entidad dependiente).
 - Crear una carpeta Data que dentro tendrá al menos la clase que representará el contexto de la base de datos (DbContext) en nuestra aplicacion. 
 - Agregar los paquetes necesarios para Incorporar Entity Framework e Identitiy en nuestros proyectos.
 - Crear el DbContext utilizando en esta primera estapa con base de datos en memoria (con fines de testing inicial, introduccion y fine tunning de las relaciones entre modelos). [DbContext](https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-3.1), [Database In-Memory](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=vs).
 - Agregar los DbSet para cada una de las entidades que queremos persistir en el DbContext.
 - Agregar Identity a nuestro poryecto, para facilitar la inclusion de funcionalidades como Iniciar y cerrar sesion, agregado de entidades de soporte para esto Usuario y Roles que nos serviran para aplicar un control de acceso basado en roles (RBAC) basico. 
 - Por medio de Scaffolding, crear en esta instancia todos los CRUD de las entidades a persistir. Luego verificaremos que se mantiene, que se remueve, que se modifica y que debemos agregar.
 - Antes de continuar es importante realizar algun tipo de pre-carga de la base de datos. No solo es requisito del proyecto, sino que les ahorrara mucho tiempo en las pruebas y adecuaciones de los ABM.
 - Testear en detalle los ABM generado, y detectar todas las modificaciones requeridas para nuestros ABM e interfaces de usuario faltantes para resolver funcionalidades requeridas. (siempre tener presente el checklist de evaluacion final, que les dara el rumbo para esto).
 - Cambiar el dabatabase service provider de Database In Memory a SQL. Para aquellos casos que algunos alumnos utilicen MAC, tendran dos opciones para avanzar (adecuar el proyecto, para utilizar SQLLite o usar un docker con SQL Server instalado alli).
 - Aplicar las adecuaciones y validaciones necesarias en los controladores.  
 - Si el proyecto lo requiere, generar el proceso de auto-registración. Es importante aclarar que este proceso estara supeditado a las necesidades de cada proyecto y seguramente para una entidad especifica. 
 - A estas alturas, ya se han topado con varios inconvenientes en los procesos de adecuacion de las vistas y por consiguiente es una buena idea que generen ViewModels para desbloquear esas problematicas que nos estan trayendo los Modelos anemicos utilizados hasta el momento.
 - En el caso de ser requerido en el enunciado, un administrador podrá realizar todas tareas que impliquen interacción del lado del negocio (ABM "Alta-Baja-Modificación" de las entidades del sistema y configuraciones en caso de ser necesarias).
 - El <Usuario Cliente o equivalente> sólo podrá tomar acción en el sistema, en base al rol que que se le ha asignado al momento de auto-registrarse o creado por otro medio o entidad.
 - Realizar todos los ajustes necesarios en los modelos y/o funcionalidades.
 - Realizar los ajustes requeridos desde la perspectiva de permisos y validaciones.
 - Todo lo referido a la presentación de la aplicaión (cuestiones visuales).
 
<hr />

 Nota: Para la pre-carga de datos, las cuentas creadas por este proceso, deben cumplir las siguientes reglas:
 1. La contraseña por defecto para todas las cuentas pre-cargadas será: Password1!
 2. El UserName y el Email deben seguir la siguiente regla:  <classname>+<rolname si corresponde diferenciar>+<indice>@ort.edu.ar Ej.: cliente1@ort.edu.ar, empleado1@ort.edu.ar, empleadorrhh1@ort.edu.ar

## Entidades 📄

- Persona
- Paciente
- Medico
- Empleado
- HistoriaClinica
- Episodio
- Evolucion
- Notas
- Epicrisis
- Diagnostico

`Importante: Todas las entidades deben tener su identificador unico. Id`

`
Las propiedades descriptas a continuación, son las minimas que deben tener las entidades. Uds. pueden agregar las que consideren necesarias.
De la misma manera Uds. deben definir los tipos de datos asociados a cada una de ellas, como así también las restricciones.
`

**Persona**
```
- UserName
- Password
- Email
- FechaAlta
```

**Paciente**
```
- Nombre
- Apellido
- DNI
- Telefono
- Direccion
- FechaAlta
- Email 
- ObraSocial
- HistoriaClinica
```

**Medico**
```
- Nombre
- Apellido
- DNI
- Telefono
- Direccion
- FechaAlta
- Email
- Matricula
- Especialidad
```

**Empleado**
```
- Nombre
- Apellido
- DNI
- Telefono
- Direccion
- FechaAlta
- Email
- Legajo
```

**HistoriaClinica**
```
- Paciente
- Episodios
```

**Episodio**
```
- Motivo
- Descripcion
- FechaYHoraInicio
- FechaYHoraAlta
- FechaYHoraCierre
- EstadoAbierto
- Evoluciones
- Epicrisis
- EmpleadoRegistra
```

**Evolucion**
```
- Medico
- FechaYHoraInicio
- FechaYHoraAlta
- FechaYHoraCierre
- DescripcionAtencion
- EstadoAbierto
- Notas 
```

**Nota**
```
- Evolucion
- Empleado
- Mensaje
- FechaYHora
```

**Epicrisis**
```
- Episodio
- Medico
- FechaYHora 
- Diagnostico
```

**Diagnostico**
```
- Epicrisis
- Descripcion
- Recomendacion
```


**NOTA:** aquí un link para refrescar el uso de los [Data annotations](https://www.c-sharpcorner.com/UploadFile/af66b7/data-annotations-for-mvc/).

<hr />

## Caracteristicas y Funcionalidades ⌨️
`Todas las entidades, deben tener implementado su correspondiente ABM, a menos que sea implicito el no tener que soportar alguna de estas acciones.`

`IMPORTANTE: Ninguna entidad en el circuito de atención medica, puede ser modificado o eliminado una vez que se ha creado. Ej. No se puede Eliminar una Historia Clinica, No se puede modificar una nota de una evolución, etc.`

**General**
- Los Pacientes pueden auto registrarse.
- La autoregistración desde el sitio, es exclusiva para los pacientes. Por lo cual, se le asignará dicho rol.
- Los empleados, deben ser agregados por otro Empleado. Lo mismo, para los Medicos.
	- Al momento, del alta del empleados y medicos, se le definirá un username y la password será definida por el sistema.
    - También se le asignará a estas cuentas el rol de empleado y/o medico según corresponda.

**Paciente**
- Un paciente puede consultar su historia clinica, con todos los detalles que la componen, en modo solo visualización.
- Puede acceder a los episodios, y por cada episodio, ver las evoluciones que se tienen, con sus detalles.
- Puede actualizar datos de contacto, como el telefono, dirección,etc.. Pero no puede modificar su DNI, Nombre, Apellido, etc.

**Empleado**
- Un empleado, puede modificar todos los datos de los pacientes. 
-- No puede quitar o asociar una nueva Historia Clinica a los pacientes.
- El Empleado puede listar todos los pacientes, y por cada uno, ver en sus detalles, la HistoriaClinica que tiene asociada y si tiene episodios abiertos. 
- El Empleado, puede crear un paciente, un empleado, y un medico. Cada uno de ellos, con su correspondientes datos requeridos y usuario.
- El Empleado, puede crear un Episodio para el Paciente, en la Historia Clinica del paciente.
-- Pero no puede hacer más nada, que crearlo con su Motivo y Descripción.

**Medico**
- Un Medico, puede crear evoluciones, en Episodios que esten en estado abierto.
-- Para ello, buscará al paciente, accederá a su Historia Clinica -> Episodio -> Crear la Evolución.
- Un medico puede cerrar una evlución, si se han completado todos los campos. El campo de FechaYHoraCierre, se guardará automaticamente. 
-- Un Empleado o Medico, pueden cargar notas en cada evolución según sea necesario.
-- Las notas pueden continuar agregandose, luego del cierre de la evolución.
- Puede cerrar un Episodio, pero para hacer esto, el sistema realizará ciertas validaciones.

**HistoriaClinica**
- La misma se crea automaticamente con la creación de un paciente.
-- No se puede eliminar, ni realizar modificaciones posteriores.
-- El detalle internos de la misma, para los Medicos y empleados, pero dependiendo del rol, es lo que podrán hacer.
-- El paciente propietario de la HC, es el unico paciente que puede ver la HC.

- Por medio de la HC, se podrá acceder a la lista de Episodios, que tenga relacionados.

**Epidodio**
- La creación de un Episodio en una HC, solo puede realizarla un empleado.
-- El empleado, deberia acceder a un Paciente -> HC -> Crear Episodio, e ingresará:
--- Motivo. Ej. Traumatismo en pierna Izquierda.
--- Descripción. Ej. El paciente se encontraba andando en Skate y sufrió un accidente.
- El episodio se:
-- Creará en estadoAbierto automaticamente
-- Con una FechaYHoraInicio también, de forma automática.
-- Con un Empleado, como el que creó el episodio. (persona en recepción, que recibe al paciente).

- Solo un medico puede cerrar un Episodio, para hacer esto, el sistema, validará:
-- 1. Que el Episodio, no tenga ninguna Evlución en estado Abierta o no tenga evoluciones. Si fuese así, deberá mostrar un mensaje.
-- 2. Cargará el Medico manualmente la FechaYHoraAlta (alta del episodio) del paciente.
-- 3. Le pedirá que cargue una Epicrisis, con su diagnostico y recomendaciones.
--- Una vez finalizado el diagnostico, el Episodio, pasará a esatr en estado Cerrado.
-- 4. La FechaYHoraCierre, será cargada automaticamente, si se cumplen los requerimientos previos.

Nota: Si el cierre del episodio, es por la condición sin evoluciones, se generará un "Cierre Administrativo", en el cual, el sistema, cargará una epicrisi, con alguna información que el empleado ingresará para dejar registro de que fue un cierre administrativo. Ej. El paciente realizó el ingreso y antes de ser atendido, se fué. 

**Evolucion**
- Una evolución, solo la puede crear y gestionar un Medico.
-- La unica excepción, es que un empleado, puede cargar notas en Evoluciones por cuestiones administrativas. Ej. Salvo, que el alta del paciente en la evolución, es 10/08/2020
- Automaticamente al crear una evolución se cargará:
-- Medico que la esta creando
-- FechaYHoraInicio
-- EstadoAbierto
-- FechaYHoraCierre (Cuando se registre el cierre)
- Manualmente:
-- La FechaYHoraAlta
-- DescripcionAtencion
-- Notas (Las que sean necesarias)

- Para cerrar una evolución, se deben haber cargado todos los datos manuales requeridos, y solo lo puede hacer un Medico.

**Nota**
- La nota pertenece a una evolución. 
-- Para crearla, uno solo puede hacerla desde una Evolución.
- En las notas, puede cargar un mensaje cualquier empleado o medico.
- Quedará automaticmente la fecha y hora, como asi también, quien es el que la cargó.


**Epicrisis**
- La epicrisis, pertenes a un Episodio.
-- Solo puede haber una epicrisis por episodio.
-- Para poder crearla, todas las evoluciones, deben estar cerradas.
-- El Episodio debe estar abierto, y al finalizar este proceso, de estar todo ok, se debe cerrar automaticamente.
-- La epicrisis, solo debe poder cargarla un Medico.
-- La excepción, es la creación automatica, si cierra un empleado, por proceso administrativo.
-- La FechayHora, se carga automaticamente
-- El Diagnostico, de forma Manual.

**Diagnostico**
- Pertenece a una Epicrisis. 
- Se cargará una descripcion de forma manual
- También se cargará una recomendacion.


**Aplicación General**
- Información institucional.
- Se deben listar el cuerpo medico, junto con sus especialidades.
- Los accesos a las funcionalidades y/o capacidades, debe estar basada en los roles que tenga cada individuo.
