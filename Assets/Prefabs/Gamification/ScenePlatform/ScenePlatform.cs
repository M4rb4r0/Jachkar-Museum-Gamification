using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class ScenePlatform : MonoBehaviour
{
    [Header("Configuración de nivel")]
    [Tooltip("Nombre de la escena a cargar al interactuar con la plataforma")]
    public string NextSceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            AdvanceLevel();
        }
    }

    private void AdvanceLevel()
    {
        if (!string.IsNullOrEmpty(NextSceneName))
        {
            SceneManager.LoadScene(NextSceneName);
        }
        else
        {
            Debug.LogWarning("NextSceneName no está configurado en ScenePlatform.");
        }
    }
      
}
