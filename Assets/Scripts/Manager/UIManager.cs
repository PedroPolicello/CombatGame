using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("---- Health Bars Info ----")]
    [SerializeField] private Slider playerHealth;
    [SerializeField] private Slider enemyHealth;

    public void SetHealthBars(int playerMaxHealth, int enemyMaxHealth)
    {
        playerHealth.maxValue = playerMaxHealth;
        playerHealth.value = playerHealth.maxValue;

        enemyHealth.maxValue = enemyMaxHealth;
        enemyHealth.value = enemyHealth.maxValue;
    }
    
    public void UpdatePlayerUI(int currentHealth)
    {
        playerHealth.value = currentHealth;
    }

    public void UpdateEnemyUI(int currentHealth)
    {
        enemyHealth.value = currentHealth;
    }
}
