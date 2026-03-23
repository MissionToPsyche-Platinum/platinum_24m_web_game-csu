using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FunFactUI : MonoBehaviour
{
    public GameObject funFactPanel;
    public TMP_Text titleText;  
    public TMP_Text factText;
    public Image factImage;


    public string[] facts;
    public Sprite[] images;

    private int currentIndex = 0;

    void Start()
    {
        UpdateUI();
    }

    public void NextFact()
    {
        currentIndex++;

        if (currentIndex >= facts.Length)
            currentIndex = 0;

        UpdateUI();
    }

    public void PrevFact()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = facts.Length - 1;

        UpdateUI();
    }
    public void ToggleFunFactUI()
    {
        bool isActive = funFactPanel.activeSelf;

        funFactPanel.SetActive(!isActive);

        if (!isActive) // opening
        {
            currentIndex = 0;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        // Update main text
        factText.text = facts[currentIndex];

        // Update title
        titleText.text = "Fun Fact " + (currentIndex + 1);
    }
}
