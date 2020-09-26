using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDestroyer : MonoBehaviour
{
	public SpriteRenderer spriteRender;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerStay2D(Collider2D c)
	{
		spriteRender.color = Color.red;
	}
	
	void OnTriggerExit2D(Collider2D c)
	{
		spriteRender.color = Color.white;
	}
}
