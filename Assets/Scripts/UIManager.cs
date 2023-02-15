using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private int remainingBounces;
    [SerializeField]
    private TMP_Text remainingBouncesText;

    public int RemainingBounces {
        get { return remainingBounces; }
        set
        {
            remainingBounces = value;
            remainingBouncesText.text = $"{remainingBounces}";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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
