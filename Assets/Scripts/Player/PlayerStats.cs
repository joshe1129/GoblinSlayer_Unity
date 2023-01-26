using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    #region Variables

    public int berserkerLevel = 10;
    public int maxBerserker;
    public int currentBerserker;
    public int exptoLevelUp = 20;
    public int experience = 0;

    #endregion

    #region Referencias
    [SerializeField] public HUD healthBar;
    [SerializeField] public HUD berserkerBar;
    [SerializeField] public SaveData saveData;

    PlayerAnimationHandler animationHandler;

    #endregion

    //dejaste el health bar mal hecho 
    private void Awake()
    {
        animationHandler = GetComponentInChildren<PlayerAnimationHandler>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponentInChildren<HUD>();
        berserkerBar = GetComponentInChildren<HUD>();
        maxHealth = SetMaxHealthfromLevel();
        maxBerserker = SetMaxBerserkerfromLevel();
        currentBerserker = 0;
        if (saveData.identifierSaveLoad == 2)
        {
            currentHealth = saveData.playerSavedHealth_SG;
        }
        else
        {
            currentHealth = maxHealth;
        }
        berserkerBar.SetMaxBerserker(maxBerserker);
        berserkerBar.SetBerserkerValue(currentBerserker);
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetLifeValue(currentHealth);
    }

    public int SetMaxHealthfromLevel()
    {
        maxHealth = level * 100;
        return maxHealth;
    }
    public int SetMaxBerserkerfromLevel()
    {
        maxBerserker = berserkerLevel * 10;
        return maxBerserker;
    }


    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }
        currentHealth = currentHealth - damage;
        currentBerserker = currentBerserker + 10;
        berserkerBar.SetBerserkerValue(currentBerserker);
        healthBar.SetLifeValue(currentHealth);
        animationHandler.PlayTargetAnimation("TakeDamage", true);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animationHandler.PlayTargetAnimation("Death", true);
            isDead = true;
            Cursor.lockState = CursorLockMode.None;
            GameManager.FindObjectOfType<GameManager>().ChangeScene("GameOver");
        }

    }
    public void GetExperience(int exp)
    {
        experience += exp;
        Checkexperience();
    }
    void Checkexperience()
    {
        if (experience >= exptoLevelUp)
        {
            
            level++;
            Debug.Log(level);
            experience = 0;
            currentHealth = SetMaxHealthfromLevel();
            healthBar.SetMaxHealth(maxHealth);
            /*
            inteligence += 10;
            stamina += 10;
            currentmana = currentmana + (inteligence * 0.1f);
            currentlife = currentlife + (stamina * 0.1f);
            currentlife = maxlife;
            currentmana = maxmana;
            */
            exptoLevelUp += (exptoLevelUp * level);
        }
    }
}
