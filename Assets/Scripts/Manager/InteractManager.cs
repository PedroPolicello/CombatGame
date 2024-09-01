using System;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public static InteractManager Instance;
    
    [Header("---- Enemy Info ----")]
    public Enemy enemySelected;
    public GameObject textGameObj;

    [Header("---- Components Info ----")]
    public Camera camera;
    public Animator animator;
    
    private void Awake()
    {
        Instance = this;
        textGameObj.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetEnemyInfo(enemySelected, camera, animator, textGameObj);
            if (!GameManager.Instance.selectedEnemy.hasBeenDefeated)
            {
                textGameObj.SetActive(true);
                enemySelected.inRange = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textGameObj.SetActive(false);
            enemySelected.inRange = false;
        }
    }

    private void OnDisable()
    {
        enemySelected.inRange = false;
    }
}
