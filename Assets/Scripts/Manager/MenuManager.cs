using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void TentarNovamente()
    {
        SceneManager.LoadScene("Game");
    }

    public void Sair()
    {
        print("Saindo...");
        Application.Quit();
    }
}
