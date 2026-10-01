using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class GuiaNavegacion : MonoBehaviour
{
    [Header("Configuración del Tooltip")]
    public float tiempoParaAparecer = 5f; 
    public float velocidadFade = 2f;

    private CanvasGroup canvasGroup;
    private float tiempoInactivo = 0f;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    void Update()
    {
        // Leemos cualquier movimiento del teclado o la cámara
        float inputMovimiento = Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical"));
        float inputRaton = Mathf.Abs(Input.GetAxis("Mouse X")) + Mathf.Abs(Input.GetAxis("Mouse Y"));

        if (inputMovimiento == 0 && inputRaton == 0)
        {
            tiempoInactivo += Time.deltaTime;
        }
        else
        {
            tiempoInactivo = 0f; // Resetea el temporizador al instante si hay input
        }

        // Definimos la opacidad objetivo (1 es visible, 0 es invisible)
        float alphaObjetivo = (tiempoInactivo >= tiempoParaAparecer) ? 1f : 0f;

        // Movemos el alpha suavemente hacia el objetivo usando MoveTowards
        if (canvasGroup.alpha != alphaObjetivo)
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, alphaObjetivo, Time.deltaTime * velocidadFade);
        }
    }
}