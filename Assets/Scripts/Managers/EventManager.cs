using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public event Action OnBeat;

    public void Beat()
    {
        RythmManager.Instance.CurrentBeat = global::OnBeat.BEAT;
        OnBeat?.Invoke(); 
    }
}
