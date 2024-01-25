using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum OnBeat { BEAT, OFFBEAT};

public class RythmManager : MonoBehaviour
{
    public static RythmManager Instance;

    public OnBeat CurrentBeat = OnBeat.OFFBEAT;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EventManager.Instance.OnBeat += Beat;
    }

    void Beat()
    {
        StartCoroutine(LingerBeat());
    }

    IEnumerator LingerBeat()
    {
        yield return new WaitForSeconds(0.25f);

        CurrentBeat = OnBeat.OFFBEAT;
    } 
}
