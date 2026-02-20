using UnityEngine;

public class DrillHover : MonoBehaviour
{
    public GameObject outline; // assign Outline child
    private Drill drill;

    private void Awake()
    {
        drill = GetComponent<Drill>();

        if (outline != null)
            outline.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if (outline != null)
            outline.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (outline != null)
            outline.SetActive(false);
    }

    private void OnMouseDown()
    {
        Debug.Log("Drill clicked!");
        if (drill == null) return;
        Debug.Log("Drill component is NULL");
        DrillUpgradeUI.Instance.Open(drill);
    }
}


