using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSequence : MonoBehaviour
{
	public float fadeWhiteTime;
	private float fadeWhiteTimer;
	public float fadeBlackTime;
	private float fadeBlackTimer;
	public float fadeTextTime;
	private float fadeTextTimer;
	public float returnToMenuTime;
	private float returnToMenuTimer;
	private bool started = false;
	private bool fadeBlack = false;
	private bool showText = false;
	
	public Image image;
	public Text text;
	public Text otherText;
	
    // Start is called before the first frame update
    void Start()
    {
        fadeWhiteTimer = fadeWhiteTime;
		fadeBlackTimer = fadeBlackTime;
		fadeTextTimer = fadeTextTime;
		returnToMenuTimer = returnToMenuTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(started)
		{
			if(fadeWhiteTimer > 0)
			{
				fadeWhiteTimer -= Time.deltaTime;
				image.color = new Color(image.color.r, image.color.g, image.color.b, 1-(fadeWhiteTimer/fadeWhiteTime));
			}
			else
			{
				fadeBlack = true;
			}
		}
		
		if(fadeBlack)
		{
			if(fadeBlackTimer > 0)
			{
				fadeBlackTimer -= Time.deltaTime;
				image.color = new Color(fadeBlackTimer/fadeBlackTime, fadeBlackTimer/fadeBlackTime, fadeBlackTimer/fadeBlackTime, 1);
			}
			else
			{
				showText = true;
			}
		}
		
		if(showText)
		{
			if(fadeTextTimer > 0)
			{
				fadeTextTimer -= Time.deltaTime;
				text.color = new Color(text.color.r, text.color.g, text.color.b, 1-(fadeTextTimer/fadeTextTime));
				otherText.color = new Color(otherText.color.r, otherText.color.g, otherText.color.b, 1-(fadeTextTimer/fadeTextTime));
			}
			else
			{
				if(returnToMenuTimer > 0)
				{
					returnToMenuTimer -= Time.deltaTime;
				}
				else
				{
					PauseMenu pause = GameObject.Find("/In-Game Canvas").GetComponent<PauseMenu>();
					pause.BackToMainMenu();
				}
			}
		}
    }
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			started = true;
		}
	}
}
