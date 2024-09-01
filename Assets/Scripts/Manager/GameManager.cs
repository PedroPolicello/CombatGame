using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public InputManager InputManager { get; private set; }

    [Header("---- Managers ----")]
    public CombatManager combatManager;
    public InventoryManager inventoryManager;
    public UIManager uiManager;
    public PlayerController playerController;

    [Header("---- UI ----")]
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject healthBarUI;
    [SerializeField] private GameObject feedback;
    public GameObject winScreen;
    public GameObject loseScreen;

    [Header("---- Camera ----")] 
    public Camera mainCamera;
    
    [Header("---- Enemy Info ----")]
    public Enemy selectedEnemy;
    public Camera enemyCamera;
    public GameObject enemyCanvasObj;
    public GameObject[] enemiesInGame;
    
    private Animator enemyAnimator;
    private bool startTurn = true;
    [HideInInspector] public int enemiesDefeated;
    [HideInInspector] public bool inCombat;
    [HideInInspector] public bool isEquipmentSelected;
    [HideInInspector] public bool endCombat;
    [HideInInspector] public bool canFlee = true;
    
    private void Awake()
    {
        Instance = this;
        InputManager = new InputManager();
        SetupGame();
        canFlee = true;
    }

    void SetupGame()
    {
        inventoryUI.SetActive(false);
        healthBarUI.SetActive(false);
        feedback.SetActive(false);
    }

    public void SetEnemyInfo(Enemy enemy, Camera camera, Animator animator, GameObject canvasObj)
    {
        selectedEnemy = enemy;
        enemyCamera = camera;
        enemyAnimator = animator;
        enemyCanvasObj = canvasObj;
    }

    public void SetEnemyVariables()
    {
        enemyCanvasObj.SetActive(false);
        InputManager.DisableMovement();
        combatManager.SetEnemyHealth(selectedEnemy);
        uiManager.SetHealthBars(combatManager.playerHealth, selectedEnemy.enemyHealth);
        inventoryUI.SetActive(true);
        healthBarUI.SetActive(true);
        feedback.SetActive(true);
    }

    public void CameraController(bool inCombat)
    {
        if (inCombat)
        {
            mainCamera.gameObject.SetActive(false);
            enemyCamera.gameObject.SetActive(true);
        }
        else
        {
            mainCamera.gameObject.SetActive(true);
            enemyCamera.gameObject.SetActive(false);
        }
    }

    public void CheckEndGame()
    {
        if (enemiesDefeated >= enemiesInGame.Length)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void FixedUpdate()
    {
        if (isEquipmentSelected && startTurn && !endCombat)
        {
            canFlee = false;
            startTurn = false;
            StartCoroutine(TurnController());
        }
        else if (endCombat)
        {
            StartCoroutine(EndCombat());
        }
    }

    IEnumerator EndCombat()
    {
        inCombat = false;
        feedback.GetComponentInChildren<TextMeshProUGUI>().text = "Fim de Combate!";
        yield return new WaitForSeconds(2f);
        CameraController(false);
        inventoryUI.SetActive(false);
        healthBarUI.SetActive(false);
        feedback.SetActive(false);
        combatManager.RecoverPlayerHealth();
        InputManager.EnableMovement();
        endCombat = false;
    }

    IEnumerator TurnController()
    {
        //AJUSTES COMBATE
        inventoryUI.SetActive(false);
        Equipment enemyEquipment = inventoryManager.GetEnemyEquipment();
        feedback.GetComponentInChildren<TextMeshProUGUI>().text = $"O JOGADOR escolheu o equipamento {inventoryManager.playerSelectedEquipment.Name}";
        yield return new WaitForSeconds(2f);
        feedback.GetComponentInChildren<TextMeshProUGUI>().text = $"O INIMIGO escolheu o equipamento {enemyEquipment.Name}";
        yield return new WaitForSeconds(2f);
        
        //ATAQUE PLAYER
        playerController.animator.SetTrigger("punch");
        combatManager.ApplyDamageToEnemy(inventoryManager.playerSelectedEquipment, enemyEquipment);
        uiManager.UpdateEnemyUI(combatManager.enemyHealth);
        feedback.GetComponentInChildren<TextMeshProUGUI>().text = $"O INIMIGO está com {combatManager.enemyHealth} de vida!";
        yield return new WaitForSeconds(2f);
        
        //ATAQUE INIMIGO
        enemyAnimator.SetTrigger("punch");
        combatManager.ApplyDamageToPlayer(inventoryManager.playerSelectedEquipment, enemyEquipment);
        uiManager.UpdatePlayerUI(combatManager.playerHealth);
        feedback.GetComponentInChildren<TextMeshProUGUI>().text = $"O JOGADOR está com {combatManager.playerHealth} de vida!";
        yield return new WaitForSeconds(2f);
        
        //REINICIAR TURNO
        feedback.GetComponentInChildren<TextMeshProUGUI>().text = "Escolha um equipamento.";
        inventoryUI.SetActive(true);
        isEquipmentSelected = false;
        canFlee = true;
        yield return new WaitForSeconds(1f);
        startTurn = true;
    }

    public void HasBeenDefeatedToFalse()
    {
        foreach (GameObject enemy in enemiesInGame)
        {
            if(enemy == null) return;
            enemy.GetComponent<InteractManager>().enemySelected.hasBeenDefeated = false;
        }
    }

    private void OnDisable()
    {
        HasBeenDefeatedToFalse();
    }
}
