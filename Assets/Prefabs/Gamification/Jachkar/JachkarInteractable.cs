using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Collider))]
public class JachkarInteractable : MonoBehaviour
{
    private Outline outlineComponent;
    private Collider col;

    void Start()
    {
        outlineComponent = GetComponent<Outline>();
        col = GetComponent<Collider>();

        outlineComponent.enabled = false;
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
