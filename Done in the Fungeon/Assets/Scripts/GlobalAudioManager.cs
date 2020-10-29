using Unity.Collections;
using UnityEngine;
using UnityEngine.Audio;

// code adapted from https://blog.csdn.net/linjf520/article/details/104329047
/// <summary>
/// author      :   jave.lin
/// date        :   2020.02.15
/// sound management
/// </summary>
public class GlobalAudioManager : MonoBehaviour
{
    private static GlobalAudioManager instance;
    public static GlobalAudioManager Instance() => instance;
    [Header("Mixer of BGM and SFX")]
    public AudioMixer Mixer;
    [Header("Current playing BGM")]
    [SerializeField] [ReadOnly] private AudioSource playing_BGM;

    [Header("Testing")]
    [SerializeField] [Range(-80f, 20f)] private float master_volume = -10;
    [SerializeField] [Range(-80f, 20f)] private float bgm_volume = -10;
    [SerializeField] [Range(-80f, 20f)] private float sfx_volume = -10;

    private void Awake()
    {
        if (instance != null) Debug.LogError($"{GetType().Name} instance already exist.");
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        PlayBGM(GetComponent<AudioSource>()); // for testing
    }
    // Translate: For testing purposes. 
    // I will put it in the Update() to update in real time.
    // Officially you can set the volume on UI panel volume slider.
    private void Update()
    {
        SetMASTER_Volume(master_volume);
        SetBGM_Volume(bgm_volume);
        SetSFX_Volume(sfx_volume);
    }
    
    public void RestartBGM()
    {
        if (playing_BGM != null)
        {
            playing_BGM.time = 0;
        }
    }

    public void PlayBGM(AudioSource bgm)
    {
        if (playing_BGM != null) playing_BGM.Stop();
        playing_BGM = bgm;
        playing_BGM.outputAudioMixerGroup = Mixer.FindMatchingGroups("BGM")[0];
        playing_BGM.Play();
    }

    public void PlaySfx(AudioSource sfx)
    {
        sfx.outputAudioMixerGroup = Mixer.FindMatchingGroups("SFX")[0];
        sfx.Play();
    }

    public float GetMASTER_Volume()
    {
        Mixer.GetFloat("MASTER_Volume", out float value);
        return value;
    }

    public void SetMASTER_Volume(float value)
    {
        Mixer.SetFloat("MASTER_Volume", Mathf.Clamp(value, -80, 20));
    }

    public float GetBGM_Volume()
    {
        Mixer.GetFloat("BGM_Volume", out float value);
        return value;
    }

    public void SetBGM_Volume(float value)
    {
        Mixer.SetFloat("BGM_Volume", Mathf.Clamp(value, -80, 20));
    }

    public float GetSFX_Volume()
    {
        Mixer.GetFloat("SFX_Volume", out float value);
        return value;
    }

    public void SetSFX_Volume(float value)
    {
        Mixer.SetFloat("SFX_Volume", Mathf.Clamp(value, -80, 20));
    }
}