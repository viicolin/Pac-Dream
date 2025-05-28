using UnityEngine;

public class Player : MonoBehaviour
{
    public bool invulnerable = false;
    public float tempsInvulnerabilitat = 2f;

    private Renderer renderitzador;

    void Start()
    {
        // Busquem el component Renderer (per canviar color)
        renderitzador = GetComponentInChildren<Renderer>();
    }

    public void RebreDany()
    {
        if (!invulnerable)
        {
            GestorJoc.instancia.PerdreVida();
            StartCoroutine(ActivarInvulnerabilitat());
        }
    }

    private System.Collections.IEnumerator ActivarInvulnerabilitat()
    {
        invulnerable = true;

        // canvi de color a vermell
        if (renderitzador != null)
            renderitzador.material.color = Color.red;

        yield return new WaitForSeconds(tempsInvulnerabilitat);

        // Tornem al color original
        if (renderitzador != null)
            renderitzador.material.color = Color.white;

        invulnerable = false;
    }
}
