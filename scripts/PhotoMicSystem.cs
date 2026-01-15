using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Android; 
using System.Collections;
// using NativeGalleryNamespace; // Si te da error de namespace, descomenta esto

public class PhotoMicSystem : MonoBehaviour
{
    [Header("Referencias Básicas")]
    public Image flashPanel;
    
    [Header("Referencias UI")]
    public GameObject grupoInstrucciones; 
    public GameObject textoFeedbackFoto;   
    public CanvasGroup hudCanvasGroup; 

    AudioClip micClip;
    bool micIniciado = false;
    bool sacandoFoto = false;

    IEnumerator Start()
    {
        if(textoFeedbackFoto != null) textoFeedbackFoto.SetActive(false);

        #if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
            yield return new WaitForSeconds(1.0f);
        }
        #endif

        if (Microphone.devices.Length > 0)
        {
            micClip = Microphone.Start(null, true, 10, 44100);
            micIniciado = true;
        }
        yield return null;
    }

    void Update()
    {
        if (micIniciado && !sacandoFoto)
        {
            if (GetVolumen() > 0.15f) SacarFoto();
        }

        #if UNITY_EDITOR
        if (!sacandoFoto && Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
        {
            SacarFoto();
        }
        #endif
    }

    float GetVolumen()
    {
        if (micClip == null) return 0;
        float[] data = new float[256];
        int micPos = Microphone.GetPosition(null) - 256 + 1;
        if (micPos < 0) return 0;
        micClip.GetData(data, micPos);
        float maxV = 0;
        foreach (float s in data) maxV += Mathf.Abs(s);
        return maxV / 256.0f;
    }

    public void SacarFoto()
    {
        if (sacandoFoto) return;
        StartCoroutine(ProcesoFotoCompleto());
    }

    IEnumerator ProcesoFotoCompleto()
    {
        sacandoFoto = true;

        if (grupoInstrucciones != null) grupoInstrucciones.SetActive(false);
        yield return new WaitForEndOfFrame();

        Texture2D screenshot = ScreenCapture.CaptureScreenshotAsTexture();
        string nombreArchivo = "StarryNight_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

        // --- CORRECCIÓN AQUÍ ---
        // Quitamos "NativeGallery.Permission permiso =" y llamamos a la función directamente.
        NativeGallery.SaveImageToGallery(screenshot, "StarryNight", nombreArchivo, (success, path) => 
        {
            if(success) Debug.Log("Foto guardada en Galería: " + path);
            else Debug.Log("Error al guardar en Galería");
        });

        Destroy(screenshot);

        if (flashPanel != null)
        {
            flashPanel.gameObject.SetActive(true);
            flashPanel.canvasRenderer.SetAlpha(1.0f);
            flashPanel.CrossFadeAlpha(0, 0.5f, false);
        }

        if (grupoInstrucciones != null) grupoInstrucciones.SetActive(true);
        StartCoroutine(AnimacionFeedback());

        yield return new WaitForSeconds(0.5f);
        if (flashPanel != null) flashPanel.gameObject.SetActive(false);
        sacandoFoto = false;
    }

    IEnumerator AnimacionFeedback()
    {
        if (textoFeedbackFoto == null || hudCanvasGroup == null) yield break;
        textoFeedbackFoto.SetActive(true);
        float alpha = 0;
        while(alpha < 1) { alpha += Time.deltaTime * 3; hudCanvasGroup.alpha = alpha; yield return null; }
        hudCanvasGroup.alpha = 1;
        yield return new WaitForSeconds(2.0f);
        while(alpha > 0) { alpha -= Time.deltaTime; hudCanvasGroup.alpha = alpha; yield return null; }
        hudCanvasGroup.alpha = 0;
        textoFeedbackFoto.SetActive(false);
        hudCanvasGroup.alpha = 1; 
    }
}