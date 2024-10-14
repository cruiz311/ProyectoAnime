using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuertePlayer : MonoBehaviour
{
    [SerializeField] private GameObject objetoConAnimacion; // Prefab del objeto que aparecer� tras la destrucci�n
    [SerializeField] private string nombreAnimacion; // Nombre de la animaci�n que quieres reproducir
    [SerializeField] private float tiempoParaDestruir = 2f; // Tiempo antes de que el objeto actual sea destruido

    private void Start()
    {
        // Puedes decidir cu�ndo deseas destruir el objeto, por ejemplo:
        // Iniciar un temporizador para destruir el objeto despu�s de cierto tiempo
        Invoke("DestruirYGenerar", tiempoParaDestruir);
    }

    private void DestruirYGenerar()
    {
        // Instanciar el nuevo objeto en la misma posici�n que el objeto actual
        GameObject nuevoObjeto = Instantiate(objetoConAnimacion, transform.position, Quaternion.identity);

        // Obtener el Animator del nuevo objeto y reproducir la animaci�n elegida en el Inspector
        Animator animator = nuevoObjeto.GetComponent<Animator>();
        if (animator != null && !string.IsNullOrEmpty(nombreAnimacion))
        {
            animator.Play(nombreAnimacion); // Reproducir la animaci�n seleccionada
        }

        // Finalmente, destruir el objeto original
        Destroy(gameObject); // Esto destruye el objeto actual
    }
}