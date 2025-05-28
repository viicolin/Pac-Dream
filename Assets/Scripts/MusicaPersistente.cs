using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicaPersistente : MonoBehaviour
{
    private static MusicaPersistente instancia;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Evita duplicats
        }
    }

    void Update()
    {
        // Si entrem a la escena de joc, eliminar la música del menú
        string escenaActual = SceneManager.GetActiveScene().name;
        if (escenaActual == "InicioJuego")
        {
            Destroy(gameObject);
        }
    }
}
