using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    [Header("Configuración del puzzle")]
    public Pedestal[] pedestals; // Array de pedestales
    public string songName;

    [Header("Audio")]
    public AudioSource coroAudioSource;

    [Header("Eventos")]
    public UnityEvent onPuzzleCompleted;

    private bool puzzleCompleted = false;

    private void Update()
    {
        if (!puzzleCompleted && CheckPuzzleCompletion())
        {
            CompletePuzzle();
        }
    }

    bool CheckPuzzleCompletion()
    {
        foreach (Pedestal pedestal in pedestals)
        {
            if (pedestal.currentJachkar == null || pedestal.currentJachkar.songName != songName)
            {
                return false; // Si algún pedestal no tiene un Jachkar o su tema no coincide, el puzzle no está completo
            }
        }
        return true; // Todos los pedestales tienen un Jachkar
    }

    void CompletePuzzle()
    {
        puzzleCompleted = true;
        
        foreach (Pedestal pedestal in pedestals)
        {
            if (pedestal.currentJachkar != null)
            {
                AudioSource audioStone = pedestal.GetComponent<AudioSource>();
                if (audioStone != null)
                {
                    audioStone.Stop();
                }
            }
        }

        if (coroAudioSource != null)
        {
            coroAudioSource.Play(); // Reproduce el audio del coro
        }
        onPuzzleCompleted.Invoke(); // Invoca el evento de puzzle completado
    }
}
