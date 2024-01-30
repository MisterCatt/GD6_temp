using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Screen2Controller : MonoBehaviour
{
    public Sprite[] picPlacePics;
    public GameObject[] picPlaces;

    bool[] sucessess;

    int step = 0, playerLives = 0, EnemyLives = 0;

    private void Start()
    {
        EventManager.Instance.OnChangeScreen += SetupGame;
        EventManager.Instance.OnBeat += NextStep;

        EventManager.Instance.OnSword += sword;
        EventManager.Instance.OnShield += shield;

        sucessess = new bool[4];
    }

    void SetupGame()
    {
        if (GameManager.Instance.currentScreen != GameManager.ScreenFocus.LEFT)
            return;

        for(int i = 0; i < picPlaces.Length; i++)
        {
            sucessess[i] = false;
            picPlaces[i].transform.GetChild(0).gameObject.SetActive(false);
            picPlaces[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
            switch (Random.Range(0, 10))
            {
                case <5:
                    picPlaces[i].GetComponent<SpriteRenderer>().sprite = picPlacePics[0];
                    break;
                case >=5:
                    picPlaces[i].GetComponent<SpriteRenderer>().sprite = picPlacePics[1];
                    break;
            }
        }

        playerLives = UnitManager.Instance.player.GetComponent<Player>().lives;
        EnemyLives = 3;
        step = 0;
    }

    void sword()
    {
        switch (step)
        {
            case 4:
                if (picPlaces[0].GetComponent<SpriteRenderer>().sprite == picPlacePics[1])
                {
                    picPlaces[0].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    sucessess[0] = true;
                    
                }
                break;
            case 5:
                if (picPlaces[1].GetComponent<SpriteRenderer>().sprite == picPlacePics[1])
                {
                    picPlaces[1].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    sucessess[1] = true;
                    
                }
                break;
            case 6:
                if (picPlaces[2].GetComponent<SpriteRenderer>().sprite == picPlacePics[1])
                {
                    picPlaces[2].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    sucessess[2] = true;
                }
                break;
            case 7:
                if (picPlaces[3].GetComponent<SpriteRenderer>().sprite == picPlacePics[1])
                {
                    picPlaces[3].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    sucessess[3] = true;
                    
                }
                break;
        }
    }

    void shield()
    {
        switch (step)
        {
            case 4:
                if (picPlaces[0].GetComponent<SpriteRenderer>().sprite == picPlacePics[0])
                {
                    picPlaces[0].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    sucessess[0] = true;
                    
                }
                break;
            case 5:
                if (picPlaces[1].GetComponent<SpriteRenderer>().sprite == picPlacePics[0])
                {
                    picPlaces[1].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    sucessess[1] = true;
                    
                }
                break;
            case 6:
                if (picPlaces[2].GetComponent<SpriteRenderer>().sprite == picPlacePics[0])
                {
                    picPlaces[2].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    sucessess[2] = true;
                    
                }
                break;
            case 7:
                if (picPlaces[3].GetComponent<SpriteRenderer>().sprite == picPlacePics[0])
                {
                    picPlaces[3].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    sucessess[3] = true;
                    
                }
                break;
        }
    }

    void ResetRound()
    {
        for(int i = 0; i < picPlaces.Length; i++)
        {
            if (sucessess[i] && picPlaces[i].GetComponent<SpriteRenderer>().sprite == picPlacePics[1])
            {
                EnemyLives--;
            }

            if(!sucessess[i] && picPlaces[i].GetComponent<SpriteRenderer>().sprite == picPlacePics[0])
            {
                playerLives--;
            }

            sucessess[i] = false;
        }

        if(EnemyLives <= 0)
        {
            print("Enemy died");
            UnitManager.Instance.player.GetComponent<PlayerInput>().SwitchCurrentActionMap("MapMovement");
            EventManager.Instance.ChangeScreen();
            return;
        }

        if(playerLives <= 0)
        {
            print("Player took damage");
            UnitManager.Instance.player.GetComponent<PlayerInput>().SwitchCurrentActionMap("MapMovement");
            EventManager.Instance.ChangeScreen();
            return;
        }

        step = 0;

        for (int i = 0; i < picPlaces.Length; i++)
        {
            picPlaces[i].transform.GetChild(0).gameObject.SetActive(false);
            picPlaces[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;

            switch (Random.Range(0, 10))
            {
                case < 5:
                    picPlaces[i].GetComponent<SpriteRenderer>().sprite = picPlacePics[0];
                    break;
                case >= 5:
                    picPlaces[i].GetComponent<SpriteRenderer>().sprite = picPlacePics[1];
                    break;
            }
        }
    }

    void NextStep()
    {
        if (GameManager.Instance.currentScreen == GameManager.ScreenFocus.LEFT)
            return;

        step++;

        switch (step)
        {
            case 4:
                picPlaces[0].transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 5:
                picPlaces[1].transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 6:
                picPlaces[2].transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 7:
                picPlaces[3].transform.GetChild(0).gameObject.SetActive(true);
                break;
        }

        if(step >= 8)
        {
            ResetRound();
        }
    }
}
