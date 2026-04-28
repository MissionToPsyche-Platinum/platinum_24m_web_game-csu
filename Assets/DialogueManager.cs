using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    [Header("Tutorial UI")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public RectTransform pointerArrow;
    public TMP_Text nextButtonText;

    [Header("Tutorial Targets")]
    public RectTransform sellTarget;
    public RectTransform spaceshipTarget;
    public RectTransform funFactsTarget;
    public RectTransform winConditionTarget;

    [Header("Typing Settings")]
    public float typingSpeed = 0.05f;

    private Queue<string> lines = new Queue<string>();
    private int currentStep = 0;
    private bool isTyping = false;
    private string currentLine = "";

    void Start()
    {
        if (PlayerPrefs.GetInt("TutorialDone", 0) == 1)
        {
            dialoguePanel.SetActive(false);

            if (pointerArrow != null)
                pointerArrow.gameObject.SetActive(false);

            return;
        }

        Time.timeScale = 0f;

        StartDialogue(new string[]
        {
            "Welcome Captain! The year is 2100, and humanity has set its sights on the asteroid Psyche, a treasure trove of valuable metals and minerals.",
            "We have finally made a space drill, capable of extracting these precious resources! You have been tasked with collecting these resources and upgrading the spacecraft",
            "Lets get you familiar with the controls and the interface.",
            "Use the drills to gather materials from the asteroid. Once you have enough, you can sell them for credits.",
            "Sell your materials here to earn credits.",
            "Use this button to go to the spaceship scene.",
            "Check your progress toward victory here.",
            "Click here to read fun facts about the Psyche asteroid!"
        });
    }

    public void StartDialogue(string[] dialogueLines)
    {
        dialoguePanel.SetActive(true);
        lines.Clear();
        currentStep = 0;

        foreach (string line in dialogueLines)
        {
            lines.Enqueue(line);
        }

        ShowStep();
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = currentLine;
            isTyping = false;
            return;
        }

        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentLine = lines.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeLine(currentLine));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
    }

    void ShowStep()
    {
        if (pointerArrow == null)
            return;

        switch (currentStep)
        {
            // Intro dialogue (no arrow)
            case 0:
            case 1:
            case 2:
            case 3:
                pointerArrow.gameObject.SetActive(false);
                if (nextButtonText != null) nextButtonText.text = "Next";
                break;

            // Sell button
            case 4:
                pointerArrow.gameObject.SetActive(true);
                MoveArrow(sellTarget, new Vector2(0f, 80f));
                if (nextButtonText != null) nextButtonText.text = "Next";
                break;

            // Spaceship button
            case 5:
                pointerArrow.gameObject.SetActive(true);
                MoveArrow(spaceshipTarget, new Vector2(0f, 80f));
                if (nextButtonText != null) nextButtonText.text = "Next";
                break;

            // Win condition button
            case 6:
                pointerArrow.gameObject.SetActive(true);
                MoveArrow(winConditionTarget, new Vector2(0f, 80f));
                if (nextButtonText != null) nextButtonText.text = "Next";
                break;

            // Fun facts button (last step)
            case 7:
                pointerArrow.gameObject.SetActive(true);
                MoveArrow(funFactsTarget, new Vector2(0f, 80f));
                if (nextButtonText != null) nextButtonText.text = "Finish";
                break;
        }
    }

    void MoveArrow(RectTransform target, Vector2 offset)
    {
        if (target == null || pointerArrow == null)
            return;

        pointerArrow.position = target.position + (Vector3)offset;
    }

    public void NextStep()
    {
        if (isTyping)
        {
            DisplayNextLine();
            return;
        }

        currentStep++;
        ShowStep();
        DisplayNextLine();
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);

        if (pointerArrow != null)
            pointerArrow.gameObject.SetActive(false);

        Time.timeScale = 1f;

        PlayerPrefs.SetInt("TutorialDone", 1);
        PlayerPrefs.Save();

        Debug.Log("Tutorial Finished");
    }
}