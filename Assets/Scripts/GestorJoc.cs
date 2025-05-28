using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorJoc : MonoBehaviour
{
    public static GestorJoc instancia;

    public int punts = 0;
    public int vides = 3;
    public string dificultat = "Normal";

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        dificultat = PlayerPrefs.GetString("Dificultat", "Normal");
        Debug.Log("Dificultat carregada al joc: " + dificultat);

        switch (dificultat)
        {
            case "Facil":
                vides = 5;
                break;
            case "Dificil":
                vides = 2;
                break;
            default:
                vides = 3;
                break;
        }

        Debug.Log("Vides inicials segons dificultat: " + vides);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void AfegirPunts(int quantitat)
    {
        punts += quantitat;
        Debug.Log("Punts actuals: " + punts);
    }

    public void PerdreVida()
    {
        vides--;
        Debug.Log("Vida perduda! Vides restants: " + vides);

        if (vides <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Debug.Log("GAME OVER");

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("FinalJuego");
    }

    public void Reiniciar()
    {
        punts = 0;

        switch (dificultat)
        {
            case "Facil":
                vides = 5;
                break;
            case "Dificil":
                vides = 2;
                break;
            default:
                vides = 3;
                break;
        }

        Debug.Log("Joc reiniciat. Punts i vides restablerts.");
    }
}