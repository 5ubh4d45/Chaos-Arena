using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public Sounds [] sounds;

    public static SoundManager instance;

    void Awake() {
        if (instance == null){
            instance = this;
        
        }
        else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach(Sounds s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            
            s.source.loop = s.loop;
        }
        Play("Music");
    }

    void Update(){
        if (SceneManager.GetActiveScene().buildIndex == 2 && PauseUI.instance.GameIsPaused == false){
            AdjustVolume("Music", 0.3f);
        } else {
            ResetVolume("Music");
        }
    }

    public void Play(string name){
       Sounds s = Array.Find(sounds, sound => sound.name == name);
        
       if (s==null){
           Debug.LogWarning("Sound: " + name + " not found!");
           //return;
       }
        s.source.Play();
    }

    public void AdjustVolume(string name, float volume){
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        
       if (s==null){
           Debug.LogWarning("Sound: " + name + " not found!");
           //return;
       }
        s.source.volume = volume;
    }

    public void ResetVolume(string name){
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        
       if (s==null){
           Debug.LogWarning("Sound: " + name + " not found!");
           //return;
       }
        s.source.volume = s.volume;
    }

    public void Stop(string name){
       Sounds s = Array.Find(sounds, sound => sound.name == name);
        
       if (s==null){
           Debug.LogWarning("Sound: " + name + " not found!");
           //return;
       }
        s.source.Stop();
    }


}
