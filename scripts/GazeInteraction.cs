using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GazeInteraction : MonoBehaviour
{
    [Header("Referencias UI")]
    public Image reticulaImagen; 
    public GameObject menuInicio; 
    public CanvasGroup menuCanvasGroup;

    [Header("Referencias Juego")]
    public GameObject hudJuego;
    public PhotoMicSystem scriptFoto;

    [Header("Configuración")]
    public float tiempoParaActivar = 1.5f;
    public float radioDelRayo = 0.5f; 
    public LayerMask capasInteractuables; 
    
    private float temporizadorBoton;
    private Color colorOriginalBoton;
    private GameObject botonActual;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool mirandoAlgoImportante = false;

        // Dibujamos el rayo en la escena para que veas si toca (Solo se ve en pestaña Scene)
        Debug.DrawRay(transform.position, transform.forward * 20, Color.red);

        if (Physics.SphereCast(ray, radioDelRayo, out hit, 500f, capasInteractuables))
        {
            if (hit.collider.CompareTag("Star"))
            {
                mirandoAlgoImportante = true;
                reticulaImagen.fillAmount = 1; 
                // Lógica de estrella (simplificada para debugging)
                Renderer rend = hit.collider.GetComponent<Renderer>();
                if (rend != null) rend.material.color = Color.yellow;
                GameEvents.TriggerStarConnected(hit.collider.transform.position);
            }
            else if (hit.collider.CompareTag("StartButton"))
            {
                mirandoAlgoImportante = true;
                GestionarBoton(hit.collider.gameObject, "Start");
            }
            else if (hit.collider.CompareTag("RestartButton"))
            {
                mirandoAlgoImportante = true;
                GestionarBoton(hit.collider.gameObject, "Restart");
            }
        }

        if (!mirandoAlgoImportante)
        {
            reticulaImagen.fillAmount = 0;
            ResetearBoton();
        }
    }

    void GestionarBoton(GameObject boton, string tipoAccion)
    {
        // 1. Inicialización al mirar por primera vez
        if (botonActual != boton)
        {
            botonActual = boton;
            Renderer rend = boton.GetComponent<Renderer>();
            if(rend != null) colorOriginalBoton = rend.material.color;
            temporizadorBoton = 0;
            Debug.Log("Mirando botón: " + tipoAccion);
        }

        // 2. Feedback visual (Color y Retícula)
        temporizadorBoton += Time.deltaTime;
        reticulaImagen.fillAmount = temporizadorBoton / tiempoParaActivar;
        
        Renderer r = boton.GetComponent<Renderer>();
        if(r != null) r.material.color = Color.Lerp(colorOriginalBoton, Color.green, temporizadorBoton / tiempoParaActivar);

        // 3. ¡TIEMPO COMPLETADO!
        if (temporizadorBoton >= tiempoParaActivar)
        {
            Debug.Log("¡Acción completada!: " + tipoAccion); // <--- MIRA SI SALE ESTO EN CONSOLA

            if (tipoAccion == "Start")
            {
                boton.GetComponent<Collider>().enabled = false; // Desactivar para no repetir
                StartCoroutine(FadeOutMenu()); 
            }
            else if (tipoAccion == "Restart")
            {
                if(GameManager.Instance != null) 
                {
                    Debug.Log("GameManager encontrado. Reiniciando...");
                    GameManager.Instance.ReiniciarNivel();
                }
                else
                {
                    Debug.LogError("¡ERROR! No existe el objeto GameManager en la escena.");
                }
            }
            
            // Reseteamos variables para evitar bucles
            temporizadorBoton = 0;
            botonActual = null;
        }
    }

    void ResetearBoton()
    {
        if (botonActual != null)
        {
            Renderer r = botonActual.GetComponent<Renderer>();
            if(r != null) r.material.color = colorOriginalBoton;
            botonActual = null;
            temporizadorBoton = 0;
        }
    }

    IEnumerator FadeOutMenu()
    {
        Debug.Log("Iniciando FadeOutMenu...");
        
        // FADE OUT
        if (menuCanvasGroup != null)
        {
            float alpha = 1.0f;
            while (alpha > 0)
            {
                alpha -= Time.deltaTime; 
                menuCanvasGroup.alpha = alpha;
                yield return null;
            }
        }
        else
        {
            Debug.LogWarning("No hay CanvasGroup asignado. El menú desaparecerá de golpe.");
        }

        // APAGAR MENU
        if(menuInicio != null) 
        {
            menuInicio.SetActive(false);
            Debug.Log("Menú apagado.");
        }
        else
        {
            Debug.LogError("¡ERROR! Variable 'menuInicio' no asignada en el Inspector.");
        }

        // ENCENDER JUEGO
        if (hudJuego != null) hudJuego.SetActive(true);
        if (scriptFoto != null) scriptFoto.enabled = true;
    }
}