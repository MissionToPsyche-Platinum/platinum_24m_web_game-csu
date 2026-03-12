using UnityEngine;
using TMPro;

public class MaterialUI : MonoBehaviour
{
    public TMP_Text materialText;
    public TMP_Text currencyText;

    void Update()
    {
        materialText.text = "Material: " + MaterialManager.Instance.materials;
        currencyText.text = "Credit: " + CurrencyManager.Instance.currency;
    }  
}
