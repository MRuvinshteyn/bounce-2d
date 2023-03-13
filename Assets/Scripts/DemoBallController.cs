using DG.Tweening;
using UnityEngine;

public class DemoBallController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 10;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 6)
        {
            Renderer renderer = collision.collider.GetComponent<Renderer>();

            DOTween.To(() => renderer.material.GetColor("_BaseColor"),
                (color) => renderer.material.SetColor("_BaseColor", color),
                Constants.BASE_WALL_COLOR,
                1f)
                .From(Constants.HIT_WALL_COLOR);
        }
    }
}
