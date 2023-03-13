using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text levelsCompletedText;
    [SerializeField]
    private TMP_Text highScoreText;
    [SerializeField]
    private Button playAgainButton;

    public GameObject demoBallPrefab;
    public GameObject wallPrefab;

    private GameObject ball;
    private List<GameObject> walls;

    // Start is called before the first frame update
    void Start()
    {
        levelsCompletedText.text = $"Levels Completed: {PlayerPrefs.GetInt("LevelsCompleted", 0)}";
        highScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighScore", 0)}";

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

        playAgainButton.onClick.AddListener(async () =>
        {
            playAgainButton.enabled = false;

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
