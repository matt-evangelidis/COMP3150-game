using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleRoomSpawner : MonoBehaviour
{
	public int rounds;
	public float timeToMatch;
	private float timerToMatch;
	public Transform[] spawnSpotsTop;
	public Transform[] spawnSpotsBottom;
	public GameObject damageZone;
	
	public float damageZoneTime;
	private float damageZoneTimer;
	
	public DoubleButton button1;
	public DoubleButton button2;
	
	public Player player;
	public GameObject middleWall;
	
	private Color currentColour;
	
    // Start is called before the first frame update
    void Start()
    {
		timerToMatch = timeToMatch;
		button1.transform.position = spawnSpotsTop[Random.Range(0,spawnSpotsTop.Length)].position;
		button2.transform.position = spawnSpotsBottom[Random.Range(0,spawnSpotsBottom.Length)].position;
    }

    // Update is called once per frame
    void Update()
    {
		if(rounds > 0) {
			if(timerToMatch > 0)
			{
				timerToMatch -= Time.deltaTime;
				currentColour = new Color(1.0f, timerToMatch/timeToMatch, timerToMatch/timeToMatch, 1.0f);
				button1.currentColour = currentColour;
				button2.currentColour = currentColour;
			}
			else
			{
				timerToMatch = timeToMatch;
				damageZoneTimer = damageZoneTime;
				button1.transform.position = spawnSpotsTop[Random.Range(0,spawnSpotsTop.Length)].position;
				button2.transform.position = spawnSpotsBottom[Random.Range(0,spawnSpotsBottom.Length)].position;
				rounds -= 1;
			}
			
			if(damageZoneTimer > 0)
			{
				damageZone.SetActive(true);
				damageZoneTimer -= Time.deltaTime;
			}
			else
			{
				damageZone.SetActive(false);
			}
			
			if(button1.pressed && button2.pressed)
			{
				timerToMatch = timeToMatch;
				button1.transform.position = spawnSpotsTop[Random.Range(0,spawnSpotsTop.Length)].position;
				button2.transform.position = spawnSpotsBottom[Random.Range(0,spawnSpotsBottom.Length)].position;
				rounds -= 1; //runs multiple times when both buttons are pressed
			}
		}
		else 
		{
			currentColour = Color.white;
			player.gameObject.SetActive(false);
			middleWall.SetActive(false);
		}
    }
}
