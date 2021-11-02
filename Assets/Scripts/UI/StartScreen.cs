using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public static StartScreen instance;

    private void Awake() {
        instance = this;    
    }
    public void NextSCene(){
        //play sound
        SoundManager.instance.Play("tone1");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
    public void QuitGame(){

        SoundManager.instance.Play("twotone1");
        Application.Quit();
    }
}
