using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField]
    Level currentLevel = Level.ONE;

    [SerializeField]
    GameObject gameOverScreen, GameOverScreenText;

    public int Score = 0;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        EventManager.Instance.OnChangeGameState += ChangeGameState;
        EventManager.Instance.OnChangeScreen += ChangeScreen;

        EventManager.Instance.OnLevelComplete += nextLevel;
        EventManager.Instance.OnAddScore += AddScore;



        currentScreen = ScreenFocus.LEFT;

        EventManager.Instance.Pause();
    }

    void AddScore(int addedScore)
    {
        Score += addedScore;
    }

    void nextLevel()
    {
        switch (currentLevel)
        {
            case Level.ONE:
                currentLevel = Level.TWO;
                break;
            case Level.TWO:
                currentLevel = Level.THREE;
                break;
            case Level.THREE:
                currentLevel = Level.FOUR;
                break;
            case Level.FOUR:
                currentLevel = Level.FIVE;
                break;
            case Level.FIVE:
                GameOverWin();
                break;
        }
    }

    public void GameOverLoose()
    {
        GameObject ui = GameObject.Find("UI");

        for (int i = 0; i < ui.transform.childCount; i++)
        {
            ui.transform.GetChild(i).gameObject.SetActive(false);
        }

        GameOverScreenText.GetComponent<TextMeshProUGUI>().text = "You loose D:";

        gameOverScreen.SetActive(true);
    }

    public void GameOverWin()
    {
        GameObject ui = GameObject.Find("UI");

        for(int i= 0; i < ui.transform.childCount; i++)
        {
            ui.transform.GetChild(i).gameObject.SetActive(false);
        }

        gameOverScreen.SetActive(true);
    }

    public void ChangeScreen()
    {
        if (currentScreen == ScreenFocus.LEFT)
        {
            currentScreen = ScreenFocus.RIGHT;
        }
        else
        {
            currentScreen = ScreenFocus.LEFT;
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
        if (UnitManager.Instance.player != null)
            UnitManager.Instance.player.GetComponent<PlayerInput>().SwitchCurrentActionMap("MapMovement");
        UIController.timerText.transform.gameObject.SetActive(false);
        EventManager.Instance.GameManagerUnPause();
        Time.timeScale = 1;
        RythmManager.Instance.Pause();
    }
}
