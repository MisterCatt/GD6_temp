using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Screen2Controller : MonoBehaviour
{
    public Sprite[] picPlacePics;
    public GameObject[] picPlaces;

    public GameObject[] playerLivesImages, enemyLivesImages;

    [SerializeField]
    GameObject playerBigPic, enemyBigPic;

    Enemy targetEnemy;

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

        targetEnemy = UnitManager.Instance.targetEnemy.GetComponent<Enemy>();

        for (int i = 0; i < picPlaces.Length; i++)
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

        foreach(var life in enemyLivesImages)
        {
            if (!life.activeSelf)
            {
                life.SetActive(true);
            }
        }

        enemyBigPic.SetActive(true);

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
                    EventManager.Instance.AddScore(25);
                }
                break;
            case 5:
                if (picPlaces[1].GetComponent<SpriteRenderer>().sprite == picPlacePics[1])
                {
                    picPlaces[1].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    sucessess[1] = true;
                    EventManager.Instance.AddScore(25);
                }
                break;
            case 6:
                if (picPlaces[2].GetComponent<SpriteRenderer>().sprite == picPlacePics[1])
                {
                    picPlaces[2].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    sucessess[2] = true;
                    EventManager.Instance.AddScore(25);
                }
                break;
            case 7:
                if (picPlaces[3].GetComponent<SpriteRenderer>().sprite == picPlacePics[1])
                {
                    picPlaces[3].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    sucessess[3] = true;
                    EventManager.Instance.AddScore(25);
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
                    EventManager.Instance.AddScore(25);

                }
                break;
            case 5:
                if (picPlaces[1].GetComponent<SpriteRenderer>().sprite == picPlacePics[0])
                {
                    picPlaces[1].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    sucessess[1] = true;
                    EventManager.Instance.AddScore(25);

                }
                break;
            case 6:
                if (picPlaces[2].GetComponent<SpriteRenderer>().sprite == picPlacePics[0])
                {
                    picPlaces[2].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    sucessess[2] = true;
                    EventManager.Instance.AddScore(25);
                }
                break;
            case 7:
                if (picPlaces[3].GetComponent<SpriteRenderer>().sprite == picPlacePics[0])
                {
                    picPlaces[3].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                    sucessess[3] = true;
                    EventManager.Instance.AddScore(25);
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
                if (EnemyLives >= 0)
                    enemyLivesImages[EnemyLives].SetActive(false);
            }

            if(!sucessess[i] && picPlaces[i].GetComponent<SpriteRenderer>().sprite == picPlacePics[0])
            {
                playerLives--;
                EventManager.Instance.AddScore(-25);
            }

            sucessess[i] = false;
        }

        if(EnemyLives <= 0)
        {
            print("Enemy died");
            targetEnemy.KillEnemy();
            targetEnemy = null;
            UnitManager.Instance.player.GetComponent<PlayerInput>().SwitchCurrentActionMap("MapMovement");
            enemyBigPic.SetActive(false);
            EventManager.Instance.ChangeScreen();
            EventManager.Instance.AddScore(100);
            return;
        }

        if(playerLives <= 0)
        {
            print("Player took damage");
            targetEnemy = null;
            UnitManager.Instance.player.GetComponent<PlayerInput>().SwitchCurrentActionMap("MapMovement");
            enemyBigPic.SetActive(false);
            EventManager.Instance.ChangeScreen();
            UnitManager.Instance.player.GetComponent<Player>().PlayerDamage();
            playerLivesImages[UnitManager.Instance.player.GetComponent<Player>().lives].SetActive(false);
            foreach (var life in enemyLivesImages)
            {
                if (life.activeSelf)
                {
                    life.SetActive(false);
                }
            }

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
