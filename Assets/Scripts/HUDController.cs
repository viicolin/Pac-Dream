using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI textPunts;
    public GameObject Cor; // Prefab del cor
    public Transform contenidorCors;

    public GameObject canvasHUD;    // Canvas del HUD (actiu durant el joc)
    public GameObject menuPausa;    // Panell o canvas del menú de pausa

    private List<GameObject> llistaCors = new List<GameObject>();
    private int videsActuals = -1;

    void Start()
    {
        menuPausa.SetActive(false); // Assegura que el menú està ocult al principi
        GenerarCors();
    }

    void Update()
    {
        if (GestorJoc.instancia != null)
        {
            int puntsActuals = GestorJoc.instancia.punts;
            int puntsNecessaris = TrobarPuntsNecessaris();
            textPunts.text = "Punts: " + puntsActuals + " / " + puntsNecessaris;

            int novesVides = GestorJoc.instancia.vides;
            if (novesVides != videsActuals)
            {
                ActualitzarCors(novesVides);
            }
        }

        DetectarPausa();
    }

    void GenerarCors()
    {
        int videsInicials = GestorJoc.instancia != null ? GestorJoc.instancia.vides : 3;

        for (int i = 0; i < videsInicials; i++)
        {
            GameObject corNou = Instantiate(Cor, contenidorCors);
            corNou.SetActive(true);
            llistaCors.Add(corNou);
        }

        videsActuals = videsInicials;
    }

    void ActualitzarCors(int videsRestants)
    {
        for (int i = 0; i < llistaCors.Count; i++)
        {
            llistaCors[i].SetActive(i < videsRestants);
        }

        videsActuals = videsRestants;
    }

    private int TrobarPuntsNecessaris()
    {
        string dificultat = PlayerPrefs.GetString("Dificultat", "Normal");
        return dificultat switch
        {
            "Facil" => 250,
            "Dificil" => 757,
            _ => 500,
        };
    }

    void DetectarPausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool estaPausat = Time.timeScale == 0f;

            if (!estaPausat)
            {
                PausarJoc();
            }
            else
            {
                ReprendreJoc();
            }
        }
    }

    public void PausarJoc()
    {
        Time.timeScale = 0f;
        canvasHUD.SetActive(false);
        menuPausa.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Debug.Log("Joc pausat");
    }

    public void ReprendreJoc()
    {
        Time.timeScale = 1f;
        canvasHUD.SetActive(true);
        menuPausa.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Debug.Log("Joc reprès");
    }
}