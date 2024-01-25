using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmBlock : MonoBehaviour
{
    Vector3 startPos;
    Rigidbody2D rb;

    [SerializeField]
    float speed;
    float StartSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;

        StartSpeed = speed;

        EventManager.Instance.ChangeGameState += Pause;
        EventManager.Instance.OnUnPause += UnPause;
    }

    public void Reset()
    {
        transform.position = startPos;
    }

    void Pause(GameManager.GameState s)
    {
        if (s == GameManager.GameState.PAUSED)
        {
            StartSpeed = speed;
            speed = 0;
        }
    }

    void UnPause()
    {
        speed = StartSpeed;
    }


    //Sträcka = 10;
    //Tid = 1/120;
    //Hastighet = Sträcka/Tid;

    private void Update()
    {
        transform.Translate(-Vector2.up * Time.deltaTime * speed);
    }
}
