using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Sonidos")]
    public AudioClip musicaBosque; // Aquí arrastras tu sonido de ambiente

    [Header("Configuración")]
    [Range(0, 1)] public float volumenMusica = 0.5f;

    private AudioSource fuenteMusica;

    void Awake()
    {
        // Configuramos solo el canal de música ambiente
        fuenteMusica = gameObject.AddComponent<AudioSource>();
        fuenteMusica.clip = musicaBosque;
        fuenteMusica.loop = true; // Importante: Que se repita en bucle infinito
        fuenteMusica.volume = volumenMusica;
        fuenteMusica.playOnAwake = true;
    }

    void Start()
    {
        // Iniciar el sonido nada más empezar el juego
        if (musicaBosque != null) 
        {
            fuenteMusica.Play();
        }
    }
}