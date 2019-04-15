using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BackgroundMusicController : MonoBehaviour
{
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Assert.IsNotNull(menuMusic);
        Assert.IsNotNull(gameMusic);
        Assert.IsNotNull(audioSource);
    }


    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        var clipToPlay = GameManager.instance.GameStarted ? gameMusic : menuMusic;
        PlayMusic(clipToPlay);
    }

    private void PlayMusic(AudioClip _clipToPlay)
    {
        if (audioSource.clip != _clipToPlay)
        {
            audioSource.clip = _clipToPlay;
            audioSource.Play(); 
        }
    }
}
