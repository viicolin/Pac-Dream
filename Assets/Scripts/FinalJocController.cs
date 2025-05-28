using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalJocController : MonoBehaviour
{
    public GameObject victoryUI;
    public GameObject gameOverUI;
    public TextMeshProUGUI textPuntsFinal;

    [Header("Música Final")]
    public AudioClip gameOverMusic;   // 🎵 Música derrota
    public AudioClip victoryMusic;    // 🎵 Música victoria
    private AudioSource audioSource;  // 🎧 Fuente de audio

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Obtenir info de dificultat i punts
        int puntsJugador = GestorJoc.instancia != null ? GestorJoc.instancia.punts : 0;
        string dificultat = PlayerPrefs.GetString("Dificultat", "Normal");
        int puntsNecessaris = dificultat switch
        {
            "Facil" => 250,
            "Dificil" => 757,
            _ => 500,
        };

        // Mostrar text punts
        if (textPuntsFinal != null)
        {
            textPuntsFinal.text = "Punts: " + puntsJugador + " / " + puntsNecessaris;
        }

        // Mostrar UI segons si ha guanyat o perdut
        bool victoria = puntsJugador >= puntsNecessaris;
        victoryUI.SetActive(victoria);
        gameOverUI.SetActive(!victoria);

        // 🎵 Reproduir música segons resultat
        if (victoria && victoryMusic != null)
        {
            audioSource.clip = victoryMusic;
            audioSource.Play();
        }
        else if (!victoria && gameOverMusic != null)
        {
            audioSource.clip = gameOverMusic;
            audioSource.Play();
        }

        // 🔽 Mostrar el cursor per a interactuar amb els botons
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Reiniciar()
    {
        if (GestorJoc.instancia != null)
            GestorJoc.instancia.Reiniciar();

        SceneManager.LoadScene("InicioJuego");
    }

    public void TornarAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}