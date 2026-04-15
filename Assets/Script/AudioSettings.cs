using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    public static AudioSettings instance;

    public AudioMixer mixer;

    private float lastVolume = 1f;
    private bool isMuted = false;

    public bool IsMuted => isMuted;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        float saved = PlayerPrefs.GetFloat("volume", 1f);
        SetVolume(saved);
    }

    public void SetVolume(float value)
    {
        lastVolume = value;

        PlayerPrefs.SetFloat("volume", value);

        if (!isMuted)
            ApplyVolume(value);
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
            mixer.SetFloat("MasterVolume", -80f);
        else
            ApplyVolume(lastVolume);
    }

    void ApplyVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }
}