using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    public void IniciarJogo()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        GameManager.Instance.uiManager.blackScreen.DOFade(1, 2f);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.mainCamera.gameObject.SetActive(true);
        GameManager.Instance.menuCamera.gameObject.SetActive(false);
        GameManager.Instance.uiManager.mainMenu.SetActive(false);
        GameManager.Instance.uiManager.blackScreen.DOFade(0, 2f);
        GameManager.Instance.InputManager.EnableMovement();
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
