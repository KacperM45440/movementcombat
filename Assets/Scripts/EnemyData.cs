using TMPro;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField] private PlayerCombat combatRef;
    [SerializeField] private TMP_Text textRef;
    private int enemyHealth = 11;

    private void Start()
    {
        ShowHealth();
    }
    public void TakeDamage(int howMuch)
    {
        enemyHealth -= howMuch;
        ShowHealth();

        if (enemyHealth <= 0)
        {
            combatRef.RemoveEnemyFromList(this);
            Destroy(gameObject);
        }
    }

    private void ShowHealth()
    {
        textRef.text = "HP: " + enemyHealth.ToString();
    }
    public int GetHealth() 
    { 
        return enemyHealth; 
    }
}
