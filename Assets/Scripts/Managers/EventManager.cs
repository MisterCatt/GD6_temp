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
        //RythmManager.Instance.CurrentBeat = global::OnBeat.BEAT;
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

    public event Action OnSword, OnShield;

    public void Sword()
    {
        OnSword?.Invoke();
    }

    public void Shield()
    {
        OnShield?.Invoke();
    }

    public event Action OnChangeScreen;

    public void ChangeScreen()
    {
        OnChangeScreen?.Invoke();
    }

    public event Action OnPlayerDamage;

    public void PlayerDamage()
    {
        OnPlayerDamage?.Invoke();
    }

    public event Action OnEnemyDeath;

    public void EnemyDeath()
    {
        OnEnemyDeath?.Invoke();
    }

    public event Action OnLevelComplete;

    public void LevelComplete()
    {
        OnLevelComplete?.Invoke();
    }

    public event Action OnGameOver;

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public event Action<int> OnAddScore;

    public void AddScore(int score)
    {
        OnAddScore?.Invoke(score);
    }
}
