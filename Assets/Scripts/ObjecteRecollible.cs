using UnityEngine;

public class ObjecteRecollible : MonoBehaviour
{
    public int punts = 1;
    public GameObject efecteParticules; // ← assigna-ho des de l’Inspector
    private bool jaRecollit = false;

    void OnTriggerEnter(Collider altre)
    {
        if (jaRecollit) return;
        if (!altre.CompareTag("Player")) return;

        jaRecollit = true;

        // Afegir punts
        if (GestorJoc.instancia != null)
        {
            GestorJoc.instancia.AfegirPunts(punts);
        }

        // Instanciar partícules
        if (efecteParticules != null)
        {
            GameObject part = Instantiate(efecteParticules, transform.position, Quaternion.identity);
            Destroy(part, 2f); // Elimina després de 2 segons
        }

        Destroy(gameObject); // Elimina el punt
    }

    void Update()
    {
        // Evita animar el punt si el joc està pausat
        if (Time.timeScale == 0f) return;

        transform.Rotate(0, 50 * Time.deltaTime, 0);
        transform.position += new Vector3(0, Mathf.Sin(Time.time * 2f) * 0.001f, 0);
    }
}