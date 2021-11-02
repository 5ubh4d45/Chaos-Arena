using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    public static Instructions instance;

    private void Awake() {
        instance = this;    
    }
    public void NextSCene(){
        //play sound
        SoundManager.instance.Play("tone1");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
    public void PreviuosScene(){

        SoundManager.instance.Play("twotone1");
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
