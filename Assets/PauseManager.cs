using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private bool isPaused;

    private void Start()
    {
        Resume();
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
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
}