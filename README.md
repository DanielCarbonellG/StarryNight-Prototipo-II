# StarryNight VR

**StarryNight VR** es una aplicación de Realidad Virtual diseñada para dispositivos Android con Google Cardboard. El proyecto explora los principios de las **Interfaces Multimodales**, permitiendo al usuario interactuar con un entorno nocturno mediante canales naturales: la mirada (atención visual), la voz/soplido (canal auditivo) y el movimiento físico (canal kinestésico).

<img width="1307" height="609" alt="StarryNight_20260114_181417" src="https://github.com/user-attachments/assets/31773949-b70b-403d-8344-39484f89013a" />

---

## Manual de Uso y Cuestiones Importantes

Para garantizar el correcto funcionamiento de la experiencia, el usuario debe tener en cuenta:

1. **Gestión de Permisos Críticos:**
* Al iniciar, la app solicitará acceso al **Micrófono** y al **Almacenamiento Externo**. Es importante aceptarlos; de lo contrario, la mecánica de "soplar para capturar" y el guardado en galería fallarán silenciosamente.


2. **Entorno Físico:**
* Se requiere un espacio libre de obstáculos para girar 360º (silla giratoria recomendada).
* El entorno debe ser moderadamente silencioso para evitar que el ruido ambiental active accidentalmente la cámara por el micrófono.


3. **Interacciones:**
* **Nubes:** Si la visión está bloqueada por nubes, agita la cabeza lateralmente ("No") con energía para dispersarlas.
* **Estrellas:** Pasa la mirada sobre una estrella para conectarla a la constelación.
* **Fotografía:** Sopla fuerte o emite un sonido alto para guardar una captura del cielo en el dispositivo.

---

## Hitos de Programación y Relación con Contenidos Impartidos

* **Realidad Virtual y Físicas:** Implementación del **Google Cardboard SDK** y uso de `SphereCast` (Raycasting avanzado) para la interacción por mirada (*Gaze*).
* **Gráficos 3D:** Generación procedural de constelaciones en tiempo real usando `LineRenderer`.
* **Arquitectura de Software:** Código mediante el **Patrón Observador** (Eventos y Delegados) en `GameEvents.cs`, demostrando dominio de *Scripts C#* y comunicación eficiente entre objetos.
* **Interfaces Multimodales (Sensores):**
    * **Micrófono:** Análisis del buffer de audio en tiempo real para detectar soplidos implementado en `PhotoMicSystem.cs`.
    * **Acelerómetro:** Detectar sacudidas (*Shake*) y limpiar el cielo (*Tema: Sensores*) mediante `ShakeDetector.cs`.
      
## Aspectos Destacados de la Aplicación

1.  **Interacción "Zero-Touch" (Manos Libres):**
    La aplicación elimina la necesidad de mandos físicos o toques en pantalla. Toda la interacción se realiza mediante interfaces naturales: **mirada** (selección), **movimiento físico** (agitar para limpiar el cielo) y **sonido** (soplar/aplaudir para capturar), logrando una inmersión total.

2.  **Uso Innovador del Micrófono (Procesamiento de Señal):**
    A diferencia del reconocimiento de voz estándar, se ha implementado un análisis del buffer de audio en tiempo real en `PhotoMicSystem.cs`. El sistema detecta umbrales de intensidad sonora, permitiendo mecánicas analógicas como un "soplido" o palmada para activar el disparador de la cámara.

3.  **Arquitectura de Software Desacoplada:**
    Se utiliza el **Patrón Observador** mediante una clase estática de eventos (`GameEvents.cs`). Esto desacopla la lógica de los sensores (Micrófono, Acelerómetro) de la lógica del juego (Constelaciones, UI), resultando en un código modular, limpio y mantenible.

4.  **Sistema de Mirada Asistida (Gaze Assist):**
    Para resolver la imprecisión del *head tracking* en móviles, se ha implementado `Physics.SphereCast` en lugar de raycasting simple. Esto crea un área de detección volumétrica que facilita la selección de objetos pequeños (estrellas) a larga distancia, reduciendo la frustración del usuario.

5.  **Integración Nativa Real:**
    El proyecto gestiona permisos de Android en tiempo de ejecución y conecta con el sistema de archivos del SO. Las capturas no se quedan en la carpeta de datos de la app, sino que se exportan automáticamente a la **Galería pública del teléfono** mediante integración nativa.
---

## Sensores Utilizados (Interfaces Multimodales)

La aplicación hace uso de los sensores integrados en el móvil, procesando los datos para convertirlos en interacciones:

| Sensor | Script | Procesamiento y Uso |
| --- | --- | --- |
| **Giroscopio / Acelerómetro (Fusión)** | *Google Cardboard Plugin* | **Head Tracking:** Procesa la orientación del dispositivo en cuaterniones para mover la cámara virtual (`TrackedPoseDriver`). |
| **Acelerómetro (Raw)** | `ShakeDetector.cs` | **Reconocimiento de Gestos:** Se monitorea la magnitud cuadrática (`sqrMagnitude`) del vector de aceleración lineal. Si supera un umbral de sensibilidad (`2.0f`) ignorando la gravedad, se dispara el evento "Shake" (agitar). |
| **Micrófono** | `PhotoMicSystem.cs` | **Detector de Umbral:** Se utiliza como sensor de intensidad sonora. No se emplea reconocimiento de voz (ASR), sino detección de picos de amplitud para simular un "disparador" por soplido. |

---


## Acta de Acuerdos del Grupo

El desarrollo se ha realizado dividiendo la implementación de sistemas clave y unificando el diseño final.

### Reparto de Tareas

**Daniel Carbonell de Chaves:**

* **Sistemas de Input Físico:** Implementación completa de `ShakeDetector.cs` y gestión del acelerómetro con el nuevo Input System.
* **Sistema de Audio y Permisos:** Desarrollo de `PhotoMicSystem.cs`, incluyendo el análisis de espectro de audio, gestión de permisos Android en tiempo de ejecución y la integración con la galería nativa.
* **Arquitectura:** Diseño del sistema de eventos (`GameEvents.cs`) para desacoplar los scripts.
* **Compilación:** Resolución de conflictos y configuración del Player Settings para Android.
* **Testing:** Testing en dispositivo y correccion de errores.

**Guillermo González Pineda:**

* **Interacción Visual (Gaze):** Desarrollo de `GazeInteraction.cs` utilizando `SphereCast` para mejorar la precisión y programación de la lógica de las miradas.
* **Lógica de Juego:** Implementación de `ConstellationManager.cs` para la conexión de estrellas y renderizado de líneas.
* **Entorno y Feedback:** Creación de `CloudManager.cs` (animación de nubes) y `AudioManager.cs`.

### Tareas Conjuntas

* Diseño conceptual del juego y diseño de la escena 3D, colocación de estrellas y diseño de la Interfaz de Usuario.
* Creación del sistema de depuración para PC (`EditorCameraMove.cs`) para agilizar el trabajo en paralelo.

---

## Check-list de Diseño de Aplicaciones de RV

Evaluación basada en las directrices de diseño para Realidad Virtual (Fuente: *Diseño de aplicaciones de RV*, ULL):

| Directriz / Heurística | Estado | Implementación en StarryNight |
| :--- | :---: | :--- |
| **Mitigación del Mareo (Motion Sickness)** | **Se contempla** | El usuario permanece estático y controla la cámara con su cabeza (sin aceleraciones artificiales ni discrepancia visual-vestibular). |
| **Control del Movimiento (Anticipación)** | **Se contempla** | El usuario siempre tiene el control de hacia dónde mira. No se fuerza el movimiento de la cámara sin su input. |
| **Mantenimiento del Head Tracking** | **Se contempla** | El seguimiento es 1:1 mediante el *Cardboard XR Plugin*. Si se pierde el foco, la aplicación no congela la imagen, sigue respondiendo. |
| **Inicio de Interacción Controlado** | **Se contempla** | La experiencia no arranca automáticamente. Existe una escena de "Menú" donde el usuario debe validar que está listo mirando el botón "Start". |
| **UI en el Campo de Visión** | **Se contempla** | Los menús y textos de feedback (como "FOTO GUARDADA") aparecen frente al usuario a una distancia legible y se emplazan en el campo de vista. |
| **Mecánica Gaze (Mirada como botón)** | **Se contempla** | Se utiliza un "Dwell Timer" (temporizador de espera) con feedback visual (botón cambiando de color) para confirmar acciones. |
| **Uso de Retícula** | **Se contempla** | La retícula está siempre presente para ayudar a apuntar a estrellas lejanas. |
| **Zonas de Confort (Viewing Zones)** | **Se contempla** | La mayoría de estrellas y menús se sitúan en la "Comfortable Content Zone" (±30° horizontal). Se evita forzar el cuello con ángulos extremos (>60° verticales). |
| **Cambios de Brillo Suaves** | **Se contempla** | El entorno es oscuro (noche) y los elementos brillantes (estrellas/UI) no generan destellos repentinos. |
| **Escala y Seguridad** | **Se contempla** | El entorno respeta la escala de un cielo abierto. Al ser una experiencia rotatoria (silla giratoria), se minimiza el riesgo de accidentes físicos. |
| **Propiocepción (Representación del cuerpo)** | **No aplica** | Se ha optado por no renderizar manos ni cuerpo virtual para evitar la disonancia cognitiva al no tener mandos con seguimiento posicional. |
| **Latencia de Audio (Inmersión)** | **No aplica** | No se ha implementado feedback auditivo al soplar o conectar estrellas, sólo uso de audio ambiental continuo. |

---

## Scripts implementados

* **`GazeInteraction.cs`**: Controla la interacción principal mediante la mirada (*SphereCast*), permitiendo activar estrellas y botones al mantener la vista fija sobre ellos.
* **`PhotoMicSystem.cs`**: Gestiona el micrófono para detectar soplidos o aplausos y captura la pantalla, guardando la imagen en la galería nativa del móvil.
* **`ShakeDetector.cs`**: Utiliza el acelerómetro (Input System) para detectar sacudidas bruscas de cabeza y lanzar el evento de "despejar el cielo".
* **`CloudManager.cs`**: Se suscribe al evento de sacudida para animar el desvanecimiento y desplazamiento de las nubes que bloquean la visión.
* **`ConstellationManager.cs`**: Dibuja líneas visuales (*LineRenderer*) en tiempo real conectando la última estrella activada con la nueva para formar constelaciones.
* **`GameEvents.cs`**: Implementa el patrón Observador mediante eventos estáticos para comunicar sistemas (sensores, UI, juego) sin generar dependencias directas.
* **`GameManager.cs`**: Administra el ciclo de vida de la aplicación, incluyendo la lógica para reiniciar la escena o cerrar el juego.
* **`AudioManager.cs`**: Se encarga de la reproducción en bucle de la música ambiental y la gestión de los canales de audio al inicio de la escena.
* **`EditorCameraMove.cs`**: Permite simular el movimiento de la cabeza con el ratón dentro del editor de Unity para facilitar las pruebas sin necesidad de compilar.

---

## Demo

https://youtu.be/etfJ629rX_Q
