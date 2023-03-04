using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text levelsCompletedText;
    [SerializeField]
    private Button playAgainButton;

    // Start is called before the first frame update
    void Start()
    {
        levelsCompletedText.text = $"Levels Completed: {PlayerPrefs.GetInt("LevelsCompleted", 0)}";
        playAgainButton.onClick.AddListener(() => SceneManager.LoadScene("Game"));
    }
}
