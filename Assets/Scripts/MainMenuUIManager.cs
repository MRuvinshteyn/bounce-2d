using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField]
    private Button playButton;

    public GameObject demoBallPrefab;
    public GameObject wallPrefab;

    private GameObject ball;
    private List<GameObject> walls;

    // Start is called before the first frame update
    void Start()
    {
        float maxHeight = Camera.main.pixelHeight;
        float maxWidth = Camera.main.pixelWidth;

        walls = new();

        // create four border walls
        walls.Add(Instantiate(wallPrefab, // top
            (Vector2)Camera.main.ScreenToWorldPoint(new Vector2(maxWidth / 2f, maxHeight)), Quaternion.identity));
        walls.Add(Instantiate(wallPrefab, // bottom
            (Vector2)Camera.main.ScreenToWorldPoint(new Vector2(maxWidth / 2f, 0f)), Quaternion.identity));
        walls.Add(Instantiate(wallPrefab, // left
            (Vector2)Camera.main.ScreenToWorldPoint(new Vector2(0f, maxHeight / 2f)), Quaternion.identity));
        walls.Add(Instantiate(wallPrefab, // right
            (Vector2)Camera.main.ScreenToWorldPoint(new Vector2(maxWidth, maxHeight / 2f)), Quaternion.identity));

        // resize walls
        walls[0].transform.localScale = new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(maxWidth, 0f)).x * 2f, 1);
        walls[1].transform.localScale = new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(maxWidth, 0f)).x * 2f, 1);
        walls[2].transform.localScale = new Vector2(1, Camera.main.ScreenToWorldPoint(new Vector2(0f, maxHeight)).y * 2f);
        walls[3].transform.localScale = new Vector2(1, Camera.main.ScreenToWorldPoint(new Vector2(0f, maxHeight)).y * 2f);

        // create demo ball
        ball = Instantiate(demoBallPrefab, new Vector2(), Quaternion.identity);

        playButton.onClick.AddListener(async () =>
        {
            playButton.enabled = false;

            ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            foreach (GameObject wall in walls)
            {
                Renderer renderer = wall.GetComponent<Renderer>();
                DOTween.To(() => renderer.material.GetFloat("_Edge"),
                    (edge) => renderer.material.SetFloat("_Edge", edge),
                    1f,
                    1f)
                    .OnComplete(() => Destroy(wall));
            }
            walls.Clear();

            Renderer ballRenderer = ball.GetComponent<Renderer>();
            DOTween.To(() => ballRenderer.material.GetFloat("_Edge"),
                (edge) => ballRenderer.material.SetFloat("_Edge", edge),
                1f,
                1f)
                .OnComplete(() => Destroy(ball));

            while (ball != null)
            {
                await System.Threading.Tasks.Task.Delay(50);
            }
            SceneManager.LoadScene("Game");
        });
    }
}
