using UnityEngine;

public class ConstellationManager : MonoBehaviour
{
    public GameObject lineaPrefab; // Arrastraremos el prefab LineaBase aquí
    private Vector3 ultimaPosicion;
    private bool primeraVez = true;

    void OnEnable() => GameEvents.OnStarConnected += DibujarLinea;
    void OnDisable() => GameEvents.OnStarConnected -= DibujarLinea;

    void DibujarLinea(Vector3 nuevaPosicion)
    {
        if (!primeraVez)
        {
            // Crear línea desde la última estrella hasta la nueva
            GameObject nuevaLinea = Instantiate(lineaPrefab);
            LineRenderer lr = nuevaLinea.GetComponent<LineRenderer>();
            
            lr.positionCount = 2;
            lr.SetPosition(0, ultimaPosicion);
            lr.SetPosition(1, nuevaPosicion);
        }

        ultimaPosicion = nuevaPosicion;
        primeraVez = false;
    }
}