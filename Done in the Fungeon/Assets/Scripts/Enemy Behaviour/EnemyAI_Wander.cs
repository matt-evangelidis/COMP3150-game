using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Wander : MonoBehaviour
{
	private enum State
	{
		Up,
		Down,
		Left,
		Right,
		Idle
	};
	private State state;
	private Rigidbody2D rb2d;
	public float movementSpeed;
	public float actionRateMax;
	public float actionRateMin;
	private float actionRate;
	private int[] weights = {0,1,2,3,4,4,4,4};
	private int action;
	
    // Start is called before the first frame update
    void Start()
    {
        action = Random.Range(0,weights.Length);
		state = (State)weights[action];
		actionRate = Random.Range(actionRateMin, actionRateMax);
		rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
		{
			case State.Up:
				if(actionRate > 0)
				{
					actionRate -= Time.deltaTime;
					rb2d.AddForce(Vector3.up * movementSpeed * Time.deltaTime);
				}
				else
				{
					generateNextAction();
				}
				break;
			case State.Down:
				if(actionRate > 0)
				{
					actionRate -= Time.deltaTime;
					rb2d.AddForce(Vector3.down * movementSpeed * Time.deltaTime);
				}
				else
				{
					generateNextAction();
				}
				break;
			case State.Left:
				if(actionRate > 0)
				{
					actionRate -= Time.deltaTime;
					rb2d.AddForce(Vector3.left * movementSpeed * Time.deltaTime);
				}
				else
				{
					generateNextAction();
				}
				break;
			case State.Right:
				if(actionRate > 0)
				{
					actionRate -= Time.deltaTime;
					rb2d.AddForce(Vector3.right * movementSpeed * Time.deltaTime);
				}
				else
				{
					generateNextAction();
				}
				break;
			case State.Idle:
				if(actionRate > 0)
				{
					actionRate -= Time.deltaTime;
				}
				else
				{
					generateNextAction();
				}
				break;
		}
    }
	
	void generateNextAction()
	{
		action = Random.Range(0,weights.Length);
		state = (State)weights[action];
		actionRate = Random.Range(actionRateMin, actionRateMax);
	}
}
