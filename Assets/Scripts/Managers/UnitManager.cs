using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    [SerializeField]
    GameObject enemyPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject player;

    public List<GameObject> enemyList;

    public List<Vector3Int> avalibleSpawnPoints = new List<Vector3Int>();

    public GameObject targetEnemy;

    List<int> numbersChosen;

    private void Start()
    {
        player = GameObject.Find("Player");

        EventManager.Instance.OnEnemyDeath += CheckEnemies;


    }

    void CheckEnemies()
    {
        if(enemyList.Count <= 0)
        {
            SpawnEnemies();
        }
    }

    void SpawnEnemy(Vector3 pos)
    {
        enemyList.Add(Instantiate(enemyPrefab, pos, Quaternion.identity));
    }

    public void SpawnEnemies()
    {
        Shuffle(avalibleSpawnPoints);

        for(int i = 0; i < 4; i++)
        {
            SpawnEnemy(avalibleSpawnPoints[i]);
        }
    }

    private static System.Random rng = new System.Random();

    public void Shuffle( List<Vector3Int> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Vector3Int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}
