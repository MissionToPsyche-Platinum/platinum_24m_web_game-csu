using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSpaceshipScene()
    {
        SceneManager.LoadScene("SpaceshipScene");
    }
    
}
