# 🌌 StarryNight VR - Experiencia Inmersiva de Constelaciones

**Repositorio GitHub:** [INSERTA AQUÍ EL ENLACE A TU GITHUB]

## 📖 Descripción del Proyecto
**StarryNight VR** es una experiencia relajante de realidad virtual para dispositivos móviles (Android / Google Cardboard) desarrollada en **Unity 6**. El objetivo es conectar estrellas para formar constelaciones en un entorno nocturno, utilizando interfaces multimodales (voz, movimiento y mirada) para interactuar con el mundo sin necesidad de mandos físicos.

---

## ⚠️ Cuestiones Importantes para el Uso

Para disfrutar de la experiencia correctamente, ten en cuenta lo siguiente:

1.  **Dispositivo:** Se requiere un móvil Android con giroscopio y acelerómetro funcional.
2.  **Visor:** Necesario visor tipo Google Cardboard o compatible.
3.  **Permisos:** Al iniciar la aplicación por primera vez, **debes conceder permisos** para:
    * 🎙️ **Micrófono:** Para la mecánica de captura de fotos por soplido/aplauso.
    * 📁 **Almacenamiento/Galería:** Para guardar las capturas de pantalla en el dispositivo.
4.  **Reinicio:** Si el centro de la cámara no está alineado, mira hacia adelante y reinicia la aplicación (mirando al cubo de "Reiniciar") o usa el botón de "Recentar" si tu visor lo permite.
5.  **Entorno:** Se recomienda jugar sentado en una silla giratoria o de pie con espacio para girar 360º.

---

## 🎯 Hitos de Programación y Contenidos Impartidos

El proyecto demuestra el dominio de los siguientes conceptos técnicos vistos en la asignatura:

* **Migración y Toolchain Moderno:** Actualización exitosa del proyecto desde Unity 2022 a **Unity 6 (2023+)**, resolviendo conflictos de Gradle, Android Manifest y Target API 34+.
* **Interfaces Multimodales (New Input System):** Implementación del nuevo sistema de entrada de Unity (`InputSystem`) para gestionar acelerómetro y teclado simultáneamente.
* **Patrón Observador (Events):** Desacoplamiento del código mediante un sistema de eventos estáticos (`GameEvents.cs`). Los scripts no se conocen entre sí, solo escuchan eventos (`OnStarConnected`, `OnShakeDetected`), lo que hace el código modular y escalable.
* **Raycasting Avanzado:** Uso de `Physics.SphereCast` (en lugar de Raycast simple) y **LayerMasks** para mejorar la precisión de la mirada en VR móvil, filtrando colisiones no deseadas (nubes, UI).
* **Corrutinas y Máquinas de Estados:** Gestión de tiempos de espera, animaciones de UI (`FadeOut`) y lógica de captura de pantalla asíncrona mediante `IEnumerator`.
* **Integración Nativa Android:** Uso de plugins externos (`NativeGallery`) y gestión de permisos en tiempo de ejecución (`UnityEngine.Android.Permission`) para interactuar con la galería del teléfono.

---

## ✨ Aspectos Destacados de la Aplicación

1.  **Interacción "Hands-Free" (Manos Libres):** Todo el juego se controla sin tocar la pantalla, usando exclusivamente la cabeza (mirada y gestos) y la voz.
2.  **Feedback Visual y Sonoro:** Sistema de retícula reactiva que se llena al mirar objetos interactuables, acompañado de cambios de color y audio espacial (ambiente vs. efectos).
3.  **Mecánica de "Soplido" para Captura:** Innovación en la interfaz al usar el micrófono no para hablar, sino para detectar picos de volumen (soplidos o aplausos) para sacar fotos.
4.  **Optimización para Móvil:** Uso de texturas ligeras, eliminación de colliders innecesarios y configuración de audio (`DecompressOnLoad` vs `Streaming`) para evitar latencia en dispositivos de gama media (como Samsung A50).

---

## 📡 Sensores Incluidos (Interfaces Multimodales)

Se han implementado y trabajado los siguientes sensores del dispositivo móvil:

| Sensor | Uso en el Proyecto | Script Principal |
| :--- | :--- | :--- |
| **Giroscopio** | Control de la cámara principal (Head Tracking). Permite al usuario mirar alrededor del escenario 360º. | `TrackedPoseDriver` (Unity System) |
| **Acelerómetro** | Detección de gestos bruscos ("Shake"). El usuario debe sacudir la cabeza para disipar las nubes que bloquean la visión. | `ShakeDetector.cs` |
| **Micrófono** | Análisis del buffer de audio en tiempo real para detectar umbrales de volumen. Se usa para activar la captura de pantalla. | `PhotoMicSystem.cs` |

---

## 🎥 Gif Animado de Ejecución

![Demo del Juego](demo_juego.gif)

*(Asegúrate de subir el archivo .gif a la carpeta del repositorio y que el nombre coincida)*

---

## 📝 Acta de Acuerdos del Grupo

**Integrantes del equipo:**
* [Nombre del Alumno 1]
* [Nombre del Alumno 2] (Si aplica)

**Reparto de Tareas:**

| Tarea | Responsable | Estado |
| :--- | :--- | :--- |
| Diseño del escenario y Assets 3D | [Nombre] | ✅ Completado |
| Programación de mecánicas VR (Gaze) | [Nombre] | ✅ Completado |
| Implementación de Sensores (Mic/Acelerómetro) | [Nombre] | ✅ Completado |
| Gestión de Audio y UI | [Nombre] | ✅ Completado |
| Migración a Unity 6 y solución de errores | [Nombre] | ✅ Completado |
| Documentación y Build Android | [Nombre] | ✅ Completado |

*Todas las decisiones de diseño, como la estética "Low Poly" y la paleta de colores nocturna, fueron consensuadas en reuniones de seguimiento.*

---

## ✅ Check-list de Recomendaciones de Diseño VR

A continuación se detalla cómo se han aplicado las recomendaciones de diseño para evitar el *motion sickness* y mejorar la usabilidad:

| Recomendación | Estado | Justificación / Implementación |
| :--- | :--- | :--- |
| **Evitar aceleraciones bruscas de cámara** | **Se contempla** | El usuario controla la cámara al 100% con su cabeza. No hay movimiento artificial del personaje. |
| **Horizonte estable** | **Se contempla** | El suelo y el cielo son referencias fijas que ayudan a la orientación. |
| **Interfaz en el espacio del mundo (Diegética)** | **Se contempla** | Los menús y botones son objetos 3D integrados en la escena, no pegados a la cara del usuario. |
| **Distancia de interacción cómoda** | **Se contempla** | Los menús flotan a 2-3 metros de distancia para evitar la fatiga visual (convergencia-acomodación). |
| **Feedback inmediato** | **Se contempla** | Al mirar un botón, este cambia de color y la retícula se llena progresivamente ("Fuse button"). |
| **Texto legible** | **Se contempla** | Se usa TextMeshPro con alto contraste y tamaño adecuado para la baja resolución de pantalla en VR. |
| **Evitar rotaciones forzadas** | **Se contempla** | El usuario decide cuándo y dónde girar. |
| **Locomoción** | **No aplica** | Es una experiencia estática (3DOF), no hay desplazamiento virtual. |

---

## 📂 Contenido del Entregable

1.  **Paquete Unity (.unitypackage):** Proyecto completo exportado.
2.  **Código Fuente (.zip):** Carpeta conteniendo exclusivamente la carpeta `Assets/Scripts` y este `README.md`.
3.  **APK Generada:** Archivo `StarryNightVR.apk` listo para instalar.

---

### 📧 Contacto
Para cualquier duda sobre la ejecución del proyecto, contactar con el equipo de desarrollo.
