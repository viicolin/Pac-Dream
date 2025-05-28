using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void IniciarJoc()
    {
        SceneManager.LoadScene("InicioJuego"); // o el nom real de l’escena del joc
    }

    public void ObrirConfiguracio()
    {
        SceneManager.LoadScene("MenuConfiguracio");
    }

    public void SortirDelJoc()
    {
        Debug.Log("Sortint del joc...");
        Application.Quit();
    }
}
