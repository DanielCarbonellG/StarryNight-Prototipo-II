# 🌌 StarryNight VR - Experiencia Inmersiva Multimodal

**StarryNight VR** es una aplicación de Realidad Virtual diseñada para dispositivos Android con visor Google Cardboard. El proyecto explora los principios de las **Interfaces Multimodales**, permitiendo al usuario interactuar con un entorno nocturno onírico mediante canales naturales: la mirada (atención visual), la voz/soplido (canal auditivo) y el movimiento físico (canal kinestésico).

## 🔗 Enlace al Repositorio

**[[ENLACE AL REPOSITORIO](https://github.com/DanielCarbonellG/StarryNight-Prototipo-II.git)]**

---

## ⚠️ Manual de Uso y Cuestiones Importantes

Para garantizar el correcto funcionamiento de la experiencia, el usuario debe tener en cuenta:

1. **Gestión de Permisos Críticos:**
* Al iniciar, la app solicitará acceso al **Micrófono** y al **Almacenamiento Externo**. Es imperativo aceptarlos; de lo contrario, la mecánica de "soplar para capturar" y el guardado en galería fallarán silenciosamente.


2. **Entorno Físico:**
* Se requiere un espacio libre de obstáculos para girar 360º (silla giratoria recomendada).
* El entorno debe ser moderadamente silencioso para evitar que el ruido ambiental active accidentalmente la cámara por el micrófono.


3. **Interacciones:**
* **Nubes:** Si la visión está bloqueada por nubes, agita la cabeza lateralmente ("No") con energía para dispersarlas.
* **Estrellas:** Mantén la mirada fija sobre una estrella durante 1.5 segundos para conectarla a la constelación.
* **Fotografía:** Sopla fuerte o emite un sonido alto para activar el flash y guardar una captura del cielo.



---

## 🚀 Hitos de Programación y Relación con la Asignatura

El desarrollo del proyecto ha cubierto los siguientes hitos técnicos, aplicando los contenidos teóricos de Unity y C#:

### 1. Arquitectura Desacoplada (Patrón Observer)

Se ha implementado una clase estática `GameEvents` que actúa como bus de eventos.

* **Logro:** Los sistemas de detección (`ShakeDetector`, `GazeInteraction`) no conocen a los sistemas de respuesta (`CloudManager`, `ConstellationManager`).
* **Ventaja:** Permite escalar el proyecto sin crear dependencias circulares.

### 2. Física y Raycasting Avanzado

En lugar de un `Raycast` simple, se ha utilizado **`Physics.SphereCast`** en `GazeInteraction.cs`.

* **Justificación:** El `SphereCast` proyecta una esfera a lo largo del rayo, creando un "volumen" de detección más grueso. Esto soluciona el problema de precisión en VR móvil, donde es difícil mantener la cabeza totalmente quieta para apuntar a objetos pequeños (como estrellas lejanas).

### 3. Procesamiento de Señal de Audio en Tiempo Real

En `PhotoMicSystem.cs` no solo se graba audio, sino que se analiza el buffer en tiempo real.

* **Técnica:** Se extrae una ventana de muestras (`GetData`) y se calcula el promedio de amplitud absoluta para determinar el nivel de presión sonora (volumen). Esto permite usar el micrófono como un "sensor de viento/soplido" en lugar de un grabador de voz.

### 4. Integración Nativa con Android

Se ha puenteado la barrera entre Unity y el Sistema Operativo Android:

* **Gestión de Permisos:** Uso de la librería `UnityEngine.Android.Permission` para solicitar autorización en tiempo de ejecución.
* **Galería de Imágenes:** Implementación de escritura en disco y refresco de la galería nativa del móvil para que las fotos aparezcan inmediatamente fuera de la app.

### 5. Optimización para Móvil (Cardboard)

* Uso de **IL2CPP** y arquitectura **ARM64** para mejorar el rendimiento de la CPU.
* Gestión eficiente de corrutinas (`IEnumerator`) para animaciones (fade out de nubes, feedback de UI) evitando el uso excesivo del `Update`.

---

## ⭐ Aspectos Destacados de la Aplicación

1. **Multimodalidad Real (Fusión de Inputs):** La aplicación no depende de un solo canal. Combina inputs pasivos (giroscopio) con activos (voz y gestos), creando una experiencia rica donde el cuerpo entero actúa como controlador.
2. **Sistema de Depuración Híbrido (PC/Móvil):**
* Se ha desarrollado el script `EditorCameraMove.cs` que utiliza directivas de preprocesador (`#if UNITY_EDITOR`) para simular el giroscopio con el ratón.

3. **Feedback de Usuario (UX):**
* **Visual:** La retícula de carga ("Dwell timer") informa al usuario de que su mirada está surtiendo efecto.
* **Auditivo:** Música ambiental en bucle sin costuras (`AudioManager`).
* **Kinestésico:** La respuesta inmediata de las nubes al movimiento de la cabeza refuerza la sensación de presencia.



---

## 📱 Sensores Utilizados (Interfaces Multimodales)

La aplicación hace uso intensivo de la sensórica integrada en el smartphone, procesando los datos crudos para convertirlos en interacciones semánticas:

| Sensor | Script | Procesamiento y Uso |
| --- | --- | --- |
| **Giroscopio / Acelerómetro (Fusión)** | *Google Cardboard Plugin* | **Head Tracking:** Procesa la orientación del dispositivo en cuaterniones para mover la cámara virtual (`TrackedPoseDriver`). |
| **Acelerómetro (Raw)** | `ShakeDetector.cs` | **Reconocimiento de Gestos:** Se monitorea la magnitud cuadrática (`sqrMagnitude`) del vector de aceleración lineal. Si supera un umbral de sensibilidad (`2.0f`) ignorando la gravedad, se dispara el evento "Shake" (agitar). |
| **Micrófono** | `PhotoMicSystem.cs` | **Detector de Umbral:** Se utiliza como sensor de intensidad sonora. No se emplea reconocimiento de voz (ASR), sino detección de picos de amplitud para simular un "disparador" por soplido. |

---

## 🎥 Gif Animado de Ejecución

![Demo del Juego](GIF/StarryNight.mp4)

---

## 🤝 Acta de Acuerdos del Grupo

El desarrollo se ha realizado siguiendo una metodología de trabajo colaborativo, dividiendo la implementación de sistemas clave y unificando el diseño final.

### Reparto de Tareas

**Daniel Carbonell de Chaves:**

* **Sistemas de Input Físico:** Implementación completa de `ShakeDetector.cs` y gestión del acelerómetro con el nuevo Input System.
* **Sistema de Audio y Permisos:** Desarrollo de `PhotoMicSystem.cs`, incluyendo el análisis de espectro de audio, gestión de permisos Android en tiempo de ejecución y la integración con la galería nativa.
* **Arquitectura:** Diseño del sistema de eventos (`GameEvents.cs`) para desacoplar los scripts.
* **Compilación:** Resolución de conflictos de Gradle y configuración del Player Settings para Android (API 26+, IL2CPP).

**Guillermo González Pineda:**

* **Interacción Visual (Gaze):** Desarrollo de `GazeInteraction.cs` utilizando `SphereCast` para mejorar la precisión y programación de la lógica de "Dwell Time" (temporizadores de mirada).
* **Lógica de Juego:** Implementación de `ConstellationManager.cs` para la conexión de estrellas y renderizado de líneas.
* **Entorno y Feedback:** Creación de `CloudManager.cs` (animación procedural de nubes) y `AudioManager.cs`. Diseño de la escena 3D, colocación de estrellas y diseño de la Interfaz de Usuario (UI).

### Tareas Conjuntas

* Diseño conceptual de la experiencia multimodal.
* Testing iterativo en dispositivo físico (Samsung Galaxy A50).
* Creación del sistema de depuración para PC (`EditorCameraMove.cs`) para agilizar el trabajo en paralelo.

---

## ✅ Check-list de Diseño de Aplicaciones de RV

Evaluación basada en las directrices de diseño para Realidad Virtual (Fuente: *Diseño de aplicaciones de RV*, ULL):

| Directriz / Heurística | Estado | Implementación en StarryNight |
| :--- | :---: | :--- |
| **Mitigación del Mareo (Motion Sickness)** | **Se contempla** | El usuario permanece estático y controla la cámara con su cabeza (sin aceleraciones artificiales ni discrepancia visual-vestibular). |
| **Control del Movimiento (Anticipación)** | **Se contempla** | El usuario siempre tiene el control de hacia dónde mira. No se fuerza el movimiento de la cámara sin su input. |
| **Mantenimiento del Head Tracking** | **Se contempla** | El seguimiento es 1:1 mediante el *Cardboard XR Plugin*. Si se pierde el foco, la aplicación no congela la imagen, sigue respondiendo. |
| **Inicio de Interacción Controlado** | **Se contempla** | La experiencia no arranca automáticamente. Existe una escena de "Menú" donde el usuario debe validar que está listo mirando el botón "Start". |
| **UI en el Campo de Visión** | **Se contempla** | Los menús y textos de feedback (como "Sopla para foto") aparecen frente al usuario a una distancia legible y se emplazan en el campo de vista. |
| **Mecánica Gaze (Mirada como botón)** | **Se contempla** | Se utiliza un "Dwell Timer" (temporizador de espera) de 1.5s con feedback visual (retícula llenándose) para confirmar acciones. |
| **Uso de Retícula** | **Se contempla** | La retícula está siempre presente para ayudar a apuntar a estrellas lejanas, cambiando su estado (fill amount) al interactuar y resaltando el punto de intersección. |
| **Zonas de Confort (Viewing Zones)** | **Se contempla** | La mayoría de estrellas y menús se sitúan en la "Comfortable Content Zone" (±30° horizontal). Se evita forzar el cuello con ángulos extremos (>60° verticales). |
| **Cambios de Brillo Suaves** | **Se contempla** | El entorno es oscuro (noche) y los elementos brillantes (estrellas/UI) no generan destellos repentinos. |
| **Escala y Seguridad** | **Se contempla** | El entorno respeta la escala de un cielo abierto. Al ser una experiencia rotatoria (silla giratoria), se minimiza el riesgo de accidentes físicos. |
| **Propiocepción (Representación del cuerpo)** | **No aplica** | Se ha optado por no renderizar manos ni cuerpo virtual para evitar la disonancia cognitiva al no tener mandos con seguimiento posicional. |
| **Latencia de Audio (Inmersión)** | **Se contempla** | Respuesta inmediata (<20ms) del feedback auditivo al soplar o conectar estrellas. Uso de audio ambiental continuo. |
