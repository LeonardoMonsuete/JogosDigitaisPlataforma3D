using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public EnemyMovement enemy;
    private void OnTriggerEnter(Collider gameObject)
    {
        if (gameObject.name == "Player")
        {
            enemy.isChasing = true;
        }
    }
}
