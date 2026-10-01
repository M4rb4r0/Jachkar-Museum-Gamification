using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class JachkarInteractable : MonoBehaviour
{
    [Header("Canción del Jachkar")]
    public string songName;
    public AudioClip songClip;
    public float volume = 0.3f;
    private Outline outlineComponent;
    private Collider col;

    private AudioSource audioSource;
    

    void Start()
    {
        outlineComponent = GetComponent<Outline>();
        col = GetComponent<Collider>();

        outlineComponent.enabled = false;

        audioSource = GetComponent<AudioSource>();

        audioSource.spatialBlend = 1.0f; // Hacer que el audio sea 3D
        audioSource.loop = true;
        audioSource.volume = volume;

        //limites de distancia para que el audio se escuche
        audioSource.minDistance = 1f;
        audioSource.maxDistance = 15f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;

        if (songClip != null)
        {
            audioSource.clip = songClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No se ha asignado un clip de audio para el Jachkar: " + gameObject.name);
        }
    }

    public void Highlight()
    {
        outlineComponent.enabled = true;
    }

    public void RemoveHighlight()
    {
        outlineComponent.enabled = false;
    }

    public void PickUp(Transform holdPos)
    {
        RemoveHighlight();
        col.enabled = false;

        transform.SetParent(holdPos);
        transform.localPosition = Vector3.zero;
        
        transform.localScale *= 0.5f;
    }

    public void Drop(Vector3 dropPos)
    {
        transform.SetParent(null);
        col.enabled = true;
        transform.localScale *= 2f;

        float floorDist = transform.position.y - col.bounds.min.y;

        transform.position = dropPos + new Vector3(0, floorDist, 0);
    }

}
