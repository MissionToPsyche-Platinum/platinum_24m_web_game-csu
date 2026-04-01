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

    //Updates UI
    void Start()
    {
        UpdateUI();
    }
    //Next Fact
    public void NextFact()
    {
        currentIndex++;

        if (currentIndex >= facts.Length)
            currentIndex = 0;

        UpdateUI();
    }
    //Previous Fact
    public void PrevFact()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = facts.Length - 1;

        UpdateUI();
    }
    //Disables GUI when inactive; click onthe button to open and close the menu
    //NOTE: I WOULD LIKE TO MAKE SO WHEN ONE MENU IS PULLED UP OTHER CAN NOT BE. I DONT WANT MENU OVERLAPPING OVER EACH OTHER
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
