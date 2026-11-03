# Prueba técnica — Bowl of Tentacles

Prototipo de exploración y puzles en primera persona desarrollado en **Unity** como prueba técnica para **Bowl of Tentacles**. El jugador explora una vivienda, consulta pistas, resuelve un puzle para obtener una llave y trata de salir, mientras se desencadena un evento de tensión.

> **Inicio rápido:** el objetivo de esta guía es permitir abrir y ejecutar el proyecto en aproximadamente **15 minutos de intervención**, sin contar descargas ni instalaciones. El tiempo de importación inicial de Unity puede variar según el equipo. Si solo quieres jugar en Windows, utiliza directamente la build distribuida.

## 1. Requisitos y versión del proyecto

- **Unity Editor:** `6000.6.3f1` (revisión `45d8eee7de74`). La versión exacta está registrada en `ProjectSettings/ProjectVersion.txt`.
- **Unity Hub:** recomendado para instalar y seleccionar esa versión del Editor.
- **Para abrir el proyecto:** PC compatible con Unity 6; se recomienda teclado y ratón.
- **Para generar la build entregada:** módulo de compilación para **Windows** y destino **Windows x86_64**.
- **Para ejecutar la build incluida:** **Windows de 64 bits**. No es necesario instalar el Editor de Unity para jugar.

### Dependencias

Las dependencias se declaran en `Packages/manifest.json` y se fijan en `Packages/packages-lock.json`. Unity Package Manager debe resolverlas automáticamente al abrir el proyecto.

| Paquete | Versión | Uso principal |
| --- | --- | --- |
| `com.unity.render-pipelines.universal` | `17.6.0` | Renderizado URP |
| `com.unity.inputsystem` | `1.20.1` | Entrada de teclado y ratón |
| `com.unity.ugui` | `2.6.0` | Interfaces de usuario |
| `com.unity.probuilder` | `6.1.4` | Construcción y edición del escenario |
| `com.unity.timeline` | `6.6.0` | Herramientas de secuenciación disponibles en el proyecto |
| `com.unity.ai.navigation` | `2.0.14` | Herramientas de navegación disponibles en el proyecto |
| `com.unity.visualscripting` | `1.9.12` | Paquete de Visual Scripting instalado |

Las dependencias del proyecto se encuentran definidas en `Packages/manifest.json` y se gestionan automáticamente mediante Unity Package Manager.

Todos los recursos necesarios para ejecutar el juego están incluidos en la carpeta `Assets/`, por lo que no es necesario importar recursos externos manualmente.

## 2. Inicio rápido

### Opción A — Ejecutar la build (sin Unity)

1. Obtén el **paquete de build** y extrae su contenido conservando la estructura de carpetas.
2. En Windows, abre `Build/PruebaTecnica_BowlOfTentacles.exe`.
3. Espera a que finalice el vídeo inicial o pulsa **Espacio** para omitirlo.
4. Juega con teclado y ratón (controles en el apartado 4).

**Importante:** el `.exe` no funciona de forma independiente: debe permanecer junto a `PruebaTecnica_BowlOfTentacles_Data/`, `UnityPlayer.dll` y las demás dependencias generadas. No traslades únicamente el ejecutable a otra carpeta.

### Opción B — Abrir y ejecutar el proyecto en Unity

1. Descarga o clona el repositorio (o descomprime el ZIP del **proyecto fuente**).
2. Abre **Unity Hub** → **Add / Add project from disk** y selecciona la carpeta raíz `PruebaTecnica_BowlOfTentacles/`, la que contiene `Assets/`, `Packages/` y `ProjectSettings/`.
3. Abre el proyecto con **Unity `6000.6.3f1`**. Permite que Unity importe los recursos y resuelva las dependencias.
4. En la ventana **Project**, abre `Assets/Scenes/Intro.unity`.
5. Pulsa **Play**. El vídeo de presentación dará paso a `Casa` automáticamente (o puedes omitirlo con **Espacio**).

> Para probar directamente la jugabilidad, abre `Assets/Scenes/Casa.unity` y pulsa **Play**. Esta opción **omite la introducción**.

## 3. Escenas y punto de entrada

| Orden en build | Escena | Función |
| --- | --- | --- |
| `0` | `Assets/Scenes/Intro.unity` | Vídeo de introducción y carga de la escena de juego |
| `1` | `Assets/Scenes/Casa.unity` | Escenario, interacción, puzle, evento de tensión y final |

`Assets/Scenes/SampleScene.unity` figura en la configuración global, pero está **desactivada**. Las demás escenas de trabajo no forman parte de la build principal. Esta configuración está en `ProjectSettings/EditorBuildSettings.asset`.

## 4. Controles

| Acción | Control |
| --- | --- |
| Moverse | **W A S D** o **flechas** |
| Mirar alrededor | **Ratón** |
| Interactuar / leer / recoger / abrir | **E** (apuntando al objeto) |
| Soltar el objeto transportado | **Q** |
| Cerrar una nota o la interfaz de la caja | **Esc** |
| Seleccionar símbolos y pulsar botones en la interfaz de la caja | **Clic izquierdo** |
| Omitir el vídeo de introducción | **Espacio** |

La acción de interacción tiene un alcance limitado y requiere apuntar hacia un objeto interactuable. **Esc** cierra interfaces; no se ha implementado como menú de pausa general. Aunque el archivo `InputSystem_Actions.inputactions` define más acciones, **saltar, esprintar y agacharse no están implementados en el controlador FPS utilizado**.

## 5. Cómo completar el recorrido

1. Explora la casa y **lee las notas** para descubrir las pistas.
2. Encuentra la **caja** e introduce la secuencia de símbolos sugerida por las pistas. Puedes borrar un intento mediante el botón correspondiente.
3. Al resolver el puzle, **recoge la llave**.
4. Regresa a la **puerta de salida** e interactúa con ella llevando la llave.
5. Observa la secuencia final. La pantalla final ofrece controles para **reiniciar** o **salir**.

El evento de tensión se activa durante la exploración en relación con una de las notas; no requiere ninguna tecla especial. Para conocer la solución del puzle y el recorrido esperado con mayor detalle, consulta [Diseño y validación](Docs/DisenoYvalidacion.docx).

## 6. Generar una build de Windows

1. Abre el proyecto con la versión indicada de Unity.
2. Ve a **File → Build Profiles**.
3. Selecciona **Windows** (o crea un perfil para Windows si lo necesitas) y actívalo mediante **Switch Profile** cuando proceda. Comprueba que la arquitectura de destino sea **x86_64 / 64-bit**.
4. Revisa la **Scene List**: deben estar **activadas y en este orden** `Intro` y `Casa`. `SampleScene` debe seguir desactivada. Si el perfil no sobrescribe la lista de escenas, se utilizará la lista global del proyecto.
5. Selecciona **Build** para generar los archivos, o **Build and Run** para generarlos y ejecutarlos.
6. Guarda el ejecutable con el nombre `PruebaTecnica_BowlOfTentacles.exe` en la carpeta **`Build/`** de la raíz del proyecto.
7. Comprueba la compilación abriendo el `.exe` y recorriendo la introducción, las interacciones, el puzle y la salida.

## 7. Limitaciones y problemas conocidos

- **Plataforma de entrega:** el ejecutable incluido es para **Windows x86_64**. No se proporciona una build de macOS, Linux o Web.
- **Entrada prevista:** la experiencia se ha preparado principalmente para **teclado y ratón**. No se documenta compatibilidad verificada con mando, aunque el asset de acciones contenga bindings adicionales.
- **Escena inicial:** ejecutar `Casa` directamente desde el Editor **omite el vídeo** de `Intro`.
- **Reinicio:** la opción de reinicio recarga la **escena activa (`Casa`)**; no vuelve a reproducir la introducción.
- **Acciones no utilizadas:** saltar, esprintar y agacharse están definidas en el asset de entrada, pero no tienen comportamiento en el controlador actual.

### Solución rápida de incidencias

- **Faltan paquetes o aparecen errores al abrir:** verifica la versión `6000.6.3f1`, la conexión necesaria para resolver paquetes y la presencia de `Packages/manifest.json` y `Packages/packages-lock.json`.
- **La build no abre o faltan archivos:** comprueba que has extraído **toda la carpeta** de la build, no solo el `.exe`.
- **La compilación abre una escena incorrecta:** revisa el orden y la activación de escenas en **File → Build Profiles**.

## 8. Índice de documentación

Los documentos complementarios se encuentran en [`Docs/`](Docs/):

| Documento | Contenido |
| --- | --- |
| [Mini preproducción](Docs/MiniPreproduccion.docx) | Planteamiento y alcance inicial del prototipo |
| [Mapa técnico](Docs/MapaTecnico.docx) | Arquitectura, componentes y relaciones entre sistemas |
| [Guía para añadir contenido](Docs/GuiaAnadirContenido.docx) | Criterios y pasos de ampliación del proyecto |
| [Diseño y validación](Docs/DisenoYvalidacion.docx) | Experiencia prevista, puzle y criterios de comprobación |
| [Memoria de ejecución](Docs/MemoriaDeEjecucion.docx) | Seguimiento del trabajo, decisiones y resultados |
| [Kanban inicial](Docs/KanbanVersionInicial.md) | Planificación inicial de tareas |

---

**Proyecto:** prueba técnica para Bowl of Tentacles · **Motor:** Unity `6000.6.3f1` · **Build incluida en el ZIP revisado:** Windows x86_64.
