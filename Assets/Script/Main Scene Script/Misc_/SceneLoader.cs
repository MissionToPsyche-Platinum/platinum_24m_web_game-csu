using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string miningSceneName = "MiningScene";
    public string spaceshipSceneName = "SpaceshipScene";

    public void ToggleScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == miningSceneName)
        {
            SceneManager.LoadScene(spaceshipSceneName);
        }
        else if (currentScene == spaceshipSceneName)
        {
            SceneManager.LoadScene(miningSceneName);
        }
    }
}