using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_TargetPlayer : MonoBehaviour
{
	public Transform target;
	private Vector3 movementVector;
	private Rigidbody2D rb2d;
	public float movementSpeed;
	
	void Awake()
	{
		GameObject player = GameObject.Find("/Player");
		target = player.transform;
	}
	
    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementVector = target.position - transform.position;
		movementVector = movementVector.normalized;
		rb2d.AddForce(movementVector * Time.deltaTime * movementSpeed);
    }
}
