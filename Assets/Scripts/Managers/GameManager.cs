using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState { PAUSED, MENU, RUNNING}
    public GameState state;

    bool gamePaused = false;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        EventManager.Instance.ChangeGameState += ChangeGameState;
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
        yield return new WaitForSeconds(1f);
        UIController.timerText.text = "2";
        yield return new WaitForSeconds(1f);
        UIController.timerText.text = "1";
        yield return new WaitForSeconds(1f);

        state = GameState.RUNNING;
        UnitManager.Instance.player.GetComponent<PlayerInput>().SwitchCurrentActionMap("MapMovement");
        UIController.timerText.transform.gameObject.SetActive(false);
        EventManager.Instance.GameManagerUnPause();
    }

    //public void TogglePauseGame()
    //{
    //    gamePaused = !gamePaused;

    //    switch (gamePaused)
    //    {
    //        case true:
    //            Time.timeScale = 0;

    //            UnitManager.Instance.player.GetComponent<PlayerInput>().SwitchCurrentActionMap("PauseScreen");
    //            break;
    //        case false:
    //            Time.timeScale = 1;

    //            UnitManager.Instance.player.GetComponent<PlayerInput>().SwitchCurrentActionMap("MapMovement");
    //            break;
    //    }
    //}
}
