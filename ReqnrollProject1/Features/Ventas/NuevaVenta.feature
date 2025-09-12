Feature: NuevaVenta

A short summary of the feature
Background: 
Given Inicio de sesión con usuario 'admin@tintoymadero.com' y contraseña 'calidad'
And Se ingresa al módulo 'Nueva Venta'

@NuevaVenta

Scenario: Registro de una nueva venta con pago al contado
	And Agregar concepto: '1010-3'	
	And Ingresar Cantidad '2' y Precio Unitario '2'
	And Activar IGV 'Si'
	And Ingresar Cliente '71310154'
	And Seleccionar Tipo de Comprobante 'NOTA'
	And Seleccionar Tipo de pago 'Contado'
	And Seleccionar Medio de Pago 'TDEB'
	And Ingresar Datos del Pago: 'XD'
	Then Guardar venta 


	