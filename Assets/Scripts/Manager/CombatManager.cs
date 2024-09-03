using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CombatManager : MonoBehaviour
{
    [Header("---- Health Info ----")]
    public int playerHealth;
    public int enemyHealth;
    private int playerMaxHealth;

    [Header("---- Particle Info ----")] 
    [SerializeField] private ParticleSystem[] particlesPrefab;
    private Transform enemySpawnPosition;

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
        }
    }

    public void ApplyDamageToEnemy(Equipment playerEquipment, Equipment enemyEquipment)
    {
        int damage = playerEquipment.Power - enemyEquipment.Defense;
        if (damage > 0)
        {
            enemyHealth -= damage;
        }
    }

    public void CheckHealth(int health, bool isPlayer)
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

    public void SetParticleSpawnPosition(Transform position)
    {
        enemySpawnPosition = position;
    }

    public void ActivateEnemyParticle()
    {
        int index = Random.Range(0, particlesPrefab.Length);
        Instantiate(particlesPrefab[index], enemySpawnPosition);
    }

    public void ActivatePlayerParticle(Transform playerSpawnPosition)
    {
        int index = Random.Range(0, particlesPrefab.Length);
        Instantiate(particlesPrefab[index], playerSpawnPosition);
    }

    IEnumerator LoseScreen()
    {
        GameManager.Instance.uiManager.blackScreen.DOFade(1,2);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.uiManager.loseScreen.SetActive(true);
        GameManager.Instance.audioManager.PlaySFX(GameManager.Instance.audioManager.lose);
        GameManager.Instance.audioManager.BattleMusic(false);
        GameManager.Instance.uiManager.blackScreen.DOFade(0,2);
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
    }
    
}
