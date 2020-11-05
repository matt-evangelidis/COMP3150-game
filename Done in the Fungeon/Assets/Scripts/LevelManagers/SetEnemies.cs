using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemies : MonoBehaviour
{
	public GameObject[] enemies;
	
	public float setPlayerTime = 0.2f;
	private float setPlayerTimer;
	
	public HoldPlayer holdPlayer;
	
    void Start()
    {
        setPlayerTimer = setPlayerTime;
    }

    void Update()
    {
        if(setPlayerTimer > 0)
		{
			setPlayerTimer -= Time.deltaTime;
		}
		else
		{
			foreach(GameObject i in enemies)
			{
				i.GetComponent<MoveTowardsSwitching>().player = holdPlayer.player.transform;
				i.GetComponent<EnemyAI_MoveTowards>().playerPos = holdPlayer.player.GetComponent<PlayerGridPos>();
				i.GetComponent<EnemyAI_TargetPlayer>().target = holdPlayer.player.transform;
			}
		}
    }
}
