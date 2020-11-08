using Unity.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// code adapted from https://blog.csdn.net/linjf520/article/details/104329047
/// <summary>
/// author      :   jave.lin
/// date        :   2020.02.15
/// sound management
/// </summary>
public class GlobalAudioManager : MonoBehaviour
{
    public Slider bgm_slider;
    public Slider sfx_slider;

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



    AudioSource source;
	
	public AudioSource sounds;
	public AudioSource music;


    private void Awake()
    {
        if (instance != null)
		{
			//Debug.LogError($"{GetType().Name} instance already exist.");
			Destroy(gameObject);
			instance = this;
		}
		else if(instance != this)
		{
			DontDestroyOnLoad(this);
			DontDestroyOnLoad(sounds);
			DontDestroyOnLoad(music);
		}
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();

        // restore the saved audio volume
        if (PlayerPrefs.HasKey("SFX_Volume"))
        {
            Mixer.SetFloat("SFX_Volume", PlayerPrefs.GetFloat("SFX_Volume"));
            sfx_slider.value = PlayerPrefs.GetFloat("SFX_Volume");
        }
        else
        {
            // first time the game is run, use the default value
            Mixer.SetFloat("SFX_Volume", 0f);
            sfx_slider.value = 0f;
        }

        // restore the saved background audio volume
        if (PlayerPrefs.HasKey("BGM_Volume"))
        {
            Mixer.SetFloat("BGM_Volume", PlayerPrefs.GetFloat("BGM_Volume"));
            bgm_slider.value = PlayerPrefs.GetFloat("BGM_Volume");
        }
        else
        {
            Mixer.SetFloat("BGM_Volume", -15f);
            bgm_slider.value = -15f;
        }

        PlayBGM(GetComponent<AudioSource>()); // for testing
    }
    // Translate: For testing purposes. 
    // I will put it in the Update() to update in real time.
    // Officially you can set the volume on UI panel volume slider.
    private void Update()
    {

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

    public void SetMASTER_Volume(Slider volume)
    {
        Mixer.SetFloat("MASTER_Volume", volume.value);
        PlayerPrefs.SetFloat("MASTER_Volume", volume.value);
        PlayerPrefs.Save();
    }

    public float GetBGM_Volume()
    {
        Mixer.GetFloat("BGM_Volume", out float value);
        return value;
    }

    public void SetBGM_Volume(Slider volume)
    {
        Mixer.SetFloat("BGM_Volume", volume.value);
        PlayerPrefs.SetFloat("BGM_Volume", volume.value);
        PlayerPrefs.Save();
    }

    public float GetSFX_Volume()
    {
        Mixer.GetFloat("SFX_Volume", out float value);
        return value;
    }

    public void SetSFX_Volume(Slider volume)
    {
        Mixer.SetFloat("SFX_Volume", volume.value);
        PlayerPrefs.SetFloat("SFX_Volume", volume.value);
        PlayerPrefs.Save();
    }
}