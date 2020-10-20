using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Finds the player and gives it a reference to the camera for camera shake
public class SetCamShake : MonoBehaviour
{
	public float timeBeforeSet = 0.1f;
	public CameraShake camShake;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBeforeSet < 0)
		{
			Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			player.camShake = camShake;
			this.enabled = false;
		}
		else
		{
			timeBeforeSet -= Time.deltaTime;
		}
    }
}
