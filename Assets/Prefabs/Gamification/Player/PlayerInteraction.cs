using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuración del Raycast")]
    public float interactionDistance = 3f;
    public LayerMask interactableLayer;
    public LayerMask placementLayer;

    public LayerMask obstacleLayer;

    [Header("Posición de la mano")]
    public Transform handPos;
    private JachkarInteractable heldObject;

    [Header("UI")]
    public GameObject tooltipUI;
    public TextMeshProUGUI tooltipText;

    private JachkarInteractable currentInteractable;

    void Start()
    {
        tooltipUI.SetActive(false);
    }


    void Update()
    {
        if (heldObject == null)
        {
            HandleLookingAndPickUp();
        }
        else
        {
            HandleHoldingAndDrop();
        }
    }

    void HandleLookingAndPickUp()
    {
        // Rayo desde la camara del jugador
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        int layerMask = interactableLayer | obstacleLayer;

        if (Physics.Raycast(ray, out hit, interactionDistance, layerMask))
        {
            JachkarInteractable hitInteractable = hit.collider.GetComponent<JachkarInteractable>();

            if (hitInteractable != null)
            {
                if (hitInteractable != currentInteractable)
                {
                    if (currentInteractable != null)
                    {
                        currentInteractable.RemoveHighlight();
                    }

                    currentInteractable = hitInteractable;
                    currentInteractable.Highlight();
                }

                tooltipUI.SetActive(true);
                tooltipText.text = "Presiona 'E' para recoger";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    heldObject = currentInteractable;
                    heldObject.PickUp(handPos);
                    currentInteractable = null;
                    tooltipUI.SetActive(false);
                }
                return;
            }
        }

        // Si el rayo no choca con nada
        if (currentInteractable != null)
        {
            currentInteractable.RemoveHighlight();
            currentInteractable = null;
        }
        tooltipUI.SetActive(false);

    }

    void HandleHoldingAndDrop()
    {
        tooltipUI.SetActive(true);
        tooltipText.text = "Presiona 'E' para soltar";
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Rayo desde la camara del jugador
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            int layerMask = placementLayer | obstacleLayer;

            if (Physics.Raycast(ray, out hit, interactionDistance, layerMask))
            {
                if ((placementLayer.value & (1 << hit.collider.gameObject.layer)) > 0)
                    if (Vector3.Angle(hit.normal, Vector3.up) < 30f) // Ver si es una superficie plana
                    {
                    heldObject.Drop(hit.point);
                    heldObject = null;
                    tooltipUI.SetActive(false);
                }
            }
        }
    }
}
    