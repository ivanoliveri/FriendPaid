
<!-- saved from url=(0067)https://raw.github.com/ivanoliveri/FriendPaidViejo/master/README.md -->
<html><head><meta http-equiv="Content-Type" content="text/html; charset=UTF-8"></head><body><pre style="word-wrap: break-word; white-space: pre-wrap;">FriendPaid
==========

Resumen
-------

Es la aplicación ideal para repartir gastos dentro de grupos de amigos. Se puede aplicar fácilmente desde asados hasta vacaciones. Un usuario puede formar parte de varios grupos de amigos, cada grupo tiene uno o varios administradores.

Está pensada para ser una aplicación pincipalmente mobile aunque tambien contará con plataforma web.

OAuth: Sería recomendable que los usuarios se puedan loguear con sus cuentas de Facebook y Twitter así cuando crean un grupo pueden invitar a sus amigos o seguidores respectivamente

Data
----

Se usa mongoDB (NoSQL) hosteada en mongolab ( https://mongolab.com ).

User:ivanoliveri
Password:tongas123

Se cuenta con 2 colecciones: Miembro y Grupo.


Escenarios De Uso
-----------------

*Escenario de Uso: Compra de Artículo*

Un miembro del grupo, realiza la compra de un artículo seleccionando si todo el grupo se ve afectado por esa compra o solo algunos miembros. Adeemás se be ingresar la descripción y el precio del artículo comprado. Una vez registrada la compra le llega automáticamente una notificación a los miembros del grupo que deben pagarle.

*Escenario de Uso: Pago de Deuda*

Un miembro del grupo le pagó quien compró el artículo en la situación anterior y desea que quede registrado en nuestra aplicación. Una vez que registra el pago en la aplicación se actualiza el _estado del Pago realizado (pasa de estar NoPagado a estar Pagado) y se le envia una notificación avisandole del pago a quien compró el artículo en la situación anterior.

*Escenario de Uso: Creación de Grupo*

El usuario que crea el grupo automáticamente se convierte en el administrador del mismo. Para su creación sólo es requerido un nombre del grupo. Si el usuario que crea el grupo está logueado mediante Twitter o Facebook puede seleccionar a sus amigos o seguidores. En caso contrario o si desean invitar a algún amigo que no se encuentre en las redes sociales anteriormente mencionadas se les brindará opción de enviar un mail invitándolo a usar FriendPaid.

*Escenario de Uso: Administrador Abandona el Grupo (Habiendo un solo administrador)*

En el caso de que el grupo cuente solo con un administrador, se nombrará administrador al miembro con más antiguedad del grupo.
</pre></body></html>