using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    // code adapted from here: https://pressstart.vip/tutorials/2018/06/28/41/keep-object-in-bounds.html
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPosition = transform.position;

        viewPosition.x = Mathf.Clamp(viewPosition.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPosition.y = Mathf.Clamp(viewPosition.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPosition;
    }
}
