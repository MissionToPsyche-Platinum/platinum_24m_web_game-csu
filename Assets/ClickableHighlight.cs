using UnityEngine;
using UnityEngine.UI;

public class ClickableHighlight : MonoBehaviour
{
    private SpriteRenderer sr;
    public Color highlightColor = new Color(1f, 0.92f, 0.016f, 1f);
    private Color originalColor;

    [Header("Upgrade Settings")]
    public Sprite upgradedSprite;
    public GameObject upgradePopup;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        upgradePopup.SetActive(false);
    }

    void OnMouseEnter() { sr.color = highlightColor; }
    void OnMouseExit()  { sr.color = originalColor; }

    void OnMouseDown()
    {
        upgradePopup.SetActive(true);
    }

    public void OnYesClicked()
    {
        sr.sprite = upgradedSprite;
        upgradePopup.SetActive(false);
        sr.color = originalColor;
    }

    public void OnNoClicked()
    {
        upgradePopup.SetActive(false);
    }
}
