#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class EditorQuitHandler
{
    static EditorQuitHandler()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
#endif