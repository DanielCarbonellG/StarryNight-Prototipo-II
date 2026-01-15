using UnityEngine;
using UnityEngine.InputSystem; // <-- NECESARIO
// Necesitamos esto para poder apagar el driver del giroscopio en el PC si usas XR
using UnityEngine.InputSystem.XR; 

public class EditorCameraMove : MonoBehaviour
{
    public float sensibilidad = 0.5f; // He bajado esto porque el delta del nuevo sistema es píxel a píxel
    private float rotacionX = 0;
    private float rotacionY = 0;

    void Start()
    {
        #if UNITY_EDITOR
            // Apagamos el Tracked Pose Driver para que no bloquee la cámara
            var driver = GetComponent<TrackedPoseDriver>();
            if (driver != null) 
            {
                driver.enabled = false;
            }

            Cursor.lockState = CursorLockMode.Locked;
        #endif
    }

    void Update()
    {
        #if UNITY_EDITOR
            // Si no hay ratón detectado, no hacemos nada
            if (Mouse.current == null) return;

            // Leemos el delta del ratón (diferencia de posición)
            Vector2 deltaMouse = Mouse.current.delta.ReadValue();

            float mouseX = deltaMouse.x * sensibilidad * Time.deltaTime * 50f; // Ajuste de velocidad
            float mouseY = deltaMouse.y * sensibilidad * Time.deltaTime * 50f;

            // Calculamos la rotación
            rotacionY += mouseX;
            rotacionX -= mouseY;
            
            rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

            transform.localRotation = Quaternion.Euler(rotacionX, rotacionY, 0);
        #endif
    }
}