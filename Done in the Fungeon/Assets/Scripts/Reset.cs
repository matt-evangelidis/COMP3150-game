using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
	public Transform laserSpawner;
	
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown("r"))
		{
			SceneManager.LoadScene("Laser Room");
		}
		
		if(Input.GetKeyDown("t"))
		{
			laserSpawner.gameObject.SetActive(true);
		}
    }
}
