using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSumoRoom : MonoBehaviour
{
	public EnemyAI_MoveTowards enemy;
	public GameObject lava;
	public GameObject exitDoor;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy == null)
		{
			lava.SetActive(false);
			exitDoor.SetActive(false);
		}
    }
}
