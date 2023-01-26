using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    #region Variables
    
    [SerializeField] public int expdrop;

    #endregion

    #region Referencias

    [SerializeField] public EnemyHud healthBar;
    EnemyAnimationHandler enemyAnim;
    public GameObject myGameObject;
    #endregion


    private void Awake()
    {
        enemyAnim = GetComponentInChildren<EnemyAnimationHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = SetMaxHealthfromLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetLifeValue(currentHealth);
    }

    public int SetMaxHealthfromLevel()
    {
        maxHealth = level * 100;
        return maxHealth;
    }


    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }
        currentHealth = currentHealth - damage;
        healthBar.SetLifeValue(currentHealth);
        enemyAnim.PlayTargetAnimation("TakeDamage", true);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            enemyAnim.PlayTargetAnimation("Death", true);
            isDead = true;
            Object.Destroy(myGameObject, 1);
        }

    }

}
