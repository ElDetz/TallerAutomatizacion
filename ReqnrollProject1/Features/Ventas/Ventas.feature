Feature: Registrar una venta

Registrar una venta normal, venta en modo caja y venta por contigencia, cada uno con sus distintos escenarios.

Background: 
Given Inicio de sesi�n con usuario 'admin@tintoymadero.com' y contrase�a 'calidad'


@NuevaVenta

Scenario: Registro de una nueva venta con pago al contado
	And Se ingresa al m�dulo 'Nueva Venta'
	