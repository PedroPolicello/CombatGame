using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public InputManager InputManager { get; private set; }

    [Header("---- Managers ----")]
    public CombatManager combatManager;
    public InventoryManager inventoryManager;
    public UIManager uiManager;
    public PlayerController playerController;
    public AudioManager audioManager;

    [Header("---- Camera ----")] 
    public Camera mainCamera;
    public Camera menuCamera;
    
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
        InputManager.DisableMovement();
        menuCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
        uiManager.SetMenus();
        enemiesInGame = GameObject.FindGameObjectsWithTag("Enemy");
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
        uiManager.inventoryUI.SetActive(true);
        uiManager.healthBarUI.SetActive(true);
        uiManager.feedback.SetActive(true);
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
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        uiManager.blackScreen.DOFade(1,2);
        yield return new WaitForSeconds(2);
        uiManager.winScreen.SetActive(true);
        audioManager.PlaySFX(audioManager.win);
        audioManager.BattleMusic(false);
        uiManager.blackScreen.DOFade(0,2);
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
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
        uiManager.feedback.GetComponentInChildren<TextMeshProUGUI>().text = "Fim de Combate!";
        yield return new WaitForSeconds(2f);
        audioManager.BattleMusic(false);
        CameraController(false);
        uiManager.feedback.GetComponentInChildren<TextMeshProUGUI>().text = "";
        uiManager.inventoryUI.SetActive(false);
        uiManager.healthBarUI.SetActive(false);
        uiManager.feedback.SetActive(false);
        combatManager.RecoverPlayerHealth();
        InputManager.EnableMovement();
        endCombat = false;
    }

    IEnumerator TurnController()
    {
        //AJUSTES COMBATE
        uiManager.inventoryUI.SetActive(false);
        Equipment enemyEquipment = inventoryManager.GetEnemyEquipment();
        uiManager.feedback.GetComponentInChildren<TextMeshProUGUI>().text = $"O JOGADOR escolheu o equipamento {inventoryManager.playerSelectedEquipment.Name}";
        yield return new WaitForSeconds(2f);
        uiManager.feedback.GetComponentInChildren<TextMeshProUGUI>().text = $"O INIMIGO escolheu o equipamento {enemyEquipment.Name}";
        yield return new WaitForSeconds(2f);
        
        //ATAQUE PLAYER
        playerController.animator.SetTrigger("punch");
        yield return new WaitForSeconds(.9f);
        audioManager.ChooseRandomPunchSFX();
        combatManager.ApplyDamageToEnemy(inventoryManager.playerSelectedEquipment, enemyEquipment);
        uiManager.UpdateEnemyUI(combatManager.enemyHealth);
        if (combatManager.enemyHealth <= 0) combatManager.enemyHealth = 0;
        uiManager.feedback.GetComponentInChildren<TextMeshProUGUI>().text = $"O INIMIGO está com {combatManager.enemyHealth} de vida!";
        yield return new WaitForSeconds(2f);
        
        //ATAQUE INIMIGO
        enemyAnimator.SetTrigger("punch");
        yield return new WaitForSeconds(.9f);
        audioManager.ChooseRandomPunchSFX();
        combatManager.ApplyDamageToPlayer(inventoryManager.playerSelectedEquipment, enemyEquipment);
        uiManager.UpdatePlayerUI(combatManager.playerHealth);
        if (combatManager.playerHealth <= 0) combatManager.playerHealth = 0;
        uiManager.feedback.GetComponentInChildren<TextMeshProUGUI>().text = $"O JOGADOR está com {combatManager.playerHealth} de vida!";
        yield return new WaitForSeconds(2f);
        
        //CHECAR VIDA E REINICIAR TURNO
        combatManager.CheckHealth(combatManager.playerHealth, true);
        combatManager.CheckHealth(combatManager.enemyHealth, false);
        uiManager.feedback.GetComponentInChildren<TextMeshProUGUI>().text = "Escolha um equipamento.";
        uiManager.inventoryUI.SetActive(true);
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
