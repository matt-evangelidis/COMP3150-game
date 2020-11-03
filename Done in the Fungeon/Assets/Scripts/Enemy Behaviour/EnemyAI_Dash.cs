using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Dash : MonoBehaviour
{
	public Transform target;
	
	private Vector3 dashDirection;
	
	private Rigidbody2D rb2d;
	
	public float speed;
	
    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
	
	void OnEnable()
	{
		dashDirection = target.position - transform.position;
		dashDirection = dashDirection.normalized;
	}

    // Update is called once per frame
    void Update()
    {
        rb2d.AddForce(dashDirection * speed * Time.deltaTime);
    }
}
