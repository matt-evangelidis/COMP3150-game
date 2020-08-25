using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float velocity = moveSpeed * Time.deltaTime;

        float verticalMove = Input.GetAxis("Vertical") * velocity;
        float horizontalMove = Input.GetAxis("Horizontal") * velocity;

        transform.Translate(horizontalMove, verticalMove, 0);

/*        if (Input.GetButton("up"))
        {
            transform.Translate(Vector3.up * verticalMove);
        }

        else if (Input.GetButton("down"))
        {
            transform.Translate(Vector3.down * verticalMove);
        }

        if (Input.GetButton("left"))
        {
            transform.Translate(Vector3.left * horizontalMove);
        }

        else if (Input.GetButton("right"))
        {
            transform.Translate(Vector3.right * horizontalMove);
        }*/
    }
}
