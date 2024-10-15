using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;       // Velocidad de movimiento del enemigo
    [SerializeField] private Transform[] ptsMov;              // Puntos entre los cuales el enemigo patrulla
    [SerializeField] private float distanciaMin;              // Distancia mínima para cambiar de punto
    [SerializeField] private GameObject dectAera, dectarea2;  // Zonas de detección del jugador

    private int indiceActual = 0;                             // Índice actual del punto de patrulla
    private SpriteRenderer spriteRenderer;                    // Controlador del sprite para girar al enemigo

    private void Start()
    {
        // Inicialización del SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar(); // Girar hacia la dirección correcta inicialmente
    }

    private void Update()
    {
        // Movimiento hacia el punto actual de patrulla
        transform.position = Vector2.MoveTowards(transform.position, ptsMov[indiceActual].position, velocidadMovimiento * Time.deltaTime);

        // Verificar si ha alcanzado el punto de patrulla actual
        if (Vector2.Distance(transform.position, ptsMov[indiceActual].position) < distanciaMin)
        {
            // Cambiar al siguiente punto de patrulla secuencialmente
            indiceActual = (indiceActual + 1) % ptsMov.Length;
            Girar(); // Girar hacia el nuevo punto de patrulla
        }
    }

    // Método para girar el sprite y activar/desactivar las áreas de detección
    private void Girar()
    {
        bool mirandoDerecha = transform.position.x < ptsMov[indiceActual].position.x;

        // Invertir el sprite según la dirección del movimiento
        spriteRenderer.flipX = !mirandoDerecha;

        // Activar/desactivar las áreas de detección según la dirección
        dectAera.SetActive(mirandoDerecha);
        dectarea2.SetActive(!mirandoDerecha);

        // Debug para verificar el giro
        Debug.Log("Girar: " + (mirandoDerecha ? "Derecha" : "Izquierda"));
    }

    // Método opcional para añadir una pausa entre los puntos de patrulla (si se quisiera)
    private IEnumerator PatrullarEntrePuntos()
    {
        while (true)
        {
            // Movimiento hacia el punto actual de patrulla
            transform.position = Vector2.MoveTowards(transform.position, ptsMov[indiceActual].position, velocidadMovimiento * Time.deltaTime);

            // Verificar si ha alcanzado el punto de patrulla actual
            if (Vector2.Distance(transform.position, ptsMov[indiceActual].position) < distanciaMin)
            {
                // Cambiar al siguiente punto de patrulla secuencialmente
                indiceActual = (indiceActual + 1) % ptsMov.Length;
                Girar(); // Girar hacia el nuevo punto de patrulla

                // Esperar un segundo antes de continuar la patrulla
                yield return new WaitForSeconds(1.0f);
            }
            yield return null;
        }
    }
}