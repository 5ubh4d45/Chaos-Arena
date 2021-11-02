using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]

public class Sounds
{   

    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;
    public bool loop;

    [HideInInspector]
    public AudioSource source;

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}

