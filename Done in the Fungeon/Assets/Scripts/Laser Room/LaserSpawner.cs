using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
	public float[] times;
	public float[] speeds;
	public int[] moveDirections;
	//public Transform playerPos;
	public GameObject player;
	public Laser laserPrefab;
	
	public GameObject[] stuffToDisable;
	
	public Transform topSpawnLocation;
	public Transform bottomSpawnLocation;
	public Transform leftSpawnLocation;
	public Transform rightSpawnLocation;
	
	private float timer;
	
	public struct LaserData
	{
		public float time;
		public float speed;
		public int moveDirection;
	};
	private Queue<LaserData> laserQueue;
	
	void Awake()
	{
		player = GameObject.Find("/Player");
		//playerPos = player.transform;
	}
	
    // Start is called before the first frame update
    void Start()
    {
		laserQueue = new Queue<LaserData>();
		for(int i = 0;i<times.Length;i++)
		{
			LaserData temp;
			temp.time = times[i];
			temp.speed = speeds[i];
			temp.moveDirection = moveDirections[i];
			
			laserQueue.Enqueue(temp);
		}
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			if(laserQueue.Count > 0)
			{
				LaserData currentData = laserQueue.Dequeue();
				Laser currentLaser = Instantiate(laserPrefab);
				//currentLaser.playerPos = playerPos;
				currentLaser.moveDirection = currentData.moveDirection;
				if(currentLaser.moveDirection == 0 || currentLaser.moveDirection == 1)
				{
					currentLaser.orientation = 1;
					currentLaser.transform.rotation = Quaternion.Euler(Vector3.forward * 90);
				}
				else if (currentLaser.moveDirection == 2 || currentLaser.moveDirection == 3)
				{
					currentLaser.orientation = 0;
				}
				
				switch(currentLaser.moveDirection)
				{
					case 0:
						currentLaser.orientation = 1;
						currentLaser.transform.position = bottomSpawnLocation.position;
						currentLaser.transform.rotation = Quaternion.Euler(Vector3.forward * 90);
						break;
					case 1:
						currentLaser.orientation = 1;
						currentLaser.transform.position = topSpawnLocation.position;
						currentLaser.transform.rotation = Quaternion.Euler(Vector3.forward * 90);
						break;
					case 2:
						currentLaser.orientation = 0;
						currentLaser.transform.position = rightSpawnLocation.position;
						break;
					case 3:
						currentLaser.orientation = 0;
						currentLaser.transform.position = leftSpawnLocation.position;
						break;
				}
				currentLaser.speed = currentData.speed;
				timer = currentData.time;
			}
			else
			{
				player.GetComponent<LevelsComplete>().RoomComplete();
				foreach(GameObject i in stuffToDisable)
				{
					i.SetActive(false);
				}
			}
		}
    }
}
