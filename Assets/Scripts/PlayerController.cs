using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 startTouch, endTouch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                startTouch = Camera.main.ScreenToWorldPoint(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouch = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 direction = startTouch - endTouch;
                GetComponent<Rigidbody2D>().velocity = direction.normalized * 10;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
