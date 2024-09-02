using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------Audio Source---------")]
    [SerializeField] AudioSource bgmSource;
    [Header("------Audio Clip---------")]
    public AudioClip bgmMusic;
   

    private void Start() 
    {
        bgmSource.clip = bgmMusic;
        bgmSource.Play();
    }

}
