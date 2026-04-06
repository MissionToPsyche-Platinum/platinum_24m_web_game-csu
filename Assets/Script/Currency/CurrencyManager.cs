using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public int currency;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            currency = 500;
        }
        else
        {
            Destroy(gameObject);
        }
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
