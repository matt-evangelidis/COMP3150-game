using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    // Ref: https://www.youtube.com/watch?v=3uyolYVsiWc
    // Get child in prefab ref: https://answers.unity.com/questions/894211/set-objects-child-to-activeinactive.html

    public int health;
    public int numOfHearts;

    public GameObject[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

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
                hearts[i].transform.GetChild(1).gameObject.SetActive(false);
                hearts[i].transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                hearts[i].transform.GetChild(1).gameObject.SetActive(true);
                hearts[i].transform.GetChild(2).gameObject.SetActive(true);
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
    }
}
