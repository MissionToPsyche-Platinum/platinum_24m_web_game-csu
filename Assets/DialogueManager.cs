using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject astronautImage;
    public TextMeshProUGUI dialogueText;

    private string[] messages = {
        "Welcome to Psyche Mining.",
        "Your goal is to collect material from the asteroid and earn credits.",
        "Upgrade your speed and depth to mine more efficiently."
    };

    private int index = 0;

    void Start()
    {
        dialoguePanel.SetActive(true);
        astronautImage.SetActive(true);
        dialogueText.text = messages[index];
    }

    public void NextMessage()
    {
        index++;

        if (index < messages.Length)
        {
            dialogueText.text = messages[index];
        }
        else
        {
            dialoguePanel.SetActive(false);
            astronautImage.SetActive(false);
        }
    }
}