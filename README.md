# La última visita a casa — Prueba técnica de Bowl of Tentacles

Experiencia breve de **terror narrativo en primera persona**, desarrollada con **Unity y C#** como prueba técnica para Bowl of Tentacles. El jugador regresa a una casa familiar, examina pistas, resuelve un puzle para recuperar una llave y trata de salir mientras sucede un acontecimiento inquietante.

El repositorio incluye el **proyecto de Unity**, una **build ejecutable para Windows** y la documentación de diseño, arquitectura, ampliación, organización y ejecución del trabajo.

> **Inicio rápido:** para jugar no necesitas instalar Unity. Si vas a revisar el código, el objetivo es poder abrir y ejecutar el proyecto siguiendo esta guía en unos 15 minutos de intervención, sin contar descargas e importación de recursos.

## 1. Requisitos y dependencias

- **Unity Editor:** `6000.6.3f1` (revisión `45d8eee7de74`), indicada en `ProjectSettings/ProjectVersion.txt`.
- **Para editar el proyecto:** Unity Hub y la versión de Unity anterior.
- **Para generar una build:** soporte de compilación para **Windows x86_64 (64 bits)**.
- **Para jugar a la build incluida:** Windows de 64 bits, teclado y ratón. No se necesita Unity Editor.

Los paquetes se especifican en `Packages/manifest.json` y `Packages/packages-lock.json`. Unity Package Manager los resolverá al abrir el proyecto. Entre los principales se encuentran:

- **Universal Render Pipeline (URP)** — `17.6.0`: renderizado e iluminación.
- **Input System** — `1.20.1`: teclado y ratón.
- **Unity UI (uGUI)** — `2.6.0`: interfaces de notas, caja y final.
- **ProBuilder** — `6.1.4`: construcción del escenario.

El proyecto también incluye otros paquetes declarados en el manifiesto, así como los recursos utilizados en `Assets/`. No es necesario importar manualmente modelos, fuentes o efectos para abrir la escena entregada.

## 2. Inicio rápido

### Opción A — Jugar en Windows, sin Unity

La versión ejecutable del videojuego está disponible en GitHub Releases.

1. Descargar el archivo PruebaTecnica_BowlOfTentacles_v1.0.0_Windows.zip.
2. Extraer todo el contenido del ZIP.
3. Ejecutar PruebaTecnica_BowlOfTentacles.exe.

ó

1. Descargar el archivo PruebaTecnica_BowlOfTentacles_v1.0.0_Windows.zip.
2. Abre la carpeta [`Build/`](Build/).
3. Ejecuta **`PruebaTecnica_BowlOfTentacles.exe`**.
4. Espera a que termine la introducción o pulsa **Espacio** para omitirla.

**Importante:** conserva juntos el `.exe`, `PruebaTecnica_BowlOfTentacles_Data/`, `UnityPlayer.dll` y los demás archivos de `Build/`. Copiar únicamente el ejecutable impide iniciar el juego.

### Opción B — Abrir el proyecto en Unity

1. Descarga o clona el repositorio.
2. En **Unity Hub**, selecciona **Add / Add project from disk** y elige la carpeta raíz, que contiene `Assets/`, `Packages/` y `ProjectSettings/`.
3. Ábrelo con **Unity `6000.6.3f1`** y espera a que termine la importación y resolución de paquetes.
4. Abre `Assets/Scenes/Intro.unity` y pulsa **Play**.
5. La introducción cargará automáticamente la escena `Casa`. También puedes omitir el vídeo con **Espacio**.

Para entrar directamente al recorrido jugable, abre `Assets/Scenes/Casa.unity` y pulsa **Play**. En este caso se omite la introducción audiovisual.

## 3. Escenas y estructura principal

Las escenas activadas en la lista de compilación, en este orden, son:

1. **`Assets/Scenes/Intro.unity`** — Secuencia audiovisual de presentación y transición al juego.
2. **`Assets/Scenes/Casa.unity`** — Exploración, interacción, pistas, puzle, evento de tensión y desenlace.

`SampleScene.unity` está desactivada y otras escenas de trabajo no forman parte de la build. La lista global se encuentra en `ProjectSettings/EditorBuildSettings.asset`.

Organización general:

- **`Assets/_Scripts/`** — Sistemas de interacción, jugador, puzles, managers y secuencias.
- **`Assets/_Prefabs/`** — Objetos reutilizables del jugador, escenario, caja, notas e interfaz.
- **`Assets/_Data/`** — Datos de notas y objetos mediante ScriptableObjects.
- **`Assets/_Models/`, `Assets/_Textures/`, `Assets/_Materials/`** — Recursos visuales.
- **`Assets/_Sounds/`, `Assets/_Sprites/`, `Assets/_Fonts/`** — Audio y recursos de interfaz.
- **`Assets/Scenes/`** — Escenas del proyecto.
- **`Docs/`** — Documentación del diseño, implementación y proceso.

Para las responsabilidades y relaciones entre componentes, consulta el [Mapa técnico](Docs/MapaTecnico.pdf).

## 4. Controles

- **W, A, S, D** o **flechas**: desplazarse.
- **Ratón**: mirar alrededor.
- **E**: interactuar, examinar, leer, recoger objetos o abrir la puerta (mirando al objeto y dentro del alcance).
- **Q**: soltar el objeto que llevas.
- **Esc**: cerrar una nota o la interfaz de la caja.
- **Clic izquierdo**: seleccionar botones y símbolos en las interfaces.
- **Espacio**: omitir el vídeo inicial.

`Esc` **no abre un menú de pausa general**. Aunque el Input System contiene acciones adicionales, el controlador de esta experiencia no implementa salto, esprint ni agacharse.

## 5. Recorrido de juego

1. Explora la casa y lee las notas para entender la rutina nocturna descrita en las pistas.
2. Examina la caja del salón e introduce la secuencia de tres símbolos. Puedes borrar un intento antes de completarlo y volver a probar si fallas.
3. Al abrirse la caja, **recoge la llave explícitamente**.
4. Regresa a la puerta de salida y úsala con la llave en tu inventario.
5. Observa el final y utiliza las opciones **Reiniciar** o **Salir**.

Durante la exploración se produce un evento de tensión asociado a una de las notas. No necesitas activar ninguna opción adicional. El recorrido completo, los textos y la solución del puzle se explican en [Diseño y validación](Docs/DisenoYvalidacion.pdf).

## 6. Generar la build de Windows

1. Abre el proyecto con Unity `6000.6.3f1`.
2. Ve a **File → Build Profiles**.
3. Selecciona el perfil **Windows**, cambia a ese perfil si procede y comprueba el destino **Windows x86_64**.
4. Revisa la lista de escenas: `Intro` debe estar antes de `Casa`, ambas activadas. `SampleScene` debe permanecer desactivada. El perfil entregado utiliza la lista global del proyecto.
5. Pulsa **Build** o **Build and Run**.
6. Selecciona una ubicación de salida y utiliza el nombre **`PruebaTecnica_BowlOfTentacles.exe`**. Para mantener la estructura documentada, genera la compilación en `Build/`.
7. Ejecuta el `.exe` obtenido, confirma que comienza en `Intro` y completa una partida para verificar la build.

Si se regenera la build dentro de la carpeta entregada, sustituye el conjunto completo de archivos generado por Unity, no únicamente el `.exe`.

## 7. Limitaciones y solución de problemas

**Limitaciones conocidas de la entrega:**

- La build suministrada está destinada a **Windows de 64 bits**; no se incluyen builds para macOS, Linux o navegador.
- La experiencia está diseñada para **teclado y ratón**; no se declara compatibilidad con mando comprobada.
- El inventario admite **un objeto a la vez**. La mecánica de soltar objetos es básica y no incorpora un sistema avanzado de colocación o físicas.
- El reinicio desde el panel final recarga la escena activa (`Casa`), **sin volver a reproducir `Intro`**.
- No hay menú de pausa general ni sistema de guardado de partidas.

**Si algo no funciona:**

- **El proyecto da errores al importar:** verifica la versión de Unity y que estén presentes `Packages/manifest.json` y `Packages/packages-lock.json`. Permite que Package Manager termine de resolver las dependencias.
- **La build no inicia:** conserva todos los archivos y subcarpetas de `Build/` junto al ejecutable.
- **Se abre una escena incorrecta:** revisa la lista y el orden de escenas en **Build Profiles**.
- **No puedes interactuar con algo:** apunta al objeto, acércate y utiliza **E**. Cierra previamente cualquier interfaz abierta con **Esc**.

Las pruebas del recorrido, los criterios de aceptación y los resultados del proceso se desarrollan en la documentación correspondiente. La build debe comprobarse de nuevo si se modifica el proyecto después de esta entrega.

## 8. Documentación entregada

La carpeta [`Docs/`](Docs/) contiene los siguientes archivos, con nombres y extensiones correspondientes a la entrega:

### Diseño y documentación técnica

- [Mini preproducción](Docs/MiniPreproduccion.pdf) — Alcance inicial, recorrido y planteamiento técnico.
- [Mapa técnico](Docs/MapaTecnico.pdf) — Organización de carpetas, componentes, comunicación y estados.
- [Guía para añadir contenido](Docs/GuiaAnadirContenido.pdf) — Pasos para crear interacciones, pistas, eventos y ampliar los puzles.
- [Diseño y validación](Docs/DisenoYvalidacion.pdf) — Narrativa, puzle, tensión, final, pruebas manuales y recursos externos.

### Planificación y ejecución

- [Organización de tareas](Docs/OrganizacionTareas.pdf) — Uso del tablero, prioridades, estimaciones y seguimiento.
- [Kanban inicial](Docs/KanbanVersionInicial.md) — Exportación de la planificación inicial.
- [Kanban final](Docs/KanbanVersionFinal.md) — Exportación del estado final de las tareas.
- [Memoria de ejecución](Docs/MemoriaDeEjecucion.pdf) — Trabajo por jornadas, decisiones, incidencias, pruebas, balance temporal y commits asociados a hitos.

La documentación de recursos de terceros y sus condiciones de uso se recoge en **Diseño y validación**. Los paquetes de Unity se identifican en `Packages/manifest.json`.

---

**Repositorio:** https://github.com/Jositopk/PruebaTecnica_BowlOfTentacles  
**Motor:** Unity `6000.6.3f1` · **Plataforma de la build:** Windows x86_64  
**Versión de entrega:** `v1.0.0`.
