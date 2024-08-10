using System.Collections;
using UnityEngine;

public class Recolectable : MonoBehaviour
{
    [SerializeField] private float cantidadPuntos;
    [SerializeField] private Puntaje puntaje;
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("efecto", true);
            puntaje.SumarPuntos(cantidadPuntos);
            StartCoroutine(DestruirDespuesDeDelay(1.5f)); // Inicia la corrutina con un retraso de 2 segundos
        }
    }

    private IEnumerator DestruirDespuesDeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
