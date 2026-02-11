using UnityEngine;

public class DrillHover : MonoBehaviour
{
    public GameObject outline; // assign the Outline child in Inspector

    private void Awake()
    {
        if (outline != null)
            outline.SetActive(false);
    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse entered drill!");
        if (outline != null)
            outline.SetActive(true);
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse exited drill!");
        if (outline != null)
            outline.SetActive(false);
    }
}


