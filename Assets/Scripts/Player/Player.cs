using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    Transform walkPoint;

    public int lives = 3;

    public GameObject playerBigPic;

    private void Start()
    {
        UnitManager.Instance.player = gameObject;
        walkPoint.parent = null;
    }

    private void FixedUpdate()
    {
        snapToGrid();
    }

    public void PlayerDamage()
    {
        lives--;

        if(lives <= 0)
        {
            Destroy(gameObject);

            playerBigPic.SetActive(false);

            GameManager.Instance.GameOverLoose();

            print("Player died");
        }
    }

    void snapToGrid()
    {
        Vector3Int gridPosition = GridController.FloorMap.WorldToCell(transform.position);

        transform.position = new Vector2(gridPosition.x + 0.5f, gridPosition.y + 0.5f);
        walkPoint.position = new Vector2(gridPosition.x + 0.5f, gridPosition.y + 0.5f);
    }

    void OnChangeScreen()
    {
        if (GameManager.Instance.currentScreen == GameManager.ScreenFocus.LEFT)
            GetComponent<PlayerInput>().SwitchCurrentActionMap("Combat");
        else
            GetComponent<PlayerInput>().SwitchCurrentActionMap("MapMovement");

        EventManager.Instance.ChangeScreen();
    }

    void OnQuit()
    {
        Application.Quit();
    }

    void OnNextLevel()
    {
        EventManager.Instance.LevelComplete();
    }

    void OnPause()
    {
        EventManager.Instance.Pause();
    }

    void OnUnPause()
    {
        EventManager.Instance.UnPause();
    }

    void OnSword()
    {
        if (RythmManager.Instance.CurrentBeat == OnBeat.BEAT)
            EventManager.Instance.Sword();
    }

    void OnShield()
    {
        if (RythmManager.Instance.CurrentBeat == OnBeat.BEAT)
            EventManager.Instance.Shield();
    }

    void OnMove(InputValue value)
    {
        Vector2 dir = value.Get<Vector2>();

        if (dir.x != 0 && dir.y != 0)
            return;

        if (RythmManager.Instance.CurrentBeat != OnBeat.BEAT)
        {
            print("Movement not on beat");
            return;
        }

        walkPoint.position = new Vector3(transform.position.x + dir.x, transform.position.y + dir.y);

        RaycastHit2D hit = Physics2D.Raycast(walkPoint.transform.position, -Vector2.up, 0.1f);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                if (GameManager.Instance.currentScreen == GameManager.ScreenFocus.LEFT)
                    GetComponent<PlayerInput>().SwitchCurrentActionMap("Combat");
                else
                    GetComponent<PlayerInput>().SwitchCurrentActionMap("MapMovement");

                UnitManager.Instance.targetEnemy = hit.transform.gameObject;

                EventManager.Instance.ChangeScreen();
            }
        }
        else
        {
            if (GridController.FloorMap.HasTile(GridController.FloorMap.WorldToCell(walkPoint.position)))
            {
                transform.position = walkPoint.transform.position;
            }

            if (RythmManager.Instance.CurrentBeat == OnBeat.BEAT)
            {
                RythmManager.Instance.CurrentBeat = OnBeat.OFFBEAT;
            }
        }
    }
}
