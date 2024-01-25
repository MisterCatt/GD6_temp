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

    public event Action<GameManager.GameState> OnChangeGameState;
    public event Action OnUnPause;

    public void Pause()
    {
        OnChangeGameState?.Invoke(GameManager.GameState.PAUSED);
    }

    public void UnPause()
    {
        if (GameManager.Instance.state == GameManager.GameState.PAUSED)
        {
            OnChangeGameState?.Invoke(GameManager.GameState.RUNNING);
        }
    }

    public void GameManagerUnPause()
    {
        OnUnPause?.Invoke();
    }

    public event Action<int> OnButtonPrompt;

    public void Sword()
    {
        OnButtonPrompt?.Invoke(1);
    }

    public void Shield()
    {
        OnButtonPrompt?.Invoke(0);
    }

    public event Action OnChangeScreen;

    public void ChangeScreen()
    {
        OnChangeScreen?.Invoke();
    }
}
