Feature: TomarAtencion

Background: 
Given Ingreso al ambiente 'https://pruebas-qa.sigesonline.com/'
And Inicio de sesión con usuario 'admin@tintoymadero.com' y contraseña 'calidad'
And Se ingresa al módulo 'Restaurante'
And Se ingresa al submódulo 'Atención'

@ItemDeUnaOrdenAtencionConMesa
Scenario: CP0002 - Registro de orden con ítem y cantidad positiva
	Given Se selecciona el ambiente 'PRINCIPAL'
	And Seleccion de la mesa '1' en estado 'disponible'
	And Se selecciona el mozo 'DIEGO EDUARDO CRUZ ORELLANA'
	When Se ingresa las siguientes ordenes:
	| Orden		| Concepto									| Cantidad	| Anotacion			|
	| ITEM		| CARTA 1/4 POLLO A LA BRASA C/PT			| 1			| Sin ensalada		|
	
	Then Se procede a 'guardar' la orden ''