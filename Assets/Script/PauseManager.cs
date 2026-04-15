using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Text muteText;

    private bool isPaused;

    private void Start()
    {
        Resume();
        SyncUI();
    }

    // 🔄 Sync slider + text when scene loads
    private void SyncUI()
    {
        if (AudioSettings.instance == null) return;

        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);

        if (volumeSlider != null)
        {
            volumeSlider.SetValueWithoutNotify(savedVolume);
        }

        UpdateMuteText();
    }

    public void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pauseMenu != null)
            pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    public void OpenPsycheWebsite()
    {
        Application.OpenURL("https://psyche.ssl.berkeley.edu/");
    }

    // 🔇 MUTE BUTTON
    public void ToggleMute()
    {
        if (AudioSettings.instance == null) return;

        AudioSettings.instance.ToggleMute();
        UpdateMuteText();
    }

    // 🎚 SLIDER
    public void SetVolume(float value)
    {
        if (AudioSettings.instance == null) return;

        AudioSettings.instance.SetVolume(value);
    }

    // 🔊 MUTE TEXT
    public void UpdateMuteText()
    {
        if (AudioSettings.instance == null || muteText == null) return;

        muteText.text = AudioSettings.instance.IsMuted
            ? "Unmute Music"
            : "Mute Music";
    }
}