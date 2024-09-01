using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

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
