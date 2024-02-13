using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSaver : MonoBehaviour
{
    public int flyCount;
    public Timer gameTimer;
    private float gameTime;
    public TextMeshProUGUI flyCountText;
    private GameObject playerCharacter;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        gameTime = gameTimer.currentTime;
        flyCountText.text = flyCount.ToString("F0");

        if (playerCharacter == null)
        {
            PlayerPrefs.SetInt("FinalFlies", flyCount);
            PlayerPrefs.SetFloat("FinalTime", gameTime);
        }
    }
}
