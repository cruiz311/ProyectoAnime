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
            Destroy(gameObject, 1f); // Destruye el objeto inmediatamente
        }
    }
}
