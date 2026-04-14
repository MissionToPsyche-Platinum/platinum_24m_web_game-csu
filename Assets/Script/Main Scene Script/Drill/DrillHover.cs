using UnityEngine;
using UnityEngine.EventSystems;

public class DrillHover : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;

    public Color highlightColor = new Color(1f, 0.92f, 0.016f, 1f);

    void Awake()
    {
        // IMPORTANT: go into the animated child
        rend = transform.Find("animatedDrill_0")
                        ?.GetComponentInChildren<Renderer>();

        if (rend != null)
            originalColor = rend.material.color;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Debug.Log("HOVER ENTER");

        if (rend != null)
            rend.material.color = highlightColor;
        else
            Debug.LogWarning("Renderer not found");
    }

    void OnMouseExit()
    {
        if (rend != null)
            rend.material.color = originalColor;
    }
}


