Feature: TomarAtencion

Background: 
Given Ingreso al ambiente 'https://pruebas-qa.sigesonline.com/'
And Inicio de sesión con usuario 'admin@tintoymadero.com' y contraseña 'calidad'
And Se ingresa al módulo 'Restaurante'
And Se ingresa al submódulo 'Atención'

@tag1
Scenario: CP0072 - Agregar ítem a la orden con una cantidad negativa '-1'
	Given Se seleciona el tipo de atencion 'Con mesa'
	And Se selecciona el ambiente 'PRINCIPAL'
	And Seleccion de la mesa '1' en estado 'disponible'
	And Se selecciona el mozo 'DIEGO EDUARDO CRUZ ORELLANA'
	When Se ingresa las siguientes ordenes:
	| Orden		| Concepto									| Cantidad	| Anotacion			|
	| ITEM		| CARTA 1/4 POLLO A LA BRASA C/PT			| -1		|					|

	Then Se procede a 'guardar' la orden ''
