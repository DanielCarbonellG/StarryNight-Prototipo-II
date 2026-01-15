using UnityEngine;
using System;

public static class GameEvents
{
    // Evento: Sacudir cabeza
    public static event Action OnShakeDetected;
    public static void TriggerShake() => OnShakeDetected?.Invoke();

    // Evento: Estrella conectada (enviamos su posición)
    public static event Action<Vector3> OnStarConnected;
    public static void TriggerStarConnected(Vector3 pos) => OnStarConnected?.Invoke(pos);

    // Evento: Orden de Foto
    public static event Action OnPhotoCommand;
    public static void TriggerPhotoCommand() => OnPhotoCommand?.Invoke();
}