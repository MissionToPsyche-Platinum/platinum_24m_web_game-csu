using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MiningScene"); // Make sure name matches exactly
    }

    public void Credits()
    {
        SceneManager.LoadScene("EndCredits"); // Make sure name matches exactly
    }

    public void QuitGame()
    {
        Debug.Log("QUIT BUTTON CLICKED");

        Application.Quit();

        // This makes quit work inside Unity Editor too
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
