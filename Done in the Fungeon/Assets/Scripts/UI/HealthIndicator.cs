using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : Singleton<HealthIndicator>
{
    // Ref: https://www.youtube.com/watch?v=3uyolYVsiWc
    // Get child in prefab ref: https://answers.unity.com/questions/894211/set-objects-child-to-activeinactive.html

    public int health; // aka. currentHP of player
    public int numOfHearts; // aka. maxHP of player

    public GameObject[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    protected HealthIndicator() { }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].transform.GetChild(1).gameObject.SetActive(true); // heart_half
                hearts[i].transform.GetChild(2).gameObject.SetActive(true); // heart_full
            }
            else
            {
                hearts[i].transform.GetChild(1).gameObject.SetActive(false);
                hearts[i].transform.GetChild(2).gameObject.SetActive(false);
            }

            if (i < numOfHearts)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
        if (health <= 0)
        {
            Debug.Log("Game Over");
            Time.timeScale = 0f;
        }
    }
}
