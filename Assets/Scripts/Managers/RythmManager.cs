using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public enum OnBeat { BEAT, OFFBEAT};

public class RythmManager : MonoBehaviour
{
    public static RythmManager Instance;

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
            ChangeBPM(BPM);
            changeBpm = false;
            print("Beat changed");
        }
    }

    public void ChangeBPM(float changeBPM)
    {
        CancelInvoke();

        BPM = changeBPM;

        InvokeRepeating("Beats", 60 / BPM, 60 / BPM);
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
        yield return new WaitForSeconds(1/(60 / BPM));
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
