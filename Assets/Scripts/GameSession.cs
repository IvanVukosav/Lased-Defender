using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    // Start is called before the first frame update
    void Awake()
    {
        setUpSingleton();
    }

    private  void setUpSingleton()
    {
       
        int numberOfGameSessions = FindObjectsOfType(GetType()).Length;
        if (numberOfGameSessions > 1)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public int Getscore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue; 
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
