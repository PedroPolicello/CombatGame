using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("---- UI ----")]
    public GameObject inventoryUI;
    public GameObject healthBarUI;
    public GameObject feedback;
    public GameObject loseScreen;
    public GameObject winScreen;
    public GameObject mainMenu;
    public GameObject missionScreen;
    public GameObject optionsScreen;
    public CanvasGroup blackScreen;
    
    [Header("---- Health Bars Info ----")]
    [SerializeField] private Slider playerHealth;
    [SerializeField] private Slider enemyHealth;

    private bool isPaused;

    public void SetMenus()
    {
        mainMenu.SetActive(true);
        inventoryUI.SetActive(false);
        healthBarUI.SetActive(false);
        feedback.SetActive(false);
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
        missionScreen.SetActive(false);
        optionsScreen.SetActive(false);
        blackScreen.alpha = 0;
    }

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

    public void Pause()
    {
        if (!isPaused)
        {
            optionsScreen.SetActive(true);
            isPaused = true;
        }
        else
        {
            optionsScreen.SetActive(false);
            isPaused = false;
        }
    }
}
