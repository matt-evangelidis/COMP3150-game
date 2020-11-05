using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoom3 : MonoBehaviour
{
	public GameObject enemy0;
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	public GameObject enemy4;
	public GameObject enemy5;
	
	public GameObject[] exitDoors;
	
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy0 == null && enemy1 == null && enemy2 == null && enemy3 == null && enemy4 == null && enemy5 == null)
		{
			foreach(GameObject i in exitDoors)
			{
				i.SetActive(false);
			}
			GameObject player = GameObject.Find("/Player");
			player.GetComponent<LevelsComplete>().RoomComplete();
		}
    }
}
