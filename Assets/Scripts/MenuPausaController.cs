using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausaController : MonoBehaviour
{
    public Slider sliderVolum;
    public AudioSource musicaFons; // opcional: si vols controlar música concreta
    public GameObject canvasHUD;
    public GameObject menuPausa;

    void Start() {
        float volum = PlayerPrefs.GetFloat("Volum", 1f);
        sliderVolum.value = volum;
        AudioListener.volume = volum;
        sliderVolum.onValueChanged.AddListener(CanviarVolum);

        // Assegura que el cursor estigui visible si estàs al menú pausa
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CanviarVolum(float valor)
    {
        PlayerPrefs.SetFloat("Volum", valor); // Guarda el volum seleccionat

        if (musicaFons != null)
        {
            musicaFons.volume = valor; // Només la música es veu afectada
        }
    }

    public void TornarAlJoc()
    {
        Time.timeScale = 1f;
        canvasHUD.SetActive(true);
        menuPausa.SetActive(false);

        // Oculta el cursor i el bloqueja quan tornes al joc
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReiniciarEscena()
    {
        Time.timeScale = 1f;

        if (GestorJoc.instancia != null)
        {
            GestorJoc.instancia.Reiniciar();
        }

        Scene escenaActual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escenaActual.name);
    }

    public void TornarAlMenu()
    {
        Time.timeScale = 1f;

        var gestor = FindObjectOfType<GestorJoc>();
        if (gestor != null)
        {
            Destroy(gestor.gameObject);
            Debug.Log("GestorJoc destruït en tornar al menú.");
        }

        SceneManager.LoadScene("Menu");
    }
}