# Registro de decisiones — evidencia original

Se conserva cada registro completo, incluyendo preguntas posteriores y ejemplos; estos no son requisitos aprobados.

## DEC-001

### Registro 49 — Arquitectura multiclub
Fuente: `4b3c22c0-989e-47f1-9644-fda0fee11637`

Respuesta previa del usuario:

Para la pregunta 1 del relevamiento SDD elijo la opción B: plataforma multiclub desde el inicio, con Obras como piloto.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 1 — Confirmada**
</row>

La plataforma será **multiclub desde el inicio**, con Obras Mendoza como primer club piloto.

Esto significa que cada club tendrá sus propios jugadores, categorías, cuotas, pagos, administradores y configuración, sin poder acceder a la información de otros clubes.

## Registro SDD — Decisión 001

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    **DEC-001 — Arquitectura multiclub**
    <badge color="success">Aprobada</badge>
  </row>
  **Requerimiento:** El sistema debe permitir administrar múltiples clubes independientes desde una misma plataforma.

  **Reglas iniciales:**
  - RN-001: Cada club tendrá un identificador único.
  - RN-002: Los datos de un club deben estar aislados de los demás.
  - RN-003: Cada club tendrá su propio portal público personalizado.
  - RN-004: Obras Mendoza será el primer club piloto.
  - RN-005: La plataforma debe permitir incorporar nuevos clubes sin modificar el código.

  <text color="secondary" size="xs">La estrategia técnica de aislamiento de datos se definirá durante el relevamiento de arquitectura.</text>
</box>

---

## Fase 1 — Modelo de negocio

<text color="secondary" weight="medium" size="xs">Pregunta 2 — Incorporación de nuevos clubes</text>

**¿Quién podrá registrar un nuevo club en la plataforma?**

Esto es importante porque define si necesitaremos un panel de superadministrador, un sistema de registro público o ambos.

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo vos podés registrar clubes**

      Tendrás un panel de superadministrador desde donde das de alta clubes, configurás su información y asignás administradores.

      <text color="secondary" size="xs">Mayor control sobre quién utiliza la plataforma.</text>
    </radio>
    <radio value="b">
      **Opción B — Los clubes se registran solos**

      Cualquier club puede ingresar a tu web, crear una cuenta y comenzar a configurar su sistema.

      <text color="secondary" size="xs">Facilita la expansión, pero requiere validaciones y onboarding.</text>
    </radio>
    <radio value="c">
      **Opción C — Modelo mixto**

      Los clubes pueden solicitar registrarse, pero vos tenés que aprobarlos antes de que puedan comenzar a operar.

      <text color="secondary" size="xs">Combina escalabilidad con control administrativo.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 2 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo el superadministrador registra clubes",b:"los clubes se registran solos",c:"los clubes solicitan registrarse y el superadministrador los aprueba"}[opcion]}.`)}>Confirmar y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación para la primera versión es la **opción A**: vos das de alta los clubes manualmente. Más adelante podemos automatizar el registro cuando el negocio empiece a crecer.

Vamos a seguir registrando cada decisión para que el documento final no deje reglas importantes libradas a la interpretación de Codex.
```

## DEC-002

### Registro 51 — Alta de clubes
Fuente: `c69d0ccd-f95b-4e8d-ade4-46423aaa4b41`

Respuesta previa del usuario:

Para la pregunta 2 del relevamiento SDD elijo la opción A: solo el superadministrador registra clubes.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 2 — Confirmada**
</row>

Queda definido que **únicamente el superadministrador de la plataforma podrá registrar nuevos clubes**.

## Registro SDD — Decisión 002

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-002 — Alta de clubes**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-006: Solo el superadministrador puede crear clubes.
  - RN-007: Los clubes no pueden registrarse por cuenta propia.
  - RN-008: Cada club tendrá un identificador único.
  - RN-009: El superadministrador podrá consultar y administrar los clubes registrados.
  - RN-010: Cada club tendrá sus propios administradores.

  <text color="secondary" size="xs">Queda pendiente definir quién puede modificar, suspender o eliminar clubes, y cómo se crean sus administradores.</text>
</box>

## Avance del relevamiento

<box gap={2}>
  <row justify="between">
    <text color="secondary" size="sm">Decisiones confirmadas</text>
    **2**
  </row>
  <row align="center" gap={2}>
    <icon name="check-circle" color="success"/>
    <text size="sm">DEC-001 — Plataforma multiclub</text>
  </row>
  <row align="center" gap={2}>
    <icon name="check-circle" color="success"/>
    <text size="sm">DEC-002 — Alta exclusiva por superadministrador</text>
  </row>
</box>

---

## Fase 1 — Administración de clubes

<text color="secondary" weight="medium" size="xs">Pregunta 3 — Suspensión y baja de clubes</text>

Supongamos que un club deja de utilizar tu plataforma, o que necesitás suspenderlo por algún motivo.

**¿Qué debería suceder con ese club y toda su información?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Eliminar definitivamente**

      El superadministrador puede eliminar el club junto con sus jugadores, cuotas y registros asociados.
    </radio>
    <radio value="b">
      **Opción B — Baja lógica (recomendada)**

      El club pasa a estado inactivo. Se bloquean nuevas operaciones, pero se conserva el historial de jugadores, cuotas y pagos para consultas y auditoría.
    </radio>
    <radio value="c">
      **Opción C — Estados más detallados**

      El club puede estar activo, suspendido o dado de baja. Cada estado tiene restricciones diferentes y se conserva el historial.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 3 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"eliminación definitiva del club y sus datos",b:"baja lógica del club conservando el historial",c:"estados activo, suspendido y dado de baja, conservando el historial"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Yo elegiría la **opción C**, porque estás construyendo una plataforma que administra dinero y es importante conservar la trazabilidad de las operaciones. También te permitiría suspender temporalmente un club sin darlo de baja definitivamente.
```

## DEC-003

### Registro 53 — Baja lógica de clubes
Fuente: `6c8ebbda-4ca6-45ee-b70d-19b939b8473f`

Respuesta previa del usuario:

Para la pregunta 3 del relevamiento SDD elijo la opción B: baja lógica del club conservando el historial.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 3 — Confirmada**
</row>

Queda establecido que los clubes tendrán **baja lógica**, conservando todo su historial de jugadores, cuotas y pagos.

## Registro SDD — Decisión 003

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-003 — Baja lógica de clubes**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-011: Un club puede encontrarse activo o inactivo.
  - RN-012: Al dar de baja un club, su estado cambia a inactivo.
  - RN-013: La baja no elimina jugadores, cuotas, pagos ni administradores.
  - RN-014: Un club inactivo no puede generar nuevas cuotas ni iniciar nuevos cobros.
  - RN-015: El superadministrador puede consultar el historial de clubes inactivos.

  **Campos propuestos para `Club`:**

  | Campo | Tipo |
  |---|---|
  | `IdClub` | UUID (PK) |
  | `Nombre` | VARCHAR(150) |
  | `Activo` | BIT |
  | `FechaAlta` | DATETIME |
  | `FechaBaja` | DATETIME NULL |

  <caption>Queda pendiente definir la reactivación de clubes y cómo tratar pagos iniciados antes de la baja.</caption>
</box>

---

## Fase 1 — Administración de clubes

<text color="secondary" weight="medium" size="xs">Pregunta 4 — Reactivación de clubes</text>

Supongamos que Obras Mendoza deja de utilizar la plataforma durante seis meses y después decide volver.

**¿Querés permitir que un club dado de baja pueda reactivarse y recuperar su funcionamiento normal?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Sí, con su historial completo**

      El superadministrador puede reactivar el club. Recupera sus jugadores, categorías, administradores y cuotas anteriores, sin generar automáticamente las cuotas de los meses en que estuvo inactivo.
    </radio>
    <radio value="b">
      **Opción B — Sí, generando también las cuotas faltantes**

      Al reactivar el club, se generan las cuotas correspondientes a los meses que estuvo inactivo.
    </radio>
    <radio value="c">
      **Opción C — No permitir reactivación**

      Si un club quiere volver, se debe crear un registro nuevo.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 4 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir reactivar clubes con todo su historial, sin generar cuotas de los meses inactivos",b:"permitir reactivar clubes generando también las cuotas de los meses inactivos",c:"no permitir reactivación; debe crearse un club nuevo"}[opcion]}.`)}>Confirmar y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A**: conserva la información y permite retomar las operaciones sin crear deudas retroactivas que el club no haya autorizado.
```

## DEC-004

### Registro 55 — Reactivación de clubes
Fuente: `e942c39f-bfac-4e6c-bd32-bac5d845502d`

Respuesta previa del usuario:

Para la pregunta 4 del relevamiento SDD elijo la opción A: permitir reactivar clubes con todo su historial, sin generar cuotas de los meses inactivos.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 4 — Confirmada**
</row>

Queda definido que **el superadministrador podrá reactivar un club dado de baja**, recuperando toda su información histórica sin generar cuotas correspondientes al período de inactividad.

## Registro SDD — Decisión 004

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-004 — Reactivación de clubes**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-016: Solo el superadministrador puede reactivar un club.
  - RN-017: La reactivación cambia el estado del club de inactivo a activo.
  - RN-018: Se conservan todos los jugadores, categorías, administradores, cuotas y pagos anteriores.
  - RN-019: No se generan automáticamente cuotas correspondientes a los meses en los que el club estuvo inactivo.
  - RN-020: Las nuevas cuotas podrán generarse a partir de la reactivación, según la configuración vigente del club.
  - RN-021: Se debe registrar la fecha de reactivación para mantener trazabilidad.

  **Ejemplo:**

  <box background="surface-secondary" radius="lg" padding={3} gap={2}>
    <row align="center" justify="between">
      <text weight="medium">Enero 2027</text>
      <badge color="success">Activo</badge>
    </row>
    <text color="secondary" size="sm">El club genera y cobra sus cuotas normalmente.</text>
    <divider color="subtle"/>
    <row align="center" justify="between">
      <text weight="medium">Febrero a abril 2027</text>
      <badge>Inactivo</badge>
    </row>
    <text color="secondary" size="sm">Se conserva el historial, sin generar nuevas cuotas.</text>
    <divider color="subtle"/>
    <row align="center" justify="between">
      <text weight="medium">Mayo 2027</text>
      <badge color="success">Reactivado</badge>
    </row>
    <text color="secondary" size="sm">El club retoma sus operaciones. No se crean automáticamente las cuotas de febrero, marzo y abril.</text>
  </box>
</box>

---

## Fase 1 — Administración de clubes

<text color="secondary" weight="medium" size="xs">Pregunta 5 — Administradores de cada club</text>

Ahora necesitamos definir quiénes van a gestionar los jugadores, cuotas y pagos dentro de cada institución.

**¿Cuántos administradores puede tener un club?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Un único administrador**

      Cada club tiene una sola cuenta administrativa que gestiona todo.
    </radio>
    <radio value="b">
      **Opción B — Múltiples administradores con los mismos permisos**

      Cada club puede tener varios administradores, todos con acceso a las mismas funcionalidades.
    </radio>
    <radio value="c">
      **Opción C — Múltiples administradores con roles**

      Cada club puede tener varios usuarios con distintos permisos: administrador general, tesorero, encargado de categoría, etc.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 5 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"un único administrador por club",b:"múltiples administradores por club con los mismos permisos",c:"múltiples administradores por club con roles y permisos diferenciados"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**, porque un club podría tener un tesorero encargado de los pagos y otras personas encargadas de administrar jugadores. Pero si querés simplificar el MVP, la opción B también es perfectamente válida.
```

## DEC-005

### Registro 57 — Administradores por club
Fuente: `c4d827c0-1470-44d4-a5cc-35e63a19822a`

Respuesta previa del usuario:

Para la pregunta 5 del relevamiento SDD elijo la opción B: múltiples administradores por club con los mismos permisos.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 5 — Confirmada**
</row>

Cada club podrá tener **múltiples administradores, todos con los mismos permisos**, sin necesidad de implementar roles diferenciados en la primera versión.

## Registro SDD — Decisión 005

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-005 — Administradores por club**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-022: Un club puede tener múltiples administradores.
  - RN-023: Todos los administradores de un mismo club tienen los mismos permisos.
  - RN-024: Los administradores pueden gestionar jugadores, categorías, cuotas y consultar pagos de su club.
  - RN-025: Un administrador no puede acceder a información de otros clubes sin autorización.
  - RN-026: El superadministrador tiene un nivel de acceso diferente al de los administradores de clubes.

  **Implicación para el MER:**

  Necesitaremos una entidad `Usuario` y una relación con `Club`. Queda pendiente definir si una misma cuenta puede administrar varios clubes y quién puede crear otros administradores.
</box>

## Estado del relevamiento

<box gap={2}>
  <row justify="between" align="center">
    <text color="secondary" size="sm">Decisiones confirmadas</text>
    **5**
  </row>
  <box background="surface-tertiary" height="6px" radius="full" clip>
    <box background="rgba(22,163,74,0.8)" height="100%" width="100%" radius="full"/>
  </box>
  <caption>Bloque inicial de estructura de clubes completado. Continuamos con usuarios y permisos.</caption>
</box>

---

## Fase 1 — Administración de clubes

<text color="secondary" weight="medium" size="xs">Pregunta 6 — Creación de administradores</text>

Supongamos que registrás Obras Mendoza en la plataforma y le asignás un administrador inicial. Después, el club quiere incorporar al tesorero y a dos personas más.

**¿Quién debería poder crear nuevos administradores dentro de un club?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo el superadministrador**

      Únicamente vos podés crear, modificar y dar de baja administradores de cualquier club.
    </radio>
    <radio value="b">
      **Opción B — Los administradores del propio club**

      Vos creás el primer administrador al registrar el club. Después, cualquier administrador del club puede agregar o dar de baja otros administradores de su institución.
    </radio>
    <radio value="c">
      **Opción C — Modelo mixto**

      Tanto el superadministrador como los administradores del club pueden crear y administrar usuarios de ese club.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 6 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo el superadministrador puede gestionar administradores",b:"el superadministrador crea el primero y luego los administradores del club gestionan los demás",c:"tanto el superadministrador como los administradores del club pueden gestionar administradores"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación sería la **opción B**. Te permite dar de alta cada club y delegar la gestión cotidiana de usuarios, evitando que tengas que intervenir cada vez que una institución incorpore un administrador nuevo.
```

## DEC-006

### Registro 59 — Gestión de administradores
Fuente: `ae4dadc2-d821-427b-945e-96e9be354201`

Respuesta previa del usuario:

Para la pregunta 6 del relevamiento SDD elijo la opción B: el superadministrador crea el primero y luego los administradores del club gestionan los demás.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 6 — Confirmada**
</row>

Queda establecido que **el superadministrador crea el primer administrador de cada club** y, a partir de ese momento, los administradores del club pueden gestionar a los demás.

## Registro SDD — Decisión 006

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    **DEC-006 — Gestión de administradores**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-027: El superadministrador crea el primer administrador al registrar un club.
  - RN-028: Los administradores pueden incorporar otros administradores a su propio club.
  - RN-029: Los administradores pueden modificar y dar de baja a otros administradores de su club.
  - RN-030: Todos los administradores del club tienen los mismos permisos.
  - RN-031: Ningún administrador puede gestionar usuarios de otros clubes.
  - RN-032: Las bajas de administradores serán lógicas, conservando el historial de sus acciones.
  - RN-033: No se permitirá dar de baja al último administrador activo de un club.

  <caption>RN-032 y RN-033 son salvaguardas propuestas que incorporaremos al diseño; podremos revisarlas si fuera necesario.</caption>
</box>

## Estado del relevamiento

<row align="center" justify="between">
  <text color="secondary" weight="medium">6 decisiones confirmadas</text>
  <badge>Fase 1</badge>
</row>

Ya definimos la estructura multiclub, altas, bajas, reactivaciones y administración básica de usuarios.

---

## Pregunta 7 — Administradores de varios clubes

Imaginá que una persona trabaja como tesorero de Obras Mendoza y también administra otro club que utiliza tu plataforma.

**¿Querés permitir que una misma cuenta de usuario pueda administrar varios clubes?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Una cuenta por club**

      Cada administrador pertenece exclusivamente a un club. Si trabaja en dos clubes, necesita dos cuentas independientes.
    </radio>
    <radio value="b">
      **Opción B — Una cuenta para varios clubes**

      Un administrador puede estar asociado a varios clubes. Al iniciar sesión, selecciona qué club quiere administrar.

      <text color="secondary" size="xs">Por ejemplo, inicia sesión una vez y puede alternar entre Obras y otro club, siempre que tenga autorización en ambos.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 7 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${opcion==="a"?"cada administrador tiene una cuenta independiente por club":"una misma cuenta puede administrar varios clubes autorizados"}.`)}>Confirmar y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación para el MVP es la **opción A**, porque simplifica la autenticación y el aislamiento entre clubes. La opción B es más flexible a futuro, pero requiere gestionar correctamente las asociaciones y permisos de cada usuario.
```

## DEC-007

### Registro 61 — Asociación administrador-club
Fuente: `e92fc343-d148-4939-a609-abef76fe64a3`

Respuesta previa del usuario:

Para la pregunta 7 del relevamiento SDD elijo la opción A: cada administrador tiene una cuenta independiente por club.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 7 — Confirmada**
</row>

Cada administrador tendrá **una cuenta independiente por club**. Si una persona administra dos clubes, necesitará dos cuentas distintas.

## Registro SDD — Decisión 007

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-007 — Asociación administrador-club**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-034: Cada cuenta administrativa pertenece a un único club.
  - RN-035: Un club puede tener múltiples cuentas administrativas.
  - RN-036: Una misma cuenta no puede administrar varios clubes.
  - RN-037: Si una persona administra dos clubes, debe utilizar cuentas independientes.
  - RN-038: Cada operación administrativa debe validar que el usuario pertenezca al club correspondiente.
  - RN-039: El superadministrador es un usuario de plataforma y no está sujeto a la asociación con un único club.

  **Implicación para el MER:**

  <box background="surface-secondary" radius="md" padding={3} gap={1}>
    <text weight="medium">Club (1) → (N) UsuarioAdministrador</text>
    <caption>La entidad UsuarioAdministrador tendrá una clave foránea IdClub obligatoria. Los usuarios de plataforma se modelarán separadamente o mediante una estrategia de autenticación que distinga ambos tipos.</caption>
  </box>
</box>

---

## Fase 1 — Autenticación y usuarios

<text color="secondary" weight="medium" size="xs">Pregunta 8 — Inicio de sesión de administradores</text>

Ahora tenemos que definir cómo ingresarán los administradores al panel privado.

**¿Qué método de inicio de sesión querés implementar?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Correo electrónico y contraseña**

      Cada administrador ingresa con su email y contraseña. El sistema permite recuperar la contraseña mediante correo electrónico.
    </radio>
    <radio value="b">
      **Opción B — Google**

      Los administradores inician sesión con su cuenta de Google, sin necesidad de gestionar una contraseña propia.
    </radio>
    <radio value="c">
      **Opción C — Ambos métodos**

      Los administradores pueden utilizar email y contraseña o iniciar sesión con Google.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 8 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"inicio de sesión con correo electrónico y contraseña, con recuperación por email",b:"inicio de sesión mediante Google",c:"inicio de sesión con email y contraseña o Google"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación para el MVP es la **opción A**, porque es sencilla de implementar y no depende de que los administradores tengan una cuenta de Google.

Más adelante también vamos a definir duración de sesiones, recuperación de cuentas y medidas de seguridad.
```

## DEC-008

### Registro 63 — Autenticación de administradores
Fuente: `af4427fa-f887-41c2-b093-9ac3ea901b39`

Respuesta previa del usuario:

Para la pregunta 8 del relevamiento SDD elijo la opción A: inicio de sesión con correo electrónico y contraseña, con recuperación por email.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 8 — Confirmada**
</row>

Los administradores iniciarán sesión mediante **correo electrónico y contraseña**, con recuperación de contraseña por email.

## Registro SDD — Decisión 008

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-008 — Autenticación de administradores**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-040: Los administradores se autentican mediante correo electrónico y contraseña.
  - RN-041: Cada cuenta administrativa tendrá un correo electrónico asociado.
  - RN-042: El sistema permitirá solicitar la recuperación de contraseña por email.
  - RN-043: La recuperación utilizará un enlace temporal de un solo uso.
  - RN-044: Las contraseñas se almacenarán mediante hashes seguros, nunca en texto plano.
  - RN-045: Los usuarios dados de baja no podrán iniciar sesión.
  - RN-046: Las operaciones privadas requerirán una sesión autenticada.

  <text color="secondary" size="xs">RN-043 a RN-046 son requisitos técnicos de seguridad propuestos para la especificación. Queda pendiente definir la unicidad del email entre clubes, la expiración de sesiones y la verificación del correo.</text>
</box>

## Avance del relevamiento

<box gap={2}>
  <row justify="between" align="center">
    <text color="secondary" size="sm">Decisiones confirmadas</text>
    **8**
  </row>
  <row align="center" gap={2}>
    <icon name="check-circle" color="success"/>
    <text size="sm">Estructura multiclub y ciclo de vida</text>
  </row>
  <row align="center" gap={2}>
    <icon name="check-circle" color="success"/>
    <text size="sm">Administradores y permisos básicos</text>
  </row>
  <row align="center" gap={2}>
    <icon name="check-circle" color="success"/>
    <text size="sm">Método de autenticación</text>
  </row>
</box>

---

## Fase 1 — Autenticación y usuarios

<text color="secondary" weight="medium" size="xs">Pregunta 9 — Correo electrónico entre clubes</text>

Como definimos que cada administrador tendrá una cuenta independiente por club, necesitamos resolver una situación concreta.

Supongamos que `tesorero@gmail.com` administra Obras Mendoza y también otro club.

**¿Puede utilizar el mismo correo electrónico para tener dos cuentas independientes?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Sí, el mismo correo en diferentes clubes**

      El email debe ser único dentro de cada club, pero puede repetirse entre clubes. Para iniciar sesión, el usuario debe indicar a qué club quiere ingresar o acceder desde el portal de ese club.
    </radio>
    <radio value="b">
      **Opción B — No, correo único en toda la plataforma**

      Un correo electrónico solo puede estar asociado a una cuenta administrativa. Para administrar otro club, necesitará utilizar otro email.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 9 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${opcion==="a"?"permitir el mismo correo en distintos clubes, manteniendo cuentas independientes":"exigir un correo único en toda la plataforma"}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A**. Mantiene la independencia de las cuentas sin obligar a una persona a crear varios correos electrónicos. En ese caso, diseñaremos el inicio de sesión para identificar inequívocamente el club correspondiente.
```

## DEC-009

### Registro 65 — Unicidad del correo electrónico
Fuente: `5272e231-1b03-4cbc-85f6-77f44994383d`

Respuesta previa del usuario:

Para la pregunta 9 del relevamiento SDD elijo la opción A: permitir el mismo correo en distintos clubes, manteniendo cuentas independientes.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 9 — Confirmada**
</row>

Un mismo correo electrónico podrá utilizarse en diferentes clubes, pero **cada cuenta administrativa será independiente**.

## Registro SDD — Decisión 009

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    **DEC-009 — Unicidad del correo electrónico**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-047: El correo electrónico debe ser único dentro de cada club.
  - RN-048: Un mismo correo puede estar registrado en múltiples clubes.
  - RN-049: Cada cuenta mantiene sus propias credenciales, estado y permisos.
  - RN-050: La autenticación debe identificar inequívocamente el club al que pertenece la cuenta.
  - RN-051: La recuperación de contraseña debe afectar únicamente a la cuenta del club correspondiente.

  **Implicación para SQL Server:**

  La entidad `UsuarioAdministrador` tendrá una restricción única compuesta:

  ```sql
  UNIQUE (IdClub, EmailNormalizado)
  ```

  Esto permite registrar `tesorero@gmail.com` tanto en Obras Mendoza como en otro club, pero impide duplicarlo dentro de Obras.
</box>

---

## Fase 1 — Autenticación y usuarios

<text color="secondary" weight="medium" size="xs">Pregunta 10 — Acceso al panel administrativo</text>

Ahora necesitamos definir cómo identifica el sistema a qué club pertenece el administrador cuando inicia sesión.

**¿Cómo querés que los administradores accedan a su panel?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — URL personalizada por club**

      Cada club tiene su propia dirección web, que identifica automáticamente a qué institución pertenece el administrador.

      <box background="surface-secondary" radius="md" padding={2} gap={1}>
        <text size="xs" weight="medium">Ejemplo</text>
        <text size="sm" color="secondary">tuplataforma.com/obras/admin</text>
        <text size="sm" color="secondary">tuplataforma.com/godoycruz/admin</text>
      </box>
    </radio>
    <radio value="b">
      **Opción B — Login centralizado con selección de club**

      Todos ingresan a una misma página. Antes de iniciar sesión, seleccionan el club al que pertenecen.

      <box background="surface-secondary" radius="md" padding={2}>
        <text size="sm" color="secondary">tuplataforma.com/admin</text>
      </box>
    </radio>
    <radio value="c">
      **Opción C — Subdominio por club**

      Cada institución tiene su propio subdominio y desde allí accede a su panel.

      <box background="surface-secondary" radius="md" padding={2} gap={1}>
        <text size="sm" color="secondary">obras.tuplataforma.com/admin</text>
        <text size="sm" color="secondary">godoycruz.tuplataforma.com/admin</text>
      </box>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 10 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"URL personalizada por club mediante rutas",b:"login centralizado con selección de club",c:"subdominio personalizado por club"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación para el MVP es la **opción A**. Es sencilla de implementar, funciona bien con tu arquitectura multiclub y permite que cada institución tenga su propio acceso público y administrativo.

Más adelante podemos incorporar dominios o subdominios personalizados sin cambiar las reglas principales del negocio.
```

## DEC-010

### Registro 67 — URLs personalizadas por club
Fuente: `0ae618e5-784f-4879-b0bf-5f7d1b490346`

Respuesta previa del usuario:

Para la pregunta 10 del relevamiento SDD elijo la opción A: URL personalizada por club mediante rutas.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 10 — Confirmada**
</row>

Cada club tendrá una **URL personalizada mediante rutas**, tanto para el portal público como para el panel administrativo.

## Registro SDD — Decisión 010

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    **DEC-010 — URLs personalizadas por club**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-052: Cada club tendrá un identificador público único (`Slug`).
  - RN-053: El portal público estará disponible en `/{slug}`.
  - RN-054: El acceso administrativo estará disponible en `/{slug}/admin`.
  - RN-055: El sistema identificará al club mediante el slug de la URL.
  - RN-056: Un administrador solo podrá acceder al panel del club al que pertenece.
  - RN-057: No se permitirá registrar dos clubes con el mismo slug.
  - RN-058: Las rutas administrativas deberán validar autenticación y pertenencia al club.

  **Ejemplo de estructura:**

  <box background="surface-secondary" radius="lg" padding={3} gap={2}>
    <row align="center" gap={2}>
      <icon name="globe" color="secondary"/>
      <box flex="1" gap={1}>
        **Portal público de Obras**
        <text size="sm" color="secondary">tuplataforma.com/obras</text>
      </box>
    </row>
    <divider color="subtle"/>
    <row align="center" gap={2}>
      <icon name="lock" color="secondary"/>
      <box flex="1" gap={1}>
        **Administración de Obras**
        <text size="sm" color="secondary">tuplataforma.com/obras/admin</text>
      </box>
    </row>
  </box>

  <caption>El slug identifica al club, pero no reemplaza las validaciones de autorización en el backend.</caption>
</box>

---

## Fase 1 — Identidad visual de los clubes

<text color="secondary" weight="medium" size="xs">Pregunta 11 — Personalización del portal</text>

Ya tenemos definido que cada club tendrá su propia URL. Ahora necesitamos determinar qué tan personalizado será su portal.

Por ejemplo, Obras Mendoza podría utilizar su escudo y sus colores rojo y blanco, mientras que otro club podría tener una identidad completamente diferente.

**¿Qué nivel de personalización querés permitir?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Personalización básica**

      Cada club puede configurar su nombre y escudo. Todos utilizan los mismos colores y diseño de la plataforma.
    </radio>
    <radio value="b">
      **Opción B — Personalización visual por club**

      Cada club puede configurar su nombre, escudo, color principal, color secundario e imagen de portada.

      <text color="secondary" size="xs">Todos comparten la misma estructura de pantalla, pero con su propia identidad visual.</text>
    </radio>
    <radio value="c">
      **Opción C — Personalización avanzada**

      Además de los colores y el escudo, cada club puede modificar textos, imágenes, secciones y componentes visibles de su portal público.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 11 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"personalización básica con nombre y escudo",b:"personalización visual con nombre, escudo, colores e imagen de portada",c:"personalización avanzada del contenido y diseño del portal"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción B**. Te permite ofrecer una experiencia visual propia para cada club sin tener que desarrollar y mantener una interfaz diferente para cada institución.
```

## DEC-011

### Registro 69 — Personalización visual por club
Fuente: `2352f88f-6dba-4348-86da-b70ea4076e2e`

Respuesta previa del usuario:

Para la pregunta 11 del relevamiento SDD elijo la opción B: personalización visual con nombre, escudo, colores e imagen de portada.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 11 — Confirmada**
</row>

Cada club tendrá una **identidad visual personalizada**, pero todos compartirán la misma estructura de interfaz. Esto permite mantener un único frontend para toda la plataforma.

## Registro SDD — Decisión 011

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    **DEC-011 — Personalización visual por club**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-059: Cada club podrá configurar su nombre visible.
  - RN-060: Cada club podrá cargar su escudo o logotipo.
  - RN-061: Cada club podrá configurar un color principal y uno secundario.
  - RN-062: Cada club podrá cargar una imagen de portada.
  - RN-063: La personalización se aplicará al portal público y a la identidad visual del panel administrativo.
  - RN-064: Todos los clubes utilizarán la misma estructura funcional.
  - RN-065: Si un club no configura algún elemento visual, se utilizará un valor predeterminado de la plataforma.

  **Campos propuestos para `Club`:**

  | Campo | Tipo sugerido |
  |---|---|
  | `Nombre` | VARCHAR(150) |
  | `Slug` | VARCHAR(100) |
  | `LogoUrl` | VARCHAR(500) |
  | `ColorPrimario` | VARCHAR(7) |
  | `ColorSecundario` | VARCHAR(7) |
  | `ImagenPortadaUrl` | VARCHAR(500) |

  <caption>Los archivos de imágenes se almacenarían fuera de la base de datos; SQL Server guardaría sus referencias. Las dimensiones, formatos y límites de carga se definirán en el relevamiento técnico.</caption>
</box>

## Ejemplo visual

<grid columns={2} gap={3}>
  <grid-item>
    <box border radius="lg" clip gap={0}>
      <box background="#A71930" theme="dark" padding={3} align="center" gap={2}>
        <icon name="shield" color="#FFFFFF" size="2xl"/>
        <text color="#FFFFFF" weight="semibold" textAlign="center">Obras Mendoza</text>
      </box>
      <box padding={3} gap={1}>
        <text weight="medium" size="sm">Portal de cuotas</text>
        <text color="secondary" size="xs">Rojo y blanco</text>
      </box>
    </box>
  </grid-item>
  <grid-item>
    <box border radius="lg" clip gap={0}>
      <box background="#174A8B" theme="dark" padding={3} align="center" gap={2}>
        <icon name="shield" color="#FFFFFF" size="2xl"/>
        <text color="#FFFFFF" weight="semibold" textAlign="center">Club Ejemplo</text>
      </box>
      <box padding={3} gap={1}>
        <text weight="medium" size="sm">Portal de cuotas</text>
        <text color="secondary" size="xs">Azul y blanco</text>
      </box>
    </box>
  </grid-item>
</grid>

<caption>Representación conceptual de la personalización, no diseños definitivos.</caption>

---

## Fase 1 — Configuración de clubes

<text color="secondary" weight="medium" size="xs">Pregunta 12 — Quién modifica la identidad visual</text>

Ya definimos qué elementos visuales tendrá cada club. Ahora necesitamos establecer quién podrá modificarlos.

**¿Quién debería poder cambiar el escudo, los colores, el nombre visible y la imagen de portada?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo el superadministrador**

      Vos configurás la identidad visual cuando das de alta el club. Los administradores del club no pueden modificarla.
    </radio>
    <radio value="b">
      **Opción B — Los administradores del club**

      Los administradores pueden modificar su identidad visual desde el panel administrativo, sin solicitarte autorización.
    </radio>
    <radio value="c">
      **Opción C — Ambos**

      Tanto el superadministrador como los administradores del club pueden modificar la identidad visual de la institución.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 12 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo el superadministrador puede modificar la identidad visual",b:"los administradores del club pueden modificar su identidad visual",c:"tanto el superadministrador como los administradores del club pueden modificarla"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción B**, para que cada club sea autónomo en su personalización y vos no tengas que intervenir ante cada cambio de escudo o portada.
```

## DEC-012

### Registro 71 — Permisos de personalización visual
Fuente: `f772308c-f0ba-4498-a4c2-a1eaff1b6863`

Respuesta previa del usuario:

Para la pregunta 12 del relevamiento SDD elijo la opción C: tanto el superadministrador como los administradores del club pueden modificarla.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 12 — Confirmada**
</row>

Tanto el **superadministrador de la plataforma** como los **administradores de cada club** podrán modificar la identidad visual de la institución.

## Registro SDD — Decisión 012

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    **DEC-012 — Permisos de personalización visual**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-066: El superadministrador puede modificar la identidad visual de cualquier club.
  - RN-067: Los administradores de un club pueden modificar la identidad visual de su propia institución.
  - RN-068: Los administradores no pueden modificar la configuración visual de otros clubes.
  - RN-069: Los cambios de identidad visual se reflejarán en el portal público y en el panel administrativo.
  - RN-070: Los cambios deberán quedar registrados para conocer quién los realizó y cuándo.

  **Importante:** el nombre visible podrá modificarse, pero eso no implica cambiar automáticamente el `Slug` del club. El tratamiento de cambios de URL quedará pendiente para otra decisión.
</box>

---

## Fase 2 — Gestión de jugadores

<text color="secondary" size="sm" color="secondary">Con la configuración básica de clubes definida, comenzamos a relevar cómo se administrarán los jugadores, sus categorías y sus cuotas.</text>

<text color="secondary" weight="medium" size="xs">Pregunta 13 — Registro de jugadores</text>

Imaginemos que Obras Mendoza comienza a utilizar la plataforma y necesita cargar a sus 180 jugadores.

**¿Cómo querés que se registren los jugadores en el sistema?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Carga manual**

      El administrador registra a cada jugador individualmente, completando sus datos desde un formulario.
    </radio>
    <radio value="b">
      **Opción B — Importación masiva**

      El administrador carga un archivo Excel con los jugadores y el sistema los registra automáticamente.
    </radio>
    <radio value="c">
      **Opción C — Ambos métodos**

      El administrador puede registrar jugadores individualmente o importar muchos desde Excel.

      <text color="secondary" size="xs">Por ejemplo, importar los 180 jugadores inicialmente y después agregar manualmente a los nuevos.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 13 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"los jugadores se registran manualmente",b:"los jugadores se registran mediante importación masiva desde Excel",c:"los jugadores pueden registrarse manualmente o mediante importación masiva desde Excel"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**. La importación masiva facilita incorporar clubes existentes y la carga manual permite mantener actualizado el padrón de jugadores sin depender de un Excel.
```

## DEC-013

### Registro 73 — Registro de jugadores
Fuente: `e4121e26-9bfb-4db6-a893-52e80ffd22dd`

Respuesta previa del usuario:

Para la pregunta 13 del relevamiento SDD elijo la opción C: los jugadores pueden registrarse manualmente o mediante importación masiva desde Excel.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 13 — Confirmada**
</row>

Los administradores podrán registrar jugadores **manualmente o mediante importación masiva desde Excel**.

## Registro SDD — Decisión 013

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    **DEC-013 — Registro de jugadores**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-071: Los administradores podrán registrar jugadores individualmente mediante un formulario.
  - RN-072: Los administradores podrán importar múltiples jugadores desde un archivo Excel.
  - RN-073: Los jugadores registrados pertenecerán al club desde el cual se realiza la operación.
  - RN-074: Un administrador no podrá registrar jugadores en otro club.

  **Requisitos propuestos para la importación:**

  - Descargar una plantilla Excel con las columnas esperadas.
  - Validar los datos antes de confirmar la importación.
  - Informar qué filas contienen errores.
  - Evitar registros duplicados según la regla de identificación que definamos.
  - Mostrar un resumen de jugadores importados y rechazados.

  <caption>Estas validaciones se incorporarán como propuestas técnicas hasta definir los campos obligatorios y las reglas de duplicidad.</caption>
</box>

---

## Fase 2 — Gestión de jugadores

<text color="secondary" weight="medium" size="xs">Pregunta 14 — Datos obligatorios del jugador</text>

Necesitamos definir qué información deberá completar el administrador cuando registra a un jugador.

Por ejemplo, un club podría necesitar solamente el DNI y nombre del jugador, mientras que otro podría querer guardar información de contacto para enviar recordatorios de pago.

**¿Qué datos querés solicitar al registrar un jugador?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Datos mínimos**

      DNI, nombre, apellido y categoría.
    </radio>
    <radio value="b">
      **Opción B — Datos completos con contacto opcional**

      Obligatorios: DNI, nombre, apellido y categoría.

      Opcionales: fecha de nacimiento, teléfono, correo electrónico y nombre del responsable o tutor.

      <text color="secondary" size="xs">Ideal para categorías infantiles y juveniles, donde muchas veces paga un padre o tutor.</text>
    </radio>
    <radio value="c">
      **Opción C — Datos completos obligatorios**

      DNI, nombre, apellido, categoría, fecha de nacimiento, teléfono y correo electrónico. El responsable o tutor será obligatorio para menores de edad.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 14 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"DNI, nombre, apellido y categoría obligatorios",b:"DNI, nombre, apellido y categoría obligatorios; fecha de nacimiento, teléfono, email y tutor opcionales",c:"DNI, nombre, apellido, categoría, fecha de nacimiento, teléfono y email obligatorios; tutor obligatorio para menores"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción B**. Permite cargar rápidamente los jugadores existentes sin exigir información que quizás el club todavía no tenga, pero deja preparado el sistema para futuras notificaciones y contactos con responsables.
```

## DEC-014

### Registro 75 — Datos del jugador
Fuente: `93f5b8e2-10af-4ef6-b94f-b78273d1ae04`

Respuesta previa del usuario:

Para la pregunta 14 del relevamiento SDD elijo la opción B: DNI, nombre, apellido y categoría obligatorios; fecha de nacimiento, teléfono, email y tutor opcionales.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 14 — Confirmada**
</row>

Cada jugador tendrá cuatro datos obligatorios: **DNI, nombre, apellido y categoría**. Los datos de contacto y del responsable serán opcionales.

## Registro SDD — Decisión 014

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    **DEC-014 — Datos del jugador**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-075: Todo jugador debe tener DNI, nombre, apellido y categoría.
  - RN-076: La fecha de nacimiento, teléfono, email y nombre del tutor serán opcionales.
  - RN-077: Los administradores podrán modificar los datos de los jugadores de su propio club.
  - RN-078: La ausencia de datos opcionales no impedirá registrar un jugador.
  - RN-079: La categoría seleccionada deberá pertenecer al mismo club que el jugador.

  **Modelo preliminar de `Jugador`:**

  | Campo | Obligatorio |
  |---|---|
  | `IdJugador` | Sí (identificador interno) |
  | `IdClub` | Sí |
  | `DNI` | Sí |
  | `Nombre` | Sí |
  | `Apellido` | Sí |
  | `IdCategoria` | Sí |
  | `FechaNacimiento` | No |
  | `Telefono` | No |
  | `Email` | No |
  | `NombreTutor` | No |
  | `IdEstadoJugador` | Sí (estado inicial pendiente de definir) |

  <caption>La regla de unicidad del DNI y el comportamiento de jugadores dados de baja se definirán en las próximas preguntas.</caption>
</box>

---

## Fase 2 — Gestión de jugadores

<text color="secondary" weight="medium" size="xs">Pregunta 15 — Jugadores registrados en varios clubes</text>

Imaginemos que un jugador está registrado en Obras Mendoza y también en otro club que utiliza nuestra plataforma.

Ambos clubes necesitan gestionar sus propias cuotas, categorías y estados.

**¿Cómo debería manejar el sistema a ese jugador?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Registro independiente por club**

      El mismo DNI puede existir en distintos clubes. Cada club administra su propia ficha del jugador, sus cuotas y su estado.

      <text color="secondary" size="xs">Dentro de un mismo club, un DNI solo puede tener una ficha de jugador.</text>
    </radio>
    <radio value="b">
      **Opción B — Jugador global compartido**

      Cada DNI corresponde a una única persona en toda la plataforma. Los clubes comparten su identidad básica, pero administran sus cuotas y categorías por separado.
    </radio>
    <radio value="c">
      **Opción C — DNI único en toda la plataforma**

      Un jugador solo puede estar registrado en un club a la vez. Para cambiar de institución, debe desvincularse del club anterior.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 15 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"registro independiente por club, permitiendo repetir DNI entre clubes pero no dentro del mismo club",b:"jugador global compartido entre clubes, con cuotas y categorías independientes",c:"DNI único en toda la plataforma, con un solo club por jugador"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A**, porque mantiene la independencia entre clubes y simplifica la gestión de jugadores, pagos y permisos, especialmente para el MVP.
```

## DEC-015

### Registro 78 — Identidad global del jugador
Fuente: `3d10b2f8-9955-4a5c-b4a1-f2920b8a2956`

Respuesta previa del usuario:

Para la pregunta 15 del relevamiento SDD elijo la opción B: jugador global compartido entre clubes, con cuotas y categorías independientes.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 15 — Confirmada**
</row>

Elegiste un **modelo de jugador global compartido**. Esto significa que una persona tendrá una única identidad dentro de la plataforma, pero podrá pertenecer a varios clubes simultáneamente.

Es una decisión importante porque modifica el modelo de datos que veníamos planteando.

## Registro SDD — Decisión 015

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    **DEC-015 — Identidad global del jugador**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-080: Cada DNI identificará a un único jugador global en la plataforma.
  - RN-081: Un jugador podrá estar vinculado a múltiples clubes.
  - RN-082: Cada club gestionará independientemente la categoría, el estado, las cuotas y los pagos de ese jugador.
  - RN-083: Un jugador no podrá tener dos vinculaciones duplicadas con el mismo club.
  - RN-084: La baja de un jugador en un club no afectará su vinculación con otros clubes.
  - RN-085: Los administradores no podrán consultar cuotas ni pagos de otros clubes por el solo hecho de compartir un jugador.

  **Cambio en el modelo de datos:**

  <box background="surface-secondary" radius="lg" padding={3} gap={2}>
    <box align="center" gap={1}>
      <box border radius="md" background="surface" padding={3} width="85%" align="center" gap={1}>
        **Jugador**
        <text color="secondary" size="xs">Identidad global · DNI único</text>
      </box>
      <icon name="arrow-down" color="secondary"/>
      <box border radius="md" background="surface" padding={3} width="85%" align="center" gap={1}>
        **JugadorClub**
        <text color="secondary" size="xs">Vinculación · Categoría · Estado</text>
      </box>
      <icon name="arrow-down" color="secondary"/>
      <row gap={2} width="100%">
        <box flex="1" border radius="md" background="surface" padding={2} align="center" gap={1}>
          **Club**
          <text color="secondary" size="xs">Institución</text>
        </box>
        <box flex="1" border radius="md" background="surface" padding={2} align="center" gap={1}>
          **Cuotas**
          <text color="secondary" size="xs">Por vinculación</text>
        </box>
      </row>
    </box>
  </box>

  La entidad `JugadorClub` será fundamental: representa la pertenencia de un jugador a una institución y permite que tenga estados y obligaciones económicas diferentes en cada club.
</box>

---

## Fase 2 — Gestión de jugadores

<text color="secondary" weight="medium" size="xs">Pregunta 16 — Modificación de datos compartidos</text>

Ahora aparece una situación que debemos resolver.

Supongamos que un jugador está registrado en Obras Mendoza y Godoy Cruz. El administrador de Obras corrige su apellido o teléfono.

**¿Qué debería suceder con esos datos en Godoy Cruz?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Actualización global automática**

      Si un administrador modifica los datos personales del jugador, se actualizan automáticamente para todos los clubes donde está registrado.
    </radio>
    <radio value="b">
      **Opción B — Identidad global, datos de contacto por club**

      El DNI, nombre, apellido y fecha de nacimiento son globales. Cada club mantiene sus propios datos de contacto y tutor.

      <text color="secondary" size="xs">Los cambios de identidad global requerirían un procedimiento controlado para evitar modificaciones incorrectas entre clubes.</text>
    </radio>
    <radio value="c">
      **Opción C — Todos los datos independientes por club**

      El DNI identifica a la misma persona globalmente, pero cada club mantiene su propia copia de nombre, apellido, fecha de nacimiento y contacto.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 16 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"todos los cambios de datos personales se actualizan globalmente para los clubes",b:"identidad global compartida y datos de contacto/tutor independientes por club, con cambios de identidad controlados",c:"DNI global, pero todos los demás datos personales independientes por club"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción B**. Conserva una identidad única, pero evita que un administrador modifique accidentalmente los teléfonos o responsables utilizados por otro club. Además, permite que una misma persona tenga distintos contactos de referencia según la institución.
```

## DEC-016

### Registro 82 — Múltiples disciplinas por club
Fuente: `0b7ad6e2-06b6-4aa8-8681-35c828ad4fb7`

Respuesta previa del usuario:

Para la pregunta 16 del relevamiento SDD elijo la opción A: un jugador puede inscribirse en múltiples disciplinas dentro de un mismo club, con categorías y cuotas independientes.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 16 — Confirmada**
</row>

Un jugador podrá practicar **varias disciplinas dentro del mismo club**, con categorías y cuotas independientes para cada una.

Esta decisión amplía el alcance del sistema: ya no será solamente una plataforma para cobrar cuotas de jugadores, sino una plataforma capaz de gestionar **inscripciones deportivas y sus obligaciones de pago**.

## Registro SDD — Decisión 016

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    **DEC-016 — Múltiples disciplinas por club**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-086: Un club podrá administrar múltiples disciplinas deportivas.
  - RN-087: Un jugador podrá estar inscripto en varias disciplinas del mismo club.
  - RN-088: Cada inscripción tendrá su propia categoría.
  - RN-089: Las cuotas serán independientes por inscripción y disciplina.
  - RN-090: La baja de una inscripción no afectará las demás inscripciones del jugador.
  - RN-091: Las cuotas y pagos históricos deberán conservarse aunque una inscripción sea dada de baja.
</box>

## Impacto en el modelo de datos

<box border radius="lg" padding={3} gap={2}>
  <box align="center" gap={1}>
    <box background="surface-secondary" radius="md" padding={3} width="80%" align="center" gap={1}>
      **Jugador global**
      <text color="secondary" size="xs">Juan Pérez · DNI 12345678</text>
    </box>
    <icon name="arrow-down" color="secondary"/>
    <box background="surface-secondary" radius="md" padding={3} width="80%" align="center" gap={1}>
      **Club**
      <text color="secondary" size="xs">Obras Mendoza</text>
    </box>
    <svg viewBox="0 0 320 40" width="100%">
      <path d="M160 0 V16 M80 16 H240 M80 16 V38 M240 16 V38" stroke="currentColor" opacity=".5" strokeWidth="1.5" fill="none"/>
    </svg>
  </box>
  <grid columns={2} gap={2}>
    <grid-item>
      <box border radius="md" padding={3} gap={2}>
        <text weight="medium" textAlign="center">Futsal</text>
        <text color="secondary" size="xs" textAlign="center">Categoría C20</text>
        <badge color="success">Inscripción activa</badge>
        <divider color="subtle"/>
        <text size="xs">Cuota de futsal</text>
        <text weight="medium">$25.000</text>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="md" padding={3} gap={2}>
        <text weight="medium" textAlign="center">Básquet</text>
        <text color="secondary" size="xs" textAlign="center">Categoría Primera</text>
        <badge color="success">Inscripción activa</badge>
        <divider color="subtle"/>
        <text size="xs">Cuota de básquet</text>
        <text weight="medium">$30.000</text>
      </box>
    </grid-item>
  </grid>
  <caption>Los importes son ilustrativos. El modelo definitivo contemplará las entidades Jugador, Club, Disciplina, Categoría, Inscripción y Cuota.</caption>
</box>

Esto también implica que el estado de un jugador y el estado de cada inscripción deberán diferenciarse. Lo relevaremos más adelante.

---

## Fase 2 — Gestión de jugadores

<text color="secondary" weight="medium" size="xs">Pregunta 17 — Modificación de datos personales compartidos</text>

Retomamos la pregunta que dejamos pendiente.

Supongamos que Juan Pérez está inscripto en Obras Mendoza y otro club. El administrador de Obras modifica su apellido o teléfono.

**¿Cómo querés manejar esos cambios entre clubes?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Todos los datos globales**

      Nombre, apellido, fecha de nacimiento, teléfono, email y tutor son compartidos. Si un club los modifica, el cambio se refleja en todos los demás.
    </radio>
    <radio value="b">
      **Opción B — Identidad global y contactos independientes**

      DNI, nombre, apellido y fecha de nacimiento pertenecen a la identidad global del jugador. Cada club administra su propio teléfono, email y tutor.

      <text color="secondary" size="xs">Los cambios de identidad global deberán realizarse mediante un procedimiento controlado.</text>
    </radio>
    <radio value="c">
      **Opción C — Solo DNI global**

      El DNI identifica a la persona, pero cada club guarda su propia versión del nombre, apellido, fecha de nacimiento y datos de contacto.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 17 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"todos los datos personales son globales y compartidos entre clubes",b:"identidad global compartida y datos de contacto/tutor independientes por club, con cambios de identidad controlados",c:"solo el DNI es global y todos los demás datos son independientes por club"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción B**, porque evita que un club sobrescriba los contactos que utiliza otro, manteniendo una identidad única para cada jugador.
```

## DEC-017

### Registro 84 — Independencia de datos personales por club
Fuente: `e6da1af3-3743-4c1c-a456-9ef5d552c533`

Respuesta previa del usuario:

Para la pregunta 17 del relevamiento SDD elijo la opción C: solo el DNI es global y todos los demás datos son independientes por club.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 17 — Confirmada**
</row>

El **DNI será el único dato global del jugador**. Cada club tendrá su propia ficha con nombre, apellido, fecha de nacimiento, teléfono, email y tutor, sin que los cambios afecten a otras instituciones.

## Registro SDD — Decisión 017

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    **DEC-017 — Independencia de datos personales por club**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-092: Cada DNI identifica a un único jugador global.
  - RN-093: Cada club mantiene su propia ficha de datos personales para ese jugador.
  - RN-094: Modificar nombre, apellido, fecha de nacimiento, teléfono, email o tutor en un club no afecta a otros clubes.
  - RN-095: Los administradores solo pueden consultar y modificar las fichas correspondientes a su club.
  - RN-096: Un jugador puede estar registrado en varios clubes y tener varias inscripciones deportivas dentro de cada uno.
  - RN-097: Las cuotas y categorías pertenecen a cada inscripción deportiva, no al registro global del jugador.
</box>

## Así queda el modelo conceptual

<box border radius="lg" padding={3} gap={2}>
  <box align="center" gap={1}>
    <box background="surface-secondary" radius="md" padding={3} width="75%" align="center" gap={1}>
      **Jugador**
      <text color="secondary" size="xs">IdJugador · DNI único</text>
    </box>
    <svg viewBox="0 0 320 40" width="100%">
      <path d="M160 0 V16 M80 16 H240 M80 16 V38 M240 16 V38" stroke="currentColor" opacity=".5" strokeWidth="1.5" fill="none"/>
    </svg>
  </box>
  <grid columns={2} gap={2}>
    <grid-item>
      <box border radius="md" padding={3} gap={1}>
        **JugadorClub**
        <text color="secondary" size="xs">Obras Mendoza</text>
        <text size="xs">Nombre, apellido y contacto propios</text>
        <divider color="subtle"/>
        <text size="xs">Inscripción: Futsal C20</text>
        <text size="xs">Inscripción: Básquet</text>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="md" padding={3} gap={1}>
        **JugadorClub**
        <text color="secondary" size="xs">Otro club</text>
        <text size="xs">Nombre, apellido y contacto propios</text>
        <divider color="subtle"/>
        <text size="xs">Inscripción: Futsal</text>
      </box>
    </grid-item>
  </grid>
  <caption>Cada inscripción tendrá su categoría, estado y cuotas. El modelo permite cambios de club y práctica simultánea de varios deportes.</caption>
</box>

---

## Fase 2 — Gestión de jugadores

<text color="secondary" weight="medium" size="xs">Pregunta 18 — Baja de jugadores</text>

Imaginemos que Juan Pérez deja de jugar en Obras Mendoza. El administrador necesita darlo de baja, pero el club debe conservar el historial de cuotas y pagos.

**¿Cómo querés manejar la baja de un jugador?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Baja completa del club**

      El administrador marca al jugador como inactivo en Obras. Se desactivan todas sus inscripciones deportivas dentro de ese club.
    </radio>
    <radio value="b">
      **Opción B — Baja por disciplina y baja general**

      El administrador puede dar de baja una disciplina específica, por ejemplo futsal, sin afectar básquet. También puede dar de baja completamente al jugador del club, desactivando todas sus inscripciones.
    </radio>
    <radio value="c">
      **Opción C — Solo baja por disciplina**

      El administrador puede dar de baja cada inscripción deportiva individualmente. El jugador permanece registrado en el club, aunque no tenga ninguna inscripción activa.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 18 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"baja completa del jugador en el club, desactivando todas sus inscripciones",b:"baja individual por disciplina y baja general del jugador en el club",c:"solo baja por disciplina, manteniendo siempre la ficha del jugador en el club"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción B**, porque contempla tanto a quien abandona una disciplina como a quien deja definitivamente la institución. En ambos casos conservaríamos el historial económico.
```

## DEC-018

### Registro 86 — Baja de jugadores
Fuente: `9220003c-c444-452b-93f6-966eabe71dfc`

Respuesta previa del usuario:

elijo la opcion b pero  volviendo atras vamos a tener que corregir el tema de los administradores porque un administrador de basquet de obras no interviene con el administrador de futsal de obras, entonces acturian de manera independiente con sus jugadores

Registro original:

```text
Perfecto. Esto que acabás de plantear es **un cambio importante en la arquitectura del sistema**, porque ahora no alcanza con separar los datos por club: también tenemos que separarlos por disciplina.

Por ejemplo, Obras Mendoza puede tener futsal y básquet, pero cada disciplina funciona con sus propios administradores, jugadores, categorías y cuotas.

Vamos a corregir las decisiones anteriores antes de continuar.

## Registro SDD — Decisión 018

<row align="center" gap={2}>
  <icon name="check-circle-2" color="success"/>
  **Baja de jugadores — Aprobada (opción B)**
</row>

- RN-098: Se podrá dar de baja una inscripción deportiva individualmente.
- RN-099: Se podrá dar de baja a un jugador de todas las disciplinas de un club.
- RN-100: Las bajas serán lógicas y conservarán el historial de cuotas y pagos.
- RN-101: La baja en una disciplina no afectará las demás disciplinas ni otros clubes.

## Corrección del modelo de administradores

Anteriormente definimos que todos los administradores de un club tenían los mismos permisos. **Esa decisión debe revisarse** para incorporar el aislamiento entre disciplinas.

<box border radius="lg" padding={3} gap={2}>
  <box align="center" gap={1}>
    <box background="surface-secondary" radius="md" padding={3} width="80%" align="center" gap={1}>
      <icon name="shield" color="secondary"/>
      **Obras Mendoza**
      <text color="secondary" size="xs">Club</text>
    </box>
    <svg viewBox="0 0 320 40" width="100%">
      <path d="M160 0 V16 M80 16 H240 M80 16 V38 M240 16 V38" stroke="currentColor" opacity=".5" strokeWidth="1.5" fill="none"/>
    </svg>
  </box>
  <grid columns={2} gap={2}>
    <grid-item>
      <box border radius="lg" padding={3} gap={2}>
        <row align="center" gap={2}>
          <icon name="circle-dot" color="secondary"/>
          **Futsal**
        </row>
        <text size="xs" color="secondary">Administradores de futsal</text>
        <divider color="subtle"/>
        <text size="xs">Jugadores de futsal</text>
        <text size="xs">Categorías de futsal</text>
        <text size="xs">Cuotas y pagos de futsal</text>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="lg" padding={3} gap={2}>
        <row align="center" gap={2}>
          <icon name="circle-dot" color="secondary"/>
          **Básquet**
        </row>
        <text size="xs" color="secondary">Administradores de básquet</text>
        <divider color="subtle"/>
        <text size="xs">Jugadores de básquet</text>
        <text size="xs">Categorías de básquet</text>
        <text size="xs">Cuotas y pagos de básquet</text>
      </box>
    </grid-item>
  </grid>
  <row align="center" justify="center" gap={2}>
    <icon name="lock" color="secondary"/>
    <text size="xs" color="secondary">Sin acceso cruzado entre disciplinas</text>
  </row>
</box>

La separación deberá aplicarse también en el backend, no solamente ocultando pantallas.

Esto nos obliga a revisar especialmente **DEC-005 y DEC-006**, que regulaban los permisos y la creación de administradores.

---

## Pregunta 19 — Estructura de administradores por disciplina

Ahora hay que decidir si, además de los administradores específicos de cada deporte, existirá un administrador general del club.

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo administradores por disciplina**

      Cada disciplina tiene sus propios administradores, que gestionan exclusivamente sus jugadores, cuotas y categorías.

      Vos, como superadministrador de la plataforma, sos el único que puede gestionar el club en su conjunto.
    </radio>
    <radio value="b">
      **Opción B — Administrador general del club + administradores por disciplina**

      Existe un administrador general de Obras que puede supervisar todas sus disciplinas, crear administradores y configurar el club.

      Los administradores de futsal y básquet solamente gestionan su propia disciplina.
    </radio>
    <radio value="c">
      **Opción C — Administradores de varias disciplinas autorizadas**

      No existe un administrador general obligatorio. Cada administrador puede recibir permisos sobre una o varias disciplinas específicas.

      Por ejemplo, un administrador podría gestionar futsal y básquet, mientras otro solamente futsal.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 19 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo administradores por disciplina y superadministrador global",b:"administrador general del club y administradores independientes por disciplina",c:"administradores con acceso a una o varias disciplinas autorizadas, sin administrador general obligatorio"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

**Mi recomendación es la opción B.** Permite que Obras tenga una autoridad general para supervisar el club, mientras que futsal y básquet trabajan completamente separados. También facilita administrar varias disciplinas sin que vos tengas que intervenir constantemente.

Una vez que resolvamos esto, ajustaremos formalmente las decisiones anteriores y seguiremos con el relevamiento de jugadores.
```

## DEC-019

### Registro 89 — Administración independiente por disciplina
Fuente: `c06d956e-b271-4cf6-84a1-7f7a9f297be0`

Respuesta previa del usuario:

Para la pregunta 19 del relevamiento SDD elijo la opción A: solo administradores por disciplina y superadministrador global.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 19 — Confirmada**
</row>

Elegiste una estructura donde **cada disciplina funciona de manera independiente**, sin un administrador general del club.

El superadministrador de la plataforma será el único con acceso global a todas las instituciones y disciplinas.

## Registro SDD — Decisión 019

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-019 — Administración independiente por disciplina**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-102: No existirá el rol de administrador general del club.
  - RN-103: Cada administrador estará asociado a un club y una disciplina.
  - RN-104: Los administradores podrán gestionar únicamente los jugadores, categorías, inscripciones, cuotas y pagos de su disciplina.
  - RN-105: Dos disciplinas del mismo club tendrán administraciones independientes.
  - RN-106: El superadministrador tendrá acceso global a todos los clubes y disciplinas.
  - RN-107: Los permisos deberán validarse en el backend para evitar accesos no autorizados.

  **Decisiones anteriores que debemos actualizar:**

  - **DEC-005:** Los administradores ya no tienen permisos sobre todo el club, sino sobre su disciplina.
  - **DEC-006:** Debemos redefinir quién puede crear, modificar y desactivar administradores.
  - **DEC-012:** La configuración visual del club ya no puede quedar abierta indiscriminadamente a todos los administradores de disciplinas; queda pendiente definir quién la gestiona.

  <caption>Las decisiones DEC-005, DEC-006 y DEC-012 quedan parcialmente pendientes de revisión; no las consideraremos vigentes en sus aspectos incompatibles con DEC-019.</caption>
</box>

## Estructura actual

<box border radius="lg" padding={3} gap={2}>
  <box align="center" gap={1}>
    <box background="surface-secondary" radius="md" padding={3} width="85%" align="center" gap={1}>
      <icon name="shield-check" color="secondary" size="lg"/>
      **Superadministrador**
      <text color="secondary" size="xs">Control global de la plataforma</text>
    </box>
    <icon name="arrow-down" color="secondary"/>
    <box border radius="md" padding={3} width="85%" align="center" gap={1}>
      **Obras Mendoza**
      <text color="secondary" size="xs">Club</text>
    </box>
    <svg viewBox="0 0 320 36" width="100%">
      <path d="M160 0 V14 M80 14 H240 M80 14 V34 M240 14 V34" stroke="currentColor" strokeWidth="1.5" opacity=".5" fill="none"/>
    </svg>
  </box>
  <grid columns={2} gap={2}>
    <grid-item>
      <box border radius="md" padding={3} gap={2}>
        **Futsal**
        <text color="secondary" size="xs">Administradores propios</text>
        <divider color="subtle"/>
        <text size="xs">Jugadores</text>
        <text size="xs">Categorías</text>
        <text size="xs">Cuotas y pagos</text>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="md" padding={3} gap={2}>
        **Básquet**
        <text color="secondary" size="xs">Administradores propios</text>
        <divider color="subtle"/>
        <text size="xs">Jugadores</text>
        <text size="xs">Categorías</text>
        <text size="xs">Cuotas y pagos</text>
      </box>
    </grid-item>
  </grid>
</box>

---

## Pregunta 20 — Creación y gestión de administradores

Ahora necesitamos resolver quién puede dar de alta nuevos administradores dentro de cada disciplina.

Por ejemplo, si Obras Futsal tiene dos encargados de cobrar cuotas y necesita incorporar un tercero, ¿quién debería poder crearle una cuenta?

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo el superadministrador**

      Vos creás, modificás y desactivás todas las cuentas administrativas de todas las disciplinas.
    </radio>
    <radio value="b">
      **Opción B — Superadministrador y administradores de la misma disciplina**

      Vos creás el primer administrador de cada disciplina. Después, los administradores de futsal pueden crear, modificar o desactivar otros administradores de futsal, pero nunca de básquet.
    </radio>
    <radio value="c">
      **Opción C — Solo el superadministrador crea cuentas**

      Vos creás los nuevos administradores, pero los administradores de cada disciplina pueden modificar o desactivar cuentas de su propia disciplina.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 20 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo el superadministrador crea, modifica y desactiva administradores",b:"el superadministrador crea el primer administrador y luego los administradores de cada disciplina gestionan las cuentas de su propia disciplina",c:"solo el superadministrador crea cuentas, pero los administradores de cada disciplina pueden modificarlas y desactivarlas dentro de su disciplina"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción B**, porque mantiene la independencia entre disciplinas y evita que tengas que intervenir cada vez que un club incorpora un nuevo encargado.
```

## DEC-020

### Registro 91 — Gestión de administradores por disciplina
Fuente: `089f40a8-cefc-4425-ba75-6210074bbb29`

Respuesta previa del usuario:

Para la pregunta 20 del relevamiento SDD elijo la opción B: el superadministrador crea el primer administrador y luego los administradores de cada disciplina gestionan las cuentas de su propia disciplina.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 20 — Confirmada**
</row>

El superadministrador creará el primer administrador de cada disciplina. A partir de ese momento, los administradores podrán gestionar otras cuentas **exclusivamente dentro de su propia disciplina**.

## Registro SDD — Decisión 020

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-020 — Gestión de administradores por disciplina**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-108: El superadministrador crea el primer administrador de cada disciplina.
  - RN-109: Los administradores pueden crear nuevas cuentas administrativas dentro de su disciplina.
  - RN-110: Los administradores pueden modificar y desactivar otras cuentas de su misma disciplina.
  - RN-111: Ningún administrador puede gestionar cuentas de otra disciplina, aunque pertenezca al mismo club.
  - RN-112: El superadministrador puede gestionar las cuentas de todas las disciplinas.
  - RN-113: Todas las cuentas administrativas de una misma disciplina tendrán los mismos permisos funcionales.

  **Medidas de seguridad pendientes de confirmar:** impedir que se desactive el último administrador activo de una disciplina y definir si un administrador puede desactivar su propia cuenta.

  <text color="secondary" size="xs">Con esta decisión, DEC-006 queda reformulada para operar a nivel de disciplina en lugar de club.</text>
</box>

---

## Fase 2 — Administración de disciplinas

<text color="secondary" weight="medium" size="xs">Pregunta 21 — Configuración general del club</text>

Hay otro punto que debemos corregir de las decisiones anteriores.

Como ahora Obras Mendoza puede tener administradores independientes de futsal y básquet, necesitamos decidir quién controla los datos generales del club: nombre, escudo, colores e imagen de portada.

**¿Quién podrá modificar la identidad visual y la configuración general del club?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo el superadministrador**

      Vos administrás los datos generales de Obras Mendoza. Los administradores de cada disciplina solamente gestionan sus jugadores, categorías y cuotas.
    </radio>
    <radio value="b">
      **Opción B — Identidad visual independiente por disciplina**

      Vos administrás los datos generales del club, pero cada disciplina puede configurar su propia identidad visual: escudo, colores e imagen de portada.

      <text color="secondary" size="xs">Por ejemplo, Obras Futsal y Obras Básquet podrían tener portales visualmente diferentes, aunque pertenezcan al mismo club.</text>
    </radio>
    <radio value="c">
      **Opción C — Configuración general compartida**

      Todos los administradores de cualquier disciplina pueden modificar la identidad visual general del club. Los cambios se reflejan en todas sus disciplinas.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 21 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo el superadministrador modifica la identidad visual y configuración general del club",b:"el superadministrador configura el club y cada disciplina administra su propia identidad visual",c:"todos los administradores de las disciplinas pueden modificar la identidad visual compartida del club"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción B**. Así mantenemos una estructura multiclub, pero permitimos que cada disciplina tenga su propia identidad y administración, sin interferir con las demás.
```

## DEC-021

### Registro 95 — Permisos del delegado general
Fuente: `6401d32a-c58d-4897-a1e0-7a5a7a72cdfb`

Respuesta previa del usuario:

Para la pregunta 21 del relevamiento SDD elijo la opción C: delegado general configura club y disciplinas y consulta todos los datos deportivos y pagos, pero no los modifica.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 21 — Confirmada**
</row>

El **delegado general del club** podrá administrar la configuración institucional y las disciplinas, además de consultar toda la información deportiva y económica. Sin embargo, **no podrá modificar jugadores, inscripciones, cuotas ni pagos**.

## Registro SDD — Decisión 021

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-021 — Permisos del delegado general**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-114: El delegado general puede modificar los datos generales y la identidad visual de su club.
  - RN-115: Puede agregar y administrar disciplinas dentro de su club.
  - RN-116: Puede gestionar las cuentas administrativas de las disciplinas de su club.
  - RN-117: Puede consultar jugadores, categorías, inscripciones, cuotas y pagos de todas las disciplinas.
  - RN-118: No puede crear, modificar ni dar de baja jugadores o inscripciones deportivas.
  - RN-119: No puede generar, modificar, anular ni registrar pagos o cuotas de las disciplinas.
  - RN-120: No puede acceder a la información de otros clubes.

  <caption>Los permisos de consulta son de solo lectura y deberán aplicarse también en el backend.</caption>
</box>

## Matriz de permisos actualizada

| Funcionalidad | Delegado general | Admin. disciplina |
|---|---|---|
| Identidad visual del club | <icon name="check" color="success"/> | <icon name="x" color="secondary"/> |
| Agregar disciplinas | <icon name="check" color="success"/> | <icon name="x" color="secondary"/> |
| Gestionar administradores | <icon name="check" color="success"/> Todas | <icon name="check" color="success"/> Propia |
| Consultar jugadores y cuotas | <icon name="check" color="success"/> Todas | <icon name="check" color="success"/> Propia |
| Modificar jugadores y cuotas | <icon name="x" color="secondary"/> | <icon name="check" color="success"/> Propia |
| Registrar pagos | <icon name="x" color="secondary"/> | <icon name="check" color="success"/> Propia |

El superadministrador conserva los permisos globales de la plataforma.

---

## Pregunta 22 — Cantidad de delegados generales

Ahora necesitamos definir cuántos delegados generales puede tener una misma institución.

Por ejemplo, Obras Mendoza podría tener un presidente, un tesorero y un secretario que necesitan supervisar todas las disciplinas.

**¿Cuántos delegados generales querés permitir por club?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Un único delegado general**

      Cada club tiene una sola cuenta con este rol. Únicamente el superadministrador puede reemplazarla.
    </radio>
    <radio value="b">
      **Opción B — Múltiples delegados generales**

      Cada club puede tener varios delegados generales, todos con los mismos permisos. Pueden crear, modificar y desactivar otros delegados generales del mismo club.

      <text color="secondary" size="xs">El superadministrador crea el primero y también puede administrar los demás.</text>
    </radio>
    <radio value="c">
      **Opción C — Múltiples delegados, gestionados solo por el superadministrador**

      Puede haber varios delegados generales, pero únicamente vos podés crear, modificar o desactivar esas cuentas.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 22 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"un único delegado general por club, reemplazado por el superadministrador",b:"múltiples delegados generales por club que pueden gestionar otras cuentas de delegado del mismo club",c:"múltiples delegados generales por club, cuyas cuentas gestiona exclusivamente el superadministrador"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción B**. Permite que varios responsables institucionales supervisen el club sin depender de una sola persona y sin que tengas que intervenir cada vez que cambien sus autoridades.
```

## DEC-022

### Registro 97 — Múltiples delegados generales
Fuente: `02ab4130-2b06-4551-b7e6-e43925d6e724`

Respuesta previa del usuario:

Para la pregunta 22 del relevamiento SDD elijo la opción B: múltiples delegados generales por club que pueden gestionar otras cuentas de delegado del mismo club.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 22 — Confirmada**
</row>

Cada club podrá tener **múltiples delegados generales**, todos con los mismos permisos. El superadministrador creará el primero y, posteriormente, los delegados podrán administrar otras cuentas de delegado de su propio club.

## Registro SDD — Decisión 022

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-022 — Múltiples delegados generales**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-121: Un club podrá tener uno o varios delegados generales.
  - RN-122: Todos los delegados generales de un mismo club tendrán los mismos permisos.
  - RN-123: El superadministrador creará el primer delegado general.
  - RN-124: Los delegados generales podrán crear, modificar y desactivar otros delegados de su club.
  - RN-125: El superadministrador podrá gestionar los delegados de cualquier club.
  - RN-126: Ningún delegado podrá gestionar cuentas de otro club.
  - RN-127: La cantidad de delegados no tendrá un límite funcional predefinido.

  **Salvaguardas propuestas, pendientes de confirmación:** impedir que un club quede sin delegados activos y definir las condiciones para desactivar la propia cuenta.
</box>

## Estructura de roles actual

<box border radius="lg" padding={3} gap={2}>
  <box align="center" gap={1}>
    <box background="surface-secondary" radius="md" padding={3} width="85%" align="center" gap={1}>
      <icon name="shield-check" color="secondary"/>
      **Superadministrador**
      <text color="secondary" size="xs">Toda la plataforma</text>
    </box>
    <icon name="arrow-down" color="secondary"/>
    <box border radius="md" padding={3} width="85%" align="center" gap={1}>
      **Club: Obras Mendoza**
      <text color="secondary" size="xs">Uno o varios delegados generales</text>
    </box>
    <svg viewBox="0 0 320 36" width="100%">
      <path d="M160 0 V14 M80 14 H240 M80 14 V34 M240 14 V34" stroke="currentColor" strokeWidth="1.5" opacity=".5" fill="none"/>
    </svg>
  </box>
  <grid columns={2} gap={2}>
    <grid-item>
      <box border radius="md" padding={3} gap={1}>
        **Futsal**
        <text color="secondary" size="xs">Administradores propios</text>
        <text size="xs">Jugadores y categorías</text>
        <text size="xs">Cuotas y pagos</text>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="md" padding={3} gap={1}>
        **Básquet**
        <text color="secondary" size="xs">Administradores propios</text>
        <text size="xs">Jugadores y categorías</text>
        <text size="xs">Cuotas y pagos</text>
      </box>
    </grid-item>
  </grid>
</box>

---

## Fase 2 — Gestión de disciplinas

<text color="secondary" weight="medium" size="xs">Pregunta 23 — Alta de nuevas disciplinas</text>

Supongamos que Obras Mendoza comienza utilizando la plataforma únicamente para futsal. Seis meses después, el club decide incorporar básquet.

Ya definimos que el club podrá agregar disciplinas con el tiempo. Ahora necesitamos establecer **cómo se habilitará una nueva disciplina**.

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo el superadministrador**

      El club debe solicitarte que agregues básquet. Vos creás la disciplina y habilitás su administración.
    </radio>
    <radio value="b">
      **Opción B — Los delegados generales pueden agregar disciplinas**

      Los delegados generales pueden crear nuevas disciplinas desde su panel, configurar sus datos y asignar administradores.

      <text color="secondary" size="xs">El superadministrador también puede hacerlo. No requiere aprobación previa.</text>
    </radio>
    <radio value="c">
      **Opción C — Solicitud con aprobación**

      El delegado general solicita incorporar una disciplina y el superadministrador debe aprobarla antes de habilitarla.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 23 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo el superadministrador puede crear disciplinas",b:"los delegados generales y el superadministrador pueden crear disciplinas sin aprobación previa",c:"los delegados generales solicitan nuevas disciplinas y el superadministrador debe aprobarlas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción B**, porque hace que la plataforma sea escalable: cada club podrá incorporar nuevos deportes sin depender de vos para cada alta, manteniendo el control dentro de sus delegados autorizados.
```

## DEC-023

### Registro 99 — Alta dinámica de disciplinas
Fuente: `3331ad23-bb93-4e01-a238-5afd013af408`

Respuesta previa del usuario:

Para la pregunta 23 del relevamiento SDD elijo la opción B: los delegados generales y el superadministrador pueden crear disciplinas sin aprobación previa.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 23 — Confirmada**
</row>

Los **delegados generales y el superadministrador** podrán crear nuevas disciplinas sin aprobación previa. De esta manera, un club podrá comenzar con futsal e incorporar básquet, vóley u otros deportes cuando lo necesite.

## Registro SDD — Decisión 023

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-023 — Alta dinámica de disciplinas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-128: El superadministrador puede crear disciplinas en cualquier club.
  - RN-129: Los delegados generales pueden crear disciplinas en su propio club.
  - RN-130: La creación de disciplinas no requiere aprobación previa.
  - RN-131: Los administradores de disciplina no pueden crear nuevas disciplinas.
  - RN-132: Un club puede comenzar con una sola disciplina y agregar otras posteriormente.
  - RN-133: Agregar una disciplina no modifica las inscripciones, cuotas ni pagos existentes de las demás disciplinas.
  - RN-134: Cada nueva disciplina tendrá su propia administración, categorías, inscripciones y cuotas.

  **Implicación técnica:** la relación `Club → Disciplina` será de uno a muchos, sin un número fijo de disciplinas.

  <text color="secondary" size="xs">Quedan pendientes las reglas para desactivar disciplinas y gestionar su historial.</text>
</box>

---

## Fase 2 — Gestión de disciplinas

<text color="secondary" weight="medium" size="xs">Pregunta 24 — Baja o desactivación de disciplinas</text>

Supongamos que Obras Mendoza tiene futsal, básquet y vóley. Después de dos años, decide dejar de utilizar la plataforma para vóley, pero quiere conservar los registros históricos.

**¿Qué debería suceder cuando se desactiva una disciplina?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Baja definitiva**

      Se elimina la disciplina y sus inscripciones. Los pagos históricos se conservan únicamente como registros contables.
    </radio>
    <radio value="b">
      **Opción B — Desactivación lógica con historial**

      La disciplina queda inactiva, pero se conservan sus jugadores, categorías, inscripciones, cuotas y pagos.

      No se generan nuevas cuotas ni se permiten nuevos pagos mientras esté inactiva. Puede reactivarse posteriormente.
    </radio>
    <radio value="c">
      **Opción C — Desactivación con cobro de deudas**

      La disciplina queda inactiva y no genera nuevas cuotas ni permite nuevas inscripciones, pero los jugadores pueden seguir pagando cuotas pendientes anteriores a la baja.

      Puede reactivarse posteriormente.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 24 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"baja definitiva de disciplina con conservación solo de registros contables",b:"desactivación lógica, conservando todo el historial, bloqueando nuevas cuotas y pagos y permitiendo reactivación",c:"desactivación lógica, conservando historial y permitiendo cobrar deudas anteriores, con posibilidad de reactivación"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**: permite que un club deje de operar una disciplina sin perder la posibilidad de cobrar cuotas que ya estaban pendientes. Así evitamos que la baja administrativa implique perder ingresos legítimos.
```

## DEC-024

### Registro 101 — Desactivación y reactivación de disciplinas
Fuente: `3b1e95af-2891-4e49-b084-3d0890dd96c1`

Respuesta previa del usuario:

Para la pregunta 24 del relevamiento SDD elijo la opción C: desactivación lógica, conservando historial y permitiendo cobrar deudas anteriores, con posibilidad de reactivación.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 24 — Confirmada**
</row>

Las disciplinas tendrán **desactivación lógica**, conservando todo su historial. Aunque estén inactivas, los jugadores podrán seguir pagando las cuotas pendientes generadas antes de la desactivación.

## Registro SDD — Decisión 024

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-024 — Desactivación y reactivación de disciplinas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-135: Las disciplinas podrán desactivarse sin eliminar sus registros.
  - RN-136: Se conservarán jugadores, categorías, inscripciones, cuotas y pagos históricos.
  - RN-137: Una disciplina inactiva no podrá registrar nuevas inscripciones.
  - RN-138: No se generarán nuevas cuotas mientras la disciplina esté inactiva.
  - RN-139: Los jugadores podrán pagar deudas generadas antes de la desactivación.
  - RN-140: Una disciplina podrá reactivarse y recuperar su configuración e historial.
  - RN-141: La desactivación de una disciplina no afectará a las demás disciplinas del club.

  **Estados propuestos:**

  <row align="center" justify="center" gap={2}>
    <badge color="success">Activa</badge>
    <icon name="arrow-left-right" color="secondary"/>
    <badge>Inactiva</badge>
  </row>

  <caption>Queda pendiente definir si la reactivación genera cuotas retroactivas por los meses de inactividad.</caption>
</box>

## Ejemplo práctico

<box border radius="lg" padding={3} gap={2}>
  **Obras Mendoza — Vóley**

  <row justify="between" align="center">
    <text color="secondary" size="sm">Estado</text>
    <badge>Inactiva</badge>
  </row>
  <divider color="subtle"/>
  <row justify="between" align="center">
    <text size="sm">Consultar historial</text>
    <icon name="check" color="success"/>
  </row>
  <row justify="between" align="center">
    <text size="sm">Cobrar cuotas anteriores</text>
    <icon name="check" color="success"/>
  </row>
  <row justify="between" align="center">
    <text size="sm">Generar nuevas cuotas</text>
    <icon name="x" color="danger"/>
  </row>
  <row justify="between" align="center">
    <text size="sm">Inscribir nuevos jugadores</text>
    <icon name="x" color="danger"/>
  </row>
  <row justify="between" align="center">
    <text size="sm">Reactivar disciplina</text>
    <icon name="check" color="success"/>
  </row>
</box>

---

## Fase 2 — Gestión de disciplinas

<text color="secondary" weight="medium" size="xs">Pregunta 25 — Permisos para desactivar y reactivar disciplinas</text>

Ya definimos que los delegados generales pueden crear disciplinas. Ahora necesitamos decidir quién podrá desactivarlas y reactivarlas.

Por ejemplo, si Obras deja de utilizar la plataforma para básquet, ¿quién puede realizar esa operación?

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo el superadministrador**

      Únicamente vos podés desactivar o reactivar disciplinas. Los delegados generales deben solicitarte el cambio.
    </radio>
    <radio value="b">
      **Opción B — Delegados generales y superadministrador**

      Los delegados generales pueden desactivar y reactivar disciplinas de su propio club. El superadministrador también puede hacerlo en cualquier club.

      <text color="secondary" size="xs">Los administradores de cada deporte no pueden desactivar su propia disciplina.</text>
    </radio>
    <radio value="c">
      **Opción C — Delegados generales solicitan, superadministrador aprueba**

      Los delegados generales pueden solicitar una desactivación o reactivación, pero el cambio requiere tu aprobación.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 25 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo el superadministrador puede desactivar y reactivar disciplinas",b:"delegados generales y superadministrador pueden desactivar y reactivar disciplinas sin aprobación previa",c:"los delegados generales solicitan la desactivación o reactivación y el superadministrador la aprueba"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción B**, porque mantiene la autonomía del delegado general para gestionar las disciplinas del club, sin otorgar a los administradores deportivos la posibilidad de desactivar toda su área de trabajo.
```

## DEC-025

### Registro 103 — Control de activación de disciplinas
Fuente: `99dd6cf4-f9b9-4342-bf81-5bb5de0159fa`

Respuesta previa del usuario:

Para la pregunta 25 del relevamiento SDD elijo la opción A: solo el superadministrador puede desactivar y reactivar disciplinas.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 25 — Confirmada**
</row>

Únicamente el **superadministrador global** podrá desactivar y reactivar disciplinas. Los delegados generales podrán crearlas y configurarlas, pero no modificar su estado de actividad.

## Registro SDD — Decisión 025

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-025 — Control de activación de disciplinas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-142: Solo el superadministrador puede desactivar una disciplina.
  - RN-143: Solo el superadministrador puede reactivar una disciplina.
  - RN-144: Los delegados generales pueden crear y configurar disciplinas, pero no desactivarlas ni reactivarlas.
  - RN-145: Los administradores de disciplina no pueden modificar el estado de actividad de ninguna disciplina.
  - RN-146: Toda desactivación o reactivación debe registrar quién la realizó y cuándo.
  - RN-147: Los permisos se validarán en el backend, independientemente de los controles visibles en la interfaz.
</box>

## Matriz de permisos actualizada

| Operación | Superadmin | Delegado general | Admin. disciplina |
|---|---|---|---|
| Crear disciplina | <icon name="check" color="success"/> | <icon name="check" color="success"/> | <icon name="x" color="secondary"/> |
| Configurar disciplina | <icon name="check" color="success"/> | <icon name="check" color="success"/> | <icon name="x" color="secondary"/> |
| Desactivar disciplina | <icon name="check" color="success"/> | <icon name="x" color="secondary"/> | <icon name="x" color="secondary"/> |
| Reactivar disciplina | <icon name="check" color="success"/> | <icon name="x" color="secondary"/> | <icon name="x" color="secondary"/> |

---

## Fase 2 — Gestión de disciplinas

<text color="secondary" weight="medium" size="xs">Pregunta 26 — Cuotas después de reactivar una disciplina</text>

Supongamos que Obras Mendoza desactiva básquet el 1 de marzo y lo reactiva el 1 de junio.

Durante marzo, abril y mayo no se generaron nuevas cuotas, pero el sistema conserva toda la información anterior.

**¿Qué debería suceder con las cuotas de esos tres meses cuando se reactive básquet?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — No generar cuotas retroactivas**

      La disciplina vuelve a funcionar desde su reactivación. No se generan automáticamente cuotas correspondientes a los meses en que estuvo inactiva.
    </radio>
    <radio value="b">
      **Opción B — Generar todas las cuotas pendientes**

      Al reactivar la disciplina, el sistema genera automáticamente las cuotas de los meses durante los cuales estuvo inactiva.
    </radio>
    <radio value="c">
      **Opción C — Decidir al momento de reactivar**

      El superadministrador elige si desea generar o no las cuotas de los meses de inactividad.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 26 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"no generar cuotas retroactivas por los meses de inactividad",b:"generar automáticamente todas las cuotas de los meses inactivos al reactivar",c:"el superadministrador decide al reactivar si se generan cuotas retroactivas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A**, consistente con lo que ya definimos para la reactivación de clubes. Evita generar deudas inesperadas por períodos en los que la disciplina no estuvo operativa.
```

## DEC-026

### Registro 105 — Reactivación sin cuotas retroactivas
Fuente: `3e08ac5b-b265-4e68-8e9c-eea09327983d`

Respuesta previa del usuario:

Para la pregunta 26 del relevamiento SDD elijo la opción A: no generar cuotas retroactivas por los meses de inactividad.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 26 — Confirmada**
</row>

Cuando una disciplina se reactive, **no se generarán cuotas retroactivas** correspondientes a los meses en que estuvo inactiva. Las deudas anteriores seguirán existiendo y podrán pagarse.

## Registro SDD — Decisión 026

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-026 — Reactivación sin cuotas retroactivas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-148: Durante la inactividad no se generan nuevas cuotas.
  - RN-149: Al reactivar una disciplina, no se generan cuotas por los períodos inactivos.
  - RN-150: La generación de nuevas cuotas se reanuda a partir de la reactivación, según la configuración de la disciplina.
  - RN-151: Las cuotas pendientes anteriores a la desactivación conservan su estado y podrán pagarse.
  - RN-152: Los pagos históricos permanecen registrados.
  - RN-153: El sistema deberá conservar las fechas de desactivación y reactivación para identificar los períodos inactivos.

  <caption>La fecha exacta de generación de la primera cuota después de la reactivación dependerá de las reglas de facturación que definiremos posteriormente.</caption>
</box>

## Ejemplo práctico

<box border radius="lg" padding={3} gap={2}>
  **Obras Mendoza — Básquet**

  <grid columns={4} gap={2}>
    {#each [{mes:"Marzo",estado:"Inactiva",activo:false},{mes:"Abril",estado:"Inactiva",activo:false},{mes:"Mayo",estado:"Inactiva",activo:false},{mes:"Junio",estado:"Reactivada",activo:true}] as item}
      <grid-item>
        <box background="surface-secondary" radius="md" padding={2} gap={1} align="center">
          <text weight="medium" size="sm">{item.mes}</text>
          <icon name={item.activo?"check-circle":"minus-circle"} color={item.activo?"success":"secondary"}/>
          <text color="secondary" size="2xs" textAlign="center">{item.estado}</text>
        </box>
      </grid-item>
    {/each}
  </grid>
  <text color="secondary" size="sm">Marzo, abril y mayo no generan deuda retroactiva. Junio vuelve a estar habilitado para la generación de cuotas según el calendario configurado.</text>
</box>

---

## Fase 2 — Gestión de categorías

<text color="secondary" weight="medium" size="xs">Pregunta 27 — Creación de categorías deportivas</text>

Ahora vamos a definir cómo se organizan las categorías dentro de cada disciplina.

Por ejemplo, Obras Futsal podría tener C15, C17, C20 y Primera, mientras que Obras Básquet podría tener U13, U15, U17 y Mayores.

**¿Quién debería poder crear, modificar y desactivar categorías?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo los delegados generales**

      Los delegados configuran las categorías de todas las disciplinas. Los administradores deportivos únicamente asignan jugadores a las categorías existentes.
    </radio>
    <radio value="b">
      **Opción B — Administradores de cada disciplina**

      Los administradores de futsal gestionan sus categorías, los de básquet las suyas, etc. Los delegados generales pueden consultarlas, pero no modificarlas.

      <text color="secondary" size="xs">El superadministrador conserva acceso global.</text>
    </radio>
    <radio value="c">
      **Opción C — Delegados generales y administradores de disciplina**

      Ambos pueden crear, modificar y desactivar categorías. Los administradores deportivos solo pueden hacerlo dentro de su propia disciplina.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 27 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo delegados generales gestionan categorías",b:"administradores de cada disciplina gestionan sus categorías y delegados solo consultan",c:"delegados generales y administradores de disciplina pueden gestionar categorías dentro de sus ámbitos"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción B**, porque mantiene la separación que definimos: cada disciplina administra sus propias categorías y jugadores, mientras que el delegado general supervisa sin intervenir en la operación deportiva.
```

## DEC-027

### Registro 107 — Administración de categorías deportivas
Fuente: `6f592877-012a-4586-97df-1e8205f5db78`

Respuesta previa del usuario:

Para la pregunta 27 del relevamiento SDD elijo la opción B: administradores de cada disciplina gestionan sus categorías y delegados solo consultan.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 27 — Confirmada**
</row>

Los **administradores de cada disciplina** serán responsables de crear, modificar y desactivar sus categorías. Los delegados generales podrán consultarlas, pero no modificarlas.

## Registro SDD — Decisión 027

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-027 — Administración de categorías deportivas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-154: Cada categoría pertenece a una disciplina específica de un club.
  - RN-155: Los administradores pueden crear categorías exclusivamente en su disciplina.
  - RN-156: Los administradores pueden modificar y desactivar las categorías de su disciplina.
  - RN-157: Los delegados generales pueden consultar las categorías de todas las disciplinas de su club, sin modificarlas.
  - RN-158: El superadministrador conserva permisos globales.
  - RN-159: Los administradores de una disciplina no pueden gestionar categorías de otra disciplina.

  **Ejemplo:**

  <grid columns={2} gap={3}>
    <grid-item>
      <box border radius="lg" padding={3} gap={2}>
        **Obras — Futsal**
        <row align="center" gap={2}>
          <icon name="check-circle" color="success"/>
          <text size="sm">C15</text>
        </row>
        <row align="center" gap={2}>
          <icon name="check-circle" color="success"/>
          <text size="sm">C17</text>
        </row>
        <row align="center" gap={2}>
          <icon name="check-circle" color="success"/>
          <text size="sm">C20</text>
        </row>
        <row align="center" gap={2}>
          <icon name="check-circle" color="success"/>
          <text size="sm">Primera</text>
        </row>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="lg" padding={3} gap={2}>
        **Obras — Básquet**
        <row align="center" gap={2}>
          <icon name="check-circle" color="success"/>
          <text size="sm">U13</text>
        </row>
        <row align="center" gap={2}>
          <icon name="check-circle" color="success"/>
          <text size="sm">U15</text>
        </row>
        <row align="center" gap={2}>
          <icon name="check-circle" color="success"/>
          <text size="sm">U17</text>
        </row>
        <row align="center" gap={2}>
          <icon name="check-circle" color="success"/>
          <text size="sm">Mayores</text>
        </row>
      </box>
    </grid-item>
  </grid>
</box>

---

## Fase 2 — Gestión de categorías

<text color="secondary" weight="medium" size="xs">Pregunta 28 — Jugadores en varias categorías</text>

Supongamos que un jugador de Obras Futsal pertenece a C17, pero también participa en partidos de C20.

Necesitamos definir si el sistema permitirá que un jugador esté inscripto en más de una categoría de la misma disciplina.

**¿Cómo querés manejar esta situación?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Una categoría por disciplina**

      Cada jugador tiene una única categoría activa dentro de cada disciplina. Puede cambiar de categoría cuando corresponda, conservando el historial.

      <text color="secondary" size="xs">Por ejemplo, Juan pertenece a C17 aunque ocasionalmente juegue partidos de C20.</text>
    </radio>
    <radio value="b">
      **Opción B — Múltiples categorías por disciplina**

      Un jugador puede estar inscripto simultáneamente en C17 y C20 dentro de futsal. Cada inscripción puede tener sus propias cuotas.
    </radio>
    <radio value="c">
      **Opción C — Categoría principal y categorías secundarias**

      Cada jugador tiene una categoría principal, que determina su cuota, pero puede estar asociado a otras categorías secundarias sin generar cuotas adicionales.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 28 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"una única categoría activa por jugador y disciplina, con historial de cambios",b:"múltiples categorías simultáneas por jugador y disciplina, con posibles cuotas independientes",c:"una categoría principal que determina la cuota y categorías secundarias sin cuotas adicionales"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A** para el MVP. Simplifica la administración y evita cobrar dos veces a un jugador que participa ocasionalmente en una categoría superior.
```

## DEC-028

### Registro 109 — Categoría única por inscripción deportiva
Fuente: `e3c73641-eeab-475c-891c-b9f5abc9ea2a`

Respuesta previa del usuario:

Para la pregunta 28 del relevamiento SDD elijo la opción A: una única categoría activa por jugador y disciplina, con historial de cambios.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 28 — Confirmada**
</row>

Cada jugador tendrá **una única categoría activa por disciplina**, pero podrá cambiar de categoría conservando el historial.

Por ejemplo, un jugador de C17 podrá participar ocasionalmente en partidos de C20 sin necesidad de tener una segunda inscripción ni pagar otra cuota.

## Registro SDD — Decisión 028

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-028 — Categoría única por inscripción deportiva**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-160: Un jugador podrá tener una única categoría activa por disciplina y club.
  - RN-161: Un jugador podrá cambiar de categoría sin perder su inscripción deportiva.
  - RN-162: Cada cambio de categoría conservará un registro histórico.
  - RN-163: El historial identificará la categoría anterior, la nueva y la fecha del cambio.
  - RN-164: Participar ocasionalmente en otra categoría no generará automáticamente una segunda inscripción ni una cuota adicional.
  - RN-165: Los administradores podrán consultar los cambios históricos de categoría de los jugadores de su disciplina.

  **Modelo de datos propuesto:**

  | Entidad | Función |
  |---|---|
  | `Categoria` | Categorías disponibles en cada disciplina |
  | `Inscripcion` | Vincula al jugador con una disciplina |
  | `HistorialCategoria` | Registra los cambios de categoría |

  <caption>La fecha de aplicación de los cambios sobre el importe de las cuotas todavía debe definirse.</caption>
</box>

---

## Fase 2 — Gestión de categorías

<text color="secondary" weight="medium" size="xs">Pregunta 29 — Cambio de categoría y cuotas</text>

Supongamos que Juan juega en C17 de Obras Futsal y paga una cuota mensual de $20.000.

El 15 de junio lo pasan a C20, donde la cuota mensual es de $25.000.

**¿Qué debería suceder con la cuota de junio?**

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    <box gap={1}>
      <text color="secondary" size="xs">Categoría anterior</text>
      **C17**
      <title size="lg" color="default" tabularNums>$20.000</title>
    </box>
    <icon name="arrow-right" color="secondary" size="lg"/>
    <box gap={1} align="end">
      <text color="secondary" size="xs">Nueva categoría</text>
      **C20**
      <title size="lg" color="default" tabularNums>$25.000</title>
    </box>
  </row>
  <caption>Importes ilustrativos para analizar el comportamiento.</caption>
</box>

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Aplicar el nuevo importe desde el mes siguiente**

      La cuota de junio conserva su importe original de $20.000. A partir de julio se utiliza la cuota de C20 ($25.000).
    </radio>
    <radio value="b">
      **Opción B — Aplicar inmediatamente**

      Si la cuota de junio todavía no fue pagada, se actualiza a $25.000. Si ya fue pagada, no se modifica.
    </radio>
    <radio value="c">
      **Opción C — El administrador decide**

      Al cambiar la categoría, el administrador elige si aplica el nuevo importe inmediatamente o desde el mes siguiente.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 29 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"el nuevo importe de categoría se aplica desde el mes siguiente, sin alterar la cuota del mes actual",b:"el nuevo importe se aplica a la cuota impaga del mes actual; las pagadas no se modifican",c:"el administrador decide si el nuevo importe se aplica inmediatamente o desde el mes siguiente"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A**. Es más predecible para los jugadores, evita modificar deudas ya generadas y simplifica el historial contable.
```

## DEC-029

### Registro 111 — Impacto económico del cambio de categoría
Fuente: `7c2b212d-1d7e-482a-9ada-c998e923fe43`

Respuesta previa del usuario:

Para la pregunta 29 del relevamiento SDD elijo la opción A: el nuevo importe de categoría se aplica desde el mes siguiente, sin alterar la cuota del mes actual.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 29 — Confirmada**
</row>

Cuando un jugador cambie de categoría, **el nuevo importe se aplicará desde el mes siguiente**. La cuota del mes actual conservará su valor original, independientemente de si fue pagada o sigue pendiente.

## Registro SDD — Decisión 029

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-029 — Impacto económico del cambio de categoría**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-166: Los cambios de categoría no modifican el importe de la cuota del mes en curso.
  - RN-167: El importe correspondiente a la nueva categoría se aplicará desde el primer día del mes siguiente.
  - RN-168: Las cuotas ya generadas conservarán sus importes originales.
  - RN-169: La regla se aplica tanto a cuotas pagadas como pendientes.
  - RN-170: El sistema conservará el historial del cambio, incluyendo su fecha y las categorías involucradas.
  - RN-171: El cambio no generará automáticamente una cuota adicional.

  **Ejemplo:**

  | Período | Categoría | Importe |
  |---|---|---:|
  | Mayo | C17 | $20.000 |
  | Junio | C17 → C20 | $20.000 |
  | Julio | C20 | $25.000 |
  | Agosto | C20 | $25.000 |

  <caption>Importes ilustrativos. El cambio se realizó el 15 de junio.</caption>
</box>

---

## Fase 3 — Configuración de cuotas

Ahora comenzamos una de las partes más importantes del sistema: **cómo se definen los importes que deberán pagar los jugadores**.

<text color="secondary" weight="medium" size="xs">Pregunta 30 — Cómo se determina el importe mensual</text>

Supongamos que Obras Futsal tiene cuatro categorías: C15, C17, C20 y Primera.

**¿Cómo querés que los administradores configuren el valor de las cuotas?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Importe único por disciplina**

      Todos los jugadores de futsal pagan el mismo importe mensual, independientemente de su categoría.

      <text color="secondary" size="xs">Ejemplo: todas las categorías pagan $25.000.</text>
    </radio>
    <radio value="b">
      **Opción B — Importe por categoría**

      Cada categoría tiene su propio valor mensual, configurado por los administradores de la disciplina.

      <box background="surface-secondary" radius="md" padding={2} gap={1}>
        <row justify="between"><text size="sm">C15</text><text size="sm" weight="medium">$20.000</text></row>
        <row justify="between"><text size="sm">C17</text><text size="sm" weight="medium">$22.000</text></row>
        <row justify="between"><text size="sm">C20</text><text size="sm" weight="medium">$25.000</text></row>
        <row justify="between"><text size="sm">Primera</text><text size="sm" weight="medium">$30.000</text></row>
      </box>
    </radio>
    <radio value="c">
      **Opción C — Importe por categoría con excepciones individuales**

      Cada categoría tiene un valor mensual predeterminado, pero el administrador puede establecer un importe diferente para determinados jugadores.

      <text color="secondary" size="xs">Por ejemplo, C20 paga $25.000, pero un jugador con una beca o descuento paga $15.000.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 30 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"importe mensual único por disciplina",b:"importe mensual independiente por categoría",c:"importe mensual por categoría con posibilidad de excepciones individuales por jugador"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**, porque contempla la realidad de los clubes: categorías con importes distintos y casos particulares como becas, descuentos familiares o acuerdos especiales. Más adelante definiríamos cómo se autorizan y registran esas excepciones.
```

## DEC-030

### Registro 113 — Cuotas por categoría con excepciones individuales
Fuente: `4ce64d97-08cb-4ed4-a922-d80735074b93`

Respuesta previa del usuario:

Para la pregunta 30 del relevamiento SDD elijo la opción C: importe mensual por categoría con posibilidad de excepciones individuales por jugador.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 30 — Confirmada**
</row>

Cada categoría deportiva tendrá un **importe mensual base**, pero se podrán configurar excepciones individuales para determinados jugadores, como becas, descuentos o acuerdos especiales.

## Registro SDD — Decisión 030

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-030 — Cuotas por categoría con excepciones individuales**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-172: Cada categoría tendrá un importe mensual base configurable.
  - RN-173: Los administradores de cada disciplina podrán configurar los importes de sus categorías.
  - RN-174: Se podrán establecer excepciones individuales para jugadores de una categoría.
  - RN-175: Cuando exista una excepción vigente, el importe individual prevalecerá sobre el importe base de la categoría.
  - RN-176: Las excepciones estarán vinculadas a la inscripción del jugador en una disciplina, sin afectar sus inscripciones en otras disciplinas o clubes.
  - RN-177: Los importes de las cuotas ya generadas no se modificarán automáticamente al cambiar el valor base o una excepción.
  - RN-178: Se conservará un historial de las modificaciones de importes y excepciones.

  <text color="secondary" size="sm">Queda pendiente definir cómo se expresan los descuentos, cuánto duran y desde cuándo comienzan a aplicarse.</text>
</box>

## Ejemplo — Obras Futsal, categoría C17

<box border radius="lg" padding={3} gap={2}>
  <text color="secondary" weight="medium" size="xs">CUOTA BASE MENSUAL</text>
  <title size="2xl" color="default" tabularNums>$25.000</title>
  <divider color="subtle"/>
  | Jugador | Condición | Cuota |
  |---|---|---:|
  | Juan Pérez | Normal | $25.000 |
  | Pedro Gómez | Beca parcial | $15.000 |
  | Lucas Díaz | Beca completa | $0 |
  | Martín López | Acuerdo especial | $20.000 |

  <caption>Ejemplo ilustrativo, no implica que ya estén definidos los tipos de descuentos.</caption>
</box>

---

## Fase 3 — Configuración de cuotas

<text color="secondary" weight="medium" size="xs">Pregunta 31 — Tipos de excepciones individuales</text>

**¿Cómo querés que los administradores establezcan descuentos o becas para un jugador?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Importe fijo personalizado**

      El administrador establece cuánto paga el jugador, independientemente del importe de su categoría.

      <text color="secondary" size="xs">Ejemplo: la categoría cuesta $25.000, pero Juan paga $15.000.</text>
    </radio>
    <radio value="b">
      **Opción B — Descuento porcentual**

      El administrador establece un porcentaje de descuento sobre el importe de la categoría.

      <text color="secondary" size="xs">Ejemplo: la categoría cuesta $25.000 y Juan tiene 40% de descuento, por lo que paga $15.000.</text>
    </radio>
    <radio value="c">
      **Opción C — Ambas modalidades**

      El administrador puede elegir entre establecer un importe fijo personalizado o aplicar un descuento porcentual.

      <text color="secondary" size="xs">También permite representar una beca completa con un importe de $0 o un descuento del 100%.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 31 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo importes fijos personalizados por jugador",b:"solo descuentos porcentuales sobre la cuota de categoría",c:"permitir tanto importes fijos personalizados como descuentos porcentuales"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**, porque permite que cada club gestione becas y descuentos con flexibilidad. Además, un descuento porcentual puede adaptarse automáticamente cuando aumenta la cuota de la categoría, mientras que un importe fijo puede mantenerse sin cambios.
```

## DEC-031

### Registro 115 — Tres modalidades de excepciones
Fuente: `4929608d-1475-4bab-a1ae-5eb118867afe`

Respuesta previa del usuario:

tambien me gustaria tener una seccion de becas, y tipo de becas que sean configurables para asignarselas directamente a los jugadores con las reglas ya definidas y asi poder usar los 3 modelos

Registro original:

```text
Perfecto. Esto amplía la pregunta 31: en lugar de limitar las excepciones a importes fijos o porcentajes, vamos a incorporar un **módulo de becas configurables**, para que los administradores puedan definir reglas una sola vez y reutilizarlas al asignarlas a los jugadores.

## Registro SDD — Decisión 031

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-031 — Tres modalidades de excepciones**
    <badge color="success">Aprobada</badge>
  </row>
  - **Importe fijo personalizado:** se establece directamente cuánto debe pagar un jugador.
  - **Descuento porcentual:** se aplica un porcentaje sobre la cuota base de su categoría.
  - **Beca preconfigurada:** se selecciona un tipo de beca previamente creado, que contiene las reglas para calcular el importe.

  El módulo de becas permitirá crear tipos reutilizables y asignarlos a jugadores sin tener que configurar manualmente cada descuento.
</box>

## Ejemplo de la sección de becas

<box border radius="xl" padding={3} gap={3}>
  <row align="center" justify="between">
    <box gap="1px">
      **Tipos de becas**
      <text color="secondary" size="xs">Ejemplo ilustrativo de configuración</text>
    </box>
    <badge>3 tipos</badge>
  </row>
  <table>
    <table-row><table-cell>**Beca completa**</table-cell><table-cell>100% descuento</table-cell></table-row>
    <table-row><table-cell>**Beca deportiva**</table-cell><table-cell>50% descuento</table-cell></table-row>
    <table-row><table-cell>**Beca especial**</table-cell><table-cell>Importe fijo: $10.000</table-cell></table-row>
  </table>
  <divider color="subtle"/>
  **Asignación a un jugador**
  <box background="surface-secondary" radius="lg" padding={3} gap={2}>
    <row justify="between" align="center">
      <text color="secondary" size="sm">Jugador</text>
      **Juan Pérez**
    </row>
    <row justify="between" align="center">
      <text color="secondary" size="sm">Categoría</text>
      **C17**
    </row>
    <row justify="between" align="center">
      <text color="secondary" size="sm">Cuota base</text>
      **$25.000**
    </row>
    <divider color="subtle"/>
    <row justify="between" align="center">
      <text color="secondary" size="sm">Beca asignada</text>
      **Beca deportiva (50%)**
    </row>
    <row justify="between" align="center">
      **Cuota resultante**
      <title size="xl" color="success" tabularNums>$12.500</title>
    </row>
  </box>
</box>

También vamos a contemplar que las becas tengan nombre, descripción, modalidad de cálculo, vigencia y registro histórico de asignaciones. Las reglas específicas las iremos definiendo.

---

## Pregunta 32 — ¿Dónde se configuran los tipos de becas?

Supongamos que Obras Mendoza tiene futsal y básquet. ¿Querés que las becas sean compartidas entre ambas disciplinas o que cada una tenga las suyas?

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Becas independientes por disciplina**

      Futsal configura sus tipos de becas y básquet los suyos. Los administradores de cada disciplina gestionan sus becas.
    </radio>
    <radio value="b">
      **Opción B — Becas generales por club**

      El delegado general configura los tipos de becas de Obras Mendoza y los administradores de cualquier disciplina pueden asignarlas a sus jugadores.
    </radio>
    <radio value="c">
      **Opción C — Becas generales y por disciplina**

      El delegado general configura becas compartidas por todo el club. Además, cada administrador puede crear becas exclusivas de su disciplina.

      <text color="secondary" size="xs">Por ejemplo, una beca familiar para todo Obras y una beca deportiva exclusiva de futsal.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 32 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"tipos de beca independientes por disciplina",b:"tipos de beca generales por club configurados por delegados generales",c:"tipos de beca generales del club y tipos específicos de cada disciplina"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**: permite que un club establezca políticas comunes de becas y que cada disciplina tenga sus propios beneficios, manteniendo los permisos que ya definimos.
```

## DEC-032

### Registro 117 — Becas independientes por disciplina
Fuente: `c0c8f77e-92f2-4cd4-8332-d5d86587e504`

Respuesta previa del usuario:

Para la pregunta 32 del relevamiento SDD elijo la opción A: tipos de beca independientes por disciplina.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 32 — Confirmada**
</row>

Cada disciplina tendrá su **propio módulo de becas**, administrado exclusivamente por sus administradores deportivos.

Por ejemplo, Obras Futsal podrá tener becas completamente diferentes de Obras Básquet, sin compartir configuraciones.

## Registro SDD — Decisión 032

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-032 — Becas independientes por disciplina**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-179: Cada tipo de beca pertenecerá a una disciplina específica de un club.
  - RN-180: Los administradores podrán crear, modificar y desactivar tipos de becas exclusivamente dentro de su disciplina.
  - RN-181: Los tipos de becas no se compartirán entre disciplinas.
  - RN-182: Los administradores podrán asignar becas únicamente a jugadores inscriptos en su disciplina.
  - RN-183: Los delegados generales podrán consultar las becas y sus asignaciones en todas las disciplinas de su club, sin modificarlas.
  - RN-184: El superadministrador conservará acceso global.
  - RN-185: Cada beca tendrá reglas de cálculo reutilizables, evitando configurar manualmente el descuento de cada jugador.
</box>

## Ejemplo — Obras Mendoza

<grid columns={2} gap={3}>
  <grid-item>
    <box border radius="lg" padding={3} gap={2}>
      <row align="center" gap={2}>
        <icon name="trophy" color="secondary"/>
        **Futsal**
      </row>
      <text size="sm">Beca deportiva — 50%</text>
      <text size="sm">Beca completa — 100%</text>
      <text size="sm">Beca especial — $10.000 de cuota</text>
    </box>
  </grid-item>
  <grid-item>
    <box border radius="lg" padding={3} gap={2}>
      <row align="center" gap={2}>
        <icon name="trophy" color="secondary"/>
        **Básquet**
      </row>
      <text size="sm">Beca rendimiento — 30%</text>
      <text size="sm">Beca social — 70%</text>
      <text size="sm">Beca completa — 100%</text>
    </box>
  </grid-item>
</grid>

---

## Fase 3 — Configuración de cuotas y becas

<text color="secondary" weight="medium" size="xs">Pregunta 33 — Vigencia de las becas</text>

Supongamos que el administrador de Obras Futsal le asigna una beca del 50% a un jugador.

**¿Durante cuánto tiempo debería mantenerse esa beca?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Indefinida**

      La beca permanece activa hasta que un administrador la quite manualmente.
    </radio>
    <radio value="b">
      **Opción B — Con fecha de vencimiento obligatoria**

      Cada beca debe tener una fecha de inicio y una fecha de finalización. Al vencer, el jugador vuelve automáticamente a pagar el importe normal de su categoría.
    </radio>
    <radio value="c">
      **Opción C — Vigencia configurable**

      Al asignar una beca, el administrador puede elegir entre una vigencia indefinida o establecer una fecha de finalización.

      <text color="secondary" size="xs">Ejemplo: una beca deportiva indefinida o una beca social que finaliza el 31 de diciembre.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 33 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"las becas tienen vigencia indefinida hasta su baja manual",b:"todas las becas requieren fecha de vencimiento",c:"la vigencia de cada beca asignada puede ser indefinida o tener fecha de finalización"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**, porque permite administrar becas permanentes y temporales sin tener que crear mecanismos distintos.
```

## DEC-033

### Registro 119 — Vigencia configurable de las becas
Fuente: `8f2fca56-57d1-4f29-9dad-5af33998e5e9`

Respuesta previa del usuario:

Para la pregunta 33 del relevamiento SDD elijo la opción C: la vigencia de cada beca asignada puede ser indefinida o tener fecha de finalización.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 33 — Confirmada**
</row>

Las becas tendrán **vigencia configurable**. Al asignar una beca a un jugador, el administrador podrá establecer una fecha de finalización o dejarla activa indefinidamente.

## Registro SDD — Decisión 033

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-033 — Vigencia configurable de las becas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-186: Cada asignación de beca tendrá una fecha de inicio.
  - RN-187: La fecha de finalización será opcional.
  - RN-188: Las becas sin fecha de finalización permanecerán vigentes hasta su desactivación manual.
  - RN-189: Las becas con fecha de finalización dejarán de aplicarse automáticamente cuando termine su vigencia.
  - RN-190: Al finalizar una beca, el jugador volverá a pagar el importe correspondiente a su categoría, salvo que tenga otra excepción vigente.
  - RN-191: Las cuotas ya generadas conservarán sus importes originales.
  - RN-192: El sistema conservará el historial de asignaciones, modificaciones y finalizaciones de becas.
</box>

## Ejemplo — Becas de Obras Futsal

| Jugador | Beca | Vigencia |
|---|---|---|
| Juan Pérez | Deportiva 50% | Indefinida |
| Pedro Gómez | Social 70% | Hasta 31/12/2026 |
| Lucas Díaz | Completa 100% | Hasta 30/06/2027 |

---

## Fase 3 — Configuración de cuotas y becas

<text color="secondary" weight="medium" size="xs">Pregunta 34 — Cuándo comienza a aplicarse una beca</text>

Supongamos que Juan paga $25.000 mensuales y el administrador le asigna una beca del 50% el **15 de junio**.

La cuota de junio ya fue generada, pero todavía no está pagada.

**¿Cuándo debería comenzar a aplicarse el descuento?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Desde el mes siguiente**

      Junio conserva su importe original de $25.000. A partir de julio, Juan paga $12.500.

      <text color="secondary" size="xs">Es consistente con la regla que definimos para los cambios de categoría.</text>
    </radio>
    <radio value="b">
      **Opción B — Aplicación inmediata**

      Si junio todavía no fue pagado, su cuota se actualiza a $12.500. Si ya fue pagado, no se modifica.
    </radio>
    <radio value="c">
      **Opción C — El administrador decide**

      Al asignar la beca, el administrador puede elegir si el descuento comienza en el mes actual o en el siguiente. Las cuotas pagadas nunca se modifican automáticamente.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 34 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"las becas comienzan a afectar las cuotas desde el mes siguiente a su asignación",b:"las becas se aplican inmediatamente a la cuota del mes actual si sigue impaga",c:"el administrador decide si la beca afecta la cuota actual impaga o comienza el mes siguiente"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A**, porque mantiene una regla uniforme para los cambios económicos y evita modificar cuotas que ya fueron emitidas.
```

## DEC-034

### Registro 121 — Aplicación de becas sobre cuotas
Fuente: `02e42b1c-be0a-4739-94ce-c31e366d4d61`

Respuesta previa del usuario:

Para la pregunta 34 del relevamiento SDD elijo la opción C: el administrador decide si la beca afecta la cuota actual impaga o comienza el mes siguiente.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 34 — Confirmada**
</row>

El administrador de cada disciplina podrá decidir **cuándo comienza a aplicarse una beca**: inmediatamente sobre la cuota impaga del mes actual o desde el mes siguiente.

## Registro SDD — Decisión 034

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-034 — Aplicación de becas sobre cuotas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-193: Al asignar una beca, el administrador seleccionará cuándo comienza su efecto económico.
  - RN-194: Podrá aplicarse a la cuota impaga del mes actual.
  - RN-195: Alternativamente, podrá comenzar a aplicarse desde el mes siguiente.
  - RN-196: Las cuotas ya pagadas no se modificarán automáticamente.
  - RN-197: Si se aplica al mes actual, el sistema recalculará el importe pendiente según las reglas de la beca.
  - RN-198: Se conservará el historial del importe original, el nuevo importe, la beca aplicada y el administrador responsable.
  - RN-199: La decisión afectará únicamente las cuotas del jugador dentro de esa disciplina.
</box>

## Ejemplo práctico

<box border radius="lg" padding={3} gap={2}>
  **Juan Pérez — Obras Futsal**

  <row align="center" justify="between">
    <text color="secondary" size="sm">Cuota de junio</text>
    **$25.000**
  </row>
  <row align="center" justify="between">
    <text color="secondary" size="sm">Beca asignada el 15/06</text>
    **50%**
  </row>
  <divider color="subtle"/>
  <grid columns={2} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text weight="medium" size="sm">Aplicar ahora</text>
        <title size="xl" color="success">$12.500</title>
        <caption>Junio impago se recalcula</caption>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text weight="medium" size="sm">Desde julio</text>
        <title size="xl">$25.000</title>
        <caption>Junio conserva su importe</caption>
      </box>
    </grid-item>
  </grid>
</box>

---

## Fase 3 — Configuración de cuotas y becas

<text color="secondary" weight="medium" size="xs">Pregunta 35 — ¿Se pueden acumular varias becas o descuentos?</text>

Supongamos que Juan tiene una beca deportiva del 50%, pero además el club quiere otorgarle un descuento adicional por una situación particular.

**¿Cómo debería comportarse el sistema cuando un jugador tiene más de un beneficio?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Un único beneficio activo por jugador y disciplina**

      El jugador puede tener una beca, un descuento porcentual o un importe personalizado, pero solo uno se aplica a su cuota en cada período.

      <text color="secondary" size="xs">Evita conflictos y cálculos ambiguos.</text>
    </radio>
    <radio value="b">
      **Opción B — Permitir acumular beneficios**

      El jugador puede tener varias becas y descuentos simultáneamente. Se aplican de forma sucesiva, sin que la cuota resulte negativa.

      <text color="secondary" size="xs">Ejemplo: cuota $25.000, beca 50% y descuento adicional 20% sobre el saldo = $10.000.</text>
    </radio>
    <radio value="c">
      **Opción C — Permitir varios beneficios, pero aplicar solo el más favorable**

      El jugador puede tener varias becas o descuentos asignados, pero el sistema calcula cuál produce la cuota más baja y aplica únicamente ese beneficio.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 35 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo un beneficio económico activo por jugador y disciplina en cada período",b:"permitir acumular múltiples beneficios económicos aplicándolos sucesivamente sin generar cuotas negativas",c:"permitir múltiples beneficios asignados pero aplicar solamente el más favorable para el jugador"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A** para mantener un cálculo de cuotas sencillo y auditable. Más adelante se podrán agregar reglas de acumulación si los clubes realmente las necesitan.
```

## DEC-035

### Registro 123 — Un único beneficio económico activo
Fuente: `ffddc99c-7807-43b6-a007-24f3bab4d368`

Respuesta previa del usuario:

Para la pregunta 35 del relevamiento SDD elijo la opción A: solo un beneficio económico activo por jugador y disciplina en cada período.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 35 — Confirmada**
</row>

Cada jugador podrá tener **un único beneficio económico aplicable por disciplina y período**. No se acumularán becas, descuentos porcentuales ni importes personalizados.

## Registro SDD — Decisión 035

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-035 — Un único beneficio económico activo**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-200: Cada jugador podrá tener como máximo un beneficio económico aplicable por disciplina y período.
  - RN-201: Los beneficios admitidos serán becas preconfiguradas, descuentos porcentuales e importes fijos personalizados.
  - RN-202: No se permitirá acumular dos o más beneficios sobre una misma cuota.
  - RN-203: El sistema validará si existe un beneficio vigente antes de asignar uno nuevo.
  - RN-204: Los cambios de beneficio conservarán el historial de asignaciones y sus fechas de vigencia.
  - RN-205: Un beneficio de futsal no afectará las cuotas del mismo jugador en básquet ni en otros clubes.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    **Juan Pérez — C17**
    <badge color="success">Beca activa</badge>
  </row>
  <row justify="between">
    <text color="secondary" size="sm">Cuota base</text>
    **$25.000**
  </row>
  <row justify="between">
    <text color="secondary" size="sm">Beneficio</text>
    **Beca deportiva 50%**
  </row>
  <divider color="subtle"/>
  <row justify="between" align="center">
    **Cuota resultante**
    <title size="xl" color="success">$12.500</title>
  </row>
  <caption>Si se desea asignar otro beneficio, primero deberá resolverse el existente. La forma de reemplazarlo se define a continuación.</caption>
</box>

---

## Fase 3 — Configuración de cuotas y becas

<text color="secondary" weight="medium" size="xs">Pregunta 36 — Reemplazo de una beca o descuento vigente</text>

Supongamos que Juan tiene una beca deportiva del 50%, pero el administrador quiere cambiarla por una beca completa del 100%.

**¿Qué debería hacer el sistema cuando se intenta asignar un nuevo beneficio a un jugador que ya tiene uno vigente?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Exigir la baja manual del beneficio anterior**

      El administrador debe finalizar primero la beca existente y después asignar la nueva, en dos operaciones separadas.
    </radio>
    <radio value="b">
      **Opción B — Reemplazo automático con confirmación**

      El sistema informa que ya existe un beneficio y permite reemplazarlo en una sola operación, solicitando confirmación y conservando el historial.

      <text color="secondary" size="xs">La nueva beca respeta la fecha de aplicación elegida y no modifica cuotas ya pagadas.</text>
    </radio>
    <radio value="c">
      **Opción C — Bloquear cualquier reemplazo hasta el vencimiento**

      El nuevo beneficio solo podrá asignarse cuando termine la vigencia del anterior.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 36 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"el administrador debe finalizar manualmente el beneficio anterior antes de asignar otro",b:"permitir reemplazar el beneficio vigente en una sola operación con confirmación e historial",c:"no permitir reemplazos hasta que finalice la vigencia del beneficio anterior"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción B**: mantiene la regla de un solo beneficio, reduce operaciones administrativas y permite auditar exactamente cuándo se reemplazó una beca por otra.
```

## DEC-036

### Registro 125 — Reemplazo de beneficios económicos
Fuente: `13db0e12-ede7-43b8-a98f-5ced26786d9b`

Respuesta previa del usuario:

Para la pregunta 36 del relevamiento SDD elijo la opción B: permitir reemplazar el beneficio vigente en una sola operación con confirmación e historial.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 36 — Confirmada**
</row>

El administrador podrá **reemplazar una beca o descuento vigente en una sola operación**, con confirmación previa y conservando el historial completo.

## Registro SDD — Decisión 036

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-036 — Reemplazo de beneficios económicos**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-206: El sistema detectará si el jugador ya tiene un beneficio vigente.
  - RN-207: Permitirá reemplazarlo por otro en una única operación.
  - RN-208: Antes del reemplazo, mostrará el beneficio anterior y el nuevo, solicitando confirmación.
  - RN-209: El beneficio anterior finalizará su aplicación cuando comience la del nuevo, evitando superposiciones.
  - RN-210: El reemplazo respetará la fecha de aplicación seleccionada por el administrador, según DEC-034.
  - RN-211: Las cuotas ya pagadas no serán modificadas automáticamente.
  - RN-212: Se registrarán el beneficio anterior, el nuevo, las fechas y el administrador responsable.
</box>

## Ejemplo — Reemplazo de beca

<box border radius="lg" padding={3} gap={2}>
  **Juan Pérez — Obras Futsal, C17**

  <row align="center" justify="between">
    <box gap={1}>
      <text color="secondary" size="xs">Beca anterior</text>
      **Deportiva 50%**
      <title size="lg" tabularNums>$12.500</title>
    </box>
    <icon name="arrow-right" color="secondary" size="lg"/>
    <box align="end" gap={1}>
      <text color="secondary" size="xs">Nueva beca</text>
      **Completa 100%**
      <title size="lg" color="success" tabularNums>$0</title>
    </box>
  </row>
  <caption>La cuota base es de $25.000. El cambio se aplica desde el período elegido y se conserva el registro de ambos beneficios.</caption>
</box>

---

## Fase 3 — Configuración de cuotas y becas

<text color="secondary" weight="medium" size="xs">Pregunta 37 — Modificación de un tipo de beca existente</text>

Supongamos que Obras Futsal creó una **Beca Deportiva del 50%** y se la asignó a 20 jugadores.

Más adelante, el administrador quiere cambiar ese tipo de beca para que otorgue un descuento del 60%.

**¿Qué debería pasar con los 20 jugadores que ya tienen esa beca asignada?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Actualización automática**

      Los 20 jugadores pasan a tener el 60% de descuento en sus próximas cuotas. Las cuotas ya generadas no se modifican.
    </radio>
    <radio value="b">
      **Opción B — Mantener las condiciones originales**

      Los 20 jugadores conservan su descuento del 50%. El nuevo 60% se aplica únicamente a las becas asignadas después de la modificación.
    </radio>
    <radio value="c">
      **Opción C — El administrador decide**

      Al modificar el tipo de beca, el sistema pregunta si el nuevo porcentaje debe aplicarse a todos los beneficiarios actuales o únicamente a las nuevas asignaciones.

      <text color="secondary" size="xs">En ambos casos se conserva el historial y no se modifican cuotas ya pagadas.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 37 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"los cambios del tipo de beca se aplican automáticamente a los beneficiarios actuales en futuras cuotas",b:"los beneficiarios actuales conservan las condiciones originales y el cambio solo afecta nuevas asignaciones",c:"al modificar un tipo de beca el administrador decide si actualiza a los beneficiarios actuales o solo las nuevas asignaciones"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**, porque permite actualizar una beca masivamente cuando cambia la política de la disciplina, pero también conservar acuerdos anteriores cuando corresponda.
```

## DEC-037

### Registro 127 — Actualización de tipos de becas
Fuente: `d6484fa5-d7be-4960-9b23-00518c74783d`

Respuesta previa del usuario:

Para la pregunta 37 del relevamiento SDD elijo la opción C: al modificar un tipo de beca el administrador decide si actualiza a los beneficiarios actuales o solo las nuevas asignaciones.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 37 — Confirmada**
</row>

Cuando se modifique un tipo de beca, el administrador podrá decidir si las nuevas condiciones se aplican a **todos los beneficiarios actuales** o únicamente a las nuevas asignaciones.

## Registro SDD — Decisión 037

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-037 — Actualización de tipos de becas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-213: Los administradores podrán modificar las condiciones de los tipos de becas de su disciplina.
  - RN-214: Al modificar un tipo de beca, el sistema permitirá elegir el alcance del cambio.
  - RN-215: Si se selecciona actualizar beneficiarios actuales, las nuevas condiciones se aplicarán a sus próximas cuotas.
  - RN-216: Si se selecciona solo nuevas asignaciones, los beneficiarios actuales conservarán las condiciones anteriores.
  - RN-217: El sistema conservará las versiones históricas de las reglas de cada tipo de beca.
  - RN-218: Las cuotas ya pagadas no podrán modificarse automáticamente.
  - RN-219: Cada modificación registrará fecha, administrador responsable, valores anteriores, valores nuevos y alcance seleccionado.
</box>

## Ejemplo — Beca Deportiva

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    <box gap={1}>
      <text color="secondary" size="xs">Descuento anterior</text>
      <title size="xl" tabularNums>50%</title>
    </box>
    <icon name="arrow-right" color="secondary" size="lg"/>
    <box align="end" gap={1}>
      <text color="secondary" size="xs">Nuevo descuento</text>
      <title size="xl" color="success" tabularNums>60%</title>
    </box>
  </row>
  <divider color="subtle"/>
  | Alcance elegido | Resultado |
  |---|---|
  | Todos los beneficiarios | Los 20 jugadores reciben el 60% en sus próximas cuotas |
  | Solo nuevas asignaciones | Los 20 jugadores conservan el 50%; las nuevas asignaciones reciben el 60% |
</box>

---

## Fase 3 — Configuración de cuotas y becas

<text color="secondary" weight="medium" size="xs">Pregunta 38 — Modificación de cuotas ya generadas e impagas</text>

Ahora necesitamos definir qué sucede con las cuotas que **ya fueron generadas, pero todavía no fueron pagadas**, cuando cambia el importe base de una categoría.

Supongamos que Obras Futsal aumenta la cuota de C17 de $20.000 a $25.000 el 15 de junio.

**¿Qué debería suceder con las cuotas impagas de junio?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Mantener las cuotas ya generadas**

      Las cuotas de junio conservan su importe original de $20.000, aunque estén impagas. El nuevo valor se aplica desde julio.
    </radio>
    <radio value="b">
      **Opción B — Actualizar automáticamente las cuotas impagas**

      Todas las cuotas de junio que todavía no fueron pagadas se recalculan a $25.000. Las cuotas pagadas permanecen intactas.
    </radio>
    <radio value="c">
      **Opción C — El administrador decide**

      Al modificar el importe de la categoría, el administrador elige si el aumento se aplica únicamente a las próximas cuotas o también a las cuotas impagas del mes actual.

      <text color="secondary" size="xs">El sistema informa cuántas cuotas y jugadores serán afectados antes de confirmar.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 38 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"mantener los importes de cuotas ya generadas y aplicar nuevos valores desde el mes siguiente",b:"actualizar automáticamente las cuotas impagas del mes actual al nuevo valor de categoría",c:"el administrador decide si actualiza cuotas impagas del mes actual o aplica el nuevo valor desde el mes siguiente"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A**, porque protege la previsibilidad de las cuotas emitidas y mantiene coherencia con lo definido para los cambios de categoría. Las becas seguirán teniendo su mecanismo específico de aplicación inmediata, según la decisión 034.
```

## DEC-038

### Registro 129 — Actualización de importes por categoría
Fuente: `5602b1f2-3851-4aa2-b962-225cc10bedf3`

Respuesta previa del usuario:

Para la pregunta 38 del relevamiento SDD elijo la opción A: mantener los importes de cuotas ya generadas y aplicar nuevos valores desde el mes siguiente.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 38 — Confirmada**
</row>

Cuando se modifique el importe mensual de una categoría, **las cuotas ya generadas conservarán su importe original**, incluso si todavía no fueron pagadas.

El nuevo valor comenzará a aplicarse desde el mes siguiente.

## Registro SDD — Decisión 038

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-038 — Actualización de importes por categoría**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-220: Los administradores podrán modificar el importe base de las categorías de su disciplina.
  - RN-221: Los cambios de importe se aplicarán desde el mes calendario siguiente.
  - RN-222: Las cuotas ya generadas conservarán su importe original, independientemente de su estado de pago.
  - RN-223: No se recalcularán automáticamente las cuotas pendientes cuando cambie el importe base.
  - RN-224: El sistema conservará un historial de los importes anteriores, nuevos, fechas de vigencia y administrador responsable.
  - RN-225: La modificación afectará únicamente a la categoría correspondiente, dentro de su disciplina y club.

  <text color="secondary" size="sm">Excepción ya aprobada: la asignación de becas puede recalcular una cuota impaga del mes actual cuando el administrador lo indique expresamente (DEC-034).</text>
</box>

## Ejemplo — Obras Futsal, C17

| Mes | Importe de cuota | Estado |
|---|---:|---|
| Mayo | $20.000 | Pagada |
| Junio | $20.000 | Pendiente |
| Julio | $25.000 | Nuevo importe |
| Agosto | $25.000 | Nuevo importe |

<caption>El aumento se configuró el 15 de junio. La deuda pendiente de junio continúa siendo de $20.000.</caption>

---

## Fase 3 — Generación de cuotas

<text color="secondary" weight="medium" size="xs">Pregunta 39 — Cómo se generan las cuotas mensuales</text>

Ahora necesitamos definir cómo se crearán las cuotas que los jugadores deberán pagar cada mes.

Por ejemplo, Obras Futsal tiene 150 jugadores activos distribuidos en cuatro categorías.

**¿Cómo querés que se generen las cuotas mensuales?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Generación automática**

      El sistema genera las cuotas todos los meses, en una fecha configurada por los administradores.

      <text color="secondary" size="xs">Por ejemplo, el día 1 de cada mes se generan automáticamente las cuotas de todos los jugadores activos.</text>
    </radio>
    <radio value="b">
      **Opción B — Generación manual**

      Los administradores deben ingresar al sistema y generar las cuotas de cada mes mediante una acción manual.
    </radio>
    <radio value="c">
      **Opción C — Automática con generación manual adicional**

      El sistema genera las cuotas automáticamente en una fecha configurada, pero los administradores también pueden generar cuotas manualmente cuando sea necesario.

      <text color="secondary" size="xs">Por ejemplo, para regularizar una cuota faltante. El sistema deberá impedir que se duplique una cuota del mismo período.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 39 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"generación automática mensual de cuotas en fecha configurable",b:"generación exclusivamente manual de cuotas por administradores",c:"generación automática mensual con posibilidad de generación manual adicional y prevención de duplicados"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**. Automatiza la cobranza mensual, pero permite resolver casos excepcionales sin depender de un proceso técnico.
```

## DEC-039

### Registro 131 — Generación automática mensual de cuotas
Fuente: `bfb61e4e-ad1d-4b15-b356-d0f1d653dc1f`

Respuesta previa del usuario:

Para la pregunta 39 del relevamiento SDD elijo la opción A: generación automática mensual de cuotas en fecha configurable.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 39 — Confirmada**
</row>

Las cuotas se generarán **automáticamente todos los meses**, en una fecha configurable por los administradores de cada disciplina.

No incluiremos la generación manual de cuotas mensuales por parte de los administradores.

## Registro SDD — Decisión 039

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-039 — Generación automática mensual de cuotas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-226: El sistema generará automáticamente las cuotas mensuales.
  - RN-227: Cada disciplina podrá configurar el día del mes en que se generan sus cuotas.
  - RN-228: La generación utilizará los importes vigentes de las categorías y los beneficios individuales aplicables.
  - RN-229: Se generarán cuotas únicamente para inscripciones habilitadas, respetando el estado del club y de la disciplina.
  - RN-230: El sistema impedirá generar cuotas duplicadas para una misma inscripción y período mensual.
  - RN-231: Los administradores podrán consultar el resultado de cada ejecución automática.
  - RN-232: Se registrarán las ejecuciones, errores y cuotas generadas para facilitar la auditoría.
  - RN-233: Los administradores no dispondrán de una acción para generar manualmente las cuotas mensuales.
</box>

## Ejemplo — Obras Mendoza

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Futsal**
    <badge>Configuración ilustrativa</badge>
  </row>
  <grid columns={3} gap={2}>
    <grid-item>
      <box background="surface-secondary" padding={2} radius="md" align="center" gap={1}>
        <text color="secondary" size="xs">Generación</text>
        <title size="xl" tabularNums>Día 1</title>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" padding={2} radius="md" align="center" gap={1}>
        <text color="secondary" size="xs">Jugadores</text>
        <title size="xl" tabularNums>150</title>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" padding={2} radius="md" align="center" gap={1}>
        <text color="secondary" size="xs">Cuotas</text>
        <title size="xl" tabularNums>150</title>
      </box>
    </grid-item>
  </grid>
  <text color="secondary" size="sm">El sistema procesa las inscripciones habilitadas, calcula el importe correspondiente y crea las cuotas del período sin intervención del administrador.</text>
</box>

---

## Fase 3 — Generación de cuotas

<text color="secondary" weight="medium" size="xs">Pregunta 40 — Fecha de generación de cuotas</text>

Supongamos que Obras Futsal quiere generar sus cuotas el día 1 de cada mes, pero Obras Básquet prefiere hacerlo el día 10.

**¿Cómo debería configurarse el día de generación?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Día único para todo el club**

      El delegado general establece un día de generación común para todas las disciplinas del club.
    </radio>
    <radio value="b">
      **Opción B — Día configurable por disciplina**

      Cada administrador configura el día de generación de su disciplina, independientemente de las demás.

      <text color="secondary" size="xs">Ejemplo: futsal el día 1, básquet el día 10 y hockey el día 5.</text>
    </radio>
    <radio value="c">
      **Opción C — Día configurable por categoría**

      Cada categoría puede tener su propio día de generación, incluso dentro de una misma disciplina.

      <text color="secondary" size="xs">Ejemplo: C15 el día 1, C17 el día 5 y C20 el día 10.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 40 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"día único de generación mensual para todas las disciplinas del club",b:"día de generación mensual configurable de forma independiente por disciplina",c:"día de generación mensual configurable de forma independiente por categoría"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción B**, porque mantiene la autonomía de cada disciplina sin agregar complejidad innecesaria por categoría.
```

## DEC-040

### Registro 133 — Calendario de generación por disciplina
Fuente: `6c426153-b73e-4e27-8724-4a1ea653b6bd`

Respuesta previa del usuario:

Para la pregunta 40 del relevamiento SDD elijo la opción B: día de generación mensual configurable de forma independiente por disciplina.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 40 — Confirmada**
</row>

Cada disciplina tendrá su **propio día de generación automática de cuotas**, configurable por sus administradores, sin depender de las demás disciplinas del club.

## Registro SDD — Decisión 040

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-040 — Calendario de generación por disciplina**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-234: Cada disciplina tendrá su propia configuración de generación mensual de cuotas.
  - RN-235: Los administradores podrán establecer y modificar el día de generación de su disciplina.
  - RN-236: Todas las categorías de una misma disciplina compartirán ese día de generación.
  - RN-237: El calendario de una disciplina será independiente del de las demás.
  - RN-238: Los delegados generales podrán consultar la configuración de todas las disciplinas de su club, sin modificarla.
  - RN-239: El sistema registrará los cambios de configuración y el administrador responsable.
  - RN-240: La generación respetará los estados de actividad del club, la disciplina y las inscripciones.
</box>

## Ejemplo — Obras Mendoza

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    <box gap={1}>
      **Futsal**
      <text color="secondary" size="xs">C15, C17, C20 y Primera</text>
    </box>
    <box align="end" gap={1}>
      <text color="secondary" size="xs">Generación mensual</text>
      <title size="lg">Día 1</title>
    </box>
  </row>
  <divider color="subtle"/>
  <row align="center" justify="between">
    <box gap={1}>
      **Básquet**
      <text color="secondary" size="xs">U13, U15, U17 y Mayores</text>
    </box>
    <box align="end" gap={1}>
      <text color="secondary" size="xs">Generación mensual</text>
      <title size="lg">Día 10</title>
    </box>
  </row>
  <divider color="subtle"/>
  <row align="center" justify="between">
    <box gap={1}>
      **Hockey**
      <text color="secondary" size="xs">Todas sus categorías</text>
    </box>
    <box align="end" gap={1}>
      <text color="secondary" size="xs">Generación mensual</text>
      <title size="lg">Día 5</title>
    </box>
  </row>
  <caption>Fechas ilustrativas; cada disciplina elegirá su configuración real.</caption>
</box>

---

## Fase 3 — Generación de cuotas

<text color="secondary" weight="medium" size="xs">Pregunta 41 — Fecha de vencimiento de las cuotas</text>

Ya definimos cuándo se generan las cuotas. Ahora necesitamos establecer **hasta qué día pueden pagarse sin considerarse vencidas**.

Por ejemplo, Obras Futsal genera sus cuotas el día 1 de cada mes.

**¿Cómo querés configurar el vencimiento?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Vencimiento fijo para todas las disciplinas del club**

      El delegado general establece un único día de vencimiento para todo el club.

      <text color="secondary" size="xs">Ejemplo: todas las cuotas de Obras vencen el día 15.</text>
    </radio>
    <radio value="b">
      **Opción B — Vencimiento configurable por disciplina**

      Cada administrador configura el día de vencimiento de su disciplina, independientemente de las demás.

      <text color="secondary" size="xs">Ejemplo: futsal genera el día 1 y vence el 10; básquet genera el 5 y vence el 15.</text>
    </radio>
    <radio value="c">
      **Opción C — Vencimiento configurable por categoría**

      Cada categoría tiene su propio día de vencimiento, incluso dentro de una misma disciplina.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 41 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"día de vencimiento único para todas las disciplinas del club",b:"día de vencimiento configurable de forma independiente por disciplina",c:"día de vencimiento configurable de forma independiente por categoría"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción B**, para que cada disciplina controle su calendario completo de cobros sin agregar configuraciones individuales por categoría.
```

## DEC-041

### Registro 135 — Vencimiento de cuotas por disciplina
Fuente: `5d8d34f4-e8eb-4737-8b67-8541c8779bb8`

Respuesta previa del usuario:

Para la pregunta 41 del relevamiento SDD elijo la opción B: día de vencimiento configurable de forma independiente por disciplina.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 41 — Confirmada**
</row>

Cada disciplina tendrá su **propio día de vencimiento mensual**, configurable por sus administradores e independiente de las demás disciplinas del club.

## Registro SDD — Decisión 041

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-041 — Vencimiento de cuotas por disciplina**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-241: Cada disciplina tendrá un día de vencimiento mensual configurable.
  - RN-242: Los administradores podrán modificar el vencimiento de su propia disciplina.
  - RN-243: Todas las categorías de una misma disciplina compartirán la configuración de vencimiento.
  - RN-244: Las disciplinas de un mismo club podrán tener distintos días de vencimiento.
  - RN-245: Cada cuota generada conservará su fecha de vencimiento, aunque posteriormente se modifique la configuración de la disciplina.
  - RN-246: Los delegados generales podrán consultar los calendarios de vencimiento, sin modificarlos.
  - RN-247: Se registrará el historial de modificaciones de esta configuración.

</box>

## Ejemplo — Obras Mendoza

| Disciplina | Generación | Vencimiento |
|---|---|---|
| Futsal | Día 1 | Día 10 |
| Básquet | Día 10 | Día 20 |
| Hockey | Día 5 | Día 15 |

<caption>Ejemplo ilustrativo. Cada disciplina tendrá su calendario independiente.</caption>

---

## Fase 3 — Gestión de cuotas vencidas

<text color="secondary" weight="medium" size="xs">Pregunta 42 — Recargos por pago fuera de término</text>

Supongamos que un jugador tiene una cuota de $25.000 que venció el 10 de junio, pero quiere pagarla el 20 de junio.

**¿Qué debería suceder con el importe de esa cuota vencida?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Sin recargos**

      El jugador puede pagar los mismos $25.000 después del vencimiento. La cuota se marca como vencida, pero no aumenta su importe.
    </radio>
    <radio value="b">
      **Opción B — Recargo automático obligatorio**

      Todas las disciplinas aplican un recargo por mora a las cuotas vencidas, según una regla definida por el sistema.
    </radio>
    <radio value="c">
      **Opción C — Recargos configurables por disciplina**

      Cada disciplina decide si aplica recargos por mora o no. Si los activa, puede configurar las condiciones y el importe del recargo.

      <text color="secondary" size="xs">Por ejemplo, futsal cobra 10% adicional después del vencimiento y básquet no aplica recargos.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 42 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"las cuotas vencidas se pueden pagar sin recargos",b:"todas las disciplinas aplican un recargo automático obligatorio por mora",c:"cada disciplina configura si aplica recargos por mora y sus condiciones"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**, porque permite que cada disciplina tenga su propia política de cobranza. Después podremos definir si los recargos son porcentuales, fijos o progresivos.
```

## DEC-042

### Registro 137 — Recargos por mora configurables
Fuente: `cd7bca03-4c23-4f0b-b341-5eab94391d96`

Respuesta previa del usuario:

Para la pregunta 42 del relevamiento SDD elijo la opción C: cada disciplina configura si aplica recargos por mora y sus condiciones.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 42 — Confirmada**
</row>

Cada disciplina podrá decidir **si aplica recargos por mora** y configurar sus propias condiciones. No habrá una política de recargos obligatoria para todos los clubes.

## Registro SDD — Decisión 042

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-042 — Recargos por mora configurables**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-248: Cada disciplina podrá activar o desactivar los recargos por mora.
  - RN-249: Los administradores configurarán los recargos exclusivamente para su disciplina.
  - RN-250: Si los recargos están desactivados, las cuotas vencidas podrán pagarse sin penalización económica.
  - RN-251: Si están activados, el sistema calculará el recargo según las condiciones configuradas.
  - RN-252: Los recargos de una disciplina no afectarán las cuotas de otras disciplinas.
  - RN-253: Los delegados generales podrán consultar la configuración de recargos, sin modificarla.
  - RN-254: Se conservará el historial de cambios de configuración y el administrador responsable.
</box>

## Ejemplo — Obras Mendoza

| Disciplina | Recargos | Ejemplo |
|---|---|---|
| Futsal | Activados | 10% después del vencimiento |
| Básquet | Desactivados | Sin recargo |
| Hockey | Activados | $2.000 después del vencimiento |

<caption>Los valores son ilustrativos. Todavía debemos definir las modalidades de cálculo admitidas.</caption>

---

## Fase 3 — Gestión de cuotas vencidas

<text color="secondary" weight="medium" size="xs">Pregunta 43 — Tipos de recargos por mora</text>

Supongamos que Obras Futsal activa los recargos para las cuotas que se pagan después del vencimiento.

**¿Qué modalidades de recargo querés permitir?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Recargo fijo**

      El administrador configura un monto que se suma a la cuota vencida, independientemente de su importe.

      <text color="secondary" size="xs">Ejemplo: cuota de $25.000 + $2.000 de recargo = $27.000.</text>
    </radio>
    <radio value="b">
      **Opción B — Recargo porcentual**

      El administrador configura un porcentaje que se aplica sobre el importe de la cuota vencida.

      <text color="secondary" size="xs">Ejemplo: cuota de $25.000 + 10% = $27.500.</text>
    </radio>
    <radio value="c">
      **Opción C — Fijo, porcentual o progresivo**

      Cada disciplina puede elegir entre un importe fijo, un porcentaje único o un recargo que aumente según los días de atraso.

      <text color="secondary" size="xs">Ejemplo progresivo: 5% durante los primeros 10 días de atraso y 10% a partir del día 11.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 43 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir solamente recargos por mora de importe fijo",b:"permitir solamente recargos por mora porcentuales",c:"permitir recargos fijos, porcentuales y progresivos según días de atraso"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**, porque permite adaptarse a distintas políticas de cobranza sin tener que modificar el sistema cuando se incorpore un nuevo club.
```

## DEC-043

### Registro 139 — Modalidades de recargos por mora
Fuente: `06bf1ebc-14b1-407d-91e7-9cca12cc0db8`

Respuesta previa del usuario:

Para la pregunta 43 del relevamiento SDD elijo la opción C: permitir recargos fijos, porcentuales y progresivos según días de atraso.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 43 — Confirmada**
</row>

Cada disciplina podrá configurar **tres modalidades de recargo por mora**: importe fijo, porcentaje único o recargo progresivo según los días de atraso.

## Registro SDD — Decisión 043

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-043 — Modalidades de recargos por mora**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-255: Cada disciplina podrá seleccionar una modalidad de recargo.
  - RN-256: El recargo fijo sumará un importe determinado a la cuota vencida.
  - RN-257: El recargo porcentual aplicará un porcentaje sobre el importe de la cuota.
  - RN-258: El recargo progresivo permitirá configurar distintos tramos según los días transcurridos desde el vencimiento.
  - RN-259: Los administradores podrán modificar la configuración de recargos de su disciplina.
  - RN-260: El sistema calculará el recargo correspondiente al momento de consultar el importe a pagar.
  - RN-261: Los recargos deberán mostrarse separados del importe original de la cuota.
  - RN-262: Se conservará el historial de configuraciones y los recargos efectivamente cobrados.
</box>

## Ejemplo — Cuota vencida de $25.000

<box border radius="lg" padding={3} gap={3}>
  <grid columns={3} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} gap={1}>
        <text color="secondary" size="xs">Fijo</text>
        <title size="lg" tabularNums>$27.000</title>
        <caption>+$2.000</caption>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} gap={1}>
        <text color="secondary" size="xs">Porcentual</text>
        <title size="lg" tabularNums>$27.500</title>
        <caption>+10%</caption>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} gap={1}>
        <text color="secondary" size="xs">Progresivo</text>
        <title size="lg" tabularNums>$28.750</title>
        <caption>+15% según tramo</caption>
      </box>
    </grid-item>
  </grid>
  <text color="secondary" size="sm">Los importes son ilustrativos. Cada disciplina definirá los valores de sus recargos.</text>
</box>

---

## Fase 3 — Gestión de cuotas vencidas

<text color="secondary" weight="medium" size="xs">Pregunta 44 — Cuándo se comienza a cobrar el recargo</text>

Supongamos que una cuota de Obras Futsal vence el **10 de junio** y la disciplina tiene configurado un recargo del 10%.

**¿Desde qué momento debería comenzar a aplicarse ese recargo?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Inmediatamente después del vencimiento**

      Si la cuota vence el 10 de junio, el recargo comienza a aplicarse el 11 de junio.
    </radio>
    <radio value="b">
      **Opción B — Con días de gracia obligatorios**

      Todas las disciplinas deben otorgar un período de gracia fijo, por ejemplo cinco días, antes de aplicar cualquier recargo.
    </radio>
    <radio value="c">
      **Opción C — Período de gracia configurable por disciplina**

      Cada disciplina puede establecer cuántos días después del vencimiento se comienza a cobrar el recargo, incluyendo cero días de gracia.

      <text color="secondary" size="xs">Ejemplo: futsal otorga 5 días de gracia; básquet aplica el recargo desde el día siguiente al vencimiento.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 44 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"recargos desde el día siguiente al vencimiento, sin período de gracia",b:"período de gracia fijo obligatorio para todas las disciplinas",c:"cada disciplina configura sus días de gracia antes de aplicar recargos, pudiendo ser cero"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**, porque complementa las modalidades de recargo que acabamos de definir y permite que cada disciplina adapte sus condiciones de cobranza.
```

## DEC-044

### Registro 141 — Período de gracia configurable
Fuente: `9b72c133-d446-46bf-acd7-a94c8e8fd27f`

Respuesta previa del usuario:

Para la pregunta 44 del relevamiento SDD elijo la opción C: cada disciplina configura sus días de gracia antes de aplicar recargos, pudiendo ser cero.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 44 — Confirmada**
</row>

Cada disciplina podrá configurar **cuántos días de gracia otorga después del vencimiento** antes de aplicar recargos por mora. También podrá establecer cero días de gracia.

## Registro SDD — Decisión 044

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-044 — Período de gracia configurable**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-263: Cada disciplina podrá configurar su propio período de gracia.
  - RN-264: El período de gracia se expresará en días corridos.
  - RN-265: Se permitirá configurar cero días de gracia.
  - RN-266: Durante el período de gracia, la cuota estará vencida, pero no tendrá recargo.
  - RN-267: El recargo comenzará a aplicarse el día siguiente al último día de gracia.
  - RN-268: La configuración será administrada por los administradores de la disciplina.
  - RN-269: El período de gracia se aplicará a las modalidades de recargo fijo, porcentual y progresivo.
  - RN-270: Se conservará el historial de modificaciones de esta configuración.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    <box gap={1}>
      <text color="secondary" size="xs">Vencimiento</text>
      **10 de junio**
    </box>
    <box align="end" gap={1}>
      <text color="secondary" size="xs">Días de gracia</text>
      **5 días**
    </box>
  </row>
  <grid columns={3} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} gap={1} align="center">
        <text color="secondary" size="xs">Hasta 10/06</text>
        <icon name="check-circle" color="success"/>
        <text size="xs" weight="medium">En término</text>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} gap={1} align="center">
        <text color="secondary" size="xs">11–15/06</text>
        <icon name="clock" color="secondary"/>
        <text size="xs" weight="medium">Sin recargo</text>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} gap={1} align="center">
        <text color="secondary" size="xs">Desde 16/06</text>
        <icon name="alert-circle" color="danger"/>
        <text size="xs" weight="medium">Con recargo</text>
      </box>
    </grid-item>
  </grid>
  <caption>Ejemplo con cinco días corridos de gracia.</caption>
</box>

---

## Fase 3 — Gestión de cuotas vencidas

<text color="secondary" weight="medium" size="xs">Pregunta 45 — Cambios en las reglas de recargos</text>

Supongamos que Obras Futsal tiene configurado un recargo del 10%, pero el administrador decide aumentarlo al 15% el 20 de junio.

Hay jugadores con cuotas de mayo y junio que siguen impagas.

**¿Cómo debería afectar este cambio a las cuotas vencidas existentes?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Conservar las reglas originales**

      Las cuotas ya generadas mantienen la política de recargos que tenían cuando fueron emitidas. El 15% se aplicará únicamente a las cuotas que se generen después del cambio.
    </radio>
    <radio value="b">
      **Opción B — Actualizar todas las cuotas impagas**

      Las cuotas que todavía no fueron pagadas utilizarán automáticamente la nueva política del 15%, aunque correspondan a meses anteriores.
    </radio>
    <radio value="c">
      **Opción C — El administrador decide**

      Al modificar la política de recargos, el administrador elige si afecta solamente a las nuevas cuotas o también a todas las cuotas vencidas e impagas.

      <text color="secondary" size="xs">El sistema informa cuántas cuotas serán afectadas y conserva el historial de cambios.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 45 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"las cuotas conservan la política de recargos vigente cuando fueron generadas",b:"todas las cuotas impagas adoptan automáticamente la nueva política de recargos",c:"el administrador decide si la nueva política afecta cuotas impagas existentes o solamente nuevas cuotas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A**, porque mantiene condiciones previsibles para los jugadores y permite auditar los importes de cada período sin que las reglas cambien retroactivamente.
```

## DEC-045

### Registro 143 — Política de recargos histórica por cuota
Fuente: `0969bf94-fd57-4987-a481-759ffc5e9288`

Respuesta previa del usuario:

Para la pregunta 45 del relevamiento SDD elijo la opción A: las cuotas conservan la política de recargos vigente cuando fueron generadas.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 45 — Confirmada**
</row>

Las cuotas conservarán **la política de recargos vigente al momento de su generación**, aunque posteriormente el administrador modifique las condiciones de la disciplina.

## Registro SDD — Decisión 045

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-045 — Política de recargos histórica por cuota**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-271: Cada cuota conservará la política de recargos vigente cuando fue generada.
  - RN-272: Los cambios posteriores en la configuración de recargos no afectarán cuotas previamente generadas.
  - RN-273: Las cuotas impagas continuarán calculando su mora según sus reglas originales.
  - RN-274: La política histórica incluirá modalidad de recargo, importes o porcentajes, tramos progresivos y días de gracia.
  - RN-275: Las nuevas cuotas utilizarán la configuración vigente al momento de su generación.
  - RN-276: El sistema conservará versiones históricas de las políticas de recargos.
  - RN-277: Los recargos efectivamente cobrados quedarán registrados para auditoría.

  **Implicación técnica:** cada cuota deberá referenciar una versión inmutable de la política de recargos o conservar una copia de sus condiciones.
</box>

## Ejemplo — Obras Futsal

El administrador aumenta el recargo del 10% al 15% el 20 de junio.

| Cuota | Política conservada | Importe base |
|---|---|---:|
| Mayo | 10% | $25.000 |
| Junio | 10% | $25.000 |
| Julio | 15% | $25.000 |

<caption>Ejemplo ilustrativo. Las cuotas de mayo y junio mantienen el 10% aunque se paguen después del cambio.</caption>

---

## Fase 3 — Gestión de cuotas vencidas

<text color="secondary" weight="medium" size="xs">Pregunta 46 — Pago de cuotas de meses anteriores</text>

Supongamos que un jugador de Obras Futsal tiene tres cuotas pendientes:

<box border radius="lg" padding={3} gap={2}>
  | Período | Importe con mora | Estado |
  |---|---:|---|
  | Abril | $27.500 | Vencida |
  | Mayo | $27.500 | Vencida |
  | Junio | $25.000 | Pendiente |
  <divider color="subtle"/>
  <row align="center" justify="between">
    **Deuda total**
    <title size="xl" tabularNums>$80.000</title>
  </row>
</box>

**¿Cómo querés que el jugador pueda pagar esas cuotas?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Pago obligatorio de toda la deuda**

      El jugador debe pagar todas sus cuotas pendientes juntas. No puede seleccionar meses individuales.
    </radio>
    <radio value="b">
      **Opción B — Pago en orden cronológico**

      Puede pagar una o varias cuotas, pero siempre comenzando por la más antigua.

      <text color="secondary" size="xs">Por ejemplo, debe pagar abril antes de poder pagar mayo o junio.</text>
    </radio>
    <radio value="c">
      **Opción C — Selección libre de cuotas**

      El jugador puede elegir qué meses pagar, independientemente de si tiene cuotas anteriores vencidas.

      <text color="secondary" size="xs">Por ejemplo, puede pagar únicamente junio, abril y mayo, o las tres juntas.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 46 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"el jugador debe pagar todas sus cuotas pendientes juntas",b:"el jugador puede pagar cuotas individuales pero obligatoriamente desde la más antigua",c:"el jugador puede seleccionar libremente qué cuotas pendientes pagar, individualmente o agrupadas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**, porque facilita la cobranza y permite que los jugadores regularicen sus cuotas según sus posibilidades, sin bloquear el pago de meses recientes por deudas anteriores.
```

## DEC-046

### Registro 145 — Selección libre de cuotas para pagar
Fuente: `520fcf43-416d-4cfc-b563-83b7ab83f828`

Respuesta previa del usuario:

Para la pregunta 46 del relevamiento SDD elijo la opción C: el jugador puede seleccionar libremente qué cuotas pendientes pagar, individualmente o agrupadas.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 46 — Confirmada**
</row>

Los jugadores podrán **seleccionar libremente qué cuotas desean pagar**, individualmente o agrupadas, sin necesidad de cancelar primero las deudas más antiguas.

## Registro SDD — Decisión 046

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-046 — Selección libre de cuotas para pagar**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-278: El jugador podrá seleccionar una o varias cuotas pendientes.
  - RN-279: No será obligatorio pagar las cuotas en orden cronológico.
  - RN-280: No será obligatorio cancelar toda la deuda para realizar un pago.
  - RN-281: Se podrán agrupar varias cuotas en una única operación de pago.
  - RN-282: Cada cuota conservará su estado individual.
  - RN-283: El importe a pagar incluirá los recargos correspondientes a las cuotas seleccionadas.
  - RN-284: El sistema mostrará el detalle de las cuotas seleccionadas antes de iniciar el pago.
  - RN-285: El pago de una cuota no modificará el estado de otras cuotas no incluidas en la operación.
</box>

## Ejemplo — Portal del jugador

{@body const cuotas = [{id:"abril",mes:"Abril",monto:27500},{id:"mayo",mes:"Mayo",monto:27500},{id:"junio",mes:"Junio",monto:25000}]}
{@body const [seleccion,setSeleccion] = DIL.useState(["abril","junio"])}
{@body const total = cuotas.filter(c=>seleccion.includes(c.id)).reduce((s,c)=>s+c.monto,0)}
<box border radius="xl" padding={3} gap={3}>
  <row align="center" justify="between">
    **Cuotas pendientes**
    <text color="secondary" size="xs">Ejemplo interactivo</text>
  </row>
  {#each cuotas as cuota}
    <row key={cuota.id} align="center" justify="between">
      <checkbox checked={seleccion.includes(cuota.id)} onChange={v=>setSeleccion(prev=>v?[...prev,cuota.id]:prev.filter(id=>id!==cuota.id))} lineThrough={false}>
        {cuota.mes}
      </checkbox>
      <text weight="medium" tabularNums>{cuota.monto.toLocaleString("es-AR",{style:"currency",currency:"ARS",maximumFractionDigits:0})}</text>
    </row>
  {/each}
  <divider color="subtle"/>
  <row align="center" justify="between">
    <box gap={1}>
      <text color="secondary" size="xs">Total seleccionado</text>
      <title size="xl" tabularNums>{total.toLocaleString("es-AR",{style:"currency",currency:"ARS",maximumFractionDigits:0})}</title>
    </box>
    <badge>{seleccion.length} {seleccion.length===1?"cuota":"cuotas"}</badge>
  </row>
  <caption>Simulación de selección; no inicia ningún cobro real.</caption>
</box>

---

## Fase 3 — Gestión de pagos

<text color="secondary" weight="medium" size="xs">Pregunta 47 — Pagos parciales de una cuota</text>

Ya definimos que el jugador puede seleccionar libremente qué meses pagar. Ahora necesitamos decidir si también puede pagar **solo una parte del importe de una cuota**.

Por ejemplo, Juan tiene una cuota pendiente de $25.000, pero actualmente dispone de $10.000.

**¿Cómo debería funcionar el sistema?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo pagos completos por cuota**

      Juan debe pagar los $25.000 para cancelar esa cuota. Puede seleccionar distintos meses, pero cada cuota seleccionada debe pagarse completamente.
    </radio>
    <radio value="b">
      **Opción B — Permitir pagos parciales**

      Juan puede pagar $10.000 ahora y los $15.000 restantes más adelante. La cuota permanece parcialmente pagada hasta completar el importe.
    </radio>
    <radio value="c">
      **Opción C — Pagos parciales configurables por disciplina**

      Cada disciplina decide si permite pagos parciales. Las que los habiliten podrán recibir varios pagos para cancelar una misma cuota.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 47 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo pagos completos de cada cuota seleccionada",b:"permitir pagos parciales de cualquier cuota",c:"cada disciplina configura si admite pagos parciales de cuotas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A** para el MVP. Simplifica la conciliación con la pasarela de pagos, los recargos por mora y el cálculo de comisiones. Más adelante podríamos incorporar pagos parciales si los clubes los necesitan.
```

## DEC-047

### Registro 147 — Pagos completos por cuota
Fuente: `b1c24332-9256-41d9-af9a-a292751d9207`

Respuesta previa del usuario:

Para la pregunta 47 del relevamiento SDD elijo la opción A: solo pagos completos de cada cuota seleccionada.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 47 — Confirmada**
</row>

Los jugadores podrán pagar una o varias cuotas en una misma operación, pero **cada cuota seleccionada deberá abonarse completamente**. No se admitirán pagos parciales.

## Registro SDD — Decisión 047

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-047 — Pagos completos por cuota**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-286: Cada cuota deberá pagarse por su importe total pendiente.
  - RN-287: No se permitirán pagos parciales de una cuota.
  - RN-288: El jugador podrá agrupar varias cuotas completas en una única operación.
  - RN-289: El importe de cada cuota incluirá los recargos por mora que correspondan.
  - RN-290: Una cuota solo se considerará pagada cuando se confirme el cobro completo.
  - RN-291: Un pago rechazado o fallido no cancelará la deuda.
  - RN-292: Cada cuota conservará su estado individual, aunque forme parte de un pago agrupado.
</box>

## Ejemplo — Portal de pagos

<box border radius="lg" padding={3} gap={2}>
  | Cuota | Importe | Acción |
  |---|---:|---|
  | Abril | $27.500 | Pagar completa |
  | Mayo | $27.500 | Pagar completa |
  | Junio | $25.000 | Pagar completa |
  <divider color="subtle"/>
  **Operaciones permitidas:**

  <row align="center" gap={2}>
    <icon name="check-circle" color="success"/>
    <text size="sm">Pagar solamente abril: $27.500</text>
  </row>
  <row align="center" gap={2}>
    <icon name="check-circle" color="success"/>
    <text size="sm">Pagar abril y junio: $52.500</text>
  </row>
  <row align="center" gap={2}>
    <icon name="x-circle" color="danger"/>
    <text size="sm">Pagar solamente $10.000 de abril</text>
  </row>
  <caption>Importes ilustrativos, sin incluir los cargos de procesamiento de la pasarela de pagos.</caption>
</box>

---

## Fase 3 — Gestión de pagos

<text color="secondary" weight="medium" size="xs">Pregunta 48 — Registro de pagos realizados fuera de la plataforma</text>

Supongamos que un jugador de Obras Futsal paga su cuota directamente en la secretaría del club, mediante efectivo o transferencia bancaria.

El sistema necesita saber si ese pago debe registrarse para que la cuota no siga apareciendo como pendiente.

**¿Cómo querés manejar los pagos externos?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo pagos realizados por la plataforma**

      Las cuotas únicamente podrán cancelarse mediante la pasarela de pagos integrada. No se registrarán pagos externos.
    </radio>
    <radio value="b">
      **Opción B — Permitir registrar pagos externos manualmente**

      El administrador podrá marcar una cuota como pagada por efectivo, transferencia u otro medio externo.

      <text color="secondary" size="xs">El pago quedará registrado con su medio, fecha y administrador responsable.</text>
    </radio>
    <radio value="c">
      **Opción C — Registrar pagos externos con comprobante opcional**

      El administrador podrá registrar pagos externos y, además, adjuntar un comprobante cuando corresponda.

      <text color="secondary" size="xs">Por ejemplo, una captura de transferencia bancaria o un recibo. Se conservará el historial de la operación.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 48 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo se admiten pagos a través de la plataforma",b:"permitir registrar manualmente pagos externos con medio, fecha y administrador responsable",c:"permitir registrar pagos externos con medio, fecha, responsable y comprobante adjunto opcional"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**, porque los clubes suelen recibir pagos por distintos medios. Registrar esos pagos evitaría deudas ficticias y permitiría mantener un historial completo de cobranza.

Hay una implicación comercial importante que definiremos después: **cómo se aplica la comisión de la plataforma a los pagos externos**, si se permiten.
```

## DEC-048

### Registro 149 — Registro de pagos externos
Fuente: `474475ec-449f-4b2a-9d08-05916fb44bee`

Respuesta previa del usuario:

Para la pregunta 48 del relevamiento SDD elijo la opción C: permitir registrar pagos externos con medio, fecha, responsable y comprobante adjunto opcional.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 48 — Confirmada**
</row>

Los administradores podrán **registrar pagos realizados fuera de la plataforma**, indicando el medio utilizado, la fecha, el responsable y, opcionalmente, adjuntando un comprobante.

## Registro SDD — Decisión 048

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-048 — Registro de pagos externos**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - RN-293: Los administradores podrán registrar pagos externos únicamente de su disciplina.
  - RN-294: Se admitirán medios como efectivo, transferencia bancaria y otros medios externos.
  - RN-295: Cada registro incluirá medio de pago, fecha e identificación del administrador responsable.
  - RN-296: Se permitirá adjuntar un comprobante, pero no será obligatorio.
  - RN-297: El registro de un pago externo completo cancelará las cuotas correspondientes.
  - RN-298: No se permitirán pagos externos parciales, respetando DEC-047.
  - RN-299: Los pagos externos se identificarán separadamente de los procesados por la pasarela.
  - RN-300: Se conservará el historial de operaciones y sus comprobantes.
  - RN-301: Los delegados generales podrán consultar estos pagos, sin registrarlos ni modificarlos.
</box>

## Ejemplo — Registro de pago externo

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Pago de Juan Pérez**
    <badge color="success">Registrado</badge>
  </row>
  | Campo | Valor de ejemplo |
  |---|---|
  | Disciplina | Obras Futsal |
  | Cuota | Junio 2026 |
  | Importe | $25.000 |
  | Medio | Transferencia bancaria |
  | Fecha | 15/06/2026 |
  | Responsable | Administrador de Futsal |
  | Comprobante | Opcional |

  <caption>El registro manual representa la declaración del administrador de que el club recibió el dinero; no equivale a una confirmación bancaria automática.</caption>
</box>

---

## Fase 3 — Gestión de pagos externos

<text color="secondary" weight="medium" size="xs">Pregunta 49 — Comisión de la plataforma sobre pagos externos</text>

Tenemos que definir una regla comercial importante.

Hasta ahora, el modelo de negocio contempla que la plataforma cobre una **comisión del 5% sobre las cuotas abonadas mediante la plataforma**.

Pero ahora también permitiremos registrar pagos que el club recibió directamente, por ejemplo, en efectivo o transferencia.

**¿Qué debería suceder con la comisión cuando se registra un pago externo?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Sin comisión para pagos externos**

      La plataforma cobra el 5% únicamente cuando procesa el pago mediante su pasarela. Los pagos externos se registran sin comisión.

      <text color="secondary" size="xs">Ejemplo: el club recibe $25.000 en efectivo y registra el pago; no debe comisión por esa operación.</text>
    </radio>
    <radio value="b">
      **Opción B — Comisión del 5% para todos los pagos**

      Tanto los pagos online como los externos generan una comisión del 5%. En los pagos externos, la plataforma registra una deuda del club por esa comisión.
    </radio>
    <radio value="c">
      **Opción C — Comisión configurable según el tipo de pago**

      El superadministrador establece qué comisión se cobra por pagos online y externos, pudiendo utilizar porcentajes distintos.

      <text color="secondary" size="xs">Ejemplo: 5% online y 1% por pagos externos.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 49 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"los pagos externos no generan comisión y el 5% se cobra solo por pagos procesados online",b:"todos los pagos generan una comisión del 5%, incluidos los externos, que generan una deuda del club",c:"el superadministrador configura comisiones distintas para pagos online y externos"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A** para el MVP: evita tener que facturar y cobrar comisiones pendientes a los clubes por operaciones que la plataforma no procesó. La contrapartida es que algunos clubes podrían priorizar los cobros externos para evitar la comisión.
```

## DEC-049

### Registro 153 — Política comercial configurable
Fuente: `5a932ee6-5f8e-42b5-9f5b-87c8dc1a330c`

Respuesta previa del usuario:

Esto debería ser configurable por disciplina y yo debería elegir si se aplica la comisión por club o por disciplina y cuánto es el minimo de pagos, elijamos los pagos como punto de revisión, mínimo de pagos y si se configura por club o por disciplina todos desde el súper admin por ej obras es un club que cada delegado me debería comprar la plataforma, no está unificado el club en lo que son disciplinas por eso no puedo multar general si no superan los x pagos 

Registro original:

```text
Entendido. Esto cambia una parte importante del modelo comercial: **un club puede estar unificado administrativamente en la plataforma, pero no necesariamente comercialmente**.

Por ejemplo, Obras Mendoza puede tener Futsal, Básquet y Vóley, pero cada disciplina puede contratar y responder económicamente por separado. Por eso, no sería correcto penalizar automáticamente a todo Obras porque Futsal no alcanzó su mínimo de pagos.

## Registro SDD — Decisión 049

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-049 — Política comercial configurable**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-302:** El superadministrador configura las condiciones comerciales.
  - **RN-303:** La política puede aplicarse a nivel club o a nivel disciplina.
  - **RN-304:** El superadministrador define el mínimo mensual de pagos requeridos.
  - **RN-305:** Se contabilizan los pagos exitosos procesados mediante la plataforma, no los jugadores únicos.
  - **RN-306:** La evaluación se realiza mensualmente.
  - **RN-307:** Si se alcanza el mínimo configurado, no se cobra comisión al club o disciplina correspondiente.
  - **RN-308:** Cuando la política es por disciplina, el incumplimiento de una no afecta económicamente a las demás.
  - **RN-309:** El sistema debe permitir que las disciplinas de un mismo club tengan condiciones comerciales independientes.
  - **RN-310:** Los administradores y delegados no pueden modificar estas condiciones; solamente el superadministrador.
</box>

## Ejemplo — Obras Mendoza

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Política por disciplina**
    <badge>Ejemplo</badge>
  </row>
  | Disciplina | Mínimo mensual | Pagos realizados | Resultado |
  |---|---:|---:|---|
  | Futsal | 100 | 125 | <text color="success" weight="medium">Cumple</text> |
  | Básquet | 80 | 62 | <text color="danger" weight="medium">No cumple</text> |
  | Vóley | 50 | 57 | <text color="success" weight="medium">Cumple</text> |

  En este escenario, solamente Básquet quedaría sujeto a la comisión por incumplimiento. Futsal y Vóley no serían afectados.
</box>

También queda claro que debemos separar **la comisión por procesamiento del pago que paga el jugador** de **la comisión comercial por no alcanzar el mínimo que podría pagar la disciplina o el club**.

---

## Pregunta 50 — ¿Cómo cobrar cuando no se alcanza el mínimo?

Supongamos que Obras Futsal tiene un mínimo de 100 pagos mensuales, pero en noviembre registra solamente 70.

**¿Qué tipo de cargo querés aplicar por incumplimiento?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Cargo fijo mensual**

      Por ejemplo, si no alcanza 100 pagos, la disciplina debe abonar $20.000, independientemente de si realizó 20 o 99 pagos.
    </radio>
    <radio value="b">
      **Opción B — Cargo por cada pago faltante**

      Por ejemplo, $500 por pago faltante. Si realizó 70 de 100, faltan 30 y debe abonar $15.000.
    </radio>
    <radio value="c">
      **Opción C — Configurable por el superadministrador (recomendada)**

      El superadministrador elige para cada club o disciplina si aplica un cargo fijo o un cargo por pago faltante, y configura su importe.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 50 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"aplicar un cargo fijo mensual cuando no se alcanza el mínimo de pagos",b:"aplicar un cargo por cada pago faltante para alcanzar el mínimo",c:"el superadministrador configura si el cargo por incumplimiento es fijo mensual o por cada pago faltante y define su importe"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**, porque te permite negociar condiciones distintas con cada disciplina sin modificar el sistema.
```

## DEC-050

### Registro 155 — Modalidad de cargo por incumplimiento
Fuente: `2f55445f-2fbd-40bd-b041-02ae42f01987`

Respuesta previa del usuario:

Para la pregunta 50 del relevamiento SDD elijo la opción C: el superadministrador configura si el cargo por incumplimiento es fijo mensual o por cada pago faltante y define su importe.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 50 — Confirmada**
</row>

El superadministrador podrá configurar **cómo se calcula el cargo por incumplimiento del mínimo mensual de pagos**, de forma independiente para cada club o disciplina.

## Registro SDD — Decisión 050

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-050 — Modalidad de cargo por incumplimiento**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-311:** El superadministrador podrá elegir entre cargo fijo mensual o cargo por pago faltante.
  - **RN-312:** El importe del cargo será configurable.
  - **RN-313:** La configuración respetará el alcance comercial definido: club o disciplina.
  - **RN-314:** El cargo se calculará solamente cuando los pagos exitosos del mes sean inferiores al mínimo establecido.
  - **RN-315:** Si se alcanza o supera el mínimo, el cargo por incumplimiento será cero.
  - **RN-316:** En modalidad fija, se cobrará el importe configurado, independientemente de cuántos pagos falten.
  - **RN-317:** En modalidad por pago faltante, se multiplicará la cantidad de pagos faltantes por el importe unitario configurado.
  - **RN-318:** El sistema conservará las condiciones comerciales utilizadas para calcular cada cargo mensual.
  - **RN-319:** Solamente el superadministrador podrá modificar estas condiciones.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <grid columns={2} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Mínimo mensual</text>
        <title size="xl" tabularNums>100</title>
        <caption>Pagos requeridos</caption>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Pagos realizados</text>
        <title size="xl" tabularNums>70</title>
        <caption>Faltan 30 pagos</caption>
      </box>
    </grid-item>
  </grid>
  | Modalidad | Configuración | Cargo |
  |---|---|---:|
  | Fija | $20.000 por mes | $20.000 |
  | Por faltante | $500 × 30 pagos | $15.000 |

  <caption>Las cifras son ilustrativas. Se aplicará únicamente la modalidad elegida para esa disciplina.</caption>
</box>

---

## Fase 3 — Modelo comercial de la plataforma

<text color="secondary" weight="medium" size="xs">Pregunta 51 — ¿Quién recibe y paga el cargo por incumplimiento?</text>

En Obras Mendoza, cada disciplina puede funcionar económicamente de manera independiente. Por eso necesitamos definir **a quién se le genera el cargo mensual cuando no cumple el mínimo**.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Siempre al club**

      Aunque la evaluación sea por disciplina, se genera una única obligación económica a nombre del club.
    </radio>
    <radio value="b">
      **Opción B — Siempre a la disciplina**

      Cada disciplina responde por sus cargos. Si la política se evalúa globalmente por club, el cargo debe asignarse posteriormente a una disciplina responsable.
    </radio>
    <radio value="c">
      **Opción C — Según el contrato configurado (recomendada)**

      El superadministrador define el responsable comercial: el club o una disciplina específica. El cargo se genera únicamente a ese responsable.

      <text color="secondary" size="xs">Ejemplo: Obras Futsal recibe su propio cargo, sin involucrar a Básquet ni a la administración general de Obras.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 51 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"el cargo por incumplimiento siempre se factura al club",b:"el cargo por incumplimiento siempre se asigna a una disciplina",c:"el superadministrador configura el responsable comercial del cargo, club o disciplina, según el contrato"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**, porque mantiene la flexibilidad comercial que necesitás: clubes que contratan de forma unificada y otros, como Obras, donde cada disciplina puede contratar por separado.
```

## DEC-051

### Registro 157 — Responsable comercial configurable
Fuente: `c2bd1065-6ec5-46c8-9beb-7cf6899feea9`

Respuesta previa del usuario:

Opcion C

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 51 — Confirmada**
</row>

El superadministrador podrá definir **quién es el responsable económico del contrato: el club o una disciplina específica**.

Esto permite que un club como Obras Mendoza tenga varias disciplinas que contratan la plataforma de forma independiente.

## Registro SDD — Decisión 051

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-051 — Responsable comercial configurable**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-320:** Cada configuración comercial tendrá un responsable económico identificado.
  - **RN-321:** El responsable podrá ser un club completo o una disciplina específica.
  - **RN-322:** Solamente el superadministrador podrá configurar o modificar al responsable.
  - **RN-323:** Los cargos mensuales se imputarán al responsable económico correspondiente.
  - **RN-324:** Una disciplina independiente no generará cargos para otras disciplinas del mismo club.
  - **RN-325:** El sistema conservará el historial de responsables comerciales y condiciones contractuales.
</box>

## Ejemplo — Obras Mendoza

<box border radius="lg" padding={3} gap={2}>
  <row align="center" gap={2}>
    <icon name="building-2" color="secondary"/>
    **Club Obras Mendoza**
  </row>
  <box padding={{left:3}} gap={2}>
    <row align="center" justify="between">
      <row align="center" gap={2}>
        <icon name="circle-dot" color="secondary"/>
        Futsal
      </row>
      <badge>Contrato propio</badge>
    </row>
    <row align="center" justify="between">
      <row align="center" gap={2}>
        <icon name="circle-dot" color="secondary"/>
        Básquet
      </row>
      <badge>Contrato propio</badge>
    </row>
    <row align="center" justify="between">
      <row align="center" gap={2}>
        <icon name="circle-dot" color="secondary"/>
        Vóley
      </row>
      <badge>Contrato propio</badge>
    </row>
  </box>
  <caption>Cada disciplina puede tener su propio mínimo de pagos, penalización y responsable económico, sin afectar a las demás.</caption>
</box>

---

## Fase 3 — Facturación comercial

<text color="secondary" weight="medium" size="xs">Pregunta 52 — ¿Cómo se cobran las penalizaciones mensuales?</text>

Supongamos que Obras Futsal no alcanza los 100 pagos mínimos en noviembre y se genera un cargo de $15.000.

**¿Cómo querés cobrarle ese importe al responsable comercial?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Pago manual fuera de la plataforma**

      El sistema genera una liquidación mensual y el superadministrador registra manualmente cuando el club o disciplina paga por transferencia u otro medio.
    </radio>
    <radio value="b">
      **Opción B — Pago online obligatorio**

      El sistema genera el cargo mensual y el responsable debe abonarlo mediante la pasarela de pagos integrada.
    </radio>
    <radio value="c">
      **Opción C — Ambas modalidades (recomendada)**

      El sistema genera la liquidación mensual y permite pagarla online o registrar un pago externo manualmente.

      <text color="secondary" size="xs">El superadministrador puede consultar liquidaciones pendientes, pagadas y vencidas.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 52 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"las penalizaciones se liquidan mensualmente y se cobran por fuera de la plataforma, con registro manual",b:"las penalizaciones mensuales deben pagarse obligatoriamente online",c:"las penalizaciones se liquidan mensualmente y pueden pagarse online o registrarse como pagos externos"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**, porque permite cobrar las penalizaciones de manera flexible y mantener un control centralizado de las deudas comerciales de cada club o disciplina.
```

## DEC-052

### Registro 159 — Cobro de penalizaciones comerciales
Fuente: `3bc3afc8-469d-4716-9781-39fc0c7c3ea3`

Respuesta previa del usuario:

Para la pregunta 52 del relevamiento SDD elijo la opción C: las penalizaciones se liquidan mensualmente y pueden pagarse online o registrarse como pagos externos.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 52 — Confirmada**
</row>

Las penalizaciones mensuales podrán abonarse **online mediante la plataforma o por medios externos**, como una transferencia bancaria, registrada manualmente por el superadministrador.

## Registro SDD — Decisión 052

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-052 — Cobro de penalizaciones comerciales**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-326:** El sistema generará una liquidación mensual cuando el responsable comercial no alcance el mínimo de pagos configurado.
  - **RN-327:** Cada liquidación estará asociada al club o disciplina responsable.
  - **RN-328:** Se permitirá pagar la liquidación mediante la pasarela online.
  - **RN-329:** El superadministrador podrá registrar manualmente pagos externos.
  - **RN-330:** Los pagos externos deberán registrar medio de pago, fecha y responsable de la registración.
  - **RN-331:** Las liquidaciones tendrán estados identificables: pendiente, pagada y vencida.
  - **RN-332:** Se conservará el historial de liquidaciones, pagos y modificaciones.
  - **RN-333:** Los cargos comerciales estarán separados de las cuotas deportivas de los jugadores.
</box>

## Ejemplo — Liquidación mensual

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    <box gap={1}>
      **Obras Mendoza — Futsal**
      <text color="secondary" size="xs">Liquidación de noviembre 2026</text>
    </box>
    <badge color="warning">Pendiente</badge>
  </row>
  <grid columns={2} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Pagos realizados</text>
        <title size="xl" tabularNums>70 / 100</title>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Importe a cobrar</text>
        <title size="xl" tabularNums>$15.000</title>
      </box>
    </grid-item>
  </grid>
  <text color="secondary" size="xs">Modalidad de ejemplo: 30 pagos faltantes × $500.</text>
  <row align="center" gap={2}>
    <icon name="credit-card" color="secondary"/>
    <text size="sm">Pago online mediante pasarela</text>
  </row>
  <row align="center" gap={2}>
    <icon name="landmark" color="secondary"/>
    <text size="sm">Pago externo registrado por superadministrador</text>
  </row>
</box>

---

## Fase 3 — Facturación comercial

<text color="secondary" weight="medium" size="xs">Pregunta 53 — ¿Qué sucede si una disciplina no paga su penalización?</text>

Supongamos que Obras Futsal no alcanza el mínimo de pagos de noviembre, se le genera una liquidación de $15.000 y llega diciembre sin haberla pagado.

**¿Qué debería hacer el sistema ante una penalización comercial vencida?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo registrar la deuda**

      La liquidación permanece vencida y el superadministrador puede consultarla, pero la disciplina continúa utilizando normalmente la plataforma.
    </radio>
    <radio value="b">
      **Opción B — Suspensión automática**

      Si la penalización no se paga dentro del plazo establecido, el sistema suspende automáticamente el acceso administrativo de la disciplina hasta regularizar la deuda.
    </radio>
    <radio value="c">
      **Opción C — Suspensión manual por el superadministrador (recomendada)**

      El sistema registra la deuda vencida y alerta al superadministrador, quien decide si suspende comercialmente la disciplina o le permite continuar funcionando.

      <text color="secondary" size="xs">La suspensión afectaría únicamente al responsable comercial incumplidor, nunca a las otras disciplinas independientes del mismo club.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 53 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"registrar la deuda vencida sin suspender el servicio",b:"suspender automáticamente el servicio tras el plazo de pago",c:"alertar al superadministrador y permitirle decidir manualmente si suspende al responsable comercial incumplidor"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**. Te permite negociar con cada disciplina, dar prórrogas cuando corresponda y evitar suspensiones automáticas que podrían perjudicar a los jugadores.
```

## DEC-053

### Registro 161 — Suspensión comercial manual
Fuente: `c1f24d18-4514-4cd8-bc49-59cdd59224ac`

Respuesta previa del usuario:

Para la pregunta 53 del relevamiento SDD elijo la opción C: alertar al superadministrador y permitirle decidir manualmente si suspende al responsable comercial incumplidor.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 53 — Confirmada**
</row>

El sistema notificará al superadministrador cuando existan penalizaciones comerciales vencidas, pero **no suspenderá automáticamente a ningún club o disciplina**. La decisión será manual.

## Registro SDD — Decisión 053

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-053 — Suspensión comercial manual**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-334:** El sistema identificará las liquidaciones comerciales vencidas e impagas.
  - **RN-335:** El superadministrador recibirá una alerta por los incumplimientos.
  - **RN-336:** La existencia de una deuda vencida no provocará una suspensión automática.
  - **RN-337:** Solamente el superadministrador podrá decidir la suspensión comercial.
  - **RN-338:** El superadministrador podrá permitir la continuidad del servicio aun existiendo deuda.
  - **RN-339:** La suspensión se aplicará exclusivamente al responsable comercial definido en el contrato.
  - **RN-340:** Si el responsable es una disciplina independiente, las demás disciplinas del club no se verán afectadas.
  - **RN-341:** Se conservará un historial de suspensiones, reactivaciones, fechas y responsables.
  - **RN-342:** La deuda comercial continuará registrada aunque se suspenda el servicio.
</box>

## Ejemplo — Obras Mendoza

<box border radius="lg" padding={3} gap={2}>
  | Disciplina | Situación comercial | Servicio |
  |---|---|---|
  | Futsal | Penalización vencida | <badge color="warning">Decisión pendiente</badge> |
  | Básquet | Al día | <text color="success">Activo</text> |
  | Vóley | Al día | <text color="success">Activo</text> |

  El superadministrador puede suspender Futsal sin interrumpir el funcionamiento de Básquet ni Vóley.
</box>

---

## Fase 3 — Suspensiones comerciales

<text color="secondary" weight="medium" size="xs">Pregunta 54 — ¿Qué implica suspender una disciplina por falta de pago?</text>

Supongamos que Obras Futsal no paga su penalización y decidís suspenderla.

Los jugadores todavía tienen cuotas pendientes y algunos necesitan consultar su historial de pagos.

**¿Qué funcionalidades deberían bloquearse durante la suspensión?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Suspensión total**

      Se bloquean los administradores, el portal de jugadores y todos los cobros. Nadie puede operar hasta que se reactive la disciplina.
    </radio>
    <radio value="b">
      **Opción B — Suspender la administración, mantener los cobros**

      Los administradores no pueden gestionar jugadores ni cuotas, pero los jugadores pueden seguir consultando y pagando sus deudas anteriores.
    </radio>
    <radio value="c">
      **Opción C — Bloquear nuevas operaciones y conservar consultas y cobros (recomendada)**

      Se bloquean nuevas inscripciones, generación de cuotas y cambios administrativos. Los administradores pueden consultar información histórica y los jugadores pueden consultar y pagar cuotas ya existentes.

      <text color="secondary" size="xs">La disciplina queda suspendida comercialmente, pero no pierde sus datos ni impide que los jugadores regularicen deudas.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 54 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"la suspensión bloquea administración, portal de jugadores y todos los cobros",b:"la suspensión bloquea la administración pero permite a los jugadores consultar y pagar cuotas anteriores",c:"la suspensión bloquea nuevas operaciones y modificaciones, conserva consultas históricas y permite cobrar cuotas existentes"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**, porque protege tu modelo comercial sin impedir que la disciplina siga recaudando dinero para regularizar su situación.
```

## DEC-054

### Registro 163 — Alcance de la suspensión comercial
Fuente: `c2fcf417-e627-42b8-a3b0-b8fc779145dd`

Respuesta previa del usuario:

Para la pregunta 54 del relevamiento SDD elijo la opción C: la suspensión bloquea nuevas operaciones y modificaciones, conserva consultas históricas y permite cobrar cuotas existentes.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 54 — Confirmada**
</row>

La suspensión comercial bloqueará nuevas operaciones y modificaciones, pero **permitirá consultar información histórica y cobrar cuotas ya generadas**.

## Registro SDD — Decisión 054

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-054 — Alcance de la suspensión comercial**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-343:** Se bloquearán las nuevas inscripciones de jugadores.
  - **RN-344:** Se detendrá la generación automática de nuevas cuotas.
  - **RN-345:** Los administradores no podrán modificar jugadores, categorías, becas ni configuraciones de la disciplina suspendida.
  - **RN-346:** Los administradores conservarán acceso de solo lectura a la información histórica.
  - **RN-347:** Los jugadores podrán consultar sus cuotas e historial de pagos.
  - **RN-348:** Se permitirá pagar cuotas generadas antes de la suspensión, incluyendo los recargos correspondientes.
  - **RN-349:** Los datos y registros históricos permanecerán intactos.
  - **RN-350:** La suspensión afectará exclusivamente al responsable comercial correspondiente.
  - **RN-351:** Solamente el superadministrador podrá levantar la suspensión comercial.
</box>

## Ejemplo — Obras Futsal suspendido

<box border radius="lg" padding={3} gap={2}>
  | Funcionalidad | Estado |
  |---|---|
  | Inscribir jugadores | <badge color="danger">Bloqueado</badge> |
  | Generar cuotas nuevas | <badge color="danger">Bloqueado</badge> |
  | Modificar categorías o becas | <badge color="danger">Bloqueado</badge> |
  | Consultar jugadores y cuotas | <badge color="success">Permitido</badge> |
  | Pagar cuotas existentes | <badge color="success">Permitido</badge> |
  | Consultar historial de pagos | <badge color="success">Permitido</badge> |
</box>

---

## Fase 3 — Reactivación comercial

<text color="secondary" weight="medium" size="xs">Pregunta 55 — ¿Qué sucede con las cuotas durante una suspensión?</text>

Supongamos que Obras Futsal estuvo suspendido durante junio y julio. El superadministrador lo reactiva el 1 de agosto.

Durante esos dos meses no se generaron cuotas nuevas, pero las anteriores continuaron disponibles para el cobro.

**¿Qué debería ocurrir con las cuotas de junio y julio al reactivar la disciplina?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — No generar cuotas retroactivas (recomendada)**

      Junio y julio quedan sin cuotas nuevas. La generación automática se retoma desde agosto.

      <text color="secondary" size="xs">Es consistente con la regla que ya definimos para reactivar disciplinas desactivadas.</text>
    </radio>
    <radio value="b">
      **Opción B — Generar automáticamente todas las cuotas pendientes**

      Al reactivar Futsal, el sistema genera las cuotas correspondientes a junio y julio para todos los jugadores activos.
    </radio>
    <radio value="c">
      **Opción C — El superadministrador decide al reactivar**

      Puede elegir entre omitir los meses suspendidos o generar retroactivamente las cuotas correspondientes.

      <text color="secondary" size="xs">La decisión se registra y se aplica únicamente a la disciplina reactivada.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 55 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"no generar cuotas retroactivas por meses suspendidos y retomar la generación desde la reactivación",b:"generar automáticamente todas las cuotas de los meses suspendidos al reactivar",c:"el superadministrador decide si generar retroactivamente las cuotas de los meses suspendidos"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A**: mantiene coherencia con las decisiones anteriores, evita deudas inesperadas para los jugadores y simplifica la reactivación comercial.
```

## DEC-055

### Registro 165 — Generación retroactiva tras suspensión comercial
Fuente: `948fabe4-ae2a-46ad-8641-681da4ace9c7`

Respuesta previa del usuario:

Para la pregunta 55 del relevamiento SDD elijo la opción C: el superadministrador decide si generar retroactivamente las cuotas de los meses suspendidos.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 55 — Confirmada**
</row>

Al levantar una suspensión comercial, **el superadministrador decidirá si se generan o no las cuotas correspondientes a los meses suspendidos**.

Esta decisión será independiente para cada club o disciplina, según el alcance de la suspensión.

## Registro SDD — Decisión 055

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-055 — Generación retroactiva tras suspensión comercial**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-352:** Solamente el superadministrador podrá reactivar un responsable suspendido comercialmente.
  - **RN-353:** Durante la reactivación deberá decidir si genera las cuotas de los meses suspendidos.
  - **RN-354:** Si decide no generarlas, esos períodos quedarán sin nuevas cuotas.
  - **RN-355:** Si decide generarlas, el sistema creará las cuotas correspondientes a los períodos omitidos.
  - **RN-356:** La generación retroactiva deberá respetar las inscripciones y condiciones económicas aplicables a cada período.
  - **RN-357:** Se evitará generar cuotas duplicadas para un mismo jugador, disciplina y período.
  - **RN-358:** Se registrará la decisión, fecha, responsable y cuotas generadas.
  - **RN-359:** La generación automática normal se retomará después de la reactivación, respetando el calendario configurado.
  - **RN-360:** Esta posibilidad de retroactividad aplica a suspensiones comerciales, no a la desactivación ordinaria de disciplinas definida anteriormente.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Reactivación comercial**
    <badge>1 de agosto</badge>
  </row>
  <grid columns={2} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Suspensión</text>
        **Junio y julio**
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Períodos omitidos</text>
        <title size="xl">2 meses</title>
      </box>
    </grid-item>
  </grid>
  <text weight="medium" size="sm">Decisión del superadministrador:</text>
  <row align="start" gap={2}>
    <icon name="circle-check" color="success"/>
    <box flex="1" gap={1}>
      **Generar cuotas retroactivas**

      <text size="sm" color="secondary">Se crean las cuotas faltantes de junio y julio.</text>
    </box>
  </row>
  <row align="start" gap={2}>
    <icon name="circle-minus" color="secondary"/>
    <box flex="1" gap={1}>
      **Omitir cuotas retroactivas**

      <text size="sm" color="secondary">Junio y julio quedan sin cuotas nuevas. Se retoma la operación desde agosto.</text>
    </box>
  </row>
</box>

---

## Fase 3 — Cuotas retroactivas

<text color="secondary" weight="medium" size="xs">Pregunta 56 — Vencimiento de cuotas generadas retroactivamente</text>

Supongamos que Obras Futsal estuvo suspendido en junio y julio, y el 1 de agosto decidís generar las cuotas de esos meses.

El problema es que sus fechas originales de vencimiento ya pasaron.

**¿Qué fecha de vencimiento deberían tener esas cuotas retroactivas?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Mantener el vencimiento original**

      La cuota de junio conserva su vencimiento de junio y la de julio el de julio. Podrían aparecer vencidas inmediatamente y generar recargos según sus políticas.
    </radio>
    <radio value="b">
      **Opción B — Asignar un nuevo vencimiento automáticamente**

      Todas las cuotas retroactivas reciben una nueva fecha de vencimiento calculada desde la reactivación, por ejemplo, 15 días después.
    </radio>
    <radio value="c">
      **Opción C — El superadministrador define el vencimiento (recomendada)**

      Al generar las cuotas retroactivas, el superadministrador decide si conservar los vencimientos originales o asignar una nueva fecha.

      <text color="secondary" size="xs">Esto permite evitar recargos injustificados cuando la suspensión fue una decisión comercial de la plataforma.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 56 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"las cuotas retroactivas mantienen sus vencimientos originales",b:"las cuotas retroactivas reciben automáticamente un nuevo vencimiento desde la reactivación",c:"el superadministrador decide si conservar los vencimientos originales o establecer una nueva fecha para las cuotas retroactivas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**, porque te da control sobre las condiciones de reactivación sin modificar las reglas habituales de las cuotas generadas normalmente.
```

## DEC-056

### Registro 170 — Regularización asistida de cuotas retroactivas
Fuente: `83f8fe66-e386-442c-bf43-424ead6f247b`

Respuesta previa del usuario:

Para la pregunta 56 del relevamiento SDD elijo la opción C: el sistema prepara las cuotas retroactivas y el administrador revisa pagos, jugadores y vencimientos antes de confirmar la regularización.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 56 — Confirmada**
</row>

La regularización de los meses suspendidos será **asistida por el sistema y supervisada por el administrador de la disciplina**.

Esto permite que Obras Futsal continúe funcionando fuera de la plataforma durante una suspensión y, al reactivarse, pueda actualizar correctamente quién pagó, quién debe y qué fechas de vencimiento corresponden.

## Registro SDD — Decisión 056

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-056 — Regularización asistida de cuotas retroactivas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-361:** El superadministrador decide al reactivar si se habilita la regularización de meses suspendidos, según DEC-055.
  - **RN-362:** El sistema prepara una propuesta de cuotas faltantes correspondientes a esos períodos.
  - **RN-363:** Las cuotas propuestas no se publicarán ni habilitarán para cobro hasta su confirmación.
  - **RN-364:** El administrador de la disciplina podrá revisar los jugadores incluidos.
  - **RN-365:** Podrá identificar y registrar pagos externos realizados durante la suspensión.
  - **RN-366:** Podrá definir o ajustar las fechas de vencimiento de las cuotas retroactivas.
  - **RN-367:** La regularización requerirá confirmación explícita del administrador.
  - **RN-368:** El sistema evitará duplicar cuotas o pagos previamente registrados.
  - **RN-369:** Se conservará trazabilidad de los cambios, pagos registrados y responsable de la confirmación.
  - **RN-370:** Esta operación será una excepción controlada a la generación mensual exclusivamente automática.
</box>

## Ejemplo — Regularización de Obras Futsal

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Regularización de junio y julio**
    <badge color="warning">Borrador</badge>
  </row>
  | Jugador | Junio | Julio |
  |---|---|---|
  | Juan Pérez | Pagó en efectivo | Pendiente |
  | Martín López | Pagó por transferencia | Pagó en efectivo |
  | Lucas Gómez | Pendiente | Pendiente |
  <divider color="subtle"/>
  <row align="center" gap={2}>
    <icon name="clipboard-check" color="secondary"/>
    <box flex="1" gap={1}>
      **Revisión del administrador**

      <text color="secondary" size="sm">Registrar pagos externos, establecer vencimientos y confirmar las cuotas que efectivamente siguen pendientes.</text>
    </box>
  </row>
  <caption>Ejemplo ilustrativo. Hasta la confirmación, las cuotas propuestas no están disponibles para el cobro.</caption>
</box>

---

## Fase 3 — Regularización y reactivación

<text color="secondary" weight="medium" size="xs">Pregunta 57 — ¿Qué sucede mientras el administrador regulariza los meses anteriores?</text>

Supongamos que Obras Futsal se reactiva el 1 de agosto, pero su administrador tarda una semana en revisar los pagos de junio y julio.

Mientras tanto, el sistema tiene programada la generación automática de las cuotas de agosto.

**¿Cómo debería funcionar la plataforma durante ese período?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Funcionamiento normal y regularización en paralelo (recomendada)**

      La disciplina vuelve a operar normalmente. Se generan las cuotas de agosto y el administrador puede inscribir jugadores, mientras regulariza junio y julio por separado.

      <text color="secondary" size="xs">Los meses anteriores quedan pendientes de revisión, sin bloquear las operaciones actuales.</text>
    </radio>
    <radio value="b">
      **Opción B — Bloquear nuevas cuotas hasta terminar**

      Los administradores pueden consultar y regularizar información, pero no se generan cuotas nuevas hasta completar los meses suspendidos.
    </radio>
    <radio value="c">
      **Opción C — El superadministrador decide**

      Al reactivar, el superadministrador elige si la disciplina funciona inmediatamente o permanece con restricciones hasta completar la regularización.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 57 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"la disciplina funciona normalmente y genera cuotas nuevas mientras regulariza los meses suspendidos en paralelo",b:"se bloquea la generación de nuevas cuotas hasta completar la regularización",c:"el superadministrador decide si la disciplina funciona normalmente o mantiene restricciones hasta regularizar"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A**. Permite recuperar la operación habitual inmediatamente sin obligar a que todos los pagos históricos estén conciliados antes de empezar a cobrar el mes actual.
```

## DEC-057

### Registro 172 — Reactivación operativa con regularización paralela
Fuente: `d82182b2-0bb6-4f59-b785-1e147eb079fb`

Respuesta previa del usuario:

Para la pregunta 57 del relevamiento SDD elijo la opción A: la disciplina funciona normalmente y genera cuotas nuevas mientras regulariza los meses suspendidos en paralelo.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 57 — Confirmada**
</row>

Cuando el superadministrador reactive una disciplina, **volverá a funcionar normalmente de inmediato**, aunque todavía tenga meses anteriores pendientes de regularización.

## Registro SDD — Decisión 057

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-057 — Reactivación operativa con regularización paralela**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-371:** La reactivación comercial restablecerá inmediatamente las funcionalidades administrativas.
  - **RN-372:** Se habilitarán nuevamente las inscripciones, modificaciones de jugadores, categorías y becas.
  - **RN-373:** Se retomará la generación automática de cuotas según el calendario de la disciplina.
  - **RN-374:** El administrador podrá regularizar los meses suspendidos en paralelo con la operación habitual.
  - **RN-375:** Las cuotas retroactivas en borrador no bloquearán la generación de cuotas actuales.
  - **RN-376:** Los pagos externos registrados durante la regularización se asociarán a sus períodos correspondientes.
  - **RN-377:** Se evitará duplicar cuotas entre la generación automática y la regularización histórica.
  - **RN-378:** El sistema permitirá consultar el estado y avance de la regularización.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **1 de agosto — Disciplina reactivada**
    <badge color="success">Activa</badge>
  </row>
  <grid columns={2} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="lg" padding={3} gap={2}>
        <row align="center" gap={2}>
          <icon name="check-circle" color="success"/>
          **Operación actual**
        </row>
        <text size="sm">Inscripciones, administración y cuotas de agosto funcionando normalmente.</text>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="lg" padding={3} gap={2}>
        <row align="center" gap={2}>
          <icon name="clock" color="secondary"/>
          **Regularización histórica**
        </row>
        <text size="sm">Revisión de cuotas y pagos externos de junio y julio, sin bloquear agosto.</text>
      </box>
    </grid-item>
  </grid>
</box>

---

## Fase 3 — Control comercial y pagos

<text color="secondary" weight="medium" size="xs">Pregunta 58 — Pagos realizados durante una suspensión</text>

Supongamos que Obras Futsal estuvo suspendido en junio y julio. Durante esos meses, los jugadores siguieron pagando directamente a la disciplina.

Al reactivar la plataforma, el administrador registra esos pagos externos para regularizar las cuotas.

**¿Deberían esos pagos contar para alcanzar el mínimo mensual de pagos exigido por tu plataforma?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — No cuentan (recomendada)**

      Solo cuentan los pagos exitosos procesados por la pasarela de la plataforma. Los pagos externos sirven para regularizar las deudas, pero no para cumplir el mínimo comercial.
    </radio>
    <radio value="b">
      **Opción B — Cuentan todos los pagos registrados**

      Tanto los pagos online como los externos cuentan para alcanzar el mínimo mensual, aunque la plataforma no haya procesado el dinero.
    </radio>
    <radio value="c">
      **Opción C — Configurable por el superadministrador**

      El superadministrador define para cada contrato si los pagos externos cuentan o no para alcanzar el mínimo mensual.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 58 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo cuentan para el mínimo comercial los pagos exitosos procesados por la pasarela; los externos no cuentan",b:"cuentan para el mínimo comercial tanto los pagos online como los externos registrados",c:"el superadministrador configura por contrato si los pagos externos cuentan para el mínimo comercial"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A**, porque tu modelo comercial depende del volumen de pagos procesados por la plataforma. Si los pagos externos contaran, una disciplina podría alcanzar el mínimo sin utilizar realmente el servicio de cobros.
```

## DEC-058

### Registro 174 — Pagos válidos para el mínimo comercial
Fuente: `9fe5c979-a05c-4b4c-a3b3-a213c9128837`

Respuesta previa del usuario:

Para la pregunta 58 del relevamiento SDD elijo la opción A: solo cuentan para el mínimo comercial los pagos exitosos procesados por la pasarela; los externos no cuentan.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 58 — Confirmada**
</row>

Para alcanzar el mínimo mensual contratado, **solamente se contabilizarán los pagos exitosos procesados mediante la pasarela de la plataforma**.

Los pagos externos servirán para regularizar las cuotas de los jugadores, pero no sumarán al cumplimiento comercial.

## Registro SDD — Decisión 058

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-058 — Pagos válidos para el mínimo comercial**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-379:** Solo se contabilizarán operaciones de pago exitosas procesadas por la pasarela integrada.
  - **RN-380:** Los pagos externos no sumarán al mínimo mensual, independientemente de su medio de pago.
  - **RN-381:** Los pagos externos registrados durante una regularización tampoco sumarán al mínimo.
  - **RN-382:** Una operación online que agrupe varias cuotas contará como un solo pago.
  - **RN-383:** Los pagos rechazados, cancelados o pendientes no se contabilizarán.
  - **RN-384:** Cada operación válida se contabilizará una única vez, evitando duplicados por notificaciones de la pasarela.
  - **RN-385:** El sistema mantendrá separado el registro de cobros de cuotas del contador comercial mensual.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={3}>
  <grid columns={3} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} align="center" gap={1}>
        <text color="secondary" size="2xs">Pagos online</text>
        <title size="xl" color="success" tabularNums>75</title>
        <caption>Computables</caption>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} align="center" gap={1}>
        <text color="secondary" size="2xs">Pagos externos</text>
        <title size="xl" tabularNums>40</title>
        <caption>No computables</caption>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} align="center" gap={1}>
        <text color="secondary" size="2xs">Mínimo</text>
        <title size="xl" tabularNums>100</title>
        <caption>Requeridos</caption>
      </box>
    </grid-item>
  </grid>
  <box gap={1}>
    <row align="center" justify="between">
      <text size="sm" weight="medium">Cumplimiento mensual</text>
      **75%**
    </row>
    <box background="surface-tertiary" height="8px" radius="full" clip>
      <box background="success-solid" width="75%" height="100%" radius="full"/>
    </box>
  </box>
  <row align="center" gap={2}>
    <icon name="alert-circle" color="danger"/>
    <text size="sm">Faltan 25 pagos online para alcanzar el mínimo.</text>
  </row>
</box>

---

## Fase 3 — Control de mínimos comerciales

<text color="secondary" weight="medium" size="xs">Pregunta 59 — ¿Cómo se asignan los pagos al mes comercial?</text>

Supongamos que un jugador paga el **5 de agosto** una cuota correspondiente a junio.

Como la plataforma evalúa el mínimo de pagos de cada mes, necesitamos definir a qué mes se le suma esa operación.

**¿Qué criterio debería utilizar el sistema?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Mes en que se realizó el pago (recomendada)**

      El pago cuenta para agosto, aunque corresponda a una cuota de junio.

      <text color="secondary" size="xs">Se mide el volumen real de operaciones procesadas durante cada mes.</text>
    </radio>
    <radio value="b">
      **Opción B — Mes al que corresponde la cuota**

      El pago cuenta para junio, aunque el dinero se haya recibido en agosto.
    </radio>
    <radio value="c">
      **Opción C — Configurable por contrato**

      El superadministrador define si se utiliza el mes del pago o el período de la cuota.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 59 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"los pagos cuentan para el mes calendario en que fueron procesados exitosamente, sin importar el período de la cuota",b:"los pagos cuentan para el mes al que corresponde la cuota abonada",c:"el superadministrador configura por contrato si los pagos cuentan por fecha de procesamiento o período de cuota"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A**, porque el mínimo comercial mide la cantidad de transacciones que tu plataforma procesó efectivamente durante el mes, no la cantidad de cuotas correspondientes a ese período.
```

## DEC-059

### Registro 176 — Imputación mensual de pagos comerciales
Fuente: `b2fd1fd6-5b56-48fd-be0b-7c90b9eab850`

Respuesta previa del usuario:

Para la pregunta 59 del relevamiento SDD elijo la opción A: los pagos cuentan para el mes calendario en que fueron procesados exitosamente, sin importar el período de la cuota.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 59 — Confirmada**
</row>

Los pagos contarán para el **mes calendario en el que fueron procesados exitosamente**, independientemente del período al que pertenezcan las cuotas abonadas.

## Registro SDD — Decisión 059

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-059 — Imputación mensual de pagos comerciales**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-386:** Cada pago online exitoso se contabilizará en el mes calendario de su confirmación.
  - **RN-387:** El período de las cuotas abonadas no modificará el mes de contabilización comercial.
  - **RN-388:** Una operación que agrupe varias cuotas contará como un único pago.
  - **RN-389:** Los pagos externos no se contabilizarán para el mínimo comercial.
  - **RN-390:** Los pagos pendientes o rechazados no se contabilizarán.
  - **RN-391:** El sistema conservará la fecha y hora de confirmación del pago.
  - **RN-392:** Se evitará contabilizar dos veces una operación por notificaciones duplicadas de la pasarela.
  - **RN-393:** Los reportes comerciales permitirán consultar los pagos computables por mes y responsable comercial.
</box>

## Ejemplo — Obras Futsal

| Cuota abonada | Fecha del pago | Mes comercial |
|---|---|---|
| Junio | 05/08/2026 | Agosto |
| Julio | 08/08/2026 | Agosto |
| Agosto | 12/08/2026 | Agosto |

Las tres operaciones suman al mínimo comercial de agosto, aunque correspondan a cuotas de diferentes meses.

---

## Fase 3 — Liquidación comercial

<text color="secondary" weight="medium" size="xs">Pregunta 60 — ¿Cuándo se genera la penalización mensual?</text>

Supongamos que Obras Futsal tiene un mínimo de 100 pagos y termina noviembre con 85 operaciones exitosas.

El sistema debe determinar cuándo cerrar ese período y generar la liquidación por los 15 pagos faltantes.

**¿Cuándo querés que se realice ese cierre?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Automáticamente el primer día del mes siguiente**

      El 1 de diciembre se cierra noviembre y se genera automáticamente la penalización correspondiente.
    </radio>
    <radio value="b">
      **Opción B — Cierre manual del superadministrador**

      El superadministrador revisa los pagos del mes y decide cuándo cerrar el período y generar la liquidación.
    </radio>
    <radio value="c">
      **Opción C — Cierre automático con revisión del superadministrador (recomendada)**

      El sistema cierra automáticamente el mes y calcula la penalización, pero la deja como liquidación pendiente de aprobación.

      <text color="secondary" size="xs">El superadministrador revisa el cálculo y confirma la liquidación antes de habilitar su cobro.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 60 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"cerrar automáticamente cada mes y generar directamente la penalización",b:"el superadministrador cierra manualmente el mes y genera las penalizaciones",c:"cerrar automáticamente el mes, calcular penalizaciones y requerir aprobación del superadministrador antes del cobro"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**: automatiza el cálculo, pero te permite verificar las penalizaciones antes de cobrarlas, especialmente cuando cada disciplina tiene condiciones contractuales distintas.
```

## DEC-060

### Registro 178 — Cierre comercial automático con aprobación
Fuente: `62fa2664-0bf3-4832-96fc-346c0cfd4425`

Respuesta previa del usuario:

Para la pregunta 60 del relevamiento SDD elijo la opción C: cerrar automáticamente el mes, calcular penalizaciones y requerir aprobación del superadministrador antes del cobro.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 60 — Confirmada**
</row>

El sistema realizará el **cierre comercial automáticamente al finalizar cada mes**, calculará las penalizaciones correspondientes y las dejará pendientes de aprobación del superadministrador antes de habilitar su cobro.

## Registro SDD — Decisión 060

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-060 — Cierre comercial automático con aprobación**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-394:** El sistema cerrará automáticamente cada período comercial mensual.
  - **RN-395:** Se contabilizarán únicamente los pagos online exitosos del mes correspondiente.
  - **RN-396:** Se evaluará el mínimo contratado de cada club o disciplina.
  - **RN-397:** Cuando exista incumplimiento, se calculará el cargo según la modalidad configurada: fijo o por pago faltante.
  - **RN-398:** La liquidación se generará inicialmente en estado *Pendiente de aprobación*.
  - **RN-399:** Solamente el superadministrador podrá aprobar una liquidación comercial.
  - **RN-400:** Ninguna penalización podrá habilitarse para el cobro antes de su aprobación.
  - **RN-401:** Se conservarán el detalle del cálculo, la configuración contractual aplicada y los pagos contabilizados.
  - **RN-402:** Se registrarán la fecha y el responsable de la aprobación.
  - **RN-403:** El cierre mensual deberá evitar generar liquidaciones duplicadas para el mismo contrato y período.
</box>

## Ejemplo — Cierre de noviembre

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Obras Mendoza — Futsal**
    <badge color="warning">Pendiente de aprobación</badge>
  </row>
  <grid columns={2} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Pagos computables</text>
        <title size="xl" tabularNums>85 / 100</title>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Pagos faltantes</text>
        <title size="xl" tabularNums>15</title>
      </box>
    </grid-item>
  </grid>
  <row align="center" justify="between">
    <box gap={1}>
      <text color="secondary" size="xs">Penalización calculada ($500 por faltante)</text>
      <title size="2xl" tabularNums>$7.500</title>
    </box>
    <icon name="clipboard-check" color="secondary" size="2xl"/>
  </row>
  <caption>Ejemplo ilustrativo. El importe todavía no está habilitado para el cobro.</caption>
</box>

---

## Fase 3 — Aprobación de liquidaciones comerciales

<text color="secondary" weight="medium" size="xs">Pregunta 61 — ¿Qué puede hacer el superadministrador al revisar una penalización?</text>

Supongamos que el sistema calcula una penalización de $7.500 para Obras Futsal.

Pero vos sabés que durante noviembre hubo un problema técnico en la pasarela y querés contemplar esa situación antes de cobrarle.

**¿Qué acciones debería permitirte el sistema?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Aprobar o rechazar**

      Podés aprobar el importe calculado o rechazar la liquidación, pero no modificar su monto.
    </radio>
    <radio value="b">
      **Opción B — Aprobar, rechazar o modificar el importe**

      Podés aprobar la penalización, descartarla o cambiar manualmente el importe antes de confirmarla.
    </radio>
    <radio value="c">
      **Opción C — Aprobar, condonar o ajustar con justificación (recomendada)**

      Podés aprobar el importe original, condonar totalmente la penalización o aplicar un ajuste parcial.

      <text color="secondary" size="xs">Cada condonación o ajuste deberá incluir un motivo y quedará registrado con el importe original, el importe final, la fecha y el superadministrador responsable.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 61 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir únicamente aprobar o rechazar la liquidación calculada",b:"permitir aprobar, rechazar o modificar manualmente el importe",c:"permitir aprobar, condonar totalmente o ajustar parcialmente la penalización con justificación y auditoría"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción C**. Te da flexibilidad comercial ante situaciones excepcionales, sin perder la trazabilidad de cuánto correspondía cobrar originalmente y por qué decidiste modificarlo.
```

## DEC-061

### Registro 180 — Aprobación o rechazo de penalizaciones
Fuente: `4c52f1dc-b8b3-435d-84d2-c2ba47b9eec6`

Respuesta previa del usuario:

Para la pregunta 61 del relevamiento SDD elijo la opción A: permitir únicamente aprobar o rechazar la liquidación calculada.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 61 — Confirmada**
</row>

El superadministrador podrá **aprobar o rechazar las penalizaciones calculadas automáticamente**, pero no modificar manualmente sus importes.

## Registro SDD — Decisión 061

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-061 — Aprobación o rechazo de penalizaciones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-404:** Las liquidaciones se generarán automáticamente en estado *Pendiente de aprobación*.
  - **RN-405:** El superadministrador podrá aprobar o rechazar cada liquidación.
  - **RN-406:** No se permitirá modificar manualmente el importe calculado.
  - **RN-407:** Una liquidación aprobada quedará habilitada para su cobro.
  - **RN-408:** Una liquidación rechazada no generará una deuda exigible al responsable comercial.
  - **RN-409:** Se conservarán el importe original, el resultado de la revisión, la fecha y el superadministrador responsable.
  - **RN-410:** Las liquidaciones rechazadas permanecerán en el historial para auditoría.
  - **RN-411:** Una misma liquidación no podrá aprobarse y rechazarse simultáneamente.
</box>

## Ejemplo — Panel del superadministrador

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    <box gap={1}>
      **Obras Mendoza — Futsal**
      <text color="secondary" size="xs">Penalización de noviembre 2026</text>
    </box>
    <badge color="warning">Pendiente</badge>
  </row>
  <row align="center" justify="between">
    <text color="secondary" size="sm">Importe calculado</text>
    <title size="2xl" tabularNums>$7.500</title>
  </row>
  <grid columns={2} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <row align="center" gap={2}>
          <icon name="check-circle" color="success"/>
          **Aprobar**
        </row>
        <text size="xs" color="secondary">Habilita el cobro de $7.500.</text>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <row align="center" gap={2}>
          <icon name="x-circle" color="danger"/>
          **Rechazar**
        </row>
        <text size="xs" color="secondary">Descarta el cargo, conservando su registro.</text>
      </box>
    </grid-item>
  </grid>
</box>

---

## Fase 3 — Auditoría comercial

<text color="secondary" weight="medium" size="xs">Pregunta 62 — Motivo de rechazo de una penalización</text>

Supongamos que rechazás una penalización de $7.500 porque la pasarela tuvo problemas técnicos durante noviembre.

**¿Debería el sistema exigirte que indiques el motivo del rechazo?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Rechazo sin justificación**

      El superadministrador puede rechazar directamente la penalización, sin escribir ningún motivo.
    </radio>
    <radio value="b">
      **Opción B — Motivo obligatorio (recomendada)**

      Para rechazar una liquidación, el superadministrador debe ingresar una justificación que quedará registrada en el historial.

      <text color="secondary" size="xs">Ejemplo: «Se rechaza por interrupciones de la pasarela durante el período».</text>
    </radio>
    <radio value="c">
      **Opción C — Motivo opcional**

      El superadministrador puede escribir una justificación, pero el sistema no la exige para completar el rechazo.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 62 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"el superadministrador puede rechazar liquidaciones sin indicar un motivo",b:"el superadministrador debe indicar obligatoriamente un motivo para rechazar una liquidación",c:"el motivo del rechazo es opcional"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción B**, porque permite mantener un historial comercial claro y justificar por qué decidiste no cobrar una penalización que el sistema había calculado correctamente.
```

### Registro 182 — Corregida y aprobada
Fuente: `6f76bfdc-b54a-4b58-8ce3-0f1e7e8e2495`

Respuesta previa del usuario:

volvamos atras, toque sin querer la a pero era la c **Opción C — Aprobar, condonar o ajustar con justificación (recomendada)&#x20;**

Registro original:

```text
Perfecto, corregimos la **Pregunta 61**. La opción A queda reemplazada por la **opción C**, que será la decisión válida para el SDD.

<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **DEC-061 — Corregida y aprobada**
</row>

## Aprobación, condonación o ajuste de penalizaciones

El superadministrador tendrá tres posibilidades al revisar una liquidación comercial:

- **Aprobar:** confirma el importe calculado automáticamente.
- **Condonar:** elimina el 100% de la penalización, dejando registro del motivo.
- **Ajustar:** reduce parcialmente el importe de la penalización, indicando una justificación.

<box border radius="lg" padding={3} gap={2}>
  **Reglas de negocio corregidas:**

  - **RN-404:** Las liquidaciones se generan pendientes de aprobación.
  - **RN-405:** El superadministrador puede aprobar, condonar totalmente o ajustar parcialmente.
  - **RN-406:** Toda condonación o ajuste requiere una justificación obligatoria.
  - **RN-407:** El importe calculado originalmente nunca se sobrescribe.
  - **RN-408:** Se registra el importe final aprobado.
  - **RN-409:** Una condonación total deja la liquidación sin saldo exigible.
  - **RN-410:** Una aprobación o ajuste habilita el cobro del importe final.
  - **RN-411:** Se registra fecha, responsable, motivo y valores anteriores y finales para auditoría.
</box>

## Ejemplo

| Acción | Importe original | Importe final |
|---|---:|---:|
| Aprobar | $7.500 | $7.500 |
| Condonar | $7.500 | $0 |
| Ajustar parcialmente | $7.500 | $4.000 |

---

## Pregunta 62 — ¿Quién puede ver las justificaciones de ajustes y condonaciones?

Ya establecimos que el superadministrador debe justificar estas decisiones. Ahora necesitamos definir su visibilidad.

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo el superadministrador**

      El club o disciplina ve el importe final, pero no la justificación interna.
    </radio>
    <radio value="b">
      **Opción B — Superadministrador y responsable comercial (recomendada)**

      El responsable comercial puede consultar el importe original, el descuento aplicado y la justificación, además del importe final.
    </radio>
    <radio value="c">
      **Opción C — Justificación interna y observación pública separadas**

      El superadministrador registra una justificación interna obligatoria y puede agregar una explicación visible para el responsable comercial.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 62 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo el superadministrador ve las justificaciones de ajustes y condonaciones",b:"el superadministrador y el responsable comercial pueden consultar las justificaciones y los importes originales y finales",c:"se registra una justificación interna obligatoria y una observación pública opcional para el responsable comercial"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-062

### Registro 185 — Transparencia de ajustes comerciales
Fuente: `c38117eb-d0a0-47db-9508-fd379478ec85`

Respuesta previa del usuario:

Para la pregunta 62 del relevamiento SDD elijo la opción B: el superadministrador y el responsable comercial pueden consultar las justificaciones y los importes originales y finales.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 62 — Confirmada**
</row>

El superadministrador y el responsable comercial podrán consultar **las justificaciones de los ajustes y condonaciones**, junto con los importes originales y finales de cada liquidación.

## Registro SDD — Decisión 062

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-062 — Transparencia de ajustes comerciales**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-412:** El superadministrador podrá consultar todas las liquidaciones, ajustes y condonaciones de la plataforma.
  - **RN-413:** Cada responsable comercial podrá consultar únicamente las liquidaciones que le correspondan.
  - **RN-414:** Se mostrará el importe originalmente calculado.
  - **RN-415:** Se mostrará el importe final aprobado, después de aplicar cualquier ajuste o condonación.
  - **RN-416:** La justificación registrada por el superadministrador será visible para el responsable comercial.
  - **RN-417:** Se identificará la fecha de la decisión y el responsable de realizarla.
  - **RN-418:** Las disciplinas independientes no podrán consultar las condiciones ni los ajustes comerciales de otras disciplinas.
  - **RN-419:** Los registros históricos permanecerán disponibles para auditoría.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Liquidación — Noviembre 2026**
    <badge color="success">Ajustada</badge>
  </row>
  <row align="center" justify="between">
    <text color="secondary" size="sm">Penalización original</text>
    <text weight="medium" tabularNums>$15.000</text>
  </row>
  <row align="center" justify="between">
    <text color="secondary" size="sm">Ajuste aplicado</text>
    <text color="success" weight="medium" tabularNums>−$5.000</text>
  </row>
  <divider color="subtle"/>
  <row align="center" justify="between">
    **Importe final**
    <title size="xl" tabularNums>$10.000</title>
  </row>
  <box background="surface-secondary" radius="md" padding={3} gap={1}>
    <text color="secondary" weight="medium" size="xs">Justificación visible</text>
    <text size="sm">Ajuste excepcional por interrupciones del servicio de pagos durante el período.</text>
  </box>
  <caption>Ejemplo ilustrativo de la información disponible para el responsable comercial.</caption>
</box>

---

## Fase 3 — Configuración de contratos comerciales

<text color="secondary" weight="medium" size="xs">Pregunta 63 — Cambios en el mínimo de pagos contratado</text>

Supongamos que Obras Futsal tiene contratado un mínimo de **100 pagos mensuales**, pero el 15 de noviembre decidís reducirlo a 70.

La disciplina ya realizó 65 pagos durante ese mes.

**¿Desde cuándo debería aplicarse el nuevo mínimo?**

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Desde el mes siguiente (recomendada)**

      Noviembre conserva el mínimo de 100 pagos. Diciembre comienza con el nuevo mínimo de 70.

      <text color="secondary" size="xs">Evita modificar condiciones comerciales de un período que ya está en curso.</text>
    </radio>
    <radio value="b">
      **Opción B — Aplicación inmediata al mes actual**

      El nuevo mínimo de 70 se aplica a todo noviembre, aunque se haya cambiado el día 15.
    </radio>
    <radio value="c">
      **Opción C — El superadministrador decide**

      Al modificar el contrato, el superadministrador elige si el cambio se aplica al mes actual o desde el siguiente.

      <text color="secondary" size="xs">El sistema conserva el mínimo anterior, el nuevo, la fecha de aplicación y quién realizó el cambio.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 63 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"los cambios en el mínimo de pagos se aplican desde el mes siguiente",b:"los cambios en el mínimo de pagos se aplican al mes actual completo",c:"el superadministrador decide si el nuevo mínimo se aplica al mes actual o al siguiente, conservando historial"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

Mi recomendación es la **opción A**, porque simplifica los cierres mensuales y mantiene condiciones contractuales previsibles. Si necesitás contemplar una excepción comercial, ya definimos que podés ajustar o condonar la penalización al aprobarla.
```

## DEC-063

### Registro 187 — Aplicación inmediata del mínimo comercial
Fuente: `e0efbc6d-adc7-44a4-ba47-9666bf634b08`

Respuesta previa del usuario:

Para la pregunta 63 del relevamiento SDD elijo la opción B: los cambios en el mínimo de pagos se aplican al mes actual completo.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 63 — Confirmada**
</row>

Los cambios en el mínimo mensual de pagos se aplicarán **al mes calendario actual completo**, incluso cuando el superadministrador realice la modificación a mitad de mes.

## Registro SDD — Decisión 063

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-063 — Aplicación inmediata del mínimo comercial**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-420:** El superadministrador podrá modificar el mínimo mensual de pagos de cada contrato comercial.
  - **RN-421:** El nuevo mínimo se aplicará a todo el mes calendario en curso.
  - **RN-422:** El cambio afectará el cálculo de cumplimiento del mes completo, independientemente del día en que se realice.
  - **RN-423:** Los pagos exitosos ya contabilizados durante el mes conservarán su validez.
  - **RN-424:** El sistema recalculará el cumplimiento mensual utilizando el nuevo mínimo.
  - **RN-425:** El nuevo mínimo también se utilizará en los meses siguientes, hasta una nueva modificación.
  - **RN-426:** Se conservarán el valor anterior, el nuevo valor, la fecha y el superadministrador responsable.
  - **RN-427:** Las liquidaciones de meses anteriores que ya fueron cerrados no se modificarán automáticamente.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={3}>
  <grid columns={2} gap={3}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Mínimo anterior</text>
        <title size="xl" tabularNums>100</title>
        <caption>Pagos mensuales</caption>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Nuevo mínimo</text>
        <title size="xl" color="success" tabularNums>70</title>
        <caption>Pagos mensuales</caption>
      </box>
    </grid-item>
  </grid>
  <text size="sm">El 15 de noviembre se modifica el mínimo. Los 65 pagos procesados hasta ese momento siguen contando y ahora faltan solamente **5 pagos** para alcanzar el objetivo de noviembre.</text>
</box>

---

## Fase 3 — Modificaciones contractuales

<text color="secondary" weight="medium" size="xs">Pregunta 64 — Cambios en el importe de las penalizaciones</text>

Ya definimos que el mínimo de pagos puede cambiar durante el mes y aplicarse al período completo.

Ahora supongamos que Obras Futsal tiene una penalización de **$500 por cada pago faltante**, pero el 20 de noviembre decidís aumentarla a $800.

**¿Desde cuándo debería aplicarse el nuevo importe de penalización?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Desde el mes siguiente**

      Noviembre conserva la penalización de $500 por pago faltante. Diciembre utiliza $800.
    </radio>
    <radio value="b">
      **Opción B — Al mes actual completo (recomendada por consistencia)**

      El nuevo importe de $800 se aplica a todo noviembre, aunque se haya modificado el día 20.

      <text color="secondary" size="xs">Mantiene el mismo criterio que elegiste para modificar el mínimo mensual de pagos.</text>
    </radio>
    <radio value="c">
      **Opción C — Configurable en cada modificación**

      El superadministrador decide si el nuevo importe se aplica al mes actual o desde el siguiente, registrando la fecha de vigencia.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 64 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"los cambios en el importe de penalizaciones se aplican desde el mes siguiente",b:"los cambios en el importe de penalizaciones se aplican al mes actual completo",c:"el superadministrador decide en cada modificación si el importe nuevo aplica al mes actual o desde el siguiente"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-064

### Registro 189 — Vigencia de importes de penalización
Fuente: `cae3bc6c-72dc-4025-8753-fd1a71355258`

Respuesta previa del usuario:

Para la pregunta 64 del relevamiento SDD elijo la opción B: los cambios en el importe de penalizaciones se aplican al mes actual completo.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 64 — Confirmada**
</row>

Los cambios en el importe de las penalizaciones comerciales se aplicarán **al mes calendario actual completo**, aunque se realicen a mitad de mes.

Esto mantiene el mismo criterio que definimos para los cambios en el mínimo mensual de pagos.

## Registro SDD — Decisión 064

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-064 — Vigencia de importes de penalización**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-428:** El superadministrador podrá modificar el importe de penalización de cada contrato comercial.
  - **RN-429:** El nuevo importe se aplicará al mes calendario en curso completo.
  - **RN-430:** La modificación afectará el cálculo de la penalización mensual, independientemente del día en que se realice.
  - **RN-431:** Se respetará la modalidad de penalización configurada: cargo fijo mensual o importe por pago faltante.
  - **RN-432:** El nuevo importe continuará vigente en los meses siguientes hasta una nueva modificación.
  - **RN-433:** Se conservarán el importe anterior, el nuevo, la fecha y el superadministrador responsable.
  - **RN-434:** Las liquidaciones de períodos anteriores ya cerrados no se recalcularán automáticamente.
  - **RN-435:** El cierre mensual utilizará el importe vigente al momento de calcular la liquidación.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={3}>
  <grid columns={2} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Penalización anterior</text>
        <title size="xl" tabularNums>$500</title>
        <caption>Por pago faltante</caption>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Penalización nueva</text>
        <title size="xl" color="success" tabularNums>$800</title>
        <caption>Por pago faltante</caption>
      </box>
    </grid-item>
  </grid>
  <text size="sm">Si el 20 de noviembre modificás el importe y al cierre faltan 10 pagos, la penalización de noviembre será de **$8.000**, no de $5.000.</text>
</box>

---

## Fase 3 — Configuración de contratos comerciales

<text color="secondary" weight="medium" size="xs">Pregunta 65 — Cambio de modalidad de penalización</text>

Además de cambiar los importes, el superadministrador puede necesitar modificar la modalidad de penalización de un contrato.

Por ejemplo, Obras Futsal tiene una penalización de **$800 por pago faltante**, pero el 20 de noviembre decidís reemplazarla por un **cargo fijo mensual de $15.000**.

**¿Desde cuándo debería aplicarse la nueva modalidad?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Desde el mes siguiente**

      Noviembre mantiene el cálculo por pago faltante y diciembre comienza con el cargo fijo.
    </radio>
    <radio value="b">
      **Opción B — Al mes actual completo (recomendada por consistencia)**

      Noviembre se calculará utilizando el cargo fijo de $15.000 si no se alcanza el mínimo mensual.

      <text color="secondary" size="xs">Aplica el mismo criterio que ya aprobaste para cambios de mínimos e importes.</text>
    </radio>
    <radio value="c">
      **Opción C — El superadministrador decide**

      En cada modificación, el superadministrador elige si la nueva modalidad se aplica al mes actual o desde el siguiente.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 65 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"los cambios de modalidad de penalización se aplican desde el mes siguiente",b:"los cambios de modalidad de penalización se aplican al mes actual completo",c:"el superadministrador decide en cada cambio si la nueva modalidad aplica al mes actual o desde el siguiente"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-065

### Registro 191 — Vigencia de modalidades de penalización
Fuente: `eb79d555-7108-4dd5-90fe-ec53e3c493ae`

Respuesta previa del usuario:

Para la pregunta 65 del relevamiento SDD elijo la opción B: los cambios de modalidad de penalización se aplican al mes actual completo.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 65 — Confirmada**
</row>

Los cambios en la modalidad de penalización comercial se aplicarán **al mes calendario actual completo**, incluso cuando se realicen a mitad de mes.

Con esto, ya tenemos un criterio uniforme para las modificaciones comerciales: **mínimos de pagos, importes y modalidades de penalización** se aplican al mes en curso.

## Registro SDD — Decisión 065

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-065 — Vigencia de modalidades de penalización**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-436:** El superadministrador podrá cambiar la modalidad de penalización de cada contrato comercial.
  - **RN-437:** Las modalidades disponibles serán cargo fijo mensual e importe por pago faltante.
  - **RN-438:** La nueva modalidad se aplicará al mes calendario actual completo.
  - **RN-439:** El cambio afectará el cálculo del mes en curso, independientemente de la fecha de modificación.
  - **RN-440:** El cierre comercial utilizará la modalidad y el importe vigentes al momento del cálculo.
  - **RN-441:** La modalidad continuará vigente en los meses siguientes hasta que se modifique nuevamente.
  - **RN-442:** Se conservarán la modalidad anterior, la nueva, la fecha y el superadministrador responsable.
  - **RN-443:** Las liquidaciones de períodos anteriores ya cerrados no se recalcularán automáticamente.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={3}>
  <grid columns={2} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Modalidad anterior</text>
        <title size="lg">$800</title>
        <caption>Por pago faltante</caption>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Modalidad nueva</text>
        <title size="lg">$15.000</title>
        <caption>Cargo fijo mensual</caption>
      </box>
    </grid-item>
  </grid>
  Si el 20 de noviembre se modifica la modalidad y el club no alcanza el mínimo contratado, la penalización de noviembre será de **$15.000**, independientemente de cuántos pagos falten.
</box>

---

## Fase 3 — Gestión de contratos comerciales

<text color="secondary" weight="medium" size="xs">Pregunta 66 — ¿Cómo se manejan los contratos comerciales nuevos?</text>

Supongamos que el 20 de noviembre incorporás una nueva disciplina, por ejemplo, **Obras Básquet**, con un mínimo contratado de 100 pagos mensuales.

Como la disciplina comenzó a utilizar la plataforma a mitad de mes, necesitamos definir cómo se evalúa su primer período comercial.

**¿Qué debería suceder con el mínimo de pagos del primer mes?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Mínimo completo desde el primer mes**

      Aunque Obras Básquet comience el 20 de noviembre, deberá alcanzar los 100 pagos antes de finalizar noviembre.
    </radio>
    <radio value="b">
      **Opción B — Primer mes sin penalización**

      Noviembre será un período de adaptación. El mínimo de 100 pagos comenzará a exigirse desde diciembre.
    </radio>
    <radio value="c">
      **Opción C — Configurable por el superadministrador (recomendada)**

      Al crear el contrato, el superadministrador define si el mínimo se exige desde el primer mes o si se otorga un período inicial de gracia.

      <text color="secondary" size="xs">Permite negociar condiciones diferentes para cada club o disciplina sin modificar las reglas generales de la plataforma.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 66 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"los contratos nuevos deben cumplir el mínimo completo desde su primer mes, aunque comiencen a mitad de mes",b:"el primer mes de un contrato nuevo no tiene penalización y el mínimo se exige desde el segundo mes",c:"el superadministrador configura por contrato si el mínimo se exige desde el primer mes o si existe un período inicial de gracia"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-066

### Registro 193 — Período de gracia comercial inicial
Fuente: `26ab8490-f169-44ca-8a62-da701d2a7f44`

Respuesta previa del usuario:

Para la pregunta 66 del relevamiento SDD elijo la opción C: el superadministrador configura por contrato si el mínimo se exige desde el primer mes o si existe un período inicial de gracia.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 66 — Confirmada**
</row>

El superadministrador podrá configurar **por cada contrato comercial** si el mínimo de pagos se exige desde el primer mes o si se otorga un período inicial de gracia.

## Registro SDD — Decisión 066

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-066 — Período de gracia comercial inicial**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-444:** Cada contrato comercial podrá tener una configuración independiente de período de gracia inicial.
  - **RN-445:** El superadministrador definirá si el mínimo de pagos se exige desde el primer mes.
  - **RN-446:** Cuando exista un período de gracia, no se generarán penalizaciones por incumplimiento del mínimo durante los meses cubiertos.
  - **RN-447:** Los pagos online realizados durante el período de gracia se registrarán normalmente.
  - **RN-448:** Finalizado el período de gracia, comenzará a exigirse el mínimo mensual contratado.
  - **RN-449:** El período de gracia afectará únicamente al responsable comercial correspondiente.
  - **RN-450:** Se conservarán la configuración, las fechas de vigencia y el superadministrador responsable.
  - **RN-451:** El historial permitirá distinguir los meses exentos de penalización de aquellos sujetos al mínimo comercial.
</box>

## Ejemplo — Obras Básquet

<box border radius="lg" padding={3} gap={2}>
  <text color="secondary" size="xs" weight="medium">Inicio del contrato: 20 de noviembre de 2026</text>
  <grid columns={3} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} align="center" gap={1}>
        **Noviembre**
        <icon name="shield-check" color="success" size="lg"/>
        <caption>Gracia</caption>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} align="center" gap={1}>
        **Diciembre**
        <icon name="target" color="secondary" size="lg"/>
        <caption>Mínimo: 100</caption>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} align="center" gap={1}>
        **Enero**
        <icon name="target" color="secondary" size="lg"/>
        <caption>Mínimo: 100</caption>
      </box>
    </grid-item>
  </grid>
  <caption>Ejemplo con un mes inicial de gracia. La duración exacta todavía debe definirse.</caption>
</box>

---

## Fase 3 — Configuración del período de gracia

<text color="secondary" weight="medium" size="xs">Pregunta 67 — ¿Cuánto puede durar el período de gracia?</text>

Ya definimos que el superadministrador puede otorgar un período de gracia al incorporar un nuevo club o disciplina.

Ahora necesitamos establecer cómo se configura su duración.

**¿Qué opciones debería tener el superadministrador?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Un mes fijo**

      El sistema permite eximir únicamente el primer mes calendario del contrato.
    </radio>
    <radio value="b">
      **Opción B — Cantidad de meses configurable**

      El superadministrador establece una cantidad de meses completos de gracia, por ejemplo 1, 2 o 3, contando el mes de inicio como primero.
    </radio>
    <radio value="c">
      **Opción C — Fecha de finalización configurable (recomendada)**

      El superadministrador establece hasta qué mes calendario se extiende la gracia.

      <text color="secondary" size="xs">Ejemplo: contrato iniciado el 20 de noviembre, con gracia hasta el 31 de enero. El mínimo comienza a exigirse en febrero.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 67 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"el período de gracia comercial dura únicamente el primer mes calendario",b:"el superadministrador configura una cantidad de meses de gracia contando el mes de inicio",c:"el superadministrador configura el mes calendario hasta el cual se extiende el período de gracia"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-067

### Registro 195 — Fecha de finalización del período de gracia
Fuente: `68afb56b-d05a-457b-aac7-b3b3ac6e36a9`

Respuesta previa del usuario:

Para la pregunta 67 del relevamiento SDD elijo la opción C: el superadministrador configura el mes calendario hasta el cual se extiende el período de gracia.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 67 — Confirmada**
</row>

El superadministrador podrá definir **hasta qué mes calendario se extiende el período de gracia** de cada contrato comercial.

## Registro SDD — Decisión 067

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-067 — Fecha de finalización del período de gracia**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-452:** El superadministrador podrá configurar el mes y año de finalización del período de gracia.
  - **RN-453:** El período de gracia incluirá el mes de finalización seleccionado.
  - **RN-454:** Durante los meses cubiertos no se generarán penalizaciones por incumplimiento del mínimo comercial.
  - **RN-455:** La exigencia del mínimo comenzará el primer día del mes siguiente al último mes de gracia.
  - **RN-456:** Cada contrato comercial tendrá su propia configuración independiente.
  - **RN-457:** Los pagos online procesados durante la gracia seguirán registrándose para estadísticas y reportes.
  - **RN-458:** El sistema conservará el historial de configuración y modificaciones.
  - **RN-459:** El vencimiento del período de gracia será automático, sin requerir intervención del superadministrador.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <text color="secondary" size="xs">Contrato iniciado el 20 de noviembre de 2026, con gracia hasta enero de 2027 inclusive.</text>
  <grid columns={4} gap={1}>
    {#each [{mes:"Nov",estado:"Gracia"},{mes:"Dic",estado:"Gracia"},{mes:"Ene",estado:"Gracia"},{mes:"Feb",estado:"Exigible"}] as item}
      <grid-item>
        <box background="surface-secondary" radius="md" padding={2} align="center" gap={1}>
          <text weight="medium" size="sm">{item.mes}</text>
          <icon name={item.estado==="Gracia"?"shield-check":"calendar-check"} color={item.estado==="Gracia"?"success":"default"}/>
          <caption>{item.estado}</caption>
        </box>
      </grid-item>
    {/each}
  </grid>
  A partir de febrero de 2027, el sistema comenzará a evaluar el mínimo contratado y calculará penalizaciones si corresponde.
</box>

---

## Fase 3 — Modificaciones de contratos comerciales

<text color="secondary" weight="medium" size="xs">Pregunta 68 — ¿Se puede extender un período de gracia existente?</text>

Supongamos que Obras Futsal tiene gracia hasta enero de 2027, pero en enero decidís extenderla hasta marzo porque todavía está incorporando jugadores.

**¿Debería el superadministrador poder modificar la fecha de finalización del período de gracia?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — No se puede modificar**

      La fecha de finalización queda fija al crear el contrato.
    </radio>
    <radio value="b">
      **Opción B — Se puede extender, pero no acortar**

      El superadministrador puede otorgar más meses de gracia, pero no reducir los meses ya concedidos.
    </radio>
    <radio value="c">
      **Opción C — Se puede extender o acortar (recomendada)**

      El superadministrador puede modificar la fecha de finalización mientras el período de gracia siga vigente.

      <text color="secondary" size="xs">Los cambios afectan al mes actual y a los siguientes, sin alterar liquidaciones de meses anteriores ya cerrados. Se registra toda modificación.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 68 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"el período de gracia no puede modificarse una vez configurado",b:"el superadministrador puede extender el período de gracia, pero no acortarlo",c:"el superadministrador puede extender o acortar un período de gracia vigente, con auditoría y sin alterar meses anteriores cerrados"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-068

### Registro 197 — Modificación del período de gracia
Fuente: `13a07e62-776e-4138-82ff-a8a648ea1d5e`

Respuesta previa del usuario:

Para la pregunta 68 del relevamiento SDD elijo la opción C: el superadministrador puede extender o acortar un período de gracia vigente, con auditoría y sin alterar meses anteriores cerrados.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 68 — Confirmada**
</row>

El superadministrador podrá **extender o acortar un período de gracia comercial mientras esté vigente**, sin modificar las liquidaciones de meses anteriores ya cerrados.

## Registro SDD — Decisión 068

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-068 — Modificación del período de gracia**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-460:** El superadministrador podrá extender o acortar un período de gracia vigente.
  - **RN-461:** La modificación se realizará cambiando el mes y año de finalización.
  - **RN-462:** Los cambios afectarán el mes calendario actual y los meses futuros.
  - **RN-463:** Si se extiende la gracia para incluir el mes actual, ese mes quedará exento de penalización.
  - **RN-464:** Si se acorta y el mes actual deja de estar cubierto, se aplicará el mínimo comercial correspondiente.
  - **RN-465:** No se recalcularán automáticamente liquidaciones de meses anteriores ya cerrados.
  - **RN-466:** Se conservarán la fecha anterior, la nueva, el momento de modificación y el superadministrador responsable.
  - **RN-467:** Las modificaciones no afectarán los contratos de otros clubes o disciplinas.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    <text color="secondary" size="sm">Finalización original</text>
    **Enero 2027**
  </row>
  <row align="center" justify="between">
    <text color="secondary" size="sm">Nueva finalización</text>
    <text color="success" weight="medium">Marzo 2027</text>
  </row>
  <divider color="subtle"/>
  <grid columns={4} gap={1}>
    {#each [{mes:"Enero",estado:"Gracia"},{mes:"Febrero",estado:"Gracia"},{mes:"Marzo",estado:"Gracia"},{mes:"Abril",estado:"Exigible"}] as item}
      <grid-item>
        <box background="surface-secondary" radius="md" padding={2} align="center" gap={1}>
          <text size="xs" weight="medium">{item.mes}</text>
          <icon name={item.estado==="Gracia"?"shield-check":"calendar-check"} color={item.estado==="Gracia"?"success":"default"}/>
          <caption>{item.estado}</caption>
        </box>
      </grid-item>
    {/each}
  </grid>
  <caption>Ejemplo de extensión realizada antes de finalizar enero. El mínimo comienza a exigirse en abril.</caption>
</box>

---

## Fase 3 — Contratos comerciales

<text color="secondary" weight="medium" size="xs">Pregunta 69 — ¿Qué sucede cuando termina un contrato comercial?</text>

Supongamos que Obras Futsal deja de tener un contrato comercial activo con tu plataforma, pero todavía tiene jugadores, cuotas pendientes y pagos históricos.

Necesitamos definir cómo se comportará el sistema cuando finalice ese contrato.

**¿Qué debería ocurrir?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Suspensión automática**

      Cuando finaliza el contrato, el sistema suspende automáticamente la disciplina, conservando el historial.
    </radio>
    <radio value="b">
      **Opción B — Continúa funcionando sin control comercial**

      La disciplina sigue operando normalmente, pero deja de estar sujeta al mínimo de pagos y a nuevas penalizaciones.
    </radio>
    <radio value="c">
      **Opción C — Alerta y decisión manual del superadministrador (recomendada)**

      Al finalizar el contrato, el sistema alerta al superadministrador, quien decide si renueva el contrato, suspende comercialmente la disciplina o permite su funcionamiento temporal.

      <text color="secondary" size="xs">Mientras no tome una decisión, la disciplina continúa funcionando con las condiciones comerciales existentes. Toda decisión queda auditada.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 69 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"al finalizar el contrato comercial se suspende automáticamente la disciplina",b:"al finalizar el contrato la disciplina continúa funcionando sin mínimos ni nuevas penalizaciones",c:"al finalizar el contrato se alerta al superadministrador, quien decide renovar, suspender o permitir funcionamiento temporal; mientras tanto continúan las condiciones existentes"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-069

### Registro 199 — Vencimiento de contratos comerciales
Fuente: `d42aae2a-a840-4ff6-b9d9-73ccb0be1a52`

Respuesta previa del usuario:

Para la pregunta 69 del relevamiento SDD elijo la opción C: al finalizar el contrato se alerta al superadministrador, quien decide renovar, suspender o permitir funcionamiento temporal; mientras tanto continúan las condiciones existentes.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 69 — Confirmada**
</row>

Cuando finalice un contrato comercial, **el sistema no suspenderá automáticamente al club o disciplina**. Alertará al superadministrador, quien decidirá cómo continuar.

## Registro SDD — Decisión 069

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-069 — Vencimiento de contratos comerciales**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-468:** Cada contrato comercial podrá tener una fecha de finalización.
  - **RN-469:** Al alcanzar esa fecha, el sistema notificará al superadministrador.
  - **RN-470:** El vencimiento no provocará una suspensión automática.
  - **RN-471:** El superadministrador podrá renovar el contrato, suspender comercialmente al responsable o autorizar su funcionamiento temporal.
  - **RN-472:** Hasta que se tome una decisión, continuarán vigentes las condiciones comerciales anteriores.
  - **RN-473:** Durante esa continuidad, se seguirán contabilizando pagos y evaluando mínimos comerciales.
  - **RN-474:** Una suspensión comercial respetará las reglas ya definidas para cuotas pendientes, historial y reactivación.
  - **RN-475:** Las decisiones afectarán exclusivamente al club o disciplina responsable según el contrato.
  - **RN-476:** Se registrarán la decisión, fecha, responsable y antecedentes contractuales para auditoría.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    <box gap={1}>
      **Contrato comercial**
      <text color="secondary" size="xs">Vencimiento: 30/11/2026</text>
    </box>
    <badge color="warning">Requiere decisión</badge>
  </row>
  <list marker="number" gap={2}>
    <list-item>El sistema detecta el vencimiento y alerta al superadministrador.</list-item>
    <list-item>Obras Futsal continúa funcionando con las condiciones comerciales existentes.</list-item>
    <list-item>El superadministrador decide renovar, suspender o permitir continuidad temporal.</list-item>
  </list>
</box>

---

## Fase 3 — Renovación de contratos comerciales

<text color="secondary" weight="medium" size="xs">Pregunta 70 — ¿Cómo se renueva un contrato?</text>

Supongamos que el contrato de Obras Futsal vence el 30 de noviembre y decidís renovarlo por otro año.

Es posible que quieras conservar las condiciones actuales o negociar un nuevo mínimo de pagos y otros importes de penalización.

**¿Cómo debería funcionar la renovación?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Extender el contrato existente**

      Se modifica únicamente su fecha de finalización. Las condiciones comerciales permanecen iguales.
    </radio>
    <radio value="b">
      **Opción B — Crear siempre un contrato nuevo**

      Cada renovación requiere cargar un contrato desde cero, con sus nuevas fechas y condiciones comerciales.
    </radio>
    <radio value="c">
      **Opción C — Renovar copiando las condiciones anteriores (recomendada)**

      El sistema prepara una nueva versión del contrato con las condiciones anteriores, y el superadministrador puede conservarlas o modificarlas antes de confirmar.

      <text color="secondary" size="xs">Se mantiene el historial de contratos y versiones anteriores, sin perder la trazabilidad comercial.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 70 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"renovar extendiendo la fecha de finalización del contrato existente sin modificar condiciones",b:"crear siempre un contrato nuevo desde cero para cada renovación",c:"renovar creando una nueva versión que copie las condiciones anteriores y permita modificarlas antes de confirmar, conservando historial"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-070

### Registro 201 — Renovación y versionado de contratos
Fuente: `18bf97f2-eefa-416c-adf6-70ef4d3c86e9`

Respuesta previa del usuario:

C

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 70 — Confirmada: opción C**
</row>

Al renovar un contrato comercial, el sistema creará una **nueva versión con las condiciones del contrato anterior**, permitiendo al superadministrador modificarlas antes de confirmar la renovación.

## Registro SDD — Decisión 070

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-070 — Renovación y versionado de contratos**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-477:** Cada renovación generará una nueva versión del contrato comercial.
  - **RN-478:** La nueva versión copiará automáticamente las condiciones del contrato anterior.
  - **RN-479:** El superadministrador podrá modificar mínimos, penalizaciones y fechas antes de confirmar.
  - **RN-480:** La nueva versión permanecerá como borrador hasta su confirmación.
  - **RN-481:** Las versiones anteriores conservarán sus condiciones e historial.
  - **RN-482:** Las liquidaciones históricas mantendrán la referencia a las condiciones utilizadas para calcularlas.
  - **RN-483:** Se registrarán la fecha de renovación y el superadministrador responsable.
  - **RN-484:** No se permitirá que dos versiones del mismo contrato tengan períodos de vigencia superpuestos.
</box>

## Ejemplo — Obras Futsal

| Condición | Contrato anterior | Renovación |
|---|---|---|
| Mínimo mensual | 100 pagos | 80 pagos |
| Penalización | $500 por faltante | $700 por faltante |
| Vencimiento | 30/11/2026 | 30/11/2027 |
| Estado | Histórico | Nueva versión |

Los valores de la renovación son ilustrativos.

---

## Fase 3 — Vigencia de contratos comerciales

<text color="secondary" weight="medium" size="xs">Pregunta 71 — ¿Desde cuándo comienza a regir una renovación?</text>

Supongamos que el contrato de Obras Futsal vence el 30 de noviembre, pero el superadministrador confirma su renovación recién el 10 de diciembre.

La nueva versión tiene un mínimo de 80 pagos, mientras que la anterior exigía 100.

**¿Qué condiciones deberían aplicarse durante diciembre?**

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Desde la fecha de confirmación**

      Las nuevas condiciones comienzan el 10 de diciembre. Antes de esa fecha continúan las anteriores.
    </radio>
    <radio value="b">
      **Opción B — Desde el inicio del mes de renovación (recomendada)**

      Las nuevas condiciones se aplican a diciembre completo, aunque la renovación se confirme el día 10.

      <text color="secondary" size="xs">Mantiene el criterio mensual que ya definimos para los cambios comerciales.</text>
    </radio>
    <radio value="c">
      **Opción C — Fecha de inicio configurable**

      El superadministrador establece desde qué mes calendario comienza a regir la nueva versión, pudiendo programarla para un mes futuro o el actual.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 71 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"las condiciones renovadas comienzan en la fecha de confirmación",b:"las condiciones renovadas se aplican al mes calendario de renovación completo",c:"el superadministrador configura el mes calendario desde el que rige la nueva versión"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-071

### Registro 203 — Inicio de vigencia de renovaciones
Fuente: `5b20da80-41d8-4e9b-8355-c3b0a11d2453`

Respuesta previa del usuario:

C

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 71 — Confirmada: opción C**
</row>

El superadministrador podrá definir **desde qué mes calendario comienza a regir la renovación de un contrato comercial**, pudiendo seleccionar el mes actual o uno futuro.

## Registro SDD — Decisión 071

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-071 — Inicio de vigencia de renovaciones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-485:** El superadministrador configurará el mes y año de inicio de cada renovación.
  - **RN-486:** La nueva versión aplicará sus condiciones al mes de inicio completo.
  - **RN-487:** Se permitirá programar renovaciones para meses futuros.
  - **RN-488:** Hasta el inicio de la nueva versión, continuarán aplicándose las condiciones comerciales anteriores, cuando corresponda.
  - **RN-489:** No podrán existir dos versiones vigentes simultáneamente para un mismo contrato.
  - **RN-490:** Se conservarán las fechas de vigencia y condiciones de cada versión.
  - **RN-491:** Las liquidaciones de períodos anteriores ya cerrados no se recalcularán automáticamente.
  - **RN-492:** Se registrarán el superadministrador responsable y la fecha de confirmación.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <text color="secondary" size="xs">Renovación confirmada el 10 de diciembre de 2026, con inicio configurado para enero de 2027.</text>
  <grid columns={3} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} align="center" gap={1}>
        **Diciembre**
        <title size="lg">100</title>
        <caption>Mínimo anterior</caption>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} align="center" gap={1}>
        **Enero**
        <title size="lg" color="success">80</title>
        <caption>Nuevo mínimo</caption>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} align="center" gap={1}>
        **Febrero**
        <title size="lg" color="success">80</title>
        <caption>Nuevo mínimo</caption>
      </box>
    </grid-item>
  </grid>
</box>

---

## Fase 3 — Contratos comerciales

<text color="secondary" weight="medium" size="xs">Pregunta 72 — ¿Qué sucede si se modifica una renovación programada?</text>

Supongamos que el 10 de diciembre programás una renovación de Obras Futsal para comenzar en febrero de 2027.

Pero en enero necesitás cambiar el mínimo de 80 a 100 pagos mensuales, antes de que la nueva versión entre en vigencia.

**¿Debería poder modificarse una renovación ya confirmada pero todavía no vigente?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — No se puede modificar**

      Una renovación confirmada queda bloqueada, aunque todavía no haya comenzado.
    </radio>
    <radio value="b">
      **Opción B — Cancelar y crear otra renovación**

      El superadministrador debe cancelar la versión programada y generar una nueva.
    </radio>
    <radio value="c">
      **Opción C — Modificarla antes de su vigencia (recomendada)**

      El superadministrador puede editar las condiciones de una renovación programada mientras no haya entrado en vigencia.

      <text color="secondary" size="xs">Se conserva el historial de modificaciones, quién las realizó y cuándo. Una vez vigente, se aplican las reglas normales de cambios comerciales.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 72 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"una renovación confirmada no puede modificarse aunque todavía no esté vigente",b:"para modificar una renovación programada hay que cancelarla y crear otra",c:"el superadministrador puede modificar una renovación programada antes de su vigencia, conservando auditoría"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-072

### Registro 205 — Modificación de renovaciones programadas
Fuente: `342d0e02-74a1-4335-9400-c2170bce9fdb`

Respuesta previa del usuario:

Para la pregunta 72 del relevamiento SDD elijo la opción C: el superadministrador puede modificar una renovación programada antes de su vigencia, conservando auditoría.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 72 — Confirmada: opción C**
</row>

El superadministrador podrá **modificar una renovación comercial ya confirmada mientras todavía no haya entrado en vigencia**, conservando un historial completo de los cambios.

## Registro SDD — Decisión 072

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-072 — Modificación de renovaciones programadas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-493:** El superadministrador podrá editar renovaciones confirmadas cuya fecha de inicio sea futura.
  - **RN-494:** Podrá modificar el mínimo mensual de pagos, modalidad e importe de penalización y demás condiciones comerciales.
  - **RN-495:** Las modificaciones no afectarán las condiciones del contrato actualmente vigente.
  - **RN-496:** Cada modificación conservará el valor anterior y el nuevo.
  - **RN-497:** Se registrarán la fecha, hora y el superadministrador responsable.
  - **RN-498:** Al comenzar el mes de vigencia, se aplicará la última configuración confirmada.
  - **RN-499:** Una vez vigente la renovación, se aplicarán las reglas ordinarias de modificaciones comerciales.
  - **RN-500:** Se mantendrá la restricción que impide superponer períodos de vigencia de versiones contractuales.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Renovación febrero 2027**
    <badge color="info">Programada</badge>
  </row>
  <table>
    <table-row><table-cell>Condición</table-cell><table-cell>Original</table-cell><table-cell>Modificada</table-cell></table-row>
    <table-row><table-cell>Mínimo mensual</table-cell><table-cell>80 pagos</table-cell><table-cell>**100 pagos**</table-cell></table-row>
    <table-row><table-cell>Penalización</table-cell><table-cell>$500</table-cell><table-cell>**$700**</table-cell></table-row>
    <table-row><table-cell>Inicio</table-cell><table-cell>Febrero 2027</table-cell><table-cell>Febrero 2027</table-cell></table-row>
  </table>
  <caption>La versión vigente no cambia. La nueva configuración comenzará a aplicarse en febrero.</caption>
</box>

---

## Fase 3 — Gestión de contratos comerciales

<text color="secondary" weight="medium" size="xs">Pregunta 73 — Cancelación de renovaciones programadas</text>

Supongamos que programaste una renovación de Obras Futsal para comenzar en febrero de 2027, pero en enero el club decide no continuar con el nuevo acuerdo.

La renovación ya está confirmada, aunque todavía no entró en vigencia.

**¿Qué debería permitir el sistema?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — No permitir cancelaciones**

      Una renovación confirmada no puede cancelarse. El superadministrador deberá esperar a que entre en vigencia para gestionar su finalización.
    </radio>
    <radio value="b">
      **Opción B — Cancelar sin justificación**

      El superadministrador podrá cancelar una renovación futura y dejarla sin efecto, sin necesidad de indicar un motivo.
    </radio>
    <radio value="c">
      **Opción C — Cancelar con justificación y auditoría (recomendada)**

      El superadministrador podrá cancelar una renovación programada antes de su inicio, indicando obligatoriamente el motivo.

      <text color="secondary" size="xs">La versión cancelada permanecerá en el historial, nunca entrará en vigencia y el contrato actual continuará bajo las reglas ya definidas para su vencimiento.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 73 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"no permitir cancelar renovaciones confirmadas",b:"permitir cancelar renovaciones programadas sin justificación",c:"permitir cancelar renovaciones programadas antes de su inicio, con motivo obligatorio y auditoría"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-073

### Registro 207 — Cancelación de renovaciones programadas
Fuente: `3a694de0-0b31-4563-ba8e-6212da0f331c`

Respuesta previa del usuario:

Para la pregunta 73 del relevamiento SDD elijo la opción C: permitir cancelar renovaciones programadas antes de su inicio, con motivo obligatorio y auditoría.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 73 — Confirmada: opción C**
</row>

El superadministrador podrá **cancelar una renovación comercial programada antes de que entre en vigencia**, siempre que indique un motivo. La cancelación quedará registrada para auditoría.

## Registro SDD — Decisión 073

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-073 — Cancelación de renovaciones programadas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-501:** Solo el superadministrador podrá cancelar renovaciones comerciales programadas.
  - **RN-502:** La cancelación estará permitida únicamente antes del inicio de vigencia de la renovación.
  - **RN-503:** Será obligatorio ingresar una justificación.
  - **RN-504:** La renovación pasará al estado *Cancelada*.
  - **RN-505:** Una renovación cancelada nunca entrará en vigencia automáticamente.
  - **RN-506:** Se conservarán las condiciones originales de la renovación cancelada.
  - **RN-507:** Se registrarán fecha, hora, motivo y superadministrador responsable.
  - **RN-508:** La cancelación no modificará el contrato actualmente vigente.
  - **RN-509:** Al finalizar el contrato actual, se aplicarán las reglas de vencimiento y decisión manual previamente aprobadas.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Renovación febrero 2027**
    <badge color="danger">Cancelada</badge>
  </row>
  <text color="secondary" size="xs">Renovación originalmente programada para el 01/02/2027.</text>
  <box background="surface-secondary" radius="md" padding={3} gap={1}>
    <text color="secondary" size="xs" weight="medium">Motivo registrado</text>
    <text size="sm">El club decidió renegociar las condiciones comerciales antes de continuar.</text>
  </box>
  <row align="center" gap={2}>
    <icon name="history" color="secondary"/>
    <text size="sm">Se conserva el historial y la renovación no se activa.</text>
  </row>
</box>

---

## Fase 3 — Continuidad comercial

<text color="secondary" weight="medium" size="xs">Pregunta 74 — ¿Cómo se controla el funcionamiento temporal después del vencimiento?</text>

Ya definimos que, cuando vence un contrato, el superadministrador puede permitir que el club o disciplina siga funcionando temporalmente.

Supongamos que el contrato de Obras Futsal venció el 30 de noviembre y decidís permitirle continuar mientras negocian una renovación.

**¿Cómo debería administrarse esa autorización temporal?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Sin fecha límite**

      El superadministrador habilita la continuidad temporal, que permanece activa hasta que decida renovarla o suspenderla.
    </radio>
    <radio value="b">
      **Opción B — Fecha límite con suspensión automática**

      El superadministrador establece una fecha de finalización. Si no se renueva el contrato antes de esa fecha, el sistema suspende automáticamente al responsable comercial.
    </radio>
    <radio value="c">
      **Opción C — Fecha límite con alerta y decisión manual (recomendada)**

      El superadministrador establece hasta cuándo autoriza la continuidad temporal.

      <text color="secondary" size="xs">Al llegar la fecha, el sistema genera una nueva alerta, pero no suspende automáticamente. El superadministrador decide renovar, extender la autorización o suspender. Mientras tanto, siguen las condiciones comerciales anteriores.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 74 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"la continuidad temporal no tiene fecha límite y dura hasta una decisión manual",b:"la continuidad temporal tiene fecha límite y al vencer se suspende automáticamente",c:"la continuidad temporal tiene fecha límite configurable y al vencer se alerta al superadministrador para una nueva decisión manual, sin suspensión automática"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-074

### Registro 209 — Control de continuidad comercial temporal
Fuente: `5afe085a-c451-4874-ba64-45fe2df9b339`

Respuesta previa del usuario:

Para la pregunta 74 del relevamiento SDD elijo la opción C: la continuidad temporal tiene fecha límite configurable y al vencer se alerta al superadministrador para una nueva decisión manual, sin suspensión automática.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 74 — Confirmada: opción C**
</row>

La continuidad temporal de un contrato comercial tendrá una **fecha límite configurable**. Cuando llegue esa fecha, el sistema alertará al superadministrador, pero **nunca suspenderá automáticamente** al club o disciplina.

## Registro SDD — Decisión 074

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-074 — Control de continuidad comercial temporal**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-510:** Solo el superadministrador podrá autorizar la continuidad temporal de un contrato vencido.
  - **RN-511:** Cada autorización tendrá una fecha límite configurable.
  - **RN-512:** Durante la continuidad temporal se mantendrán las condiciones comerciales anteriores.
  - **RN-513:** Se seguirán contabilizando pagos online y evaluando los mínimos comerciales correspondientes.
  - **RN-514:** Al alcanzar la fecha límite, el sistema generará una alerta para el superadministrador.
  - **RN-515:** El vencimiento de la autorización no provocará una suspensión automática.
  - **RN-516:** El superadministrador podrá renovar el contrato, extender la autorización temporal o suspender comercialmente al responsable.
  - **RN-517:** Mientras no exista una nueva decisión, la operación continuará bajo las condiciones comerciales existentes.
  - **RN-518:** Cada autorización, extensión y decisión conservará fecha, responsable e historial de auditoría.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Continuidad comercial**
    <badge color="warning">Temporal</badge>
  </row>
  <list marker="number" gap={2}>
    <list-item>El contrato vence el **30/11/2026**.</list-item>
    <list-item>El superadministrador autoriza la continuidad hasta el **31/01/2027**.</list-item>
    <list-item>El 31 de enero, el sistema genera una alerta para revisar la situación.</list-item>
    <list-item>Hasta que se tome una decisión, Obras Futsal continúa operando normalmente.</list-item>
  </list>
</box>

---

## Fase 3 — Alertas comerciales

<text color="secondary" weight="medium" size="xs">Pregunta 75 — ¿Cuándo avisar que un contrato está por vencer?</text>

Ya definimos qué sucede cuando vence un contrato, pero sería útil que el superadministrador pudiera anticiparse y negociar una renovación antes de llegar a esa fecha.

**¿Cómo querés que funcionen las alertas previas al vencimiento?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Aviso fijo 7 días antes**

      El sistema genera una alerta cuando faltan 7 días para el vencimiento.
    </radio>
    <radio value="b">
      **Opción B — Avisos fijos a los 30, 15 y 7 días**

      El sistema genera tres alertas antes del vencimiento para facilitar el seguimiento comercial.
    </radio>
    <radio value="c">
      **Opción C — Anticipación configurable (recomendada)**

      El superadministrador configura con cuántos días de anticipación desea recibir alertas, por ejemplo 30, 15 y 7 días.

      <text color="secondary" size="xs">La configuración se aplica a los vencimientos de contratos y autorizaciones temporales, evitando notificaciones duplicadas.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 75 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"generar una alerta fija 7 días antes del vencimiento",b:"generar alertas fijas a los 30, 15 y 7 días del vencimiento",c:"permitir al superadministrador configurar los días de anticipación de alertas para contratos y continuidades temporales"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-075

### Registro 211 — Alertas anticipadas de vencimiento
Fuente: `90bbcb38-b542-4512-86aa-5eb498ab9a46`

Respuesta previa del usuario:

Para la pregunta 75 del relevamiento SDD elijo la opción B: generar alertas fijas a los 30, 15 y 7 días del vencimiento.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 75 — Confirmada: opción B**
</row>

El sistema generará **alertas automáticas a los 30, 15 y 7 días antes del vencimiento** de los contratos comerciales y de las autorizaciones de continuidad temporal.

Estos plazos serán fijos, sin necesidad de configurarlos individualmente.

## Registro SDD — Decisión 075

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-075 — Alertas anticipadas de vencimiento**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-519:** El sistema generará alertas a los 30, 15 y 7 días calendario antes del vencimiento.
  - **RN-520:** Las alertas aplicarán a contratos comerciales y autorizaciones de continuidad temporal.
  - **RN-521:** El destinatario será el superadministrador.
  - **RN-522:** Los plazos serán fijos para todos los contratos.
  - **RN-523:** Cada alerta identificará al club o disciplina responsable, el contrato y la fecha de vencimiento.
  - **RN-524:** No se generarán alertas duplicadas para un mismo vencimiento y plazo.
  - **RN-525:** Si el contrato se renueva o la autorización se modifica, se actualizará la programación de alertas pendientes.
  - **RN-526:** Las alertas generadas quedarán registradas para seguimiento y auditoría.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={3}>
  <text color="secondary" size="xs">Contrato con vencimiento el 30 de noviembre de 2026.</text>
  <list marker="number" connector="solid" gap={3}>
    <list-item>
      **31 de octubre — Primera alerta**

      <text color="secondary" size="sm">Faltan 30 días para el vencimiento.</text>
    </list-item>
    <list-item>
      **15 de noviembre — Segunda alerta**

      <text color="secondary" size="sm">Faltan 15 días para el vencimiento.</text>
    </list-item>
    <list-item>
      **23 de noviembre — Última alerta previa**

      <text color="secondary" size="sm">Faltan 7 días para el vencimiento.</text>
    </list-item>
  </list>
</box>

---

## Fase 3 — Notificaciones comerciales

<text color="secondary" weight="medium" size="xs">Pregunta 76 — ¿Por dónde recibe las alertas el superadministrador?</text>

Ya definimos cuándo se generan las alertas. Ahora necesitamos establecer cómo se notifican.

Por ejemplo, cuando falten 7 días para que venza el contrato de Obras Futsal, **¿dónde querés recibir el aviso?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo dentro de la plataforma**

      Las alertas aparecen en el panel del superadministrador, con un centro de notificaciones.
    </radio>
    <radio value="b">
      **Opción B — Panel y correo electrónico**

      Las alertas aparecen en el panel y también se envían automáticamente al correo del superadministrador.
    </radio>
    <radio value="c">
      **Opción C — Panel, correo y preferencias configurables (recomendada)**

      Todas las alertas quedan registradas en la plataforma, y el superadministrador puede activar o desactivar los avisos por correo electrónico.

      <text color="secondary" size="xs">El historial de notificaciones siempre estará disponible, aunque se desactiven los correos.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 76 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"las alertas comerciales se muestran solamente en el panel del superadministrador",b:"las alertas comerciales se muestran en el panel y se envían por correo electrónico",c:"las alertas siempre se registran en el panel y el superadministrador puede configurar si también recibe correos electrónicos"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-076

### Registro 213 — Canal de notificaciones comerciales
Fuente: `0c14ce12-9186-491d-b794-d10eb7ffc9b3`

Respuesta previa del usuario:

Para la pregunta 76 del relevamiento SDD elijo la opción A: las alertas comerciales se muestran solamente en el panel del superadministrador.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 76 — Confirmada: opción A**
</row>

Las alertas comerciales se mostrarán **únicamente dentro del panel del superadministrador**. No se enviarán correos electrónicos.

## Registro SDD — Decisión 076

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-076 — Canal de notificaciones comerciales**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-527:** Todas las alertas comerciales se mostrarán en el panel del superadministrador.
  - **RN-528:** No se enviarán notificaciones comerciales por correo electrónico.
  - **RN-529:** El panel tendrá un centro de notificaciones comerciales.
  - **RN-530:** Cada notificación identificará al club o disciplina, el motivo y la fecha de vencimiento.
  - **RN-531:** Las alertas de 30, 15 y 7 días se generarán automáticamente según lo aprobado en DEC-075.
  - **RN-532:** Las notificaciones conservarán su fecha de generación.
  - **RN-533:** El sistema evitará generar notificaciones duplicadas.
  - **RN-534:** El historial de notificaciones permanecerá disponible para consulta.
</box>

## Ejemplo — Panel del superadministrador

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    <row align="center" gap={2}>
      <icon name="bell" size="lg"/>
      **Notificaciones comerciales**
    </row>
    <badge>3 avisos</badge>
  </row>
  {#each [{name:"Obras Futsal",desc:"Contrato próximo a vencer",time:"7 días",icon:"calendar-clock"},{name:"Obras Básquet",desc:"Contrato próximo a vencer",time:"15 días",icon:"calendar-clock"},{name:"Club Municipal",desc:"Continuidad temporal próxima a vencer",time:"30 días",icon:"clock"}] as item,i}
    <box gap={2}>
      {#if i>0}<divider color="subtle"/>{/if}
      <row align="center" gap={3}>
        <icon name={item.icon} color="secondary" size="lg"/>
        <box flex="1" gap={1}>
          <text weight="medium" size="sm">{item.name}</text>
          <text color="secondary" size="xs">{item.desc}</text>
        </box>
        <badge color="warning">{item.time}</badge>
      </row>
    </box>
  {/each}
  <caption>Vista ilustrativa del centro de notificaciones.</caption>
</box>

---

## Fase 3 — Gestión de notificaciones

<text color="secondary" weight="medium" size="xs">Pregunta 77 — ¿Cómo se administran las notificaciones comerciales?</text>

Supongamos que recibís una alerta indicando que el contrato de Obras Futsal vence en 7 días.

Entrás al panel, revisás la situación y comenzás a negociar la renovación.

**¿Qué debería permitirte hacer el sistema con esa notificación?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo visualizar**

      Las notificaciones aparecen en el panel y desaparecen automáticamente cuando deja de existir el motivo.
    </radio>
    <radio value="b">
      **Opción B — Marcar como leída**

      Podés marcar una notificación como leída para que deje de aparecer como pendiente, pero permanece en el historial.
    </radio>
    <radio value="c">
      **Opción C — Leída y resuelta (recomendada)**

      Podés marcar una notificación como leída y, por separado, marcarla como resuelta cuando hayas gestionado el asunto.

      <text color="secondary" size="xs">La notificación permanece en el historial con su estado, fecha y responsable. Marcarla como resuelta no renueva ni suspende automáticamente el contrato.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 77 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"las notificaciones comerciales son solo informativas y desaparecen cuando deja de existir el motivo",b:"el superadministrador puede marcar las notificaciones como leídas, conservando historial",c:"el superadministrador puede marcar las notificaciones como leídas y resueltas por separado, conservando historial y sin ejecutar acciones contractuales automáticamente"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-077

### Registro 215 — Comportamiento de notificaciones comerciales
Fuente: `08e3b6fd-b8eb-497d-a254-97df043a5da2`

Respuesta previa del usuario:

Para la pregunta 77 del relevamiento SDD elijo la opción A: las notificaciones comerciales son solo informativas y desaparecen cuando deja de existir el motivo.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 77 — Confirmada: opción A**
</row>

Las notificaciones comerciales serán **únicamente informativas** y desaparecerán automáticamente del panel cuando deje de existir el motivo que las generó.

No será necesario marcarlas como leídas o resueltas.

## Registro SDD — Decisión 077

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-077 — Comportamiento de notificaciones comerciales**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-535:** Las notificaciones comerciales tendrán carácter exclusivamente informativo.
  - **RN-536:** No existirán acciones manuales para marcarlas como leídas o resueltas.
  - **RN-537:** Las alertas permanecerán visibles mientras exista la situación comercial que las origina.
  - **RN-538:** Cuando se resuelva la situación, las alertas relacionadas desaparecerán automáticamente de la vista activa.
  - **RN-539:** La renovación de un contrato actualizará las alertas asociadas a su vencimiento anterior.
  - **RN-540:** La extensión de una continuidad temporal actualizará las alertas según su nueva fecha límite.
  - **RN-541:** La desaparición de una alerta no ejecutará ninguna modificación contractual.
  - **RN-542:** Se conservará un registro técnico de las alertas generadas para auditoría, aunque ya no aparezcan en el panel.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    <row align="center" gap={2}>
      <icon name="bell" color="secondary"/>
      **Contrato próximo a vencer**
    </row>
    <badge color="warning">7 días</badge>
  </row>
  <text color="secondary" size="sm">Obras Futsal — Vencimiento: 30/11/2026</text>
  <divider color="subtle"/>
  <row align="center" gap={2}>
    <icon name="check-circle-2" color="success"/>
    <text size="sm">Cuando se confirma una renovación que resuelve ese vencimiento, la alerta desaparece automáticamente.</text>
  </row>
</box>

---

## Fase 3 — Panel del superadministrador

<text color="secondary" weight="medium" size="xs">Pregunta 78 — ¿Qué información comercial debe mostrar el panel principal?</text>

Ya definimos contratos, mínimos de pagos, penalizaciones, liquidaciones y alertas.

Ahora necesitamos decidir cómo querés visualizar el estado comercial de toda la plataforma cuando ingresás como superadministrador.

**¿Qué nivel de información debería tener el dashboard principal?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Resumen básico**

      Cantidad de clubes, disciplinas, contratos activos, liquidaciones pendientes y alertas comerciales.
    </radio>
    <radio value="b">
      **Opción B — Resumen financiero y comercial**

      Además del resumen básico, mostrar pagos online procesados, recaudación de la plataforma, penalizaciones pendientes y cumplimiento de mínimos.
    </radio>
    <radio value="c">
      **Opción C — Dashboard completo con filtros (recomendada)**

      Mostrar toda la información anterior, con gráficos de evolución, cumplimiento de mínimos, ingresos por comisiones, penalizaciones y filtros por club, disciplina y período.

      <text color="secondary" size="xs">Permite supervisar el negocio completo y analizar individualmente el rendimiento de cada contrato comercial.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 78 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"dashboard principal con resumen básico de clubes, disciplinas, contratos, liquidaciones y alertas",b:"dashboard con resumen financiero y comercial, pagos, recaudación, penalizaciones y cumplimiento de mínimos",c:"dashboard completo con métricas, gráficos de evolución y filtros por club, disciplina y período"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-078

### Registro 217 — Dashboard global del superadministrador
Fuente: `7e2c7375-e22f-4c0c-ac2b-43fb7eb0dc26`

Respuesta previa del usuario:

Para la pregunta 78 del relevamiento SDD elijo la opción C: dashboard completo con métricas, gráficos de evolución y filtros por club, disciplina y período.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 78 — Confirmada: opción C**
</row>

El superadministrador tendrá un **dashboard comercial y financiero completo**, con métricas, gráficos de evolución y filtros por club, disciplina y período.

## Registro SDD — Decisión 078

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-078 — Dashboard global del superadministrador**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-543:** El dashboard mostrará cantidades de clubes, disciplinas y contratos comerciales.
  - **RN-544:** Mostrará pagos online exitosos, importes procesados e ingresos por comisiones de la plataforma.
  - **RN-545:** Incluirá penalizaciones calculadas, aprobadas, pendientes de aprobación y pendientes de cobro.
  - **RN-546:** Mostrará el cumplimiento de los mínimos comerciales de cada contrato.
  - **RN-547:** Incluirá gráficos de evolución mensual de pagos, ingresos y penalizaciones.
  - **RN-548:** Permitirá filtrar por club, disciplina y período.
  - **RN-549:** Los indicadores y gráficos se actualizarán según los filtros seleccionados.
  - **RN-550:** Se distinguirán los pagos online de los pagos externos.
  - **RN-551:** Se respetará la independencia económica de cada disciplina y su responsable contractual.
  - **RN-552:** El dashboard será exclusivo del superadministrador y permitirá consultar información consolidada de toda la plataforma.
</box>

## Vista conceptual del dashboard

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Resumen comercial**
    <badge>Ejemplo ilustrativo</badge>
  </row>
  <grid columns={2} gap={2}>
    {#each [{label:"Pagos online",value:"1.250",icon:"credit-card"},{label:"Volumen procesado",value:"$25 M",icon:"wallet"},{label:"Ingresos por comisión",value:"$1,25 M",icon:"trending-up"},{label:"Liquidaciones pendientes",value:"8",icon:"file-clock"}] as item}
      <grid-item>
        <box background="surface-secondary" radius="md" padding={3} gap={1}>
          <row align="center" justify="between">
            <text color="secondary" size="xs">{item.label}</text>
            <icon name={item.icon} color="secondary"/>
          </row>
          <title size="xl" tabularNums>{item.value}</title>
        </box>
      </grid-item>
    {/each}
  </grid>
  <text weight="medium" size="sm">Evolución de pagos online</text>
  <Chart content={{"chartType":"bar","meta":{"footer":"Datos ficticios para ilustrar la visualización."},"xKey":"mes","series":[{"dataKey":"pagos","label":"Pagos exitosos"}],"data":[{"mes":"Jun","pagos":760},{"mes":"Jul","pagos":880},{"mes":"Ago","pagos":1020},{"mes":"Sep","pagos":940},{"mes":"Oct","pagos":1120},{"mes":"Nov","pagos":1250}]}}/>
</box>

---

## Fase 3 — Reportes y exportación

<text color="secondary" weight="medium" size="xs">Pregunta 79 — ¿Cómo se exporta la información comercial?</text>

Además de consultar el dashboard, podrías necesitar descargar los datos para analizarlos en Excel, compartirlos con tu socio o utilizarlos para la contabilidad de la plataforma.

**¿Qué opciones de exportación debería ofrecer el sistema?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Sin exportación**

      Toda la información se consulta únicamente dentro del dashboard.
    </radio>
    <radio value="b">
      **Opción B — Exportación a Excel**

      El superadministrador puede descargar archivos Excel con pagos, comisiones, penalizaciones y cumplimiento de mínimos, respetando los filtros aplicados.
    </radio>
    <radio value="c">
      **Opción C — Excel y PDF (recomendada)**

      Permitir descargar Excel para análisis detallado y PDF para informes comerciales, con los filtros seleccionados.

      <text color="secondary" size="xs">Los reportes incluirán el período, club o disciplina, indicadores relevantes y fecha de generación.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 79 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"la información comercial se consulta solo en el dashboard, sin exportación",b:"permitir exportar los datos comerciales a Excel respetando los filtros aplicados",c:"permitir exportar reportes comerciales a Excel y PDF respetando los filtros aplicados"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-079

### Registro 219 — Exportación de reportes comerciales
Fuente: `f80cd4da-e95a-48cd-9816-32fbba458fc2`

Respuesta previa del usuario:

Para la pregunta 79 del relevamiento SDD elijo la opción C: permitir exportar reportes comerciales a Excel y PDF respetando los filtros aplicados.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 79 — Confirmada: opción C**
</row>

El superadministrador podrá **exportar reportes comerciales en Excel y PDF**, respetando los filtros aplicados en el dashboard.

## Registro SDD — Decisión 079

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-079 — Exportación de reportes comerciales**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-553:** El superadministrador podrá exportar reportes comerciales en formato Excel (`.xlsx`).
  - **RN-554:** También podrá exportar reportes en formato PDF.
  - **RN-555:** Las exportaciones respetarán los filtros de club, disciplina y período seleccionados.
  - **RN-556:** Los reportes incluirán métricas de pagos online, importes procesados y comisiones de la plataforma.
  - **RN-557:** Incluirán información sobre penalizaciones y liquidaciones comerciales.
  - **RN-558:** Permitirán consultar el cumplimiento de mínimos comerciales.
  - **RN-559:** Los reportes identificarán el período, los filtros utilizados y la fecha de generación.
  - **RN-560:** Las exportaciones conservarán la separación entre pagos online y pagos externos.
  - **RN-561:** Los importes originales y finales de las penalizaciones se mostrarán diferenciados cuando existan ajustes o condonaciones.
  - **RN-562:** La exportación global estará disponible únicamente para el superadministrador.
</box>

## Ejemplo — Exportación del dashboard

<box border radius="lg" padding={3} gap={3}>
  <row align="center" gap={2}>
    <icon name="file-bar-chart" size="lg"/>
    **Reporte comercial**
  </row>
  <grid columns={2} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Club</text>
        **Obras Mendoza**
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Disciplina</text>
        **Futsal**
      </box>
    </grid-item>
  </grid>
  <text color="secondary" size="sm">Período seleccionado: enero a diciembre de 2026</text>
  <row gap={2}>
    <box flex="1" border radius="md" padding={3} align="center" gap={1}>
      <icon name="file-spreadsheet" color="success" size="lg"/>
      **Excel**
      <caption>Datos y análisis</caption>
    </box>
    <box flex="1" border radius="md" padding={3} align="center" gap={1}>
      <icon name="file-text" color="secondary" size="lg"/>
      **PDF**
      <caption>Informe comercial</caption>
    </box>
  </row>
  <caption>Vista conceptual de los formatos disponibles. No representa una exportación real.</caption>
</box>

---

## Fase 3 — Acceso a reportes

<text color="secondary" weight="medium" size="xs">Pregunta 80 — ¿Los clubes y disciplinas pueden consultar sus propios reportes?</text>

Hasta ahora definimos el dashboard comercial global para el superadministrador.

Pero también podría ser útil que **Obras Futsal consulte sus propias estadísticas**, como cuotas cobradas, pagos pendientes, recaudación y evolución mensual.

**¿Qué acceso a reportes deberían tener los administradores de cada club o disciplina?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo el superadministrador**

      Los clubes y disciplinas no tendrán un módulo de reportes. Únicamente podrán consultar sus operaciones individuales.
    </radio>
    <radio value="b">
      **Opción B — Reportes internos básicos**

      Los administradores de disciplina podrán consultar recaudación, cuotas pagadas y pendientes, y cantidad de jugadores. Los delegados generales podrán ver información consolidada de su club.
    </radio>
    <radio value="c">
      **Opción C — Dashboard completo por nivel de permisos (recomendada)**

      Cada administrador de disciplina tendrá un dashboard con métricas, gráficos, filtros y exportación a Excel y PDF de su propia disciplina.

      <text color="secondary" size="xs">Los delegados generales podrán consultar información consolidada del club y sus disciplinas, sin modificar operaciones. Los datos comerciales globales de la plataforma seguirán siendo exclusivos del superadministrador.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 80 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo el superadministrador tendrá reportes; los demás roles consultarán operaciones individuales",b:"los administradores de disciplina tendrán reportes internos básicos y los delegados generales una vista consolidada del club",c:"cada disciplina tendrá un dashboard completo con gráficos y exportaciones, y los delegados generales una vista consolidada del club, respetando permisos"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-080

### Registro 221 — Dashboards por nivel de permisos
Fuente: `5507f560-3a42-4f4b-b4f1-c493f386cd38`

Respuesta previa del usuario:

Para la pregunta 80 del relevamiento SDD elijo la opción C: cada disciplina tendrá un dashboard completo con gráficos y exportaciones, y los delegados generales una vista consolidada del club, respetando permisos.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 80 — Confirmada: opción C**
</row>

Cada disciplina tendrá su propio dashboard completo, mientras que los delegados generales podrán consultar información consolidada de todas las disciplinas de su club, respetando los permisos establecidos.

## Registro SDD — Decisión 080

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-080 — Dashboards por nivel de permisos**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-563:** Cada administrador de disciplina tendrá un dashboard exclusivo de su disciplina.
  - **RN-564:** El dashboard mostrará jugadores, cuotas generadas, pagos realizados, deudas pendientes y recaudación.
  - **RN-565:** Incluirá gráficos de evolución, indicadores financieros y filtros por período y categoría.
  - **RN-566:** Permitirá exportar información en Excel y PDF.
  - **RN-567:** Los delegados generales tendrán un dashboard consolidado de su club.
  - **RN-568:** Los delegados podrán consultar estadísticas individuales de cada disciplina de su club.
  - **RN-569:** El acceso de los delegados a las operaciones deportivas y financieras será exclusivamente de lectura.
  - **RN-570:** Los administradores de una disciplina no podrán acceder a información privada de otras disciplinas.
  - **RN-571:** Los usuarios de un club no podrán acceder a información de otros clubes.
  - **RN-572:** Los indicadores comerciales globales de la plataforma permanecerán reservados al superadministrador.
</box>

## Estructura de dashboards

<box border radius="lg" padding={3} gap={2}>
  <box background="surface-secondary" radius="md" padding={3} gap={1}>
    <row align="center" gap={2}>
      <icon name="shield" color="secondary"/>
      **Superadministrador**
    </row>
    <text size="sm" color="secondary">Vista global de clubes, disciplinas, ingresos de la plataforma, contratos y penalizaciones.</text>
  </box>
  <box align="center">
    <icon name="arrow-down" color="tertiary"/>
  </box>
  <box background="surface-secondary" radius="md" padding={3} gap={1}>
    <row align="center" gap={2}>
      <icon name="building-2" color="secondary"/>
      **Delegado general — Obras Mendoza**
    </row>
    <text size="sm" color="secondary">Vista consolidada de las disciplinas del club, sin permisos para modificar sus operaciones.</text>
  </box>
  <box align="center">
    <icon name="arrow-down" color="tertiary"/>
  </box>
  <grid columns={2} gap={2}>
    <grid-item>
      <box border radius="md" padding={3} gap={1}>
        <icon name="chart-column" color="secondary"/>
        **Admin. Futsal**
        <caption>Solo Futsal</caption>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="md" padding={3} gap={1}>
        <icon name="chart-column" color="secondary"/>
        **Admin. Básquet**
        <caption>Solo Básquet</caption>
      </box>
    </grid-item>
  </grid>
</box>

---

## Fase 3 — Indicadores financieros

<text color="secondary" weight="medium" size="xs">Pregunta 81 — ¿Cómo se muestra la recaudación en los dashboards?</text>

Supongamos que Obras Futsal cobra una cuota de $20.000 mediante la plataforma.

El jugador paga ese importe más la comisión de tu plataforma y los costos de la pasarela.

Al consultar la recaudación, es importante distinguir **cuánto pagó el jugador y cuánto corresponde realmente a la disciplina**.

**¿Qué nivel de detalle financiero querés mostrar?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo importe de cuotas cobradas**

      Mostrar únicamente el total nominal de las cuotas que los jugadores pagaron.
    </radio>
    <radio value="b">
      **Opción B — Recaudación online y externa separadas**

      Mostrar cuánto se cobró mediante la plataforma y cuánto se registró como pago externo, con sus totales.
    </radio>
    <radio value="c">
      **Opción C — Desglose financiero completo (recomendada)**

      Mostrar importe nominal de cuotas, pagos online, pagos externos, comisión de la plataforma, costos de pasarela y monto neto correspondiente a la disciplina.

      <text color="secondary" size="xs">Los conceptos se mostrarán separados para evitar confundir la recaudación deportiva con los ingresos de la plataforma o los cargos pagados por el jugador. Los importes dependerán de la información efectivamente disponible de la pasarela.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 81 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"mostrar únicamente el importe nominal de cuotas cobradas",b:"mostrar por separado la recaudación online y los pagos externos",c:"mostrar un desglose financiero completo de cuotas, pagos online y externos, comisión de plataforma, costos de pasarela y neto correspondiente a la disciplina"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-081

### Registro 223 — Desglose financiero de recaudación
Fuente: `2f8bb987-778d-496e-aeb7-921494e838d1`

Respuesta previa del usuario:

Para la pregunta 81 del relevamiento SDD elijo la opción C: mostrar un desglose financiero completo de cuotas, pagos online y externos, comisión de plataforma, costos de pasarela y neto correspondiente a la disciplina.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 81 — Confirmada: opción C**
</row>

Los dashboards mostrarán un **desglose financiero completo**, diferenciando la recaudación de cada disciplina, los ingresos de la plataforma y los costos de la pasarela de pagos.

## Registro SDD — Decisión 081

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-081 — Desglose financiero de recaudación**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-573:** Mostrar el importe nominal de las cuotas cobradas.
  - **RN-574:** Diferenciar los cobros online de los pagos externos registrados manualmente.
  - **RN-575:** Identificar por separado las comisiones correspondientes a la plataforma.
  - **RN-576:** Mostrar los costos de la pasarela de pagos cuando esa información esté disponible.
  - **RN-577:** Mostrar el importe neto correspondiente a cada disciplina.
  - **RN-578:** Diferenciar el total pagado por el jugador del importe de la cuota deportiva.
  - **RN-579:** Evitar contabilizar dos veces una misma operación.
  - **RN-580:** Permitir consultar el desglose por operación y de forma consolidada.
  - **RN-581:** Aplicar los filtros y permisos establecidos en DEC-080.
  - **RN-582:** Incluir el desglose financiero en las exportaciones Excel y PDF.
</box>

## Ejemplo — Cuota de Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <text color="secondary" size="xs">Valores ilustrativos, suponiendo comisión de plataforma del 5% y costo de pasarela de $800, ambos adicionales a la cuota.</text>
  <table>
    <table-row><table-cell>Cuota deportiva</table-cell><table-cell align="end">**$20.000**</table-cell></table-row>
    <table-row><table-cell>Comisión plataforma (5%)</table-cell><table-cell align="end">$1.000</table-cell></table-row>
    <table-row><table-cell>Costo pasarela</table-cell><table-cell align="end">$800</table-cell></table-row>
    <table-row><table-cell>**Total pagado por jugador**</table-cell><table-cell align="end">**$21.800**</table-cell></table-row>
  </table>
  <divider color="subtle"/>
  <grid columns={2} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Neto para Obras Futsal</text>
        <title size="xl" color="default" tabularNums>$20.000</title>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Ingreso bruto plataforma</text>
        <title size="xl" color="default" tabularNums>$1.000</title>
      </box>
    </grid-item>
  </grid>
  <caption>El reparto efectivo dependerá de las condiciones y capacidades de la pasarela seleccionada.</caption>
</box>

---

## Fase 3 — Conciliación de pagos

<text color="secondary" weight="medium" size="xs">Pregunta 82 — ¿Cómo se controla que los pagos online realmente se hayan acreditado?</text>

Una cosa es que la pasarela informe que un jugador pagó y otra es verificar que el dinero haya sido efectivamente liquidado al destinatario.

Por ejemplo, Obras Futsal podría tener 100 pagos aprobados, pero algunos todavía pendientes de liquidación.

**¿Cómo querés manejar esa diferencia?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo registrar pagos aprobados**

      Cuando la pasarela confirma un pago, se considera cobrado. No se realiza seguimiento de la liquidación posterior.
    </radio>
    <radio value="b">
      **Opción B — Seguimiento de estados de liquidación**

      El sistema distingue pagos aprobados, pendientes de liquidación y liquidados, utilizando los datos que proporciona la pasarela.
    </radio>
    <radio value="c">
      **Opción C — Conciliación completa (recomendada)**

      Además de registrar los estados de liquidación, el sistema permite detectar diferencias entre lo cobrado y lo liquidado, identificar operaciones pendientes y consultar un historial de conciliación.

      <text color="secondary" size="xs">La conciliación será automática cuando la pasarela proporcione información suficiente. Las diferencias podrán revisarse manualmente sin alterar el registro original del pago.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 82 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"registrar únicamente los pagos aprobados sin seguimiento de liquidación",b:"registrar pagos aprobados y hacer seguimiento de sus estados de liquidación según datos de la pasarela",c:"implementar conciliación completa con estados de liquidación, detección de diferencias, pendientes e historial, con revisión manual cuando corresponda"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-082

### Registro 225 — Conciliación de pagos online
Fuente: `57d8eaab-1d30-4130-9ae5-c9ff5262b164`

Respuesta previa del usuario:

Para la pregunta 82 del relevamiento SDD elijo la opción C: implementar conciliación completa con estados de liquidación, detección de diferencias, pendientes e historial, con revisión manual cuando corresponda.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 82 — Confirmada: opción C**
</row>

La plataforma implementará una **conciliación financiera completa**, para verificar que los pagos aprobados por la pasarela coincidan con los importes efectivamente liquidados.

## Registro SDD — Decisión 082

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-082 — Conciliación de pagos online**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-583:** Diferenciar el estado del pago del estado de su liquidación.
  - **RN-584:** Registrar pagos aprobados, pendientes de liquidación y liquidados.
  - **RN-585:** Consultar automáticamente la información de liquidaciones cuando la pasarela lo permita.
  - **RN-586:** Comparar los importes esperados con los efectivamente liquidados.
  - **RN-587:** Detectar diferencias de importes, operaciones faltantes y liquidaciones pendientes.
  - **RN-588:** Identificar cada diferencia con su operación, club, disciplina y destinatario.
  - **RN-589:** Permitir la revisión manual de las diferencias detectadas.
  - **RN-590:** Conservar un historial de conciliaciones, resultados y revisiones.
  - **RN-591:** Registrar quién realizó una revisión manual, cuándo y qué observaciones agregó.
  - **RN-592:** Una conciliación manual no podrá modificar silenciosamente el pago original.
  - **RN-593:** Los dashboards financieros distinguirán importes aprobados de importes efectivamente liquidados.
  - **RN-594:** La conciliación respetará el aislamiento de información entre clubes y disciplinas.
</box>

## Ejemplo — Conciliación de Obras Futsal

<box border radius="lg" padding={3} gap={3}>
  <text color="secondary" size="xs">Datos ficticios para ilustrar el funcionamiento.</text>
  <grid columns={3} gap={2}>
    {#each [{label:"Esperado",value:"$200.000"},{label:"Liquidado",value:"$180.000"},{label:"Diferencia",value:"$20.000"}] as item}
      <grid-item>
        <box background="surface-secondary" radius="md" padding={2} gap={1}>
          <text color="secondary" size="2xs">{item.label}</text>
          <title size="md" tabularNums>{item.value}</title>
        </box>
      </grid-item>
    {/each}
  </grid>
  <table>
    <table-row><table-cell>Pago #1001</table-cell><table-cell><badge color="success">Liquidado</badge></table-cell></table-row>
    <table-row><table-cell>Pago #1002</table-cell><table-cell><badge color="warning">Pendiente</badge></table-cell></table-row>
    <table-row><table-cell>Pago #1003</table-cell><table-cell><badge color="danger">Diferencia</badge></table-cell></table-row>
  </table>
  <text size="sm" color="secondary">El sistema permitirá identificar qué operaciones explican la diferencia, sin considerar automáticamente que todo pago aprobado ya fue acreditado.</text>
</box>

---

## Fase 3 — Gestión de diferencias financieras

<text color="secondary" weight="medium" size="xs">Pregunta 83 — ¿Quién puede gestionar las diferencias de conciliación?</text>

Supongamos que el sistema detecta que una liquidación de Obras Futsal tiene una diferencia de $20.000.

El administrador de Futsal necesita conocer el problema, pero también hay que definir **quién tiene permiso para revisarlo y registrar su resolución**.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo el superadministrador**

      El superadministrador visualiza, investiga y registra la resolución de todas las diferencias. Los clubes y disciplinas solo ven los estados de liquidación.
    </radio>
    <radio value="b">
      **Opción B — Cada disciplina gestiona sus diferencias**

      Los administradores de disciplina pueden revisar y registrar la resolución de sus propias diferencias. El superadministrador supervisa todas.
    </radio>
    <radio value="c">
      **Opción C — Gestión centralizada con visibilidad local (recomendada)**

      El superadministrador es el único que puede registrar resoluciones de conciliación. Los administradores de disciplina pueden ver sus diferencias y agregar observaciones o documentación.

      <text color="secondary" size="xs">Los delegados generales pueden supervisar las diferencias de su club en modo lectura. Toda resolución conserva trazabilidad y no modifica el pago original.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 83 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solo el superadministrador revisa y resuelve diferencias; clubes y disciplinas ven estados de liquidación",b:"cada disciplina revisa y resuelve sus diferencias, con supervisión global del superadministrador",c:"el superadministrador registra las resoluciones, los administradores de disciplina pueden consultar y aportar observaciones o documentos, y los delegados generales supervisan en modo lectura"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-083

### Registro 227 — Responsables de conciliación financiera
Fuente: `f5aaa60b-1a58-4222-8420-300fdcf8b08b`

Respuesta previa del usuario:

Para la pregunta 83 del relevamiento SDD elijo la opción B: cada disciplina revisa y resuelve sus diferencias, con supervisión global del superadministrador.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 83 — Confirmada: opción B**
</row>

Cada disciplina será responsable de **revisar y resolver sus propias diferencias de conciliación**, mientras que el superadministrador podrá supervisar todas las operaciones de la plataforma.

## Registro SDD — Decisión 083

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-083 — Responsables de conciliación financiera**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-595:** Los administradores de disciplina podrán consultar las diferencias de conciliación correspondientes a su disciplina.
  - **RN-596:** Podrán investigar y registrar la resolución de esas diferencias.
  - **RN-597:** Ningún administrador podrá gestionar diferencias de otra disciplina.
  - **RN-598:** El superadministrador tendrá visibilidad global de las conciliaciones.
  - **RN-599:** Los delegados generales podrán consultar las diferencias de todas las disciplinas de su club, exclusivamente en modo lectura.
  - **RN-600:** Cada resolución registrará el administrador responsable y la fecha.
  - **RN-601:** Se conservará el historial de estados y acciones realizadas.
  - **RN-602:** Resolver una diferencia no modificará automáticamente el importe ni el estado del pago original.
  - **RN-603:** Las diferencias pendientes y resueltas estarán disponibles en los reportes correspondientes.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Conciliación #2048**
    <badge color="warning">Diferencia detectada</badge>
  </row>
  <grid columns={2} gap={2}>
    <grid-item>
      <text color="secondary" size="xs">Importe esperado</text>
      <title size="lg" tabularNums>$50.000</title>
    </grid-item>
    <grid-item>
      <text color="secondary" size="xs">Importe liquidado</text>
      <title size="lg" tabularNums>$48.000</title>
    </grid-item>
  </grid>
  <divider color="subtle"/>
  <text size="sm">El administrador de Futsal investiga la diferencia de $2.000 y registra su resolución. El superadministrador puede consultar el resultado y su historial.</text>
</box>

---

## Fase 3 — Resolución de conciliaciones

<text color="secondary" weight="medium" size="xs">Pregunta 84 — ¿Qué información se exige para resolver una diferencia?</text>

Ya definimos quién puede gestionar las diferencias. Ahora necesitamos determinar **qué debe registrar el administrador al resolver una conciliación**.

Por ejemplo, si faltan $2.000 porque la pasarela aplicó un cargo adicional, debería quedar constancia del motivo.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Resolución simple**

      El administrador marca la diferencia como resuelta, sin necesidad de agregar explicaciones.
    </radio>
    <radio value="b">
      **Opción B — Motivo obligatorio**

      Para resolver una diferencia, debe indicar una explicación. El sistema registra automáticamente usuario, fecha y hora.
    </radio>
    <radio value="c">
      **Opción C — Motivo y documentación (recomendada)**

      El administrador debe indicar un motivo, seleccionar un tipo de resolución y puede adjuntar comprobantes o documentación.

      <text color="secondary" size="xs">El sistema conserva el importe original, la diferencia detectada, el resultado de la revisión y el historial completo. Los adjuntos son opcionales.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 84 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir marcar diferencias como resueltas sin explicación",b:"exigir motivo obligatorio y registrar automáticamente usuario y fecha de resolución",c:"exigir motivo y tipo de resolución, permitir adjuntar documentación opcional y conservar trazabilidad completa"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-084

### Registro 229 — Registro y trazabilidad de resoluciones
Fuente: `ec21859b-b8f9-4184-8357-88339e08a542`

Respuesta previa del usuario:

Para la pregunta 84 del relevamiento SDD elijo la opción C: exigir motivo y tipo de resolución, permitir adjuntar documentación opcional y conservar trazabilidad completa.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 84 — Confirmada: opción C**
</row>

Para resolver una diferencia de conciliación, el administrador deberá indicar **un motivo y un tipo de resolución**, con posibilidad de adjuntar documentación. Todo quedará registrado para auditoría.

## Registro SDD — Decisión 084

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-084 — Registro y trazabilidad de resoluciones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-604:** Toda resolución de una diferencia de conciliación requerirá un motivo obligatorio.
  - **RN-605:** El administrador deberá seleccionar un tipo de resolución.
  - **RN-606:** Se permitirá adjuntar comprobantes o documentación de manera opcional.
  - **RN-607:** El sistema registrará automáticamente usuario, fecha y hora de la resolución.
  - **RN-608:** Se conservarán el importe esperado, el importe liquidado y la diferencia detectada.
  - **RN-609:** Se almacenará el tipo de resolución seleccionado y la explicación proporcionada.
  - **RN-610:** El historial conservará los cambios de estado y las intervenciones realizadas.
  - **RN-611:** Resolver una diferencia no modificará silenciosamente los datos originales del pago.
  - **RN-612:** El superadministrador podrá auditar las resoluciones de todas las disciplinas.
  - **RN-613:** Los delegados generales podrán consultar las resoluciones de su club en modo lectura.
</box>

## Ejemplo — Resolución de una diferencia

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Conciliación #2048**
    <badge color="success">Resuelta</badge>
  </row>
  <table>
    <table-row><table-cell>Importe esperado</table-cell><table-cell align="end">$50.000</table-cell></table-row>
    <table-row><table-cell>Importe liquidado</table-cell><table-cell align="end">$48.000</table-cell></table-row>
    <table-row><table-cell>**Diferencia**</table-cell><table-cell align="end">**$2.000**</table-cell></table-row>
  </table>
  <divider color="subtle"/>
  <text size="sm">**Tipo de resolución:** Cargo adicional de pasarela</text>
  <text size="sm">**Motivo:** Se verificó un descuento adicional informado por el proveedor de pagos.</text>
  <row align="center" gap={2}>
    <icon name="paperclip" color="secondary"/>
    <text size="sm">Comprobante de liquidación adjunto</text>
  </row>
  <caption>Ejemplo ilustrativo. La operación original permanece intacta.</caption>
</box>

---

## Fase 3 — Tipos de resolución

<text color="secondary" weight="medium" size="xs">Pregunta 85 — ¿Cómo se definen los tipos de resolución de conciliaciones?</text>

Ya establecimos que el administrador debe seleccionar un tipo de resolución. Ahora necesitamos definir de dónde salen esos tipos.

Por ejemplo: comisión adicional, liquidación pendiente, error de la pasarela, importe incorrecto u otro motivo.

**¿Cómo querés administrar esas categorías?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Lista fija del sistema**

      La plataforma ofrece tipos de resolución predefinidos, iguales para todos los clubes y disciplinas. No pueden modificarse.
    </radio>
    <radio value="b">
      **Opción B — Lista configurable por disciplina**

      Cada disciplina puede crear y administrar sus propios tipos de resolución.
    </radio>
    <radio value="c">
      **Opción C — Catálogo global configurable (recomendada)**

      La plataforma incluye tipos de resolución predeterminados y el superadministrador puede agregar, modificar o desactivar tipos para toda la plataforma.

      <text color="secondary" size="xs">Los administradores de disciplina seleccionan los tipos disponibles, pero no pueden modificar el catálogo. Los registros históricos conservan el tipo utilizado aunque luego se desactive.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 85 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"usar una lista fija de tipos de resolución, sin modificaciones",b:"permitir que cada disciplina configure sus propios tipos de resolución",c:"usar un catálogo global de tipos de resolución configurable por el superadministrador, con tipos predeterminados e historial preservado"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-085

### Registro 231 — Catálogo global de tipos de resolución
Fuente: `d1cf1c9a-0175-41b2-85fd-2250966923b6`

Respuesta previa del usuario:

Para la pregunta 85 del relevamiento SDD elijo la opción C: usar un catálogo global de tipos de resolución configurable por el superadministrador, con tipos predeterminados e historial preservado.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 85 — Confirmada: opción C**
</row>

Los tipos de resolución de conciliaciones se administrarán mediante un **catálogo global configurable exclusivamente por el superadministrador**, con valores predeterminados e historial preservado.

## Registro SDD — Decisión 085

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-085 — Catálogo global de tipos de resolución**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-614:** La plataforma tendrá un catálogo global de tipos de resolución de conciliaciones.
  - **RN-615:** El catálogo incluirá tipos predeterminados desde la configuración inicial.
  - **RN-616:** El superadministrador podrá crear nuevos tipos de resolución.
  - **RN-617:** El superadministrador podrá modificar los tipos existentes.
  - **RN-618:** El superadministrador podrá desactivar tipos sin eliminarlos físicamente.
  - **RN-619:** Los administradores de disciplina únicamente podrán seleccionar tipos activos.
  - **RN-620:** Los administradores de disciplina no podrán modificar el catálogo global.
  - **RN-621:** Los tipos desactivados no estarán disponibles para nuevas resoluciones.
  - **RN-622:** Las conciliaciones históricas conservarán el tipo y su descripción utilizados originalmente.
  - **RN-623:** Las modificaciones del catálogo quedarán registradas con usuario, fecha y valores anteriores y nuevos.
</box>

## Ejemplo — Catálogo de tipos de resolución

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Tipos de resolución**
    <badge>Ejemplo</badge>
  </row>
  <table>
    <table-row><table-cell>Comisión adicional de pasarela</table-cell><table-cell><badge color="success">Activo</badge></table-cell></table-row>
    <table-row><table-cell>Liquidación pendiente</table-cell><table-cell><badge color="success">Activo</badge></table-cell></table-row>
    <table-row><table-cell>Error informado por pasarela</table-cell><table-cell><badge color="success">Activo</badge></table-cell></table-row>
    <table-row><table-cell>Ajuste administrativo</table-cell><table-cell><badge color="success">Activo</badge></table-cell></table-row>
    <table-row><table-cell>Otro motivo</table-cell><table-cell><badge color="success">Activo</badge></table-cell></table-row>
    <table-row><table-cell>Tipo antiguo</table-cell><table-cell><badge>Inactivo</badge></table-cell></table-row>
  </table>
  <caption>Catálogo ilustrativo; los valores iniciales definitivos se establecerán en la especificación funcional.</caption>
</box>

---

## Fase 3 — Seguimiento de diferencias

<text color="secondary" weight="medium" size="xs">Pregunta 86 — ¿Qué ocurre cuando una diferencia de conciliación permanece sin resolver?</text>

Supongamos que Obras Futsal tiene una diferencia de $20.000 detectada hace 10 días y su administrador todavía no la resolvió.

**¿Cómo debería actuar el sistema ante diferencias pendientes durante mucho tiempo?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo mostrar pendientes**

      Las diferencias permanecen visibles en el módulo de conciliación hasta que un administrador las resuelva. No se generan alertas adicionales.
    </radio>
    <radio value="b">
      **Opción B — Alertas después de un plazo fijo**

      Si una diferencia continúa pendiente durante 7 días calendario, el sistema genera una alerta para el administrador de la disciplina y el superadministrador.
    </radio>
    <radio value="c">
      **Opción C — Alertas con plazo configurable (recomendada)**

      El superadministrador configura cuántos días puede permanecer pendiente una diferencia antes de generar una alerta.

      <text color="secondary" size="xs">Al superar ese plazo, se notifica dentro de la plataforma a los administradores de la disciplina y al superadministrador. La alerta desaparece cuando se resuelve la diferencia, conservando el historial.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 86 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"las diferencias pendientes solo se muestran en el módulo de conciliación, sin alertas adicionales",b:"generar alertas internas para administradores de disciplina y superadministrador después de 7 días calendario",c:"permitir que el superadministrador configure el plazo de alerta de diferencias pendientes, notificando internamente a la disciplina y al superadministrador"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-086

### Registro 233 — Alertas por diferencias de conciliación pendientes
Fuente: `fe675ee7-2ca7-447e-af81-c8fd839332ef`

Respuesta previa del usuario:

Para la pregunta 86 del relevamiento SDD elijo la opción C: permitir que el superadministrador configure el plazo de alerta de diferencias pendientes, notificando internamente a la disciplina y al superadministrador.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 86 — Confirmada: opción C**
</row>

El superadministrador podrá configurar el plazo máximo durante el cual una diferencia de conciliación puede permanecer pendiente antes de generar alertas internas.

## Registro SDD — Decisión 086

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-086 — Alertas por diferencias de conciliación pendientes**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-624:** El superadministrador podrá configurar el plazo de alerta para diferencias pendientes.
  - **RN-625:** El plazo se expresará en días calendario.
  - **RN-626:** El sistema calculará el tiempo transcurrido desde la detección de cada diferencia.
  - **RN-627:** Cuando se supere el plazo configurado, se generará una alerta interna.
  - **RN-628:** La alerta será visible para los administradores de la disciplina afectada.
  - **RN-629:** El superadministrador también recibirá la alerta en su panel global.
  - **RN-630:** No se enviarán correos electrónicos para estas alertas.
  - **RN-631:** El sistema evitará generar alertas duplicadas por una misma diferencia pendiente.
  - **RN-632:** Cuando se resuelva la diferencia, la alerta desaparecerá automáticamente de la vista activa.
  - **RN-633:** Se conservará el historial técnico de las alertas generadas.
  - **RN-634:** Las alertas no modificarán automáticamente el estado de la conciliación ni del pago original.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Diferencia de conciliación #2048**
    <badge color="warning">Requiere revisión</badge>
  </row>
  <grid columns={2} gap={2}>
    <grid-item>
      <text color="secondary" size="xs">Plazo configurado</text>
      <title size="lg">7 días</title>
    </grid-item>
    <grid-item>
      <text color="secondary" size="xs">Tiempo pendiente</text>
      <title size="lg">10 días</title>
    </grid-item>
  </grid>
  <row align="center" gap={2}>
    <icon name="bell-ring" color="secondary"/>
    <text size="sm">Se muestra una alerta en el panel de Obras Futsal y en el panel del superadministrador.</text>
  </row>
  <caption>Ejemplo ilustrativo. La alerta se retira cuando se registra la resolución.</caption>
</box>

---

## Fase 3 — Configuración de conciliaciones

<text color="secondary" weight="medium" size="xs">Pregunta 87 — ¿El plazo de alerta debe ser global o configurable por disciplina?</text>

Ya definimos que el superadministrador configura cuándo alertar por diferencias pendientes.

Ahora debemos decidir si ese plazo será igual para todos o si podrá variar según las necesidades de cada disciplina.

{@body const [opcion,setOpcion] = DIL.useState("a")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Plazo global único (recomendada)**

      El superadministrador configura un solo plazo para toda la plataforma. Por ejemplo, todas las disciplinas reciben alertas después de 7 días.
    </radio>
    <radio value="b">
      **Opción B — Plazo independiente por disciplina**

      El superadministrador configura un plazo diferente para cada disciplina.
    </radio>
    <radio value="c">
      **Opción C — Plazo global con excepciones**

      Existe un plazo general para toda la plataforma, pero el superadministrador puede establecer excepciones por club o disciplina.

      <text color="secondary" size="xs">Por ejemplo, 7 días para todas las disciplinas, excepto Obras Futsal, que podría tener un plazo de 3 días.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 87 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"usar un plazo global único de alerta de conciliaciones para toda la plataforma",b:"configurar un plazo de alerta independiente para cada disciplina",c:"usar un plazo global predeterminado con excepciones configurables por club o disciplina"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-087

### Registro 235 — Plazo global de alertas de conciliación
Fuente: `76036290-0f32-42d3-9df7-3375551f4e28`

Respuesta previa del usuario:

Para la pregunta 87 del relevamiento SDD elijo la opción A: usar un plazo global único de alerta de conciliaciones para toda la plataforma.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 87 — Confirmada: opción A**
</row>

El plazo para generar alertas por diferencias de conciliación pendientes será **único para toda la plataforma**, configurable exclusivamente por el superadministrador.

## Registro SDD — Decisión 087

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-087 — Plazo global de alertas de conciliación**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-635:** Existirá un único plazo global para alertar sobre diferencias de conciliación pendientes.
  - **RN-636:** Solo el superadministrador podrá configurar o modificar ese plazo.
  - **RN-637:** El plazo se expresará en días calendario.
  - **RN-638:** La configuración se aplicará a todos los clubes y disciplinas.
  - **RN-639:** No existirán excepciones de plazo por club ni por disciplina.
  - **RN-640:** Las modificaciones del plazo quedarán registradas con usuario, fecha y valores anteriores y nuevos.
  - **RN-641:** El sistema evaluará las diferencias pendientes utilizando el plazo global vigente.
  - **RN-642:** Las alertas se mostrarán únicamente dentro de la plataforma, según DEC-086.
</box>

## Ejemplo — Configuración global

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Alertas de conciliación**
    <badge>Configuración global</badge>
  </row>
  <row align="center" justify="between">
    <text color="secondary" size="sm">Plazo de alerta ilustrativo</text>
    <title size="lg" tabularNums>7 días</title>
  </row>
  <divider color="subtle"/>
  <table>
    <table-row>
      <table-cell>Obras Futsal</table-cell>
      <table-cell align="end">7 días</table-cell>
    </table-row>
    <table-row>
      <table-cell>Obras Básquet</table-cell>
      <table-cell align="end">7 días</table-cell>
    </table-row>
    <table-row>
      <table-cell>Club Municipal</table-cell>
      <table-cell align="end">7 días</table-cell>
    </table-row>
  </table>
  <caption>Todos utilizan el mismo valor. Los 7 días son solo un ejemplo, no un plazo aprobado.</caption>
</box>

---

## Fase 3 — Automatización de conciliaciones

<text color="secondary" weight="medium" size="xs">Pregunta 88 — ¿Con qué frecuencia debe ejecutarse la conciliación automática?</text>

Ya definimos que el sistema comparará los pagos aprobados con las liquidaciones informadas por la pasarela.

Ahora necesitamos determinar **cada cuánto tiempo se realizará esa verificación automática**.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Una vez por día**

      El sistema ejecuta automáticamente una conciliación diaria de todas las operaciones pendientes.
    </radio>
    <radio value="b">
      **Opción B — Varias veces al día**

      El sistema verifica las liquidaciones periódicamente, por ejemplo, cada 6 horas, para detectar diferencias más rápidamente.
    </radio>
    <radio value="c">
      **Opción C — Frecuencia configurable (recomendada)**

      El superadministrador configura la frecuencia global de conciliación automática.

      <text color="secondary" size="xs">Por ejemplo, cada 6, 12 o 24 horas. La ejecución dependerá de la información y límites de consulta que permita la pasarela seleccionada.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 88 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"ejecutar la conciliación automática una vez por día",b:"ejecutar la conciliación automática varias veces al día con una frecuencia fija",c:"permitir que el superadministrador configure la frecuencia global de conciliación automática, respetando las capacidades de la pasarela"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-088

### Registro 237 — Frecuencia global de conciliación automática
Fuente: `a3d22681-e771-4924-b0f5-3db810823dff`

Respuesta previa del usuario:

Para la pregunta 88 del relevamiento SDD elijo la opción C: permitir que el superadministrador configure la frecuencia global de conciliación automática, respetando las capacidades de la pasarela.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 88 — Confirmada: opción C**
</row>

El superadministrador podrá configurar **cada cuánto tiempo se ejecuta la conciliación automática** para toda la plataforma, respetando las capacidades de la pasarela de pagos.

## Registro SDD — Decisión 088

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-088 — Frecuencia global de conciliación automática**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-643:** La conciliación automática tendrá una frecuencia de ejecución configurable.
  - **RN-644:** Solo el superadministrador podrá modificar esa frecuencia.
  - **RN-645:** La frecuencia será global y se aplicará a todos los clubes y disciplinas.
  - **RN-646:** La configuración deberá respetar los límites y capacidades de consulta de la pasarela.
  - **RN-647:** El sistema ejecutará las conciliaciones automáticamente según la frecuencia vigente.
  - **RN-648:** Cada ejecución registrará fecha, hora, resultado y cantidad de operaciones procesadas.
  - **RN-649:** Se evitará procesar simultáneamente una misma conciliación de forma incompatible o generar registros duplicados.
  - **RN-650:** Los cambios de frecuencia quedarán registrados para auditoría.
  - **RN-651:** Si la pasarela no está disponible, el sistema registrará el error sin modificar incorrectamente los estados financieros.
  - **RN-652:** Una ejecución posterior podrá volver a consultar las operaciones que continúen pendientes.
</box>

## Ejemplo — Configuración global

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Conciliación automática**
    <badge>Ejemplo</badge>
  </row>
  <grid columns={3} gap={2}>
    {#each [{label:"Frecuencia",value:"6 horas"},{label:"Última ejecución",value:"12:00"},{label:"Próxima prevista",value:"18:00"}] as item}
      <grid-item>
        <box background="surface-secondary" radius="md" padding={2} gap={1}>
          <text color="secondary" size="2xs">{item.label}</text>
          <title size="md">{item.value}</title>
        </box>
      </grid-item>
    {/each}
  </grid>
  <row align="center" gap={2}>
    <icon name="check-circle-2" color="success"/>
    <text size="sm">Última ejecución completada correctamente.</text>
  </row>
  <caption>Horarios ilustrativos. No se ha definido todavía una frecuencia inicial obligatoria.</caption>
</box>

---

## Fase 3 — Operación y recuperación

<text color="secondary" weight="medium" size="xs">Pregunta 89 — ¿Se puede ejecutar una conciliación manualmente?</text>

Supongamos que la conciliación automática se ejecuta cada 12 horas, pero Obras Futsal detecta una diferencia y quiere verificar inmediatamente si la pasarela ya actualizó la liquidación.

**¿Quién debería poder iniciar una conciliación adicional sin esperar a la próxima ejecución programada?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Nadie**

      Las conciliaciones se ejecutan únicamente de forma automática, según la frecuencia configurada.
    </radio>
    <radio value="b">
      **Opción B — Solo el superadministrador**

      El superadministrador puede iniciar conciliaciones manuales de cualquier club o disciplina.
    </radio>
    <radio value="c">
      **Opción C — Superadministrador y administradores de disciplina (recomendada)**

      El superadministrador puede ejecutar conciliaciones de cualquier disciplina. Cada administrador de disciplina puede iniciar una conciliación de sus propias operaciones.

      <text color="secondary" size="xs">Las ejecuciones manuales respetarán los límites de la pasarela, quedarán auditadas y no modificarán la frecuencia automática global. Los delegados generales conservarán acceso de solo lectura.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 89 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir únicamente conciliaciones automáticas programadas",b:"permitir solo al superadministrador ejecutar conciliaciones manuales",c:"permitir al superadministrador ejecutar conciliaciones manuales globales y a los administradores de disciplina ejecutarlas para sus propias operaciones, con auditoría y límites de pasarela"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-089

### Registro 239 — Ejecución manual de conciliaciones
Fuente: `b8e3671d-4df5-4d69-87e7-3fb158c090c3`

Respuesta previa del usuario:

Para la pregunta 89 del relevamiento SDD elijo la opción C: permitir al superadministrador ejecutar conciliaciones manuales globales y a los administradores de disciplina ejecutarlas para sus propias operaciones, con auditoría y límites de pasarela.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 89 — Confirmada: opción C**
</row>

El superadministrador podrá ejecutar conciliaciones manuales de cualquier club o disciplina. Los administradores de disciplina también podrán iniciarlas, pero **únicamente sobre sus propias operaciones**.

## Registro SDD — Decisión 089

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-089 — Ejecución manual de conciliaciones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-653:** El superadministrador podrá iniciar conciliaciones manuales de cualquier club o disciplina.
  - **RN-654:** Los administradores de disciplina podrán iniciar conciliaciones manuales de sus propias operaciones.
  - **RN-655:** Un administrador no podrá ejecutar conciliaciones sobre otras disciplinas.
  - **RN-656:** Los delegados generales podrán consultar los resultados de conciliación de su club, pero no iniciar ejecuciones.
  - **RN-657:** Las ejecuciones manuales deberán respetar los límites de consulta y disponibilidad de la pasarela.
  - **RN-658:** El sistema evitará ejecuciones simultáneas incompatibles y resultados duplicados.
  - **RN-659:** Cada ejecución manual registrará el usuario responsable, fecha, hora y alcance.
  - **RN-660:** Se registrarán los resultados, las operaciones procesadas y los errores detectados.
  - **RN-661:** Una ejecución manual no modificará la frecuencia de conciliación automática configurada.
  - **RN-662:** Los resultados se integrarán al historial de conciliaciones y podrán generar o actualizar diferencias pendientes.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Conciliación manual**
    <badge>Ejemplo</badge>
  </row>
  <row align="center" gap={2}>
    <icon name="shield-check" color="success"/>
    <box flex="1" gap={1}>
      <text weight="medium" size="sm">Administrador de Obras Futsal</text>
      <text color="secondary" size="xs">Puede verificar los pagos y liquidaciones de Futsal.</text>
    </box>
  </row>
  <divider color="subtle"/>
  <row align="center" gap={2}>
    <icon name="shield-x" color="danger"/>
    <box flex="1" gap={1}>
      <text weight="medium" size="sm">Operaciones de Obras Básquet</text>
      <text color="secondary" size="xs">No puede iniciar conciliaciones sobre esa disciplina.</text>
    </box>
  </row>
  <divider color="subtle"/>
  <row align="center" gap={2}>
    <icon name="shield" color="secondary"/>
    <box flex="1" gap={1}>
      <text weight="medium" size="sm">Superadministrador</text>
      <text color="secondary" size="xs">Puede iniciar una conciliación global o seleccionar una disciplina específica.</text>
    </box>
  </row>
</box>

---

## Fase 3 — Alcance de las conciliaciones manuales

<text color="secondary" weight="medium" size="xs">Pregunta 90 — ¿Qué operaciones se pueden seleccionar para una conciliación manual?</text>

Supongamos que Obras Futsal tiene 500 pagos registrados, pero necesita verificar únicamente un pago específico porque el dinero todavía no aparece como liquidado.

**¿Qué nivel de selección debería permitir el sistema?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Todas las operaciones pendientes**

      Cada ejecución manual verifica todas las operaciones pendientes de conciliación dentro de la disciplina seleccionada.
    </radio>
    <radio value="b">
      **Opción B — Por período**

      El administrador puede elegir un rango de fechas para verificar los pagos correspondientes a ese período.
    </radio>
    <radio value="c">
      **Opción C — Selección flexible (recomendada)**

      Permitir conciliar todas las operaciones pendientes, seleccionar un período o verificar una operación específica mediante su identificador.

      <text color="secondary" size="xs">Siempre se respetarán los permisos del usuario y los límites de consulta de la pasarela. Todas las ejecuciones quedarán auditadas.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 90 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"cada conciliación manual procesa todas las operaciones pendientes del alcance autorizado",b:"permitir seleccionar un rango de fechas para las conciliaciones manuales",c:"permitir conciliar todas las operaciones pendientes, un rango de fechas o una operación específica por identificador, respetando permisos y límites de la pasarela"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-090

### Registro 241 — Alcance de las conciliaciones manuales
Fuente: `fc2c67f1-06ab-4c84-93cf-1552ac7ba1a8`

Respuesta previa del usuario:

Para la pregunta 90 del relevamiento SDD elijo la opción C: permitir conciliar todas las operaciones pendientes, un rango de fechas o una operación específica por identificador, respetando permisos y límites de la pasarela.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 90 — Confirmada: opción C**
</row>

Las conciliaciones manuales tendrán **tres modalidades de selección**: todas las operaciones pendientes, un rango de fechas o una operación específica mediante su identificador.

## Registro SDD — Decisión 090

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-090 — Alcance de las conciliaciones manuales**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-663:** El usuario podrá iniciar una conciliación de todas las operaciones pendientes dentro de su ámbito autorizado.
  - **RN-664:** Podrá seleccionar un rango de fechas para ejecutar la conciliación.
  - **RN-665:** Podrá conciliar una operación individual mediante su identificador.
  - **RN-666:** El sistema validará que las operaciones seleccionadas pertenezcan al club o disciplina autorizado.
  - **RN-667:** El superadministrador podrá utilizar cualquiera de las modalidades sobre cualquier club o disciplina.
  - **RN-668:** Los administradores de disciplina solo podrán conciliar operaciones de su disciplina.
  - **RN-669:** Las consultas respetarán las capacidades y limitaciones de la pasarela.
  - **RN-670:** Cada ejecución registrará el alcance seleccionado y sus parámetros.
  - **RN-671:** El sistema evitará generar registros de conciliación duplicados.
  - **RN-672:** Los resultados se incorporarán al historial y actualizarán las diferencias correspondientes sin modificar silenciosamente los pagos originales.
</box>

## Ejemplo — Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <text weight="medium" size="sm">Modalidades disponibles</text>
  <row align="center" gap={3}>
    <icon name="list-checks" color="secondary" size="lg"/>
    <box flex="1" gap={1}>
      **Todas las pendientes**
      <text color="secondary" size="xs">Verificar las operaciones que todavía necesitan conciliación.</text>
    </box>
  </row>
  <divider color="subtle"/>
  <row align="center" gap={3}>
    <icon name="calendar-range" color="secondary" size="lg"/>
    <box flex="1" gap={1}>
      **Por período**
      <text color="secondary" size="xs">Por ejemplo, del 1 al 15 de noviembre de 2026.</text>
    </box>
  </row>
  <divider color="subtle"/>
  <row align="center" gap={3}>
    <icon name="search" color="secondary" size="lg"/>
    <box flex="1" gap={1}>
      **Por identificador**
      <text color="secondary" size="xs">Verificar únicamente el pago #100245.</text>
    </box>
  </row>
</box>

---

## Fase 3 — Errores y recuperación automática

<text color="secondary" weight="medium" size="xs">Pregunta 91 — ¿Qué ocurre si falla una conciliación por un error de la pasarela?</text>

Supongamos que el sistema intenta conciliar 200 operaciones, pero la pasarela deja de responder a mitad del proceso.

Necesitamos definir cómo se recuperará la plataforma sin perder información ni generar registros duplicados.

**¿Qué comportamiento preferís?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Esperar la próxima ejecución**

      Se registra el error y las operaciones pendientes vuelven a procesarse en la siguiente conciliación programada.
    </radio>
    <radio value="b">
      **Opción B — Reintentos automáticos**

      El sistema reintenta las operaciones fallidas una cantidad fija de veces antes de esperar la siguiente ejecución.
    </radio>
    <radio value="c">
      **Opción C — Reintentos configurables y recuperación parcial (recomendada)**

      El superadministrador configura la cantidad de reintentos y el intervalo entre ellos.

      <text color="secondary" size="xs">Las operaciones conciliadas correctamente conservan su resultado; solo se reintentan las fallidas o pendientes. Si el error persiste, queda registrado para su revisión y posterior recuperación, sin duplicar resultados.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 91 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"registrar errores de conciliación y reintentar las operaciones pendientes en la siguiente ejecución programada",b:"realizar una cantidad fija de reintentos automáticos antes de esperar la próxima ejecución",c:"permitir configurar cantidad e intervalo de reintentos, conservar resultados exitosos y recuperar solo operaciones fallidas o pendientes sin duplicarlas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-091

### Registro 243 — Reintentos y recuperación de conciliaciones
Fuente: `77f5302f-9d91-417c-95f6-11b5d8443c40`

Respuesta previa del usuario:

Para la pregunta 91 del relevamiento SDD elijo la opción C: permitir configurar cantidad e intervalo de reintentos, conservar resultados exitosos y recuperar solo operaciones fallidas o pendientes sin duplicarlas.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 91 — Confirmada: opción C**
</row>

La plataforma tendrá un mecanismo de **reintentos automáticos configurables y recuperación parcial**, sin perder los resultados exitosos ni duplicar conciliaciones.

## Registro SDD — Decisión 091

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-091 — Reintentos y recuperación de conciliaciones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-673:** El superadministrador podrá configurar la cantidad máxima de reintentos.
  - **RN-674:** Podrá configurar el intervalo de tiempo entre reintentos.
  - **RN-675:** La configuración será global para todas las disciplinas.
  - **RN-676:** Ante un error recuperable de la pasarela, el sistema realizará reintentos automáticos según la configuración vigente.
  - **RN-677:** Las operaciones conciliadas exitosamente conservarán sus resultados.
  - **RN-678:** Solo se volverán a procesar las operaciones fallidas o pendientes.
  - **RN-679:** El sistema evitará generar conciliaciones o resultados duplicados.
  - **RN-680:** Cada intento registrará fecha, hora, operación, resultado y error, cuando corresponda.
  - **RN-681:** Si se agotan los reintentos, las operaciones sin conciliar permanecerán identificadas para su recuperación posterior.
  - **RN-682:** Los reintentos respetarán los límites de consulta de la pasarela.
  - **RN-683:** La recuperación no modificará silenciosamente los pagos originales.
  - **RN-684:** El historial permitirá identificar ejecuciones exitosas, parcialmente exitosas y fallidas.
</box>

## Ejemplo — Recuperación parcial

<box border radius="lg" padding={3} gap={3}>
  <text color="secondary" size="xs">Ejemplo ilustrativo: conciliación de 200 operaciones.</text>
  <grid columns={3} gap={2}>
    {#each [{label:"Exitosas",value:"170",color:"success"},{label:"Recuperadas",value:"20",color:"success"},{label:"Pendientes",value:"10",color:"warning"}] as item}
      <grid-item>
        <box background="surface-secondary" radius="md" padding={2} gap={1}>
          <text color="secondary" size="2xs">{item.label}</text>
          <title size="xl" color={item.color} tabularNums>{item.value}</title>
        </box>
      </grid-item>
    {/each}
  </grid>
  <box gap={1}>
    <row align="center" justify="between">
      <text weight="medium" size="sm">Operaciones conciliadas</text>
      <text weight="medium" size="sm" tabularNums>95%</text>
    </row>
    <box background="surface-tertiary" height="10px" radius="full" clip>
      <row height="100%" gap="0">
        <box background="rgba(22,163,74,0.85)" width="85%"/>
        <box background="rgba(22,163,74,0.45)" width="10%"/>
      </row>
    </box>
  </box>
  <text color="secondary" size="sm">Las 170 operaciones exitosas no se vuelven a procesar. Se recuperan otras 20 y las 10 restantes quedan pendientes para una ejecución posterior.</text>
</box>

---

## Fase 3 — Supervisión de errores

<text color="secondary" weight="medium" size="xs">Pregunta 92 — ¿Qué sucede cuando se agotan todos los reintentos de conciliación?</text>

Supongamos que la pasarela permanece caída y, después de todos los reintentos configurados, todavía quedan 10 operaciones sin conciliar.

**¿Cómo debería enterarse el responsable y qué debería permitir el sistema?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo registrar el error**

      Las operaciones quedan pendientes y el error se guarda en el historial. Se recuperarán en una próxima ejecución automática o manual.
    </radio>
    <radio value="b">
      **Opción B — Alerta al superadministrador**

      Además de registrar el error, se genera una alerta interna para el superadministrador, quien podrá supervisar y volver a ejecutar la conciliación.
    </radio>
    <radio value="c">
      **Opción C — Alertas según el alcance y recuperación manual (recomendada)**

      El sistema genera una alerta interna para el superadministrador y para los administradores de las disciplinas afectadas.

      <text color="secondary" size="xs">Se identifican las operaciones pendientes y el motivo del fallo. Los usuarios autorizados pueden iniciar una nueva conciliación manual según DEC-089 y DEC-090. La alerta desaparece cuando se recuperan las operaciones, conservando la auditoría.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 92 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"registrar los errores persistentes y dejar las operaciones pendientes para una conciliación posterior, sin alertas",b:"generar una alerta interna para el superadministrador cuando se agoten los reintentos",c:"generar alertas internas al superadministrador y administradores de las disciplinas afectadas, identificar fallos y permitir recuperación manual autorizada"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-092

### Registro 245 — Alertas y recuperación de fallos persistentes
Fuente: `2df35ebb-ae0c-43af-928c-bf6c47cd5975`

Respuesta previa del usuario:

Para la pregunta 92 del relevamiento SDD elijo la opción C: generar alertas internas al superadministrador y administradores de las disciplinas afectadas, identificar fallos y permitir recuperación manual autorizada.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 92 — Confirmada: opción C**
</row>

Cuando se agoten los reintentos de conciliación, el sistema generará **alertas internas para el superadministrador y las disciplinas afectadas**, permitiendo recuperar las operaciones mediante una nueva conciliación manual autorizada.

## Registro SDD — Decisión 092

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-092 — Alertas y recuperación de fallos persistentes**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-685:** Cuando se agoten los reintentos configurados, las operaciones sin conciliar quedarán identificadas como pendientes por error.
  - **RN-686:** El sistema generará una alerta interna para el superadministrador.
  - **RN-687:** Los administradores de las disciplinas afectadas también recibirán la alerta en sus respectivos paneles.
  - **RN-688:** Las alertas identificarán las operaciones afectadas, la fecha del fallo y el motivo informado.
  - **RN-689:** Se conservarán los resultados de las operaciones conciliadas exitosamente.
  - **RN-690:** Los usuarios autorizados podrán iniciar una conciliación manual para recuperar las operaciones pendientes.
  - **RN-691:** Las recuperaciones respetarán los permisos y alcances definidos en DEC-089 y DEC-090.
  - **RN-692:** El sistema evitará ejecuciones incompatibles y resultados duplicados.
  - **RN-693:** Las alertas desaparecerán de la vista activa cuando se hayan recuperado las operaciones afectadas.
  - **RN-694:** Se conservará el historial de errores, alertas, reintentos y recuperaciones.
  - **RN-695:** No se enviarán correos electrónicos para estas alertas.
</box>

## Ejemplo — Fallo de conciliación

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    <row align="center" gap={2}>
      <icon name="alert-triangle" color="danger"/>
      **Conciliación incompleta**
    </row>
    <badge color="danger">Reintentos agotados</badge>
  </row>
  <text color="secondary" size="sm">Obras Mendoza — Futsal</text>
  <grid columns={2} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Operaciones pendientes</text>
        <title size="xl" tabularNums>10</title>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Reintentos realizados</text>
        <title size="xl" tabularNums>3</title>
      </box>
    </grid-item>
  </grid>
  <text size="sm">**Motivo:** La pasarela de pagos no respondió dentro del tiempo esperado.</text>
  <row align="center" gap={2}>
    <icon name="refresh-cw" color="secondary"/>
    <text size="sm">El administrador podrá iniciar una nueva conciliación de las operaciones pendientes cuando el servicio esté disponible.</text>
  </row>
  <caption>Datos ilustrativos; la cantidad de reintentos es configurable.</caption>
</box>

---

## Fase 3 — Auditoría y seguimiento

<text color="secondary" weight="medium" size="xs">Pregunta 93 — ¿Cuánto tiempo debe conservarse el historial de conciliaciones?</text>

Ya definimos que las conciliaciones, diferencias, reintentos y resoluciones quedarán registrados.

Ahora necesitamos establecer **durante cuánto tiempo se conservará esa información** para consultas, auditorías y controles financieros.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Últimos 12 meses**

      El historial detallado estará disponible durante un año. Luego podrá eliminarse según la política de retención.
    </radio>
    <radio value="b">
      **Opción B — Últimos 5 años**

      El historial se conservará durante cinco años para permitir revisiones financieras de períodos anteriores.
    </radio>
    <radio value="c">
      **Opción C — Conservación histórica sin vencimiento automático (recomendada)**

      Conservar todas las conciliaciones, diferencias, resoluciones y auditorías, sin eliminarlas automáticamente por antigüedad.

      <text color="secondary" size="xs">Se podrán aplicar mecanismos de archivado para mantener el rendimiento. La conservación deberá adecuarse a las obligaciones legales y de protección de datos aplicables.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 93 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"conservar el historial detallado de conciliaciones durante 12 meses",b:"conservar el historial detallado de conciliaciones durante 5 años",c:"conservar el historial completo de conciliaciones sin vencimiento automático, con posibilidad de archivado y cumplimiento legal"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-093

### Registro 247 — Conservación histórica de conciliaciones
Fuente: `332c4c37-7fc1-4273-ae43-f9757a125388`

Respuesta previa del usuario:

Para la pregunta 93 del relevamiento SDD elijo la opción C: conservar el historial completo de conciliaciones sin vencimiento automático, con posibilidad de archivado y cumplimiento legal.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 93 — Confirmada: opción C**
</row>

La plataforma conservará **el historial completo de conciliaciones sin vencimiento automático**, con posibilidad de archivado para optimizar el rendimiento y respetando las obligaciones legales aplicables.

## Registro SDD — Decisión 093

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-093 — Conservación histórica de conciliaciones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-696:** Se conservará el historial completo de todas las conciliaciones financieras.
  - **RN-697:** No se eliminarán automáticamente registros por su antigüedad.
  - **RN-698:** Se conservarán ejecuciones automáticas y manuales, con sus resultados.
  - **RN-699:** Se mantendrá el historial de diferencias detectadas y sus resoluciones.
  - **RN-700:** Se conservarán los registros de errores, reintentos y recuperaciones.
  - **RN-701:** Las acciones de los usuarios mantendrán su trazabilidad, incluyendo responsable, fecha y modificaciones.
  - **RN-702:** El sistema podrá archivar información histórica para mejorar el rendimiento.
  - **RN-703:** El archivado no implicará la eliminación de los registros ni la pérdida de su trazabilidad.
  - **RN-704:** Los usuarios autorizados podrán consultar información histórica respetando los permisos de club y disciplina.
  - **RN-705:** La conservación y el tratamiento de datos deberán ajustarse a las obligaciones legales y de protección de datos aplicables, incluso cuando exijan su eliminación o anonimización.
</box>

## Ejemplo — Historial de conciliaciones

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Historial financiero**
    <badge>Ejemplo</badge>
  </row>
  <table>
    <table-row>
      <table-cell width="28%">**Período**</table-cell>
      <table-cell>**Estado**</table-cell>
      <table-cell>**Disponibilidad**</table-cell>
    </table-row>
    <table-row>
      <table-cell>2026</table-cell>
      <table-cell><badge color="success">Activo</badge></table-cell>
      <table-cell>Consulta directa</table-cell>
    </table-row>
    <table-row>
      <table-cell>2025</table-cell>
      <table-cell><badge color="success">Activo</badge></table-cell>
      <table-cell>Consulta directa</table-cell>
    </table-row>
    <table-row>
      <table-cell>2024</table-cell>
      <table-cell><badge>Archivado</badge></table-cell>
      <table-cell>Consulta histórica</table-cell>
    </table-row>
  </table>
  <caption>Ejemplo ilustrativo. Todavía no se definió cuándo archivar registros.</caption>
</box>

---

## Fase 3 — Archivado de información financiera

<text color="secondary" weight="medium" size="xs">Pregunta 94 — ¿Cómo se decide cuándo archivar las conciliaciones antiguas?</text>

Ya definimos que los registros históricos no se eliminarán automáticamente, pero podrán archivarse para evitar que las consultas financieras se vuelvan lentas con el paso de los años.

**¿Quién debería controlar ese archivado?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Archivado automático fijo**

      El sistema archiva automáticamente las conciliaciones con más de 12 meses de antigüedad, sin configuración adicional.
    </radio>
    <radio value="b">
      **Opción B — Archivado manual**

      El superadministrador decide cuándo archivar información histórica y qué períodos incluir.
    </radio>
    <radio value="c">
      **Opción C — Archivado automático configurable (recomendada)**

      El superadministrador configura la antigüedad mínima para archivar registros, por ejemplo, 12, 24 o 36 meses.

      <text color="secondary" size="xs">El sistema ejecuta el archivado automáticamente, sin eliminar información. Los registros archivados siguen disponibles para consultas y auditorías autorizadas.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 94 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"archivar automáticamente las conciliaciones con más de 12 meses",b:"permitir que el superadministrador archive manualmente los períodos históricos",c:"permitir que el superadministrador configure la antigüedad para el archivado automático, conservando la consulta histórica"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-094

### Registro 249 — Archivado automático configurable
Fuente: `d7031e9b-8346-4e06-9af4-6d5062d5a06b`

Respuesta previa del usuario:

Para la pregunta 94 del relevamiento SDD elijo la opción C: permitir que el superadministrador configure la antigüedad para el archivado automático, conservando la consulta histórica.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 94 — Confirmada: opción C**
</row>

El superadministrador podrá configurar la **antigüedad mínima para el archivado automático** de conciliaciones, sin eliminar información ni perder la posibilidad de consultar registros históricos.

## Registro SDD — Decisión 094

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-094 — Archivado automático configurable**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-706:** El superadministrador podrá configurar la antigüedad mínima para archivar conciliaciones.
  - **RN-707:** La antigüedad se expresará en meses.
  - **RN-708:** La configuración será global para todos los clubes y disciplinas.
  - **RN-709:** El sistema identificará automáticamente los registros que cumplan la antigüedad configurada.
  - **RN-710:** El archivado se realizará automáticamente, sin requerir intervención manual en cada ejecución.
  - **RN-711:** El archivado no eliminará los registros financieros ni sus auditorías.
  - **RN-712:** Las conciliaciones archivadas continuarán disponibles para consultas históricas autorizadas.
  - **RN-713:** Los permisos de acceso se mantendrán tanto para registros activos como archivados.
  - **RN-714:** Los cambios en la configuración de archivado quedarán auditados.
  - **RN-715:** El archivado no deberá interferir con conciliaciones o diferencias todavía pendientes de resolución.
  - **RN-716:** La conservación de información respetará las obligaciones legales aplicables, según DEC-093.
</box>

## Ejemplo — Configuración de archivado

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Política de archivado**
    <badge>Ejemplo</badge>
  </row>
  <row align="center" justify="between">
    <text color="secondary" size="sm">Antigüedad mínima</text>
    <title size="lg">24 meses</title>
  </row>
  <divider color="subtle"/>
  <row align="center" gap={2}>
    <icon name="database" color="success"/>
    <text size="sm">Conciliaciones recientes: disponibles en almacenamiento operativo.</text>
  </row>
  <row align="center" gap={2}>
    <icon name="archive" color="secondary"/>
    <text size="sm">Conciliaciones antiguas finalizadas: archivadas automáticamente.</text>
  </row>
  <row align="center" gap={2}>
    <icon name="search-check" color="secondary"/>
    <text size="sm">Registros archivados: consultables por usuarios autorizados.</text>
  </row>
  <caption>Los 24 meses son ilustrativos; el valor definitivo será configurable.</caption>
</box>

---

## Fase 3 — Consulta del historial archivado

<text color="secondary" weight="medium" size="xs">Pregunta 95 — ¿Cómo se consultan las conciliaciones archivadas?</text>

Supongamos que el administrador de Obras Futsal necesita revisar una conciliación realizada hace cuatro años.

Ya definimos que esa información debe conservarse. Ahora necesitamos decidir cómo se accede a ella.

**¿Qué experiencia de consulta preferís?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Consulta separada**

      Los registros archivados estarán en una sección específica de historial, separada de las conciliaciones recientes.
    </radio>
    <radio value="b">
      **Opción B — Solicitud de recuperación**

      Para consultar registros archivados, el usuario solicita su recuperación y el sistema los prepara para su visualización.
    </radio>
    <radio value="c">
      **Opción C — Consulta unificada (recomendada)**

      El usuario consulta conciliaciones recientes y archivadas desde el mismo módulo, utilizando filtros por período, club, disciplina, estado e identificador.

      <text color="secondary" size="xs">El sistema recupera automáticamente los registros archivados cuando sea necesario, sin exigir pasos adicionales. Se respetan los permisos y se informa si una consulta histórica demora más de lo habitual.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 95 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"consultar conciliaciones archivadas en una sección separada del historial",b:"solicitar la recuperación de conciliaciones archivadas antes de consultarlas",c:"consultar conciliaciones recientes y archivadas desde un mismo módulo con filtros y recuperación automática, respetando permisos"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-095

### Registro 251 — Consulta unificada del historial de conciliaciones
Fuente: `6629c31c-b616-4ee7-81d5-8ef0df490bc5`

Respuesta previa del usuario:

Para la pregunta 95 del relevamiento SDD elijo la opción C: consultar conciliaciones recientes y archivadas desde un mismo módulo con filtros y recuperación automática, respetando permisos.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 95 — Confirmada: opción C**
</row>

Las conciliaciones recientes y archivadas se consultarán desde **un único módulo**, con filtros y recuperación automática de información histórica, sin necesidad de solicitar una restauración manual.

## Registro SDD — Decisión 095

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-095 — Consulta unificada del historial de conciliaciones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-717:** Existirá un único módulo de consulta de conciliaciones recientes y archivadas.
  - **RN-718:** Los usuarios podrán filtrar por período, club, disciplina, estado e identificador de operación, según sus permisos.
  - **RN-719:** El sistema identificará automáticamente si la información solicitada está en almacenamiento operativo o archivado.
  - **RN-720:** Los registros archivados se recuperarán para consulta sin intervención manual del usuario.
  - **RN-721:** La interfaz mantendrá una experiencia de búsqueda uniforme para ambos tipos de registros.
  - **RN-722:** Se informará al usuario cuando una consulta histórica requiera mayor tiempo de procesamiento.
  - **RN-723:** Los administradores de disciplina solo podrán consultar conciliaciones de su propia disciplina.
  - **RN-724:** Los delegados generales podrán consultar el historial de las disciplinas de su club en modo lectura.
  - **RN-725:** El superadministrador podrá consultar el historial global de la plataforma.
  - **RN-726:** Las consultas históricas conservarán el detalle de operaciones, diferencias, resoluciones y auditorías.
  - **RN-727:** Consultar información archivada no modificará su estado ni sus registros originales.
</box>

## Ejemplo — Historial unificado

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Conciliaciones — Obras Futsal**
    <badge>Ejemplo</badge>
  </row>
  <table>
    <table-row>
      <table-cell>**Período**</table-cell>
      <table-cell>**Estado**</table-cell>
      <table-cell>**Almacenamiento**</table-cell>
    </table-row>
    <table-row>
      <table-cell>Nov. 2026</table-cell>
      <table-cell><badge color="success">Conciliada</badge></table-cell>
      <table-cell>Operativo</table-cell>
    </table-row>
    <table-row>
      <table-cell>Mar. 2025</table-cell>
      <table-cell><badge color="success">Conciliada</badge></table-cell>
      <table-cell><icon name="archive" inline size="xs"/> Archivado</table-cell>
    </table-row>
    <table-row>
      <table-cell>Jun. 2022</table-cell>
      <table-cell><badge color="success">Conciliada</badge></table-cell>
      <table-cell><icon name="archive" inline size="xs"/> Archivado</table-cell>
    </table-row>
  </table>
  <caption>Fechas y estados ilustrativos. Los registros se muestran juntos, independientemente de dónde estén almacenados.</caption>
</box>

---

## Fase 3 — Exportación del historial financiero

<text color="secondary" weight="medium" size="xs">Pregunta 96 — ¿Cómo se exportan las conciliaciones históricas?</text>

Ya definimos que el usuario podrá consultar conciliaciones recientes y archivadas desde un mismo módulo.

Ahora necesitamos decidir si también podrá **descargar ese historial completo**, incluyendo diferencias, resoluciones y auditorías.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo consulta en pantalla**

      Las conciliaciones históricas pueden visualizarse, pero no exportarse.
    </radio>
    <radio value="b">
      **Opción B — Exportación a Excel**

      Los usuarios autorizados pueden exportar conciliaciones recientes y archivadas a Excel, respetando los filtros seleccionados.
    </radio>
    <radio value="c">
      **Opción C — Excel y PDF con auditoría (recomendada)**

      Los usuarios autorizados pueden exportar conciliaciones recientes y archivadas a Excel y PDF, incluyendo operaciones, diferencias, resoluciones y trazabilidad.

      <text color="secondary" size="xs">Las exportaciones respetarán los filtros y permisos. Cada descarga quedará registrada con usuario, fecha, período y alcance de la información exportada.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 96 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir solo la consulta en pantalla del historial de conciliaciones, sin exportación",b:"permitir exportar a Excel conciliaciones recientes y archivadas respetando filtros y permisos",c:"permitir exportar a Excel y PDF conciliaciones recientes y archivadas con detalle de diferencias, resoluciones y auditoría de descargas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-096

### Registro 253 — Exportación y auditoría del historial de conciliaciones
Fuente: `a207b1c0-000a-4df3-b3ec-497350472f73`

Respuesta previa del usuario:

Para la pregunta 96 del relevamiento SDD elijo la opción C: permitir exportar a Excel y PDF conciliaciones recientes y archivadas con detalle de diferencias, resoluciones y auditoría de descargas.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 96 — Confirmada: opción C**
</row>

Los usuarios autorizados podrán exportar **conciliaciones recientes y archivadas en Excel y PDF**, incluyendo diferencias, resoluciones y trazabilidad. Cada exportación quedará registrada para auditoría.

## Registro SDD — Decisión 096

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-096 — Exportación y auditoría del historial de conciliaciones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-728:** Se permitirá exportar conciliaciones recientes y archivadas en Excel (`.xlsx`).
  - **RN-729:** También se permitirá exportarlas en PDF.
  - **RN-730:** Las exportaciones incluirán el detalle de las operaciones conciliadas.
  - **RN-731:** Se incluirán las diferencias detectadas y sus importes.
  - **RN-732:** Se incluirán los tipos de resolución, motivos y responsables.
  - **RN-733:** Los reportes incorporarán la trazabilidad de ejecuciones, reintentos y modificaciones.
  - **RN-734:** Las exportaciones respetarán los filtros de período, club, disciplina, estado e identificador.
  - **RN-735:** Se aplicarán los mismos permisos de acceso definidos para las consultas.
  - **RN-736:** Cada exportación registrará usuario, fecha, hora, formato y alcance de los datos.
  - **RN-737:** Los registros de exportación permanecerán disponibles para auditoría.
  - **RN-738:** La exportación de información archivada no modificará los registros originales.
  - **RN-739:** Si una exportación requiere recuperar un volumen importante de información histórica, el sistema informará su estado de procesamiento.
</box>

## Ejemplo — Exportación de conciliaciones

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Historial de Obras Futsal**
    <badge>Ejemplo</badge>
  </row>
  <grid columns={2} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Período</text>
        **2022–2026**
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Registros</text>
        **1.250**
      </box>
    </grid-item>
  </grid>
  <row gap={2}>
    <box flex="1" border radius="md" padding={3} align="center" gap={1}>
      <icon name="file-spreadsheet" color="success" size="lg"/>
      **Excel**
      <caption>Detalle de operaciones y auditoría</caption>
    </box>
    <box flex="1" border radius="md" padding={3} align="center" gap={1}>
      <icon name="file-text" color="secondary" size="lg"/>
      **PDF**
      <caption>Informe histórico consolidado</caption>
    </box>
  </row>
  <caption>Vista conceptual. La exportación incluirá información operativa y archivada según los filtros y permisos del usuario.</caption>
</box>

---

## Fase 3 — Exportaciones de gran volumen

<text color="secondary" weight="medium" size="xs">Pregunta 97 — ¿Cómo se procesan las exportaciones grandes?</text>

Supongamos que el superadministrador quiere descargar cinco años de conciliaciones de toda la plataforma, con cientos de miles de registros.

Generar el archivo puede tardar varios minutos. Necesitamos definir qué experiencia tendrá el usuario durante ese proceso.

**¿Cómo querés manejar las exportaciones de gran volumen?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Descarga inmediata**

      Todas las exportaciones se procesan en el momento. El usuario espera hasta que termine la generación del archivo.
    </radio>
    <radio value="b">
      **Opción B — Procesamiento en segundo plano**

      Las exportaciones grandes se generan en segundo plano. El usuario puede continuar utilizando la plataforma y descargar el archivo cuando esté listo.
    </radio>
    <radio value="c">
      **Opción C — Procesamiento en segundo plano con historial (recomendada)**

      Las exportaciones pequeñas se descargan inmediatamente. Las grandes se procesan en segundo plano, mostrando su estado y conservando un historial de solicitudes.

      <text color="secondary" size="xs">Cada usuario podrá consultar sus exportaciones pendientes, completadas o fallidas, descargar los archivos disponibles y volver a intentar una generación fallida, siempre respetando sus permisos.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 97 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"procesar todas las exportaciones inmediatamente, haciendo esperar al usuario",b:"procesar las exportaciones grandes en segundo plano y permitir descargarlas cuando estén listas",c:"procesar exportaciones pequeñas inmediatamente y grandes en segundo plano, con estados, historial de solicitudes, descarga y reintentos autorizados"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-097

### Registro 255 — Procesamiento de exportaciones
Fuente: `c8dced5c-5f12-483c-9ce6-b4f3295a8c06`

Respuesta previa del usuario:

Para la pregunta 97 del relevamiento SDD elijo la opción C: procesar exportaciones pequeñas inmediatamente y grandes en segundo plano, con estados, historial de solicitudes, descarga y reintentos autorizados.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 97 — Confirmada: opción C**
</row>

La plataforma tendrá un sistema de **exportaciones inteligente**, que procesará inmediatamente los archivos pequeños y enviará los grandes a segundo plano, con seguimiento, historial y posibilidad de reintentos.

## Registro SDD — Decisión 097

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-097 — Procesamiento de exportaciones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-740:** Las exportaciones pequeñas se generarán y descargarán inmediatamente.
  - **RN-741:** Las exportaciones grandes se procesarán mediante tareas en segundo plano.
  - **RN-742:** El usuario podrá continuar utilizando la plataforma mientras se genera un archivo.
  - **RN-743:** El sistema determinará si una exportación requiere procesamiento en segundo plano según su volumen estimado.
  - **RN-744:** Cada solicitud tendrá un identificador único.
  - **RN-745:** Se registrarán los estados: pendiente, en proceso, completada y fallida.
  - **RN-746:** Los usuarios podrán consultar el historial de sus solicitudes.
  - **RN-747:** Los archivos completados estarán disponibles para su descarga según los permisos correspondientes.
  - **RN-748:** Las solicitudes fallidas podrán reintentarse sin duplicar registros financieros.
  - **RN-749:** Los reintentos conservarán la trazabilidad de la solicitud original.
  - **RN-750:** Se registrarán el usuario solicitante, los filtros, el formato, las fechas y los resultados.
  - **RN-751:** Los procesos de exportación respetarán los permisos y el aislamiento entre clubes y disciplinas.
</box>

## Ejemplo — Centro de exportaciones

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Mis exportaciones**
    <badge>Ejemplo</badge>
  </row>
  <table>
    <table-row>
      <table-cell>**Archivo**</table-cell>
      <table-cell>**Estado**</table-cell>
      <table-cell>**Resultado**</table-cell>
    </table-row>
    <table-row>
      <table-cell>
        <row align="center" gap={1}>
          <icon name="file-spreadsheet" color="success"/>
          <text size="sm">Conciliaciones 2026</text>
        </row>
      </table-cell>
      <table-cell><badge color="success">Completada</badge></table-cell>
      <table-cell><icon name="download" color="secondary"/></table-cell>
    </table-row>
    <table-row>
      <table-cell>
        <row align="center" gap={1}>
          <icon name="file-text" color="secondary"/>
          <text size="sm">Histórico 2022–2026</text>
        </row>
      </table-cell>
      <table-cell><badge color="info">En proceso</badge></table-cell>
      <table-cell><icon name="loader-circle" color="secondary"/></table-cell>
    </table-row>
    <table-row>
      <table-cell>
        <row align="center" gap={1}>
          <icon name="file-spreadsheet" color="success"/>
          <text size="sm">Histórico 2021</text>
        </row>
      </table-cell>
      <table-cell><badge color="danger">Fallida</badge></table-cell>
      <table-cell><icon name="refresh-cw" color="secondary"/></table-cell>
    </table-row>
  </table>
  <caption>Representación conceptual de los estados de exportación, no archivos reales.</caption>
</box>

---

## Fase 3 — Seguridad y almacenamiento de archivos

<text color="secondary" weight="medium" size="xs">Pregunta 98 — ¿Cuánto tiempo estarán disponibles los archivos exportados para descargarlos?</text>

Aunque los datos financieros originales se conservan históricamente, los archivos Excel y PDF generados ocupan espacio de almacenamiento y pueden contener información sensible.

Necesitamos definir qué sucede con esos archivos después de generarlos.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Conservación permanente**

      Los archivos exportados permanecen disponibles indefinidamente, salvo eliminación manual autorizada.
    </radio>
    <radio value="b">
      **Opción B — Eliminación automática a los 30 días**

      Los archivos generados se eliminan automáticamente después de 30 días. El historial de solicitudes se conserva y los reportes pueden generarse nuevamente.
    </radio>
    <radio value="c">
      **Opción C — Retención configurable (recomendada)**

      El superadministrador configura cuántos días permanecen disponibles los archivos generados, por ejemplo, 7, 15 o 30 días.

      <text color="secondary" size="xs">Al vencer el plazo se elimina únicamente el archivo descargable, no los datos financieros originales ni el historial de solicitudes y auditorías. El usuario puede solicitar una nueva exportación cuando la necesite.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 98 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"conservar permanentemente los archivos exportados salvo eliminación manual autorizada",b:"eliminar los archivos exportados después de 30 días, conservando el historial de solicitudes",c:"permitir que el superadministrador configure los días de retención de archivos exportados, eliminando solo los archivos vencidos y conservando los datos financieros y auditorías"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-098

### Registro 257 — Retención configurable de archivos exportados
Fuente: `0d2a5aae-6b1d-4f14-94e6-48658b8ffc7c`

Respuesta previa del usuario:

Para la pregunta 98 del relevamiento SDD elijo la opción C: permitir que el superadministrador configure los días de retención de archivos exportados, eliminando solo los archivos vencidos y conservando los datos financieros y auditorías.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 98 — Confirmada: opción C**
</row>

El superadministrador podrá configurar **cuántos días permanecen disponibles los archivos exportados**. Al vencer ese plazo, se eliminarán los archivos generados, pero se conservarán los datos financieros originales y sus auditorías.

## Registro SDD — Decisión 098

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-098 — Retención configurable de archivos exportados**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-752:** El superadministrador podrá configurar el plazo de retención de archivos exportados, expresado en días.
  - **RN-753:** La configuración será global para todos los clubes y disciplinas.
  - **RN-754:** El plazo comenzará cuando el archivo termine de generarse correctamente.
  - **RN-755:** Los archivos Excel y PDF permanecerán disponibles para descarga durante el período configurado.
  - **RN-756:** Al vencer el plazo, el sistema eliminará automáticamente los archivos generados.
  - **RN-757:** La eliminación de archivos no afectará los registros financieros originales.
  - **RN-758:** El historial de solicitudes de exportación permanecerá disponible para auditoría.
  - **RN-759:** Los usuarios podrán solicitar una nueva exportación cuando el archivo anterior haya vencido.
  - **RN-760:** Las nuevas exportaciones respetarán los permisos y filtros correspondientes.
  - **RN-761:** El sistema identificará los archivos vencidos y evitará ofrecer enlaces de descarga inválidos.
  - **RN-762:** Los cambios en la configuración de retención quedarán auditados.
  - **RN-763:** Las eliminaciones automáticas se registrarán con fecha, archivo y resultado.
</box>

## Ejemplo — Ciclo de vida de una exportación

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Retención configurada**
    <title size="lg" tabularNums>30 días</title>
  </row>
  <list connector="solid" marker="number" gap={3}>
    <list-item>
      **Día 1 — Archivo generado**

      <text color="secondary" size="sm">El reporte Excel queda disponible para descarga.</text>
    </list-item>
    <list-item>
      **Día 15 — Archivo disponible**

      <text color="secondary" size="sm">El usuario autorizado todavía puede descargarlo.</text>
    </list-item>
    <list-item>
      **Día 31 — Archivo vencido**

      <text color="secondary" size="sm">El sistema elimina el archivo generado y conserva el historial de la solicitud.</text>
    </list-item>
    <list-item>
      **Posteriormente — Nueva exportación**

      <text color="secondary" size="sm">El usuario puede generar otro reporte utilizando los datos históricos conservados.</text>
    </list-item>
  </list>
  <caption>Ejemplo ilustrativo; el plazo será configurable.</caption>
</box>

---

## Fase 3 — Seguridad de los archivos exportados

<text color="secondary" weight="medium" size="xs">Pregunta 99 — ¿Quién puede descargar un archivo exportado?</text>

Supongamos que un administrador de Obras Futsal genera un Excel con las conciliaciones de su disciplina.

El archivo permanece almacenado durante 30 días. Necesitamos definir quién podrá descargarlo durante ese período.

**¿Qué política de acceso preferís?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo el usuario que lo generó**

      Cada archivo exportado será privado y únicamente podrá descargarlo su creador.
    </radio>
    <radio value="b">
      **Opción B — Usuarios del mismo ámbito**

      Todos los administradores autorizados de la misma disciplina podrán descargar los archivos generados por sus compañeros.
    </radio>
    <radio value="c">
      **Opción C — Acceso según permisos y alcance (recomendada)**

      El creador podrá descargar su archivo mientras conserve los permisos necesarios. Otros usuarios autorizados también podrán acceder según su rol y ámbito.

      <text color="secondary" size="xs">Por ejemplo, un administrador de Futsal no podrá acceder a archivos de Básquet; los delegados generales podrán acceder a reportes de su club y el superadministrador tendrá alcance global. Cada descarga quedará auditada y los permisos se verificarán nuevamente al descargar.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 99 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir descargar cada archivo exportado únicamente al usuario que lo generó",b:"permitir descargar archivos exportados a administradores autorizados del mismo ámbito",c:"permitir descargar archivos exportados según los permisos vigentes y alcance de cada rol, verificando acceso en cada descarga y auditándola"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-099

### Registro 259 — Control de acceso a archivos exportados
Fuente: `65713ba6-3dba-4bdc-8620-3fa14e81adc6`

Respuesta previa del usuario:

Para la pregunta 99 del relevamiento SDD elijo la opción C: permitir descargar archivos exportados según los permisos vigentes y alcance de cada rol, verificando acceso en cada descarga y auditándola.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 99 — Confirmada: opción C**
</row>

Los archivos exportados podrán ser descargados por **usuarios con permisos vigentes y alcance suficiente**, independientemente de quién los haya generado. Cada descarga requerirá una nueva validación de autorización y quedará auditada.

## Registro SDD — Decisión 099

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-099 — Control de acceso a archivos exportados**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-764:** Los archivos exportados no serán exclusivos del usuario que los generó.
  - **RN-765:** Solo podrán descargarlos usuarios autenticados y autorizados.
  - **RN-766:** Los administradores de disciplina podrán acceder únicamente a archivos correspondientes a su disciplina.
  - **RN-767:** Los delegados generales podrán descargar archivos correspondientes a las disciplinas de su club, según sus permisos de lectura.
  - **RN-768:** El superadministrador tendrá acceso global a los archivos exportados.
  - **RN-769:** El sistema verificará los permisos vigentes en cada solicitud de descarga.
  - **RN-770:** Si un usuario pierde los permisos necesarios, dejará de poder descargar los archivos afectados.
  - **RN-771:** No se permitirán descargas mediante enlaces públicos sin autorización.
  - **RN-772:** El sistema verificará que el contenido del archivo esté dentro del alcance autorizado para el usuario.
  - **RN-773:** Cada descarga registrará usuario, fecha, hora, archivo y resultado.
  - **RN-774:** Los intentos de descarga rechazados quedarán registrados para auditoría de seguridad.
  - **RN-775:** Los archivos vencidos según DEC-098 no podrán descargarse, aunque el usuario tenga permisos.
</box>

## Ejemplo — Acceso a un reporte de Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Conciliaciones_Futsal_2026.xlsx**
    <badge>Ejemplo</badge>
  </row>
  <table>
    <table-row>
      <table-cell>**Usuario**</table-cell>
      <table-cell>**Acceso**</table-cell>
    </table-row>
    <table-row>
      <table-cell>Administrador de Obras Futsal</table-cell>
      <table-cell><row align="center" gap={1}><icon name="check-circle" color="success" size="sm"/> Permitido</row></table-cell>
    </table-row>
    <table-row>
      <table-cell>Administrador de Obras Básquet</table-cell>
      <table-cell><row align="center" gap={1}><icon name="x-circle" color="danger" size="sm"/> Denegado</row></table-cell>
    </table-row>
    <table-row>
      <table-cell>Delegado general de Obras</table-cell>
      <table-cell><row align="center" gap={1}><icon name="check-circle" color="success" size="sm"/> Permitido</row></table-cell>
    </table-row>
    <table-row>
      <table-cell>Superadministrador</table-cell>
      <table-cell><row align="center" gap={1}><icon name="check-circle" color="success" size="sm"/> Permitido</row></table-cell>
    </table-row>
  </table>
  <caption>Los permisos se comprueban al momento de cada descarga, no solamente cuando se genera el archivo.</caption>
</box>

---

## Fase 3 — Seguridad de las descargas

<text color="secondary" weight="medium" size="xs">Pregunta 100 — ¿Cómo se entregan los archivos exportados de forma segura?</text>

Ya definimos quién puede descargar cada archivo. Ahora necesitamos determinar cómo se protege el enlace de descarga para evitar que alguien lo comparta y permita acceder a información financiera sin autorización.

**¿Qué mecanismo preferís?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Descarga desde la plataforma**

      El usuario descarga el archivo desde su sesión autenticada. El sistema valida sus permisos antes de entregarlo.
    </radio>
    <radio value="b">
      **Opción B — Enlaces temporales**

      La plataforma genera enlaces de descarga con vencimiento corto, por ejemplo, 5 minutos, que dejan de funcionar cuando expiran.
    </radio>
    <radio value="c">
      **Opción C — Autorización y enlaces temporales (recomendada)**

      El usuario solicita la descarga desde la plataforma autenticada. El sistema valida sus permisos y genera un enlace temporal, protegido y de corta duración.

      <text color="secondary" size="xs">El enlace estará vinculado a la autorización del usuario, no podrá utilizarse libremente por terceros y dejará de funcionar al vencer. Cada solicitud y descarga quedará auditada.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 100 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"descargar archivos desde la plataforma autenticada validando permisos en cada solicitud",b:"generar enlaces temporales de descarga con vencimiento corto",c:"combinar autenticación, validación de permisos y enlaces temporales protegidos, vinculados a la autorización del usuario y auditados"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-100

### Registro 261 — Descargas seguras mediante enlaces temporales
Fuente: `b8fa336f-7807-44ec-9521-15a3c8e602c4`

Respuesta previa del usuario:

Para la pregunta 100 del relevamiento SDD elijo la opción C: combinar autenticación, validación de permisos y enlaces temporales protegidos, vinculados a la autorización del usuario y auditados.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 100 — Confirmada: opción C**
</row>

Las exportaciones financieras se descargarán mediante **enlaces temporales protegidos**, generados después de autenticar al usuario y verificar sus permisos. Los enlaces no podrán utilizarse libremente por terceros.

## Registro SDD — Decisión 100

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-100 — Descargas seguras mediante enlaces temporales**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-776:** Las descargas deberán iniciarse desde una sesión autenticada.
  - **RN-777:** El sistema validará los permisos vigentes del usuario antes de autorizar cada descarga.
  - **RN-778:** Se comprobará que el archivo corresponda al club y las disciplinas autorizadas.
  - **RN-779:** Cada descarga autorizada utilizará un enlace temporal protegido.
  - **RN-780:** Los enlaces tendrán un tiempo de expiración corto y configurable técnicamente.
  - **RN-781:** Los enlaces estarán vinculados a la autorización del usuario solicitante.
  - **RN-782:** Un tercero no podrá descargar el archivo simplemente por conocer el enlace.
  - **RN-783:** Los enlaces vencidos serán rechazados.
  - **RN-784:** Si los permisos del usuario fueron revocados, se impedirá la descarga.
  - **RN-785:** Los archivos eliminados por vencimiento según DEC-098 no podrán descargarse.
  - **RN-786:** Cada solicitud de descarga registrará usuario, fecha, hora, archivo y resultado.
  - **RN-787:** Los accesos rechazados quedarán registrados para auditoría.
  - **RN-788:** La protección de los enlaces deberá impedir que se utilicen para acceder a archivos de otros clubes o disciplinas.
</box>

## Ejemplo — Descarga protegida

<box border radius="lg" padding={3} gap={2}>
  <list marker="number" connector="solid" gap={3}>
    <list-item>
      **Solicitud de descarga**

      <text color="secondary" size="sm">El administrador de Obras Futsal solicita un reporte Excel.</text>
    </list-item>
    <list-item>
      **Validación de permisos**

      <text color="secondary" size="sm">El sistema comprueba que sigue autorizado para acceder a esa información.</text>
    </list-item>
    <list-item>
      **Generación del enlace**

      <text color="secondary" size="sm">Se crea un acceso temporal protegido, válido durante un período breve.</text>
    </list-item>
    <list-item>
      **Descarga y auditoría**

      <text color="secondary" size="sm">El archivo se entrega al usuario autorizado y se registra la operación.</text>
    </list-item>
  </list>
</box>

---

## Fase 3 — Seguridad de los archivos almacenados

<text color="secondary" weight="medium" size="xs">Pregunta 101 — ¿Cómo se protegen los archivos exportados mientras están almacenados?</text>

Ya definimos cómo proteger las descargas. Ahora necesitamos establecer cómo se almacenan los Excel y PDF durante los días en que permanecen disponibles.

Estos archivos pueden contener datos personales, pagos y conciliaciones financieras.

**¿Qué nivel de protección querés implementar?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Almacenamiento privado**

      Los archivos se guardan en un almacenamiento no público, accesible únicamente desde el backend de la plataforma.
    </radio>
    <radio value="b">
      **Opción B — Almacenamiento privado y cifrado**

      Además del acceso privado, los archivos se almacenan cifrados para proteger su contenido frente a accesos no autorizados al almacenamiento.
    </radio>
    <radio value="c">
      **Opción C — Almacenamiento privado, cifrado y segregado (recomendada)**

      Los archivos se almacenan de forma privada y cifrada, con separación lógica por club y disciplina.

      <text color="secondary" size="xs">El backend controla los accesos, las claves de cifrado se administran de forma segura y las operaciones sobre los archivos quedan auditadas. Se mantiene la política de eliminación automática aprobada en DEC-098.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 101 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"almacenar los archivos exportados en almacenamiento privado accesible desde el backend",b:"almacenar los archivos exportados de forma privada y cifrada",c:"almacenar los archivos exportados de forma privada, cifrada y segregada lógicamente por club y disciplina, con gestión segura de claves y auditoría"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-101

### Registro 263 — Almacenamiento seguro de archivos exportados
Fuente: `7b7affad-3ce5-4624-8b70-076475630035`

Respuesta previa del usuario:

Para la pregunta 101 del relevamiento SDD elijo la opción C: almacenar los archivos exportados de forma privada, cifrada y segregada lógicamente por club y disciplina, con gestión segura de claves y auditoría.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 101 — Confirmada: opción C**
</row>

Los archivos exportados estarán **cifrados, almacenados de forma privada y separados lógicamente por club y disciplina**, con gestión segura de claves y auditoría de accesos.

## Registro SDD — Decisión 101

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-101 — Almacenamiento seguro de archivos exportados**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-789:** Los archivos Excel y PDF generados se almacenarán en repositorios privados.
  - **RN-790:** Los archivos estarán cifrados mientras permanezcan almacenados.
  - **RN-791:** Existirá segregación lógica de archivos por club y disciplina.
  - **RN-792:** Ningún usuario podrá acceder directamente al almacenamiento sin pasar por los controles de autorización.
  - **RN-793:** El backend verificará los permisos vigentes antes de entregar un archivo.
  - **RN-794:** Las claves de cifrado se gestionarán mediante mecanismos seguros, separados del contenido almacenado.
  - **RN-795:** Las credenciales y claves no se expondrán en enlaces de descarga ni en respuestas públicas.
  - **RN-796:** Se registrarán las operaciones de acceso, descarga y eliminación de archivos.
  - **RN-797:** Los archivos se eliminarán al vencer el plazo configurado en DEC-098.
  - **RN-798:** La eliminación de archivos no afectará los registros financieros originales ni sus auditorías.
  - **RN-799:** Los controles de almacenamiento serán coherentes con las autorizaciones y enlaces temporales definidos en DEC-099 y DEC-100.
  - **RN-800:** El sistema impedirá que errores en la identificación de club o disciplina expongan archivos de otros ámbitos.
</box>

## Ejemplo — Organización segura

<box border radius="lg" padding={3} gap={2}>
  <row align="center" gap={2}>
    <icon name="lock-keyhole" color="success"/>
    **Almacenamiento privado y cifrado**
  </row>
  <box border radius="md" padding={3} gap={2}>
    <row align="center" gap={2}>
      <icon name="folder" color="secondary"/>
      **Obras Mendoza**
    </row>
    <box padding={{left:3}} gap={2}>
      <row align="center" gap={2}>
        <icon name="folder-closed" color="secondary"/>
        **Futsal**
      </row>
      <row align="center" gap={2}>
        <icon name="file-lock" color="success"/>
        <text size="sm">Conciliaciones_2026.xlsx</text>
      </row>
      <row align="center" gap={2}>
        <icon name="folder-closed" color="secondary"/>
        **Básquet**
      </row>
      <row align="center" gap={2}>
        <icon name="file-lock" color="success"/>
        <text size="sm">Conciliaciones_2026.pdf</text>
      </row>
    </box>
  </box>
  <caption>Separación lógica ilustrativa; no requiere carpetas físicas independientes.</caption>
</box>

---

## Fase 3 — Continuidad operativa

<text color="secondary" weight="medium" size="xs">Pregunta 102 — ¿Qué sucede si falla el procesamiento de una exportación?</text>

Ya definimos que las exportaciones grandes se ejecutan en segundo plano. Sin embargo, podrían fallar por una interrupción del servidor, un problema de almacenamiento o un error temporal.

**¿Cómo debería recuperarse el sistema?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Reintento manual**

      La exportación queda marcada como fallida y el usuario debe solicitar una nueva generación.
    </radio>
    <radio value="b">
      **Opción B — Reintentos automáticos fijos**

      El sistema realiza hasta tres reintentos automáticos. Si todos fallan, el usuario puede solicitar una nueva exportación.
    </radio>
    <radio value="c">
      **Opción C — Reintentos configurables y recuperación automática (recomendada)**

      El superadministrador configura la cantidad máxima de reintentos y el intervalo entre ellos.

      <text color="secondary" size="xs">El sistema recupera automáticamente las exportaciones interrumpidas, evita generar archivos duplicados y registra cada intento. Si se agotan los reintentos, la solicitud queda fallida y el usuario puede volver a ejecutarla manualmente.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 102 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"marcar como fallidas las exportaciones con errores y permitir únicamente reintentos manuales",b:"realizar hasta tres reintentos automáticos antes de permitir una nueva solicitud manual",c:"permitir al superadministrador configurar cantidad e intervalo de reintentos automáticos de exportaciones, recuperar procesos interrumpidos, evitar duplicados y permitir reintentos manuales al agotarse"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-102

### Registro 265 — Recuperación automática de exportaciones fallidas
Fuente: `7f6e4393-a02a-465f-9fd0-701ea051b0d5`

Respuesta previa del usuario:

Para la pregunta 102 del relevamiento SDD elijo la opción C: permitir al superadministrador configurar cantidad e intervalo de reintentos automáticos de exportaciones, recuperar procesos interrumpidos, evitar duplicados y permitir reintentos manuales al agotarse.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 102 — Confirmada: opción C**
</row>

Las exportaciones tendrán **reintentos automáticos configurables**, recuperación de procesos interrumpidos y posibilidad de reintento manual cuando se agoten los intentos.

## Registro SDD — Decisión 102

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-102 — Recuperación automática de exportaciones fallidas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-801:** El superadministrador podrá configurar la cantidad máxima de reintentos automáticos de exportaciones.
  - **RN-802:** Podrá configurar el intervalo de tiempo entre reintentos.
  - **RN-803:** La configuración será global para todos los clubes y disciplinas.
  - **RN-804:** Las exportaciones grandes fallidas por errores recuperables se reintentarán automáticamente.
  - **RN-805:** El sistema podrá recuperar tareas interrumpidas por reinicios o fallos del servidor.
  - **RN-806:** Los reintentos no deberán generar archivos finales duplicados para una misma solicitud.
  - **RN-807:** Cada intento registrará fecha, hora, resultado y causa del error, cuando corresponda.
  - **RN-808:** Las solicitudes conservarán su identificador original durante los reintentos automáticos.
  - **RN-809:** Cuando se agoten los reintentos, la solicitud quedará en estado *fallida*.
  - **RN-810:** Los usuarios autorizados podrán iniciar manualmente una nueva ejecución de una exportación fallida.
  - **RN-811:** Las recuperaciones respetarán los permisos, filtros y controles de seguridad definidos anteriormente.
  - **RN-812:** Los archivos incompletos no estarán disponibles para descarga.
  - **RN-813:** El historial conservará la trazabilidad de todos los intentos y recuperaciones.
</box>

## Ejemplo — Recuperación de una exportación

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Exportación histórica 2022–2026**
    <badge>Ejemplo</badge>
  </row>
  <list marker="number" connector="solid" gap={3}>
    <list-item>
      **Primer intento**

      <row align="center" gap={2}>
        <icon name="x-circle" color="danger"/>
        <text size="sm">Falla por interrupción del servidor.</text>
      </row>
    </list-item>
    <list-item>
      **Reintento automático 1**

      <row align="center" gap={2}>
        <icon name="x-circle" color="danger"/>
        <text size="sm">El almacenamiento no responde.</text>
      </row>
    </list-item>
    <list-item>
      **Reintento automático 2**

      <row align="center" gap={2}>
        <icon name="check-circle" color="success"/>
        <text size="sm">Archivo generado correctamente.</text>
      </row>
    </list-item>
  </list>
  <divider color="subtle"/>
  <row align="center" justify="between">
    **Estado final**
    <badge color="success">Completada</badge>
  </row>
  <caption>Ejemplo ilustrativo. La cantidad y el intervalo de reintentos serán configurables.</caption>
</box>

---

## Fase 3 — Notificaciones de exportaciones

<text color="secondary" weight="medium" size="xs">Pregunta 103 — ¿Cómo se entera el usuario cuando termina una exportación en segundo plano?</text>

Supongamos que un administrador solicita un Excel con cinco años de conciliaciones y continúa trabajando en otra sección de la plataforma.

El archivo termina de generarse 10 minutos después.

**¿Cómo debería avisarle el sistema?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo consultar el historial**

      El usuario debe ingresar al centro de exportaciones para verificar si el archivo está listo.
    </radio>
    <radio value="b">
      **Opción B — Notificación interna**

      Cuando termina la exportación, el sistema muestra una notificación interna al usuario que la solicitó, con acceso al archivo.
    </radio>
    <radio value="c">
      **Opción C — Notificaciones internas de éxito y error (recomendada)**

      El sistema notifica al usuario solicitante cuando una exportación se completa o falla definitivamente.

      <text color="secondary" size="xs">Las notificaciones aparecen dentro de la plataforma, incluyen el estado y un acceso al centro de exportaciones. No se envían correos electrónicos ni se adjuntan archivos sensibles. Los permisos se vuelven a validar al descargar.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 103 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"mostrar el estado únicamente en el historial de exportaciones, sin notificaciones",b:"enviar una notificación interna al usuario cuando se complete una exportación",c:"enviar notificaciones internas al solicitante cuando una exportación termine correctamente o falle definitivamente, sin correos ni adjuntos, manteniendo la validación de permisos"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-103

### Registro 267 — Notificaciones de resultados de exportaciones
Fuente: `528439cc-d4d8-4c81-8a9e-714827acb7ef`

Respuesta previa del usuario:

Para la pregunta 103 del relevamiento SDD elijo la opción C: enviar notificaciones internas al solicitante cuando una exportación termine correctamente o falle definitivamente, sin correos ni adjuntos, manteniendo la validación de permisos.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 103 — Confirmada: opción C**
</row>

El sistema enviará **notificaciones internas al usuario solicitante** cuando una exportación finalice correctamente o falle definitivamente, sin utilizar correos electrónicos ni adjuntar archivos sensibles.

## Registro SDD — Decisión 103

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-103 — Notificaciones de resultados de exportaciones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-814:** Las exportaciones en segundo plano generarán una notificación interna al completarse correctamente.
  - **RN-815:** También se notificará cuando una exportación falle definitivamente tras agotar los reintentos.
  - **RN-816:** Las notificaciones se dirigirán al usuario que solicitó la exportación.
  - **RN-817:** Las notificaciones estarán disponibles dentro de la plataforma.
  - **RN-818:** No se enviarán correos electrónicos por estos eventos.
  - **RN-819:** No se incluirán archivos Excel o PDF adjuntos a las notificaciones.
  - **RN-820:** Cada notificación identificará la exportación y su estado final.
  - **RN-821:** Las notificaciones permitirán acceder al centro de exportaciones.
  - **RN-822:** El acceso desde una notificación no omitirá las verificaciones de permisos.
  - **RN-823:** Los archivos vencidos o eliminados no podrán descargarse desde una notificación antigua.
  - **RN-824:** El sistema evitará notificaciones duplicadas por reintentos automáticos.
  - **RN-825:** Los eventos de notificación conservarán su trazabilidad.
</box>

## Ejemplo — Centro de notificaciones

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Notificaciones**
    <badge>Ejemplo</badge>
  </row>
  <row align="start" gap={3}>
    <icon name="check-circle-2" color="success" size="lg"/>
    <box flex="1" gap={1}>
      **Exportación completada**
      <text size="sm">Tu reporte de conciliaciones 2022–2026 está listo para descargar.</text>
      <caption>Acceso al centro de exportaciones</caption>
    </box>
  </row>
  <divider color="subtle"/>
  <row align="start" gap={3}>
    <icon name="alert-circle" color="danger" size="lg"/>
    <box flex="1" gap={1}>
      **Exportación fallida**
      <text size="sm">No se pudo generar el reporte de conciliaciones de 2021 después de los reintentos configurados.</text>
      <caption>Podés solicitar un nuevo intento desde el centro de exportaciones.</caption>
    </box>
  </row>
</box>

---

## Fase 3 — Gestión de notificaciones

<text color="secondary" weight="medium" size="xs">Pregunta 104 — ¿Cómo se administran las notificaciones de exportaciones?</text>

Ya definimos que el usuario recibirá avisos internos cuando sus exportaciones finalicen o fallen.

Ahora necesitamos decidir qué sucede con esas notificaciones después de que el usuario las vea.

**¿Qué comportamiento preferís?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Notificaciones temporales**

      Las notificaciones aparecen cuando ocurre el evento y desaparecen automáticamente después de unos segundos. El historial de exportaciones sigue disponible.
    </radio>
    <radio value="b">
      **Opción B — Notificaciones persistentes**

      Las notificaciones permanecen en el centro de notificaciones hasta que el usuario las marque como leídas.
    </radio>
    <radio value="c">
      **Opción C — Centro de notificaciones con estados (recomendada)**

      Las notificaciones se guardan en un centro interno, con estados de leída y no leída.

      <text color="secondary" size="xs">El usuario podrá marcarlas como leídas individualmente o todas juntas. También podrá consultar notificaciones anteriores. Marcar una notificación como leída no eliminará el archivo exportado ni alterará el historial de solicitudes.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 104 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"mostrar notificaciones temporales que desaparecen automáticamente, conservando el historial de exportaciones",b:"mantener notificaciones persistentes hasta que el usuario las marque como leídas",c:"implementar un centro de notificaciones con estados leída/no leída, marcado individual o masivo y consulta histórica, sin afectar archivos ni auditorías"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-104

### Registro 269 — Gestión de notificaciones de exportaciones
Fuente: `aae06cee-31d1-4977-aa6e-6e8bc7b62794`

Respuesta previa del usuario:

Para la pregunta 104 del relevamiento SDD elijo la opción C: implementar un centro de notificaciones con estados leída/no leída, marcado individual o masivo y consulta histórica, sin afectar archivos ni auditorías.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 104 — Confirmada: opción C**
</row>

La plataforma tendrá un **centro de notificaciones interno**, donde cada usuario podrá consultar sus avisos, distinguir entre leídos y no leídos y marcarlos individualmente o en conjunto.

## Registro SDD — Decisión 104

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-104 — Gestión de notificaciones de exportaciones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-826:** Cada usuario dispondrá de un centro de notificaciones interno.
  - **RN-827:** Las notificaciones de exportaciones tendrán estados *leída* y *no leída*.
  - **RN-828:** Las notificaciones nuevas se registrarán inicialmente como no leídas.
  - **RN-829:** El usuario podrá marcar individualmente una notificación como leída.
  - **RN-830:** El usuario podrá marcar todas sus notificaciones como leídas mediante una acción masiva.
  - **RN-831:** Se mostrará un contador de notificaciones no leídas.
  - **RN-832:** El usuario podrá consultar notificaciones anteriores.
  - **RN-833:** Las notificaciones estarán asociadas al usuario destinatario y a la exportación correspondiente.
  - **RN-834:** Un usuario no podrá consultar ni modificar las notificaciones privadas de otro usuario.
  - **RN-835:** Marcar una notificación como leída no modificará el estado de la exportación.
  - **RN-836:** La lectura de una notificación no eliminará ni extenderá la disponibilidad del archivo.
  - **RN-837:** El historial de exportaciones y sus auditorías permanecerá independiente del estado de lectura.
  - **RN-838:** Las notificaciones históricas respetarán las restricciones de acceso a la información asociada.
</box>

## Ejemplo — Centro de notificaciones

{@body const [avisos,setAvisos] = DIL.useState([{id:1,title:"Exportación completada",detail:"Conciliaciones 2022–2026",read:false,success:true},{id:2,title:"Exportación fallida",detail:"Conciliaciones 2021",read:false,success:false},{id:3,title:"Exportación completada",detail:"Conciliaciones octubre 2026",read:true,success:true}])}
<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    <row align="center" gap={2}>
      **Mis notificaciones**
      {#if avisos.filter(a=>!a.read).length>0}
        <badge color="info">{avisos.filter(a=>!a.read).length} sin leer</badge>
      {/if}
    </row>
    <button size="xs" color="secondary" variant="outline" disabled={avisos.every(a=>a.read)} onClick={()=>setAvisos(v=>v.map(a=>({...a,read:true})))}>Marcar todas leídas</button>
  </row>
  {#each avisos as aviso}
    <box key={aviso.id} gap={2}>
      <row align="start" gap={2}>
        <icon name={aviso.success?"check-circle-2":"alert-circle"} color={aviso.success?"success":"danger"}/>
        <box flex="1" gap={1}>
          <row align="center" gap={2} wrap="wrap">
            <text weight={aviso.read?"normal":"semibold"}>{aviso.title}</text>
            {#if !aviso.read}
              <badge color="info">Nueva</badge>
            {/if}
          </row>
          <text color="secondary" size="sm">{aviso.detail}</text>
        </box>
        {#if !aviso.read}
          <button size="xs" variant="ghost" color="secondary" onClick={()=>setAvisos(v=>v.map(a=>a.id===aviso.id?{...a,read:true}:a))}><icon name="check" inline/> Leída</button>
        {/if}
      </row>
      {#if aviso.id!==3}
        <divider color="subtle"/>
      {/if}
    </box>
  {/each}
  <caption>Demostración interactiva del comportamiento aprobado. No representa notificaciones reales.</caption>
</box>

---

## Fase 3 — Retención de notificaciones

<text color="secondary" weight="medium" size="xs">Pregunta 105 — ¿Durante cuánto tiempo se conservarán las notificaciones internas?</text>

Ya definimos que los usuarios podrán consultar notificaciones históricas. Sin embargo, con el tiempo podrían acumularse miles de avisos por exportaciones y otros eventos.

**¿Cómo querés gestionar su conservación?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Conservación permanente**

      Las notificaciones permanecen indefinidamente en el centro de notificaciones, tanto leídas como no leídas.
    </radio>
    <radio value="b">
      **Opción B — Eliminación automática a los 90 días**

      Las notificaciones se eliminan automáticamente después de 90 días. Los registros de auditoría y exportaciones se conservan por separado.
    </radio>
    <radio value="c">
      **Opción C — Retención configurable (recomendada)**

      El superadministrador configura cuántos días permanecen visibles las notificaciones, por ejemplo, 30, 60 o 90 días.

      <text color="secondary" size="xs">Al vencer el plazo, las notificaciones se retiran automáticamente del centro del usuario, sin eliminar los registros financieros, las solicitudes de exportación ni sus auditorías. La configuración será global.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 105 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"conservar permanentemente todas las notificaciones internas",b:"eliminar automáticamente las notificaciones después de 90 días, conservando auditorías",c:"permitir que el superadministrador configure los días de visibilidad de notificaciones internas y retirarlas automáticamente al vencer, conservando datos financieros y auditorías"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-105

### Registro 271 — Retención configurable de notificaciones internas
Fuente: `d99d4980-aea8-4501-b575-c6555e296d9c`

Respuesta previa del usuario:

Para la pregunta 105 del relevamiento SDD elijo la opción C: permitir que el superadministrador configure los días de visibilidad de notificaciones internas y retirarlas automáticamente al vencer, conservando datos financieros y auditorías.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 105 — Confirmada: opción C**
</row>

El superadministrador podrá configurar **cuántos días permanecen visibles las notificaciones internas**. Una vez vencido ese plazo, desaparecerán del centro de notificaciones, pero se conservarán los registros financieros y las auditorías correspondientes.

## Registro SDD — Decisión 105

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-105 — Retención configurable de notificaciones internas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-839:** El superadministrador podrá configurar el período de visibilidad de las notificaciones internas.
  - **RN-840:** El período se expresará en días.
  - **RN-841:** La configuración será global para todos los clubes y disciplinas.
  - **RN-842:** El plazo comenzará desde la fecha de creación de cada notificación.
  - **RN-843:** Las notificaciones vencidas se retirarán automáticamente del centro de notificaciones.
  - **RN-844:** La política se aplicará tanto a notificaciones leídas como no leídas.
  - **RN-845:** Las notificaciones retiradas dejarán de contabilizarse como pendientes de lectura.
  - **RN-846:** La eliminación de visibilidad no afectará los registros financieros originales.
  - **RN-847:** Se conservarán las solicitudes de exportación y sus auditorías.
  - **RN-848:** Los cambios en la configuración de retención quedarán auditados.
  - **RN-849:** El sistema registrará las ejecuciones de limpieza de notificaciones.
  - **RN-850:** La retención de notificaciones será independiente de la retención de archivos exportados definida en DEC-098.
</box>

## Ejemplo — Ciclo de vida de una notificación

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Visibilidad configurada**
    <title size="lg">60 días</title>
  </row>
  <list marker="number" connector="solid" gap={3}>
    <list-item>
      **Día 1 — Notificación creada**

      <text color="secondary" size="sm">El usuario recibe el aviso de que su exportación terminó correctamente.</text>
    </list-item>
    <list-item>
      **Día 20 — Notificación leída**

      <text color="secondary" size="sm">El aviso permanece disponible en el historial del centro de notificaciones.</text>
    </list-item>
    <list-item>
      **Después de 60 días — Retiro automático**

      <text color="secondary" size="sm">El aviso desaparece del centro de notificaciones, sin afectar los datos financieros ni la auditoría.</text>
    </list-item>
  </list>
  <caption>Los 60 días son ilustrativos; el plazo será configurable.</caption>
</box>

---

## Fase 3 — Preferencias de notificaciones

<text color="secondary" weight="medium" size="xs">Pregunta 106 — ¿Los usuarios podrán personalizar qué notificaciones reciben?</text>

Ya definimos un centro de notificaciones con historial y retención configurable. A medida que agreguemos funcionalidades, habrá distintos avisos: exportaciones, conciliaciones fallidas y posiblemente otros eventos operativos.

**¿Querés que cada usuario pueda configurar sus preferencias?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Todas obligatorias**

      Los usuarios reciben todas las notificaciones correspondientes a su rol, sin posibilidad de desactivarlas.
    </radio>
    <radio value="b">
      **Opción B — Preferencias individuales libres**

      Cada usuario puede activar o desactivar cualquier categoría de notificación interna desde su perfil.
    </radio>
    <radio value="c">
      **Opción C — Notificaciones críticas obligatorias y opcionales configurables (recomendada)**

      Las notificaciones críticas, como fallos persistentes de conciliación, serán obligatorias para los responsables autorizados.

      <text color="secondary" size="xs">Las notificaciones informativas podrán activarse o desactivarse individualmente. El superadministrador definirá globalmente qué categorías son críticas y cuáles opcionales, sin permitir que se omitan alertas esenciales para la operación.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 106 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"hacer obligatorias todas las notificaciones correspondientes a cada rol",b:"permitir que cada usuario active o desactive libremente cualquier categoría de notificaciones",c:"mantener obligatorias las notificaciones críticas y permitir preferencias individuales para las informativas, con clasificación global administrada por el superadministrador"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-106

### Registro 273 — Preferencias y clasificación de notificaciones
Fuente: `0a3738fa-2cfb-4edb-8244-cff9aee33ce6`

Respuesta previa del usuario:

Para la pregunta 106 del relevamiento SDD elijo la opción C: mantener obligatorias las notificaciones críticas y permitir preferencias individuales para las informativas, con clasificación global administrada por el superadministrador.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 106 — Confirmada: opción C**
</row>

La plataforma distinguirá entre **notificaciones críticas obligatorias** y **notificaciones informativas configurables por cada usuario**. El superadministrador controlará globalmente la clasificación.

## Registro SDD — Decisión 106

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-106 — Preferencias y clasificación de notificaciones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-851:** Las notificaciones internas se clasificarán en críticas e informativas.
  - **RN-852:** Las notificaciones críticas serán obligatorias para los usuarios destinatarios según sus roles y permisos.
  - **RN-853:** Los usuarios no podrán desactivar individualmente las notificaciones críticas.
  - **RN-854:** Cada usuario podrá activar o desactivar categorías informativas desde sus preferencias.
  - **RN-855:** Las preferencias serán individuales y no afectarán las configuraciones de otros usuarios.
  - **RN-856:** El superadministrador administrará globalmente la clasificación de categorías.
  - **RN-857:** Los administradores de clubes y disciplinas no podrán modificar la clasificación global.
  - **RN-858:** Los fallos persistentes de conciliación podrán clasificarse como eventos críticos.
  - **RN-859:** Las notificaciones de exportaciones completadas podrán clasificarse como informativas.
  - **RN-860:** Las preferencias no modificarán el historial de operaciones, exportaciones ni auditorías.
  - **RN-861:** Las notificaciones respetarán los permisos y el alcance de cada usuario.
  - **RN-862:** Los cambios de clasificación global quedarán auditados.
  - **RN-863:** La configuración de preferencias no habilitará el envío por correo electrónico de avisos definidos exclusivamente como internos.
</box>

## Ejemplo — Preferencias de notificaciones

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Configuración del usuario**
    <badge>Ejemplo</badge>
  </row>
  <row align="center" justify="between">
    <box flex="1" gap={1}>
      **Fallos persistentes de conciliación**
      <text color="secondary" size="xs">Notificación crítica</text>
    </box>
    <row align="center" gap={1}>
      <icon name="lock" color="secondary" size="sm"/>
      <badge color="success">Obligatoria</badge>
    </row>
  </row>
  <divider color="subtle"/>
  {@body const [exportaciones,setExportaciones] = DIL.useState(true)}
  {@body const [avisosInformativos,setAvisosInformativos] = DIL.useState(false)}
  <checkbox checked={exportaciones} onChange={setExportaciones} lineThrough={false}>
    <box gap={1}>
      **Exportaciones completadas**
      <text color="secondary" size="xs">Notificación informativa</text>
    </box>
  </checkbox>
  <checkbox checked={avisosInformativos} onChange={setAvisosInformativos} lineThrough={false}>
    <box gap={1}>
      **Otros avisos informativos**
      <text color="secondary" size="xs">Notificación informativa</text>
    </box>
  </checkbox>
  <caption>Demostración interactiva de preferencias individuales; no modifica ninguna configuración real.</caption>
</box>

---

## Fase 3 — Cambios en la clasificación de notificaciones

<text color="secondary" weight="medium" size="xs">Pregunta 107 — ¿Qué sucede cuando el superadministrador cambia una categoría de informativa a crítica?</text>

Supongamos que las alertas de una categoría eran opcionales y algunos administradores las tenían desactivadas. El superadministrador decide convertirlas en críticas.

**¿Cómo debería aplicarse el cambio?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo para usuarios nuevos**

      Los usuarios existentes conservan sus preferencias anteriores. La nueva clasificación se aplica únicamente a usuarios creados posteriormente.
    </radio>
    <radio value="b">
      **Opción B — Aplicación inmediata sin historial**

      La nueva clasificación se aplica inmediatamente a todos los usuarios, reemplazando las preferencias anteriores.
    </radio>
    <radio value="c">
      **Opción C — Aplicación inmediata con auditoría y conservación de preferencias (recomendada)**

      La categoría pasa a ser obligatoria inmediatamente para todos los destinatarios autorizados, incluso si antes la tenían desactivada.

      <text color="secondary" size="xs">El sistema conserva las preferencias individuales anteriores para poder restaurarlas si la categoría vuelve a ser informativa. Registra quién realizó el cambio, cuándo y cuál era la clasificación anterior. No altera retroactivamente las notificaciones ya generadas.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 107 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"aplicar los cambios de clasificación de notificaciones solo a usuarios nuevos",b:"aplicar inmediatamente la nueva clasificación a todos los usuarios, reemplazando preferencias anteriores",c:"aplicar inmediatamente la clasificación crítica a todos los destinatarios autorizados, conservar preferencias previas para una posible restauración, auditar el cambio y no alterar notificaciones históricas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-107

### Registro 275 — Aplicación de cambios de clasificación
Fuente: `c513e38d-de12-4708-ad8a-e7eaf2b81548`

Respuesta previa del usuario:

Para la pregunta 107 del relevamiento SDD elijo la opción C: aplicar inmediatamente la clasificación crítica a todos los destinatarios autorizados, conservar preferencias previas para una posible restauración, auditar el cambio y no alterar notificaciones históricas.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 107 — Confirmada: opción C**
</row>

Los cambios de clasificación de notificaciones se aplicarán **inmediatamente a todos los destinatarios autorizados**, conservando las preferencias anteriores y sin modificar las notificaciones históricas.

## Registro SDD — Decisión 107

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-107 — Aplicación de cambios de clasificación**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-864:** Los cambios de clasificación realizados por el superadministrador tendrán efecto inmediato.
  - **RN-865:** Una categoría reclasificada como crítica será obligatoria para todos sus destinatarios autorizados.
  - **RN-866:** La obligatoriedad prevalecerá sobre las preferencias individuales que tuvieran desactivada esa categoría.
  - **RN-867:** El sistema conservará las preferencias individuales anteriores al cambio.
  - **RN-868:** Si la categoría vuelve a ser informativa, se restaurarán las preferencias individuales conservadas.
  - **RN-869:** Los usuarios podrán modificar nuevamente sus preferencias cuando la categoría sea informativa.
  - **RN-870:** Cada cambio registrará el superadministrador responsable, la fecha y la hora.
  - **RN-871:** Se conservarán la clasificación anterior y la nueva.
  - **RN-872:** Los cambios no modificarán retroactivamente las notificaciones ya generadas.
  - **RN-873:** Las notificaciones futuras utilizarán la clasificación vigente al momento de generarse.
  - **RN-874:** Los cambios no ampliarán los permisos ni el alcance de los destinatarios.
  - **RN-875:** El historial de cambios permanecerá disponible para auditoría.
</box>

## Ejemplo — Cambio de clasificación

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Alertas de conciliación**
    <badge>Ejemplo</badge>
  </row>
  <grid columns={2} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Antes</text>
        <badge>Informativa</badge>
        <text size="sm">Usuario la tenía desactivada.</text>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Después</text>
        <badge color="warning">Crítica</badge>
        <text size="sm">Recepción obligatoria.</text>
      </box>
    </grid-item>
  </grid>
  <row align="start" gap={2}>
    <icon name="history" color="secondary"/>
    <text size="sm">La preferencia anterior permanece guardada. Si la categoría vuelve a ser informativa, se restaura automáticamente.</text>
  </row>
</box>

---

## Fase 3 — Administración de categorías

<text color="secondary" weight="medium" size="xs">Pregunta 108 — ¿Cómo se administran las categorías de notificaciones?</text>

Ya definimos que el superadministrador puede clasificar las notificaciones como críticas o informativas.

Ahora necesitamos establecer si también podrá crear nuevas categorías, modificar sus nombres o desactivarlas cuando la plataforma incorpore funcionalidades.

**¿Qué nivel de administración preferís?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Categorías fijas**

      Las categorías estarán definidas en el código de la plataforma. Solo podrán cambiarse mediante una actualización del software.
    </radio>
    <radio value="b">
      **Opción B — Categorías predefinidas con clasificación editable**

      El sistema tendrá categorías predefinidas. El superadministrador podrá cambiar su clasificación entre crítica e informativa, pero no crear categorías nuevas.
    </radio>
    <radio value="c">
      **Opción C — Catálogo global administrable (recomendada)**

      El sistema tendrá categorías predefinidas y el superadministrador podrá crear, editar y desactivar categorías adicionales.

      <text color="secondary" size="xs">Cada categoría estará asociada a eventos reales del sistema, tendrá una clasificación y conservará su historial. Las categorías críticas esenciales no podrán desactivarse si eso impide notificar eventos obligatorios.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 108 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"mantener categorías de notificaciones fijas definidas en el código",b:"utilizar categorías predefinidas permitiendo al superadministrador cambiar solo su clasificación",c:"implementar un catálogo global de categorías predefinidas y administrables por el superadministrador, con creación, edición, desactivación controlada, asociación a eventos y auditoría"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-108

### Registro 277 — Catálogo global de categorías de notificaciones
Fuente: `76db966e-61a1-4451-9c36-2b092a2cb9b4`

Respuesta previa del usuario:

Para la pregunta 108 del relevamiento SDD elijo la opción C: implementar un catálogo global de categorías predefinidas y administrables por el superadministrador, con creación, edición, desactivación controlada, asociación a eventos y auditoría.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 108 — Confirmada: opción C**
</row>

La plataforma contará con un **catálogo global de categorías de notificaciones**, administrado por el superadministrador, con categorías predefinidas y posibilidad de crear, editar y desactivar otras.

## Registro SDD — Decisión 108

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-108 — Catálogo global de categorías de notificaciones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-876:** Existirá un catálogo global de categorías de notificaciones.
  - **RN-877:** El sistema incluirá categorías predefinidas para los eventos implementados.
  - **RN-878:** El superadministrador podrá crear nuevas categorías.
  - **RN-879:** Podrá editar el nombre, la descripción y la clasificación de las categorías administrables.
  - **RN-880:** Cada categoría estará asociada a uno o más eventos reales del sistema.
  - **RN-881:** Las categorías tendrán una clasificación crítica o informativa, conforme a DEC-106.
  - **RN-882:** El superadministrador podrá desactivar categorías cuando no sean necesarias.
  - **RN-883:** No se permitirá desactivar una categoría si eso impide entregar notificaciones críticas obligatorias.
  - **RN-884:** Las categorías desactivadas dejarán de utilizarse para generar nuevas notificaciones, salvo que exista una obligación crítica que deba preservarse.
  - **RN-885:** La desactivación no eliminará las notificaciones históricas ni sus auditorías.
  - **RN-886:** Los cambios de clasificación respetarán las reglas de aplicación inmediata y conservación de preferencias definidas en DEC-107.
  - **RN-887:** Cada modificación registrará usuario responsable, fecha, hora y valores anteriores y nuevos.
  - **RN-888:** Solo el superadministrador podrá administrar el catálogo global.
</box>

## Ejemplo — Catálogo de categorías

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Categorías de notificaciones**
    <badge>Ejemplo</badge>
  </row>
  <table>
    <table-row>
      <table-cell>**Categoría**</table-cell>
      <table-cell>**Clasificación**</table-cell>
      <table-cell>**Estado**</table-cell>
    </table-row>
    <table-row>
      <table-cell>Errores de conciliación</table-cell>
      <table-cell><badge color="warning">Crítica</badge></table-cell>
      <table-cell><badge color="success">Activa</badge></table-cell>
    </table-row>
    <table-row>
      <table-cell>Exportación completada</table-cell>
      <table-cell><badge>Informativa</badge></table-cell>
      <table-cell><badge color="success">Activa</badge></table-cell>
    </table-row>
    <table-row>
      <table-cell>Avisos operativos</table-cell>
      <table-cell><badge>Informativa</badge></table-cell>
      <table-cell><badge color="secondary">Inactiva</badge></table-cell>
    </table-row>
  </table>
  <caption>Catálogo ilustrativo. Crear una categoría no implica crear automáticamente un nuevo evento en el software.</caption>
</box>

---

## Fase 3 — Asociación entre eventos y notificaciones

<text color="secondary" weight="medium" size="xs">Pregunta 109 — ¿Cómo se vinculan los eventos del sistema con las categorías de notificaciones?</text>

Por ejemplo, cuando falla definitivamente una conciliación, el sistema debe saber qué categoría utilizar, qué usuarios deben recibir el aviso y si corresponde tratarlo como crítico.

**¿Quién debería poder configurar esa asociación?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Asociaciones fijas en el código**

      Cada evento tendrá su categoría y destinatarios definidos por los desarrolladores. Cualquier cambio requerirá una actualización del software.
    </radio>
    <radio value="b">
      **Opción B — Categorías configurables, destinatarios fijos**

      El superadministrador podrá cambiar la categoría asociada a cada evento, pero los destinatarios seguirán definidos en el sistema.
    </radio>
    <radio value="c">
      **Opción C — Asociaciones y destinatarios configurables con restricciones (recomendada)**

      El sistema tendrá un catálogo de eventos implementados. El superadministrador podrá asociarlos a categorías y configurar qué roles recibirán sus notificaciones.

      <text color="secondary" size="xs">Se respetará siempre el ámbito de club y disciplina. Los destinatarios obligatorios de eventos críticos no podrán eliminarse. Cada cambio quedará auditado y no modificará las notificaciones históricas.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 109 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"definir en el código las asociaciones entre eventos, categorías y destinatarios",b:"permitir configurar la categoría de cada evento, manteniendo destinatarios fijos",c:"permitir al superadministrador configurar asociaciones entre eventos implementados, categorías y roles destinatarios, respetando ámbitos, destinatarios críticos obligatorios y auditoría"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-109

### Registro 279 — Asociación configurable de eventos y destinatarios
Fuente: `4ce2b256-cf21-4918-af68-b1246b194a6a`

Respuesta previa del usuario:

Para la pregunta 109 del relevamiento SDD elijo la opción C: permitir al superadministrador configurar asociaciones entre eventos implementados, categorías y roles destinatarios, respetando ámbitos, destinatarios críticos obligatorios y auditoría.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 109 — Confirmada: opción C**
</row>

El superadministrador podrá configurar **qué eventos generan notificaciones, a qué categorías pertenecen y qué roles las reciben**, respetando los permisos y manteniendo obligatorios los destinatarios esenciales de eventos críticos.

## Registro SDD — Decisión 109

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-109 — Asociación configurable de eventos y destinatarios**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-889:** Existirá un catálogo de eventos del sistema que pueden generar notificaciones.
  - **RN-890:** Solo podrán configurarse eventos que estén implementados en la plataforma.
  - **RN-891:** El superadministrador podrá asociar cada evento con una categoría de notificación activa.
  - **RN-892:** Podrá configurar los roles destinatarios de cada evento.
  - **RN-893:** Los destinatarios se determinarán según el rol y el ámbito correspondiente al evento.
  - **RN-894:** Los administradores de disciplina solo recibirán notificaciones relacionadas con sus disciplinas autorizadas.
  - **RN-895:** Los delegados generales solo recibirán notificaciones correspondientes a sus clubes.
  - **RN-896:** El superadministrador podrá recibir notificaciones de alcance global.
  - **RN-897:** Los destinatarios obligatorios de eventos críticos no podrán eliminarse de su configuración.
  - **RN-898:** Las asociaciones no podrán ampliar los permisos de acceso a información financiera o personal.
  - **RN-899:** Los cambios se aplicarán a los eventos futuros, sin modificar las notificaciones históricas.
  - **RN-900:** Cada modificación registrará usuario, fecha, hora y configuración anterior y nueva.
  - **RN-901:** Las asociaciones respetarán la clasificación y las preferencias definidas en DEC-106 y DEC-107.
  - **RN-902:** Crear una categoría no permitirá generar eventos inexistentes en el software.
</box>

## Ejemplo — Configuración de eventos

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Evento: conciliación fallida**
    <badge>Ejemplo</badge>
  </row>
  <box background="surface-secondary" radius="md" padding={3} gap={2}>
    <row align="center" justify="between">
      <text color="secondary" size="sm">Categoría</text>
      <badge color="warning">Crítica</badge>
    </row>
    <divider color="subtle"/>
    <text color="secondary" size="xs">Destinatarios obligatorios</text>
    <row align="center" gap={2}>
      <icon name="lock" color="secondary"/>
      <text size="sm">Superadministrador</text>
    </row>
    <row align="center" gap={2}>
      <icon name="lock" color="secondary"/>
      <text size="sm">Administradores de la disciplina afectada</text>
    </row>
  </box>
  <caption>La configuración respeta los destinatarios obligatorios aprobados en DEC-092.</caption>
</box>

---

## Fase 3 — Prevención de notificaciones duplicadas

<text color="secondary" weight="medium" size="xs">Pregunta 110 — ¿Cómo evitamos que un mismo evento genere notificaciones repetidas?</text>

Supongamos que una conciliación falla y el sistema realiza varios reintentos automáticos. No queremos que los administradores reciban diez notificaciones idénticas por el mismo problema.

**¿Cómo debería controlar esto la plataforma?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Notificar cada evento**

      Cada vez que se detecta el problema, se genera una nueva notificación, aunque sea el mismo incidente.
    </radio>
    <radio value="b">
      **Opción B — Una notificación por período fijo**

      El sistema limita los avisos repetidos, por ejemplo, a uno cada 24 horas para el mismo problema.
    </radio>
    <radio value="c">
      **Opción C — Agrupación por incidente y actualización de estado (recomendada)**

      El sistema identifica cada incidente mediante una clave única y genera una sola notificación activa por incidente y destinatario.

      <text color="secondary" size="xs">Los nuevos reintentos actualizan el incidente existente sin generar avisos duplicados. Cuando se resuelve, se actualiza su estado. Si ocurre un incidente nuevo e independiente, se genera una nueva notificación. Se conserva toda la trazabilidad técnica.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 110 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"generar una notificación por cada ocurrencia del evento aunque se repita",b:"limitar las notificaciones repetidas a una por período fijo",c:"agrupar notificaciones por incidente y destinatario, actualizar el incidente durante reintentos, resolver su estado sin duplicados y conservar trazabilidad"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-110

### Registro 281 — Agrupación y actualización de notificaciones por incidente
Fuente: `0746a675-e182-4847-9422-9b212bac395c`

Respuesta previa del usuario:

Para la pregunta 110 del relevamiento SDD elijo la opción C: agrupar notificaciones por incidente y destinatario, actualizar el incidente durante reintentos, resolver su estado sin duplicados y conservar trazabilidad.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 110 — Confirmada: opción C**
</row>

La plataforma agrupará las notificaciones relacionadas con un mismo incidente, evitando avisos duplicados durante los reintentos y conservando toda la trazabilidad.

## Registro SDD — Decisión 110

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-110 — Agrupación y actualización de notificaciones por incidente**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-903:** Cada incidente notificable tendrá un identificador único.
  - **RN-904:** El sistema asociará las notificaciones al incidente que las originó.
  - **RN-905:** Existirá como máximo una notificación activa por incidente y destinatario.
  - **RN-906:** Los reintentos automáticos de una operación no generarán notificaciones duplicadas del mismo incidente.
  - **RN-907:** Las nuevas ocurrencias del mismo incidente actualizarán su información y estado.
  - **RN-908:** La resolución del incidente actualizará el estado de la notificación correspondiente.
  - **RN-909:** Un incidente nuevo e independiente podrá generar una nueva notificación, aunque pertenezca a la misma categoría.
  - **RN-910:** El sistema conservará el historial de intentos y errores asociados al incidente.
  - **RN-911:** La agrupación no eliminará registros técnicos ni auditorías.
  - **RN-912:** Las notificaciones agrupadas respetarán los destinatarios y ámbitos autorizados.
  - **RN-913:** El sistema deberá impedir duplicados incluso ante procesamiento concurrente o reintentos de entrega.
  - **RN-914:** El estado del incidente será independiente del estado leída/no leída de cada destinatario.
  - **RN-915:** La agrupación no modificará los registros originales de pagos, conciliaciones o exportaciones.
</box>

## Ejemplo — Conciliación con errores

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Incidente #CON-2048**
    <badge color="warning">En seguimiento</badge>
  </row>
  <list marker="number" connector="solid" gap={3}>
    <list-item>
      **Primer fallo**

      <text color="secondary" size="sm">Se identifica el incidente y, cuando corresponde notificarlo, se genera el aviso para cada destinatario.</text>
    </list-item>
    <list-item>
      **Segundo y tercer intento fallidos**

      <text color="secondary" size="sm">Se actualiza el incidente existente. No se generan avisos duplicados.</text>
    </list-item>
    <list-item>
      **Conciliación recuperada**

      <text color="secondary" size="sm">El incidente pasa a resuelto y conserva el historial de intentos.</text>
    </list-item>
  </list>
  <row align="center" justify="between">
    <text color="secondary" size="sm">Notificaciones activas por destinatario</text>
    <title size="lg" color="default" tabularNums>1</title>
  </row>
  <caption>La notificación inicial de fallos persistentes sigue respetando el agotamiento de reintentos definido en DEC-092.</caption>
</box>

---

## Fase 3 — Resolución de incidentes y notificaciones

<text color="secondary" weight="medium" size="xs">Pregunta 111 — ¿Qué sucede con una notificación cuando su incidente se resuelve?</text>

Por ejemplo, un administrador recibe una alerta crítica porque una conciliación falló. Más tarde, el sistema consigue reconciliar correctamente la operación.

**¿Cómo querés que se refleje esa resolución en el centro de notificaciones?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Desaparición inmediata**

      La notificación desaparece automáticamente cuando el incidente se resuelve, sin permanecer visible en el historial del usuario.
    </radio>
    <radio value="b">
      **Opción B — Mantener la notificación sin cambios**

      La notificación permanece exactamente igual hasta que venza su período de retención, aunque el problema ya esté resuelto.
    </radio>
    <radio value="c">
      **Opción C — Actualizar a resuelta y conservar en historial (recomendada)**

      La notificación cambia a estado *resuelta*, deja de mostrarse entre los incidentes activos y permanece consultable en el historial hasta cumplir su período de retención.

      <text color="secondary" size="xs">Se registra cuándo se resolvió y se mantiene independiente el estado leída/no leída. No se genera una segunda notificación por la resolución. Las alertas activas desaparecen como se definió para conciliaciones y avisos comerciales.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 111 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"hacer desaparecer inmediatamente las notificaciones cuando se resuelve el incidente",b:"mantener sin cambios las notificaciones hasta vencer su retención aunque el incidente se resuelva",c:"actualizar la notificación a resuelta, retirarla de incidentes activos, conservarla en historial hasta su retención y mantener independiente el estado de lectura"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-111

### Registro 283 — Resolución y conservación de notificaciones
Fuente: `4b32cd7f-768d-42d5-957b-6be191efacbc`

Respuesta previa del usuario:

Para la pregunta 111 del relevamiento SDD elijo la opción C: actualizar la notificación a resuelta, retirarla de incidentes activos, conservarla en historial hasta su retención y mantener independiente el estado de lectura.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 111 — Confirmada: opción C**
</row>

Cuando un incidente se resuelva, su notificación se actualizará automáticamente a **resuelta**, desaparecerá de los incidentes activos y permanecerá en el historial hasta cumplir el plazo de retención.

## Registro SDD — Decisión 111

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-111 — Resolución y conservación de notificaciones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-916:** Las notificaciones vinculadas a incidentes tendrán un estado de resolución.
  - **RN-917:** Cuando el incidente se resuelva, sus notificaciones asociadas pasarán automáticamente a estado *resuelta*.
  - **RN-918:** Las notificaciones resueltas dejarán de aparecer entre los incidentes activos.
  - **RN-919:** Permanecerán disponibles en el historial del destinatario hasta vencer su período de retención.
  - **RN-920:** Se registrarán la fecha y hora de resolución del incidente.
  - **RN-921:** La resolución no generará una segunda notificación para el mismo incidente.
  - **RN-922:** El estado de lectura será independiente del estado de resolución.
  - **RN-923:** Una notificación podrá estar resuelta y continuar sin leer.
  - **RN-924:** La resolución no marcará automáticamente la notificación como leída.
  - **RN-925:** Cada destinatario conservará su propio estado de lectura.
  - **RN-926:** El historial técnico y las auditorías permanecerán disponibles según sus políticas de conservación.
  - **RN-927:** La expiración de visibilidad respetará la configuración global establecida en DEC-105.
  - **RN-928:** La actualización del estado no modificará los datos financieros ni las operaciones originales.
</box>

## Ejemplo — Ciclo de una notificación crítica

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Conciliación #2048**
    <badge color="success">Resuelta</badge>
  </row>
  <list marker="number" connector="solid" gap={3}>
    <list-item>
      **10:00 — Se detecta un incidente crítico**

      <text color="secondary" size="sm">Tras agotarse los reintentos, se genera la notificación obligatoria.</text>
    </list-item>
    <list-item>
      **10:15 — Se recupera la conciliación**

      <text color="secondary" size="sm">La notificación cambia a resuelta y sale de la vista de incidentes activos.</text>
    </list-item>
    <list-item>
      **10:30 — El administrador consulta el historial**

      <text color="secondary" size="sm">Puede consultar el incidente resuelto y marcar la notificación como leída.</text>
    </list-item>
  </list>
  <grid columns={2} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Estado del incidente</text>
        <row align="center" gap={1}>
          <icon name="check-circle-2" color="success"/>
          **Resuelto**
        </row>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <text color="secondary" size="xs">Estado de lectura</text>
        <row align="center" gap={1}>
          <icon name="mail" color="secondary"/>
          **Sin leer**
        </row>
      </box>
    </grid-item>
  </grid>
</box>

---

## Fase 3 — Historial de notificaciones

<text color="secondary" weight="medium" size="xs">Pregunta 112 — ¿Qué filtros debería tener el centro de notificaciones?</text>

Ya definimos los estados de lectura, resolución, clasificación y retención. Ahora necesitamos establecer cómo podrán los usuarios encontrar notificaciones anteriores.

**¿Qué nivel de búsqueda y filtrado querés implementar?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Listado cronológico simple**

      Mostrar todas las notificaciones ordenadas por fecha, de la más reciente a la más antigua, sin filtros.
    </radio>
    <radio value="b">
      **Opción B — Filtros básicos**

      Permitir filtrar por leídas/no leídas, categoría y rango de fechas.
    </radio>
    <radio value="c">
      **Opción C — Filtros avanzados y búsqueda (recomendada)**

      Permitir filtrar por estado de lectura, estado del incidente, categoría, criticidad y rango de fechas.

      <text color="secondary" size="xs">Incluir búsqueda por texto o identificador de incidente, orden cronológico y paginación. Para usuarios con acceso a múltiples ámbitos, permitir filtrar por club y disciplina autorizados, sin mostrar información fuera de sus permisos.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 112 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"mostrar las notificaciones en un listado cronológico sin filtros",b:"permitir filtros básicos por lectura, categoría y fechas",c:"implementar filtros avanzados por lectura, resolución, categoría, criticidad, fechas, club y disciplina autorizados, búsqueda por texto o incidente y paginación"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-112

### Registro 285 — Búsqueda y filtrado de notificaciones
Fuente: `fc8ce25a-52ff-47c0-9a56-ace62928546c`

Respuesta previa del usuario:

Para la pregunta 112 del relevamiento SDD elijo la opción C: implementar filtros avanzados por lectura, resolución, categoría, criticidad, fechas, club y disciplina autorizados, búsqueda por texto o incidente y paginación.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 112 — Confirmada: opción C**
</row>

El centro de notificaciones contará con **filtros avanzados, búsqueda y paginación**, respetando los permisos de cada usuario y los ámbitos de club y disciplina.

## Registro SDD — Decisión 112

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-112 — Búsqueda y filtrado de notificaciones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-929:** El centro de notificaciones permitirá consultar el historial visible del usuario.
  - **RN-930:** Se podrá filtrar por estado de lectura: todas, leídas y no leídas.
  - **RN-931:** Se podrá filtrar por estado del incidente: activo o resuelto, cuando corresponda.
  - **RN-932:** Se podrá filtrar por categoría de notificación.
  - **RN-933:** Se podrá filtrar por clasificación crítica o informativa.
  - **RN-934:** Se podrá filtrar por rango de fechas.
  - **RN-935:** Los usuarios con acceso a varios clubes podrán filtrar por los clubes autorizados.
  - **RN-936:** Los usuarios con acceso a varias disciplinas podrán filtrar por las disciplinas autorizadas.
  - **RN-937:** Se permitirá buscar por texto dentro de la información visible de las notificaciones.
  - **RN-938:** Se permitirá buscar por identificador de incidente.
  - **RN-939:** Las notificaciones se ordenarán por fecha, mostrando primero las más recientes.
  - **RN-940:** Los resultados tendrán paginación.
  - **RN-941:** Los filtros y búsquedas deberán respetar los permisos vigentes del usuario.
  - **RN-942:** La búsqueda no permitirá consultar notificaciones de otros destinatarios sin autorización.
  - **RN-943:** Las notificaciones retiradas por vencimiento de retención no aparecerán en los resultados.
  - **RN-944:** Los filtros no modificarán el estado de lectura ni el estado de resolución.
</box>

## Ejemplo — Centro de notificaciones

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Mis notificaciones**
    <badge>Vista ilustrativa</badge>
  </row>
  {@body const [lectura,setLectura] = DIL.useState("todas")}
  {@body const [estado,setEstado] = DIL.useState("todos")}
  {@body const [busqueda,setBusqueda] = DIL.useState("")}
  {@body const ejemplos = [{id:"CON-2048",titulo:"Conciliación fallida",estado:"resuelto",lectura:"no-leida",categoria:"Crítica"},{id:"EXP-1052",titulo:"Exportación completada",estado:"sin-incidente",lectura:"leida",categoria:"Informativa"},{id:"CON-2051",titulo:"Conciliación pendiente",estado:"activo",lectura:"no-leida",categoria:"Crítica"}]}
  {@body const filtradas = ejemplos.filter(n=>(lectura==="todas"||n.lectura===lectura)&&(estado==="todos"||n.estado===estado)&&(n.titulo+" "+n.id).toLowerCase().includes(busqueda.toLowerCase()))}
  <input placeholder="Buscar por texto o ID de incidente..." value={busqueda} onChange={setBusqueda}/>
  <grid columns={2} gap={2}>
    <grid-item>
      <label>Lectura</label>
      <select block value={lectura} onChange={setLectura} options={[{value:"todas",label:"Todas"},{value:"leida",label:"Leídas"},{value:"no-leida",label:"No leídas"}]}/>
    </grid-item>
    <grid-item>
      <label>Incidente</label>
      <select block value={estado} onChange={setEstado} options={[{value:"todos",label:"Todos"},{value:"activo",label:"Activos"},{value:"resuelto",label:"Resueltos"}]}/>
    </grid-item>
  </grid>
  <divider color="subtle"/>
  {#each filtradas as n}
    <row key={n.id} align="center" gap={2}>
      <icon name={n.lectura==="no-leida"?"mail":"mail-open"} color="secondary"/>
      <box flex="1" gap="2px">
        <text weight="medium">{n.titulo}</text>
        <text size="xs" color="secondary">{n.id} · {n.categoria}</text>
      </box>
      <badge color={n.estado==="activo"?"warning":n.estado==="resuelto"?"success":"secondary"}>{n.estado==="sin-incidente"?"Informativa":n.estado==="activo"?"Activo":"Resuelto"}</badge>
    </row>
  {/each}
  {#if filtradas.length===0}
    <text color="secondary" size="sm" textAlign="center">No hay notificaciones que coincidan con los filtros.</text>
  {/if}
  <caption>Demostración interactiva con datos ficticios. El sistema real incorporará todos los filtros aprobados.</caption>
</box>

---

## Fase 3 — Actualización del centro de notificaciones

<text color="secondary" weight="medium" size="xs">Pregunta 113 — ¿Cómo deberían aparecer las nuevas notificaciones mientras el usuario está conectado?</text>

Por ejemplo, un administrador está trabajando en el panel de cuotas y, en ese momento, se genera una notificación crítica.

**¿Querés que el sistema le avise inmediatamente o que deba actualizar la página?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Actualización manual**

      Las nuevas notificaciones aparecen cuando el usuario actualiza la página o vuelve a ingresar al centro de notificaciones.
    </radio>
    <radio value="b">
      **Opción B — Consulta automática periódica**

      El sistema consulta cada cierto tiempo si existen nuevas notificaciones y actualiza el contador y el listado.
    </radio>
    <radio value="c">
      **Opción C — Tiempo real con mecanismo de respaldo (recomendada)**

      Las notificaciones nuevas aparecen automáticamente mientras el usuario está conectado, sin recargar la página.

      <text color="secondary" size="xs">El contador de no leídas se actualiza en tiempo real. Si la conexión en tiempo real se interrumpe, el sistema utiliza consultas periódicas o sincroniza al reconectarse. Las notificaciones quedan persistidas aunque el usuario esté desconectado.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 113 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"mostrar nuevas notificaciones solamente al actualizar o ingresar nuevamente",b:"actualizar notificaciones mediante consultas automáticas periódicas",c:"entregar y actualizar notificaciones en tiempo real con respaldo mediante consultas periódicas o resincronización, persistiendo avisos durante desconexiones"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-113

### Registro 288 — Notificaciones en tiempo real y recuperación ante desconexiones
Fuente: `2fb35a82-e528-4153-a865-9bab3f68d33b`

Respuesta previa del usuario:

Para la pregunta 113 del relevamiento SDD elijo la opción C: entregar y actualizar notificaciones en tiempo real con respaldo mediante consultas periódicas o resincronización, persistiendo avisos durante desconexiones.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 113 — Confirmada: opción C**
</row>

El centro de notificaciones funcionará **en tiempo real**, sin necesidad de actualizar la página. Si se interrumpe la conexión, el sistema podrá recuperar las notificaciones pendientes mediante sincronización automática.

## Registro SDD — Decisión 113

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-113 — Notificaciones en tiempo real y recuperación ante desconexiones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-945:** El sistema permitirá entregar notificaciones internas en tiempo real a usuarios conectados.
  - **RN-946:** Las nuevas notificaciones aparecerán sin necesidad de recargar la página.
  - **RN-947:** El contador de notificaciones no leídas se actualizará automáticamente.
  - **RN-948:** Los cambios de estado de lectura y resolución se reflejarán en la interfaz.
  - **RN-949:** Las notificaciones se almacenarán de forma persistente, independientemente de que el destinatario esté conectado.
  - **RN-950:** Una desconexión temporal no provocará la pérdida de notificaciones.
  - **RN-951:** Si falla el canal de tiempo real, el sistema dispondrá de un mecanismo de consulta periódica como respaldo.
  - **RN-952:** Al restablecerse la conexión, se sincronizarán las notificaciones y sus estados vigentes.
  - **RN-953:** La sincronización evitará duplicados, respetando la agrupación por incidente definida en DEC-110.
  - **RN-954:** Las actualizaciones se enviarán exclusivamente a los destinatarios autorizados.
  - **RN-955:** El sistema validará la autenticación y los permisos de las conexiones.
  - **RN-956:** La recuperación respetará la retención de notificaciones establecida en DEC-105.
  - **RN-957:** Los errores de entrega y sincronización quedarán registrados para diagnóstico.
  - **RN-958:** La falta de conexión en tiempo real no impedirá que el usuario utilice las demás funcionalidades disponibles de la plataforma.
</box>

## Ejemplo — Recuperación automática

<box border radius="lg" padding={3} gap={3}>
  <list marker="number" connector="solid" gap={3}>
    <list-item>
      **10:00 — Administrador conectado**

      <text color="secondary" size="sm">Recibe una notificación crítica inmediatamente en su panel.</text>
    </list-item>
    <list-item>
      **10:05 — Se pierde la conexión**

      <text color="secondary" size="sm">El sistema conserva las nuevas notificaciones que se generan durante la desconexión.</text>
    </list-item>
    <list-item>
      **10:15 — Se recupera la conexión**

      <text color="secondary" size="sm">Se sincronizan automáticamente las notificaciones pendientes y sus estados actuales.</text>
    </list-item>
  </list>
  <row align="center" justify="between">
    **Resultado**
    <badge color="success"><icon name="check" inline/> Sin pérdida de avisos</badge>
  </row>
</box>

---

## Fase 3 — Notificaciones en múltiples dispositivos

<text color="secondary" weight="medium" size="xs">Pregunta 114 — ¿Cómo sincronizamos las notificaciones cuando un usuario tiene varias sesiones abiertas?</text>

Por ejemplo, un administrador tiene abierta la plataforma en su computadora y en su celular. Lee una notificación en la computadora.

**¿Qué debería suceder en el celular?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Estados independientes por dispositivo**

      Cada dispositivo mantiene sus propios estados de lectura. Leer una notificación en la computadora no la marca como leída en el celular.
    </radio>
    <radio value="b">
      **Opción B — Sincronización al actualizar**

      Los estados son compartidos entre dispositivos, pero los cambios se reflejan únicamente cuando se actualiza la página o se vuelve a ingresar.
    </radio>
    <radio value="c">
      **Opción C — Sincronización inmediata entre sesiones (recomendada)**

      Todas las sesiones del mismo usuario comparten los estados de lectura y el contador de notificaciones.

      <text color="secondary" size="xs">Si marca una notificación como leída en la computadora, se actualiza automáticamente en el celular. Si algún dispositivo está desconectado, se sincroniza al reconectarse. Las preferencias individuales también son compartidas entre sesiones y no se mezclan con otras cuentas.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 114 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"mantener estados de notificaciones independientes por dispositivo",b:"compartir estados entre dispositivos pero sincronizarlos solo al actualizar",c:"sincronizar inmediatamente entre sesiones del mismo usuario estados de lectura, contadores y preferencias, con recuperación tras desconexión y aislamiento entre cuentas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-114

### Registro 290 — Sincronización de notificaciones entre sesiones
Fuente: `06ef2f5d-b48a-44cd-a571-5b885a594e47`

Respuesta previa del usuario:

Para la pregunta 114 del relevamiento SDD elijo la opción C: sincronizar inmediatamente entre sesiones del mismo usuario estados de lectura, contadores y preferencias, con recuperación tras desconexión y aislamiento entre cuentas.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 114 — Confirmada: opción C**
</row>

Todas las sesiones de una misma cuenta compartirán **estados de lectura, contadores y preferencias de notificaciones**, con sincronización inmediata y recuperación automática ante desconexiones.

## Registro SDD — Decisión 114

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-114 — Sincronización de notificaciones entre sesiones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-959:** Los estados de lectura se almacenarán por cuenta de usuario, no por dispositivo.
  - **RN-960:** Todas las sesiones activas de una misma cuenta compartirán el estado de las notificaciones.
  - **RN-961:** Marcar una notificación como leída actualizará automáticamente las demás sesiones conectadas.
  - **RN-962:** La acción de marcar todas como leídas se sincronizará entre sesiones.
  - **RN-963:** Los contadores de notificaciones no leídas serán consistentes entre dispositivos.
  - **RN-964:** Las preferencias individuales se almacenarán centralmente por cuenta.
  - **RN-965:** Los cambios de preferencias se reflejarán en las demás sesiones activas.
  - **RN-966:** Las sesiones desconectadas recuperarán los estados vigentes al reconectarse.
  - **RN-967:** La sincronización evitará duplicados y operaciones inconsistentes.
  - **RN-968:** Las cuentas independientes no compartirán notificaciones, aunque utilicen el mismo correo electrónico.
  - **RN-969:** Cada sesión deberá estar autenticada para recibir actualizaciones.
  - **RN-970:** Al perder permisos o cerrar sesión, se interrumpirá el acceso a las notificaciones correspondientes.
  - **RN-971:** Los cambios de estado respetarán la retención y los permisos definidos anteriormente.
  - **RN-972:** La sincronización no modificará el historial financiero ni los registros de auditoría.
</box>

## Ejemplo — Dos dispositivos conectados

<box border radius="lg" padding={3} gap={3}>
  <grid columns={2} gap={3}>
    <grid-item>
      <box background="surface-secondary" radius="lg" padding={3} align="center" gap={2}>
        <icon name="monitor" size="2xl" color="secondary"/>
        **Computadora**
        <badge color="success">Conectada</badge>
        <text size="xs" color="secondary" textAlign="center">Marca una notificación como leída</text>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="lg" padding={3} align="center" gap={2}>
        <icon name="smartphone" size="2xl" color="secondary"/>
        **Celular**
        <badge color="success">Sincronizado</badge>
        <text size="xs" color="secondary" textAlign="center">Actualiza automáticamente su contador</text>
      </box>
    </grid-item>
  </grid>
  <row align="center" justify="center" gap={2}>
    <icon name="refresh-cw" color="success"/>
    <text size="sm" weight="medium">Una misma cuenta, estados compartidos</text>
  </row>
  <caption>Las cuentas independientes de distintos clubes permanecen aisladas, incluso si tienen el mismo email.</caption>
</box>

---

## Fase 3 — Presentación de alertas críticas

<text color="secondary" weight="medium" size="xs">Pregunta 115 — ¿Cómo debería avisar la plataforma cuando llega una notificación crítica?</text>

Ya definimos que las notificaciones se reciben en tiempo real. Ahora falta decidir cómo se presentan visualmente mientras el administrador está trabajando.

**¿Qué comportamiento preferís para las notificaciones críticas?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo contador**

      Las notificaciones críticas aparecen en el centro de notificaciones y aumentan el contador, sin mostrar avisos adicionales.
    </radio>
    <radio value="b">
      **Opción B — Aviso emergente para todas**

      Cada notificación nueva genera un aviso emergente, independientemente de su criticidad.
    </radio>
    <radio value="c">
      **Opción C — Avisos diferenciados por criticidad (recomendada)**

      Las notificaciones críticas muestran un aviso visual destacado dentro de la plataforma, además de actualizar el centro de notificaciones.

      <text color="secondary" size="xs">Las informativas actualizan el contador sin interrumpir el trabajo. Los avisos críticos no bloquean la pantalla, pueden cerrarse sin marcar la notificación como leída y no se repiten innecesariamente por cada reintento del mismo incidente. No se requieren permisos de notificaciones del navegador.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 115 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"mostrar todas las notificaciones únicamente en el centro y su contador",b:"mostrar un aviso emergente para cada notificación nueva sin distinguir criticidad",c:"mostrar avisos visuales destacados no bloqueantes para notificaciones críticas, mantener las informativas en el contador y evitar emergentes duplicados por incidente"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-115

### Registro 292 — Presentación visual de notificaciones según criticidad
Fuente: `9d965e9a-4d6c-4b20-90cc-317eef11ba2e`

Respuesta previa del usuario:

Para la pregunta 115 del relevamiento SDD elijo la opción C: mostrar avisos visuales destacados no bloqueantes para notificaciones críticas, mantener las informativas en el contador y evitar emergentes duplicados por incidente.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 115 — Confirmada: opción C**
</row>

Las notificaciones críticas tendrán **avisos visuales destacados, no bloqueantes**, mientras que las informativas se mostrarán en el centro de notificaciones y su contador, sin interrumpir al usuario.

## Registro SDD — Decisión 115

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-115 — Presentación visual de notificaciones según criticidad**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-973:** Las notificaciones críticas generarán un aviso visual destacado dentro de la plataforma.
  - **RN-974:** Los avisos críticos no bloquearán la interacción con el sistema.
  - **RN-975:** Las notificaciones informativas actualizarán el centro y su contador sin generar avisos emergentes.
  - **RN-976:** Los avisos críticos incluirán información suficiente para identificar el problema.
  - **RN-977:** Los avisos podrán incluir un acceso al detalle del incidente, sujeto a los permisos del usuario.
  - **RN-978:** El usuario podrá cerrar el aviso visual sin eliminar la notificación.
  - **RN-979:** Cerrar el aviso no marcará automáticamente la notificación como leída.
  - **RN-980:** Cerrar el aviso no modificará el estado del incidente.
  - **RN-981:** Los reintentos de un mismo incidente no generarán avisos emergentes duplicados.
  - **RN-982:** Un incidente nuevo e independiente podrá generar un nuevo aviso crítico.
  - **RN-983:** Los avisos respetarán los destinatarios obligatorios y los ámbitos autorizados.
  - **RN-984:** La presentación no dependerá de permisos de notificaciones del navegador.
  - **RN-985:** Las notificaciones seguirán disponibles en el centro aunque se cierre su aviso visual.
  - **RN-986:** Los cambios de clasificación de categorías respetarán DEC-107 para los avisos futuros.
</box>

## Ejemplo — Alerta crítica dentro del panel

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Panel de administración**
    <row align="center" gap={2}>
      <icon name="bell" color="secondary"/>
      <badge color="danger">2</badge>
    </row>
  </row>
  {@body const [visible,setVisible] = DIL.useState(true)}
  {#if visible}
    <box background="surface-secondary" border={{size:1,color:"default"}} radius="lg" padding={3} gap={2}>
      <row align="start" gap={2}>
        <icon name="alert-triangle" color="danger" size="lg"/>
        <box flex="1" gap={1}>
          <row align="center" justify="between">
            **Conciliación fallida**
            <badge color="danger">Crítica</badge>
          </row>
          <text size="sm">La operación #CON-2048 agotó los reintentos automáticos y requiere revisión.</text>
          <text size="xs" color="secondary">Obras Mendoza · Futsal</text>
        </box>
        <button size="xs" variant="ghost" color="secondary" onClick={()=>setVisible(false)}><icon name="x"/></button>
      </row>
      <row justify="end">
        <button size="sm" variant="outline" color="secondary" onClick={()=>GenUI.issueNewTurn("Continuemos el relevamiento SDD considerando el acceso seguro al detalle de incidentes críticos desde sus notificaciones.")}>Ver detalle del incidente <icon name="arrow-right" inline/></button>
      </row>
    </box>
  {:else}
    <row align="center" gap={2}>
      <icon name="check" color="success"/>
      <text color="secondary" size="sm">Aviso visual cerrado. La notificación permanece sin leer en el centro.</text>
      <button size="xs" variant="outline" color="secondary" onClick={()=>setVisible(true)}>Mostrar ejemplo</button>
    </row>
  {/if}
  <caption>Vista ilustrativa. Cerrar el aviso no cambia el contador ni el estado del incidente.</caption>
</box>

---

## Fase 3 — Seguridad de las notificaciones

<text color="secondary" weight="medium" size="xs">Pregunta 116 — ¿Qué sucede con las notificaciones cuando un usuario pierde permisos?</text>

Supongamos que un administrador deja de pertenecer a una disciplina. Tenía notificaciones anteriores sobre pagos, conciliaciones y exportaciones de esa disciplina.

**¿Qué debería poder consultar después de perder el acceso?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Conservar acceso al historial anterior**

      El usuario podrá seguir consultando todas las notificaciones recibidas anteriormente, aunque ya no tenga permisos sobre la disciplina.
    </radio>
    <radio value="b">
      **Opción B — Eliminar todas las notificaciones relacionadas**

      Al perder permisos, se eliminarán definitivamente sus notificaciones de esa disciplina, incluso del historial.
    </radio>
    <radio value="c">
      **Opción C — Revocación inmediata con conservación de auditoría (recomendada)**

      El usuario dejará de recibir y consultar las notificaciones correspondientes al ámbito del que fue desvinculado.

      <text color="secondary" size="xs">El sistema revocará el acceso incluso en sesiones abiertas y enlaces anteriores. Las notificaciones y sus registros históricos se conservarán internamente según las políticas vigentes, pero no estarán disponibles para el usuario sin autorización. Si recupera permisos, se evaluará su acceso nuevamente según su rol y las reglas de retención.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 116 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir consultar notificaciones históricas aunque se hayan perdido permisos",b:"eliminar definitivamente las notificaciones del ámbito al perder permisos",c:"revocar inmediatamente la recepción y consulta de notificaciones del ámbito sin permisos, conservar historial y auditoría internos, y revalidar acceso ante futuros cambios de rol"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-116

### Registro 294 — Revocación de acceso a notificaciones por cambios de permisos
Fuente: `4e9b2ee8-7737-4c6d-a100-8c7fa87591a3`

Respuesta previa del usuario:

Para la pregunta 116 del relevamiento SDD elijo la opción C: revocar inmediatamente la recepción y consulta de notificaciones del ámbito sin permisos, conservar historial y auditoría internos, y revalidar acceso ante futuros cambios de rol.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 116 — Confirmada: opción C**
</row>

Cuando un usuario pierda permisos sobre un club o disciplina, se revocará inmediatamente su acceso a las notificaciones correspondientes, **sin eliminar los registros históricos ni las auditorías internas**.

## Registro SDD — Decisión 116

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-116 — Revocación de acceso a notificaciones por cambios de permisos**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-987:** Las notificaciones estarán vinculadas a sus destinatarios y al ámbito de club o disciplina correspondiente.
  - **RN-988:** La pérdida de permisos revocará inmediatamente la recepción de nuevas notificaciones del ámbito afectado.
  - **RN-989:** El usuario dejará de poder consultar las notificaciones históricas de ese ámbito.
  - **RN-990:** La revocación se aplicará también a las sesiones que permanezcan abiertas.
  - **RN-991:** Los enlaces anteriores hacia notificaciones o incidentes protegidos dejarán de conceder acceso.
  - **RN-992:** El sistema revalidará los permisos al consultar una notificación o su información asociada.
  - **RN-993:** La revocación no eliminará los registros históricos ni las auditorías internas.
  - **RN-994:** Los registros conservarán las políticas de retención correspondientes.
  - **RN-995:** Si el usuario recupera permisos, su acceso se evaluará nuevamente según el rol y ámbito vigentes.
  - **RN-996:** Recuperar permisos no restaurará notificaciones que ya hayan vencido su período de visibilidad.
  - **RN-997:** La pérdida de acceso a una disciplina no afectará los permisos que el usuario conserve sobre otras disciplinas autorizadas.
  - **RN-998:** Los contadores, búsquedas y filtros excluirán las notificaciones que el usuario ya no esté autorizado a consultar.
  - **RN-999:** Los cambios de permisos y las revocaciones quedarán registrados para auditoría.
</box>

## Ejemplo — Administrador desvinculado

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Acceso a Obras Futsal**
    <badge color="danger">Revocado</badge>
  </row>
  <list marker="number" connector="solid" gap={3}>
    <list-item>
      **Antes de la desvinculación**

      <text color="secondary" size="sm">El administrador puede consultar las notificaciones de conciliaciones, pagos y exportaciones de su disciplina.</text>
    </list-item>
    <list-item>
      **Al perder permisos**

      <text color="secondary" size="sm">Las notificaciones desaparecen de su vista y se bloquean los accesos anteriores, incluso si mantiene abierta una sesión.</text>
    </list-item>
    <list-item>
      **Si recupera el acceso**

      <text color="secondary" size="sm">Se vuelven a validar sus permisos. Solo podrá consultar las notificaciones todavía visibles y autorizadas.</text>
    </list-item>
  </list>
</box>

---

## Fase 4 — Portal de jugadores y responsables de pago

<text color="secondary" weight="medium" size="xs">Pregunta 117 — ¿Cómo deberían identificarse los jugadores o responsables para consultar y pagar cuotas?</text>

Ya definimos gran parte de la administración, los pagos y las notificaciones. Ahora necesitamos precisar un aspecto fundamental del portal público: **cómo impedir que cualquier persona consulte las deudas de un jugador con solo conocer su DNI**.

**¿Qué mecanismo de acceso preferís?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Consulta únicamente por DNI**

      El usuario ingresa el DNI del jugador y puede visualizar sus cuotas pendientes y pagarlas, sin verificaciones adicionales.

      <text color="secondary" size="xs">Es la opción más sencilla, pero permite consultar información de terceros conociendo su documento.</text>
    </radio>
    <radio value="b">
      **Opción B — Cuenta con email y contraseña**

      Cada jugador o responsable debe registrarse e iniciar sesión para consultar cuotas y realizar pagos.

      <text color="secondary" size="xs">Ofrece un acceso autenticado, pero requiere registro, gestión de contraseñas y recuperación de cuentas.</text>
    </radio>
    <radio value="c">
      **Opción C — DNI más verificación de identidad sin contraseña (recomendada)**

      El usuario ingresa el DNI y el sistema envía un código temporal al email o teléfono previamente registrado y verificado para ese jugador o responsable autorizado.

      <text color="secondary" size="xs">Después de validar el código, puede consultar las cuotas y pagar. El sistema controla la vigencia del acceso y limita los intentos. Para jugadores sin contacto verificado, se requiere un procedimiento seguro de vinculación gestionado por el club o disciplina.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 117 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir consultar y pagar cuotas ingresando únicamente el DNI",b:"exigir una cuenta de jugador o responsable con email y contraseña",c:"utilizar DNI más código temporal enviado a un contacto previamente verificado, con acceso limitado, control de intentos y vinculación segura de jugadores sin contacto verificado"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-117

### Registro 297 — Autenticación de jugadores y responsables de pago
Fuente: `3f80d91d-b1d5-48d3-824b-cb456dc73bba`

Respuesta previa del usuario:

Para la pregunta 117 del relevamiento SDD elijo la opción C: utilizar DNI más código temporal enviado a un contacto previamente verificado, con acceso limitado, control de intentos y vinculación segura de jugadores sin contacto verificado.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 117 — Confirmada: opción C**
</row>

El portal de jugadores y responsables utilizará **DNI + código temporal de verificación (OTP)**, sin necesidad de contraseña. Nadie podrá consultar las cuotas de un jugador únicamente conociendo su DNI.

## Registro SDD — Decisión 117

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-117 — Autenticación de jugadores y responsables de pago**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1000:** El portal permitirá iniciar la identificación ingresando el DNI del jugador.
  - **RN-1001:** El DNI por sí solo no habilitará la consulta de cuotas, deudas ni información personal.
  - **RN-1002:** El acceso requerirá verificar un código temporal de un solo uso (OTP).
  - **RN-1003:** El código se enviará a un correo electrónico o teléfono previamente registrado y verificado.
  - **RN-1004:** El sistema admitirá la verificación de jugadores y responsables de pago autorizados.
  - **RN-1005:** Los códigos tendrán una vigencia limitada.
  - **RN-1006:** Cada código podrá utilizarse una sola vez.
  - **RN-1007:** El sistema limitará los intentos incorrectos de verificación.
  - **RN-1008:** Se limitará la frecuencia de solicitud y reenvío de códigos.
  - **RN-1009:** Las respuestas de identificación no deberán revelar innecesariamente si un DNI está registrado.
  - **RN-1010:** Una verificación exitosa habilitará una sesión temporal para consultar y pagar cuotas autorizadas.
  - **RN-1011:** La sesión tendrá vencimiento y controles de seguridad.
  - **RN-1012:** La autenticación no permitirá acceder a jugadores o disciplinas sin autorización.
  - **RN-1013:** Los jugadores sin contacto verificado necesitarán un procedimiento seguro de vinculación.
  - **RN-1014:** La vinculación inicial será gestionada por personal autorizado del club o disciplina.
  - **RN-1015:** Los eventos de verificación, intentos fallidos y vinculaciones quedarán registrados para auditoría, sin almacenar códigos en texto plano.
  - **RN-1016:** El acceso al portal será independiente de las cuentas administrativas.
</box>

## Ejemplo — Acceso al portal

<box border radius="lg" padding={3} gap={3}>
  <list marker="number" connector="solid" gap={3}>
    <list-item>
      **Ingresar DNI**

      <box background="surface-secondary" radius="md" padding={3}>
        <label>DNI del jugador</label>
        <input disabled value="42123456"/>
      </box>
    </list-item>
    <list-item>
      **Verificar identidad**

      <text color="secondary" size="sm">El sistema envía un código al contacto autorizado previamente verificado.</text>
      <box background="surface-secondary" radius="md" padding={3} gap={1}>
        <label>Código de verificación</label>
        <input disabled value="••••••"/>
        <text color="secondary" size="xs">Código temporal de un solo uso</text>
      </box>
    </list-item>
    <list-item>
      **Consultar y pagar cuotas**

      <text color="secondary" size="sm">Una vez validado el código, se habilita el acceso a las cuotas autorizadas.</text>
    </list-item>
  </list>
  <caption>Ejemplo ilustrativo; no realiza una autenticación real.</caption>
</box>

---

## Fase 4 — Responsables de pago

<text color="secondary" weight="medium" size="xs">Pregunta 118 — ¿Cómo se vinculan los responsables de pago con los jugadores?</text>

Un jugador menor de edad puede tener a su madre, padre o tutor como responsable. También puede ocurrir que una misma persona pague las cuotas de varios hermanos.

**¿Qué modelo de vinculación querés implementar?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Un responsable por jugador**

      Cada jugador podrá tener un único responsable de pago, registrado por el administrador de su disciplina.
    </radio>
    <radio value="b">
      **Opción B — Varios responsables por jugador**

      Cada jugador podrá tener múltiples responsables, pero cada responsable deberá acceder a las cuotas de cada jugador por separado.
    </radio>
    <radio value="c">
      **Opción C — Relación muchos a muchos con acceso unificado (recomendada)**

      Un jugador podrá tener varios responsables autorizados y un responsable podrá estar vinculado a varios jugadores.

      <text color="secondary" size="xs">Después de verificar su identidad, el responsable podrá consultar desde un mismo acceso las cuotas de todos los jugadores vinculados dentro de los clubes autorizados. Las vinculaciones serán administradas por personal autorizado, con posibilidad de revocación y auditoría. El sistema no presumirá vínculos familiares por compartir apellido o domicilio.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 118 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir un único responsable de pago por jugador",b:"permitir varios responsables por jugador, pero con acceso separado para cada jugador",c:"implementar relaciones muchos a muchos entre responsables y jugadores, acceso unificado a jugadores vinculados, administración autorizada, revocación y auditoría"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-118

### Registro 299 — Vinculación de responsables de pago y jugadores
Fuente: `dc15b527-dafb-423c-9570-2ab6b656ed9c`

Respuesta previa del usuario:

Para la pregunta 118 del relevamiento SDD elijo la opción C: implementar relaciones muchos a muchos entre responsables y jugadores, acceso unificado a jugadores vinculados, administración autorizada, revocación y auditoría.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 118 — Confirmada: opción C**
</row>

La plataforma permitirá que **un responsable de pago esté vinculado a varios jugadores y que cada jugador tenga varios responsables autorizados**, con acceso unificado a las cuotas correspondientes.

## Registro SDD — Decisión 118

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-118 — Vinculación de responsables de pago y jugadores**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1017:** Existirá una relación muchos a muchos entre responsables de pago y jugadores.
  - **RN-1018:** Un jugador podrá tener múltiples responsables autorizados.
  - **RN-1019:** Un responsable podrá estar vinculado a múltiples jugadores.
  - **RN-1020:** Cada responsable tendrá una identidad propia, independiente de los jugadores vinculados.
  - **RN-1021:** El acceso del responsable requerirá la verificación de identidad definida en DEC-117.
  - **RN-1022:** Una vez autenticado, el responsable podrá consultar las cuotas de todos los jugadores con los que mantenga una vinculación vigente y autorizada.
  - **RN-1023:** El portal ofrecerá una vista unificada de los jugadores vinculados.
  - **RN-1024:** Las cuotas conservarán su asociación original con el jugador, club y disciplina.
  - **RN-1025:** Las vinculaciones serán creadas y administradas por personal autorizado del club o disciplina.
  - **RN-1026:** Se permitirá revocar una vinculación sin eliminar al jugador ni al responsable.
  - **RN-1027:** La revocación impedirá inmediatamente nuevas consultas y operaciones basadas en ese vínculo.
  - **RN-1028:** Las vinculaciones respetarán el aislamiento entre clubes y disciplinas.
  - **RN-1029:** El sistema no presumirá parentescos ni autorizaciones por coincidencia de apellido, domicilio o contacto.
  - **RN-1030:** Se registrarán altas, modificaciones y revocaciones con usuario responsable, fecha y hora.
  - **RN-1031:** Los cambios de vinculación no modificarán los pagos históricos ni sus comprobantes.
  - **RN-1032:** La visualización unificada no permitirá consultar información de jugadores sin autorización vigente.
</box>

## Ejemplo — Una familia con varios jugadores

<box border radius="lg" padding={3} gap={3}>
  <box background="surface-secondary" radius="lg" padding={3} align="center" gap={1}>
    <icon name="user-round" size="xl" color="secondary"/>
    **Responsable de pago**
    <text size="xs" color="secondary">Identidad verificada</text>
  </box>
  <row align="center" justify="center">
    <icon name="git-branch" color="secondary" size="lg"/>
  </row>
  <grid columns={2} gap={2}>
    <grid-item>
      <box border radius="md" padding={3} align="center" gap={1}>
        <icon name="user" color="secondary"/>
        **Jugador A**
        <text color="secondary" size="xs">Obras · Futsal</text>
        <badge>2 cuotas pendientes</badge>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="md" padding={3} align="center" gap={1}>
        <icon name="user" color="secondary"/>
        **Jugador B**
        <text color="secondary" size="xs">Obras · Básquet</text>
        <badge>1 cuota pendiente</badge>
      </box>
    </grid-item>
  </grid>
  <row align="center" justify="between">
    <text color="secondary" size="sm">Vista unificada del responsable</text>
    **3 cuotas pendientes**
  </row>
  <caption>Ejemplo ficticio. Cada cuota conserva su disciplina y jugador de origen.</caption>
</box>

---

## Fase 4 — Autorización de responsables

<text color="secondary" weight="medium" size="xs">Pregunta 119 — ¿Cómo se autoriza inicialmente a una persona como responsable de pago de un jugador?</text>

Ya definimos que un responsable puede pagar por varios jugadores. Ahora necesitamos evitar que alguien consiga acceso a las cuotas de un menor simplemente solicitando que lo vinculen.

**¿Qué procedimiento de autorización querés implementar?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Vinculación directa por administrador**

      El administrador de la disciplina registra al responsable y la vinculación queda activa inmediatamente, sin verificaciones adicionales.
    </radio>
    <radio value="b">
      **Opción B — Vinculación por solicitud del responsable**

      El responsable solicita vincularse ingresando el DNI del jugador. Un administrador revisa la solicitud y decide aprobarla o rechazarla.
    </radio>
    <radio value="c">
      **Opción C — Vinculación administrativa con verificación y trazabilidad (recomendada)**

      Un administrador autorizado registra la vinculación, verifica la identidad y la legitimidad de la autorización mediante un procedimiento definido por el club y confirma el contacto del responsable.

      <text color="secondary" size="xs">La vinculación queda activa únicamente después de completar las verificaciones exigidas. Se registra quién la aprobó, cuándo y bajo qué procedimiento. Puede revocarse posteriormente. Para menores de edad se contemplan controles específicos de autorización, sin exigir almacenar documentación sensible innecesaria.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 119 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir la vinculación inmediata por un administrador sin verificaciones adicionales",b:"permitir solicitudes de vinculación iniciadas por responsables y aprobadas por administradores",c:"exigir vinculación administrativa con verificación de identidad, legitimidad de autorización y contacto, aprobación trazable, controles para menores y revocación"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-119

### Registro 301 — Vinculación directa de responsables por administradores
Fuente: `f43af9ff-2a12-4834-9a81-98827682645b`

Respuesta previa del usuario:

Para la pregunta 119 del relevamiento SDD elijo la opción A: permitir la vinculación inmediata por un administrador sin verificaciones adicionales.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 119 — Confirmada: opción A**
</row>

La vinculación entre un jugador y un responsable de pago será **inmediata cuando la realice un administrador autorizado**, sin requerir verificaciones adicionales ni una aprobación posterior.

Esto simplifica la operación diaria de los clubes. Sin embargo, el responsable seguirá necesitando autenticarse mediante el mecanismo OTP definido en DEC-117 para ingresar al portal.

## Registro SDD — Decisión 119

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-119 — Vinculación directa de responsables por administradores**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1033:** Los administradores autorizados podrán vincular responsables de pago con jugadores.
  - **RN-1034:** La vinculación quedará activa inmediatamente después de ser confirmada por el administrador.
  - **RN-1035:** No se requerirá un proceso adicional de aprobación.
  - **RN-1036:** No se exigirá una verificación adicional de identidad o parentesco para activar la vinculación.
  - **RN-1037:** El administrador solo podrá vincular jugadores pertenecientes a su ámbito autorizado.
  - **RN-1038:** Un responsable podrá vincularse con varios jugadores.
  - **RN-1039:** Un jugador podrá tener varios responsables activos.
  - **RN-1040:** El sistema evitará vinculaciones activas duplicadas entre el mismo responsable y jugador dentro del mismo ámbito.
  - **RN-1041:** Cada vinculación registrará el administrador responsable, fecha y hora.
  - **RN-1042:** Las vinculaciones podrán revocarse conforme a DEC-118.
  - **RN-1043:** La activación del vínculo no reemplazará la autenticación OTP requerida para acceder al portal.
  - **RN-1044:** El acceso efectivo a las cuotas requerirá una vinculación vigente y una sesión autenticada.
  - **RN-1045:** La vinculación no modificará las cuotas, pagos ni comprobantes históricos.
  - **RN-1046:** Las altas y revocaciones permanecerán disponibles para auditoría.
</box>

<row align="start" gap={2}>
  <icon name="alert-triangle" color="warning" size="lg"/>
  <box flex="1" gap={1}>
    **Consideración de seguridad**

    <text size="sm">Con esta decisión, una vinculación incorrecta realizada por un administrador podría dar acceso a información de un jugador. Por eso mantenemos los controles de permisos, la autenticación del responsable y la trazabilidad, aunque no haya una verificación adicional al crear el vínculo.</text>
  </box>
</row>

---

## Fase 4 — Gestión de responsables de pago

<text color="secondary" weight="medium" size="xs">Pregunta 120 — ¿Quién podrá modificar o revocar las vinculaciones de responsables de pago?</text>

Supongamos que un jugador cambia de responsable de pago, o que un administrador vinculó por error a una persona.

**¿Quién debería tener permiso para corregir o eliminar esa vinculación?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo superadministrador**

      Únicamente el superadministrador podrá modificar o revocar vinculaciones existentes.
    </radio>
    <radio value="b">
      **Opción B — Administradores y responsables**

      Los administradores podrán gestionar vinculaciones, y los propios responsables podrán desvincularse o solicitar nuevos jugadores desde el portal.
    </radio>
    <radio value="c">
      **Opción C — Gestión administrativa por ámbito (recomendada)**

      Los administradores de disciplina podrán crear, modificar y revocar vinculaciones de jugadores de su propia disciplina.

      <text color="secondary" size="xs">Los delegados generales podrán gestionar vinculaciones dentro de su club y el superadministrador podrá intervenir globalmente. Los responsables de pago solo podrán consultar sus vínculos, sin agregar ni quitar jugadores por cuenta propia. Todos los cambios serán auditados y las revocaciones tendrán efecto inmediato.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 120 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir modificar y revocar vinculaciones solo al superadministrador",b:"permitir que administradores y responsables gestionen o soliciten cambios en vinculaciones",c:"permitir gestión administrativa por ámbito a administradores de disciplina, delegados generales y superadministrador, dejando a responsables acceso de solo consulta y auditando revocaciones inmediatas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-120

### Registro 303 — Administración de vinculaciones por ámbito
Fuente: `d680cb22-bca9-49a5-8c30-84c4acdad872`

Respuesta previa del usuario:

Para la pregunta 120 del relevamiento SDD elijo la opción C: permitir gestión administrativa por ámbito a administradores de disciplina, delegados generales y superadministrador, dejando a responsables acceso de solo consulta y auditando revocaciones inmediatas.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 120 — Confirmada: opción C**
</row>

La gestión de vinculaciones entre responsables de pago y jugadores estará disponible para los **administradores de disciplina, delegados generales y superadministrador**, cada uno dentro de sus permisos. Los responsables únicamente podrán consultar sus vinculaciones.

## Registro SDD — Decisión 120

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-120 — Administración de vinculaciones por ámbito**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1047:** Los administradores de disciplina podrán crear, modificar y revocar vinculaciones de jugadores de su propia disciplina.
  - **RN-1048:** Los delegados generales podrán administrar vinculaciones dentro de su club.
  - **RN-1049:** El superadministrador podrá administrar vinculaciones de cualquier club o disciplina.
  - **RN-1050:** Los responsables de pago podrán consultar sus vinculaciones vigentes, pero no modificarlas.
  - **RN-1051:** Los responsables no podrán agregar jugadores por cuenta propia.
  - **RN-1052:** Los responsables no podrán revocar directamente sus vinculaciones.
  - **RN-1053:** Toda operación administrativa deberá validar los permisos vigentes del usuario.
  - **RN-1054:** Las modificaciones deberán respetar el aislamiento entre clubes y disciplinas.
  - **RN-1055:** Las revocaciones tendrán efecto inmediato.
  - **RN-1056:** Una vinculación revocada dejará de habilitar consultas y nuevos pagos del jugador por parte de ese responsable.
  - **RN-1057:** La revocación no anulará pagos previamente realizados.
  - **RN-1058:** Las vinculaciones revocadas conservarán su historial para auditoría.
  - **RN-1059:** Cada alta, modificación y revocación registrará usuario responsable, fecha, hora y valores anteriores y nuevos cuando corresponda.
  - **RN-1060:** Los cambios se reflejarán en las sesiones activas de los responsables afectados.
  - **RN-1061:** El acceso al portal continuará sujeto a la autenticación y autorización definidas en DEC-117 y DEC-118.
</box>

## Ejemplo — Permisos de gestión

| Rol | Alcance | Gestionar vínculos |
|---|---|---|
| Superadministrador | Todos los clubes | <icon name="check" color="success" inline/> |
| Delegado general | Su club | <icon name="check" color="success" inline/> |
| Administrador de disciplina | Su disciplina | <icon name="check" color="success" inline/> |
| Responsable de pago | Jugadores vinculados | Solo consulta |

---

## Fase 4 — Pagos de múltiples jugadores

<text color="secondary" weight="medium" size="xs">Pregunta 121 — ¿Un responsable podrá pagar cuotas de varios jugadores en una sola operación?</text>

Por ejemplo, una madre tiene dos hijos en Obras Mendoza: uno juega futsal y otro básquet. Ambos tienen cuotas pendientes.

Ya definimos que puede consultar las cuotas de ambos desde una misma cuenta. Ahora debemos decidir cómo se procesarán sus pagos.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Pagos separados por jugador**

      El responsable deberá realizar una operación de pago independiente para cada jugador.
    </radio>
    <radio value="b">
      **Opción B — Pago conjunto dentro de una disciplina**

      Podrá agrupar cuotas de varios jugadores únicamente cuando pertenezcan a la misma disciplina. Las cuotas de disciplinas diferentes se pagarán por separado.
    </radio>
    <radio value="c">
      **Opción C — Carrito unificado con distribución por jugador y disciplina (recomendada)**

      El responsable podrá seleccionar cuotas completas de varios jugadores y disciplinas autorizadas en un mismo carrito.

      <text color="secondary" size="xs">Si la pasarela de pagos permite una operación conjunta con distribución correcta de fondos, se realizará un único pago. De lo contrario, el sistema dividirá el carrito en operaciones independientes, informándolo antes de confirmar. Cada cuota conservará su jugador, disciplina, comisión, estado y trazabilidad, sin duplicar cobros.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 121 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"exigir pagos separados por cada jugador",b:"permitir pagos agrupados solo para jugadores de una misma disciplina",c:"implementar carrito unificado de cuotas completas de varios jugadores y disciplinas, con pago conjunto si la pasarela admite distribución de fondos o división transparente en operaciones separadas, conservando trazabilidad y evitando duplicados"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-121

### Registro 305 — Pagos agrupados únicamente dentro de una disciplina
Fuente: `a31de425-64bb-4842-ba35-2d01d1e1ac04`

Respuesta previa del usuario:

recorda que cada disciplina la vamos a intentar aislar lo mas posible de la otra asi podemos difernciar pago por ende voy a elegir la opcion b

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 121 — Confirmada: opción B**
</row>

Queda establecido un principio importante para toda la plataforma: **cada disciplina debe mantener la mayor independencia operativa y financiera posible**, especialmente en pagos, comisiones, liquidaciones y conciliaciones.

Un responsable podrá consultar cuotas de varias disciplinas, pero **no mezclarlas en una misma operación de pago**.

## Registro SDD — Decisión 121

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-121 — Pagos agrupados únicamente dentro de una disciplina**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1062:** Un responsable podrá seleccionar cuotas completas de varios jugadores vinculados, siempre que pertenezcan a la misma disciplina.
  - **RN-1063:** No se permitirá agrupar cuotas de distintas disciplinas en una misma operación de pago.
  - **RN-1064:** Cada operación tendrá una única disciplina asociada.
  - **RN-1065:** Cada disciplina mantendrá su propia identificación de operaciones, pagos y conciliaciones.
  - **RN-1066:** Las comisiones y costos de procesamiento se identificarán por disciplina.
  - **RN-1067:** Los pagos agrupados conservarán el detalle individual de cada jugador y cuota.
  - **RN-1068:** Las cuotas de distintas disciplinas deberán pagarse mediante operaciones independientes.
  - **RN-1069:** El portal podrá mostrar las cuotas de todas las disciplinas autorizadas sin mezclar sus procesos de cobro.
  - **RN-1070:** Una falla en el pago de una disciplina no modificará el resultado de pagos de otras disciplinas.
  - **RN-1071:** La conciliación y trazabilidad de cada pago permanecerán asociadas exclusivamente a su disciplina.
  - **RN-1072:** Se evitarán cobros duplicados y se respetará la regla de pago completo de cuotas definida en DEC-047.
</box>

## Ejemplo — Pagos separados por disciplina

<box border radius="lg" padding={3} gap={3}>
  <text color="secondary" size="sm">Un responsable tiene tres jugadores vinculados.</text>
  <box border radius="md" padding={3} gap={2}>
    <row align="center" justify="between">
      **Obras · Futsal**
      <badge color="success">Pago agrupado permitido</badge>
    </row>
    <row justify="between">
      <text size="sm">Jugador A</text>
      <text tabularNums>$25.000</text>
    </row>
    <row justify="between">
      <text size="sm">Jugador B</text>
      <text tabularNums>$25.000</text>
    </row>
    <divider color="subtle"/>
    <row justify="between">
      **Total nominal Futsal**
      **$50.000**
    </row>
  </box>
  <box border radius="md" padding={3} gap={2}>
    <row align="center" justify="between">
      **Obras · Básquet**
      <badge>Pago independiente</badge>
    </row>
    <row justify="between">
      <text size="sm">Jugador C</text>
      <text tabularNums>$30.000</text>
    </row>
    <divider color="subtle"/>
    <row justify="between">
      **Total nominal Básquet**
      **$30.000**
    </row>
  </box>
  <caption>Se generan dos operaciones independientes. Los importes son ilustrativos y no incluyen cargos de procesamiento.</caption>
</box>

---

## Fase 4 — Aislamiento financiero entre disciplinas

<text color="secondary" weight="medium" size="xs">Pregunta 122 — ¿Cómo deberían configurarse las cuentas receptoras de los pagos?</text>

Para mantener esa independencia, necesitamos definir a dónde llega el dinero que paga cada jugador.

**¿Cada disciplina debería tener su propia cuenta de cobro?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Una cuenta receptora por club**

      Todas las disciplinas del mismo club cobran mediante una cuenta común. El sistema diferencia internamente los movimientos.
    </radio>
    <radio value="b">
      **Opción B — Una cuenta receptora obligatoria por disciplina**

      Cada disciplina deberá configurar su propia cuenta receptora. No podrá compartirla con otras disciplinas, aunque pertenezcan al mismo club.
    </radio>
    <radio value="c">
      **Opción C — Cuenta receptora configurable por disciplina (recomendada)**

      Cada disciplina tendrá una configuración de cobro independiente y podrá utilizar una cuenta receptora propia o una cuenta compartida del club.

      <text color="secondary" size="xs">Aunque compartan una cuenta bancaria, cada operación, liquidación, comisión y conciliación seguirá identificada por disciplina. Se deberá verificar que la pasarela elegida soporte esta configuración y la distribución correspondiente de fondos.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 122 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"utilizar una cuenta receptora común para todas las disciplinas del club",b:"exigir una cuenta receptora independiente y exclusiva para cada disciplina",c:"configurar la cuenta receptora por disciplina, permitiendo cuentas propias o compartidas, pero manteniendo operaciones, liquidaciones, comisiones y conciliaciones separadas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-122

### Registro 307 — Configuración independiente de cuentas receptoras
Fuente: `5e412633-fbed-427c-b05f-813ba95e9c74`

Respuesta previa del usuario:

Para la pregunta 122 del relevamiento SDD elijo la opción C: configurar la cuenta receptora por disciplina, permitiendo cuentas propias o compartidas, pero manteniendo operaciones, liquidaciones, comisiones y conciliaciones separadas.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 122 — Confirmada: opción C**
</row>

Cada disciplina tendrá **su propia configuración de cobro**, pudiendo utilizar una cuenta receptora exclusiva o una compartida con otras disciplinas del club.

La independencia financiera se mantendrá incluso cuando compartan una misma cuenta bancaria.

## Registro SDD — Decisión 122

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-122 — Configuración independiente de cuentas receptoras**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1073:** Cada disciplina tendrá una configuración de cobro independiente.
  - **RN-1074:** Una disciplina podrá utilizar una cuenta receptora exclusiva.
  - **RN-1075:** Varias disciplinas de un mismo club podrán compartir una cuenta receptora.
  - **RN-1076:** Cada operación de pago pertenecerá a una única disciplina, conforme a DEC-121.
  - **RN-1077:** Los pagos deberán conservar el identificador de su disciplina, independientemente de la cuenta receptora.
  - **RN-1078:** Las liquidaciones se registrarán y consultarán separadamente por disciplina.
  - **RN-1079:** Las comisiones de la plataforma se calcularán e identificarán por disciplina.
  - **RN-1080:** Los costos de la pasarela se registrarán por disciplina cuando estén disponibles.
  - **RN-1081:** Las conciliaciones se realizarán y registrarán por disciplina, incluso si comparten cuenta receptora.
  - **RN-1082:** Compartir una cuenta bancaria no otorgará acceso a los datos administrativos de otras disciplinas.
  - **RN-1083:** Los reportes financieros permitirán distinguir los movimientos de cada disciplina.
  - **RN-1084:** La configuración deberá respetar las capacidades técnicas y contractuales de la pasarela seleccionada.
  - **RN-1085:** El sistema no asumirá que una cuenta bancaria compartida implica una liquidación conjunta.
  - **RN-1086:** Los cambios de cuenta receptora conservarán el historial de operaciones y liquidaciones anteriores.
  - **RN-1087:** Las modificaciones de configuración quedarán auditadas.
</box>

## Ejemplo — Independencia con cuenta compartida

<box border radius="lg" padding={3} gap={3}>
  <box background="surface-secondary" radius="lg" padding={3} gap={1} align="center">
    <icon name="landmark" size="xl" color="secondary"/>
    **Cuenta receptora del club**
    <text color="secondary" size="xs">Cuenta bancaria compartida</text>
  </box>
  <row justify="center">
    <icon name="git-branch" color="secondary" size="lg"/>
  </row>
  <grid columns={2} gap={2}>
    <grid-item>
      <box border radius="md" padding={3} gap={2}>
        **Disciplina Futsal**
        <text color="secondary" size="xs">Operaciones propias</text>
        <title size="lg" tabularNums>$500.000</title>
        <badge color="success">Conciliación Futsal</badge>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="md" padding={3} gap={2}>
        **Disciplina Básquet**
        <text color="secondary" size="xs">Operaciones propias</text>
        <title size="lg" tabularNums>$300.000</title>
        <badge color="success">Conciliación Básquet</badge>
      </box>
    </grid-item>
  </grid>
  <caption>Montos ilustrativos. Una cuenta compartida no mezcla los registros financieros de las disciplinas.</caption>
</box>

---

## Fase 4 — Administración de cuentas receptoras

<text color="secondary" weight="medium" size="xs">Pregunta 123 — ¿Quién podrá configurar o modificar la cuenta receptora de una disciplina?</text>

Esta configuración es sensible porque determina dónde se depositan los pagos de los jugadores. Un cambio incorrecto podría desviar fondos.

**¿Qué nivel de autorización querés implementar?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Administrador de disciplina**

      Cada administrador de disciplina podrá configurar y modificar directamente su cuenta receptora, sin aprobación adicional.
    </radio>
    <radio value="b">
      **Opción B — Solo superadministrador**

      Únicamente el superadministrador podrá registrar, modificar o reemplazar las cuentas receptoras de cualquier disciplina.
    </radio>
    <radio value="c">
      **Opción C — Solicitud administrativa con aprobación del superadministrador (recomendada)**

      Los administradores de disciplina y delegados generales podrán solicitar el alta o cambio de cuenta receptora dentro de sus ámbitos.

      <text color="secondary" size="xs">El superadministrador deberá aprobar la solicitud antes de que entre en vigencia. Se registrarán los datos anteriores y nuevos, quién solicitó y aprobó el cambio, y cuándo se aplicó. Los pagos iniciados antes del cambio conservarán la configuración con la que se crearon, según las capacidades de la pasarela.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 123 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir que administradores de disciplina modifiquen directamente su cuenta receptora",b:"reservar al superadministrador toda configuración o modificación de cuentas receptoras",c:"permitir solicitudes de alta o cambio por administradores de disciplina y delegados, con aprobación obligatoria del superadministrador, trazabilidad y preservación de operaciones previas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-123

### Registro 309 — Administración autónoma de cuentas receptoras
Fuente: `cb0da57a-dd02-4537-996e-5501abb86be4`

Respuesta previa del usuario:

Para la pregunta 123 del relevamiento SDD elijo la opción A: permitir que administradores de disciplina modifiquen directamente su cuenta receptora.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 123 — Confirmada: opción A**
</row>

Los administradores de cada disciplina podrán **configurar y modificar directamente su cuenta receptora**, sin solicitar aprobación al superadministrador.

Esto refuerza la autonomía financiera que estamos definiendo para cada disciplina.

## Registro SDD — Decisión 123

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-123 — Administración autónoma de cuentas receptoras**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1088:** Los administradores de disciplina podrán registrar su cuenta receptora de pagos.
  - **RN-1089:** Podrán modificar o reemplazar la cuenta receptora sin aprobación del superadministrador.
  - **RN-1090:** Solo podrán administrar la configuración de las disciplinas para las que tengan permisos.
  - **RN-1091:** Un administrador no podrá modificar las cuentas receptoras de otras disciplinas.
  - **RN-1092:** El sistema deberá validar los datos requeridos de la cuenta antes de activar la configuración.
  - **RN-1093:** La cuenta deberá cumplir los requisitos técnicos y de titularidad que exija la pasarela seleccionada.
  - **RN-1094:** Cada cambio registrará el administrador responsable, fecha y hora.
  - **RN-1095:** Se conservará el historial de configuraciones anteriores.
  - **RN-1096:** Los pagos ya iniciados mantendrán la referencia a la configuración receptora utilizada al crearlos.
  - **RN-1097:** Los cambios no modificarán retroactivamente pagos, liquidaciones ni conciliaciones históricas.
  - **RN-1098:** Las nuevas operaciones utilizarán la configuración vigente y validada.
  - **RN-1099:** El sistema impedirá iniciar nuevos cobros si no existe una configuración receptora operativa exigida por la pasarela.
  - **RN-1100:** Los cambios quedarán disponibles para auditoría del superadministrador.
  - **RN-1101:** La administración de cuentas receptoras respetará el aislamiento financiero establecido en DEC-121 y DEC-122.
</box>

<row align="start" gap={2}>
  <icon name="shield-alert" color="warning" size="lg"/>
  <box flex="1" gap={1}>
    **Consideración de seguridad**

    <text size="sm">Como estos cambios no requieren una segunda aprobación, conviene proteger especialmente esta operación. Una cuenta receptora incorrecta podría provocar desvíos de fondos. La siguiente decisión definirá qué protección adicional utilizar.</text>
  </box>
</row>

---

## Fase 4 — Seguridad de cuentas receptoras

<text color="secondary" weight="medium" size="xs">Pregunta 124 — ¿Qué verificación debería exigirse al cambiar una cuenta receptora?</text>

Supongamos que un administrador de Futsal inicia sesión y quiere reemplazar la cuenta donde se reciben los pagos.

**¿Qué medida de seguridad preferís?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Confirmación simple**

      El administrador modifica los datos y confirma el cambio desde su sesión activa, sin verificaciones adicionales.
    </radio>
    <radio value="b">
      **Opción B — Confirmación con contraseña**

      Antes de guardar la nueva cuenta, el administrador debe volver a ingresar su contraseña.
    </radio>
    <radio value="c">
      **Opción C — Verificación reforzada mediante código temporal (recomendada)**

      El administrador podrá realizar el cambio directamente, pero deberá confirmar la operación mediante un código temporal enviado a su correo electrónico verificado.

      <text color="secondary" size="xs">El código tendrá vencimiento y límite de intentos. Una vez validado, el cambio se aplicará sin aprobación de otro usuario. Se registrará la operación y se notificará al administrador y al superadministrador como evento de seguridad, sin concederles una facultad de aprobación.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 124 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir cambiar cuentas receptoras con confirmación simple desde la sesión activa",b:"exigir que el administrador vuelva a ingresar su contraseña antes de cambiar la cuenta receptora",c:"exigir un código temporal al correo verificado del administrador para confirmar el cambio, sin aprobación adicional, con auditoría y avisos de seguridad"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-124

### Registro 315 — Verificación reforzada de cuentas receptoras
Fuente: `598bdef1-9775-4234-918a-b9155ced369b`

Respuesta previa del usuario:

Para la pregunta 124 del relevamiento SDD elijo la opción C: exigir un código temporal al correo verificado del administrador para confirmar el cambio, sin aprobación adicional, con auditoría y avisos de seguridad.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 124 — Confirmada: opción C**
</row>

Queda establecido que los administradores de disciplina podrán modificar directamente la cuenta vinculada a la pasarela de pagos, pero deberán **confirmar el cambio mediante un código temporal enviado a su correo electrónico verificado**.

No necesitarán aprobación del superadministrador.

## Registro SDD — Decisión 124

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-124 — Verificación reforzada de cuentas receptoras**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1102:** El alta o reemplazo de una cuenta vinculada a la pasarela requerirá verificación mediante OTP.
  - **RN-1103:** El código se enviará al correo electrónico previamente verificado del administrador que realiza la operación.
  - **RN-1104:** El código será de un solo uso y tendrá un vencimiento.
  - **RN-1105:** Se limitarán los intentos fallidos y las solicitudes de nuevos códigos.
  - **RN-1106:** La operación deberá quedar asociada al administrador autenticado y a su disciplina.
  - **RN-1107:** No se requerirá aprobación de otro administrador ni del superadministrador.
  - **RN-1108:** La validación del OTP no reemplazará los controles de vinculación y titularidad exigidos por la pasarela.
  - **RN-1109:** Una cuenta nueva no podrá utilizarse para cobrar hasta completar su vinculación y validación.
  - **RN-1110:** Se registrarán fecha, hora, administrador, disciplina, cuenta anterior y nueva, sin almacenar credenciales sensibles innecesarias.
  - **RN-1111:** El administrador recibirá un aviso de seguridad cuando se complete el cambio.
  - **RN-1112:** El superadministrador también recibirá un aviso de seguridad, sin necesidad de aprobar la operación.
  - **RN-1113:** Los avisos deberán respetar los permisos, ámbitos y reglas del sistema de notificaciones.
  - **RN-1114:** Los pagos iniciados antes del cambio conservarán su referencia original para conciliación y trazabilidad.
  - **RN-1115:** Si la validación falla, la nueva cuenta no se activará y se conservará la configuración anterior, si sigue operativa.
  - **RN-1116:** Los eventos de seguridad y los cambios de configuración permanecerán auditados.
</box>

## Ejemplo de funcionamiento

<list marker="number" connector="solid" gap={2}>
  <list-item>El administrador de **Obras Futsal** solicita vincular una nueva cuenta de Mercado Pago.</list-item>
  <list-item>El sistema envía un código temporal al correo verificado del administrador.</list-item>
  <list-item>El administrador ingresa el código y autoriza la vinculación con la pasarela.</list-item>
  <list-item>Cuando la pasarela confirma que la cuenta está habilitada, se activa la nueva configuración.</list-item>
  <list-item>Se registra el cambio y se generan los avisos de seguridad correspondientes.</list-item>
</list>

---

## Fase 4 — Integración con la pasarela de pagos

<text color="secondary" weight="medium" size="xs">Pregunta 125 — ¿Cómo debería vincular una disciplina su cuenta de Mercado Pago u otra pasarela?</text>

Ahora necesitamos definir cómo será el proceso de conexión entre la disciplina y la pasarela, teniendo en cuenta el modelo de **Split Payment**.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Configuración manual de credenciales**

      El administrador ingresa los datos y credenciales de integración de su cuenta en el panel de la disciplina.
    </radio>
    <radio value="b">
      **Opción B — Vinculación exclusiva mediante autorización externa**

      El administrador utiliza un botón para conectarse a la pasarela, inicia sesión allí y autoriza la integración. No se ingresan credenciales manualmente en nuestra plataforma.
    </radio>
    <radio value="c">
      **Opción C — Vinculación segura según las capacidades de cada pasarela (recomendada)**

      Se priorizará la autorización externa mediante OAuth u otro mecanismo oficial de conexión.

      <text color="secondary" size="xs">Si una pasarela requiere otro mecanismo, se implementará una integración específica y segura. Cada disciplina tendrá su conexión independiente, incluso si varias utilizan el mismo proveedor. Solo se habilitarán conexiones compatibles con el split requerido.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 125 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"configurar manualmente las credenciales de la cuenta de pasarela",b:"vincular exclusivamente mediante autorización externa de la pasarela",c:"priorizar autorización externa oficial y admitir mecanismos seguros específicos por pasarela, manteniendo conexiones independientes por disciplina y compatibilidad con split de pagos"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-125

### Registro 317 — Integración segura de pasarelas por disciplina
Fuente: `86e957da-2af8-44db-b67a-aeaaa2b1f8f5`

Respuesta previa del usuario:

Para la pregunta 125 del relevamiento SDD elijo la opción C: priorizar autorización externa oficial y admitir mecanismos seguros específicos por pasarela, manteniendo conexiones independientes por disciplina y compatibilidad con split de pagos.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 125 — Confirmada: opción C**
</row>

La plataforma utilizará preferentemente los mecanismos oficiales de autorización de cada pasarela, como OAuth, y admitirá otros mecanismos seguros cuando sean necesarios.

**Cada disciplina tendrá su propia conexión lógica con la pasarela**, manteniendo el aislamiento financiero y el modelo de Split Payment.

## Registro SDD — Decisión 125

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-125 — Integración segura de pasarelas por disciplina**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1117:** Cada disciplina tendrá una configuración independiente de integración con la pasarela.
  - **RN-1118:** Se priorizarán mecanismos oficiales de autorización externa, como OAuth.
  - **RN-1119:** Se admitirán otros mecanismos de integración seguros cuando el proveedor los requiera.
  - **RN-1120:** No se solicitará la contraseña personal de la cuenta de la pasarela cuando exista un mecanismo oficial de autorización.
  - **RN-1121:** Las credenciales y tokens de integración se almacenarán de forma segura y no se expondrán en el frontend.
  - **RN-1122:** Las conexiones deberán respetar los permisos y el ámbito de cada disciplina.
  - **RN-1123:** Varias disciplinas podrán utilizar el mismo proveedor de pagos sin compartir sus registros operativos.
  - **RN-1124:** Las conexiones de distintas disciplinas serán independientes a nivel lógico, incluso si utilizan una misma cuenta autorizada.
  - **RN-1125:** Solo se habilitarán integraciones que permitan implementar el modelo de Split Payment acordado.
  - **RN-1126:** La plataforma deberá identificar por separado el importe de la cuota, su comisión del 5% y los costos de la pasarela.
  - **RN-1127:** Los fondos correspondientes a las cuotas se acreditarán directamente a sus destinatarios mediante la pasarela, sin custodia por parte de nuestra plataforma.
  - **RN-1128:** El alta o reemplazo de una conexión requerirá la verificación adicional definida en DEC-124.
  - **RN-1129:** Los errores de autorización o conexión deberán registrarse sin exponer datos sensibles.
  - **RN-1130:** La plataforma deberá detectar cuando una conexión deje de estar autorizada o resulte inutilizable.
  - **RN-1131:** No podrán iniciarse nuevos pagos con una conexión que no esté operativa.
  - **RN-1132:** La desconexión o sustitución de una cuenta no alterará el historial de pagos y conciliaciones.
  - **RN-1133:** La implementación definitiva de cada proveedor estará condicionada a sus capacidades técnicas y contractuales de split.
</box>

## Ejemplo de integración

<box border radius="lg" padding={3} gap={2}>
  <box background="surface-secondary" radius="md" padding={3} align="center" gap={1}>
    **Plataforma SaaS**
    <text color="secondary" size="xs">Gestión centralizada, conexiones independientes</text>
  </box>
  <row justify="center">
    <icon name="arrow-down" color="secondary"/>
  </row>
  <grid columns={2} gap={2}>
    <grid-item>
      <box border radius="md" padding={3} align="center" gap={1}>
        **Obras Futsal**
        <icon name="link-2" color="success" size="lg"/>
        <text size="xs" color="secondary">Conexión propia</text>
        <badge color="success">Autorizada</badge>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="md" padding={3} align="center" gap={1}>
        **Obras Básquet**
        <icon name="link-2" color="success" size="lg"/>
        <text size="xs" color="secondary">Conexión propia</text>
        <badge color="success">Autorizada</badge>
      </box>
    </grid-item>
  </grid>
  <caption>Las conexiones pueden utilizar el mismo proveedor, pero sus operaciones, liquidaciones y conciliaciones permanecen separadas.</caption>
</box>

---

## Fase 4 — Continuidad operativa de los cobros

<text color="secondary" weight="medium" size="xs">Pregunta 126 — ¿Qué debería ocurrir cuando la conexión de una disciplina con su pasarela deja de funcionar?</text>

Por ejemplo, si Obras Futsal pierde la autorización de su cuenta de Mercado Pago, no podrá iniciar nuevos cobros online hasta restablecerla.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Bloqueo simple de pagos**

      Se bloquean los nuevos pagos online de esa disciplina hasta que el administrador vuelva a conectar la cuenta. No se generan alertas adicionales.
    </radio>
    <radio value="b">
      **Opción B — Bloqueo y alerta al administrador**

      Se bloquean los nuevos pagos online y se notifica a los administradores de la disciplina para que restablezcan la conexión.
    </radio>
    <radio value="c">
      **Opción C — Bloqueo aislado, alertas y recuperación controlada (recomendada)**

      Se bloquean únicamente los nuevos pagos online de la disciplina afectada, sin perjudicar a otras disciplinas.

      <text color="secondary" size="xs">Se notifica a sus administradores y al superadministrador; se conserva el estado de las operaciones ya iniciadas y se siguen conciliando según la información disponible. La disciplina podrá continuar registrando pagos externos autorizados. Una vez restablecida y validada la conexión, se habilitan nuevamente los cobros online, sin duplicar operaciones.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 126 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"bloquear nuevos pagos online de la disciplina hasta reconectar la pasarela, sin alertas adicionales",b:"bloquear nuevos pagos online y alertar a los administradores de la disciplina",c:"bloquear solo los nuevos pagos online de la disciplina afectada, alertar a sus administradores y al superadministrador, conservar y conciliar operaciones previas, permitir pagos externos autorizados y recuperar cobros sin duplicaciones"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-126

### Registro 319 — Gestión de interrupciones de pasarelas por disciplina
Fuente: `c195f742-ea56-47a3-9af3-07549220050e`

Respuesta previa del usuario:

Para la pregunta 126 del relevamiento SDD elijo la opción C: bloquear solo los nuevos pagos online de la disciplina afectada, alertar a sus administradores y al superadministrador, conservar y conciliar operaciones previas, permitir pagos externos autorizados y recuperar cobros sin duplicaciones.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 126 — Confirmada: opción C**
</row>

Queda definido que **una falla de la pasarela afectará únicamente a la disciplina involucrada**, sin interrumpir los cobros de las demás disciplinas del club.

Además, las operaciones iniciadas conservarán su trazabilidad y podrán seguir conciliándose.

## Registro SDD — Decisión 126

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-126 — Gestión de interrupciones de pasarelas por disciplina**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1134:** Cada disciplina tendrá un estado independiente de conexión con su pasarela.
  - **RN-1135:** Si una conexión deja de estar operativa, se bloqueará el inicio de nuevos pagos online de esa disciplina.
  - **RN-1136:** El bloqueo no afectará a otras disciplinas con conexiones operativas.
  - **RN-1137:** Se notificará a los administradores de la disciplina afectada.
  - **RN-1138:** También se notificará al superadministrador.
  - **RN-1139:** Las alertas respetarán el sistema de notificaciones y la agrupación por incidente definidos anteriormente.
  - **RN-1140:** Las cuotas pendientes seguirán disponibles para consulta.
  - **RN-1141:** Las operaciones iniciadas antes de la interrupción conservarán su estado y referencia original.
  - **RN-1142:** Se continuará intentando conciliar las operaciones previas cuando la información de la pasarela esté disponible.
  - **RN-1143:** Los administradores podrán continuar registrando pagos externos autorizados, respetando DEC-048.
  - **RN-1144:** Los pagos externos no se contabilizarán como transacciones online para los mínimos comerciales.
  - **RN-1145:** La recuperación de la conexión requerirá verificar nuevamente que esté operativa.
  - **RN-1146:** Una vez validada, se habilitarán los nuevos cobros online de la disciplina.
  - **RN-1147:** La recuperación no generará pagos duplicados ni modificará operaciones ya confirmadas.
  - **RN-1148:** Se conservará un registro auditable de la interrupción, sus causas conocidas y su recuperación.
  - **RN-1149:** La resolución del incidente actualizará las notificaciones correspondientes sin generar avisos duplicados.
</box>

## Ejemplo — Falla aislada

<box border radius="lg" padding={3} gap={2}>
  <grid columns={2} gap={2}>
    <grid-item>
      <box border radius="md" padding={3} gap={2}>
        **Obras Futsal**
        <badge color="danger"><icon name="circle-alert" inline/> Conexión caída</badge>
        <text color="secondary" size="xs">Nuevos pagos online bloqueados</text>
        <row align="center" gap={1}>
          <icon name="check" color="success"/>
          <text size="xs">Pagos externos permitidos</text>
        </row>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="md" padding={3} gap={2}>
        **Obras Básquet**
        <badge color="success"><icon name="check-circle" inline/> Operativa</badge>
        <text color="secondary" size="xs">Cobros online funcionando normalmente</text>
        <row align="center" gap={1}>
          <icon name="check" color="success"/>
          <text size="xs">Sin interrupciones</text>
        </row>
      </box>
    </grid-item>
  </grid>
  <caption>El aislamiento operativo evita que una falla en Futsal bloquee los cobros de Básquet.</caption>
</box>

---

## Fase 4 — Estados de conexión con pasarelas

<text color="secondary" weight="medium" size="xs">Pregunta 127 — ¿Cómo debería detectar la plataforma que una conexión con la pasarela dejó de funcionar?</text>

No todas las interrupciones son iguales: una autorización puede vencer, una cuenta puede desconectarse o el proveedor puede sufrir una caída temporal.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Detección al intentar cobrar**

      El sistema detecta el problema únicamente cuando un usuario intenta iniciar un pago y la pasarela devuelve un error.
    </radio>
    <radio value="b">
      **Opción B — Verificación periódica**

      La plataforma consulta periódicamente el estado de las conexiones y bloquea los cobros cuando detecta que una conexión no está autorizada.
    </radio>
    <radio value="c">
      **Opción C — Detección combinada y clasificación de fallas (recomendada)**

      La plataforma combina verificaciones periódicas, eventos oficiales de la pasarela y errores reales de operaciones.

      <text color="secondary" size="xs">Distingue entre autorización revocada, configuración inválida y fallas temporales del proveedor. Solo bloquea los cobros cuando corresponde, evitando desconectar una disciplina por un error transitorio aislado. Registra el incidente y aplica la recuperación definida en DEC-126.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 127 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"detectar problemas de conexión únicamente al intentar iniciar un pago",b:"verificar periódicamente la autorización de las conexiones con la pasarela",c:"combinar verificaciones periódicas, eventos oficiales y errores de operaciones, clasificando fallas permanentes y temporales para evitar bloqueos innecesarios"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-127

### Registro 322 — Detección y clasificación de fallas de pasarelas
Fuente: `8cccae1b-3bdf-41bb-8170-66480bb5879c`

Respuesta previa del usuario:

Para la pregunta 127 del relevamiento SDD elijo la opción C: combinar verificaciones periódicas, eventos oficiales y errores de operaciones, clasificando fallas permanentes y temporales para evitar bloqueos innecesarios.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 127 — Confirmada: opción C**
</row>

La plataforma utilizará un sistema de **monitoreo combinado de conexiones**, diferenciando los problemas temporales de las fallas que realmente requieren bloquear los cobros.

Esto permitirá mantener la autonomía de cada disciplina sin interrumpir innecesariamente sus operaciones.

## Registro SDD — Decisión 127

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-127 — Detección y clasificación de fallas de pasarelas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1150:** Cada conexión de disciplina será monitoreada independientemente.
  - **RN-1151:** Se realizarán verificaciones periódicas de conectividad y autorización, según las capacidades de la pasarela.
  - **RN-1152:** Se procesarán los eventos oficiales del proveedor, cuando estén disponibles.
  - **RN-1153:** Los errores producidos durante operaciones reales también servirán para detectar problemas.
  - **RN-1154:** Se distinguirán fallas de autorización, configuración y disponibilidad temporal.
  - **RN-1155:** Una autorización revocada o una configuración inválida confirmada bloqueará nuevos cobros online de la disciplina afectada.
  - **RN-1156:** Un error temporal aislado no provocará automáticamente una desconexión permanente.
  - **RN-1157:** Las fallas temporales se evaluarán mediante criterios de recurrencia y disponibilidad configurados por el sistema.
  - **RN-1158:** Si una interrupción temporal impide procesar pagos de forma segura, se suspenderán temporalmente nuevos intentos de cobro de esa disciplina.
  - **RN-1159:** Las verificaciones no deberán generar cobros de prueba ni movimientos financieros reales.
  - **RN-1160:** Los eventos repetidos de una misma incidencia se agruparán para evitar notificaciones duplicadas.
  - **RN-1161:** Las interrupciones y recuperaciones respetarán las reglas de DEC-126.
  - **RN-1162:** Se registrarán el origen, tipo, fecha, estado y evolución de cada incidente.
  - **RN-1163:** El monitoreo no expondrá tokens, credenciales ni información financiera sensible.
  - **RN-1164:** Una falla en una disciplina no modificará el estado operativo de otras conexiones.
  - **RN-1165:** La recuperación se confirmará mediante verificaciones válidas antes de reactivar los cobros.
</box>

## Ejemplo — Clasificación de fallas

<box border radius="lg" padding={3} gap={2}>
  <row align="center" gap={3}>
    <icon name="wifi" color="success" size="lg"/>
    <box flex="1" gap={1}>
      **Conexión operativa**
      <text color="secondary" size="sm">La autorización y los servicios necesarios funcionan correctamente.</text>
    </box>
    <badge color="success">Cobros habilitados</badge>
  </row>
  <divider color="subtle"/>
  <row align="center" gap={3}>
    <icon name="clock" color="warning" size="lg"/>
    <box flex="1" gap={1}>
      **Falla temporal aislada**
      <text color="secondary" size="sm">Se registra el error y se verifica nuevamente, sin revocar la conexión.</text>
    </box>
    <badge color="warning">En observación</badge>
  </row>
  <divider color="subtle"/>
  <row align="center" gap={3}>
    <icon name="shield-x" color="danger" size="lg"/>
    <box flex="1" gap={1}>
      **Autorización revocada**
      <text color="secondary" size="sm">Se bloquean nuevos cobros online hasta restablecer la conexión.</text>
    </box>
    <badge color="danger">Cobros bloqueados</badge>
  </row>
</box>

---

## Fase 4 — Recuperación automática de conexiones

<text color="secondary" weight="medium" size="xs">Pregunta 128 — ¿Cómo debería recuperarse una disciplina después de una falla temporal de su pasarela?</text>

Por ejemplo, Mercado Pago tiene una interrupción durante 10 minutos y después vuelve a funcionar normalmente.

**¿La plataforma debería reactivar los cobros automáticamente o esperar a un administrador?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Recuperación manual**

      Después de cualquier bloqueo, un administrador de disciplina deberá verificar la conexión y reactivar los cobros manualmente.
    </radio>
    <radio value="b">
      **Opción B — Recuperación automática**

      Cuando el sistema detecte que la conexión vuelve a funcionar, reactivará automáticamente los cobros, incluso si el bloqueo se originó por un problema de autorización.
    </radio>
    <radio value="c">
      **Opción C — Recuperación según el tipo de falla (recomendada)**

      Las interrupciones temporales podrán recuperarse automáticamente después de verificaciones exitosas.

      <text color="secondary" size="xs">Si la autorización fue revocada, la cuenta cambió o existe una configuración inválida, el administrador deberá completar nuevamente la vinculación correspondiente. La plataforma no reactivará cobros hasta confirmar que la conexión es válida. Toda recuperación quedará auditada y actualizará el incidente existente.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 128 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"exigir reactivación manual por un administrador después de cualquier bloqueo",b:"reactivar automáticamente los cobros cuando vuelva a funcionar la conexión, sin distinguir el origen de la falla",c:"recuperar automáticamente fallas temporales verificadas y exigir nueva vinculación administrativa para autorizaciones revocadas o configuraciones inválidas, con auditoría y actualización del incidente"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-128

### Registro 325 — Reactivación automática de conexiones
Fuente: `52d25648-40a4-40e6-a296-2d05bb7140fd`

Respuesta previa del usuario:

Para la pregunta 128 del relevamiento SDD elijo la opción B: reactivar automáticamente los cobros cuando vuelva a funcionar la conexión, sin distinguir el origen de la falla.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 128 — Confirmada: opción B**
</row>

La plataforma **reactivará automáticamente los cobros online cuando compruebe que la conexión volvió a funcionar**, sin exigir intervención manual del administrador, independientemente del tipo de falla que originó el bloqueo.

Una precisión técnica: si la pasarela revocó realmente la autorización, el sistema no podrá utilizarla hasta que vuelva a ser válida. No obstante, una vez restablecida, la reactivación será automática.

## Registro SDD — Decisión 128

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-128 — Reactivación automática de conexiones**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1166:** La plataforma verificará automáticamente la recuperación de las conexiones bloqueadas.
  - **RN-1167:** Cuando una conexión vuelva a estar operativa y autorizada, se habilitarán los nuevos cobros online.
  - **RN-1168:** No será necesaria una aprobación ni una reactivación manual del administrador.
  - **RN-1169:** La recuperación automática se aplicará independientemente de la clasificación original de la falla.
  - **RN-1170:** No se reactivarán cobros mientras la autorización de la pasarela continúe siendo inválida.
  - **RN-1171:** Cada disciplina se recuperará de forma independiente.
  - **RN-1172:** La recuperación no modificará los estados de pagos anteriores.
  - **RN-1173:** No se reintentarán automáticamente cobros que pudieran generar duplicaciones.
  - **RN-1174:** Se registrarán fecha, hora y resultado de la verificación de recuperación.
  - **RN-1175:** El incidente existente se actualizará como resuelto cuando corresponda.
  - **RN-1176:** Los administradores de la disciplina y el superadministrador podrán consultar el historial de la interrupción.
  - **RN-1177:** La reactivación respetará otros bloqueos independientes, como la suspensión comercial o la desactivación de la disciplina.
</box>

## Ejemplo

<box border radius="lg" padding={3} gap={2}>
  <list marker="number" connector="solid" gap={2}>
    <list-item>
      **10:00 — Interrupción**

      Mercado Pago deja de responder correctamente para Obras Futsal. Se suspenden temporalmente sus nuevos cobros.
    </list-item>
    <list-item>
      **10:10 — Servicio restablecido**

      La plataforma detecta que la conexión vuelve a responder y verifica que está autorizada.
    </list-item>
    <list-item>
      **10:11 — Reactivación automática**

      Se habilitan los cobros de Futsal sin intervención administrativa.
    </list-item>
  </list>
  <caption>Horarios ilustrativos. La recuperación real dependerá de las verificaciones y de la disponibilidad de la pasarela.</caption>
</box>

---

## Fase 4 — Configuración de monitoreo

<text color="secondary" weight="medium" size="xs">Pregunta 129 — ¿Quién debería configurar la frecuencia de verificación de las conexiones con las pasarelas?</text>

Por ejemplo, cada cuánto tiempo se comprueba si una conexión está operativa o si una conexión bloqueada ya puede recuperarse.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Frecuencia fija del sistema**

      La plataforma utiliza intervalos definidos técnicamente, sin permitir que los administradores los modifiquen.
    </radio>
    <radio value="b">
      **Opción B — Configuración por disciplina**

      Cada administrador de disciplina podrá definir la frecuencia con la que se verifica su conexión.
    </radio>
    <radio value="c">
      **Opción C — Configuración global por el superadministrador (recomendada)**

      El superadministrador podrá configurar los intervalos de verificación y recuperación para cada proveedor de pagos.

      <text color="secondary" size="xs">Las frecuencias respetarán los límites de las API y las recomendaciones de cada pasarela. Todas las disciplinas que utilicen un mismo proveedor seguirán las reglas generales correspondientes, conservando sus estados independientes.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 129 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"utilizar frecuencias técnicas fijas de monitoreo y recuperación",b:"permitir que cada administrador de disciplina configure la frecuencia de verificación de su conexión",c:"permitir que el superadministrador configure globalmente las frecuencias de verificación y recuperación por proveedor, respetando límites de API y aislamiento por disciplina"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-129

### Registro 327 — Configuración global del monitoreo de pasarelas
Fuente: `4b349d14-f615-428f-ae3d-f567926bc904`

Respuesta previa del usuario:

Para la pregunta 129 del relevamiento SDD elijo la opción C: permitir que el superadministrador configure globalmente las frecuencias de verificación y recuperación por proveedor, respetando límites de API y aislamiento por disciplina.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 129 — Confirmada: opción C**
</row>

El **superadministrador configurará globalmente las frecuencias de monitoreo y recuperación de cada pasarela de pagos**.

Todas las disciplinas que utilicen un mismo proveedor compartirán los parámetros técnicos, pero mantendrán sus conexiones y estados operativos independientes.

## Registro SDD — Decisión 129

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-129 — Configuración global del monitoreo de pasarelas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1178:** El superadministrador podrá configurar la frecuencia de verificación de conexiones por proveedor de pagos.
  - **RN-1179:** Podrá configurar los intervalos de verificación para recuperar conexiones bloqueadas.
  - **RN-1180:** Los parámetros serán globales para todas las disciplinas que utilicen el mismo proveedor.
  - **RN-1181:** Cada proveedor podrá tener una configuración de monitoreo diferente.
  - **RN-1182:** Las frecuencias deberán respetar los límites y restricciones técnicas de las API de cada pasarela.
  - **RN-1183:** Se evitarán verificaciones innecesarias que puedan provocar bloqueos por exceso de solicitudes.
  - **RN-1184:** Las conexiones conservarán estados operativos independientes por disciplina.
  - **RN-1185:** Una falla detectada en una conexión no bloqueará automáticamente las demás conexiones del proveedor.
  - **RN-1186:** Los cambios de frecuencia se aplicarán a las verificaciones futuras, sin alterar registros históricos.
  - **RN-1187:** Se registrarán los cambios de configuración, incluyendo valores anteriores, nuevos, fecha y usuario responsable.
  - **RN-1188:** Los administradores de disciplina podrán consultar el estado de su conexión, pero no modificar los parámetros globales.
  - **RN-1189:** La recuperación automática continuará respetando DEC-128.
  - **RN-1190:** El sistema podrá aplicar límites técnicos de seguridad para impedir configuraciones incompatibles con las restricciones del proveedor.
</box>

## Ejemplo de configuración

<box border radius="lg" padding={3} gap={2}>
  <text color="secondary" size="xs">Valores ilustrativos, no definitivos.</text>
  <table>
    <table-row><table-cell width="40%">**Proveedor**</table-cell><table-cell>**Verificación**</table-cell><table-cell>**Recuperación**</table-cell></table-row>
    <table-row><table-cell>Mercado Pago</table-cell><table-cell>5 min</table-cell><table-cell>2 min</table-cell></table-row>
    <table-row><table-cell>Otra pasarela</table-cell><table-cell>10 min</table-cell><table-cell>5 min</table-cell></table-row>
  </table>
  <row align="center" gap={2}>
    <icon name="shield-check" color="success"/>
    <text size="sm">Una configuración por proveedor, con aislamiento de conexiones por disciplina.</text>
  </row>
</box>

---

## Fase 4 — Configuración de proveedores de pago

<text color="secondary" weight="medium" size="xs">Pregunta 130 — ¿Quién podrá habilitar las pasarelas de pago disponibles en la plataforma?</text>

Supongamos que inicialmente integramos Mercado Pago y, más adelante, agregamos Mobbex u otra pasarela compatible con Split Payment.

Necesitamos definir quién controla qué proveedores pueden utilizar los clubes y sus disciplinas.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Una única pasarela obligatoria**

      Todas las disciplinas utilizarán la misma pasarela definida por la plataforma. No podrán elegir entre proveedores.
    </radio>
    <radio value="b">
      **Opción B — Cada disciplina elige libremente**

      Cada administrador de disciplina podrá seleccionar y configurar cualquier pasarela que esté técnicamente integrada, sin habilitación global adicional.
    </radio>
    <radio value="c">
      **Opción C — Catálogo global de pasarelas habilitadas (recomendada)**

      El superadministrador administrará qué proveedores integrados están disponibles para utilizar.

      <text color="secondary" size="xs">Cada disciplina podrá elegir entre los proveedores habilitados y vincular su cuenta de forma independiente. Deshabilitar un proveedor para nuevas vinculaciones no alterará pagos históricos; el tratamiento de conexiones existentes deberá contemplar operaciones pendientes y conciliaciones.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 130 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"utilizar una única pasarela obligatoria para todas las disciplinas",b:"permitir que cada disciplina elija libremente cualquier pasarela técnicamente integrada",c:"mantener un catálogo global de pasarelas habilitadas por el superadministrador, permitiendo elección y vinculación independiente por disciplina"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-130

### Registro 329 — Catálogo global de pasarelas de pago
Fuente: `b0155776-99e8-4c39-9916-20618b83a777`

Respuesta previa del usuario:

Para la pregunta 130 del relevamiento SDD elijo la opción C: mantener un catálogo global de pasarelas habilitadas por el superadministrador, permitiendo elección y vinculación independiente por disciplina.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 130 — Confirmada: opción C**
</row>

Queda definido que el **superadministrador gestionará un catálogo global de pasarelas de pago**, mientras que cada disciplina podrá elegir entre los proveedores habilitados y vincular su propia cuenta.

Esto mantiene la flexibilidad comercial de la plataforma sin perder el aislamiento financiero entre disciplinas.

## Registro SDD — Decisión 130

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-130 — Catálogo global de pasarelas de pago**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1191:** Existirá un catálogo global de proveedores de pago integrados.
  - **RN-1192:** Solo el superadministrador podrá habilitar o deshabilitar proveedores del catálogo.
  - **RN-1193:** Únicamente podrán habilitarse proveedores con una integración técnica implementada y compatible con el modelo de Split Payment.
  - **RN-1194:** Cada disciplina podrá seleccionar un proveedor habilitado.
  - **RN-1195:** Los administradores de disciplina podrán vincular su cuenta mediante los mecanismos establecidos en DEC-125.
  - **RN-1196:** Cada disciplina conservará una configuración de conexión independiente.
  - **RN-1197:** Varias disciplinas podrán utilizar el mismo proveedor sin compartir sus operaciones financieras.
  - **RN-1198:** La habilitación de un proveedor no conectará automáticamente las cuentas de las disciplinas.
  - **RN-1199:** Deshabilitar un proveedor impedirá nuevas vinculaciones mientras permanezca deshabilitado.
  - **RN-1200:** La deshabilitación no eliminará pagos, liquidaciones ni conciliaciones históricos.
  - **RN-1201:** Los administradores de disciplina no podrán habilitar proveedores globalmente.
  - **RN-1202:** El catálogo identificará el estado de disponibilidad de cada proveedor.
  - **RN-1203:** Los cambios de habilitación quedarán auditados con usuario, fecha, hora y valores anteriores y nuevos.
  - **RN-1204:** La gestión de proveedores respetará los parámetros globales de monitoreo definidos en DEC-129.
  - **RN-1205:** El comportamiento de las conexiones existentes al deshabilitar un proveedor se definirá por separado.
</box>

## Ejemplo — Catálogo de pasarelas

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Mercado Pago**
    <badge color="success">Habilitada</badge>
  </row>
  <text color="secondary" size="sm">Disponible para vincular cuentas de disciplinas, siempre que su integración de split esté implementada y validada.</text>
  <divider color="subtle"/>
  <row align="center" justify="between">
    **Mobbex**
    <badge>Deshabilitada</badge>
  </row>
  <text color="secondary" size="sm">No disponible para nuevas vinculaciones mientras permanezca deshabilitada.</text>
  <caption>Estados ilustrativos; todavía no seleccionamos ni validamos contractualmente un proveedor definitivo.</caption>
</box>

---

## Fase 4 — Deshabilitación de pasarelas

<text color="secondary" weight="medium" size="xs">Pregunta 131 — ¿Qué sucede con las disciplinas que ya utilizan una pasarela cuando el superadministrador la deshabilita?</text>

Por ejemplo, Obras Futsal ya tiene Mercado Pago vinculado y funcionando. El superadministrador decide deshabilitar Mercado Pago en el catálogo global.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo impedir nuevas vinculaciones**

      Las disciplinas que ya utilizan esa pasarela podrán continuar cobrando normalmente. Únicamente se impedirán nuevas conexiones.
    </radio>
    <radio value="b">
      **Opción B — Bloqueo inmediato de todos los cobros nuevos**

      Se bloquearán inmediatamente los nuevos cobros online de todas las disciplinas que utilicen ese proveedor, aunque sus conexiones estén funcionando.
    </radio>
    <radio value="c">
      **Opción C — Dos modalidades de deshabilitación (recomendada)**

      El superadministrador podrá elegir entre:

      <text color="secondary" size="sm">**Deshabilitación comercial:** impide nuevas vinculaciones, pero permite continuar cobrando a las disciplinas que ya lo utilizan.</text>

      <text color="secondary" size="sm">**Deshabilitación operativa:** bloquea nuevos cobros online de todas las disciplinas que utilizan ese proveedor, por ejemplo, ante un problema de seguridad.</text>

      <text color="secondary" size="xs">En ambos casos se preservan las operaciones anteriores, se continúa la conciliación cuando sea posible y se registran los cambios y notificaciones correspondientes.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 131 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"deshabilitar una pasarela solo para nuevas vinculaciones y permitir que las conexiones existentes sigan cobrando",b:"bloquear inmediatamente nuevos cobros de todas las disciplinas que utilicen la pasarela deshabilitada",c:"implementar deshabilitación comercial para impedir nuevas vinculaciones y deshabilitación operativa para bloquear nuevos cobros de conexiones existentes, conservando historial y conciliación"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-131

### Registro 331 — Deshabilitación comercial y operativa de pasarelas
Fuente: `c1211db6-74c2-4940-87be-39a84e959761`

Respuesta previa del usuario:

Para la pregunta 131 del relevamiento SDD elijo la opción C: implementar deshabilitación comercial para impedir nuevas vinculaciones y deshabilitación operativa para bloquear nuevos cobros de conexiones existentes, conservando historial y conciliación.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 131 — Confirmada: opción C**
</row>

Quedan definidas **dos modalidades de deshabilitación de pasarelas**: una comercial, que impide nuevas vinculaciones, y otra operativa, que bloquea nuevos cobros online de las disciplinas que utilizan el proveedor.

## Registro SDD — Decisión 131

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-131 — Deshabilitación comercial y operativa de pasarelas**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1206:** El superadministrador podrá deshabilitar un proveedor mediante dos modalidades: comercial y operativa.
  - **RN-1207:** La deshabilitación comercial impedirá crear nuevas vinculaciones con el proveedor.
  - **RN-1208:** Las disciplinas que ya tengan conexiones válidas podrán continuar cobrando durante una deshabilitación comercial.
  - **RN-1209:** La deshabilitación operativa bloqueará inmediatamente el inicio de nuevos cobros online mediante ese proveedor.
  - **RN-1210:** El bloqueo operativo afectará a todas las disciplinas que utilicen el proveedor deshabilitado.
  - **RN-1211:** Las demás pasarelas habilitadas continuarán funcionando normalmente.
  - **RN-1212:** Ninguna modalidad eliminará ni modificará pagos históricos.
  - **RN-1213:** Las operaciones iniciadas antes del bloqueo conservarán sus referencias y estados.
  - **RN-1214:** Se continuará conciliando esas operaciones cuando la información del proveedor esté disponible.
  - **RN-1215:** Los administradores de las disciplinas afectadas serán notificados cuando un bloqueo operativo interrumpa sus cobros.
  - **RN-1216:** El superadministrador podrá consultar el estado y alcance de la deshabilitación.
  - **RN-1217:** Cada cambio registrará modalidad, motivo, usuario responsable, fecha y hora.
  - **RN-1218:** El sistema distinguirá un bloqueo global del proveedor de una falla particular de conexión.
  - **RN-1219:** La recuperación automática de DEC-128 no podrá levantar un bloqueo operativo global decidido por el superadministrador.
  - **RN-1220:** Al levantar el bloqueo global, cada conexión deberá estar operativa y autorizada antes de permitir nuevos cobros.
  - **RN-1221:** Las cuotas pendientes seguirán disponibles para consulta y registro de pagos externos autorizados.
</box>

## Diferencia entre ambas modalidades

| Comportamiento | Comercial | Operativa |
|---|---|---|
| Nuevas vinculaciones | <icon name="x" color="danger"/> | <icon name="x" color="danger"/> |
| Nuevos cobros en conexiones existentes | <icon name="check" color="success"/> | <icon name="x" color="danger"/> |
| Consulta de cuotas | <icon name="check" color="success"/> | <icon name="check" color="success"/> |
| Conciliación de pagos anteriores | <icon name="check" color="success"/> | <icon name="check" color="success"/> |
| Historial financiero | Conservado | Conservado |

---

## Fase 4 — Reactivación de proveedores

<text color="secondary" weight="medium" size="xs">Pregunta 132 — ¿Cómo debería reactivarse una pasarela deshabilitada globalmente?</text>

Supongamos que el superadministrador bloqueó operativamente Mercado Pago por un incidente de seguridad. El problema se solucionó y ahora quiere volver a habilitar los cobros.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Reactivación manual inmediata**

      El superadministrador vuelve a habilitar el proveedor y todas las conexiones existentes que estén autorizadas pueden comenzar a cobrar inmediatamente.
    </radio>
    <radio value="b">
      **Opción B — Reactivación automática del proveedor**

      Cuando el sistema detecta que el proveedor vuelve a funcionar, levanta automáticamente la deshabilitación global, sin intervención del superadministrador.
    </radio>
    <radio value="c">
      **Opción C — Reactivación manual global y validación automática por disciplina (recomendada)**

      Solo el superadministrador podrá levantar una deshabilitación global.

      <text color="secondary" size="xs">Después, el sistema verificará automáticamente cada conexión de disciplina. Las que estén operativas y autorizadas volverán a cobrar sin intervención de sus administradores; las que tengan problemas particulares seguirán bloqueadas hasta recuperarse. Se conservará la auditoría y se actualizarán las notificaciones correspondientes.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 132 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir al superadministrador reactivar manualmente el proveedor y habilitar inmediatamente las conexiones autorizadas",b:"levantar automáticamente la deshabilitación global cuando el proveedor vuelva a funcionar",c:"exigir reactivación global por el superadministrador y luego validar automáticamente cada conexión por disciplina, habilitando solo las operativas y autorizadas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-132

### Registro 333 — Reactivación global con validación individual por disciplina
Fuente: `7aac55b2-274f-4d12-b681-3619c114a37f`

Respuesta previa del usuario:

Para la pregunta 132 del relevamiento SDD elijo la opción C: exigir reactivación global por el superadministrador y luego validar automáticamente cada conexión por disciplina, habilitando solo las operativas y autorizadas.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 132 — Confirmada: opción C**
</row>

La reactivación de una pasarela deshabilitada globalmente requerirá una acción del **superadministrador**. Después, el sistema verificará automáticamente las conexiones de cada disciplina y habilitará únicamente aquellas que estén operativas y autorizadas.

Esto mantiene la independencia entre disciplinas y evita que una conexión defectuosa afecte a las demás.

## Registro SDD — Decisión 132

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-132 — Reactivación global con validación individual por disciplina**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1222:** Solo el superadministrador podrá levantar una deshabilitación global de una pasarela.
  - **RN-1223:** La reactivación global requerirá una acción administrativa explícita.
  - **RN-1224:** El monitoreo automático no podrá levantar por sí mismo una deshabilitación global.
  - **RN-1225:** Al reactivar el proveedor, se iniciará la validación de las conexiones de las disciplinas afectadas.
  - **RN-1226:** Cada conexión se verificará de forma independiente.
  - **RN-1227:** Las conexiones operativas y autorizadas podrán reanudar automáticamente sus cobros online.
  - **RN-1228:** No será necesaria una reactivación manual por parte de cada administrador de disciplina.
  - **RN-1229:** Las conexiones que continúen presentando fallas permanecerán bloqueadas.
  - **RN-1230:** Una conexión con problemas no impedirá la recuperación de las demás.
  - **RN-1231:** La reactivación respetará otros bloqueos vigentes, como la suspensión comercial o la desactivación de una disciplina.
  - **RN-1232:** Los pagos iniciados antes de la deshabilitación conservarán su estado e historial.
  - **RN-1233:** No se generarán nuevos cobros automáticamente por el solo hecho de reactivar el proveedor.
  - **RN-1234:** La reactivación global registrará usuario responsable, fecha, hora y proveedor afectado.
  - **RN-1235:** Se auditará el resultado de la validación de cada conexión.
  - **RN-1236:** Los incidentes y notificaciones correspondientes se actualizarán según el estado real de recuperación.
  - **RN-1237:** Las conexiones que permanezcan bloqueadas continuarán sujetas al monitoreo y recuperación automática definidos en DEC-127 y DEC-128.
</box>

## Ejemplo — Reactivación de Mercado Pago

<box border radius="lg" padding={3} gap={3}>
  <row align="center" gap={2}>
    <icon name="shield-check" color="success" size="lg"/>
    <box flex="1" gap={1}>
      **Superadministrador reactiva Mercado Pago**
      <text color="secondary" size="xs">Se elimina el bloqueo global y comienza la validación individual.</text>
    </box>
  </row>
  <divider color="subtle"/>
  <grid columns={2} gap={2}>
    <grid-item>
      <box border radius="md" padding={3} gap={2}>
        **Obras Futsal**
        <badge color="success">Conexión válida</badge>
        <row align="center" gap={1}>
          <icon name="check-circle" color="success"/>
          <text size="xs">Cobros habilitados</text>
        </row>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="md" padding={3} gap={2}>
        **Obras Básquet**
        <badge color="danger">Conexión inválida</badge>
        <row align="center" gap={1}>
          <icon name="lock" color="danger"/>
          <text size="xs">Cobros bloqueados</text>
        </row>
      </box>
    </grid-item>
  </grid>
  <caption>Ambas disciplinas utilizan el mismo proveedor, pero su recuperación depende del estado de su propia conexión.</caption>
</box>

---

## Fase 4 — Selección y cambio de pasarelas

<text color="secondary" weight="medium" size="xs">Pregunta 133 — ¿Una disciplina podrá cambiar de pasarela de pagos después de comenzar a operar?</text>

Por ejemplo, Obras Futsal comienza cobrando mediante Mercado Pago y, meses después, quiere utilizar Mobbex porque ofrece mejores condiciones comerciales.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Pasarela fija**

      Una vez que la disciplina comienza a cobrar, no podrá cambiar de proveedor. Deberá mantener la pasarela original.
    </radio>
    <radio value="b">
      **Opción B — Cambio inmediato**

      El administrador podrá elegir otra pasarela habilitada y comenzar a utilizarla inmediatamente, sin un proceso especial de transición.
    </radio>
    <radio value="c">
      **Opción C — Cambio permitido con transición controlada (recomendada)**

      El administrador podrá vincular una nueva pasarela habilitada y convertirla en la principal de su disciplina.

      <text color="secondary" size="xs">Los nuevos cobros utilizarán el nuevo proveedor cuando su conexión esté validada. Las operaciones iniciadas con la pasarela anterior conservarán su referencia y continuarán conciliándose con ella. El cambio no alterará pagos históricos ni generará duplicaciones. Se aplicará la verificación de seguridad de DEC-124.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 133 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"mantener fija la pasarela original de cada disciplina",b:"permitir cambio inmediato de pasarela sin transición especial",c:"permitir cambiar de pasarela con transición controlada, validación de la nueva conexión, preservación y conciliación de operaciones anteriores y verificación OTP"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-133

### Registro 335 — Cambio de pasarela con transición controlada
Fuente: `395cb944-1813-4769-87cc-beebfe8d472e`

Respuesta previa del usuario:

Para la pregunta 133 del relevamiento SDD elijo la opción C: permitir cambiar de pasarela con transición controlada, validación de la nueva conexión, preservación y conciliación de operaciones anteriores y verificación OTP.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 133 — Confirmada: opción C**
</row>

Cada disciplina podrá **cambiar de pasarela de pagos sin perder su historial financiero**, mediante una transición controlada.

Por ejemplo, Obras Futsal podrá migrar de Mercado Pago a Mobbex, mientras que Obras Básquet continuará utilizando Mercado Pago sin verse afectado.

## Registro SDD — Decisión 133

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-133 — Cambio de pasarela con transición controlada**
    <badge color="success">Aprobada</badge>
  </row>
  **Reglas de negocio:**

  - **RN-1238:** Los administradores de disciplina podrán solicitar el cambio de pasarela dentro de su ámbito.
  - **RN-1239:** La nueva pasarela deberá estar habilitada en el catálogo global.
  - **RN-1240:** El cambio requerirá la verificación OTP definida en DEC-124.
  - **RN-1241:** La nueva conexión deberá completar la autorización y validación correspondientes.
  - **RN-1242:** Se verificará que la nueva integración sea compatible con el Split Payment requerido.
  - **RN-1243:** La disciplina mantendrá una única pasarela principal para iniciar nuevos cobros online.
  - **RN-1244:** La nueva pasarela comenzará a utilizarse únicamente después de confirmar su activación.
  - **RN-1245:** Los pagos iniciados antes del cambio conservarán su proveedor y referencia originales.
  - **RN-1246:** Las operaciones anteriores continuarán conciliándose mediante la información del proveedor con el que fueron creadas.
  - **RN-1247:** El cambio no modificará pagos, comisiones ni liquidaciones históricas.
  - **RN-1248:** No se crearán pagos duplicados durante la transición.
  - **RN-1249:** Si falla la validación de la nueva pasarela, no se activará el cambio y se conservará la configuración anterior, si sigue operativa.
  - **RN-1250:** La migración de una disciplina no afectará las conexiones de otras disciplinas.
  - **RN-1251:** Se conservará el historial de proveedores utilizados por cada disciplina.
  - **RN-1252:** Se auditarán el administrador responsable, fecha, hora, proveedor anterior y proveedor nuevo.
  - **RN-1253:** La plataforma permitirá consultar y conciliar operaciones históricas de proveedores anteriores, siempre que sus mecanismos de consulta continúen disponibles.
</box>

## Ejemplo — Migración de pasarela

<box border radius="lg" padding={3} gap={2}>
  <row align="center" gap={2}>
    <icon name="arrow-left-right" size="lg" color="secondary"/>
    **Obras Futsal**
  </row>
  <grid columns={2} gap={2}>
    <grid-item>
      <box border radius="md" padding={3} gap={2}>
        <text color="secondary" size="xs">Pasarela anterior</text>
        **Mercado Pago**
        <badge>Histórica</badge>
        <text size="xs">Mantiene pagos y conciliaciones anteriores.</text>
      </box>
    </grid-item>
    <grid-item>
      <box border radius="md" padding={3} gap={2}>
        <text color="secondary" size="xs">Nueva pasarela</text>
        **Mobbex**
        <badge color="success">Principal</badge>
        <text size="xs">Procesa los nuevos cobros después de su activación.</text>
      </box>
    </grid-item>
  </grid>
  <caption>Ejemplo ilustrativo. La disponibilidad real de cada pasarela dependerá de su integración y compatibilidad con split.</caption>
</box>

---

## Fase 4 — Pagos durante una migración

<text color="secondary" weight="medium" size="xs">Pregunta 134 — ¿Qué hacemos con los pagos pendientes que ya fueron iniciados en la pasarela anterior?</text>

Supongamos que un responsable abrió un checkout de Mercado Pago, pero todavía no terminó de pagar. Mientras tanto, el administrador de Futsal cambia la pasarela principal a Mobbex.

**¿Qué debería pasar con ese checkout anterior?**

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Cancelar todos los checkouts anteriores**

      Al activar la nueva pasarela, se intentan cancelar los pagos pendientes de la anterior. Los responsables deberán iniciar nuevamente el pago.
    </radio>
    <radio value="b">
      **Opción B — Mantener todos los checkouts anteriores**

      Los checkouts ya iniciados podrán completarse normalmente con la pasarela anterior, aunque la disciplina ya esté utilizando una nueva.
    </radio>
    <radio value="c">
      **Opción C — Respetar checkouts vigentes y controlar duplicaciones (recomendada)**

      Los checkouts anteriores podrán completarse mientras sigan vigentes y el proveedor lo permita.

      <text color="secondary" size="xs">La plataforma identificará las cuotas involucradas y evitará generar un nuevo cobro incompatible mientras exista una operación pendiente válida. Si el checkout vence, se cancela o falla definitivamente, el responsable podrá iniciar otro con la nueva pasarela. Se confirmarán los resultados mediante eventos y conciliación, sin asumir que cerrar la pantalla cancela el pago.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 134 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"intentar cancelar los checkouts pendientes de la pasarela anterior y exigir nuevos pagos",b:"permitir que todos los checkouts iniciados sigan completándose en la pasarela anterior",c:"mantener checkouts anteriores mientras estén vigentes, controlar duplicaciones por cuota y permitir nuevos intentos en la nueva pasarela solo tras vencimiento, cancelación o falla definitiva"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-134

### Registro 337 — Continuidad de checkouts y prevención de cobros duplicados
Fuente: `39977ea5-190b-44fe-8239-47b98a9e7888`

Respuesta previa del usuario:

Para la pregunta 134 del relevamiento SDD elijo la opción C: mantener checkouts anteriores mientras estén vigentes, controlar duplicaciones por cuota y permitir nuevos intentos en la nueva pasarela solo tras vencimiento, cancelación o falla definitiva.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 134 — Confirmada: opción C**
</row>

Durante una migración de pasarela, **los checkouts ya iniciados seguirán siendo válidos mientras no hayan vencido, sido cancelados o fallado definitivamente**.

El objetivo es evitar que un jugador termine pagando dos veces la misma cuota.

## Registro SDD — Decisión 134

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-134 — Continuidad de checkouts y prevención de cobros duplicados**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1254:** Los checkouts iniciados antes del cambio de pasarela conservarán su proveedor original.
  - **RN-1255:** Podrán completarse mientras estén vigentes y el proveedor permita procesarlos.
  - **RN-1256:** Cada checkout mantendrá identificadas las cuotas y el jugador o jugadores involucrados.
  - **RN-1257:** Una cuota con una operación de pago pendiente válida no podrá incorporarse a otro checkout incompatible.
  - **RN-1258:** La restricción de duplicados se aplicará aunque las operaciones utilicen pasarelas diferentes.
  - **RN-1259:** Cerrar la pantalla del checkout no significará que el pago fue cancelado.
  - **RN-1260:** El sistema verificará el estado real de la operación mediante eventos, consultas y conciliación.
  - **RN-1261:** Si el checkout vence, se cancela o falla definitivamente, sus cuotas podrán volver a estar disponibles para un nuevo intento, siempre que no estén pagadas.
  - **RN-1262:** Los nuevos intentos utilizarán la pasarela principal vigente de la disciplina.
  - **RN-1263:** Los eventos repetidos de la pasarela deberán procesarse de forma idempotente.
  - **RN-1264:** Un pago confirmado no podrá volver a imputarse sobre las mismas cuotas.
  - **RN-1265:** Las operaciones de distintos proveedores conservarán identificadores y trazabilidad independientes.
  - **RN-1266:** Los pagos confirmados en la pasarela anterior continuarán conciliándose con ella.
  - **RN-1267:** Ante un estado incierto, el sistema no habilitará un segundo cobro hasta verificar que el anterior no puede completarse.
  - **RN-1268:** Se registrarán los cambios de estado y los intentos de pago asociados a cada cuota.
  - **RN-1269:** La transición no afectará las operaciones de otras disciplinas.
</box>

## Ejemplo de funcionamiento

<box border radius="lg" padding={3} gap={2}>
  <list marker="number" connector="solid" gap={2}>
    <list-item>
      **10:00 — Checkout creado**

      Un responsable inicia el pago de una cuota de Futsal mediante Mercado Pago.
    </list-item>
    <list-item>
      **10:02 — Cambio de pasarela**

      El administrador activa Mobbex como proveedor principal. El checkout anterior sigue vigente.
    </list-item>
    <list-item>
      **10:05 — Nuevo intento**

      El responsable intenta pagar nuevamente la misma cuota. El sistema impide generar un cobro duplicado mientras el anterior continúe pendiente.
    </list-item>
    <list-item>
      **10:15 — Resolución**

      Si el checkout anterior venció y se confirmó que no se pagó, el responsable podrá iniciar otro con Mobbex.
    </list-item>
  </list>
  <caption>Horarios ilustrativos. Los vencimientos reales dependerán de cada pasarela.</caption>
</box>

---

## Fase 4 — Vencimiento de checkouts

<text color="secondary" weight="medium" size="xs">Pregunta 135 — ¿Cómo debería determinarse cuánto tiempo permanece vigente un checkout pendiente?</text>

Esto es importante porque, mientras un checkout siga vigente, las cuotas involucradas estarán protegidas contra nuevos intentos de cobro incompatibles.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Vencimiento fijo de la plataforma**

      Todos los checkouts tendrán una duración fija, por ejemplo, 15 minutos, independientemente de la pasarela utilizada.
    </radio>
    <radio value="b">
      **Opción B — Vencimiento definido exclusivamente por la pasarela**

      La plataforma respetará el vencimiento informado por cada proveedor, sin aplicar reglas adicionales.
    </radio>
    <radio value="c">
      **Opción C — Vencimiento según proveedor y verificación de estado (recomendada)**

      Se respetará el vencimiento real de cada checkout según la pasarela y su modalidad de pago.

      <text color="secondary" size="xs">La plataforma podrá definir tiempos internos para detectar operaciones aparentemente abandonadas, pero no liberará cuotas únicamente porque haya transcurrido ese plazo. Antes de permitir otro checkout, verificará que el pago anterior haya vencido, sido cancelado o fallado definitivamente. Si el estado es incierto, mantendrá la protección contra duplicados y continuará la conciliación.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 135 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"establecer un vencimiento fijo para todos los checkouts independientemente de la pasarela",b:"respetar exclusivamente el vencimiento informado por cada pasarela",c:"respetar vencimientos reales de cada proveedor y modalidad, usar tiempos internos solo para monitoreo y verificar el estado definitivo antes de liberar cuotas para otro intento"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-135

### Registro 339 — Vigencia de checkouts y liberación segura de cuotas
Fuente: `1aae1e61-1d9a-407e-a68d-f654b080c5af`

Respuesta previa del usuario:

Para la pregunta 135 del relevamiento SDD elijo la opción C: respetar vencimientos reales de cada proveedor y modalidad, usar tiempos internos solo para monitoreo y verificar el estado definitivo antes de liberar cuotas para otro intento.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 135 — Confirmada: opción C**
</row>

La plataforma respetará **los vencimientos reales de cada pasarela y modalidad de pago**. Los tiempos internos servirán para monitorear operaciones, pero nunca para liberar automáticamente una cuota sin comprobar el estado del pago anterior.

## Registro SDD — Decisión 135

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-135 — Vigencia de checkouts y liberación segura de cuotas**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1270:** Cada checkout conservará su proveedor, modalidad de pago y vencimiento informado.
  - **RN-1271:** La vigencia del checkout dependerá de las reglas reales de su pasarela.
  - **RN-1272:** No se impondrá un vencimiento universal que contradiga las condiciones del proveedor.
  - **RN-1273:** La plataforma podrá establecer tiempos internos para detectar operaciones aparentemente abandonadas.
  - **RN-1274:** Un tiempo interno de monitoreo no implicará la cancelación de un checkout.
  - **RN-1275:** Las cuotas permanecerán protegidas contra nuevos cobros incompatibles mientras exista una operación pendiente válida.
  - **RN-1276:** Antes de liberar una cuota, se verificará el estado real del pago anterior.
  - **RN-1277:** Una cuota podrá liberarse cuando se confirme que el checkout venció, fue cancelado o falló definitivamente, siempre que no exista un pago confirmado asociado.
  - **RN-1278:** Si el estado de la operación es incierto, la cuota permanecerá protegida contra duplicaciones.
  - **RN-1279:** El sistema continuará consultando y conciliando las operaciones cuyo estado no esté resuelto.
  - **RN-1280:** Los eventos tardíos o repetidos de la pasarela deberán procesarse de forma idempotente.
  - **RN-1281:** La liberación de cuotas no eliminará el historial del checkout anterior.
  - **RN-1282:** Los nuevos intentos utilizarán la pasarela principal vigente de la disciplina.
  - **RN-1283:** Se conservará la trazabilidad entre cuotas, checkouts e intentos de pago.
  - **RN-1284:** Las reglas se aplicarán independientemente a cada disciplina.
</box>

## Ejemplo — Checkout pendiente

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Cuota de Futsal**
    <badge color="warning">Pago pendiente</badge>
  </row>
  <text color="secondary" size="sm">Un responsable inició un checkout y luego cerró la pantalla.</text>
  <divider color="subtle"/>
  <row align="center" gap={2}>
    <icon name="clock" color="warning"/>
    <box flex="1" gap={1}>
      **Se supera el tiempo interno de monitoreo**
      <text size="sm" color="secondary">La plataforma consulta el estado, pero no libera automáticamente la cuota.</text>
    </box>
  </row>
  <row align="center" gap={2}>
    <icon name="shield-check" color="success"/>
    <box flex="1" gap={1}>
      **La pasarela confirma el vencimiento sin pago**
      <text size="sm" color="secondary">La cuota vuelve a estar disponible para iniciar otro checkout.</text>
    </box>
  </row>
</box>

---

## Fase 4 — Operaciones con estado incierto

<text color="secondary" weight="medium" size="xs">Pregunta 136 — ¿Qué debería ocurrir si una pasarela no puede confirmar el estado de un pago durante un período prolongado?</text>

Por ejemplo, un responsable intentó pagar una cuota, pero la pasarela dejó de responder. Pasaron varias horas y todavía no sabemos si el dinero fue cobrado.

El objetivo es evitar cobros duplicados sin dejar cuotas bloqueadas indefinidamente.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Mantener el bloqueo indefinidamente**

      La cuota seguirá bloqueada hasta que la pasarela confirme automáticamente un resultado definitivo, sin intervención administrativa.
    </radio>
    <radio value="b">
      **Opción B — Liberación automática después de un plazo**

      Si transcurre un tiempo configurable sin respuesta, la plataforma libera la cuota para intentar pagar nuevamente, aunque el estado anterior siga siendo incierto.
    </radio>
    <radio value="c">
      **Opción C — Revisión administrativa sin liberar cobros inciertos (recomendada)**

      La plataforma continuará intentando verificar y conciliar el pago. Si supera un plazo configurable, generará una alerta para los administradores de la disciplina y el superadministrador.

      <text color="secondary" size="xs">Los administradores podrán revisar la operación y aportar información o evidencia, pero no liberar la cuota para otro cobro online mientras exista riesgo de que el pago anterior se confirme. La cuota se liberará únicamente cuando se obtenga evidencia suficiente de que el cobro anterior no se completó, dejando toda resolución auditada.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 136 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"mantener bloqueadas las cuotas de pagos inciertos hasta recibir confirmación automática definitiva",b:"liberar automáticamente las cuotas tras un plazo configurable aunque el pago anterior siga incierto",c:"mantener conciliación y protección contra duplicados, alertar para revisión administrativa tras un plazo y liberar solo con evidencia suficiente de que el pago anterior no se completó"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-136

### Registro 341 — Revisión de pagos inciertos y liberación segura de cuotas
Fuente: `367e9c3b-6951-44fc-9b00-d0a2c4ea5975`

Respuesta previa del usuario:

Para la pregunta 136 del relevamiento SDD elijo la opción C: mantener conciliación y protección contra duplicados, alertar para revisión administrativa tras un plazo y liberar solo con evidencia suficiente de que el pago anterior no se completó.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 136 — Confirmada: opción C**
</row>

Cuando una pasarela no pueda confirmar si un pago se realizó, la plataforma mantendrá la cuota protegida contra cobros duplicados y continuará intentando conciliar la operación.

Si el problema persiste, **se generará una alerta para revisión administrativa**, pero no se permitirá cobrar nuevamente sin evidencia suficiente de que el pago anterior no se completó.

## Registro SDD — Decisión 136

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-136 — Revisión de pagos inciertos y liberación segura de cuotas**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1285:** Las operaciones cuyo resultado no pueda confirmarse se identificarán como pagos de estado incierto.
  - **RN-1286:** Las cuotas involucradas permanecerán protegidas contra nuevos cobros incompatibles.
  - **RN-1287:** La plataforma continuará consultando y conciliando el estado de estas operaciones.
  - **RN-1288:** Se establecerá un plazo configurable para determinar cuándo un pago incierto requiere revisión administrativa.
  - **RN-1289:** Al superar ese plazo, se alertará a los administradores de la disciplina afectada.
  - **RN-1290:** El superadministrador también recibirá la alerta.
  - **RN-1291:** Los avisos se agruparán por incidente para evitar duplicaciones.
  - **RN-1292:** Los administradores autorizados podrán consultar el detalle y los intentos de conciliación.
  - **RN-1293:** Podrán registrar observaciones y aportar evidencia para la revisión.
  - **RN-1294:** La revisión administrativa no permitirá liberar arbitrariamente cuotas cuyo pago anterior todavía pueda confirmarse.
  - **RN-1295:** Una cuota solo podrá liberarse cuando exista evidencia suficiente de que el cobro anterior no se completó.
  - **RN-1296:** La resolución deberá identificar la evidencia utilizada y el usuario o proceso responsable.
  - **RN-1297:** Toda resolución y liberación quedará auditada.
  - **RN-1298:** Los pagos confirmados tardíamente deberán tratarse de forma idempotente, evitando imputaciones duplicadas.
  - **RN-1299:** La resolución del incidente actualizará las alertas correspondientes.
  - **RN-1300:** Las reglas se aplicarán exclusivamente a las operaciones de la disciplina involucrada.
</box>

## Ejemplo — Pago de resultado incierto

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Cuota de Obras Futsal**
    <badge color="warning">Estado incierto</badge>
  </row>
  <list marker="number" connector="solid" gap={2}>
    <list-item>El responsable intenta pagar y la pasarela deja de responder.</list-item>
    <list-item>El sistema conserva la operación pendiente y evita generar otro cobro de la misma cuota.</list-item>
    <list-item>Se supera el plazo de revisión: los administradores reciben una alerta.</list-item>
    <list-item>La conciliación confirma que no se realizó el cobro, o se obtiene evidencia suficiente para resolverlo.</list-item>
    <list-item>La cuota vuelve a estar disponible para un nuevo intento, con la resolución auditada.</list-item>
  </list>
</box>

---

## Fase 4 — Configuración de alertas de pagos inciertos

<text color="secondary" weight="medium" size="xs">Pregunta 137 — ¿Quién debería configurar el plazo para alertar sobre pagos de estado incierto?</text>

Por ejemplo, podríamos generar una alerta cuando un pago lleva 30 minutos, 2 horas o más tiempo sin un resultado verificable.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Plazo fijo del sistema**

      La plataforma utilizará un único plazo técnico para todos los pagos inciertos, sin permitir modificaciones administrativas.
    </radio>
    <radio value="b">
      **Opción B — Configuración por disciplina**

      Cada administrador de disciplina podrá elegir cuánto tiempo esperar antes de recibir alertas sobre sus pagos inciertos.
    </radio>
    <radio value="c">
      **Opción C — Configuración global por pasarela (recomendada)**

      El superadministrador podrá configurar el plazo de alerta para cada proveedor de pagos.

      <text color="secondary" size="xs">Esto permite contemplar diferencias entre proveedores y modalidades de pago. Todas las disciplinas que utilicen la misma pasarela seguirán los parámetros correspondientes, pero las alertas y operaciones permanecerán aisladas por disciplina. El plazo solo activa la revisión: nunca libera automáticamente las cuotas.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 137 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"establecer un plazo técnico fijo para alertas de pagos inciertos",b:"permitir que cada disciplina configure su propio plazo de alerta de pagos inciertos",c:"permitir que el superadministrador configure globalmente plazos de alerta por pasarela, conservando aislamiento por disciplina y sin liberar cuotas automáticamente"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-137

### Registro 343 — Plazos globales de alerta para pagos inciertos
Fuente: `8cc228ed-7f3b-4b7d-bade-b6356e6f5bb1`

Respuesta previa del usuario:

Para la pregunta 137 del relevamiento SDD elijo la opción C: permitir que el superadministrador configure globalmente plazos de alerta por pasarela, conservando aislamiento por disciplina y sin liberar cuotas automáticamente.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 137 — Confirmada: opción C**
</row>

El **superadministrador podrá configurar el plazo de alerta para pagos inciertos según la pasarela de pagos**.

Todas las disciplinas que utilicen el mismo proveedor compartirán ese parámetro, pero sus operaciones, alertas y conciliaciones permanecerán completamente separadas.

## Registro SDD — Decisión 137

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-137 — Plazos globales de alerta para pagos inciertos**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1301:** El superadministrador podrá configurar el plazo de alerta de pagos inciertos.
  - **RN-1302:** Cada proveedor de pagos podrá tener un plazo diferente.
  - **RN-1303:** Todas las disciplinas que utilicen un mismo proveedor compartirán su configuración global.
  - **RN-1304:** Los administradores de disciplina no podrán modificar estos plazos.
  - **RN-1305:** El plazo se calculará desde el momento en que la operación ingrese en estado incierto.
  - **RN-1306:** Al superar el plazo configurado, se generará una alerta para los administradores de la disciplina afectada.
  - **RN-1307:** El superadministrador también recibirá la alerta.
  - **RN-1308:** Las alertas conservarán la identificación del proveedor, disciplina y operación.
  - **RN-1309:** Las notificaciones repetidas de una misma incidencia se agruparán.
  - **RN-1310:** El vencimiento del plazo no modificará automáticamente el estado financiero de la operación.
  - **RN-1311:** El plazo no autorizará liberar cuotas ni iniciar nuevos cobros incompatibles.
  - **RN-1312:** La conciliación continuará hasta obtener información suficiente para resolver el pago.
  - **RN-1313:** Los cambios de configuración se aplicarán a las evaluaciones futuras, sin modificar eventos de alerta históricos.
  - **RN-1314:** Toda modificación del plazo registrará usuario, fecha, hora y valores anteriores y nuevos.
  - **RN-1315:** El monitoreo respetará el aislamiento de datos entre disciplinas.
</box>

## Ejemplo de configuración

<box border radius="lg" padding={3} gap={2}>
  <caption>Valores ilustrativos; todavía no definimos los plazos reales.</caption>
  <table>
    <table-row>
      <table-cell>**Pasarela**</table-cell>
      <table-cell>**Plazo de alerta**</table-cell>
      <table-cell>**Acción**</table-cell>
    </table-row>
    <table-row>
      <table-cell>Mercado Pago</table-cell>
      <table-cell>2 horas</table-cell>
      <table-cell>Revisión administrativa</table-cell>
    </table-row>
    <table-row>
      <table-cell>Mobbex</table-cell>
      <table-cell>4 horas</table-cell>
      <table-cell>Revisión administrativa</table-cell>
    </table-row>
  </table>
  <row align="center" gap={2}>
    <icon name="shield-check" color="success"/>
    <text size="sm">Las cuotas permanecen protegidas hasta confirmar que pueden liberarse de forma segura.</text>
  </row>
</box>

---

## Fase 4 — Evidencia para resolver pagos inciertos

<text color="secondary" weight="medium" size="xs">Pregunta 138 — ¿Qué evidencia será suficiente para liberar una cuota bloqueada por un pago incierto?</text>

Supongamos que Mercado Pago no puede confirmar el resultado de una operación y el administrador de Futsal necesita resolverla.

Debemos establecer qué información permitirá determinar que la cuota puede volver a cobrarse sin riesgo de duplicación.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Declaración del administrador**

      El administrador podrá indicar manualmente que el pago no se realizó y liberar la cuota, dejando una justificación auditada.
    </radio>
    <radio value="b">
      **Opción B — Confirmación exclusiva de la pasarela**

      La cuota solo podrá liberarse cuando el proveedor confirme oficialmente que el pago venció, fue cancelado o falló definitivamente.
    </radio>
    <radio value="c">
      **Opción C — Evidencia verificable y resolución controlada (recomendada)**

      Se priorizará la confirmación oficial de la pasarela. También podrá admitirse evidencia externa verificable cuando el proveedor no permita obtener una respuesta definitiva.

      <text color="secondary" size="xs">En casos excepcionales, el superadministrador podrá resolver manualmente la operación con justificación y evidencia obligatorias, siempre que se haya descartado razonablemente un cobro exitoso. Una simple declaración del administrador de disciplina no será suficiente. Toda resolución quedará auditada y continuará siendo rastreable ante eventos tardíos.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 138 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir liberar cuotas por declaración justificada del administrador de disciplina",b:"liberar cuotas exclusivamente ante confirmación oficial definitiva de la pasarela",c:"priorizar confirmación oficial, admitir evidencia externa verificable y permitir resolución excepcional del superadministrador con evidencia y justificación obligatorias"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-138

### Registro 345 — Evidencia y resolución excepcional de pagos inciertos
Fuente: `91f11397-6587-4a08-8ac8-c796200f47cb`

Respuesta previa del usuario:

Para la pregunta 138 del relevamiento SDD elijo la opción C: priorizar confirmación oficial, admitir evidencia externa verificable y permitir resolución excepcional del superadministrador con evidencia y justificación obligatorias.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 138 — Confirmada: opción C**
</row>

Queda establecido que **la confirmación oficial de la pasarela será la evidencia principal** para resolver pagos inciertos.

En situaciones excepcionales, el superadministrador podrá liberar una cuota mediante una resolución manual, pero deberá presentar evidencia verificable y una justificación obligatoria.

## Registro SDD — Decisión 138

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-138 — Evidencia y resolución excepcional de pagos inciertos**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1316:** Se priorizará la confirmación oficial de la pasarela para determinar el resultado de un pago incierto.
  - **RN-1317:** Una confirmación definitiva de vencimiento, cancelación o fallo permitirá evaluar la liberación de las cuotas afectadas.
  - **RN-1318:** Antes de liberar una cuota, se verificará que no exista otro pago confirmado asociado.
  - **RN-1319:** Cuando la pasarela no proporcione una respuesta definitiva, podrá considerarse evidencia externa verificable.
  - **RN-1320:** La evidencia externa deberá permitir identificar la operación y sustentar razonablemente que no se completó el cobro.
  - **RN-1321:** Los administradores de disciplina podrán aportar observaciones y documentación para la revisión.
  - **RN-1322:** Una declaración del administrador de disciplina, por sí sola, no será suficiente para liberar una cuota.
  - **RN-1323:** Solo el superadministrador podrá autorizar una resolución manual excepcional de un pago incierto.
  - **RN-1324:** Toda resolución manual excepcional requerirá una justificación obligatoria.
  - **RN-1325:** También requerirá evidencia verificable asociada a la operación.
  - **RN-1326:** Antes de resolver, deberá haberse descartado razonablemente la existencia de un cobro exitoso.
  - **RN-1327:** No se permitirá resolver manualmente como impago una operación que tenga confirmación oficial de pago exitoso.
  - **RN-1328:** La resolución conservará la identificación del proveedor, disciplina, operación y cuotas afectadas.
  - **RN-1329:** Se registrarán el superadministrador responsable, fecha, hora, justificación y referencias de evidencia.
  - **RN-1330:** La liberación no eliminará el historial del intento de pago original.
  - **RN-1331:** Los eventos tardíos de la pasarela seguirán procesándose y podrán detectar discrepancias posteriores.
  - **RN-1332:** Una discrepancia posterior no generará automáticamente una segunda imputación ni alterará silenciosamente el historial financiero.
  - **RN-1333:** Las discrepancias deberán ingresar al circuito de conciliación y resolución correspondiente.
  - **RN-1334:** La resolución actualizará el incidente y las notificaciones asociadas.
  - **RN-1335:** Las operaciones de otras disciplinas no se verán afectadas.
</box>

## Ejemplo — Resolución excepcional

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    **Pago de Obras Futsal**
    <badge color="warning">Estado incierto</badge>
  </row>
  <text color="secondary" size="sm">La pasarela no confirma el resultado de una operación y las verificaciones automáticas no logran resolverla.</text>
  <list marker="number" connector="solid" gap={2}>
    <list-item>El administrador de Futsal aporta la documentación disponible.</list-item>
    <list-item>El superadministrador revisa la evidencia y las consultas realizadas al proveedor.</list-item>
    <list-item>Si existe evidencia suficiente de que no se cobró, registra la justificación y autoriza la liberación.</list-item>
    <list-item>La cuota vuelve a estar disponible, conservando todo el historial del intento anterior.</list-item>
  </list>
  <caption>Si posteriormente aparece un pago exitoso del intento original, se tratará como una discrepancia de conciliación, no como una operación que pueda ignorarse.</caption>
</box>

---

## Fase 4 — Seguridad de resoluciones excepcionales

<text color="secondary" weight="medium" size="xs">Pregunta 139 — ¿Qué protección adicional debería exigirse al superadministrador para liberar manualmente un pago incierto?</text>

Esta operación es sensible porque puede permitir iniciar otro cobro sobre una cuota que anteriormente tuvo un resultado incierto.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Justificación y evidencia**

      El superadministrador podrá confirmar la resolución desde su sesión activa, con justificación y evidencia obligatorias, sin otra verificación.
    </radio>
    <radio value="b">
      **Opción B — Confirmación mediante contraseña**

      Además de la justificación y evidencia, el superadministrador deberá volver a ingresar su contraseña antes de confirmar la resolución.
    </radio>
    <radio value="c">
      **Opción C — Verificación reforzada mediante OTP (recomendada)**

      Además de la justificación y evidencia, el superadministrador deberá confirmar la operación con un código temporal enviado a su correo electrónico verificado.

      <text color="secondary" size="xs">El código tendrá vencimiento y límite de intentos. La resolución se ejecutará únicamente después de validar el OTP y se registrará en la auditoría, incluyendo la evidencia utilizada. No será necesaria la aprobación de otro usuario.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 139 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"exigir justificación y evidencia obligatorias sin verificación adicional",b:"exigir justificación, evidencia y confirmación de contraseña del superadministrador",c:"exigir justificación, evidencia y OTP enviado al correo verificado del superadministrador antes de liberar excepcionalmente la cuota"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-139

### Registro 347 — Confirmación simple de resoluciones excepcionales
Fuente: `9f7b9703-9126-48a7-9945-98edb0afff57`

Respuesta previa del usuario:

Para la pregunta 139 del relevamiento SDD elijo la opción A: exigir justificación y evidencia obligatorias sin verificación adicional.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 139 — Confirmada: opción A**
</row>

El superadministrador podrá resolver excepcionalmente un pago incierto **sin OTP ni reingreso de contraseña**, siempre que proporcione una justificación y evidencia verificable obligatorias.

Esto simplifica la operación administrativa sin eliminar los controles de permisos y auditoría.

## Registro SDD — Decisión 139

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-139 — Confirmación simple de resoluciones excepcionales**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1336:** Solo el superadministrador podrá confirmar la liberación excepcional de una cuota asociada a un pago incierto.
  - **RN-1337:** La operación podrá realizarse desde una sesión autenticada y vigente.
  - **RN-1338:** No se requerirá un código OTP adicional.
  - **RN-1339:** No se requerirá volver a ingresar la contraseña.
  - **RN-1340:** No será necesaria la aprobación de otro usuario.
  - **RN-1341:** La justificación de la resolución será obligatoria.
  - **RN-1342:** La evidencia verificable también será obligatoria.
  - **RN-1343:** El sistema impedirá confirmar una resolución si faltan la justificación o la evidencia.
  - **RN-1344:** Se verificarán los permisos del superadministrador antes de ejecutar la operación.
  - **RN-1345:** La resolución respetará los criterios de evidencia definidos en DEC-138.
  - **RN-1346:** No se permitirá liberar una cuota cuando exista confirmación oficial de pago exitoso.
  - **RN-1347:** La operación registrará usuario responsable, fecha, hora, motivo y evidencia asociada.
  - **RN-1348:** El historial original del pago permanecerá disponible para conciliación y auditoría.
  - **RN-1349:** La resolución no generará automáticamente un nuevo checkout.
  - **RN-1350:** Si posteriormente se recibe un resultado contradictorio, se registrará una discrepancia para revisión.
</box>

## Ejemplo de resolución

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    **Pago incierto · Obras Futsal**
    <badge color="warning">Revisión requerida</badge>
  </row>
  <box background="surface-secondary" padding={3} radius="md" gap={2}>
    <text color="secondary" size="xs">Ejemplo ilustrativo de los datos requeridos</text>
    **Justificación**

    <text size="sm">El proveedor confirmó mediante soporte que la operación no fue acreditada.</text>
    **Evidencia**

    <row align="center" gap={2}>
      <icon name="file-check" color="success"/>
      <text size="sm">Constancia verificable del proveedor</text>
    </row>
  </box>
  <row align="center" gap={2}>
    <icon name="check-circle" color="success"/>
    <text size="sm">El superadministrador puede confirmar directamente, sin OTP adicional.</text>
  </row>
</box>

---

## Fase 4 — Conservación de evidencias financieras

<text color="secondary" weight="medium" size="xs">Pregunta 140 — ¿Durante cuánto tiempo deberían conservarse las evidencias utilizadas para resolver pagos inciertos?</text>

Estas evidencias pueden incluir constancias del proveedor, comprobantes y documentación utilizada para justificar una resolución excepcional.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Conservación indefinida**

      Todas las evidencias se conservarán permanentemente junto con el historial financiero, sin eliminación automática.
    </radio>
    <radio value="b">
      **Opción B — Plazo fijo**

      Las evidencias se conservarán durante un período fijo definido por la plataforma, por ejemplo cinco años, y después se eliminarán automáticamente cuando corresponda.
    </radio>
    <radio value="c">
      **Opción C — Plazo global configurable con protección del historial (recomendada)**

      El superadministrador podrá configurar cuántos años se conservarán los archivos de evidencia financiera.

      <text color="secondary" size="xs">Al vencer el plazo, podrán eliminarse los archivos cuando la normativa aplicable lo permita, pero se mantendrán los registros de operaciones, resoluciones y auditoría conforme a sus políticas de conservación. Las evidencias vinculadas a disputas o investigaciones abiertas no se eliminarán mientras deban preservarse.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 140 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"conservar indefinidamente todos los archivos de evidencia financiera",b:"conservar evidencias por un plazo fijo definido por la plataforma",c:"permitir que el superadministrador configure globalmente los años de conservación de evidencias, protegiendo el historial y evitando eliminar archivos sujetos a disputas u obligaciones legales"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-140

### Registro 349 — Conservación configurable de evidencias financieras
Fuente: `5f48679c-b241-4f58-b1e0-c699abc7f0b5`

Respuesta previa del usuario:

Para la pregunta 140 del relevamiento SDD elijo la opción C: permitir que el superadministrador configure globalmente los años de conservación de evidencias, protegiendo el historial y evitando eliminar archivos sujetos a disputas u obligaciones legales.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 140 — Confirmada: opción C**
</row>

El superadministrador podrá definir **cuántos años se conservarán las evidencias financieras**. Una vez cumplido ese plazo, los archivos podrán eliminarse cuando corresponda legalmente, pero se preservarán los registros históricos y de auditoría según sus propias políticas.

## Registro SDD — Decisión 140

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-140 — Conservación configurable de evidencias financieras**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1351:** El superadministrador podrá configurar globalmente el período de conservación de archivos de evidencia financiera.
  - **RN-1352:** El período se expresará en años.
  - **RN-1353:** La configuración será común a todos los clubes y disciplinas.
  - **RN-1354:** Los administradores de clubes y disciplinas no podrán modificar esta política.
  - **RN-1355:** Cada evidencia conservará su fecha de incorporación y la operación financiera a la que pertenece.
  - **RN-1356:** El sistema identificará los archivos que hayan alcanzado el plazo configurado.
  - **RN-1357:** La eliminación automática solo procederá cuando no exista una obligación legal, contractual o de preservación que lo impida.
  - **RN-1358:** Las evidencias relacionadas con disputas, investigaciones o revisiones abiertas permanecerán protegidas.
  - **RN-1359:** Una evidencia protegida no se eliminará aunque haya superado el plazo general.
  - **RN-1360:** Una vez finalizada la causa de preservación, se volverá a evaluar si corresponde eliminarla.
  - **RN-1361:** La eliminación del archivo no eliminará el registro de la operación financiera.
  - **RN-1362:** Las resoluciones excepcionales y su historial de auditoría se conservarán conforme a sus políticas específicas.
  - **RN-1363:** Cuando se elimine una evidencia, se mantendrá un registro de su existencia anterior y de la eliminación efectuada.
  - **RN-1364:** Los cambios de configuración quedarán auditados con usuario, fecha y valores anteriores y nuevos.
  - **RN-1365:** El acceso a evidencias respetará los permisos y el aislamiento de cada disciplina.
  - **RN-1366:** La política deberá respetar los períodos mínimos de conservación exigidos por la normativa aplicable.
</box>

## Ejemplo de conservación

<box border radius="lg" padding={3} gap={2}>
  <text color="secondary" size="xs">Ejemplo ilustrativo: política configurada en 5 años</text>
  <table>
    <table-row>
      <table-cell>**Situación**</table-cell>
      <table-cell>**Resultado**</table-cell>
    </table-row>
    <table-row>
      <table-cell>Evidencia de hace 2 años</table-cell>
      <table-cell><badge>Conservar</badge></table-cell>
    </table-row>
    <table-row>
      <table-cell>Evidencia de hace 6 años, sin restricciones</table-cell>
      <table-cell><badge>Elegible para eliminación</badge></table-cell>
    </table-row>
    <table-row>
      <table-cell>Evidencia de hace 7 años, con disputa abierta</table-cell>
      <table-cell><badge color="warning">Protegida</badge></table-cell>
    </table-row>
  </table>
  <caption>Los cinco años son solo un ejemplo, no un plazo legal establecido para la plataforma.</caption>
</box>

---

## Fase 4 — Almacenamiento de evidencias financieras

<text color="secondary" weight="medium" size="xs">Pregunta 141 — ¿Cómo deberían almacenarse los archivos de evidencia financiera?</text>

Estos archivos pueden incluir comprobantes, constancias bancarias y documentos utilizados para resolver pagos inciertos. Algunos podrían contener datos personales o financieros.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Almacenamiento interno simple**

      Guardar los archivos en el almacenamiento del servidor de la aplicación, con acceso restringido a usuarios autorizados.
    </radio>
    <radio value="b">
      **Opción B — Almacenamiento externo privado**

      Guardar los archivos en un servicio de almacenamiento privado, con acceso autenticado y separación por disciplina.
    </radio>
    <radio value="c">
      **Opción C — Almacenamiento privado cifrado y acceso temporal (recomendada)**

      Utilizar almacenamiento privado de archivos, cifrado en reposo y separación lógica por club y disciplina.

      <text color="secondary" size="xs">Los documentos no tendrán enlaces públicos permanentes. Cada descarga requerirá autorización y utilizará un acceso temporal protegido. Se auditarán las consultas y descargas, y se aplicarán las políticas de conservación definidas en DEC-140.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 141 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"almacenar evidencias en el servidor de aplicación con acceso restringido",b:"almacenar evidencias en un servicio privado externo con autorización y separación por disciplina",c:"almacenar evidencias en almacenamiento privado cifrado, aislado por club y disciplina, con accesos temporales autorizados y auditoría de consultas y descargas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-141

### Registro 351 — Almacenamiento seguro de evidencias financieras
Fuente: `fa24fc2f-9b46-4f1f-8e16-f903d0c75579`

Respuesta previa del usuario:

Para la pregunta 141 del relevamiento SDD elijo la opción C: almacenar evidencias en almacenamiento privado cifrado, aislado por club y disciplina, con accesos temporales autorizados y auditoría de consultas y descargas.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 141 — Confirmada: opción C**
</row>

Las evidencias financieras se almacenarán de forma **privada, cifrada y aislada por club y disciplina**. Cada acceso requerirá autorización, tendrá una vigencia limitada y quedará registrado para auditoría.

## Registro SDD — Decisión 141

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-141 — Almacenamiento seguro de evidencias financieras**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1367:** Los archivos de evidencia financiera se almacenarán en un repositorio privado.
  - **RN-1368:** Los archivos deberán estar cifrados en reposo.
  - **RN-1369:** El almacenamiento tendrá aislamiento lógico por club y disciplina.
  - **RN-1370:** Ningún usuario podrá consultar evidencias de una disciplina sin autorización.
  - **RN-1371:** Los archivos no tendrán enlaces públicos permanentes.
  - **RN-1372:** Toda solicitud de consulta o descarga deberá validar la identidad del usuario.
  - **RN-1373:** Se verificarán los permisos vigentes sobre el club, la disciplina y la operación financiera.
  - **RN-1374:** Los accesos temporales tendrán un vencimiento limitado.
  - **RN-1375:** Un enlace temporal vencido no permitirá nuevas descargas.
  - **RN-1376:** La autorización deberá verificarse nuevamente al solicitar otro acceso.
  - **RN-1377:** Se auditarán las consultas de evidencias financieras.
  - **RN-1378:** Se auditarán las solicitudes y operaciones de descarga.
  - **RN-1379:** Los registros de auditoría identificarán usuario, fecha, hora, archivo y operación asociada.
  - **RN-1380:** Los documentos conservarán su asociación con el pago incierto y su resolución.
  - **RN-1381:** Los archivos estarán sujetos a las políticas de conservación definidas en DEC-140.
  - **RN-1382:** La eliminación de una evidencia no eliminará su historial de auditoría.
  - **RN-1383:** Las evidencias protegidas por disputas o investigaciones no podrán eliminarse mientras exista una obligación de preservación.
  - **RN-1384:** Los accesos deberán utilizar comunicaciones seguras.
</box>

## Ejemplo de acceso

<box border radius="lg" padding={3} gap={2}>
  <row align="center" gap={2}>
    <icon name="file-lock-2" color="secondary" size="xl"/>
    <box flex="1" gap={1}>
      **Constancia de pago — Obras Futsal**
      <text color="secondary" size="xs">Archivo privado asociado a una resolución financiera</text>
    </box>
    <badge color="success">Protegido</badge>
  </row>
  <divider color="subtle"/>
  <list marker="number" connector="solid" gap={2}>
    <list-item>Un administrador autorizado solicita consultar la evidencia.</list-item>
    <list-item>El sistema valida su identidad y permisos sobre Obras Futsal.</list-item>
    <list-item>Se habilita un acceso temporal al documento.</list-item>
    <list-item>La consulta o descarga queda registrada en la auditoría.</list-item>
  </list>
</box>

---

## Fase 4 — Integridad de evidencias financieras

<text color="secondary" weight="medium" size="xs">Pregunta 142 — ¿Se podrán reemplazar o modificar evidencias financieras después de cargarlas?</text>

Por ejemplo, un administrador adjunta una constancia bancaria equivocada a una operación y después necesita corregirla.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Edición libre**

      Los administradores autorizados podrán reemplazar o eliminar evidencias mientras tengan permisos sobre la operación.
    </radio>
    <radio value="b">
      **Opción B — Evidencias completamente inmutables**

      Una vez cargada una evidencia, no podrá reemplazarse ni corregirse. Solo podrán agregarse nuevos archivos.
    </radio>
    <radio value="c">
      **Opción C — Evidencias inmutables con correcciones versionadas (recomendada)**

      Los archivos originales no podrán sobrescribirse. Si se carga una evidencia equivocada, un usuario autorizado podrá incorporar una versión corregida y registrar el motivo.

      <text color="secondary" size="xs">La versión anterior quedará identificada como reemplazada, pero se conservará en el historial mientras corresponda según las políticas de retención y preservación. Cada versión tendrá su propia fecha, responsable y registro de auditoría. Una corrección no modificará automáticamente una resolución financiera ya confirmada.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 142 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir que administradores autorizados reemplacen o eliminen libremente evidencias",b:"mantener todas las evidencias completamente inmutables y permitir solo adjuntar nuevos archivos",c:"mantener originales inmutables y permitir correcciones versionadas con motivo, responsable, auditoría y conservación histórica sin alterar automáticamente resoluciones financieras"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-142

### Registro 355 — Versionado e integridad de evidencias financieras
Fuente: `453201bd-fe48-4d1c-94da-5028f0ba8ca5`

Respuesta previa del usuario:

Para la pregunta 142 del relevamiento SDD elijo la opción C: mantener originales inmutables y permitir correcciones versionadas con motivo, responsable, auditoría y conservación histórica sin alterar automáticamente resoluciones financieras.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 142 — Confirmada: opción C**
</row>

Las evidencias financieras serán **inmutables, pero admitirán correcciones mediante versiones**. Ningún archivo original podrá sobrescribirse y toda corrección quedará registrada sin modificar automáticamente las resoluciones financieras existentes.

## Registro SDD — Decisión 142

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-142 — Versionado e integridad de evidencias financieras**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1385:** Toda evidencia financiera cargada será inmutable.
  - **RN-1386:** No se permitirá sobrescribir el contenido de un archivo existente.
  - **RN-1387:** Los usuarios autorizados podrán incorporar versiones corregidas.
  - **RN-1388:** Cada corrección requerirá un motivo obligatorio.
  - **RN-1389:** Cada versión identificará al usuario responsable.
  - **RN-1390:** Se registrarán la fecha y hora de incorporación.
  - **RN-1391:** Cada versión tendrá un identificador único.
  - **RN-1392:** La nueva versión conservará su relación con la evidencia original.
  - **RN-1393:** Las versiones anteriores se identificarán como reemplazadas.
  - **RN-1394:** Las versiones reemplazadas permanecerán consultables para usuarios autorizados mientras deban conservarse.
  - **RN-1395:** Las políticas de conservación de DEC-140 se aplicarán a las distintas versiones.
  - **RN-1396:** Las versiones sujetas a disputas o investigaciones permanecerán protegidas.
  - **RN-1397:** Se auditarán todas las incorporaciones y correcciones.
  - **RN-1398:** Las correcciones no modificarán automáticamente una resolución financiera confirmada.
  - **RN-1399:** Si una corrección contradice la evidencia utilizada para resolver una operación, deberá señalarse para revisión.
  - **RN-1400:** La consulta y descarga de versiones respetará los controles de acceso de DEC-141.
</box>

## Ejemplo — Corrección de comprobante

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Evidencia de pago · Obras Futsal**
    <badge color="success">Versionada</badge>
  </row>
  <list marker="number" connector="solid" gap={3}>
    <list-item>
      <row align="center" justify="between">
        **Versión 1 — Comprobante original**
        <badge>Reemplazada</badge>
      </row>
      <text color="secondary" size="sm">Archivo cargado inicialmente con información incorrecta. Se conserva para auditoría.</text>
    </list-item>
    <list-item>
      <row align="center" justify="between">
        **Versión 2 — Comprobante corregido**
        <badge color="success">Vigente</badge>
      </row>
      <text color="secondary" size="sm">Nueva evidencia, motivo de corrección y responsable registrados.</text>
    </list-item>
  </list>
  <caption>La corrección del documento no cambia por sí sola el estado del pago.</caption>
</box>

---

## Fase 4 — Devoluciones y contracargos

<text color="secondary" size="sm">Para mantener el objetivo de cerrar el relevamiento cerca de la pregunta 170, pasamos ahora a las decisiones financieras más importantes que todavía faltan.</text>

<text color="secondary" weight="medium" size="xs">Pregunta 143 — ¿Quién podrá gestionar devoluciones de pagos online?</text>

Por ejemplo, un responsable paga una cuota por error y solicita que le devuelvan el dinero.

Recordemos que cada disciplina tiene su propia cuenta en la pasarela y que la plataforma cobra una comisión del 5% mediante Split Payment.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Administradores de disciplina**

      Los administradores de cada disciplina podrán solicitar y ejecutar devoluciones de sus propios pagos directamente desde la plataforma, utilizando las funciones disponibles en la pasarela.
    </radio>
    <radio value="b">
      **Opción B — Solo superadministrador**

      Toda devolución deberá gestionarla el superadministrador, aunque el dinero corresponda a una disciplina específica.
    </radio>
    <radio value="c">
      **Opción C — Gestión por disciplina con supervisión global (recomendada)**

      Los administradores de disciplina podrán iniciar y gestionar devoluciones de sus propios pagos, siempre que la pasarela y los permisos de la cuenta lo permitan.

      <text color="secondary" size="xs">El superadministrador podrá supervisar todas las devoluciones y actuar cuando tenga autorización suficiente. Se registrarán motivos, importes, estados y resultados. La devolución de la comisión del 5% y de los costos de la pasarela dependerá de las capacidades y condiciones reales del proveedor; no se asumirá que el Split Payment se revierte automáticamente.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 143 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir que administradores de disciplina gestionen directamente devoluciones de sus pagos mediante la pasarela",b:"reservar todas las devoluciones al superadministrador",c:"permitir gestión de devoluciones por administradores de disciplina, con supervisión global del superadministrador, auditoría y tratamiento de comisiones según las capacidades reales de la pasarela"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-143

### Registro 357 — Gestión descentralizada de devoluciones online
Fuente: `8db4a8ec-287c-4c5a-990a-5ecceee26d51`

Respuesta previa del usuario:

Para la pregunta 143 del relevamiento SDD elijo la opción C: permitir gestión de devoluciones por administradores de disciplina, con supervisión global del superadministrador, auditoría y tratamiento de comisiones según las capacidades reales de la pasarela.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 143 — Confirmada: opción C**
</row>

Los administradores de cada disciplina podrán gestionar devoluciones de sus propios pagos, mientras que el superadministrador tendrá supervisión global.

Las devoluciones se realizarán mediante las capacidades reales de la pasarela, respetando el funcionamiento del **Split Payment y la comisión del 5%**.

## Registro SDD — Decisión 143

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-143 — Gestión descentralizada de devoluciones online**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1401:** Los administradores de disciplina podrán iniciar solicitudes de devolución de pagos online correspondientes a su disciplina.
  - **RN-1402:** La ejecución de devoluciones dependerá de las capacidades y permisos otorgados por la pasarela.
  - **RN-1403:** Un administrador no podrá gestionar devoluciones de otra disciplina.
  - **RN-1404:** El superadministrador podrá consultar y supervisar las devoluciones de todos los clubes y disciplinas.
  - **RN-1405:** El superadministrador podrá intervenir operativamente cuando disponga de autorización suficiente.
  - **RN-1406:** Cada devolución estará vinculada al pago original.
  - **RN-1407:** Toda solicitud de devolución deberá registrar un motivo.
  - **RN-1408:** Se registrarán el importe solicitado y el importe efectivamente devuelto.
  - **RN-1409:** Se conservarán los estados informados por la pasarela.
  - **RN-1410:** Una solicitud enviada no se considerará completada hasta obtener confirmación suficiente del proveedor.
  - **RN-1411:** Las operaciones deberán ser idempotentes para evitar devoluciones duplicadas.
  - **RN-1412:** El tratamiento de la comisión del 5% dependerá de las capacidades y condiciones contractuales del proveedor.
  - **RN-1413:** Los costos de procesamiento de la pasarela se tratarán conforme a sus condiciones reales.
  - **RN-1414:** No se asumirá que una devolución revierte automáticamente todas las partes del Split Payment.
  - **RN-1415:** Los importes de la disciplina, la plataforma y el pagador deberán identificarse separadamente cuando el proveedor suministre esa información.
  - **RN-1416:** Las devoluciones deberán reflejarse en los reportes financieros y la conciliación.
  - **RN-1417:** Los eventos tardíos y repetidos del proveedor no podrán duplicar devoluciones.
  - **RN-1418:** Se auditarán el solicitante, fecha, motivo, importe, proveedor y resultado.
  - **RN-1419:** El historial del pago original permanecerá disponible.
  - **RN-1420:** Los errores o rechazos de devolución deberán quedar registrados para seguimiento.
</box>

## Ejemplo — Devolución de una cuota

<box border radius="lg" padding={3} gap={2}>
  <text color="secondary" size="xs">Ejemplo ilustrativo: cuota de $20.000, comisión de plataforma del 5% y costo hipotético de pasarela de $800.</text>
  <table>
    <table-row><table-cell>Cuota de Obras Futsal</table-cell><table-cell align="end">$20.000</table-cell></table-row>
    <table-row><table-cell>Comisión plataforma (5%)</table-cell><table-cell align="end">$1.000</table-cell></table-row>
    <table-row><table-cell>Costo pasarela (ejemplo)</table-cell><table-cell align="end">$800</table-cell></table-row>
    <table-row><table-cell>**Total pagado**</table-cell><table-cell align="end">**$21.800**</table-cell></table-row>
  </table>
  Si se solicita una devolución, el sistema deberá consultar qué componentes pueden revertirse realmente. **No se dará por hecho que los $21.800 se devuelven íntegramente.**
</box>

---

## Fase 4 — Alcance de las devoluciones

<text color="secondary" weight="medium" size="xs">Pregunta 144 — ¿Se permitirán devoluciones parciales o solamente totales?</text>

Por ejemplo, un responsable pagó tres cuotas de $20.000 en un único checkout de Futsal, pero necesita que le devuelvan solo una.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo devolución total**

      Si se devuelve un pago, deberá devolverse el checkout completo. No se podrán devolver cuotas individuales de un pago agrupado.
    </radio>
    <radio value="b">
      **Opción B — Devoluciones parciales libres**

      Los administradores podrán devolver cualquier importe hasta el total disponible del pago, aunque no corresponda exactamente a una cuota.
    </radio>
    <radio value="c">
      **Opción C — Devolución total o por cuotas completas (recomendada)**

      Se permitirá devolver el pago completo o seleccionar una o varias cuotas completas incluidas en el checkout.

      <text color="secondary" size="xs">No se permitirán devoluciones arbitrarias de una fracción de cuota. La operación estará sujeta a que la pasarela soporte devoluciones parciales y a que los componentes del Split Payment puedan tratarse correctamente. Se evitará devolver más de lo efectivamente cobrado o duplicar devoluciones.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 144 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir únicamente devoluciones totales del checkout",b:"permitir devoluciones parciales de cualquier importe hasta el saldo disponible",c:"permitir devoluciones totales o de cuotas completas individuales dentro de un checkout, condicionadas al soporte real de la pasarela y del Split Payment"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-144

### Registro 360 — Devoluciones totales y parciales por cuota
Fuente: `1fda8a25-ac49-4a99-886c-636e7021d603`

Respuesta previa del usuario:

Para la pregunta 144 del relevamiento SDD elijo la opción C: permitir devoluciones totales o de cuotas completas individuales dentro de un checkout, condicionadas al soporte real de la pasarela y del Split Payment.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 144 — Confirmada: opción C**
</row>

La plataforma permitirá **devoluciones totales o de cuotas completas individuales** dentro de un mismo checkout, siempre que la pasarela y su mecanismo de Split Payment lo permitan.

## Registro SDD — Decisión 144

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-144 — Devoluciones totales y parciales por cuota**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1421:** Se permitirán devoluciones totales de un checkout.
  - **RN-1422:** Se podrán seleccionar una o varias cuotas completas para devolver dentro del mismo checkout.
  - **RN-1423:** No se permitirán devoluciones de fracciones arbitrarias de una cuota.
  - **RN-1424:** Cada devolución estará vinculada al checkout original.
  - **RN-1425:** El sistema identificará individualmente las cuotas incluidas en la devolución.
  - **RN-1426:** Las devoluciones parciales dependerán del soporte real de la pasarela.
  - **RN-1427:** También se verificará la compatibilidad con el mecanismo de Split Payment.
  - **RN-1428:** No se permitirá solicitar una devolución superior al importe disponible para devolver.
  - **RN-1429:** Una cuota completamente devuelta no podrá devolverse nuevamente.
  - **RN-1430:** Se impedirán solicitudes simultáneas que puedan duplicar devoluciones.
  - **RN-1431:** Las devoluciones pendientes mantendrán reservado su importe hasta obtener un resultado definitivo.
  - **RN-1432:** Se registrarán los importes correspondientes a cuota, comisión de plataforma y costos de pasarela, según la información disponible.
  - **RN-1433:** No se asumirá que la comisión del 5% se devuelve automáticamente.
  - **RN-1434:** Cada devolución tendrá su propio identificador, estado e historial.
  - **RN-1435:** Los resultados se actualizarán mediante confirmaciones verificables de la pasarela.
  - **RN-1436:** Las devoluciones se reflejarán en la conciliación y los reportes financieros.
  - **RN-1437:** Todas las acciones quedarán auditadas.
  - **RN-1438:** Se mantendrá el aislamiento financiero entre disciplinas.
</box>

## Ejemplo — Checkout con tres cuotas

<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Pago agrupado · Obras Futsal**
    <text weight="medium" tabularNums>$60.000</text>
  </row>
  <caption>Importes nominales, sin incluir comisiones ni costos de procesamiento.</caption>
  <table>
    <table-row>
      <table-cell>Cuota de jugador A</table-cell>
      <table-cell align="end">$20.000</table-cell>
      <table-cell><badge color="success">Conservar</badge></table-cell>
    </table-row>
    <table-row>
      <table-cell>Cuota de jugador B</table-cell>
      <table-cell align="end">$20.000</table-cell>
      <table-cell><badge color="warning">Devolver</badge></table-cell>
    </table-row>
    <table-row>
      <table-cell>Cuota de jugador C</table-cell>
      <table-cell align="end">$20.000</table-cell>
      <table-cell><badge color="success">Conservar</badge></table-cell>
    </table-row>
  </table>
  **Resultado:** se solicita devolver únicamente la cuota B. Las otras dos permanecen pagadas. El importe efectivo de devolución dependerá de las reglas de la pasarela.
</box>

---

## Fase 4 — Estado de las cuotas devueltas

<text color="secondary" weight="medium" size="xs">Pregunta 145 — ¿Qué debería ocurrir con una cuota cuando su pago es devuelto?</text>

Por ejemplo, un responsable paga noviembre, pero posteriormente se devuelve ese pago. Debemos decidir si la cuota vuelve a quedar pendiente o si se considera cancelada administrativamente.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Siempre vuelve a pendiente**

      Cuando se confirma una devolución, la cuota vuelve automáticamente a estar pendiente de pago.
    </radio>
    <radio value="b">
      **Opción B — Siempre queda cancelada**

      Una cuota devuelta se considera cancelada administrativamente y no vuelve a exigirse su pago.
    </radio>
    <radio value="c">
      **Opción C — Según el motivo de devolución (recomendada)**

      Al solicitar una devolución, el administrador deberá indicar si la cuota seguirá siendo exigible o si corresponde anularla.

      <text color="secondary" size="xs">Si sigue siendo exigible, volverá a pendiente únicamente cuando se confirme la devolución y se descarte otro cobro vigente. Si se anula, quedará cerrada sin deuda. Ambas situaciones conservarán el historial del pago, la devolución y la decisión administrativa.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 145 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"toda cuota devuelta vuelve automáticamente a pendiente",b:"toda cuota devuelta queda cancelada administrativamente",c:"el administrador decide justificadamente si la cuota devuelta vuelve a pendiente o queda anulada, con efecto tras confirmación y trazabilidad completa"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 144 decisiones aprobadas. Aproximadamente 26 preguntas hasta el objetivo propuesto de 170.</caption>
```

## DEC-145

### Registro 362 — Estado de cuotas después de una devolución
Fuente: `8788f831-5fca-4c98-b42b-150ebdf072fd`

Respuesta previa del usuario:

Para la pregunta 145 del relevamiento SDD elijo la opción C: el administrador decide justificadamente si la cuota devuelta vuelve a pendiente o queda anulada, con efecto tras confirmación y trazabilidad completa.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 145 — Confirmada: opción C**
</row>

Cuando se devuelva un pago, el administrador de la disciplina decidirá, con una justificación obligatoria, si la cuota **vuelve a estar pendiente o queda anulada definitivamente**.

El cambio de estado se aplicará únicamente después de confirmar la devolución.

## Registro SDD — Decisión 145

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-145 — Estado de cuotas después de una devolución**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1439:** Al solicitar una devolución, el administrador deberá definir el destino de cada cuota afectada.
  - **RN-1440:** Los destinos posibles serán volver a pendiente o quedar anulada.
  - **RN-1441:** La decisión requerirá una justificación obligatoria.
  - **RN-1442:** La decisión podrá establecerse individualmente para cada cuota incluida en una devolución.
  - **RN-1443:** Mientras la devolución esté pendiente, no se aplicará el cambio definitivo de estado.
  - **RN-1444:** El cambio se aplicará cuando la pasarela confirme efectivamente la devolución.
  - **RN-1445:** Antes de volver a habilitar el cobro, se comprobará que no existan otros pagos o checkouts incompatibles vigentes.
  - **RN-1446:** Una cuota devuelta que siga siendo exigible volverá a estado pendiente.
  - **RN-1447:** Una cuota devuelta y anulada quedará cerrada sin deuda exigible.
  - **RN-1448:** Una cuota anulada no podrá incluirse en nuevos checkouts.
  - **RN-1449:** Si la devolución falla, se conservará el estado financiero correspondiente al pago original.
  - **RN-1450:** La plataforma conservará la relación entre cuota, pago original y devolución.
  - **RN-1451:** Se registrarán el administrador responsable, la fecha y el motivo de la decisión.
  - **RN-1452:** Las devoluciones y anulaciones se reflejarán en los reportes financieros.
  - **RN-1453:** Los eventos repetidos de confirmación no producirán cambios duplicados.
  - **RN-1454:** Toda la operación quedará auditada y aislada dentro de su disciplina.
</box>

## Ejemplo — Dos resultados posibles

<grid columns={2} gap={3}>
  <grid-item>
    <box border radius="lg" padding={3} gap={2}>
      <icon name="rotate-ccw" color="secondary" size="lg"/>
      **Devolución con deuda vigente**
      <badge color="warning">Pendiente</badge>
      <text size="sm">Se devuelve el pago, pero el jugador todavía debe abonar la cuota.</text>
    </box>
  </grid-item>
  <grid-item>
    <box border radius="lg" padding={3} gap={2}>
      <icon name="circle-check" color="success" size="lg"/>
      **Devolución con anulación**
      <badge color="success">Anulada</badge>
      <text size="sm">Se devuelve el pago y la cuota deja de ser exigible.</text>
    </box>
  </grid-item>
</grid>

---

## Fase 4 — Contracargos y desconocimientos de pago

<text color="secondary" weight="medium" size="xs">Pregunta 146 — ¿Cómo debería actuar la plataforma cuando un responsable desconoce un pago ante su banco?</text>

Un *contracargo* ocurre cuando el pagador disputa una operación y el banco o la pasarela puede retener o revertir el dinero.

A diferencia de una devolución voluntaria, el administrador no controla necesariamente el resultado.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Volver inmediatamente a pendiente**

      Apenas se recibe un aviso de contracargo, la cuota vuelve a estar pendiente y se habilita un nuevo pago.
    </radio>
    <radio value="b">
      **Opción B — Mantener la cuota pagada**

      Los contracargos se registran como incidencias financieras, pero no modifican el estado de las cuotas.
    </radio>
    <radio value="c">
      **Opción C — Gestión según el estado real del contracargo (recomendada)**

      Cuando se inicia una disputa, la cuota queda identificada como disputada, sin habilitar inmediatamente otro cobro.

      <text color="secondary" size="xs">Si el contracargo se revierte a favor de la disciplina, se conserva el pago. Si se confirma la pérdida del dinero, la cuota vuelve a pendiente, salvo que exista una anulación administrativa justificada. El sistema registra las retenciones, comisiones, resultados y alertas correspondientes, siempre según los datos reales de la pasarela.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 146 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"volver inmediatamente a pendiente la cuota al recibir aviso de contracargo",b:"registrar contracargos sin modificar el estado de las cuotas",c:"gestionar contracargos según su estado real, marcar cuotas disputadas sin habilitar cobro duplicado y resolverlas al confirmarse el resultado, con trazabilidad y conciliación"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance del relevamiento: 145 decisiones aprobadas. Aproximadamente 25 preguntas restantes hasta el objetivo propuesto.</caption>
```

## DEC-146

### Registro 364 — Gestión de contracargos y desconocimientos de pago
Fuente: `66343511-3abb-404e-a08a-05109349d29a`

Respuesta previa del usuario:

Para la pregunta 146 del relevamiento SDD elijo la opción C: gestionar contracargos según su estado real, marcar cuotas disputadas sin habilitar cobro duplicado y resolverlas al confirmarse el resultado, con trazabilidad y conciliación.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 146 — Confirmada: opción C**
</row>

Los contracargos se gestionarán según su estado real en la pasarela. Mientras exista una disputa, la cuota permanecerá protegida contra nuevos cobros. Una vez resuelta, se actualizará su estado según el resultado definitivo.

## Registro SDD — Decisión 146

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-146 — Gestión de contracargos y desconocimientos de pago**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1455:** Cada contracargo estará vinculado a su pago original.
  - **RN-1456:** Los contracargos se identificarán mediante los datos proporcionados por la pasarela.
  - **RN-1457:** Cuando se inicie una disputa, las cuotas afectadas se identificarán como disputadas.
  - **RN-1458:** Una disputa abierta no habilitará automáticamente un nuevo cobro.
  - **RN-1459:** Las cuotas disputadas permanecerán protegidas contra checkouts incompatibles.
  - **RN-1460:** Se registrarán los estados y cambios informados por el proveedor.
  - **RN-1461:** Se registrarán las retenciones de fondos cuando la pasarela proporcione esa información.
  - **RN-1462:** Se identificarán los costos o comisiones asociados al contracargo.
  - **RN-1463:** Si la disputa se resuelve favorablemente y el pago permanece válido, la cuota conservará su condición de pagada.
  - **RN-1464:** Si se confirma definitivamente la pérdida del pago, la cuota volverá a pendiente, salvo anulación administrativa justificada.
  - **RN-1465:** No se habilitará otro cobro mientras exista incertidumbre sobre el resultado financiero.
  - **RN-1466:** Los eventos repetidos o tardíos se procesarán de forma idempotente.
  - **RN-1467:** Los contracargos se incorporarán a la conciliación financiera.
  - **RN-1468:** Los reportes reflejarán los importes disputados, retenidos y definitivamente revertidos, según la información disponible.
  - **RN-1469:** Se notificará a los administradores de la disciplina afectada.
  - **RN-1470:** El superadministrador podrá supervisar los contracargos globalmente.
  - **RN-1471:** Se conservará el historial del pago original y de la disputa.
  - **RN-1472:** Las resoluciones quedarán auditadas y aisladas por disciplina.
</box>

## Ejemplo — Cuota desconocida ante el banco

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    **Cuota de Futsal · $20.000**
    <badge color="warning">En disputa</badge>
  </row>
  <text color="secondary" size="sm">El responsable desconoce el pago y el banco inicia un contracargo.</text>
  <divider color="subtle"/>
  <grid columns={2} gap={2}>
    <grid-item>
      <box gap={1}>
        <icon name="check-circle-2" color="success"/>
        **Disputa ganada**
        <text size="sm">El pago se mantiene y la cuota continúa pagada.</text>
      </box>
    </grid-item>
    <grid-item>
      <box gap={1}>
        <icon name="alert-circle" color="danger"/>
        **Disputa perdida**
        <text size="sm">La cuota vuelve a pendiente, salvo anulación justificada.</text>
      </box>
    </grid-item>
  </grid>
</box>

---

## Fase 4 — Defensa de contracargos

<text color="secondary" weight="medium" size="xs">Pregunta 147 — ¿Quién debería gestionar la presentación de pruebas para defender un contracargo?</text>

Algunas pasarelas permiten presentar documentación para demostrar que un pago fue legítimo, dentro de un plazo determinado.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo superadministrador**

      Todas las defensas de contracargos serán gestionadas por el superadministrador, independientemente de la disciplina.
    </radio>
    <radio value="b">
      **Opción B — Solo administradores de disciplina**

      Cada disciplina gestionará exclusivamente sus contracargos, sin intervención operativa del superadministrador.
    </radio>
    <radio value="c">
      **Opción C — Gestión por disciplina y supervisión global (recomendada)**

      Los administradores de disciplina podrán consultar los contracargos, adjuntar documentación y presentar defensas cuando la pasarela y sus permisos lo permitan.

      <text color="secondary" size="xs">El superadministrador podrá supervisar todas las disputas e intervenir cuando esté autorizado. Se registrarán los plazos, evidencias, presentaciones y resultados. El sistema alertará sobre vencimientos próximos, pero no prometerá resultados ni presentará pruebas automáticamente sin autorización.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 147 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"reservar al superadministrador la defensa de todos los contracargos",b:"permitir exclusivamente a administradores de disciplina defender sus contracargos",c:"permitir a administradores de disciplina gestionar defensas con evidencia y plazos, y al superadministrador supervisar e intervenir cuando esté autorizado"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 146 decisiones aprobadas. Aproximadamente 24 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-147

### Registro 366 — Gestión de defensas ante contracargos
Fuente: `c9d63d22-db41-44b7-8dd9-486dfef8ba1e`

Respuesta previa del usuario:

Para la pregunta 147 del relevamiento SDD elijo la opción C: permitir a administradores de disciplina gestionar defensas con evidencia y plazos, y al superadministrador supervisar e intervenir cuando esté autorizado.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 147 — Confirmada: opción C**
</row>

Los administradores de cada disciplina podrán gestionar la defensa de sus contracargos, presentar documentación y responder dentro de los plazos establecidos por la pasarela.

El superadministrador tendrá supervisión global y podrá intervenir cuando disponga de los permisos necesarios.

## Registro SDD — Decisión 147

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-147 — Gestión de defensas ante contracargos**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1473:** Los administradores de disciplina podrán consultar los contracargos correspondientes a su disciplina.
  - **RN-1474:** Podrán incorporar documentación y evidencias para defender operaciones disputadas.
  - **RN-1475:** La presentación de defensas mediante la plataforma dependerá de las capacidades de la pasarela.
  - **RN-1476:** Se verificarán los permisos de la cuenta conectada antes de enviar una defensa.
  - **RN-1477:** Los administradores no podrán gestionar disputas de otras disciplinas.
  - **RN-1478:** El superadministrador podrá supervisar todas las disputas de la plataforma.
  - **RN-1479:** Podrá intervenir operativamente únicamente cuando tenga autorización suficiente.
  - **RN-1480:** Cada disputa conservará la fecha límite de presentación informada por el proveedor.
  - **RN-1481:** Se identificarán las disputas próximas a vencer.
  - **RN-1482:** Se generarán alertas internas sobre vencimientos próximos.
  - **RN-1483:** Las evidencias utilizadas deberán respetar las políticas de seguridad y versionado aprobadas.
  - **RN-1484:** La presentación de una defensa requerirá una acción autorizada.
  - **RN-1485:** El sistema no enviará automáticamente documentación sin autorización.
  - **RN-1486:** Se registrarán los intentos de presentación y las respuestas del proveedor.
  - **RN-1487:** Se conservarán los estados de la disputa y su resultado definitivo.
  - **RN-1488:** No se considerará ganada una disputa por el solo hecho de presentar evidencia.
  - **RN-1489:** Los resultados se incorporarán a la conciliación financiera.
  - **RN-1490:** Se auditarán las acciones, responsables, fechas y documentos asociados.
</box>

## Ejemplo — Defensa de un contracargo

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Disputa · Obras Futsal**
    <badge color="warning">Defensa pendiente</badge>
  </row>
  <text color="secondary" size="xs">Ejemplo ilustrativo: el proveedor informa que quedan 5 días para presentar documentación.</text>
  <list marker="number" connector="solid" gap={2}>
    <list-item>El administrador recibe una alerta de contracargo.</list-item>
    <list-item>Consulta la operación y adjunta las evidencias correspondientes.</list-item>
    <list-item>Presenta la defensa mediante la pasarela, si esta funcionalidad está disponible.</list-item>
    <list-item>El sistema registra la presentación y espera el resultado oficial.</list-item>
  </list>
</box>

---

## Fase 4 — Alertas de vencimiento de contracargos

<text color="secondary" weight="medium" size="xs">Pregunta 148 — ¿Cómo deberían configurarse los avisos de vencimiento para presentar defensas de contracargos?</text>

El objetivo es que los administradores no pierdan el plazo para defender una operación.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Aviso fijo**

      El sistema notificará automáticamente cuando falten 24 horas para el vencimiento, sin configuración adicional.
    </radio>
    <radio value="b">
      **Opción B — Configuración por disciplina**

      Cada administrador de disciplina podrá elegir con cuánta anticipación recibir las alertas de sus contracargos.
    </radio>
    <radio value="c">
      **Opción C — Configuración global con múltiples avisos (recomendada)**

      El superadministrador podrá configurar globalmente varios momentos de aviso, por ejemplo 7 días, 3 días y 24 horas antes del vencimiento.

      <text color="secondary" size="xs">Los avisos se enviarán a los administradores de la disciplina afectada y serán visibles para el superadministrador. Se respetarán los plazos reales de cada proveedor, se evitarán notificaciones duplicadas y las alertas se cerrarán cuando la disputa deje de requerir presentación.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 148 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"enviar un único aviso fijo 24 horas antes del vencimiento de defensa",b:"permitir que cada disciplina configure sus avisos de vencimiento de contracargos",c:"permitir que el superadministrador configure globalmente múltiples avisos anticipados de vencimiento, respetando plazos reales y aislamiento por disciplina"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 147 decisiones aprobadas. Aproximadamente 23 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-148

### Registro 368 — Aviso fijo de vencimiento de contracargos
Fuente: `451dc9b8-e436-4e0e-81e0-65702407a721`

Respuesta previa del usuario:

Para la pregunta 148 del relevamiento SDD elijo la opción A: enviar un único aviso fijo 24 horas antes del vencimiento de defensa.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 148 — Confirmada: opción A**
</row>

La plataforma enviará **un único aviso automático 24 horas antes del vencimiento** del plazo para presentar una defensa de contracargo.

No será necesario configurar distintos plazos por pasarela o disciplina.

## Registro SDD — Decisión 148

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-148 — Aviso fijo de vencimiento de contracargos**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1491:** Cada disputa conservará la fecha y hora límite de defensa informada por la pasarela.
  - **RN-1492:** Se programará un aviso automático 24 horas antes de ese vencimiento.
  - **RN-1493:** El plazo de anticipación será fijo para toda la plataforma.
  - **RN-1494:** Los administradores de disciplina no podrán modificar la anticipación.
  - **RN-1495:** El superadministrador tampoco necesitará configurar este parámetro.
  - **RN-1496:** El aviso se dirigirá a los administradores autorizados de la disciplina afectada.
  - **RN-1497:** El superadministrador conservará visibilidad global de las disputas próximas a vencer.
  - **RN-1498:** Se generará un único aviso por disputa y vencimiento para cada destinatario.
  - **RN-1499:** No se emitirán avisos de presentación pendiente cuando la defensa ya haya sido presentada correctamente.
  - **RN-1500:** Tampoco se emitirán si la disputa fue cerrada o dejó de requerir defensa.
  - **RN-1501:** Si la pasarela informa una disputa cuando faltan menos de 24 horas, se generará inmediatamente el aviso correspondiente.
  - **RN-1502:** Si cambia oficialmente la fecha límite, se actualizará la programación sin duplicar avisos innecesarios.
  - **RN-1503:** El aviso identificará la disputa, la operación y su fecha límite.
  - **RN-1504:** Las notificaciones respetarán el aislamiento entre disciplinas.
  - **RN-1505:** Se conservará la trazabilidad de la generación y entrega de los avisos.
</box>

## Ejemplo

<box border radius="lg" padding={3} gap={2}>
  <row align="center" gap={2}>
    <icon name="calendar-clock" color="secondary" size="lg"/>
    <box flex="1" gap={1}>
      **Vencimiento de defensa**
      <text color="secondary" size="sm">Viernes 27 de noviembre · 18:00</text>
    </box>
  </row>
  <divider color="subtle"/>
  <row align="center" gap={2}>
    <icon name="bell-ring" color="warning"/>
    <box flex="1" gap={1}>
      **Aviso automático**
      <text size="sm">Jueves 26 de noviembre · 18:00</text>
    </box>
    <badge>24 h antes</badge>
  </row>
</box>

---

## Fase 4 — Devoluciones y comisiones de la plataforma

<text color="secondary" weight="medium" size="xs">Pregunta 149 — ¿Qué debería ocurrir con la comisión del 5% de la plataforma cuando se devuelve una cuota?</text>

Recordemos que el responsable paga el importe de la cuota más el 5% de comisión de la plataforma y los costos de procesamiento de la pasarela.

Si se devuelve una cuota de $20.000, debemos definir cómo se trata la comisión de $1.000 que cobró la plataforma.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — La plataforma conserva siempre el 5%**

      Cuando se devuelve una cuota, se devuelve el importe nominal correspondiente a la disciplina, pero la plataforma conserva su comisión.
    </radio>
    <radio value="b">
      **Opción B — La plataforma devuelve siempre el 5%**

      Cada cuota devuelta implica también devolver íntegramente la comisión del 5% correspondiente, incluso si se requiere una operación separada.
    </radio>
    <radio value="c">
      **Opción C — Devolver el 5% cuando sea técnicamente posible (recomendada)**

      La política comercial será devolver al pagador la comisión del 5% correspondiente a cada cuota efectivamente devuelta.

      <text color="secondary" size="xs">La plataforma deberá verificar que la pasarela permita revertir correctamente su parte del Split Payment. Si no es posible realizar esa reversión, no se ejecutará una devolución automática que produzca importes incorrectos: se registrará la limitación y se requerirá una resolución operativa controlada. Los costos propios de la pasarela seguirán sus condiciones contractuales.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 149 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"la plataforma conserva siempre su comisión del 5% cuando se devuelve una cuota",b:"la plataforma debe devolver siempre el 5% correspondiente incluso mediante una operación separada",c:"establecer como política devolver el 5% de las cuotas devueltas, ejecutándolo solo con reversión técnicamente correcta del Split Payment y resolución controlada si no es posible"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 148 decisiones aprobadas. Aproximadamente 22 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-149

### Registro 371 — Reversión de la comisión de plataforma en devoluciones
Fuente: `7d34b284-977e-4d18-bae8-93e4d7ec99c6`

Respuesta previa del usuario:

Para la pregunta 149 del relevamiento SDD elijo la opción C: establecer como política devolver el 5% de las cuotas devueltas, ejecutándolo solo con reversión técnicamente correcta del Split Payment y resolución controlada si no es posible.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 149 — Confirmada: opción C**
</row>

Queda establecida la política de que **la plataforma devolverá su comisión del 5% cuando se devuelva una cuota**.

La devolución deberá realizarse correctamente mediante la pasarela y su mecanismo de Split Payment. Si existe una limitación técnica, se requerirá una resolución operativa controlada.

## Registro SDD — Decisión 149

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-149 — Reversión de la comisión de plataforma en devoluciones**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1506:** La política comercial será devolver al pagador la comisión de plataforma asociada a cada cuota efectivamente devuelta.
  - **RN-1507:** La comisión a revertir corresponderá al 5% del importe nominal de la cuota devuelta.
  - **RN-1508:** En devoluciones parciales por cuotas completas, solo se revertirá la comisión correspondiente a las cuotas seleccionadas.
  - **RN-1509:** En devoluciones totales, se considerará la totalidad de las comisiones de plataforma cobradas en el checkout.
  - **RN-1510:** Antes de ejecutar la devolución, se verificará la compatibilidad del proveedor con la reversión del Split Payment.
  - **RN-1511:** No se asumirá que la devolución del importe nominal revierte automáticamente la comisión de plataforma.
  - **RN-1512:** No se ejecutarán devoluciones automáticas que generen importes incorrectos o inconsistencias entre las partes.
  - **RN-1513:** Cuando el proveedor no permita realizar correctamente la reversión, se registrará la limitación.
  - **RN-1514:** Las operaciones afectadas requerirán una resolución operativa controlada.
  - **RN-1515:** No se marcará una devolución como completamente resuelta mientras existan componentes obligatorios pendientes de tratamiento.
  - **RN-1516:** Los costos de procesamiento de la pasarela se tratarán conforme a las condiciones contractuales reales del proveedor.
  - **RN-1517:** Se registrarán por separado los importes nominales, las comisiones de plataforma y los costos de pasarela.
  - **RN-1518:** Se impedirá devolver dos veces una misma comisión.
  - **RN-1519:** Las reversiones deberán reflejarse en los reportes comerciales y financieros.
  - **RN-1520:** Toda devolución y resolución excepcional quedará auditada.
  - **RN-1521:** Se conservará la trazabilidad entre el checkout original, las cuotas y los importes devueltos.
</box>

## Ejemplo — Devolución de una cuota

<box border radius="lg" padding={3} gap={2}>
  <text color="secondary" size="xs">Ejemplo ilustrativo, sin incluir costos de pasarela.</text>
  <table>
    <table-row>
      <table-cell>Importe nominal de la cuota</table-cell>
      <table-cell align="end">$20.000</table-cell>
    </table-row>
    <table-row>
      <table-cell>Comisión de plataforma (5%)</table-cell>
      <table-cell align="end">$1.000</table-cell>
    </table-row>
    <table-row>
      <table-cell>**Importe a devolver**</table-cell>
      <table-cell align="end">**$21.000**</table-cell>
    </table-row>
  </table>
  <text size="sm" color="secondary">El sistema deberá comprobar que la devolución de los $20.000 de la disciplina y los $1.000 de la plataforma pueda realizarse correctamente. El tratamiento de los costos de pasarela se determina por separado.</text>
</box>

---

## Fase 4 — Costos de pasarela en las devoluciones

<text color="secondary" weight="medium" size="xs">Pregunta 150 — ¿Qué debería ocurrir con los costos de procesamiento de la pasarela cuando se devuelve una cuota?</text>

Por ejemplo, un responsable pagó una cuota de $20.000, más $1.000 de comisión de plataforma y $800 de procesamiento de la pasarela.

Ya decidimos que la plataforma intentará devolver sus $1.000. Falta definir quién asume los $800 si la pasarela no los reintegra.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Los asume el pagador**

      Si la pasarela no devuelve sus costos de procesamiento, el responsable recibirá el importe nominal de la cuota más la comisión del 5%, pero no los costos retenidos por la pasarela.
    </radio>
    <radio value="b">
      **Opción B — Los asume la plataforma**

      El responsable recibirá el total original, incluidos los costos de procesamiento, aunque la pasarela no los reintegre. La plataforma cubrirá esa diferencia.
    </radio>
    <radio value="c">
      **Opción C — Según las condiciones del proveedor, con información transparente (recomendada)**

      La plataforma intentará devolver también los costos de procesamiento cuando el proveedor lo permita.

      <text color="secondary" size="xs">Si la pasarela no los reintegra, se aplicarán sus condiciones reales y la política de devolución comunicada al pagador, sin asumir automáticamente que la plataforma cubrirá la diferencia. Antes de confirmar la solicitud, el administrador visualizará el importe estimado a devolver y los costos no recuperables. Cualquier excepción requerirá una resolución autorizada y auditada.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 150 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"si la pasarela no reintegra sus costos, estos no se devolverán al pagador",b:"la plataforma cubrirá los costos de pasarela no reintegrados para devolver siempre el total original",c:"tratar los costos según las condiciones reales de la pasarela y la política informada al pagador, mostrar los importes antes de confirmar y gestionar excepciones autorizadas y auditadas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 149 decisiones aprobadas. Aproximadamente 21 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-150

### Registro 373 — Tratamiento de costos de pasarela en devoluciones
Fuente: `ab5dd86a-3982-4223-830d-62c524cb0f91`

Respuesta previa del usuario:

Para la pregunta 150 del relevamiento SDD elijo la opción C: tratar los costos según las condiciones reales de la pasarela y la política informada al pagador, mostrar los importes antes de confirmar y gestionar excepciones autorizadas y auditadas.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 150 — Confirmada: opción C**
</row>

Los costos de procesamiento de la pasarela se tratarán según las condiciones reales del proveedor y la política de devolución comunicada al pagador.

Antes de confirmar una devolución, el administrador verá **cuánto dinero se devolverá y qué costos no podrán recuperarse**.

## Registro SDD — Decisión 150

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-150 — Tratamiento de costos de pasarela en devoluciones**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1522:** Se intentarán reintegrar los costos de procesamiento cuando el proveedor lo permita.
  - **RN-1523:** El tratamiento de estos costos dependerá de las condiciones reales de cada pasarela.
  - **RN-1524:** Se respetará la política de devolución previamente comunicada al pagador.
  - **RN-1525:** No se asumirá que los costos de procesamiento son siempre recuperables.
  - **RN-1526:** La plataforma no cubrirá automáticamente los costos que el proveedor no reintegre.
  - **RN-1527:** Antes de confirmar una devolución, se presentará un desglose de los importes estimados.
  - **RN-1528:** El desglose identificará el importe nominal de las cuotas seleccionadas.
  - **RN-1529:** Se identificará por separado la comisión del 5% correspondiente.
  - **RN-1530:** Se mostrarán los costos de pasarela recuperables y no recuperables, cuando esa información esté disponible.
  - **RN-1531:** Los importes estimados se distinguirán de los efectivamente confirmados por el proveedor.
  - **RN-1532:** Si no puede determinarse un importe confiable, se impedirá confirmar automáticamente una devolución potencialmente incorrecta.
  - **RN-1533:** Las excepciones requerirán autorización de un usuario con permisos suficientes.
  - **RN-1534:** Toda excepción deberá registrar una justificación.
  - **RN-1535:** Se auditarán los importes, responsables, decisiones y resultados.
  - **RN-1536:** Las diferencias entre importes estimados y efectivamente devueltos deberán conciliarse.
  - **RN-1537:** Los reportes financieros distinguirán los costos de procesamiento de las comisiones propias de la plataforma.
  - **RN-1538:** Las devoluciones conservarán el aislamiento financiero entre disciplinas.
</box>

## Ejemplo de devolución

<box border radius="lg" padding={3} gap={2}>
  <text color="secondary" size="xs">Ejemplo hipotético: la pasarela no reintegra su costo de procesamiento.</text>
  <table>
    <table-row>
      <table-cell>Cuota original</table-cell>
      <table-cell align="end">$20.000</table-cell>
    </table-row>
    <table-row>
      <table-cell>Comisión plataforma (5%)</table-cell>
      <table-cell align="end">$1.000</table-cell>
    </table-row>
    <table-row>
      <table-cell>Costo pasarela</table-cell>
      <table-cell align="end">$800</table-cell>
    </table-row>
    <table-row>
      <table-cell>**Total originalmente pagado**</table-cell>
      <table-cell align="end">**$21.800**</table-cell>
    </table-row>
    <table-row>
      <table-cell>**Devolución estimada**</table-cell>
      <table-cell align="end">**$21.000**</table-cell>
    </table-row>
    <table-row>
      <table-cell>Costo no recuperable</table-cell>
      <table-cell align="end">$800</table-cell>
    </table-row>
  </table>
  <caption>El ejemplo supone que esta distribución está permitida por la pasarela y la política comunicada al pagador. No representa una condición confirmada de Mercado Pago ni de otro proveedor.</caption>
</box>

---

## Fase 4 — Excepciones en devoluciones

<text color="secondary" weight="medium" size="xs">Pregunta 151 — ¿Quién podrá autorizar una excepción cuando una devolución requiera asumir costos adicionales?</text>

Por ejemplo, la pasarela no devuelve $800 de procesamiento, pero se decide que el responsable reciba igualmente los $21.800 que pagó originalmente.

Alguien deberá autorizar quién cubre esa diferencia.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Administrador de disciplina**

      Cada administrador de disciplina podrá autorizar excepciones para sus propias devoluciones y asumir el costo adicional desde la cuenta de su disciplina, cuando técnicamente sea posible.
    </radio>
    <radio value="b">
      **Opción B — Solo superadministrador**

      Todas las excepciones deberán ser aprobadas por el superadministrador, independientemente de quién asuma el costo.
    </radio>
    <radio value="c">
      **Opción C — Según quién asuma el costo (recomendada)**

      Si la diferencia económica la asume la disciplina, sus administradores podrán autorizarla dentro de sus permisos y de las capacidades de la pasarela.

      <text color="secondary" size="xs">Si la diferencia la asume la plataforma, deberá autorizarla el superadministrador. En ambos casos se registrarán el motivo, el importe adicional, quién lo financia y el resultado. Nunca se moverán fondos entre cuentas sin una operación autorizada y compatible con el proveedor.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 151 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir que administradores de disciplina autoricen excepciones de devolución en sus propias operaciones",b:"reservar al superadministrador la autorización de todas las excepciones",c:"permitir que cada disciplina autorice costos adicionales a su cargo y reservar al superadministrador los costos asumidos por la plataforma, con auditoría y validación de pasarela"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 150 decisiones aprobadas. Aproximadamente 20 preguntas restantes hasta el objetivo propuesto de 170.</caption>
```

## DEC-151

### Registro 375 — Autorización de costos excepcionales en devoluciones
Fuente: `38a2be52-2d72-4a2f-8479-4f084e5ceeb1`

Respuesta previa del usuario:

Para la pregunta 151 del relevamiento SDD elijo la opción C: permitir que cada disciplina autorice costos adicionales a su cargo y reservar al superadministrador los costos asumidos por la plataforma, con auditoría y validación de pasarela.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 151 — Confirmada: opción C**
</row>

Las excepciones de devolución se autorizarán según **quién asuma el costo adicional**: la disciplina podrá aprobar los costos propios y el superadministrador deberá autorizar los que asuma la plataforma.

## Registro SDD — Decisión 151

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-151 — Autorización de costos excepcionales en devoluciones**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1539:** Toda excepción de devolución deberá identificar el costo adicional que se pretende cubrir.
  - **RN-1540:** Se deberá identificar quién asumirá económicamente la diferencia.
  - **RN-1541:** Si la diferencia corresponde a la disciplina, sus administradores autorizados podrán aprobarla.
  - **RN-1542:** Los administradores de disciplina no podrán comprometer fondos de otras disciplinas.
  - **RN-1543:** Si la diferencia corresponde a la plataforma, únicamente el superadministrador podrá autorizarla.
  - **RN-1544:** Los administradores de disciplina no podrán autorizar gastos con cargo a la plataforma.
  - **RN-1545:** Toda excepción requerirá un motivo obligatorio.
  - **RN-1546:** Se registrará el importe adicional autorizado.
  - **RN-1547:** Se registrarán el usuario responsable y la fecha de autorización.
  - **RN-1548:** Antes de ejecutar la operación, se comprobarán las capacidades reales de la pasarela.
  - **RN-1549:** No se realizarán movimientos entre cuentas sin una operación autorizada y técnicamente compatible.
  - **RN-1550:** La aprobación administrativa no implicará que la devolución haya sido ejecutada exitosamente.
  - **RN-1551:** Se registrará el resultado efectivo informado por el proveedor.
  - **RN-1552:** Las excepciones se incorporarán a la conciliación financiera.
  - **RN-1553:** Los reportes identificarán qué parte asumió cada costo.
  - **RN-1554:** Se evitarán ejecuciones duplicadas mediante controles de idempotencia.
  - **RN-1555:** Todas las autorizaciones y resultados quedarán auditados.
</box>

## Ejemplo — ¿Quién paga los $800?

<grid columns={2} gap={3}>
  <grid-item>
    <box border radius="lg" padding={3} gap={2}>
      <icon name="users" color="secondary" size="lg"/>
      **Los asume Futsal**
      <text color="secondary" size="sm">Un administrador autorizado de Futsal aprueba cubrir los $800 con fondos de la disciplina.</text>
    </box>
  </grid-item>
  <grid-item>
    <box border radius="lg" padding={3} gap={2}>
      <icon name="shield-check" color="secondary" size="lg"/>
      **Los asume la plataforma**
      <text color="secondary" size="sm">El superadministrador autoriza cubrir los $800 con fondos de la plataforma.</text>
    </box>
  </grid-item>
</grid>

---

## Fase 4 — Solicitudes de devolución

<text color="secondary" weight="medium" size="xs">Pregunta 152 — ¿Los responsables de pago podrán solicitar devoluciones desde su propio portal?</text>

Actualmente definimos que los administradores de disciplina gestionan las devoluciones. Falta decidir si los responsables podrán iniciar una solicitud sin comunicarse directamente con el club.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Solo por contacto con el club**

      Los responsables deberán comunicarse con la disciplina. Únicamente los administradores podrán registrar y gestionar solicitudes de devolución.
    </radio>
    <radio value="b">
      **Opción B — Solicitud y devolución automática**

      Los responsables podrán seleccionar una cuota pagada y solicitar su devolución, que se ejecutará automáticamente cuando la pasarela lo permita.
    </radio>
    <radio value="c">
      **Opción C — Solicitud desde el portal con aprobación administrativa (recomendada)**

      Los responsables podrán solicitar la devolución de una o varias cuotas completas desde su portal, indicando el motivo.

      <text color="secondary" size="xs">La solicitud se enviará a los administradores de la disciplina correspondiente, quienes decidirán si corresponde aprobarla o rechazarla. El responsable podrá consultar su estado, pero no ejecutar directamente la devolución. La aprobación seguirá las reglas de comisiones, costos, Split Payment y auditoría ya definidas.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 152 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir solicitudes de devolución solo mediante contacto con la disciplina y registro administrativo",b:"permitir que responsables soliciten y ejecuten devoluciones automáticas desde su portal",c:"permitir solicitudes de devolución desde el portal del responsable, con motivo obligatorio, seguimiento y aprobación administrativa por disciplina"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 151 decisiones aprobadas. Aproximadamente 19 preguntas restantes hasta el objetivo propuesto de 170.</caption>
```

### Registro 379 — Actualizada
Fuente: `7bb97432-16dd-49bd-9dc0-4fea17350474`

Respuesta previa del usuario:

en realidad depende la situacion, si es un error tecnico de la plataforma los aceptamos cubrir nosotros, si es una equivocacion de ellos los que pagaron y futsal decide reintegrar el dinero es problema de ellos esos 800

Registro original:

```text
Perfecto. Esto modifica la **DEC-151**: el costo adicional no se asigna libremente, sino según **quién fue responsable del problema que originó la devolución**.

## DEC-151 — Actualizada

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Responsabilidad por costos de devolución**
    <badge color="success">Actualizada</badge>
  </row>
  <table>
    <table-row>
      <table-cell width="48%">
        **Motivo de devolución**
      </table-cell>
      <table-cell>
        **Quién asume el costo no recuperable**
      </table-cell>
    </table-row>
    <table-row>
      <table-cell>Error técnico de nuestro SaaS, como un cobro duplicado provocado por el sistema</table-cell>
      <table-cell>**Nuestra plataforma**</table-cell>
    </table-row>
    <table-row>
      <table-cell>Equivocación del pagador y Futsal decide aceptar la devolución</table-cell>
      <table-cell>**Futsal**</table-cell>
    </table-row>
    <table-row>
      <table-cell>Error de gestión atribuible a Futsal</table-cell>
      <table-cell>**Futsal**</table-cell>
    </table-row>
    <table-row>
      <table-cell>Causa incierta o responsabilidad compartida</table-cell>
      <table-cell>**Evaluación excepcional**, sin asignación automática</table-cell>
    </table-row>
  </table>
</box>

### Reglas adicionales

- **RN-1556:** El sistema registrará la causa de cada devolución excepcional.
- **RN-1557:** Los costos no recuperables causados por fallas técnicas imputables a nuestro SaaS serán responsabilidad de la plataforma.
- **RN-1558:** Los costos derivados de errores del pagador, cuando la disciplina acepte devolver el pago, serán responsabilidad de esa disciplina.
- **RN-1559:** Los errores atribuibles a la gestión de una disciplina serán responsabilidad de esa disciplina.
- **RN-1560:** El superadministrador autorizará los costos asumidos por nuestra plataforma.
- **RN-1561:** Los administradores de disciplina autorizarán los costos que correspondan a su disciplina.
- **RN-1562:** Se registrarán la causa, evidencia, responsable económico, autorización e importe.
- **RN-1563:** Los casos ambiguos se revisarán antes de asignar costos, sin presumir automáticamente la responsabilidad.
- **RN-1564:** La ejecución seguirá dependiendo de las capacidades de la pasarela y de las condiciones aplicables.

**Ejemplo:** si nuestro sistema cobra dos veces por un error propio, nosotros asumimos los $800 no recuperables. Si un padre paga la cuota equivocada y Futsal acepta devolverla, esos $800 los asume Futsal.

---

## Pregunta 152 — ¿Los responsables podrán solicitar devoluciones desde su portal?

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **A — Solo mediante contacto con el club.**

      El responsable se comunica con Futsal y un administrador registra la solicitud.
    </radio>
    <radio value="b">
      **B — Devolución automática.**

      El responsable solicita la devolución desde su portal y el sistema intenta ejecutarla sin aprobación administrativa.
    </radio>
    <radio value="c">
      **C — Solicitud desde el portal con aprobación de Futsal (recomendada).**

      El responsable selecciona las cuotas completas, explica el motivo y envía la solicitud. Futsal evalúa si corresponde aprobarla y el sistema determina el tratamiento de los costos según la causa.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 152 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"solicitudes únicamente mediante contacto con la disciplina",b:"solicitudes y devoluciones automáticas desde el portal",c:"solicitudes desde el portal con motivo y aprobación administrativa de la disciplina, respetando la responsabilidad por costos definida"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>
```

## DEC-152

### Registro 381 — Solicitudes de devolución desde el portal
Fuente: `4c06a873-451a-42ac-8a84-ef223d8dc77e`

Respuesta previa del usuario:

Para la pregunta 152 del relevamiento SDD elijo la opción C: solicitudes desde el portal con motivo y aprobación administrativa de la disciplina, respetando la responsabilidad por costos definida.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 152 — Confirmada: opción C**
</row>

Los responsables de pago podrán **solicitar devoluciones desde su portal**, pero la devolución no se ejecutará automáticamente. Los administradores de la disciplina correspondiente deberán evaluarla y aprobarla o rechazarla.

## Registro SDD — Decisión 152

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-152 — Solicitudes de devolución desde el portal**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1565:** Los responsables autenticados podrán solicitar devoluciones desde su portal.
  - **RN-1566:** Solo podrán solicitar devoluciones de pagos asociados a jugadores para los que tengan una vinculación vigente y autorizada.
  - **RN-1567:** Podrán seleccionar una o varias cuotas completas pertenecientes al mismo checkout.
  - **RN-1568:** No podrán solicitar devoluciones por importes arbitrarios ni fracciones de cuotas.
  - **RN-1569:** Cada solicitud deberá incluir un motivo obligatorio.
  - **RN-1570:** La solicitud quedará vinculada al pago original, las cuotas seleccionadas y la disciplina.
  - **RN-1571:** Crear una solicitud no implicará ejecutar automáticamente una devolución.
  - **RN-1572:** Los administradores autorizados de la disciplina recibirán la solicitud para evaluarla.
  - **RN-1573:** Los administradores podrán aprobar o rechazar la solicitud, registrando su decisión y justificación.
  - **RN-1574:** El responsable podrá consultar el estado de su solicitud.
  - **RN-1575:** Se impedirá crear solicitudes activas duplicadas sobre las mismas cuotas.
  - **RN-1576:** La aprobación deberá respetar las capacidades reales de la pasarela y las reglas de Split Payment.
  - **RN-1577:** Se aplicará la política de reversión del 5% establecida en DEC-149.
  - **RN-1578:** Los costos no recuperables se asignarán según la causa y responsabilidad establecidas en DEC-151.
  - **RN-1579:** Si corresponde asumir costos por un error técnico de nuestro SaaS, intervendrá el superadministrador para autorizar ese gasto.
  - **RN-1580:** Los estados de la solicitud y los de la devolución efectiva se distinguirán claramente.
  - **RN-1581:** Se notificará al responsable cuando su solicitud sea aprobada, rechazada o se complete la devolución.
  - **RN-1582:** Toda solicitud, decisión y operación quedará auditada y asociada a su disciplina.
</box>

## Ejemplo de funcionamiento

<box border radius="lg" padding={3} gap={2}>
  <list marker="number" connector="solid" gap={2}>
    <list-item>Un padre ingresa al portal y selecciona una cuota de Futsal que pagó por equivocación.</list-item>
    <list-item>Solicita la devolución indicando el motivo.</list-item>
    <list-item>Los administradores de Futsal reciben y evalúan la solicitud.</list-item>
    <list-item>Futsal aprueba la devolución y asume los costos de pasarela que correspondan según DEC-151.</list-item>
    <list-item>El sistema procesa la devolución cuando las condiciones técnicas lo permiten y actualiza su estado.</list-item>
    <list-item>El padre recibe una notificación con el resultado.</list-item>
  </list>
</box>

---

## Fase 4 — Gestión de solicitudes de devolución

<text color="secondary" weight="medium" size="xs">Pregunta 153 — ¿Los responsables podrán cancelar una solicitud de devolución que todavía no fue procesada?</text>

Por ejemplo, un padre solicita una devolución y, unos minutos después, se da cuenta de que la cuota estaba correctamente pagada.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — No permitir cancelaciones**

      Una vez enviada la solicitud, únicamente los administradores podrán cerrarla o rechazarla.
    </radio>
    <radio value="b">
      **Opción B — Cancelación en cualquier momento**

      El responsable podrá cancelar la solicitud incluso después de que haya sido aprobada o iniciada la devolución.
    </radio>
    <radio value="c">
      **Opción C — Cancelación hasta antes de iniciar la devolución (recomendada)**

      El responsable podrá cancelar su solicitud mientras no se haya iniciado ninguna operación de devolución ante la pasarela.

      <text color="secondary" size="xs">Si la solicitud ya fue aprobada pero todavía no se envió la operación, podrá cancelarla. Si la devolución está en proceso, confirmada o con resultado incierto, no podrá cancelarla unilateralmente. Se conservará el historial y se notificará a los administradores.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 153 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"no permitir que responsables cancelen solicitudes de devolución",b:"permitir cancelar solicitudes de devolución en cualquier estado",c:"permitir cancelar solicitudes hasta antes de iniciar la operación de devolución ante la pasarela, conservando historial y notificaciones"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 152 decisiones aprobadas. Aproximadamente 18 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-153

### Registro 383 — Cancelación de solicitudes de devolución
Fuente: `c2b3ac8b-de99-48c7-960e-3e7f33fadc17`

Respuesta previa del usuario:

Para la pregunta 153 del relevamiento SDD elijo la opción C: permitir cancelar solicitudes hasta antes de iniciar la operación de devolución ante la pasarela, conservando historial y notificaciones.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 153 — Confirmada: opción C**
</row>

Los responsables podrán cancelar sus solicitudes de devolución **hasta antes de que se inicie la operación ante la pasarela**, incluso si la disciplina ya aprobó administrativamente la solicitud.

## Registro SDD — Decisión 153

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-153 — Cancelación de solicitudes de devolución**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1583:** El responsable podrá cancelar una solicitud propia mientras la devolución no haya comenzado a ejecutarse.
  - **RN-1584:** Se permitirá cancelar solicitudes pendientes de revisión.
  - **RN-1585:** También se permitirá cancelar solicitudes aprobadas administrativamente si todavía no se inició la operación ante la pasarela.
  - **RN-1586:** Una vez iniciada la devolución, el responsable no podrá cancelarla unilateralmente.
  - **RN-1587:** Las devoluciones en proceso, confirmadas o con resultado incierto no admitirán cancelación desde el portal.
  - **RN-1588:** El sistema verificará el estado actualizado antes de aceptar una cancelación.
  - **RN-1589:** La cancelación y el inicio de ejecución deberán controlarse de forma atómica para evitar operaciones simultáneas contradictorias.
  - **RN-1590:** Una solicitud cancelada no podrá iniciar posteriormente una devolución.
  - **RN-1591:** La cancelación no modificará automáticamente el estado financiero de las cuotas originales.
  - **RN-1592:** Se registrarán el responsable, la fecha y el estado anterior de la solicitud.
  - **RN-1593:** Los administradores de la disciplina recibirán una notificación de la cancelación.
  - **RN-1594:** El responsable podrá consultar la solicitud cancelada en su historial.
  - **RN-1595:** La cancelación conservará toda la trazabilidad y auditoría.
  - **RN-1596:** Se respetará el aislamiento de solicitudes entre disciplinas.
</box>

## Ejemplo

<box border radius="lg" padding={3} gap={2}>
  <table>
    <table-row>
      <table-cell>Solicitud pendiente</table-cell>
      <table-cell align="end"><badge color="success">Puede cancelar</badge></table-cell>
    </table-row>
    <table-row>
      <table-cell>Aprobada, sin ejecución iniciada</table-cell>
      <table-cell align="end"><badge color="success">Puede cancelar</badge></table-cell>
    </table-row>
    <table-row>
      <table-cell>Devolución iniciada</table-cell>
      <table-cell align="end"><badge>Ya no puede</badge></table-cell>
    </table-row>
    <table-row>
      <table-cell>Devolución completada</table-cell>
      <table-cell align="end"><badge>Ya no puede</badge></table-cell>
    </table-row>
  </table>
</box>

---

## Fase 4 — Plazos para solicitar devoluciones

<text color="secondary" weight="medium" size="xs">Pregunta 154 — ¿Hasta cuánto tiempo después de pagar una cuota se podrá solicitar una devolución desde el portal?</text>

Por ejemplo, un padre pagó una cuota hace tres meses y ahora quiere solicitar su devolución.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Plazo fijo de 30 días**

      Los responsables solo podrán solicitar devoluciones durante los primeros 30 días desde el pago. Después deberán comunicarse con el club.
    </radio>
    <radio value="b">
      **Opción B — Sin límite de tiempo interno**

      Los responsables podrán solicitar una devolución desde su portal en cualquier momento, aunque la posibilidad de ejecutarla dependerá de la pasarela y las condiciones aplicables.
    </radio>
    <radio value="c">
      **Opción C — Plazo configurable por disciplina (recomendada)**

      Cada disciplina podrá establecer cuántos días después del pago se aceptan solicitudes de devolución desde el portal.

      <text color="secondary" size="xs">El sistema respetará además los límites reales de la pasarela y las obligaciones legales aplicables. Fuera del plazo configurado, el responsable podrá contactar a la disciplina para una revisión excepcional, sin que ello garantice la devolución.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 154 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir solicitudes desde el portal solo dentro de 30 días del pago",b:"permitir solicitudes desde el portal sin límite de tiempo interno, sujetas a condiciones reales de pasarela y legales",c:"permitir que cada disciplina configure el plazo para solicitar devoluciones desde el portal, respetando límites de pasarela y obligaciones legales"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 153 decisiones aprobadas. Aproximadamente 17 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-154

### Registro 385 — Plazo configurable para solicitar devoluciones
Fuente: `f88629b4-e1b1-4d70-821d-48e437d338a6`

Respuesta previa del usuario:

Para la pregunta 154 del relevamiento SDD elijo la opción C: permitir que cada disciplina configure el plazo para solicitar devoluciones desde el portal, respetando límites de pasarela y obligaciones legales.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 154 — Confirmada: opción C**
</row>

Cada disciplina podrá configurar **cuántos días después de un pago se aceptan solicitudes de devolución desde el portal**.

Este plazo será independiente para cada disciplina y deberá respetar las condiciones de la pasarela y las obligaciones legales aplicables.

## Registro SDD — Decisión 154

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-154 — Plazo configurable para solicitar devoluciones**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1597:** Cada disciplina podrá configurar su propio plazo para solicitar devoluciones desde el portal.
  - **RN-1598:** El plazo se expresará en días desde la fecha del pago confirmado.
  - **RN-1599:** Los administradores autorizados de la disciplina podrán modificar esta configuración.
  - **RN-1600:** La configuración de una disciplina no afectará a otras disciplinas.
  - **RN-1601:** El sistema verificará el plazo al momento de enviar una nueva solicitud.
  - **RN-1602:** Dentro del plazo configurado, el responsable podrá presentar una solicitud sobre pagos elegibles.
  - **RN-1603:** Fuera del plazo, se bloqueará la solicitud ordinaria desde el portal.
  - **RN-1604:** Se informará al responsable que puede contactar a la disciplina para solicitar una revisión excepcional.
  - **RN-1605:** Una revisión excepcional no garantizará la aprobación de la devolución.
  - **RN-1606:** Se respetarán los límites técnicos y operativos de la pasarela.
  - **RN-1607:** Las obligaciones legales aplicables prevalecerán sobre las restricciones internas.
  - **RN-1608:** Estar dentro del plazo configurado no garantizará que la devolución pueda ejecutarse.
  - **RN-1609:** Las solicitudes presentadas válidamente conservarán su historial aunque posteriormente cambie la configuración.
  - **RN-1610:** Los cambios de configuración quedarán auditados.
  - **RN-1611:** Las devoluciones excepcionales respetarán las reglas de autorización, costos y responsabilidad previamente aprobadas.
</box>

## Ejemplo

<grid columns={2} gap={3}>
  <grid-item>
    <box border radius="lg" padding={3} gap={1}>
      <text color="secondary" weight="medium" size="xs">Obras Futsal</text>
      <title size="xl" color="default" tabularNums>30 días</title>
      <text size="xs" color="secondary">Plazo configurado de ejemplo</text>
    </box>
  </grid-item>
  <grid-item>
    <box border radius="lg" padding={3} gap={1}>
      <text color="secondary" weight="medium" size="xs">Obras Básquet</text>
      <title size="xl" color="default" tabularNums>15 días</title>
      <text size="xs" color="secondary">Plazo configurado de ejemplo</text>
    </box>
  </grid-item>
</grid>

Un responsable que pagó hace 20 días podría solicitar una devolución desde Futsal, pero ya estaría fuera del plazo ordinario de Básquet.

---

## Fase 4 — Cambios en las políticas de devolución

<text color="secondary" weight="medium" size="xs">Pregunta 155 — ¿Qué ocurre cuando una disciplina modifica el plazo para solicitar devoluciones?</text>

Por ejemplo, Futsal tenía un plazo de 30 días y decide reducirlo a 10. Un padre pagó hace 20 días y todavía no solicitó la devolución.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Aplicar el nuevo plazo inmediatamente a todos**

      El padre pierde la posibilidad de solicitar la devolución desde el portal porque el nuevo plazo es de 10 días, aunque cuando pagó era de 30.
    </radio>
    <radio value="b">
      **Opción B — Aplicar los cambios solo a pagos futuros**

      Los pagos realizados antes del cambio conservan el plazo que estaba vigente cuando se efectuaron.
    </radio>
    <radio value="c">
      **Opción C — Conservar las condiciones originales y registrar versiones (recomendada)**

      Cada pago conserva la versión de la política de devolución vigente cuando fue realizado. Los cambios se aplican únicamente a pagos posteriores.

      <text color="secondary" size="xs">Se mantiene un historial de versiones con fecha de vigencia, responsable del cambio y condiciones aplicadas. Las obligaciones legales y las condiciones reales de la pasarela siguen prevaleciendo. Así evitamos que una disciplina reduzca retroactivamente los derechos informados al pagador.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 155 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"aplicar inmediatamente el nuevo plazo de devolución a todos los pagos",b:"aplicar cambios solo a pagos futuros y conservar el plazo original para pagos anteriores",c:"versionar las políticas de devolución, asociar a cada pago la versión vigente y aplicar cambios solo a pagos futuros, respetando obligaciones legales y condiciones de pasarela"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 154 decisiones aprobadas. Aproximadamente 16 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-155

### Registro 387 — Versionado de políticas de devolución
Fuente: `1991a39d-e7d8-4bc0-94c1-964df335ea09`

Respuesta previa del usuario:

Para la pregunta 155 del relevamiento SDD elijo la opción C: versionar las políticas de devolución, asociar a cada pago la versión vigente y aplicar cambios solo a pagos futuros, respetando obligaciones legales y condiciones de pasarela.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 155 — Confirmada: opción C**
</row>

Cada pago conservará la **versión de la política de devolución vigente al momento de realizarse**. Los cambios que haga una disciplina afectarán únicamente a los pagos futuros.

## Registro SDD — Decisión 155

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-155 — Versionado de políticas de devolución**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1612:** Cada disciplina tendrá políticas de devolución versionadas.
  - **RN-1613:** Cada versión tendrá un identificador único y una fecha de entrada en vigencia.
  - **RN-1614:** Cada pago conservará una referencia a la versión vigente cuando fue realizado.
  - **RN-1615:** Las modificaciones de política se aplicarán únicamente a pagos posteriores a su entrada en vigencia.
  - **RN-1616:** Los cambios no reducirán retroactivamente el plazo de solicitud informado para pagos anteriores.
  - **RN-1617:** Las versiones anteriores se conservarán para consulta y auditoría.
  - **RN-1618:** Una versión utilizada por pagos existentes no podrá sobrescribirse ni eliminarse.
  - **RN-1619:** Los administradores autorizados podrán crear nuevas versiones para su disciplina.
  - **RN-1620:** Se registrarán el usuario que realizó el cambio, la fecha y los valores anteriores y nuevos.
  - **RN-1621:** Al solicitar una devolución, se utilizará la versión asociada al pago correspondiente.
  - **RN-1622:** Las obligaciones legales aplicables prevalecerán sobre las restricciones de la política.
  - **RN-1623:** Los límites técnicos y contractuales reales de la pasarela deberán verificarse antes de ejecutar una devolución.
  - **RN-1624:** Las solicitudes existentes conservarán su trazabilidad aunque posteriormente cambie la política.
  - **RN-1625:** El versionado será independiente para cada disciplina.
  - **RN-1626:** El sistema permitirá identificar qué política se aplicó a cada solicitud y devolución.
</box>

## Ejemplo — Cambio de política en Futsal

<box border radius="lg" padding={3} gap={2}>
  <list marker="number" connector="solid" gap={2}>
    <list-item>
      **1 de noviembre:** un padre paga una cuota bajo la política V1, que permite solicitar devoluciones durante 30 días.
    </list-item>
    <list-item>
      **15 de noviembre:** Futsal publica la política V2, que reduce el plazo a 10 días.
    </list-item>
    <list-item>
      **20 de noviembre:** el padre todavía puede solicitar la devolución del pago del día 1, porque conserva la política V1.
    </list-item>
    <list-item>
      Los pagos realizados desde el 15 de noviembre utilizan V2 y su plazo de 10 días.
    </list-item>
  </list>
</box>

---

## Fase 4 — Motivos de devolución

<text color="secondary" weight="medium" size="xs">Pregunta 156 — ¿Cómo deberían registrarse los motivos de las solicitudes de devolución?</text>

Esto es importante porque ya definimos que **la causa del problema determina quién asume los costos no recuperables**: nuestra plataforma si fue un error técnico propio, o la disciplina cuando corresponde a una equivocación del pagador y decide aceptar la devolución.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Texto libre**

      El responsable explica el motivo con sus palabras. El administrador lo interpreta y decide cómo proceder.
    </radio>
    <radio value="b">
      **Opción B — Lista fija de motivos**

      El responsable selecciona un motivo predefinido, como pago equivocado, cobro duplicado o solicitud personal. No puede agregar una explicación.
    </radio>
    <radio value="c">
      **Opción C — Motivos predefinidos más explicación y validación administrativa (recomendada)**

      El responsable selecciona un motivo y puede agregar detalles. Los administradores verifican la causa real antes de aprobar la devolución.

      <text color="secondary" size="xs">La plataforma mantiene un catálogo de motivos, incluyendo error del pagador, error de la disciplina, posible error técnico del sistema y otros. La selección del responsable no determina automáticamente quién paga los costos: esa responsabilidad se establece mediante revisión y evidencia. Los casos atribuibles al SaaS requieren intervención del superadministrador.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 156 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"registrar motivos de devolución únicamente mediante texto libre",b:"utilizar exclusivamente una lista fija de motivos sin explicación adicional",c:"utilizar motivos predefinidos con explicación adicional y validación administrativa de la causa real antes de asignar costos"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 155 decisiones aprobadas. Aproximadamente 15 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-156

### Registro 389 — Motivos y validación de devoluciones
Fuente: `8d9bedfb-7b55-4e73-8b9e-3fa53116aeef`

Respuesta previa del usuario:

Para la pregunta 156 del relevamiento SDD elijo la opción C: utilizar motivos predefinidos con explicación adicional y validación administrativa de la causa real antes de asignar costos.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 156 — Confirmada: opción C**
</row>

Las solicitudes de devolución tendrán **motivos predefinidos y una explicación adicional**. Los administradores deberán verificar la causa real antes de determinar quién asume los costos.

Esto evita, por ejemplo, que un responsable seleccione «error de la plataforma» y que automáticamente nuestra empresa tenga que cubrir los gastos.

## Registro SDD — Decisión 156

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-156 — Motivos y validación de devoluciones**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1627:** Toda solicitud de devolución deberá incluir un motivo predefinido.
  - **RN-1628:** El responsable podrá agregar una explicación complementaria.
  - **RN-1629:** La plataforma dispondrá de un catálogo de motivos de devolución.
  - **RN-1630:** El catálogo contemplará errores del pagador, errores de la disciplina, posibles errores técnicos de la plataforma y otros motivos.
  - **RN-1631:** Cuando se seleccione «Otro», será obligatorio explicar la situación.
  - **RN-1632:** El motivo declarado por el responsable no determinará automáticamente la responsabilidad económica.
  - **RN-1633:** Los administradores de la disciplina deberán revisar la solicitud y sus antecedentes.
  - **RN-1634:** Se registrará por separado el motivo declarado y la causa verificada administrativamente.
  - **RN-1635:** La clasificación de la causa deberá contar con una justificación.
  - **RN-1636:** Se podrán incorporar evidencias que respalden la evaluación.
  - **RN-1637:** Si se determina que existió un error técnico imputable a nuestro SaaS, deberá intervenir el superadministrador para autorizar los costos correspondientes.
  - **RN-1638:** Si el error corresponde al pagador y la disciplina acepta devolver el dinero, los costos no recuperables serán responsabilidad de la disciplina.
  - **RN-1639:** Los casos de responsabilidad incierta o compartida requerirán revisión excepcional.
  - **RN-1640:** La asignación de costos respetará la DEC-151 actualizada.
  - **RN-1641:** Toda decisión conservará la identidad del administrador, la fecha y su justificación.
  - **RN-1642:** El historial de motivos y clasificaciones no podrá alterarse sin trazabilidad.
  - **RN-1643:** Las solicitudes y sus evidencias respetarán el aislamiento entre disciplinas.
</box>

## Ejemplo práctico

<box border radius="lg" padding={3} gap={2}>
  <text color="secondary" weight="medium" size="xs">SOLICITUD DEL RESPONSABLE</text>
  <row align="center" justify="between">
    **Motivo seleccionado**
    <badge>Posible cobro duplicado</badge>
  </row>
  **Explicación:** «Me cobraron dos veces la misma cuota».

  <divider color="subtle"/>

  <text color="secondary" weight="medium" size="xs">REVISIÓN ADMINISTRATIVA</text>
  <list marker="bullet" gap={1}>
    <list-item>Si el sistema generó incorrectamente dos cobros, se clasifica como error técnico del SaaS.</list-item>
    <list-item>Si el responsable realizó dos pagos distintos por equivocación, se clasifica como error del pagador.</list-item>
    <list-item>Si no se puede determinar la causa, queda pendiente de revisión y no se asigna automáticamente el costo.</list-item>
  </list>
</box>

---

## Fase 4 — Evidencias en solicitudes de devolución

<text color="secondary" weight="medium" size="xs">Pregunta 157 — ¿Los responsables podrán adjuntar comprobantes o imágenes al solicitar una devolución?</text>

Por ejemplo, un padre afirma que pagó dos veces y quiere adjuntar capturas de sus movimientos bancarios.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Sin archivos adjuntos**

      El responsable únicamente podrá seleccionar el motivo y escribir una explicación. Los comprobantes se enviarán por fuera del sistema.
    </radio>
    <radio value="b">
      **Opción B — Comprobantes obligatorios**

      Toda solicitud deberá incluir al menos un archivo de respaldo, independientemente del motivo.
    </radio>
    <radio value="c">
      **Opción C — Adjuntos opcionales y almacenamiento seguro (recomendada)**

      El responsable podrá adjuntar comprobantes, capturas o documentos, pero no será obligatorio en todos los casos.

      <text color="secondary" size="xs">Se permitirán formatos como PDF, JPG y PNG, con límites de tamaño y cantidad. Los archivos serán privados, accesibles únicamente a usuarios autorizados, y se conservarán vinculados a la solicitud con auditoría y controles de seguridad. Los administradores podrán solicitar evidencia adicional si la necesitan.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 157 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"no permitir archivos adjuntos en solicitudes de devolución",b:"exigir al menos un comprobante en toda solicitud de devolución",c:"permitir adjuntos opcionales PDF/JPG/PNG con límites, almacenamiento seguro, acceso autorizado y posibilidad de solicitar evidencia adicional"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 156 decisiones aprobadas. Aproximadamente 14 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-157

### Registro 391 — Evidencias adjuntas en solicitudes de devolución
Fuente: `137d22d0-e0c5-497b-b3ca-14f8057a64ee`

Respuesta previa del usuario:

Para la pregunta 157 del relevamiento SDD elijo la opción C: permitir adjuntos opcionales PDF/JPG/PNG con límites, almacenamiento seguro, acceso autorizado y posibilidad de solicitar evidencia adicional.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 157 — Confirmada: opción C**
</row>

Los responsables podrán adjuntar **comprobantes, capturas y documentos de forma opcional** cuando soliciten una devolución. Los archivos se almacenarán de manera segura y los administradores podrán solicitar evidencia adicional.

## Registro SDD — Decisión 157

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-157 — Evidencias adjuntas en solicitudes de devolución**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1644:** Los responsables podrán adjuntar archivos a sus solicitudes de devolución.
  - **RN-1645:** Los archivos serán opcionales al crear la solicitud.
  - **RN-1646:** Se admitirán formatos PDF, JPG y PNG.
  - **RN-1647:** Se establecerán límites de tamaño por archivo y cantidad de adjuntos.
  - **RN-1648:** El sistema validará el tipo y contenido de los archivos antes de almacenarlos.
  - **RN-1649:** Se aplicarán controles de seguridad para prevenir archivos maliciosos.
  - **RN-1650:** Los documentos se almacenarán de forma privada y protegidos mediante cifrado en reposo.
  - **RN-1651:** Los archivos estarán vinculados a la solicitud de devolución correspondiente.
  - **RN-1652:** Los administradores de disciplina solo podrán consultar evidencias de solicitudes bajo su responsabilidad.
  - **RN-1653:** El superadministrador podrá consultar evidencias dentro de sus permisos globales.
  - **RN-1654:** Los responsables solo podrán consultar los archivos de solicitudes a las que tengan acceso vigente.
  - **RN-1655:** Las descargas requerirán autorización y no utilizarán enlaces públicos permanentes.
  - **RN-1656:** Los administradores podrán solicitar evidencia adicional cuando resulte necesaria.
  - **RN-1657:** El responsable podrá incorporar nuevos archivos mientras la solicitud admita información complementaria.
  - **RN-1658:** Adjuntar documentos no implicará aprobar automáticamente la solicitud.
  - **RN-1659:** Se registrarán las incorporaciones, consultas y descargas relevantes para auditoría.
  - **RN-1660:** Los archivos estarán sujetos a las políticas de conservación aplicables, sin eliminar evidencias necesarias para disputas o auditorías.
  - **RN-1661:** Se mantendrá el aislamiento de archivos entre clubes y disciplinas.
</box>

## Ejemplo — Solicitud de devolución

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Solicitud #DEV-001**
    <badge color="warning">Pendiente de revisión</badge>
  </row>
  <text color="secondary" size="sm">Motivo: posible cobro duplicado</text>
  <row align="center" gap={2}>
    <icon name="file-image" color="secondary"/>
    <box flex="1" gap="0">
      **captura_banco.png**
      <text size="xs" color="secondary">Evidencia aportada por el responsable</text>
    </box>
    <icon name="check-circle-2" color="success"/>
  </row>
  <row align="center" gap={2}>
    <icon name="file-text" color="secondary"/>
    <box flex="1" gap="0">
      **comprobante.pdf**
      <text size="xs" color="secondary">Documento de respaldo</text>
    </box>
    <icon name="check-circle-2" color="success"/>
  </row>
  <divider color="subtle"/>
  <text size="sm">El administrador de Futsal puede revisar los documentos, pedir información adicional y decidir si corresponde aprobar la devolución.</text>
</box>

---

## Fase 4 — Información adicional en solicitudes

<text color="secondary" weight="medium" size="xs">Pregunta 158 — ¿Qué debería ocurrir cuando un administrador solicita más información para evaluar una devolución?</text>

Por ejemplo, Futsal recibe una solicitud por cobro duplicado, pero necesita que el responsable adjunte otro comprobante antes de decidir.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Comunicación fuera de la plataforma**

      El administrador contacta al responsable por teléfono o correo y luego registra la decisión final en el sistema.
    </radio>
    <radio value="b">
      **Opción B — Solicitud de información con plazo fijo de 7 días**

      El administrador solicita información desde el sistema. El responsable tiene 7 días para responder y, si no lo hace, la solicitud se rechaza automáticamente.
    </radio>
    <radio value="c">
      **Opción C — Solicitud de información dentro del portal, sin rechazo automático (recomendada)**

      El administrador puede solicitar información adicional, indicando qué necesita. La solicitud pasa al estado «Esperando información del responsable».

      <text color="secondary" size="xs">El responsable recibe una notificación, puede responder y adjuntar documentos desde su portal. La solicitud vuelve a revisión cuando responde. No se rechaza automáticamente por falta de respuesta; el administrador decide cómo proceder, respetando los plazos reales de la pasarela y las obligaciones aplicables. Se conserva el historial completo.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 158 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"gestionar solicitudes de información adicional fuera de la plataforma",b:"solicitar información dentro del sistema con rechazo automático si no responde en 7 días",c:"permitir solicitudes de información adicional dentro del portal, con notificaciones, respuestas, adjuntos y seguimiento, sin rechazo automático por falta de respuesta"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 157 decisiones aprobadas. Aproximadamente 13 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-158

### Registro 393 — Información adicional por canales externos
Fuente: `a77faac2-be9f-409a-b2e6-d71a856889f4`

Respuesta previa del usuario:

Para la pregunta 158 del relevamiento SDD elijo la opción A: gestionar solicitudes de información adicional fuera de la plataforma.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 158 — Confirmada: opción A**
</row>

Cuando un administrador necesite información adicional para evaluar una devolución, **se comunicará directamente con el responsable por fuera de la plataforma**, mediante teléfono, correo electrónico u otro canal.

Esto simplifica el sistema y evita desarrollar un módulo de mensajería para las devoluciones.

## Registro SDD — Decisión 158

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-158 — Información adicional por canales externos**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1662:** Los administradores de disciplina podrán solicitar información adicional por canales externos.
  - **RN-1663:** La plataforma no incluirá un chat para solicitudes de devolución.
  - **RN-1664:** No se implementará un intercambio de mensajes entre responsable y administrador dentro del expediente.
  - **RN-1665:** No será necesario incorporar un estado específico de «Esperando información del responsable».
  - **RN-1666:** Los administradores podrán utilizar los datos de contacto autorizados del responsable.
  - **RN-1667:** El administrador podrá registrar observaciones internas sobre la información recibida.
  - **RN-1668:** Los administradores autorizados podrán incorporar al expediente comprobantes obtenidos por canales externos.
  - **RN-1669:** Los documentos incorporados deberán respetar los controles de seguridad definidos en DEC-157.
  - **RN-1670:** La falta de respuesta del responsable no provocará un rechazo automático.
  - **RN-1671:** El administrador conservará la responsabilidad de aprobar o rechazar la solicitud.
  - **RN-1672:** La evaluación deberá respetar los plazos reales de la pasarela y las obligaciones legales aplicables.
  - **RN-1673:** La decisión final y su justificación quedarán registradas.
  - **RN-1674:** El responsable podrá seguir consultando el estado de su solicitud desde el portal.
  - **RN-1675:** Se mantendrán las notificaciones de resolución previamente definidas.
  - **RN-1676:** Toda incorporación administrativa de evidencia o modificación relevante quedará auditada.
</box>

## Ejemplo

Un padre solicita la devolución de una cuota por un supuesto cobro duplicado.

1. El administrador de Futsal revisa la solicitud.
2. Necesita otro comprobante y llama al padre.
3. El padre envía el documento por WhatsApp.
4. El administrador puede adjuntarlo al expediente y registrar una observación.
5. Futsal decide aprobar o rechazar la solicitud y el padre recibe la notificación correspondiente.

---

## Fase 4 — Historial de devoluciones

<text color="secondary" weight="medium" size="xs">Pregunta 159 — ¿Qué información deberían poder consultar los responsables sobre sus devoluciones?</text>

Ya definimos que los responsables pueden solicitar devoluciones y consultar sus estados. Falta determinar qué nivel de detalle tendrá su historial.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Historial básico**

      Mostrar fecha, cuota, importe solicitado y estado actual de cada devolución.
    </radio>
    <radio value="b">
      **Opción B — Historial completo de todas las operaciones**

      Mostrar todos los eventos, movimientos financieros, comisiones, costos de pasarela y observaciones administrativas internas.
    </radio>
    <radio value="c">
      **Opción C — Historial detallado para el responsable, sin información interna (recomendada)**

      Mostrar las cuotas involucradas, importes solicitados y efectivamente devueltos, motivo, fecha, estado, resolución y comprobante de devolución cuando esté disponible.

      <text color="secondary" size="xs">También se informarán los costos no reintegrados que correspondan al pagador. No se mostrarán notas internas, evidencias reservadas, datos de otros usuarios ni información comercial privada de la plataforma o de la disciplina.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 159 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"mostrar solo un historial básico de devoluciones con fecha, cuota, importe y estado",b:"mostrar el historial completo incluyendo operaciones financieras y observaciones internas",c:"mostrar historial detallado de devoluciones al responsable con importes, motivos, estados, resolución y comprobantes, excluyendo información administrativa interna y privada"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 158 decisiones aprobadas. Aproximadamente 12 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-159

### Registro 395 — Historial de devoluciones del responsable
Fuente: `2f682705-f04a-4123-b165-f8466c99df50`

Respuesta previa del usuario:

Para la pregunta 159 del relevamiento SDD elijo la opción C: mostrar historial detallado de devoluciones al responsable con importes, motivos, estados, resolución y comprobantes, excluyendo información administrativa interna y privada.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 159 — Confirmada: opción C**
</row>

Los responsables tendrán un **historial detallado de sus solicitudes y devoluciones**, con importes, estados, motivos, resoluciones y comprobantes disponibles, sin acceder a información administrativa interna.

## Registro SDD — Decisión 159

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-159 — Historial de devoluciones del responsable**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1677:** Los responsables podrán consultar el historial de solicitudes y devoluciones para las que mantengan acceso autorizado.
  - **RN-1678:** Cada registro identificará la disciplina y las cuotas involucradas.
  - **RN-1679:** Se mostrará la fecha de solicitud y el motivo declarado.
  - **RN-1680:** Se mostrará el importe originalmente solicitado.
  - **RN-1681:** Se identificará el estado actual de la solicitud.
  - **RN-1682:** Se distinguirá la aprobación administrativa de la ejecución efectiva de la devolución.
  - **RN-1683:** Se mostrará el importe efectivamente devuelto una vez confirmado por la pasarela.
  - **RN-1684:** Se informarán los costos no reintegrados que correspondan al pagador.
  - **RN-1685:** Se mostrará la resolución comunicable al responsable, incluida la justificación del rechazo cuando corresponda.
  - **RN-1686:** Se ofrecerá el comprobante de devolución cuando esté disponible.
  - **RN-1687:** Los comprobantes estarán sujetos a controles de acceso y descarga segura.
  - **RN-1688:** No se mostrarán observaciones administrativas internas.
  - **RN-1689:** No se expondrán evidencias reservadas ni información financiera privada de la disciplina o plataforma.
  - **RN-1690:** No se permitirá consultar datos de solicitudes de otros responsables sin autorización.
  - **RN-1691:** Se conservará la trazabilidad de solicitudes canceladas, rechazadas y completadas.
  - **RN-1692:** Las operaciones pendientes o inciertas no se presentarán como devoluciones confirmadas.
  - **RN-1693:** La visibilidad respetará la vigencia de las vinculaciones entre responsables y jugadores.
  - **RN-1694:** El historial mantendrá el aislamiento entre clubes y disciplinas.
</box>

## Ejemplo — Vista del responsable

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    <box gap="0">
      **Devolución #DEV-001**
      <text color="secondary" size="xs">Obras Futsal · Cuota noviembre</text>
    </box>
    <badge color="success">Completada</badge>
  </row>
  <divider color="subtle"/>
  <table>
    <table-row>
      <table-cell>Motivo</table-cell>
      <table-cell align="end">Pago equivocado</table-cell>
    </table-row>
    <table-row>
      <table-cell>Importe solicitado</table-cell>
      <table-cell align="end">$21.800</table-cell>
    </table-row>
    <table-row>
      <table-cell>Importe devuelto</table-cell>
      <table-cell align="end">**$21.000**</table-cell>
    </table-row>
    <table-row>
      <table-cell>Costo no reintegrado</table-cell>
      <table-cell align="end">$800</table-cell>
    </table-row>
    <table-row>
      <table-cell>Estado</table-cell>
      <table-cell align="end">Devolución confirmada</table-cell>
    </table-row>
  </table>
  <row align="center" gap={2}>
    <icon name="file-check-2" color="secondary"/>
    <text size="sm">Comprobante disponible para descarga segura</text>
  </row>
  <caption>Ejemplo ilustrativo sujeto a las condiciones de devolución comunicadas y a la confirmación real del proveedor.</caption>
</box>

---

## Fase 4 — Gestión administrativa de devoluciones

<text color="secondary" weight="medium" size="xs">Pregunta 160 — ¿Cómo deberían organizarse las solicitudes de devolución para los administradores?</text>

Imaginemos que Futsal recibe 50 solicitudes en un mes. Necesitamos definir cómo las visualizarán y gestionarán sus administradores.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Listado sencillo**

      Mostrar todas las solicitudes ordenadas por fecha, con su estado y acceso al detalle.
    </radio>
    <radio value="b">
      **Opción B — Listados separados por estado**

      Tener secciones independientes para pendientes, aprobadas, rechazadas, canceladas y completadas.
    </radio>
    <radio value="c">
      **Opción C — Bandeja unificada con filtros y búsqueda (recomendada)**

      Mostrar todas las solicitudes en una única bandeja, con filtros por estado, fecha, jugador, responsable, motivo y disciplina según los permisos del usuario.

      <text color="secondary" size="xs">Los administradores podrán buscar solicitudes, consultar su detalle y priorizar las pendientes. El superadministrador tendrá una vista global filtrable por club y disciplina. Se mantendrá el aislamiento operativo: un administrador de Futsal no podrá gestionar devoluciones de Básquet.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 160 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"utilizar un listado sencillo de solicitudes ordenadas por fecha",b:"organizar solicitudes en listados separados por estado",c:"utilizar una bandeja unificada de devoluciones con filtros, búsqueda, priorización de pendientes y vistas según permisos"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 159 decisiones aprobadas. Aproximadamente 11 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-160

### Registro 397 — Bandeja administrativa de devoluciones
Fuente: `7a94dcf0-7334-43ac-b5c9-46aaa90aa18d`

Respuesta previa del usuario:

Para la pregunta 160 del relevamiento SDD elijo la opción C: utilizar una bandeja unificada de devoluciones con filtros, búsqueda, priorización de pendientes y vistas según permisos.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 160 — Confirmada: opción C**
</row>

La plataforma tendrá una **bandeja unificada de solicitudes de devolución**, con filtros, búsqueda y priorización de solicitudes pendientes.

Cada administrador verá únicamente las solicitudes que puede gestionar. El superadministrador tendrá una vista global.

## Registro SDD — Decisión 160

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-160 — Bandeja administrativa de devoluciones**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1695:** Las solicitudes de devolución se gestionarán desde una bandeja unificada.
  - **RN-1696:** La bandeja mostrará solicitudes pendientes, aprobadas, rechazadas, canceladas, en proceso y completadas.
  - **RN-1697:** Se podrá filtrar por estado de solicitud.
  - **RN-1698:** Se podrá filtrar por rango de fechas.
  - **RN-1699:** Se podrá buscar por jugador y responsable de pago.
  - **RN-1700:** Se podrá filtrar por motivo de devolución.
  - **RN-1701:** Las solicitudes pendientes tendrán una visualización prioritaria.
  - **RN-1702:** Cada registro mostrará información resumida y permitirá consultar su detalle.
  - **RN-1703:** La bandeja tendrá paginación para manejar grandes volúmenes de solicitudes.
  - **RN-1704:** Los administradores de disciplina solo podrán gestionar solicitudes de su propia disciplina.
  - **RN-1705:** El superadministrador dispondrá de una vista global.
  - **RN-1706:** La vista global permitirá filtrar por club y disciplina.
  - **RN-1707:** El acceso global no permitirá ejecutar operaciones financieras sin las autorizaciones correspondientes.
  - **RN-1708:** Las acciones disponibles dependerán del estado de la solicitud y de los permisos del usuario.
  - **RN-1709:** La bandeja distinguirá las solicitudes aprobadas de las devoluciones efectivamente confirmadas.
  - **RN-1710:** Los cambios de estado se reflejarán en el historial de cada solicitud.
  - **RN-1711:** Las búsquedas y consultas respetarán el aislamiento de datos entre clubes y disciplinas.
  - **RN-1712:** Las acciones administrativas conservarán su trazabilidad y auditoría.
</box>

## Ejemplo — Bandeja de Futsal

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Solicitudes de devolución**
    <badge>Obras Futsal</badge>
  </row>
  <grid columns={3} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} gap={1} align="center">
        <title size="xl" tabularNums>8</title>
        <text size="2xs" color="secondary">Pendientes</text>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} gap={1} align="center">
        <title size="xl" tabularNums>3</title>
        <text size="2xs" color="secondary">En proceso</text>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} gap={1} align="center">
        <title size="xl" tabularNums>39</title>
        <text size="2xs" color="secondary">Resueltas</text>
      </box>
    </grid-item>
  </grid>
  <table>
    <table-row>
      <table-cell>
        **DEV-001**
        <text color="secondary" size="xs">Cuota noviembre</text>
      </table-cell>
      <table-cell align="end"><badge color="warning">Pendiente</badge></table-cell>
    </table-row>
    <table-row>
      <table-cell>
        **DEV-002**
        <text color="secondary" size="xs">Cuota octubre</text>
      </table-cell>
      <table-cell align="end"><badge>En proceso</badge></table-cell>
    </table-row>
    <table-row>
      <table-cell>
        **DEV-003**
        <text color="secondary" size="xs">Cuota septiembre</text>
      </table-cell>
      <table-cell align="end"><badge color="success">Completada</badge></table-cell>
    </table-row>
  </table>
  <caption>Datos ilustrativos para representar la futura interfaz administrativa.</caption>
</box>

---

## Fase 5 — Definición del alcance inicial (MVP)

Ya tenemos 160 decisiones. Es momento de empezar a definir qué funcionalidades deberán estar disponibles desde el primer lanzamiento y cuáles podrían incorporarse posteriormente.

<text color="secondary" weight="medium" size="xs">Pregunta 161 — ¿Qué alcance debería tener la primera versión comercial de la plataforma?</text>

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — MVP mínimo**

      Lanzar con clubes, disciplinas, jugadores, categorías, generación de cuotas, pagos online y registro de pagos externos.

      <text color="secondary" size="xs">Dejar para etapas posteriores becas avanzadas, reportes completos, conciliación avanzada, contracargos y devoluciones desde el portal.</text>
    </radio>
    <radio value="b">
      **Opción B — MVP completo pero por etapas (recomendada)**

      Primera versión con clubes, disciplinas, usuarios, jugadores, cuotas, becas, pagos online mediante Split Payment validado, pagos externos, portal del responsable, reportes básicos y conciliación esencial.

      <text color="secondary" size="xs">Para operar con dinero real se incluirán desde el inicio los controles indispensables de seguridad, auditoría, idempotencia y gestión de incidencias. Las funciones avanzadas de devoluciones, contracargos, reportes y automatizaciones se incorporarán en versiones posteriores, manteniendo procedimientos operativos controlados para resolver esos casos desde el lanzamiento.</text>
    </radio>
    <radio value="c">
      **Opción C — Lanzar con todas las funcionalidades relevadas**

      No publicar comercialmente hasta implementar y probar todas las funcionalidades, incluidas devoluciones completas, contracargos, conciliación avanzada, notificaciones, exportaciones y gestión comercial.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 161 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"lanzar un MVP mínimo con gestión de clubes, jugadores, cuotas y cobros esenciales",b:"lanzar un MVP funcional completo en lo esencial, con seguridad financiera y procedimientos controlados, dejando módulos avanzados para versiones posteriores",c:"implementar todas las funcionalidades relevadas antes del primer lanzamiento comercial"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 160 decisiones aprobadas. Aproximadamente 10 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-161

### Registro 399 — Alcance inicial del MVP
Fuente: `b9e42b6e-2067-452c-8d80-4c7cb2979408`

Respuesta previa del usuario:

Para la pregunta 161 del relevamiento SDD elijo la opción B: lanzar un MVP funcional completo en lo esencial, con seguridad financiera y procedimientos controlados, dejando módulos avanzados para versiones posteriores.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 161 — Confirmada: opción B**
</row>

La plataforma se lanzará con un **MVP funcional, completo en sus operaciones esenciales**, dejando las funcionalidades avanzadas para versiones posteriores.

El objetivo será que Obras Futsal pueda gestionar jugadores, generar cuotas y cobrar de forma segura, sin esperar a tener implementados todos los módulos administrativos.

## Registro SDD — Decisión 161

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-161 — Alcance inicial del MVP**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1713:** El producto se desarrollará y lanzará por etapas.
  - **RN-1714:** El MVP incluirá administración de clubes y disciplinas.
  - **RN-1715:** Incluirá usuarios, roles, permisos y aislamiento entre disciplinas.
  - **RN-1716:** Incluirá gestión de jugadores, inscripciones y categorías.
  - **RN-1717:** Incluirá generación automática de cuotas y configuración de importes.
  - **RN-1718:** Incluirá becas y beneficios económicos esenciales.
  - **RN-1719:** Incluirá registro de pagos externos.
  - **RN-1720:** Incluirá pagos online únicamente mediante una integración de Split Payment técnicamente validada.
  - **RN-1721:** Incluirá el portal del responsable para consultar y pagar cuotas autorizadas.
  - **RN-1722:** Incluirá reportes financieros básicos por disciplina.
  - **RN-1723:** Incluirá conciliación esencial de pagos y estados financieros.
  - **RN-1724:** Los controles de autenticación, autorización y seguridad serán obligatorios desde el MVP.
  - **RN-1725:** Se implementarán controles de idempotencia y prevención de cobros duplicados.
  - **RN-1726:** Los pagos inciertos deberán tratarse de forma segura antes de permitir nuevos intentos.
  - **RN-1727:** Las operaciones financieras deberán conservar trazabilidad y auditoría.
  - **RN-1728:** Las devoluciones y contracargos deberán contar con procedimientos operativos controlados desde el lanzamiento, aunque sus módulos avanzados se desarrollen posteriormente.
  - **RN-1729:** Los reportes avanzados, automatizaciones, exportaciones y gestión avanzada de devoluciones podrán incorporarse en versiones posteriores.
  - **RN-1730:** No se habilitarán cobros reales sin validar las capacidades de la pasarela, la distribución de fondos y la conciliación.
  - **RN-1731:** Las funcionalidades postergadas deberán conservar compatibilidad con las decisiones funcionales ya aprobadas.
</box>

## Organización inicial del producto

<box border radius="lg" padding={3} gap={3}>
  <row align="start" gap={3}>
    <icon name="rocket" color="success" size="lg"/>
    <box flex="1" gap={1}>
      **Versión 1.0 — MVP**

      Clubes, disciplinas, jugadores, categorías, cuotas, becas, pagos online, pagos externos, portal del responsable, reportes básicos y conciliación esencial.
    </box>
  </row>
  <divider color="subtle"/>
  <row align="start" gap={3}>
    <icon name="layers" color="secondary" size="lg"/>
    <box flex="1" gap={1}>
      **Versiones posteriores**

      Gestión avanzada de devoluciones y contracargos, automatizaciones, reportes detallados, exportaciones y otras herramientas operativas.
    </box>
  </row>
</box>

---

## Fase 5 — Lanzamiento y validación del MVP

<text color="secondary" weight="medium" size="xs">Pregunta 162 — ¿Cómo debería realizarse el primer lanzamiento de la plataforma?</text>

Tenemos a Obras Mendoza como club piloto, pero necesitamos decidir si comenzamos con una sola disciplina o habilitamos varias desde el principio.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Lanzamiento abierto desde el primer día**

      Permitir que distintos clubes y disciplinas comiencen a utilizar la plataforma apenas esté disponible el MVP.
    </radio>
    <radio value="b">
      **Opción B — Piloto exclusivo con Obras Futsal**

      Comenzar únicamente con Obras Futsal. Una vez validado el funcionamiento, habilitar otras disciplinas y clubes.
    </radio>
    <radio value="c">
      **Opción C — Piloto progresivo con Obras Futsal (recomendada)**

      Comenzar con Obras Futsal y un grupo controlado de jugadores y responsables.

      <text color="secondary" size="xs">Primero verificar generación de cuotas, acceso al portal, cobros, Split Payment y conciliación. Luego incorporar progresivamente al resto de Futsal, otras disciplinas de Obras y finalmente otros clubes. La arquitectura multi-club estará preparada desde el inicio, aunque el acceso comercial se habilite por etapas.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 162 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"abrir la plataforma a múltiples clubes y disciplinas desde el primer día",b:"realizar un piloto exclusivo con todo Obras Futsal antes de incorporar otros clubes y disciplinas",c:"realizar un piloto progresivo comenzando con un grupo controlado de Obras Futsal y ampliando por etapas a otras disciplinas y clubes"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 161 decisiones aprobadas. Aproximadamente 9 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-162

### Registro 401 — Estrategia de lanzamiento progresivo
Fuente: `7d98917a-6ac0-4d4d-9aee-45444e97854e`

Respuesta previa del usuario:

Para la pregunta 162 del relevamiento SDD elijo la opción C: realizar un piloto progresivo comenzando con un grupo controlado de Obras Futsal y ampliando por etapas a otras disciplinas y clubes.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 162 — Confirmada: opción C**
</row>

El lanzamiento será **progresivo**, comenzando con un grupo controlado de jugadores y responsables de Obras Futsal.

Una vez comprobado que los cobros, la distribución de fondos y la conciliación funcionan correctamente, ampliaremos el acceso por etapas.

## Registro SDD — Decisión 162

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-162 — Estrategia de lanzamiento progresivo**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1732:** El lanzamiento inicial se realizará mediante un piloto controlado.
  - **RN-1733:** Obras Futsal será la primera disciplina habilitada.
  - **RN-1734:** El piloto comenzará con un grupo limitado de jugadores y responsables.
  - **RN-1735:** La plataforma deberá permitir habilitar progresivamente el acceso de nuevos participantes.
  - **RN-1736:** La arquitectura multi-club estará disponible desde el inicio, aunque el lanzamiento sea limitado.
  - **RN-1737:** El aislamiento de datos y operaciones entre clubes y disciplinas será obligatorio desde la primera versión.
  - **RN-1738:** Durante el piloto se verificará la generación correcta de cuotas.
  - **RN-1739:** Se comprobarán los accesos y permisos de administradores y responsables.
  - **RN-1740:** Se validará el funcionamiento de los pagos online y del Split Payment.
  - **RN-1741:** Se verificará que los fondos lleguen correctamente a las cuentas correspondientes.
  - **RN-1742:** Se comprobará la conciliación entre pagos, cuotas y operaciones de la pasarela.
  - **RN-1743:** Se evaluarán los controles contra cobros duplicados y pagos inciertos.
  - **RN-1744:** Los incidentes detectados durante el piloto deberán registrarse y analizarse.
  - **RN-1745:** La ampliación del piloto dependerá de la validación satisfactoria de las operaciones esenciales.
  - **RN-1746:** La segunda etapa incorporará progresivamente al resto de Obras Futsal.
  - **RN-1747:** Posteriormente podrán habilitarse otras disciplinas de Obras Mendoza.
  - **RN-1748:** La expansión comercial a otros clubes se realizará después de validar el funcionamiento del piloto.
  - **RN-1749:** La incorporación de nuevos participantes no deberá alterar los registros financieros existentes.
</box>

## Etapas del lanzamiento

<box border radius="lg" padding={3} gap={3}>
  <list marker="number" connector="solid" gap={3}>
    <list-item>
      **Piloto controlado — Obras Futsal**

      Grupo reducido de jugadores y responsables, con pruebas de cuotas, pagos reales controlados y conciliación.
    </list-item>
    <list-item>
      **Expansión — Obras Futsal**

      Incorporación progresiva del resto de jugadores y responsables de la disciplina.
    </list-item>
    <list-item>
      **Expansión — Obras Mendoza**

      Habilitación de otras disciplinas, cada una con su administración y configuración financiera independiente.
    </list-item>
    <list-item>
      **Lanzamiento comercial multi-club**

      Incorporación de nuevos clubes utilizando la misma plataforma y manteniendo el aislamiento de sus operaciones.
    </list-item>
  </list>
</box>

---

## Fase 5 — Criterios para avanzar entre etapas

<text color="secondary" weight="medium" size="xs">Pregunta 163 — ¿Quién debería decidir cuándo el piloto está listo para ampliarse?</text>

Por ejemplo, Obras Futsal ya procesó sus primeros pagos correctamente y quiere incorporar a todos sus jugadores.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Decisión del administrador de disciplina**

      Los administradores de Futsal deciden cuándo incorporar más jugadores, sin necesitar autorización de la plataforma.
    </radio>
    <radio value="b">
      **Opción B — Decisión exclusiva del superadministrador**

      El superadministrador decide cuándo avanzar, basándose en su evaluación del funcionamiento general.
    </radio>
    <radio value="c">
      **Opción C — Validaciones técnicas obligatorias y aprobación del superadministrador (recomendada)**

      Antes de ampliar el piloto, se deberán cumplir criterios mínimos: pagos y Split Payment verificados, conciliación sin diferencias críticas abiertas, seguridad operativa y ausencia de errores bloqueantes.

      <text color="secondary" size="xs">El sistema mostrará un resumen de estas verificaciones. El superadministrador autorizará la ampliación cuando se cumplan los criterios. Los administradores de Futsal podrán solicitar avanzar, pero no omitir los controles obligatorios. Toda decisión quedará registrada.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 163 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"permitir que administradores de disciplina decidan cuándo ampliar el piloto",b:"dejar la ampliación exclusivamente a criterio del superadministrador",c:"exigir validaciones técnicas y financieras mínimas antes de cada ampliación, con aprobación del superadministrador y auditoría"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 162 decisiones aprobadas. Aproximadamente 8 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-163

### Registro 403 — Autorización para ampliar el piloto
Fuente: `274acf46-016f-4c4d-a5e7-b711477cd522`

Respuesta previa del usuario:

Para la pregunta 163 del relevamiento SDD elijo la opción C: exigir validaciones técnicas y financieras mínimas antes de cada ampliación, con aprobación del superadministrador y auditoría.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 163 — Confirmada: opción C**
</row>

Cada ampliación del piloto requerirá **validaciones técnicas y financieras obligatorias**, además de la aprobación del superadministrador.

Los administradores de Futsal podrán solicitar avanzar a la siguiente etapa, pero no podrán omitir los controles de seguridad ni las verificaciones financieras.

## Registro SDD — Decisión 163

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-163 — Autorización para ampliar el piloto**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1750:** Cada ampliación del piloto requerirá una validación previa.
  - **RN-1751:** Los administradores de disciplina podrán solicitar una ampliación.
  - **RN-1752:** Únicamente el superadministrador podrá autorizar el avance entre etapas.
  - **RN-1753:** La aprobación estará condicionada al cumplimiento de criterios técnicos y financieros mínimos.
  - **RN-1754:** Se verificará que los pagos online se procesen correctamente.
  - **RN-1755:** Se comprobará la distribución de fondos mediante Split Payment.
  - **RN-1756:** No podrán existir diferencias críticas de conciliación sin resolver.
  - **RN-1757:** Se validarán los controles de autenticación y autorización.
  - **RN-1758:** Se comprobará el aislamiento entre clubes y disciplinas.
  - **RN-1759:** No deberán existir errores bloqueantes que comprometan las operaciones esenciales.
  - **RN-1760:** El sistema presentará el estado de las verificaciones requeridas.
  - **RN-1761:** Cada validación deberá conservar evidencia o referencias a los resultados correspondientes.
  - **RN-1762:** Si algún criterio obligatorio no se cumple, la ampliación quedará bloqueada.
  - **RN-1763:** Los administradores de disciplina no podrán omitir controles obligatorios.
  - **RN-1764:** Se registrarán el usuario que solicita la ampliación y el superadministrador que la aprueba.
  - **RN-1765:** Toda aprobación o rechazo deberá conservar fecha, motivo y resultado.
  - **RN-1766:** La ampliación no modificará pagos ni registros financieros anteriores.
  - **RN-1767:** Los resultados y decisiones quedarán disponibles para auditoría.
</box>

## Ejemplo — Validación antes de ampliar Futsal

<box border radius="lg" padding={3} gap={2}>
  <row justify="between" align="center">
    **Solicitud de ampliación**
    <badge>Etapa 1 → Etapa 2</badge>
  </row>
  <table>
    <table-row>
      <table-cell><icon name="check-circle-2" color="success" inline/> Pagos online verificados</table-cell>
      <table-cell align="end"><text color="success" weight="medium">Correcto</text></table-cell>
    </table-row>
    <table-row>
      <table-cell><icon name="check-circle-2" color="success" inline/> Split Payment validado</table-cell>
      <table-cell align="end"><text color="success" weight="medium">Correcto</text></table-cell>
    </table-row>
    <table-row>
      <table-cell><icon name="check-circle-2" color="success" inline/> Conciliación financiera</table-cell>
      <table-cell align="end"><text color="success" weight="medium">Correcto</text></table-cell>
    </table-row>
    <table-row>
      <table-cell><icon name="alert-circle" color="danger" inline/> Error bloqueante pendiente</table-cell>
      <table-cell align="end"><text color="danger" weight="medium">Sin resolver</text></table-cell>
    </table-row>
  </table>
  <row align="center" gap={2}>
    <icon name="lock" color="secondary"/>
    <text size="sm">La ampliación permanece bloqueada hasta resolver el error y completar las verificaciones.</text>
  </row>
  <caption>Ejemplo ilustrativo del control de lanzamiento, no representa resultados reales del piloto.</caption>
</box>

---

## Fase 5 — Configuración del entorno piloto

<text color="secondary" weight="medium" size="xs">Pregunta 164 — ¿Cómo deberíamos separar las pruebas del sistema de los pagos reales?</text>

Durante el desarrollo necesitaremos probar cuotas, cobros, errores de pasarela y devoluciones sin afectar el dinero ni los registros de los jugadores reales.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Un único entorno**

      Desarrollo, pruebas y operaciones reales utilizan la misma instalación, diferenciando los datos mediante usuarios y registros de prueba.
    </radio>
    <radio value="b">
      **Opción B — Desarrollo y producción separados**

      Se utiliza un entorno de desarrollo para programar y probar, y otro de producción para operar con jugadores y pagos reales.
    </radio>
    <radio value="c">
      **Opción C — Desarrollo, pruebas y producción independientes (recomendada)**

      Mantener tres entornos separados:

      <text color="secondary" size="sm">**Desarrollo:** programación y pruebas técnicas.</text>

      <text color="secondary" size="sm">**Testing/Staging:** validaciones integrales, pruebas de pasarela en modo sandbox y simulación del piloto.</text>

      <text color="secondary" size="sm">**Producción:** jugadores y pagos reales, con credenciales, bases de datos y configuraciones independientes.</text>

      <text color="secondary" size="xs">Los entornos no compartirán credenciales financieras ni permitirán que operaciones de prueba afecten cobros reales. La habilitación de producción requerirá verificaciones previas.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 164 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"utilizar un único entorno para desarrollo, pruebas y producción",b:"separar desarrollo y producción en dos entornos",c:"mantener entornos independientes de desarrollo, testing/staging y producción, con bases de datos, credenciales y operaciones financieras aisladas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 163 decisiones aprobadas. Aproximadamente 7 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-164

### Registro 405 — Separación de entornos
Fuente: `c384d4d6-ffd2-4606-9965-bc6da081c9ed`

Respuesta previa del usuario:

Para la pregunta 164 del relevamiento SDD elijo la opción B: separar desarrollo y producción en dos entornos.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 164 — Confirmada: opción B**
</row>

Trabajaremos con **dos entornos independientes: Desarrollo y Producción**. No implementaremos un tercer entorno de Testing/Staging durante el MVP.

Esto reduce los costos y la complejidad de infraestructura, manteniendo separados los datos y pagos reales de las pruebas.

## Registro SDD — Decisión 164

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-164 — Separación de entornos**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1768:** El MVP contará con dos entornos: Desarrollo y Producción.
  - **RN-1769:** Desarrollo se utilizará para programación, pruebas técnicas e integraciones.
  - **RN-1770:** Producción se utilizará para operaciones reales de clubes, disciplinas y responsables.
  - **RN-1771:** Cada entorno tendrá su propia base de datos.
  - **RN-1772:** Las configuraciones y credenciales serán independientes.
  - **RN-1773:** Desarrollo utilizará credenciales de prueba o sandbox para las pasarelas de pago.
  - **RN-1774:** Las credenciales financieras reales estarán disponibles únicamente en Producción.
  - **RN-1775:** No se permitirán cobros reales desde Desarrollo.
  - **RN-1776:** Los datos de prueba no se mezclarán con los registros financieros de Producción.
  - **RN-1777:** Los cambios deberán validarse antes de desplegarse en Producción.
  - **RN-1778:** Los despliegues a Producción deberán realizarse mediante un procedimiento controlado.
  - **RN-1779:** Se deberá poder identificar qué versión del sistema está desplegada en cada entorno.
  - **RN-1780:** Las pruebas financieras deberán evitar modificar operaciones reales.
  - **RN-1781:** No será obligatorio disponer de un entorno Staging independiente para el MVP.
  - **RN-1782:** La arquitectura podrá incorporar nuevos entornos en el futuro.
</box>

## Arquitectura de entornos

<grid columns={2} gap={3}>
  <grid-item>
    <box border radius="lg" padding={3} gap={2}>
      <row align="center" gap={2}>
        <icon name="code-2" color="secondary" size="lg"/>
        **Desarrollo**
      </row>
      <badge>DEV</badge>
      <text size="sm">Backend, frontend, base de datos de prueba y pasarela sandbox.</text>
      <text size="xs" color="secondary">Sin dinero real.</text>
    </box>
  </grid-item>
  <grid-item>
    <box border radius="lg" padding={3} gap={2}>
      <row align="center" gap={2}>
        <icon name="server" color="secondary" size="lg"/>
        **Producción**
      </row>
      <badge color="success">PROD</badge>
      <text size="sm">Backend, frontend, base de datos real y pasarela productiva.</text>
      <text size="xs" color="secondary">Operaciones financieras reales.</text>
    </box>
  </grid-item>
</grid>

---

## Fase 5 — Despliegues y actualizaciones

<text color="secondary" weight="medium" size="xs">Pregunta 165 — ¿Cómo deberían realizarse las actualizaciones del sistema en Producción?</text>

Una vez que Obras Futsal esté cobrando cuotas reales, necesitaremos publicar nuevas versiones sin comprometer los pagos ni los datos existentes.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Despliegues manuales**

      El desarrollador actualiza Producción manualmente cuando termina una funcionalidad, realizando las verificaciones necesarias.
    </radio>
    <radio value="b">
      **Opción B — Despliegue automático desde la rama principal**

      Cada cambio integrado a la rama principal y que supere las pruebas automáticas se publica directamente en Producción.
    </radio>
    <radio value="c">
      **Opción C — CI/CD con aprobación para Producción (recomendada)**

      Los cambios se prueban automáticamente al integrarse. Para publicar en Producción se requiere una aprobación explícita del responsable técnico.

      <text color="secondary" size="xs">El proceso incluirá validaciones, control de migraciones de base de datos, registro de versiones y un procedimiento de recuperación ante fallas. Las actualizaciones no deberán interrumpir operaciones financieras en curso ni provocar duplicaciones de pagos.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 165 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"realizar despliegues manuales controlados a producción",b:"desplegar automáticamente a producción cada cambio validado de la rama principal",c:"implementar CI/CD con pruebas automáticas y aprobación explícita antes de desplegar a producción, con controles financieros y recuperación ante fallas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 164 decisiones aprobadas. Aproximadamente 6 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-165

### Registro 407 — Despliegues controlados mediante CI/CD
Fuente: `ceffca5e-696f-4211-9074-ab366c96e4ca`

Respuesta previa del usuario:

Para la pregunta 165 del relevamiento SDD elijo la opción C: implementar CI/CD con pruebas automáticas y aprobación explícita antes de desplegar a producción, con controles financieros y recuperación ante fallas.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 165 — Confirmada: opción C**
</row>

Implementaremos **CI/CD con pruebas automáticas y aprobación manual antes de publicar en Producción**.

Esto permitirá automatizar las verificaciones sin que cada cambio de código termine directamente en el sistema que utilizan los clubes.

## Registro SDD — Decisión 165

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-165 — Despliegues controlados mediante CI/CD**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1783:** El proyecto utilizará un proceso de integración y despliegue continuo (CI/CD).
  - **RN-1784:** Cada cambio integrado deberá pasar por verificaciones automáticas.
  - **RN-1785:** El pipeline deberá compilar y validar los componentes afectados.
  - **RN-1786:** Se ejecutarán pruebas automáticas antes de habilitar un despliegue productivo.
  - **RN-1787:** Las pruebas deberán contemplar reglas críticas de cuotas, pagos, permisos y aislamiento.
  - **RN-1788:** Ningún cambio se desplegará automáticamente en Producción sin aprobación explícita.
  - **RN-1789:** La aprobación corresponderá al responsable técnico autorizado.
  - **RN-1790:** Los despliegues deberán utilizar configuraciones y credenciales propias del entorno.
  - **RN-1791:** Las migraciones de base de datos deberán validarse antes de aplicarse.
  - **RN-1792:** Se deberá evitar la pérdida o alteración indebida de registros financieros durante una migración.
  - **RN-1793:** Cada despliegue identificará la versión publicada y los cambios incluidos.
  - **RN-1794:** Se registrarán el responsable de la aprobación, la fecha y el resultado.
  - **RN-1795:** Existirá un procedimiento documentado de recuperación ante fallas.
  - **RN-1796:** La recuperación deberá considerar la compatibilidad de los cambios de base de datos.
  - **RN-1797:** Los despliegues no deberán generar cobros duplicados ni ejecutar nuevamente operaciones financieras ya procesadas.
  - **RN-1798:** Las operaciones de pago en curso deberán conservar su trazabilidad y poder reconciliarse después de un despliegue.
  - **RN-1799:** Si falla una verificación obligatoria, se bloqueará la publicación.
  - **RN-1800:** El proceso respetará los dos entornos aprobados: Desarrollo y Producción.
</box>

## Flujo de publicación

<box border radius="lg" padding={3} gap={2}>
  <list marker="number" connector="solid" gap={2}>
    <list-item>**Desarrollo:** se implementa una nueva funcionalidad.</list-item>
    <list-item>**CI:** se compila y se ejecutan pruebas automáticas.</list-item>
    <list-item>**Validación:** se comprueban migraciones y controles financieros.</list-item>
    <list-item>**Aprobación:** el responsable técnico autoriza la publicación.</list-item>
    <list-item>**Producción:** se despliega la versión y se verifica su funcionamiento.</list-item>
    <list-item>**Recuperación:** si aparece una falla crítica, se aplica el procedimiento correspondiente.</list-item>
  </list>
</box>

---

## Fase 5 — Seguridad y continuidad operativa

<text color="secondary" weight="medium" size="xs">Pregunta 166 — ¿Cómo deberíamos realizar las copias de seguridad de la base de datos de Producción?</text>

La plataforma manejará jugadores, cuotas, pagos, comprobantes y registros financieros. Necesitamos definir cómo proteger esa información ante una falla de infraestructura.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Backup diario**

      Realizar una copia automática completa cada 24 horas, conservando las últimas 7 copias.
    </radio>
    <radio value="b">
      **Opción B — Backup diario con mayor retención**

      Realizar copias automáticas diarias y conservarlas durante 30 días, con restauración manual ante incidentes.
    </radio>
    <radio value="c">
      **Opción C — Backups automáticos con recuperación a un punto en el tiempo (recomendada)**

      Utilizar backups completos y registros de transacciones para recuperar la base de datos hasta un momento cercano a la falla, según las capacidades de la infraestructura elegida.

      <text color="secondary" size="xs">Las copias estarán cifradas, aisladas del entorno productivo y sujetas a una política de retención. Se realizarán pruebas periódicas de restauración y, después de recuperar el sistema, se reconciliarán los pagos con la pasarela para evitar inconsistencias o cobros duplicados.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 166 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"realizar backups completos diarios con retención de siete copias",b:"realizar backups diarios con retención de treinta días y restauración manual",c:"implementar backups automáticos cifrados con recuperación a un punto en el tiempo, pruebas de restauración y reconciliación financiera posterior"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 165 decisiones aprobadas. Aproximadamente 5 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-166

### Registro 409 — Backups y recuperación de datos
Fuente: `3bec309c-bfde-48f7-98f4-b408221a6730`

Respuesta previa del usuario:

Para la pregunta 166 del relevamiento SDD elijo la opción C: implementar backups automáticos cifrados con recuperación a un punto en el tiempo, pruebas de restauración y reconciliación financiera posterior.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 166 — Confirmada: opción C**
</row>

La plataforma tendrá **backups automáticos cifrados, recuperación a un punto en el tiempo (PITR) y pruebas periódicas de restauración**.

Además, después de recuperar la base de datos, será obligatorio reconciliar las operaciones con la pasarela para evitar inconsistencias financieras.

## Registro SDD — Decisión 166

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-166 — Backups y recuperación de datos**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1801:** La base de datos de Producción contará con backups automáticos.
  - **RN-1802:** Se implementará recuperación a un punto en el tiempo (PITR), según las capacidades de la infraestructura seleccionada.
  - **RN-1803:** Se utilizarán copias completas y registros de transacciones, o mecanismos equivalentes del proveedor.
  - **RN-1804:** Los backups deberán estar cifrados.
  - **RN-1805:** Las copias se almacenarán de manera aislada del entorno productivo.
  - **RN-1806:** El acceso a las copias estará restringido a personal autorizado.
  - **RN-1807:** Se establecerá una política de conservación de backups.
  - **RN-1808:** Se supervisará la ejecución de las copias automáticas.
  - **RN-1809:** Los errores de backup deberán generar alertas operativas.
  - **RN-1810:** Se realizarán pruebas periódicas de restauración.
  - **RN-1811:** Las pruebas verificarán que la información recuperada sea consistente y utilizable.
  - **RN-1812:** Toda restauración productiva deberá quedar registrada y auditada.
  - **RN-1813:** Después de restaurar, se verificará la consistencia de cuotas, pagos y operaciones financieras.
  - **RN-1814:** Se reconciliarán los pagos con los registros reales de la pasarela.
  - **RN-1815:** Los pagos con resultado incierto no podrán volver a cobrarse sin verificar previamente su estado.
  - **RN-1816:** Se mantendrán los controles de idempotencia y prevención de duplicaciones.
  - **RN-1817:** La reanudación de operaciones financieras dependerá de la validación de consistencia.
  - **RN-1818:** El procedimiento de recuperación deberá estar documentado.
  - **RN-1819:** Las pruebas y restauraciones deberán respetar la confidencialidad de los datos de los clubes.
</box>

## Ejemplo de recuperación

<box border radius="lg" padding={3} gap={2}>
  <list marker="number" connector="solid" gap={2}>
    <list-item>**14:00:** la plataforma funciona normalmente y procesa pagos.</list-item>
    <list-item>**14:37:** ocurre una falla grave en la base de datos.</list-item>
    <list-item>**Recuperación:** se restaura hasta el último punto consistente disponible, por ejemplo, las 14:36.</list-item>
    <list-item>**Conciliación:** se consulta la pasarela para detectar pagos confirmados que no quedaron registrados en la base restaurada.</list-item>
    <list-item>**Reanudación:** se habilitan nuevamente los cobros cuando las verificaciones críticas resultan satisfactorias.</list-item>
  </list>
  <caption>El punto de recuperación real dependerá de la infraestructura contratada y de su configuración.</caption>
</box>

---

## Fase 5 — Política de conservación de backups

<text color="secondary" weight="medium" size="xs">Pregunta 167 — ¿Durante cuánto tiempo deberíamos conservar los backups de Producción?</text>

El tiempo de conservación afecta directamente el costo de infraestructura y la capacidad de recuperar información histórica.

{@body const [opcion,setOpcion] = DIL.useState("b")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — 7 días**

      Conservar los puntos de recuperación de los últimos 7 días. Es más económico, pero ofrece una ventana de recuperación histórica reducida.
    </radio>
    <radio value="b">
      **Opción B — 30 días más copias mensuales (recomendada)**

      Mantener una ventana de recuperación a un punto en el tiempo de hasta 30 días y conservar adicionalmente copias mensuales durante 12 meses.

      <text color="secondary" size="xs">Si el proveedor no ofrece PITR de 30 días, se deberá contratar o implementar un mecanismo equivalente antes de considerar cumplido el requisito. Las copias mensuales estarán cifradas y sujetas a pruebas de restauración. Esta política no reemplaza la conservación legal de registros financieros.</text>
    </radio>
    <radio value="c">
      **Opción C — Configurable por el superadministrador**

      El superadministrador podrá configurar la ventana de recuperación y la conservación de copias históricas, dentro de los límites técnicos y legales, con auditoría de los cambios.
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 167 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"conservar backups y puntos de recuperación durante siete días",b:"mantener PITR de hasta treinta días y copias mensuales cifradas durante doce meses",c:"permitir que el superadministrador configure la retención de backups y puntos de recuperación dentro de límites técnicos y legales"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 166 decisiones aprobadas. Aproximadamente 4 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-167

### Registro 411 — Retención de backups de Producción
Fuente: `cdcbf711-6662-404d-8bd8-ea46785f5f15`

Respuesta previa del usuario:

Para la pregunta 167 del relevamiento SDD elijo la opción B: mantener PITR de hasta treinta días y copias mensuales cifradas durante doce meses.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 167 — Confirmada: opción B**
</row>

Definimos una política de recuperación con **30 días de PITR y 12 meses de copias mensuales cifradas**.

Esto permitirá recuperar información reciente con precisión y conservar puntos históricos de restauración durante un año.

## Registro SDD — Decisión 167

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-167 — Retención de backups de Producción**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1820:** La plataforma mantendrá una ventana objetivo de recuperación a un punto en el tiempo de 30 días.
  - **RN-1821:** Se conservarán copias mensuales durante 12 meses.
  - **RN-1822:** Los backups mensuales serán independientes de la ventana de recuperación PITR.
  - **RN-1823:** Todas las copias deberán permanecer cifradas.
  - **RN-1824:** El almacenamiento estará aislado del entorno productivo.
  - **RN-1825:** El acceso estará limitado a personal técnico autorizado.
  - **RN-1826:** Se verificará que el proveedor de infraestructura soporte los períodos definidos.
  - **RN-1827:** Si el proveedor no ofrece PITR de 30 días, deberá implementarse una solución equivalente antes de considerar cumplido el requisito.
  - **RN-1828:** Se comprobará periódicamente la integridad de los backups.
  - **RN-1829:** Se realizarán pruebas de restauración que incluyan puntos recientes y copias mensuales.
  - **RN-1830:** Los errores de generación o conservación deberán generar alertas operativas.
  - **RN-1831:** Las copias vencidas se eliminarán de forma segura, salvo obligaciones legales o medidas de preservación aplicables.
  - **RN-1832:** La creación, restauración y eliminación de backups deberá conservar trazabilidad operativa.
  - **RN-1833:** La política de backups no reemplazará la conservación legal de comprobantes, operaciones financieras ni auditorías.
  - **RN-1834:** La restauración de una copia histórica no permitirá omitir la conciliación financiera posterior definida en DEC-166.
</box>

## Política aprobada

<grid columns={2} gap={3}>
  <grid-item>
    <box border radius="lg" padding={3} gap={1}>
      <text color="secondary" weight="medium" size="xs">Recuperación PITR</text>
      <title size="2xl" color="default" tabularNums>30 días</title>
      <text color="secondary" size="xs">Recuperación a puntos recientes</text>
    </box>
  </grid-item>
  <grid-item>
    <box border radius="lg" padding={3} gap={1}>
      <text color="secondary" weight="medium" size="xs">Copias mensuales</text>
      <title size="2xl" color="default" tabularNums>12 meses</title>
      <text color="secondary" size="xs">Conservación histórica cifrada</text>
    </box>
  </grid-item>
</grid>

---

## Fase 5 — Monitoreo de Producción

<text color="secondary" weight="medium" size="xs">Pregunta 168 — ¿Cómo deberíamos detectar y comunicar los problemas técnicos de la plataforma?</text>

Por ejemplo, un viernes por la noche deja de responder la pasarela, falla la generación de cuotas o la base de datos alcanza un límite crítico.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Monitoreo manual**

      El equipo técnico revisa periódicamente los registros y el funcionamiento del sistema. Los usuarios pueden informar problemas.
    </radio>
    <radio value="b">
      **Opción B — Monitoreo automático con alertas por correo**

      Supervisar disponibilidad, errores, base de datos y operaciones financieras críticas. Enviar alertas automáticas por correo al responsable técnico.
    </radio>
    <radio value="c">
      **Opción C — Monitoreo integral con alertas según gravedad (recomendada)**

      Supervisar automáticamente servicios, API, base de datos, backups, pasarelas, colas y procesos de cuotas y conciliación.

      <text color="secondary" size="xs">Los incidentes se clasificarán por gravedad. Los críticos generarán avisos inmediatos al equipo técnico por canales configurados; los menores quedarán registrados para seguimiento. Se evitarán alertas duplicadas y se conservará el historial de incidentes. Las notificaciones operativas a administradores respetarán las reglas ya aprobadas para cada tipo de incidente.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 168 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"utilizar monitoreo manual y reportes de usuarios",b:"implementar monitoreo automático con alertas por correo al responsable técnico",c:"implementar monitoreo integral con clasificación de gravedad, alertas inmediatas para incidentes críticos, deduplicación e historial, respetando las notificaciones operativas ya definidas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 167 decisiones aprobadas. Aproximadamente 3 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-168

### Registro 414 — Monitoreo automático y alertas técnicas
Fuente: `9311945a-6ebe-4194-91e7-16f970fc9dd1`

Respuesta previa del usuario:

Para la pregunta 168 del relevamiento SDD elijo la opción B: implementar monitoreo automático con alertas por correo al responsable técnico.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 168 — Confirmada: opción B**
</row>

Implementaremos **monitoreo automático de Producción con alertas por correo electrónico al responsable técnico**.

No será necesario desarrollar inicialmente un sistema integral de alertas multicanal y clasificación avanzada de incidentes.

## Registro SDD — Decisión 168

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-168 — Monitoreo automático y alertas técnicas**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1835:** El entorno de Producción contará con monitoreo automático.
  - **RN-1836:** Se supervisará la disponibilidad de la API y los servicios esenciales.
  - **RN-1837:** Se detectarán errores técnicos que afecten el funcionamiento de la plataforma.
  - **RN-1838:** Se monitoreará la disponibilidad y el estado operativo de la base de datos.
  - **RN-1839:** Se supervisarán fallas en la generación automática de cuotas.
  - **RN-1840:** Se detectarán errores críticos de procesamiento de pagos.
  - **RN-1841:** Se supervisarán fallas en los procesos esenciales de conciliación.
  - **RN-1842:** Se detectarán fallas en la ejecución de backups automáticos.
  - **RN-1843:** Los problemas detectados generarán alertas automáticas por correo electrónico al responsable técnico.
  - **RN-1844:** Las direcciones de los destinatarios técnicos deberán mantenerse configuradas y actualizadas.
  - **RN-1845:** Las alertas incluirán información suficiente para identificar el servicio, problema y momento de detección.
  - **RN-1846:** Los correos no deberán exponer contraseñas, tokens ni datos financieros sensibles.
  - **RN-1847:** Se registrarán los incidentes técnicos detectados para facilitar su diagnóstico.
  - **RN-1848:** Se evitará el envío repetitivo de alertas idénticas mientras persista un mismo incidente, sin impedir avisos de fallas nuevas.
  - **RN-1849:** Se conservará el registro de recuperación o resolución de los incidentes detectados.
  - **RN-1850:** Las alertas técnicas por correo no reemplazarán las notificaciones operativas a administradores y superadministradores definidas previamente.
  - **RN-1851:** No será obligatorio implementar canales adicionales ni clasificación avanzada de incidentes durante el MVP.
</box>

## Ejemplo de alerta técnica

<box border radius="lg" padding={3} gap={2}>
  <row align="center" gap={2}>
    <icon name="mail" color="secondary" size="lg"/>
    **Correo automático**
  </row>
  <divider color="subtle"/>
  <text size="sm">**Asunto:** Alerta de Producción — Error en conciliación</text>
  <text size="sm">**Servicio:** Procesador de conciliación</text>
  <text size="sm">**Entorno:** Producción</text>
  <text size="sm">**Problema:** Falló una ejecución programada.</text>
  <text size="sm">**Acción:** Revisar registros técnicos y estado de operaciones pendientes.</text>
  <caption>Ejemplo ilustrativo. No corresponde a un incidente real.</caption>
</box>

---

## Fase 5 — Preparación para el lanzamiento

<text color="secondary" weight="medium" size="xs">Pregunta 169 — ¿Cómo deberíamos incorporar los jugadores y las cuotas existentes de Obras Futsal al comenzar el piloto?</text>

Actualmente el club podría tener jugadores registrados en planillas, cuotas pendientes y pagos realizados fuera de la plataforma. Necesitamos definir qué información inicial se migrará.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Comenzar desde cero**

      Importar los jugadores, pero generar y administrar únicamente cuotas correspondientes a los períodos posteriores al lanzamiento. Las deudas anteriores permanecen fuera del sistema.
    </radio>
    <radio value="b">
      **Opción B — Importar jugadores y deudas pendientes**

      Incorporar jugadores, categorías y cuotas anteriores que sigan impagas. Los pagos históricos quedan fuera de la plataforma.
    </radio>
    <radio value="c">
      **Opción C — Migración inicial controlada (recomendada)**

      Importar jugadores, categorías, beneficios vigentes, cuotas pendientes y, cuando existan datos confiables, pagos históricos externos.

      <text color="secondary" size="xs">Antes de confirmar la migración, los administradores revisarán un resumen con registros válidos, duplicados y errores. Los pagos históricos se identificarán como operaciones externas anteriores al lanzamiento, sin simular transacciones de pasarela ni generar la comisión del 5%. La importación deberá ser idempotente y conservar trazabilidad.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 169 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"importar jugadores y comenzar con cuotas nuevas, sin migrar deudas anteriores",b:"importar jugadores, categorías y deudas pendientes, sin pagos históricos",c:"realizar una migración inicial controlada de jugadores, categorías, beneficios, deudas y pagos externos históricos confiables, con validación, auditoría y sin comisiones retroactivas"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 168 decisiones aprobadas. Aproximadamente 2 preguntas restantes hasta el objetivo de 170.</caption>
```

## DEC-169

### Registro 417 — Migración inicial de datos
Fuente: `d399f8ac-1aae-485e-898e-7d5ff26de860`

Respuesta previa del usuario:

Para la pregunta 169 del relevamiento SDD elijo la opción C: realizar una migración inicial controlada de jugadores, categorías, beneficios, deudas y pagos externos históricos confiables, con validación, auditoría y sin comisiones retroactivas.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 169 — Confirmada: opción C**
</row>

Realizaremos una **migración inicial controlada**, incorporando jugadores, categorías, beneficios, deudas pendientes y pagos externos históricos confiables.

Los administradores deberán revisar y aprobar la información antes de incorporarla definitivamente. Los pagos anteriores al lanzamiento no generarán comisiones retroactivas del 5%.

## Registro SDD — Decisión 169

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-169 — Migración inicial de datos**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1852:** El MVP permitirá realizar una migración inicial controlada de información.
  - **RN-1853:** Se podrán importar jugadores y sus datos de identificación.
  - **RN-1854:** Se podrán importar categorías e inscripciones correspondientes a la disciplina.
  - **RN-1855:** Se podrán incorporar becas y beneficios económicos vigentes.
  - **RN-1856:** Se podrán importar cuotas anteriores que continúen pendientes de pago.
  - **RN-1857:** Se podrán incorporar pagos externos históricos cuando exista información suficientemente confiable.
  - **RN-1858:** Los pagos históricos conservarán su fecha y medio de pago original cuando estén disponibles.
  - **RN-1859:** Los pagos anteriores al lanzamiento se identificarán como registros históricos externos.
  - **RN-1860:** No se crearán transacciones ficticias en la pasarela para representar pagos históricos.
  - **RN-1861:** Los pagos históricos importados no generarán comisiones retroactivas del 5%.
  - **RN-1862:** Antes de confirmar la migración se realizará una validación de los datos.
  - **RN-1863:** El sistema identificará registros válidos, duplicados y registros con errores.
  - **RN-1864:** Los administradores autorizados podrán revisar el resultado de la validación.
  - **RN-1865:** La incorporación definitiva requerirá aprobación de un administrador autorizado dentro de su ámbito.
  - **RN-1866:** Los registros con errores deberán corregirse o excluirse antes de incorporarlos.
  - **RN-1867:** La importación será idempotente para evitar duplicaciones al reintentar un proceso.
  - **RN-1868:** La migración respetará el aislamiento entre clubes y disciplinas.
  - **RN-1869:** Se conservará trazabilidad de los datos importados, su origen y el responsable de la operación.
  - **RN-1870:** Se verificará la consistencia de los saldos pendientes después de la migración.
  - **RN-1871:** Los registros financieros históricos no podrán sobrescribir operaciones reales previamente confirmadas.
</box>

## Ejemplo — Migración de Obras Futsal

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **Vista previa de importación**
    <badge>Ejemplo</badge>
  </row>
  <grid columns={3} gap={2}>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} align="center" gap={1}>
        <title size="xl" color="default" tabularNums>120</title>
        <text size="2xs" color="secondary">Válidos</text>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} align="center" gap={1}>
        <title size="xl" color="default" tabularNums>4</title>
        <text size="2xs" color="secondary">Duplicados</text>
      </box>
    </grid-item>
    <grid-item>
      <box background="surface-secondary" radius="md" padding={2} align="center" gap={1}>
        <title size="xl" color="default" tabularNums>2</title>
        <text size="2xs" color="secondary">Con errores</text>
      </box>
    </grid-item>
  </grid>
  <text size="sm">El administrador revisa los registros, corrige las inconsistencias y confirma la importación.</text>
  <text color="secondary" size="xs">Los importes históricos se conservan según los registros originales, sin generar cobros nuevos ni comisiones de la plataforma.</text>
</box>

---

## Fase 5 — Cierre del relevamiento funcional

<text color="secondary" weight="medium" size="xs">Pregunta 170 — ¿Qué condiciones deberían cumplirse para considerar que el MVP está listo para comenzar el piloto con pagos reales?</text>

Esta decisión definirá el criterio final de aceptación del producto antes de habilitar Obras Futsal en Producción.

{@body const [opcion,setOpcion] = DIL.useState("c")}
<box gap={3}>
  <radio-group direction="col" value={opcion} onChange={setOpcion}>
    <radio value="a">
      **Opción A — Aprobación funcional**

      El MVP podrá lanzarse cuando las funcionalidades principales estén terminadas y el responsable técnico confirme que funcionan.
    </radio>
    <radio value="b">
      **Opción B — Pruebas funcionales y financieras**

      El lanzamiento requerirá pruebas exitosas de jugadores, cuotas, portal, pagos online, Split Payment y conciliación, con aprobación del responsable técnico.
    </radio>
    <radio value="c">
      **Opción C — Checklist integral de salida a Producción (recomendada)**

      Antes de comenzar a cobrar dinero real deberán cumplirse todas las condiciones críticas:

      - Funcionalidades esenciales del MVP verificadas.
      - Permisos y aislamiento entre clubes y disciplinas probados.
      - Split Payment validado con la pasarela real y sus condiciones comerciales.
      - Pruebas de cobros, idempotencia, pagos inciertos y conciliación satisfactorias.
      - Migración inicial revisada y aprobada.
      - Backups, recuperación y monitoreo configurados y verificados.
      - Procedimientos operativos para incidentes, devoluciones y contracargos documentados.
      - Aprobación formal del responsable técnico y habilitación del superadministrador.

      <text color="secondary" size="xs">Si falla una condición crítica, no se habilitan pagos reales. Los pendientes no críticos podrán documentarse y planificarse para versiones posteriores.</text>
    </radio>
  </radio-group>
  <button block onClick={()=>GenUI.issueNewTurn(`Para la pregunta 170 del relevamiento SDD elijo la opción ${opcion.toUpperCase()}: ${{a:"habilitar el piloto cuando estén terminadas las funcionalidades principales y el responsable técnico las apruebe",b:"exigir pruebas funcionales y financieras satisfactorias y aprobación técnica",c:"exigir un checklist integral de salida a Producción con seguridad, Split Payment validado, pruebas financieras, migración, backups, monitoreo, procedimientos operativos y aprobaciones formales; bloquear cobros reales si falla un requisito crítico"}[opcion]}.`)}>Confirmar respuesta y continuar <icon name="arrow-right" inline/></button>
</box>

<caption>Avance: 169 decisiones aprobadas. La pregunta 170 permitirá cerrar esta etapa del relevamiento y preparar la documentación SDD.</caption>
```

## DEC-170

### Registro 419 — Criterios de aceptación del MVP
Fuente: `0d9e6281-fd00-4c30-9a66-90c6b3bdf3ad`

Respuesta previa del usuario:

Para la pregunta 170 del relevamiento SDD elijo la opción C: exigir un checklist integral de salida a Producción con seguridad, Split Payment validado, pruebas financieras, migración, backups, monitoreo, procedimientos operativos y aprobaciones formales; bloquear cobros reales si falla un requisito crítico.

Registro original:

```text
<row align="center" gap={2}>
  <icon name="check-circle-2" color="success" size="lg"/>
  **Pregunta 170 — Confirmada: opción C**
</row>

Queda aprobado que **ningún club podrá comenzar a procesar pagos reales hasta superar un checklist integral de salida a Producción**.

Esta decisión cierra la etapa de relevamiento funcional del proyecto.

## Registro SDD — Decisión 170

<box border radius="lg" padding={3} gap={2}>
  <row align="center" justify="between">
    **DEC-170 — Criterios de aceptación del MVP**
    <badge color="success">Aprobada</badge>
  </row>

  **Reglas de negocio:**

  - **RN-1872:** El MVP deberá superar un checklist de aceptación antes de habilitar cobros reales.
  - **RN-1873:** Todas las funcionalidades esenciales comprometidas para el MVP deberán estar implementadas y verificadas.
  - **RN-1874:** Se deberán validar autenticación, autorización y aislamiento entre clubes y disciplinas.
  - **RN-1875:** La integración de Split Payment deberá estar validada técnica, operativa y comercialmente con la pasarela seleccionada.
  - **RN-1876:** Se verificará la distribución correcta de fondos y comisiones, respetando las condiciones legales y contractuales aplicables.
  - **RN-1877:** Las pruebas de cobros, idempotencia y prevención de pagos duplicados deberán ser satisfactorias.
  - **RN-1878:** Se deberán probar los escenarios de pagos inciertos, reintentos y conciliación.
  - **RN-1879:** La migración inicial deberá estar validada y aprobada.
  - **RN-1880:** Los backups y mecanismos de recuperación deberán estar configurados y probados.
  - **RN-1881:** El monitoreo automático y las alertas técnicas deberán estar operativos.
  - **RN-1882:** Los procedimientos para incidentes financieros, devoluciones y contracargos deberán estar documentados y ser ejecutables.
  - **RN-1883:** El responsable técnico deberá aprobar formalmente la versión candidata.
  - **RN-1884:** El superadministrador deberá autorizar la habilitación del piloto.
  - **RN-1885:** Cualquier requisito crítico incumplido bloqueará la habilitación de nuevos cobros reales.
  - **RN-1886:** Los pendientes no críticos podrán aceptarse únicamente si están documentados, evaluados y autorizados.
  - **RN-1887:** El resultado del checklist deberá conservar evidencias y trazabilidad de las aprobaciones.
  - **RN-1888:** La aprobación inicial no reemplazará las validaciones necesarias para posteriores ampliaciones del piloto.
  - **RN-1889:** Si se detecta una condición crítica después de habilitar el piloto, se aplicarán los controles de suspensión de nuevas operaciones afectadas, preservando la conciliación de pagos anteriores.
</box>

## Checklist de salida a Producción

{@body const checks = ["Funcionalidades esenciales del MVP verificadas","Seguridad, roles y aislamiento validados","Split Payment y distribución de fondos validados","Pruebas financieras e idempotencia satisfactorias","Migración inicial revisada y aprobada","Backups y restauración verificados","Monitoreo y alertas operativos","Procedimientos de incidentes documentados","Aprobación formal del responsable técnico","Habilitación del superadministrador"]}
{@body const [marked,setMarked] = DIL.useState([])}
<box border radius="lg" padding={3} gap={3}>
  <row align="center" justify="between">
    **Control de preparación del piloto**
    <text color="secondary" size="xs" tabularNums>{marked.length} de {checks.length}</text>
  </row>
  <box background="surface-tertiary" radius="full" height="6px" clip>
    <box background="surface-inverted" radius="full" height="100%" width={`${marked.length/checks.length*100}%`}/>
  </box>
  {#each checks as item,i}
    <checkbox key={i} checked={marked.includes(i)} onChange={v=>setMarked(p=>v?[...p,i]:p.filter(x=>x!==i))} lineThrough={false}>{item}</checkbox>
  {/each}
  <divider color="subtle"/>
  <row align="center" gap={2}>
    <icon name={marked.length===checks.length?"check-circle-2":"lock"} color={marked.length===checks.length?"success":"secondary"}/>
    <text weight="medium" size="sm">{marked.length===checks.length?"Checklist marcado completo; pendiente validación formal de evidencias.":"Cobros reales bloqueados hasta verificar todos los requisitos críticos."}</text>
  </row>
  <caption>Lista interactiva de planificación. Marcar elementos no constituye aprobación real ni habilita operaciones.</caption>
</box>

---

## Relevamiento SDD finalizado

<box border radius="xl" padding={3} gap={3}>
  <grid columns={3} gap={2}>
    <grid-item>
      <box align="center" gap={1}>
        <title size="2xl" color="default" tabularNums>170</title>
        <text size="2xs" color="secondary" textAlign="center">Decisiones aprobadas</text>
      </box>
    </grid-item>
    <grid-item>
      <box align="center" gap={1}>
        <title size="2xl" color="default" tabularNums>1.889</title>
        <text size="2xs" color="secondary" textAlign="center">Reglas numeradas</text>
      </box>
    </grid-item>
    <grid-item>
      <box align="center" gap={1}>
        <title size="2xl" color="default" tabularNums>5</title>
        <text size="2xs" color="secondary" textAlign="center">Etapas de relevamiento</text>
      </box>
    </grid-item>
  </grid>
  <divider color="subtle"/>
  El alcance funcional está definido para comenzar la siguiente fase de **Spec-Driven Development**. Esto no significa que el MVP esté desarrollado ni que las integraciones financieras hayan sido validadas todavía.
</box>

## Próximo paso: transformar las decisiones en especificaciones

Mi propuesta es generar una documentación SDD organizada, en lugar de un único documento enorme:

1. **PRD:** visión del producto, objetivos, actores, alcance del MVP y roadmap.
2. **Especificaciones funcionales:** clubes, disciplinas, jugadores, cuotas, becas, pagos y administración.
3. **Reglas de negocio y trazabilidad:** decisiones DEC y reglas RN organizadas por módulo.
4. **Arquitectura técnica:** componentes, modelo de datos, permisos, integraciones y seguridad.
5. **Plan de implementación:** funcionalidades ordenadas por dependencias, fases y criterios de aceptación.
6. **Plan de pruebas:** escenarios funcionales, financieros, operativos y condiciones para lanzar el piloto.

Antes de implementar, también tendremos que cerrar las dependencias técnicas pendientes, especialmente la pasarela que soporte el Split Payment requerido.

<box gap={2}>
  <button block onClick={()=>GenUI.issueNewTurn("Terminamos las 170 preguntas del relevamiento SDD. Quiero comenzar la consolidación formal de la documentación, empezando por el PRD y manteniendo trazabilidad con todas las decisiones DEC y reglas RN, sin inventar requisitos.")}>Comenzar documentación SDD <icon name="arrow-right" inline/></button>
  <button block variant="outline" color="secondary" onClick={()=>GenUI.issueNewTurn("Antes de generar la documentación SDD, quiero revisar las 170 decisiones aprobadas, detectar contradicciones y dependencias pendientes y definir cuáles deben resolverse antes de implementar.")}>Revisar decisiones y pendientes primero</button>
</box>
```

## Consolidación posterior

Consultar [CON.md](CON.md) para las 20 decisiones aprobadas y CON-020 descartada. Los registros originales se conservan como fuente histórica; las CON prevalecen dentro de su alcance.
