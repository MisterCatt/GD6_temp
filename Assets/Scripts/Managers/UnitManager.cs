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

    public List<Vector3Int> enemySpawnPoints = new List<Vector3Int>();

    [SerializeField]
    GameObject enemy;

    public GameObject targetEnemy;

    List<int> numbersChosen;

    private void Start()
    {
        player = GameObject.Find("Player");
        
    }

    void SpawnEnemy(Vector3 pos)
    {
        enemyList.Add(Instantiate(enemy, pos, Quaternion.identity));
    }

    public void SpawnEnemies()
    {
        numbersChosen = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            bool okNumber = false;
            int num = 0;
            while (!okNumber)
            {
                num = Random.Range(0, enemySpawnPoints.Count);

                for(int j = 0; j < numbersChosen.Count; j++)
                {
                    if (numbersChosen[j] == num)
                    {
                        okNumber = false;
                        break;
                    }
                    okNumber = true;
                }

            }
            numbersChosen.Add(num);
            SpawnEnemy(enemySpawnPoints[num]);
        }
    }

}
