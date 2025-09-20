Feature: Registro de una nueva venta
  Como vendedor
  Quiero registrar ventas en el sistema SIGES

  Background:
    Given el usuario ingresa al ambiente 'https://taller2025-qa.sigesonline.com/'
    And el usuario inicia sesión con usuario 'admin@plazafer.com' y contraseña 'calidad'
    And accede al módulo 'Venta'
    And accede al submódulo 'Nueva Venta'

  @NuevaVenta
  Scenario: Registrar una venta con pago al contado
    When el usuario agrega el concepto '1010-3' 
    And ingresa la cantidad '2'
    And selecciona al cliente con documento '71310154'
    And selecciona el tipo de comprobante 'BOLETA'
    And selecciona el tipo de pago 'Contado'
    Then la venta se guarda correctamente


	