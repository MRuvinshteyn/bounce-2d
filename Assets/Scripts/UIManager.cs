using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text remainingBouncesText;

    public string RemainingBounces
    {
        get { return remainingBouncesText.text; }
        set { remainingBouncesText.text = value; }
    }

    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GetComponent<LevelManager>();

        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 10));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 10));

        remainingBouncesText.rectTransform.sizeDelta = new Vector2(
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
