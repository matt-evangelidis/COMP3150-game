using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetKey : MonoBehaviour
{
	public BallReset resetter;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r"))
		{
			resetter.Reset();
		}
    }
}
