using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour
{
    private int highscore;
    TextMeshProUGUI highscoreText;


    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
        highscoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Score.score > highscore)
        {
            highscore = Score.score;
            PlayerPrefs.SetInt("Highscore", highscore);
        }

        highscoreText.SetText("Highscore: " + highscore);

    }
}
