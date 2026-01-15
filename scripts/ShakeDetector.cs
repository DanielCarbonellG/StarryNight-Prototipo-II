using UnityEngine;
using UnityEngine.InputSystem; 

public class ShakeDetector : MonoBehaviour
{
    public float sensibilidad = 2.0f; 

    void Start()
    {
        // IMPORTANTE: Encender el sensor explícitamente
        if (Accelerometer.current != null)
        {
            InputSystem.EnableDevice(Accelerometer.current);
        }
    }

    void Update()
    {
        if (Accelerometer.current != null)
        {
            // Usamos acceleration (incluye gravedad). Si agitas fuerte, superará la gravedad + fuerza.
            if (Accelerometer.current.acceleration.ReadValue().sqrMagnitude >= sensibilidad * sensibilidad)
            {
                GameEvents.TriggerShake();
            }
        }

        #if UNITY_EDITOR
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GameEvents.TriggerShake();
        }
        #endif
    }
    
    // Apagar el sensor al salir para ahorrar batería
    void OnDisable()
    {
        if (Accelerometer.current != null)
            InputSystem.DisableDevice(Accelerometer.current);
    }
}