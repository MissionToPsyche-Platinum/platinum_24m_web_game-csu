using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMainMenu : MonoBehaviour
{
    public GameObject confirmExitPopup;

    public void OnExitButtonClicked()
    {
        confirmExitPopup.SetActive(true);
    }

    public void OnConfirmYes()
    {
        Time.timeScale = 1f;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        //SceneManager.LoadScene("MainMenu");
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.currency = 500; // Reset currency to default value

        }
        if (MaterialManager.Instance != null)
        {
            MaterialManager.Instance.materials = 0; // Reset materials to default value
        }
        SceneManager.LoadScene("MainMenu");

    }

    public void OnConfirmNo()
    {
        confirmExitPopup.SetActive(false);
    }
}
