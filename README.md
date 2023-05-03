# EjercicioPersonas
Desarrollo en .Net Core 3.1 de una api con EF
## Explicación
Se sube un .sql con el diseño de la tabla y 100 datos mock para poder hacer pruebas rapidas, Esta pensado para usarse con SQL Server.

Modificar en el appsettings.json para colocar la conecctionstring deseada a usar.

El desarrollo cuenta con swagger para no necesitar de usar postman a la hora de probar cada end-point.

Se requiere de primero generar un token para usarlo en la parte de Personas.

La pruebas unitarias dan falsos positivos debido a la inyeccion de dependencia, se sigue investigando para resolverlo.

