using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class Pedestal : MonoBehaviour
{
    [Tooltip("Jachkar actualmente en el pedestal")]
    public JachkarInteractable currentJachkar = null;

    [Tooltip("Audio del pedestal")]
    public AudioClip StoneClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        JachkarInteractable jachkar = other.GetComponent<JachkarInteractable>();
        if (jachkar != null)
        {
            currentJachkar = jachkar;

            if (StoneClip != null)
            {
                audioSource.PlayOneShot(StoneClip);
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.GetComponent<JachkarInteractable>() == currentJachkar)
        {
            currentJachkar = null;
        }
    }
}
