using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    


    public Sprite[] lives;


    public Image livesDisplay;

    public Text scoreDisplay;

    public Image menuDisplay;

    public int score;


    void Start()
    {
        scoreDisplay.enabled = false;
        livesDisplay.enabled = false;

    }


    public void UpdateLives(int currentLives)
    {
        livesDisplay.sprite = lives[currentLives];
    }



    public void UpdateScore()
    {
        score += 100;
        scoreDisplay.text = "Score: " + score;

    }


    public void ShowTitleScreen()
    {
        menuDisplay.enabled = false;

        scoreDisplay.enabled = true;
        livesDisplay.enabled = true;
        score = 0;
        scoreDisplay.text = "Score " + score;

    }

    public void HideTitleScreen()
    {
        menuDisplay.enabled= true;
        scoreDisplay.enabled = false;
        livesDisplay.enabled = false;
    }
	
}
