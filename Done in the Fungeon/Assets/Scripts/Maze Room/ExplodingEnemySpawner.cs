using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemySpawner : MonoBehaviour
{
	public GameObject enemyPrefab;
	public GenerateNav nav;
	public Transform startNode;
	private PlayerGridPos playerPos;
	private Player player;
	
	public float findPlayerTime = 0.1f;
	
    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		playerPos = player.gameObject.GetComponent<PlayerGridPos>();
    }

    // Update is called once per frame
    void Update()
    {
		if(findPlayerTime < 0)
		{
			playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGridPos>();
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}
		else
		{
			findPlayerTime -= Time.deltaTime;
		}
    }
	
	public void CreateEnemy()
	{
		GameObject enemy = Instantiate(enemyPrefab);
		enemy.gameObject.transform.position = transform.position;
		enemy.GetComponent<EnemyAI_MoveTowards>().nav = nav;
		enemy.GetComponent<EnemyAI_MoveTowards>().startNode = startNode;
		enemy.GetComponent<EnemyAI_MoveTowards>().playerPos = playerPos;
		enemy.gameObject.GetComponent<MoveTowardsSwitching>().player = player.transform;
	}
}
