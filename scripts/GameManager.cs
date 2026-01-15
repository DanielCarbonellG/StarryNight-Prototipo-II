using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        // Singleton básico para poder llamarlo desde cualquier lado
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ReiniciarNivel()
    {
        // Recarga la escena activa actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}