using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public int currency;

    void Start()
    {
        Instance = this;
        currency = 500;
    }

    public void AddCurrency(int amount) {
        currency += amount;
    }

    public bool SpendCurrency(int amount) {
        if (currency < amount)
            return false;

        currency -= amount;
        return true;
    }
}
