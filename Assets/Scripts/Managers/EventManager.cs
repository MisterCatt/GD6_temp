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

    public event Action<GameManager.GameState> ChangeGameState;
    public event Action OnUnPause;

    public void Pause()
    {
        ChangeGameState?.Invoke(GameManager.GameState.PAUSED);
    }

    public void UnPause()
    {
        if (GameManager.Instance.state == GameManager.GameState.PAUSED)
        {
            ChangeGameState?.Invoke(GameManager.GameState.RUNNING);
        }
    }

    public void GameManagerUnPause()
    {
        OnUnPause?.Invoke();
    }

}
