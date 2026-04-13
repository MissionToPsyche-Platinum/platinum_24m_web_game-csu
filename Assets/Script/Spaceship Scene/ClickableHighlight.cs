using UnityEngine;
using UnityEngine.UI;  // for Button
using TMPro;           // for TMP_Text

public class ClickableHighlight : MonoBehaviour
{
    private SpriteRenderer sr;
    public Color highlightColor = new Color(1f, 0.92f, 0.016f, 1f);
    private Color originalColor;
    public Camera mainCamera;
    public float zoomStep = 1.5f;

    [Header("Upgrade Sprites (Level 2 to 5)")]
    public Sprite level2Sprite;
    public Sprite level3Sprite;
    public Sprite level4Sprite;
    public Sprite level5Sprite;

    [Header("Upgrade Costs")]
    public int[] materialCosts = { 100, 500, 1000, 50000 }; // levels 2-5
    public int[] currencyCosts = { 1500, 5000, 20000, 50000 };

    [Header("Popup")]
    public GameObject upgradePopup;
    public TMP_Text upgradeCostText;  
    public Button yesButton;

    private int currentLevel = 1;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        upgradePopup.SetActive(false);
        currentLevel = PlayerPrefs.GetInt("ShipLevel", 1);
        ApplyLevelSprite();
    }

    void OnMouseEnter() { sr.color = highlightColor; }
    void OnMouseExit() { sr.color = originalColor; }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                ShowUpgradePopup();
            }
        }
    }

    private void ShowUpgradePopup()
{
    if (currentLevel >= 5)
    {
        Debug.Log("Ship is already at max level!");
        upgradePopup.SetActive(false);
        return;
    }

    int nextIndex = currentLevel - 1;
    int nextMaterialCost = materialCosts[nextIndex];
    int nextCurrencyCost = currencyCosts[nextIndex];

    if (DisasterManager.Instance.hasDebt)
        nextCurrencyCost += DisasterManager.Instance.repairCost;

    if (DisasterManager.Instance.hasDebt)
        upgradeCostText.text = $"Upgrade your ship for {nextCurrencyCost} credits (includes {DisasterManager.Instance.repairCost} repair debt) and {nextMaterialCost} materials.";
    else
        upgradeCostText.text = $"Upgrade your ship for {nextCurrencyCost} credits and {nextMaterialCost} materials.";

    bool canAfford = MaterialManager.Instance.materials >= nextMaterialCost &&
                     CurrencyManager.Instance.currency >= nextCurrencyCost;
    yesButton.interactable = canAfford;

    upgradePopup.SetActive(true);
}

public void OnYesClicked()
{
    int nextIndex = currentLevel - 1;
    int requiredMaterials = materialCosts[nextIndex];
    int requiredCurrency = currencyCosts[nextIndex];

    if (DisasterManager.Instance.hasDebt)
        requiredCurrency += DisasterManager.Instance.repairCost;

    if (MaterialManager.Instance.materials < requiredMaterials ||
        CurrencyManager.Instance.currency < requiredCurrency)
    {
        Debug.Log("Not enough resources to upgrade!");
        upgradePopup.SetActive(false);
        return;
    }

    MaterialManager.Instance.SpendMaterial(requiredMaterials);
    CurrencyManager.Instance.SpendCurrency(requiredCurrency);
    DisasterManager.Instance.hasDebt = false; // clear debt on upgrade

    currentLevel++;
    PlayerPrefs.SetInt("ShipLevel", currentLevel);
    switch (currentLevel)
    {
        case 2: sr.sprite = level2Sprite; break;
        case 3: sr.sprite = level3Sprite; break;
        case 4: sr.sprite = level4Sprite; break;
        case 5: sr.sprite = level5Sprite; break;
    }

    upgradePopup.SetActive(false);
    sr.color = originalColor;

    if (mainCamera != null)
        mainCamera.orthographicSize += zoomStep;
}

    public void OnNoClicked()
    {
        upgradePopup.SetActive(false);
    }

    void ApplyLevelSprite()
{
    switch (currentLevel)
    {
        case 2: sr.sprite = level2Sprite; break;
        case 3: sr.sprite = level3Sprite; break;
        case 4: sr.sprite = level4Sprite; break;
        case 5: sr.sprite = level5Sprite; break;
    }
}
}

