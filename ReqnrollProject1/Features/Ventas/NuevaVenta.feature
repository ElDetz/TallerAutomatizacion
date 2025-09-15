Feature: NuevaVenta

Registrar una venta con tipo de pago al contado
Background: 
Given Inicio de sesión con usuario 'admin@plazafer.com' y contraseña 'calidad'
And Se ingresa al módulo 'Nueva Venta'

@NuevaVenta

Scenario: Registro de una nueva venta con pago al contado
	And Agregar concepto: '1010-3'
	And Ingreso la cantidad '2'
	And Activar IGV 'Si'
	And Ingresar Cliente '71310154'
	And Seleccionar Tipo de Comprobante 'BOLETA'
	And Seleccionar Tipo de pago 'Contado'
	And Seleccionar Medio de Pago 'TDEB'
	And Ingresar Datos del Pago: 'NRO 5'
	Then Guardar venta 


	