using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public enum OnBeat { BEAT, OFFBEAT};

public class RythmManager : MonoBehaviour
{
    public static RythmManager Instance;

    [SerializeField] AudioClip hundredTwentyBPM, hundredFiftyBPM;

    [SerializeField]
    float BPM = 120;

    [SerializeField]
    bool changeBpm = false;

    public OnBeat CurrentBeat = OnBeat.OFFBEAT;

    [SerializeField]
    AudioSource music;

    private void Awake()
    {
        Instance = this;
        
    }

    IEnumerator Start()
    {
        StartCoroutine(Beats());
        yield return new WaitForSeconds((60 / BPM) / 2);
        music.Play();
    }

    private void Update()
    {
        if (changeBpm)
        {
            ChangeTo150();
            changeBpm = false;
            print("Beat changed");
        }
    }

    public void ChangeTo150()
    {
        StopAllCoroutines();
        music.clip = hundredFiftyBPM;

        BPM = 150;

        StartCoroutine(Start());
    }

    IEnumerator Beats()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / (BPM / 60));
            EventManager.Instance.Beat();
            CurrentBeat = OnBeat.BEAT;
            StartCoroutine(lingerBeat());
        }
    }

    IEnumerator lingerBeat()
    {
        yield return new WaitForSeconds(1f/(1f/2f));
        CurrentBeat = OnBeat.OFFBEAT;
    }

    public void Pause()
    {
        if (music.isPlaying)
        {
            music.Pause();
        }
        else
        {
            music.Play();
        }  
    }
    
}
