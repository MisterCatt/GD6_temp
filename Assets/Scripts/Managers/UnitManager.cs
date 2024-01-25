using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject player;

    public List<GameObject> enemyList;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
}
