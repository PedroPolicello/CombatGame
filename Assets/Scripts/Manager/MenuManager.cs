using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    private bool inPause;
    
    public void IniciarJogo()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        inPause = true;
        GameManager.Instance.uiManager.blackScreen.DOFade(1, 2f);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.mainCamera.gameObject.SetActive(true);
        GameManager.Instance.menuCamera.gameObject.SetActive(false);
        GameManager.Instance.uiManager.mainMenu.SetActive(false);
        GameManager.Instance.uiManager.blackScreen.DOFade(0, 2f);
        GameManager.Instance.InputManager.EnableMovement();
    }

    public void FugirBatalha()
    {
        if(GameManager.Instance.canFlee && GameManager.Instance.inCombat) GameManager.Instance.endCombat = true;
    }

    public void VoltarOpcoes()
    {
        if (inPause)
        {
            GameManager.Instance.uiManager.optionsScreen.SetActive(false);
        }
        else
        {
            GameManager.Instance.uiManager.optionsScreen.SetActive(false);
            GameManager.Instance.uiManager.mainMenu.SetActive(true);
        }
    }

    public void TentarNovamente()
    {
        GameManager.Instance.HasBeenDefeatedToFalse();
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void Sair()
    {
        print("Saindo...");
        Time.timeScale = 1;
        Application.Quit(); 
    }
}
