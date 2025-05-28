using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashEntrada : MonoBehaviour
{
    public float duracioSplash = 5f;
    private float tempsActual = 0f;

    void Update()
    {
        tempsActual += Time.deltaTime;

        if (tempsActual >= duracioSplash)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
