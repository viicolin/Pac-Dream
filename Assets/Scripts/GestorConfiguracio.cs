using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestorConfiguracio : MonoBehaviour
{
    public Button botoFacil, botoNormal, botoDificil;
    public Slider controlVolum;
    public AudioSource musicaDeFons;

    private void Start()
    {
        // Carregar dificultat desada
        string dificultat = PlayerPrefs.GetString("Dificultat", "Normal");
        ActualitzarEstatBotons(dificultat);

        // Carregar volum desat
        float volum = PlayerPrefs.GetFloat("Volum", 1f);
        controlVolum.value = volum;
        AudioListener.volume = volum;

        controlVolum.onValueChanged.AddListener(CanviarVolum);
    }

    public void SeleccionarDificultat(string nivell) {
        PlayerPrefs.SetString("Dificultat", nivell);
        PlayerPrefs.Save();
        ActualitzarEstatBotons(nivell);

        // Mostrar dificultat seleccionada
        string dificultat = PlayerPrefs.GetString("Dificultat", "No definida");
        Debug.Log("Dificultat seleccionada: " + dificultat);
    }

    void ActualitzarEstatBotons(string nivell)
    {
        botoFacil.interactable = nivell != "Facil";
        botoNormal.interactable = nivell != "Normal";
        botoDificil.interactable = nivell != "Dificil";
    }

    public void CanviarVolum(float valor)
    {
        AudioListener.volume = valor;
        PlayerPrefs.SetFloat("Volum", valor);
    }

    public void TornarAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}