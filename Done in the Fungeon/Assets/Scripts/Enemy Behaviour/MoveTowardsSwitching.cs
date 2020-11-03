using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For chasing enemies, they're not very good at hitting the player with regular targeting alone, so I'll have it so that enemies will move straight towards the player if they are within a certain distance.
public class MoveTowardsSwitching : MonoBehaviour
{
	public Transform player;
	public MonoBehaviour farBehaviour;
	public MonoBehaviour closeBehaviour;
	public float switchDistance;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < switchDistance)
		{
			closeBehaviour.enabled = true;
			farBehaviour.enabled = false;
		}
		else
		{
			closeBehaviour.enabled = false;
			farBehaviour.enabled = true;
		}
    }
}
