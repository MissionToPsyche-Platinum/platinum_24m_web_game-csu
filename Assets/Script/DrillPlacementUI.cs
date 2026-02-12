using UnityEngine;

public class DrillUIPanel : MonoBehaviour
{
    private DrillSlot currentSlot;

    // Called by the slot when clicked
    public void Open(DrillSlot slot)
    {
        currentSlot = slot;
        gameObject.SetActive(true);
    }

    
    public void OnBuyClicked()
    {
        if (currentSlot == null)
            return;

        currentSlot.BuildDrill();
        Close();
    }

   
    public void OnCancelClicked()
    {
        Close();
    }

    private void Close()
    {
        currentSlot = null;
        gameObject.SetActive(false);
    }
}




