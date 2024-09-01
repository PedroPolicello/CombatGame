using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CombatManager : MonoBehaviour
{
    [Header("---- Health Info ----")]
    public int playerHealth;
    public int enemyHealth;
    private int playerMaxHealth;

    private void Awake()
    {
        playerMaxHealth = playerHealth;
    }

    public void RecoverPlayerHealth()
    {
        playerHealth = playerMaxHealth;
    }

    public void SetEnemyHealth(Enemy enemy)
    {
        enemyHealth = enemy.enemyHealth;
    }

    public void ApplyDamageToPlayer(Equipment playerEquipment, Equipment enemyEquipment)
    {
        int damage = enemyEquipment.Power - playerEquipment.Defense;
        if (damage > 0)
        {
            playerHealth -= damage;
            CheckHealth(playerHealth, true);
        }
    }

    public void ApplyDamageToEnemy(Equipment playerEquipment, Equipment enemyEquipment)
    {
        int damage = playerEquipment.Power - enemyEquipment.Defense;
        if (damage > 0)
        {
            enemyHealth -= damage;
            CheckHealth(enemyHealth, false);
        }
    }

    void CheckHealth(int health, bool isPlayer)
    {
        if (health <= 0)
        {
            if (isPlayer)
            {
                GameManager.Instance.endCombat = true;
                StartCoroutine(LoseScreen());
            }
            else
            {
                GameManager.Instance.endCombat = true;
                GameManager.Instance.selectedEnemy.hasBeenDefeated = true;
                GameManager.Instance.enemiesDefeated++;
                GameManager.Instance.CheckEndGame();
            }
        }
    }

    IEnumerator LoseScreen()
    {
        GameManager.Instance.uiManager.blackScreen.DOFade(1,2);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.uiManager.loseScreen.SetActive(true);
        Time.timeScale = 0;
        GameManager.Instance.uiManager.blackScreen.DOFade(0,2);
    }
    
}
