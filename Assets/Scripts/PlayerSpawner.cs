using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform puntDeRespawn;

    void Start() {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");

        if (puntDeRespawn != null && jugador != null)
        {
            jugador.transform.position = puntDeRespawn.position;
        }
        else
        {
            Debug.LogWarning("Falta el punt de respawn o el jugador amb tag 'Player'.");
        }
    }
}