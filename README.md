# 🌌 Starry Night

Una experiencia inmersiva de Realidad Virtual para Android (Google Cardboard) donde el jugador debe explorar el cielo nocturno y descubrir constelaciones.

![Gif de Ejecución](ruta_o_url_de_tu_gif_aqui.gif)

---

## 🚀 Cuestiones Importantes para el Uso

Para disfrutar de la experiencia correctamente, ten en cuenta lo siguiente:

1.  **Hardware:** Necesitas un móvil Android con giroscopio y un visor tipo Google Cardboard.
2.  **Permisos:** Al iniciar la app por primera vez, **debes aceptar el permiso de micrófono**. Es vital para una de las mecánicas principales.
3.  **Entorno:** Juega en un lugar donde puedas girar 360° sobre ti mismo (silla giratoria o de pie).

---

## 🎯 Hitos de Programación y Contenidos Impartidos

El desarrollo del proyecto ha puesto en práctica los siguientes conceptos clave de la asignatura:

* **Scripting Avanzado en C#:**
    * Uso de **Singletons** y Eventos estáticos (`GameEvents`, `ConstellationManager`) para gestionar el estado global del juego.
    * Implementación de **Corrutinas** (`IEnumerator`) para temporizadores, secuencias de animación y capturas de pantalla.
    * Listas y Arrays para la gestión dinámica de estrellas y niveles.
* **Interacción Multimodal (Sensores):**
    * Procesamiento de entrada de audio en tiempo real (`Microphone` class) para detectar soplidos.
    * Lectura del acelerómetro (`Input.acceleration`) para detectar gestos de agitación.
* **Realidad Virtual (XR):**
    * Implementación de **Gaze Interaction** (Interacción por mirada) usando Raycasting físico (`Physics.Raycast`).
    * Configuración del **XR Plugin Management** con Google Cardboard.

---

## ⭐ Aspectos Destacados

Lo que hace especial a esta aplicación:

1.  **Mecánica de "Soplar/Hablar":** No solo usamos botones; el jugador debe interactuar físicamente soplando o hablando fuerte al micrófono para **tomar una foto (captura de pantalla)** de su descubrimiento.
2.  **Feedback Visual y Sonoro:** Sistema de recompensas con partículas y sonidos ("Cling!") al conectar estrellas correctamente, reforzando la sensación de logro.
3.  **Interfaz Diegética:** No hay menús flotantes molestos; la interfaz está integrada en el mundo (las propias estrellas y constelaciones).
4.  **Entorno Dinámico:** El cielo empieza cubierto y el jugador debe interactuar físicamente para revelarlo.

---

## 📱 Sensores Incluidos (Interfaces Multimodales)

Se han integrado y programado los siguientes sensores del dispositivo móvil:

| Sensor | Uso en el Juego |
| :--- | :--- |
| **Giroscopio** | **Head Tracking:** Permite al usuario mirar alrededor del cielo estrellado moviendo la cabeza (Cámara VR). |
| **Acelerómetro** | **Shake Detection:** Detecta cuando el usuario agita el móvil para **disipar las nubes** y despejar el cielo (efecto de viento). |
| **Micrófono** | **Loudness Detection:** Analiza el volumen ambiental para detectar soplidos fuertes que activan la cámara y **toman una fotografía** de la constelación. |

---

## 📝 Acta de Acuerdos del Grupo

**Reparto de Tareas:**

| Integrante | Rol / Tareas Principales |
| :--- | :--- |
| **[Tu Nombre]** | Programación de sensores (Micrófono/Acelerómetro), Configuración de Unity y Android Build (Gradle), Lógica del GameManager. |
| **[Nombre 2]** | Diseño de niveles (colocación de estrellas), Búsqueda de Assets (Sonidos/Modelos), Diseño de UI. |
| **[Nombre 3]** | Programación del LineRenderer, Mecánica de Gaze Interaction, Documentación. |

*Nota: Todas las decisiones de diseño se tomaron por consenso en las reuniones semanales.*

---

## ✅ Check-list de Diseño de Aplicaciones de RV

Evaluación de buenas prácticas de diseño VR aplicadas al proyecto:

| Criterio de Diseño | Estado | Observaciones |
| :--- | :--- | :--- |
| **Evitar cinetosis (Motion Sickness)** | **Se contempla** | El movimiento es 100% controlado por el usuario (no hay movimiento artificial de cámara), manteniendo frames estables. |
| **Interfaz de Usuario (UI) Espacial** | **Se contempla** | Los textos y avisos están en "World Space" a una distancia cómoda de lectura, no pegados a la cara. |
| **Zona de Confort (Cuello)** | **Se contempla** | Las estrellas principales están situadas mayoritariamente en la línea del horizonte para evitar mirar mucho tiempo hacia arriba o abajo. |
| **Feedback Inmediato** | **Se contempla** | Las estrellas se iluminan al mirarlas (Hover) y suenan al conectarlas. |
| **Escala del Mundo** | **Se contempla** | El tamaño de las estrellas y la distancia respetan una escala coherente para la estereoscopía. |
| **Locomoción / Teletransporte** | **No aplica** | Es una experiencia estática (observación 360º), no requiere desplazamiento. |
| **Audio Espacial** | **No se contempla** | Se usa audio estéreo estándar, suficiente para la mecánica actual. |
