using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    public Text highScoreText;

    public Text scoreText_UI;
    public Text highScoreText_UI;

    private int score;
    private int highscore;

    //private int playerprefHighScore = PlayerPrefs.GetInt("HighScore");
    // setting up instance
    void Awake(){
        instance = this;
    }

    void start(){

        //setting up Playerprefs
        if(!PlayerPrefs.HasKey("HighScore")){
        PlayerPrefs.SetInt("HighScore", 0);
        }

        //Settings up score and highscore on start and coverting score into string
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    private void Update() {
        //playerprefHighScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore").ToString();

        highScoreText_UI.text = PlayerPrefs.GetInt("HighScore").ToString();
        scoreText_UI.text = score.ToString();

        //RESET HighSCORE
        if (Input.GetKeyDown("r")){
            ResetHighScore();
        }
    }

    public void AddScore(){
        // adding the score
        score += 100;
        scoreText.text = "Score: " + score.ToString();

        //calling the playerhealth reward to give player reward
        PlayerHealth.instance.HealthGain(100);

        //setting up highscore
        if (score > PlayerPrefs.GetInt("HighScore")){
            
            // setting the highscore in ui
            highscore = score;
            
            // soting high score in player prefs
            PlayerPrefs.SetInt("HighScore", highscore);

            //highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore").ToString();

            PlayerPrefs.Save();
        }
    }

    public void ResetHighScore(){
        PlayerPrefs.DeleteAll();
    }

}
