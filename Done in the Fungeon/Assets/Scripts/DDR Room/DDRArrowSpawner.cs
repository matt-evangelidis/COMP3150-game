using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDRArrowSpawner : ObjectPool
{
    public GameObject door;
	public Player player;
	
	private float tempMoveSpeed;
	private float tempDashSpeed;
	
	private float timer;
	
	public GameObject playerDamageZone;
	
	public Transform topSpawnZone;
	public Transform bottomSpawnZone;
	public Transform leftSpawnZone;
	public Transform rightSpawnZone;
	private Transform[] zones;
	
	public float[] times;
	public float[] speeds;
	public Direction[] directions;
	
	public enum Direction
	{
		Up,
		Left,
		Down,
		Right
	};
	
	public struct ArrowData
	{
		public Direction dir;
		public float speed;
		public float time;
	};
	
	private ArrowData[] arrows; // using an array like this because reordering them in the unity inspector is painful
	private Queue<ArrowData> arrowQueue = new Queue<ArrowData>();
	
	void Start()
	{
		zones = new Transform[] {bottomSpawnZone, rightSpawnZone, topSpawnZone, leftSpawnZone};
		
		arrows = new ArrowData[4];
		//ArrowData current = arrows[0];
		arrows[0].dir = Direction.Left;
		arrows[0].speed = 4;
		arrows[0].time = 1;
		
		//current = arrows[1];
		arrows[1].dir = Direction.Right;
		arrows[1].speed = 4;
		arrows[1].time = 1;
		
		//current = arrows[2];
		arrows[2].dir = Direction.Up;
		arrows[2].speed = 4;
		arrows[2].time = 1;
		
		//current = arrows[3];
		arrows[3].dir = Direction.Down;
		arrows[3].speed = 4;
		arrows[3].time = 1;
		
		/*for(int i = 0;i<times.Length;i++)
		{
			ArrowData temp;
		}*/
		
		foreach(ArrowData a in arrows)
		{
			arrowQueue.Enqueue(a);
		}
	}
	
	void Update()
	{
		if(timer > 0)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			if(arrowQueue.Count > 0)
			{
				ArrowData a = arrowQueue.Dequeue();
				
				timer = a.time;
				
				GameObject o = this.GetFromPool();
				SetParams(o, a.speed, a.dir, zones[(int)a.dir]);
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			door.SetActive(true);
			
			tempMoveSpeed = player.moveSpeed;
			player.moveSpeed = 0;
			tempDashSpeed = player.dashSpeed;
			player.dashSpeed = 0;
			player.transform.position = transform.position;
		}
	}
	
	private void SetParams(GameObject arrow, float spe, Direction dir, Transform spawnLocation)
	{
		arrow.transform.eulerAngles = new Vector3(0,0,90*(int)dir);
		arrow.transform.position = spawnLocation.position;
		arrow.GetComponent<Arrow>().speed = spe;
	}
}
