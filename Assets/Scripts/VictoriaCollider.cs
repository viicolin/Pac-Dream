using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoriaCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador ha arribat a la sortida. Carregant FinalJuego...");
            SceneManager.LoadScene("FinalJuego");
        }
    }
}