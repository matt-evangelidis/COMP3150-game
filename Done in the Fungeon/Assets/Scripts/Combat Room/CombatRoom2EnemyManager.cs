using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoom2EnemyManager : MonoBehaviour
{
	public GameObject enemy0;
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	public GameObject enemy4;
	public GameObject enemy5;
	public GameObject enemy6;
	
	public GameObject[] exitDoors;

    // Update is called once per frame
    void Update()
    {
        if(enemy0 == null && enemy1 == null && enemy2 == null && enemy3 == null && enemy4 == null && enemy5 == null && enemy6 == null)
		{
			foreach(GameObject i in exitDoors)
			{
				i.SetActive(false);
			}
		}
    }
}
