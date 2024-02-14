using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreLoader : MonoBehaviour
{
    private int finalFlies;
    private int finalTime;
    private int finalScore;
    public TextMeshProUGUI finalFliesText;
    public TextMeshProUGUI finalTimeText;
    public TextMeshProUGUI finalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        finalFlies = PlayerPrefs.GetInt("FinalFlies", 0);
        finalTime = (int)PlayerPrefs.GetFloat("FinalTime", 0);

        finalScore = (int)((finalFlies * finalTime) / 10);

        finalFliesText.text = finalFlies.ToString("F0");
        finalTimeText.text = finalTime.ToString("F0");
        finalScoreText.text = finalScore.ToString("F0");
    }
}