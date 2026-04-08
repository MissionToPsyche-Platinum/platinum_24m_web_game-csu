/*using UnityEngine;
using TMPro;

public class MaterialUI : MonoBehaviour
{
    public TMP_Text materialText;
    public TMP_Text currencyText;

    void Update()
    {
        materialText.text = ""  + MaterialManager.Instance.materials;
        currencyText.text = "" + CurrencyManager.Instance.currency;
        hii yo
    }  
}*/
using UnityEngine;
using TMPro;

public class MaterialUI : MonoBehaviour
{
    public TMP_Text materialText;
    public TMP_Text currencyText;

    void Update()
    {
        if (materialText != null)
            materialText.text = "" + MaterialManager.Instance.materials;
        if (currencyText != null)
            currencyText.text = "" + CurrencyManager.Instance.currency;
    }
}

