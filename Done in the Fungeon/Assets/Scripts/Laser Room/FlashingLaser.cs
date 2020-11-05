using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLaser : MonoBehaviour
{
	public float flashPeriod;
	private float flashTimer;
	public float offset;
	
	public GameObject onLaser;
	public GameObject offLaser;
	
    // Start is called before the first frame update
    void Start()
    {
        flashTimer = flashPeriod + offset;
    }

    // Update is called once per frame
    void Update()
    {
        if(flashTimer > flashPeriod/2)
		{
			flashTimer -= Time.deltaTime;
			onLaser.SetActive(true);
			offLaser.SetActive(false);
		}
		else if(flashTimer <= flashPeriod/2 && flashTimer > 0)
		{
			flashTimer -= Time.deltaTime;
			onLaser.SetActive(false);
			offLaser.SetActive(true);
		}
		else
		{
			flashTimer = flashPeriod;
		}
    }
}
