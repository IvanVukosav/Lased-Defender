using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreDisplay : MonoBehaviour
{
    GameSession gameSession;
  //  Text scoreText;
    TextMeshProUGUI scoreText;
    void Awake()
    {
       scoreText=GetComponent<TextMeshProUGUI>();
       gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = gameSession.Getscore().ToString();
    }
}
