# ADR-0002 — Mercado Pago para Split Payments

**Estado:** Aceptada con validaciones obligatorias
**Fecha:** 2026-11-24

## Contexto

Cada checkout agrupa cuotas de una sola disciplina y debe distribuir fondos entre la cuenta receptora y la plataforma, sin custodia directa del dinero por TuCuota.

## Decisión

Seleccionar Mercado Pago y su modelo Marketplace Split Payments 1:1. Cada vendedor deberá autorizar la aplicación mediante OAuth. La modalidad concreta entre Checkout Pro, Checkout API o Bricks se decidirá después de una prueba técnica.

## Condiciones obligatorias

- Validar disponibilidad comercial y contractual para Argentina y para las cuentas de los clubes o disciplinas.
- Comprobar KYC, OAuth, renovación y revocación segura de tokens por vendedor.
- Confirmar que `marketplace_fee` o `application_fee` implementa correctamente la comisión aprobada.
- Verificar el orden real de descuentos: Mercado Pago informa que su comisión se descuenta del vendedor antes de la comisión del marketplace.
- Validar pagos agrupados, webhooks, idempotencia, conciliación, contracargos y estados inciertos.
- Probar devoluciones totales y por cuotas completas. La documentación advierte que un reembolso total puede depender del saldo disponible del vendedor.
- No habilitar cobros reales hasta satisfacer DEC-170.

## Consecuencias

La pasarela queda seleccionada, pero su adecuación financiera no queda certificada. Cualquier incompatibilidad con distribución de costos, reembolsos o Split Payment deberá resolverse mediante aprobación explícita; no se modificará silenciosamente el modelo de negocio.

## Referencias oficiales

- [Requisitos de Split Payments 1:1](https://www.mercadopago.com.ar/developers/es/docs/split-payments/split-1-1/prerequisites)
- [Integración del marketplace](https://www.mercadopago.com.ar/developers/es/docs/split-payments/split-1-1/integration-configuration/integrate-marketplace)
- [Configuración y OAuth](https://www.mercadopago.com.ar/developers/es/docs/split-payments/split-1-1/integration-configuration/create-configuration)
- [Webhooks](https://www.mercadopago.com.ar/developers/es/docs/split-payments/additional-content/your-integrations/notifications/webhooks)

