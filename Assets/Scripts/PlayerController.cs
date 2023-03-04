using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 startTouch, endTouch;

    private GameObject gameManager;
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        levelManager = gameManager.GetComponent<LevelManager>();
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
        if (collision.collider.gameObject.layer == 6)
        {
            levelManager.RemainingBounces--;
            Renderer renderer = collision.collider.GetComponent<Renderer>();

            if (!collision.collider.GetComponent<WallController>().IsEnd)
            {
                DOTween.To(() => renderer.material.GetColor("_BaseColor"),
                    (color) => renderer.material.SetColor("_BaseColor", color),
                    Constants.BASE_WALL_COLOR,
                    1f)
                    .From(Constants.HIT_WALL_COLOR);
            }
            else
            {
                DOTween.To(() => renderer.material.GetColor("_BaseColor"),
                    (color) => renderer.material.SetColor("_BaseColor", color),
                    Constants.END_WALL_COLOR,
                    1f)
                    .From(Constants.HIT_WALL_COLOR);
            }
        }
    }
}
