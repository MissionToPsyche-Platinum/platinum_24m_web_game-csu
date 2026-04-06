using UnityEngine;

public class ClickableHighlight : MonoBehaviour
{
    private SpriteRenderer sr;
    public Color highlightColor = new Color(1f, 0.92f, 0.016f, 1f);
    private Color originalColor;

    [Header("Upgrade Sprites (Level 2 to 5)")]
    public Sprite level2Sprite;
    public Sprite level3Sprite;
    public Sprite level4Sprite;
    public Sprite level5Sprite;

    [Header("Popup")]
    public GameObject upgradePopup;

    private int currentLevel = 1;

    void Start()
{
    sr = GetComponent<SpriteRenderer>();
    originalColor = sr.color;
    upgradePopup.SetActive(false); // this already disables the whole canvas
}


    void OnMouseEnter() { sr.color = highlightColor; }
    void OnMouseExit()  { sr.color = originalColor; }

   void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            if (currentLevel < 5)
                upgradePopup.SetActive(true);
        }
    }
}

    public void OnYesClicked()
    {
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
    }

    public void OnNoClicked()
    {
        upgradePopup.SetActive(false);
    }
}
