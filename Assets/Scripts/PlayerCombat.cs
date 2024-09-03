using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator animatorRef;
    [SerializeField] private PlayerMovement movementRef;
    private float attackInterval = 0.5f;
    private float lastPunchTime;
    private List<EnemyData> enemiesInRange = new();

    private void Update()
    {
        TakeInput();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyData enemyData))
        {
            enemiesInRange.Add(enemyData);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyData enemyData))
        {
            enemiesInRange.Remove(enemyData);
        }
    }

    private void TakeInput()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time > lastPunchTime + attackInterval)
        {
            Punch();
            lastPunchTime = Time.time;
        }
    }

    private int CalculateDamage()
    {
        int damage = (int)movementRef.GetVelocity();

        if (damage < 5) 
        {
            damage = 5;
        }

        return damage;
    }

    private void Punch()
    {
        animatorRef.SetTrigger("Punch");

        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            EnemyData enemy = enemiesInRange[i];
            enemy.TakeDamage(CalculateDamage());
        }
    }

    public void RemoveEnemyFromList(EnemyData givenEnemy)
    {
        enemiesInRange.Remove(givenEnemy);
    }    
}
