using UnityEngine;
using UnityEngine.EventSystems;

public class DrillHover : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;

    public Color highlightColor = new Color(1f, 0.92f, 0.016f, 1f);

    private Drill drill;

    void Awake()
    {
        drill = GetComponent<Drill>();

        rend = transform.Find("animatedDrill_0")
                        ?.GetComponentInChildren<Renderer>();

        if (rend != null)
            originalColor = rend.material.color;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (rend != null)
            rend.material.color = highlightColor;
    }

    void OnMouseExit()
    {
        if (rend != null)
            rend.material.color = originalColor;
    }

    void OnMouseDown()
    {
        // Block clicks ONLY if UI is open and you're clicking UI
        if (DrillUpgradeUI.Instance != null &&
            DrillUpgradeUI.Instance.gameObject.activeSelf &&
            EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (drill != null)
        {
            Debug.Log("Drill clicked!");
            DrillUpgradeUI.Instance.Open(drill);
        }
    }
}


