Feature: Registro de una nueva venta
  Como vendedor quiero registrar ventas en el sistema SIGES

  Background:
    Given el usuario ingresa al ambiente 'https://taller2025-qa.sigesonline.com/'
    And el usuario inicia sesion con usuario 'admin@plazafer.com' y contrasena 'calidad'
    And accede al modulo 'Venta'
    And accede al submodulo 'Nueva Venta'

  @NuevaVenta
  Scenario: Registrar una venta con pago al contado
    When el usuario agrega el concepto '400000437' 
    And ingresa la cantidad '2'
    And selecciona igv
    And selecciona al cliente con documento '71310154'
    And selecciona el tipo de comprobante 'BOLETA'
    And selecciona el tipo de pago 'Contado'
    And selecciona el medio de pago 'TDEB'
    And ingrese la informacion del pago 'cancelado'
    Then la venta se guarda correctamente

