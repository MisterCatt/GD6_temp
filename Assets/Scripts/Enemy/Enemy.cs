using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void Start()
    {
        snapToGrid();
    }

    void snapToGrid()
    {
        Vector3Int gridPosition = GridController.WallMap.WorldToCell(transform.position);

        transform.position = new Vector2(gridPosition.x + 0.5f, gridPosition.y + 0.5f);
    }

    public void KillEnemy()
    {
        UnitManager.Instance.enemyList.Remove(gameObject);
        Destroy(gameObject);
        EventManager.Instance.EnemyDeath();
    }
}
