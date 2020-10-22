using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public EnemyAI_MoveTowards enemyPrefab;
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
        if(Input.GetKeyDown("t"))
		{
			CreateEnemy();
		}
		
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
		EventTracking.enemiesSpawned++;
		
		EnemyAI_MoveTowards enemy = Instantiate(enemyPrefab);
		enemy.gameObject.transform.position = transform.position;
		enemy.nav = nav;
		enemy.startNode = startNode;
		enemy.playerPos = playerPos;
		enemy.gameObject.GetComponent<EnemyAI_TargetPlayer>().target = player.transform;
		enemy.gameObject.GetComponent<MoveTowardsSwitching>().player = player.transform;
	}
}
