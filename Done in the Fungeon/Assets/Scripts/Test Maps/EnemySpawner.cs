using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public EnemyAI_MoveTowards enemyPrefab;
	public GenerateNav nav;
	public Transform startNode;
	public PlayerGridPos playerPos;
	
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("/Player");
		playerPos = player.GetComponent<PlayerGridPos>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("t"))
		{
			EnemyAI_MoveTowards enemy = Instantiate(enemyPrefab);
			enemy.gameObject.transform.position = transform.position;
			enemy.nav = nav;
			enemy.startNode = startNode;
			enemy.playerPos = playerPos;
		}
    }
}
