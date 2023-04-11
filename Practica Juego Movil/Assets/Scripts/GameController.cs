using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    public int highScore;
    public int actualScore;   
    public int level = 0;

    public static GameController singleton;

    void Awake()
    {

        if(singleton == null)
            singleton = this;
        else if(singleton!=this)
            Destroy(gameObject);
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    public void LevelUp()
    {
        level++;
        FindObjectOfType<PlayerController>().resetPlayer();
        FindObjectOfType<PilarController>().LoadLevel(level);
    }

    public void Restart()
    {
        singleton.actualScore = 0;
        FindObjectOfType<PlayerController>().resetPlayer();
        FindObjectOfType<PilarController>().LoadLevel(level);
    }

    public void Score(int score)
    {
        actualScore += score;

        if (actualScore > highScore)
            highScore = actualScore;
            PlayerPrefs.SetInt("HighScore", actualScore);
        
    }

}
