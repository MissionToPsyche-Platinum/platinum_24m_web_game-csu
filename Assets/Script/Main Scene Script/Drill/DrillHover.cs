using UnityEngine;
using UnityEngine.EventSystems;

public class DrillHover : MonoBehaviour
{
    public GameObject outline; 
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
        
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Debug.Log("Drill clicked!");

        if (drill == null)
        {
            Debug.Log("Drill component is NULL");
            return;
        }

        DrillUpgradeUI.Instance.Open(drill);
    }
}


