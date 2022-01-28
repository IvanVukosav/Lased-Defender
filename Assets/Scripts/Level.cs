using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level : MonoBehaviour
{
    
    public void LoadStartMenu()
   {
        SceneManager.LoadScene(0);
   }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());//prvo izvrsi kod pa onda zove, barem sam skuzio da to radi pomocu load scene
                                      //ako tu stoji load scena umjesto di je, ne ceka mi onda 2 sekunde
        

    }
    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOver");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
