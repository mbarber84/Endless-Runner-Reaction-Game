using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float score = 0.0f;

    private int difficultylevel = 1;
    private int maxDifficultyLevel = 50;
    private int scoreToNextLevel = 10;

    private bool isDead = false;

    public Text scoreText;

    public DeathMenu deathMenu;


    // Update is called once per frame
    void Update()
    {

        if (isDead)
            return;

        if (score >= scoreToNextLevel)
            LevelUp();


      score +=Time.deltaTime * difficultylevel;
      scoreText.text = ((int)score).ToString();
    }

    void LevelUp()
    {
        if (difficultylevel > maxDifficultyLevel)
            return;

        scoreToNextLevel *= 2;
        difficultylevel++;

        GetComponent<PlayerMotor>().SetSpeed(difficultylevel);

        Debug.Log(difficultylevel);
    }

    public void OnDeath()
    {
        isDead = true;

        //checking highscore is higher or lower to saved
        if (PlayerPrefs.GetFloat("Highscore") < score)
            //built in to unity to save highscore or game in editor Reg
            PlayerPrefs.SetFloat("Highscore", score);

        deathMenu.ToggleEndMenu(score);
    }

}
