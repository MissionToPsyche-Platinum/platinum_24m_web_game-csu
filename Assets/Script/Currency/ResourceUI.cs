using UnityEngine;
using TMPro;

public class MaterialUI : MonoBehaviour
{
    public TMP_Text materialText;
    public TMP_Text currencyText;

    void Update()
    {
        materialText.text = ""  + MaterialManager.Instance.materials;
        currencyText.text = "" + CurrencyManager.Instance.currency;
    }  
}
