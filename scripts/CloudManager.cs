using UnityEngine;
using System.Collections;

public class CloudManager : MonoBehaviour
{
    [Header("Configuración del Efecto")]
    public float velocidadFade = 1.0f;       // Rapidez con la que desaparecen
    public float velocidadViento = 3.0f;     // Rapidez con la que se mueven
    public Vector3 direccionViento = new Vector3(1, 0.2f, 0); // Se mueven a la Derecha (X) y un poco Arriba (Y)

    void OnEnable() => GameEvents.OnShakeDetected += IniciarDesvanecimiento;
    void OnDisable() => GameEvents.OnShakeDetected -= IniciarDesvanecimiento;

    void IniciarDesvanecimiento()
    {
        StartCoroutine(AnimacionFadeOut());
    }

    IEnumerator AnimacionFadeOut()
    {
        Renderer[] nubes = GetComponentsInChildren<Renderer>();
        float alpha = 1.0f;

        // Mientras sigan siendo visibles...
        while (alpha > 0)
        {
            float delta = Time.deltaTime;

            // --- 1. EFECTO DESVANECER (Fade) ---
            alpha -= delta * velocidadFade;
            foreach (Renderer r in nubes)
            {
                // Importante: Asegúrate que el material sea "Fade" o "Transparent"
                if (r.material.HasProperty("_Color"))
                {
                    Color colorActual = r.material.color;
                    colorActual.a = alpha;
                    r.material.color = colorActual;
                }
            }

            // --- 2. EFECTO VIENTO (Movimiento) ---
            // Movemos el grupo entero en la dirección elegida
            transform.Translate(direccionViento * delta * velocidadViento);

            yield return null;
        }

        gameObject.SetActive(false);
    }
}