using UnityEngine;

public class SellMaterials : MonoBehaviour
{
    public int materialValue = 20;
    public void Sell()
    {
        int materials = MaterialManager.Instance.materials;
        int currencyEarned = materials * materialValue;
        CurrencyManager.Instance.AddCurrency(currencyEarned);
        MaterialManager.Instance.ClearMaterial();

    }
}
