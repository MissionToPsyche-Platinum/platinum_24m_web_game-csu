using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneloader : MonoBehaviour
{
    public void LoadEndCredits()
    {
        SceneManager.LoadScene("EndCredits");
    }
}