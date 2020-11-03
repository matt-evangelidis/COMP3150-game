using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
	public AudioClip[] sounds;
	public AudioSource audioSource;
	private Dictionary<string, int> soundDict;
	
    // Start is called before the first frame update
    void Start()
    {
        soundDict = new Dictionary<string, int>()
		{
			{"Basic Attack 1", 0},
			{"Basic Attack 2", 1},
			{"Charge", 2},
			{"Dash",3},
			{"Heal",4},
			{"Hurt",5}
		};
    }
	
	public void PlaySound(string soundEffectName)
	{
		audioSource.clip = sounds[soundDict[soundEffectName]];
		audioSource.Play();
	}
	
	public void StopSound()
	{
		audioSource.Stop();
	}
}
