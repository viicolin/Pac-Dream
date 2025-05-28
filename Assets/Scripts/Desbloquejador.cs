using UnityEngine;

public class Desbloquejador : MonoBehaviour
{
    public GameObject[] objectesADesactivar;
    public GameObject[] objectesAActivar;
    public AudioSource soActivacio;

    private bool jaExecutat = false;
    private int puntsNecessaris;

    void Start()
    {
        // Assignar punts segons dificultat
        string dificultat = PlayerPrefs.GetString("Dificultat", "Normal");

        switch (dificultat)
        {
            case "Facil":
                puntsNecessaris = 250;
                break;
            case "Dificil":
                puntsNecessaris = 757;
                break;
            default:
                puntsNecessaris = 500;
                break;
        }

        Debug.Log("Punts necessaris per obrir la porta: " + puntsNecessaris);
    }

    void Update()
    {
        if (!jaExecutat && GestorJoc.instancia != null && GestorJoc.instancia.punts >= puntsNecessaris)
        {
            ExecutarAccio();
        }
    }

    void ExecutarAccio()
    {
        Debug.Log("Punts suficients! Executant canvi de zona...");

        foreach (GameObject obj in objectesADesactivar)
        {
            if (obj != null) obj.SetActive(false);
        }

        foreach (GameObject obj in objectesAActivar)
        {
            if (obj != null) obj.SetActive(true);
        }

        if (soActivacio != null)
        {
            soActivacio.Play();
        }

        jaExecutat = true;
    }
}