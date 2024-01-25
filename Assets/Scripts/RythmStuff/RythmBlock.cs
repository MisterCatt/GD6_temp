using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmBlock : MonoBehaviour
{
    Vector3 startPos;
    Rigidbody2D rb;

    [SerializeField]
    float speed = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    public void Reset()
    {
        transform.position = startPos;
    }


    //Str�cka = 10;
    //Tid = 1/120;
    //Hastighet = Str�cka/Tid;

    private void Update()
    {
        transform.Translate(-Vector2.up * Time.deltaTime * speed);
    }
}
