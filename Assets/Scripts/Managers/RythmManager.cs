using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public enum OnBeat { BEAT, OFFBEAT};
public enum Level { ONE, TWO, THREE, FOUR, FIVE };

public class RythmManager : MonoBehaviour
{
    [SerializeField]
    Level currentLevel = Level.ONE;

    public static RythmManager Instance;

    [SerializeField] AudioClip song1, song2, song3, song4, song5;

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

        EventManager.Instance.OnLevelComplete += ChangeLevel;

    }

    IEnumerator Start()
    {
        StartCoroutine(Beats());
        yield return new WaitForSeconds((60 / BPM) / 2);
        music.Play();
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

    void SwapMusic(AudioClip song, int bpm)
    {
        StopAllCoroutines();
        music.clip = song;

        BPM = bpm;

        StartCoroutine(Start());
    }

    void ChangeLevel()
    {
        switch (currentLevel)
        {
            case Level.ONE:
                SwapMusic(song2, 125);
                currentLevel = Level.TWO;
                break;
            case Level.TWO:
                SwapMusic(song3, 130);
                currentLevel = Level.THREE;
                break;
            case Level.THREE:
                SwapMusic(song4, 135);
                currentLevel = Level.FOUR;
                break;
            case Level.FOUR:
                SwapMusic(song5, 140);
                currentLevel = Level.FIVE;
                break;
            case Level.FIVE:
                break;
        }
    }
    
}
