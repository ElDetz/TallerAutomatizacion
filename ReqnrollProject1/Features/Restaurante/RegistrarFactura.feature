Feature: RegistrarFactura

Background: 
Given Ingreso al ambiente 'https://pruebas-qa.sigesonline.com/'
And Inicio de sesión con usuario 'admin@tintoymadero.com' y contraseña 'calidad'
And Se ingresa al módulo 'Nueva Venta'

@tag1
Scenario: [scenario name]
	Given [context]
	When [action]
	Then [outcome]
