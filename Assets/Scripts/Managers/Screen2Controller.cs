using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Screen2Controller : MonoBehaviour
{
    public Sprite[] picPlacePics;
    public GameObject[] picPlaces;

    enum CombatPrompts { SWORD, SHIELD}
    CombatPrompts[] prompts;

    [SerializeField]
    bool isMainScreen = false;

    int playerLives = 0, enemyLives = 3;

    int step = 0;

    bool roundSuccess = false;

    private void Start()
    {
        EventManager.Instance.OnChangeScreen += QuicktimeGame;
        EventManager.Instance.OnBeat += NextStep;

        EventManager.Instance.OnButtonPrompt += buttonPrompt;

    }

    void buttonPrompt(int i)
    {
        switch (i)
        {
            case 0:
                if (prompts[step] == CombatPrompts.SHIELD)
                {
                    roundSuccess = true;
                }
                break;
            case 1:
                if (prompts[step] == CombatPrompts.SWORD)
                {
                    enemyLives--;
                    roundSuccess = true;
                }
                break;
        }
    }

    void QuicktimeGame()
    {
        if (GameManager.Instance.currentScreen != GameManager.ScreenFocus.RIGHT)
        {
            isMainScreen = false;
            return;
        }
        isMainScreen = true;
        Setup();
    }

    void Setup()
    {
        playerLives = UnitManager.Instance.player.GetComponent<Player>().lives;
        step = 0;
        enemyLives = 3;

        for(int i = 0; i < 3; i++)
        {
            switch (Random.Range(0,10))
            {
                case < 5:
                    picPlaces[i].GetComponent<SpriteRenderer>().sprite = picPlacePics[0];
                    break;
                case >= 5:
                    picPlaces[i].GetComponent<SpriteRenderer>().sprite = picPlacePics[1];
                    break;
            }

                       
        }

        for (int i = 0; i < 4; i++)
        {
            picPlaces[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void resetRound()
    {
        for (int i = 0; i < 4; i++)
        {
            picPlaces[i].GetComponent<SpriteRenderer>().color = Color.white;
        }

        for (int i = 0; i < 4; i++)
        {
            picPlaces[i].GetComponent<SpriteRenderer>().sprite = picPlacePics[Random.Range(0, 1)];
        }
        step = 0;
    }

    void NextStep()
    {
        if (!isMainScreen)
            return;

        if (step > 3)
        {
            if (playerLives <= 0)
            {
                print("Player Died");
                Application.Quit();
                return;
            }

            if (enemyLives <= 0)
            {
                print("Enemy died");
                GameManager.Instance.ChangeScreen();
                return;
            }

            resetRound();
        }

        if (!roundSuccess)
        {
            playerLives--;
            picPlaces[step].GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            picPlaces[step].GetComponent<SpriteRenderer>().color = Color.green;
        }

        step++;
        roundSuccess = false;        
    }
}
