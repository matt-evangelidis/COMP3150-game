using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer : MonoBehaviour
{
	public float findPlayerTime;
	private float findPlayerTimer;
	
	public HoldPlayer holdPlayer;
	
    // Start is called before the first frame update
    void Start()
    {
        findPlayerTimer = findPlayerTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(findPlayerTime > 0)
		{
			findPlayerTime -= Time.deltaTime;
		}
		else
		{
			holdPlayer.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			this.enabled = false;
		}
    }
}
