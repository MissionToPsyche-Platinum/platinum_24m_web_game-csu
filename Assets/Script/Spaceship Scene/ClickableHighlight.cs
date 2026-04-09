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
    public int[] materialCosts = { 100, 5000, 10000, 50000 }; // levels 2-5
    public int[] currencyCosts = { 1500, 20000, 300000, 500000 };

    [Header("Popup")]
    public GameObject upgradePopup;
    public TMP_Text upgradeCostText;  // assign TextMeshPro object
    public Button yesButton;

    private int currentLevel = 1;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        upgradePopup.SetActive(false);
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

        // Update combined cost text
        upgradeCostText.text = $"Upgrade your ship for {nextCurrencyCost} credits and {nextMaterialCost} materials.";

        // Enable/disable Yes button if affordable
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

        // Double-check affordability
        if (MaterialManager.Instance.materials < requiredMaterials ||
            CurrencyManager.Instance.currency < requiredCurrency)
        {
            Debug.Log("Not enough resources to upgrade!");
            upgradePopup.SetActive(false);
            return;
        }

        // Spend resources
        MaterialManager.Instance.SpendMaterial(requiredMaterials);
        CurrencyManager.Instance.SpendCurrency(requiredCurrency);

        // Upgrade ship sprite
        currentLevel++;
        switch (currentLevel)
        {
            case 2: sr.sprite = level2Sprite; break;
            case 3: sr.sprite = level3Sprite; break;
            case 4: sr.sprite = level4Sprite; break;
            case 5: sr.sprite = level5Sprite; break;
        }

        upgradePopup.SetActive(false);
        sr.color = originalColor;

        // Zoom camera if applicable
        if (mainCamera != null)
            mainCamera.orthographicSize += zoomStep;
    }

    public void OnNoClicked()
    {
        upgradePopup.SetActive(false);
    }
}
