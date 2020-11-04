using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	// I originally used enums for this, but they're not accessible outside of the class
	public int orientation; // 0 = vertical, 1 = horizontal
	public int moveDirection; // 0 = up, 1 = down, 2 = left, 3 = right
	
	public Transform playerPos;
	public Damager damager;
	public float speed;
	private Vector3 knockbackPos;
	
	public float lifeTime = 10f;
	private float lifeTimer;
	
	void Start()
	{
		damager.disableSource = true;
		lifeTimer = lifeTime;
	}

    void Update()
    {
		// This code is for the laser's knockback. It just modifies the laser's damager so that its source is always following the player around. This ensures uniform knockback no matter where the player touches the beam.
		switch(orientation)
		{
			case 0: // vertical
				if(transform.position.x < playerPos.position.x) // player is to the right of the laser
				{
					knockbackPos = new Vector3(playerPos.position.x - 1, playerPos.position.y, playerPos.position.z);
					damager.source = knockbackPos;
				}
				else // player is to the left of the laser
				{
					knockbackPos = new Vector3(playerPos.position.x + 1, playerPos.position.y, playerPos.position.z);
					damager.source = knockbackPos;
				}
				break;
			case 1: // horizontal
				if(transform.position.y < playerPos.position.y) // player is above the laser
				{
					knockbackPos = new Vector3(playerPos.position.x, playerPos.position.y - 1, playerPos.position.z);
					damager.source = knockbackPos;
				}
				else // player is below the laser
				{
					knockbackPos = new Vector3(playerPos.position.x, playerPos.position.y + 1, playerPos.position.z);
					damager.source = knockbackPos;
				}
				break;
		}
		
		switch(moveDirection)
		{
			case 0:
				transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
				break;
			case 1:
				transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
				break;
			case 2:
				transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
				break;
			case 3:
				transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
				break;
		}
		
		lifeTimer -= Time.deltaTime;
		if(lifeTimer < 0)
		{
			Destroy(gameObject);
		}
    }
}
