using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject screen1;
    public GameObject screen2;

    public enum GameState { PAUSED, MENU, RUNNING}
    public GameState state;

    public enum ScreenFocus { LEFT,RIGHT }
    public ScreenFocus currentScreen;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        EventManager.Instance.OnChangeGameState += ChangeGameState;
        EventManager.Instance.OnChangeScreen += ChangeScreen;
        currentScreen = ScreenFocus.LEFT;
        //screen1.GetComponent<Pulser>().ShouldPulse = true;
    }

    public void ChangeScreen()
    {
        if (currentScreen == ScreenFocus.LEFT)
        {
            currentScreen = ScreenFocus.RIGHT;
            //screen1.GetComponent<Pulser>().ShouldPulse = false;
            screen2.GetComponent<Pulser>().ShouldPulse = true;
        }
        else
        {
            currentScreen = ScreenFocus.LEFT;
            //screen1.GetComponent<Pulser>().ShouldPulse = true;
            screen2.GetComponent<Pulser>().ShouldPulse = false;
        }
    }

    public void ChangeGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.MENU:
                state = newState;
                UnitManager.Instance.player.GetComponent<PlayerInput>().SwitchCurrentActionMap("PauseScreen");
                break;
            case GameState.RUNNING:
                unPauseGame();
                break;
            case GameState.PAUSED:
                state = newState;
                UnitManager.Instance.player.GetComponent<PlayerInput>().SwitchCurrentActionMap("PauseScreen");
                RythmManager.Instance.Pause();
                Time.timeScale = 0;
                break;

        }
    }

    void unPauseGame()
    {
        StartCoroutine(unPauseTimer());
    }
    IEnumerator unPauseTimer()
    {
        UIController.timerText.transform.gameObject.SetActive(true);

        UIController.timerText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        UIController.timerText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        UIController.timerText.text = "1";
        yield return new WaitForSecondsRealtime(1f);

        state = GameState.RUNNING;
        UnitManager.Instance.player.GetComponent<PlayerInput>().SwitchCurrentActionMap("MapMovement");
        UIController.timerText.transform.gameObject.SetActive(false);
        EventManager.Instance.GameManagerUnPause();
        Time.timeScale = 1;
        RythmManager.Instance.Pause();
    }
}
