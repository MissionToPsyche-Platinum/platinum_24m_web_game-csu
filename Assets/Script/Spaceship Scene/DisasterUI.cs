using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisasterUI : MonoBehaviour
{
    public static DisasterUI Instance;

    [Header("UI References")]
    public GameObject disasterPopup;
    public GameObject redFlash;
    public TMP_Text disasterText;
    public Button fixButton;
    public Button debtButton;

    private int currentRepairCost;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        disasterPopup.SetActive(false);
        redFlash.SetActive(false);
    }

    public void ShowDisasterPopup(int cost)
    {
        currentRepairCost = cost;
        disasterText.text = "ASTEROID SHOWER ALERT!\nSHIP DAMAGED!\nCOST TO FIX: " + cost;
        disasterPopup.SetActive(true);
        redFlash.SetActive(true);
        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        Image flashImage = redFlash.GetComponent<Image>();
        for (int i = 0; i < 6; i++)
        {
            flashImage.color = new Color(1, 0, 0, 0.4f);
            yield return new WaitForSeconds(0.2f);
            flashImage.color = new Color(1, 0, 0, 0f);
            yield return new WaitForSeconds(0.2f);
        }
        flashImage.color = new Color(1, 0, 0, 0.15f);
    }

    public void OnFixPressed()
    {
        if (CurrencyManager.Instance.SpendCurrency(currentRepairCost))
        {
            DisasterManager.Instance.hasDebt = false;
            CloseDisaster();
        }
        else
        {
            disasterText.text = "NOT ENOUGH CREDITS!\nTAKE DEBT OR PAY ON NEXT UPGRADE!";
        }
    }

    public void OnDebtPressed()
    {
        DisasterManager.Instance.hasDebt = true;
        CloseDisaster();
    }

    void CloseDisaster()
    {
        disasterPopup.SetActive(false);
        redFlash.SetActive(false);
        DisasterManager.Instance.ResolveDisaster();
    }
}