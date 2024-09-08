using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombateJugador : MonoBehaviour
{
    [SerializeField] int vida;
    [SerializeField] int maxVida;
    [SerializeField] int vidasRestantes = 3; // Tres vidas iniciales
    public UnityEvent<int> cambioVida;

    [Header("Retroceso")]
    public float fuerzaRetroceso = 5f; // Controla la fuerza del retroceso
    private Rigidbody2D rb;
    private Plamov movimientoJugador; // Referencia al script de movimiento
    private DisparoP disparoJugador;  // Referencia al script de disparo

    private bool puedeMoverse = true; // Controla si el jugador puede moverse

    private void Start()
    {
        vida = maxVida;
        cambioVida.Invoke(vidasRestantes);  // Invoca el cambio con las vidas restantes
        rb = GetComponent<Rigidbody2D>();  // Obtiene el componente Rigidbody2D
        movimientoJugador = GetComponent<Plamov>(); // Obtiene el script de movimiento
        disparoJugador = GetComponent<DisparoP>();  // Obtiene el script de disparo
    }

    public void TomarDaño(int daño)
    {
        if (!puedeMoverse) return; // Si no puede moverse, no toma más daño

        vida -= daño;
        if (vida <= 0)
        {
            vidasRestantes--;
            if (vidasRestantes > 0)
            {
                vida = maxVida; // Restaura la vida al máximo cuando pierde una vida
            }
            else
            {
                Muerte();
            }
        }

        // Calcula la dirección de retroceso basada en hacia dónde mira el jugador
        Vector2 direccionRetroceso = movimientoJugador.mirandoDerecha ? Vector2.right : Vector2.left;

        // Obtiene la velocidad actual del jugador
        Vector2 velocidadJugador = rb.velocity;

        // Si la velocidad del jugador es alta, limita el retroceso máximo
        float factorRetroceso = Mathf.Max(velocidadJugador.magnitude, 1f); // Evita un factor de retroceso menor a 1
        Vector2 fuerzaRetrocesoProporcional = direccionRetroceso * factorRetroceso * fuerzaRetroceso;

        // Aplica la fuerza de retroceso proporcional a la velocidad
        rb.AddForce(-velocidadJugador.normalized * fuerzaRetrocesoProporcional.magnitude, ForceMode2D.Impulse);

        // Desactiva el movimiento por 2 segundos
        StartCoroutine(DesactivarMovimiento(2f));

        // Bloquea el disparo por 2 segundos
        if (disparoJugador != null)
        {
            disparoJugador.BloquearDisparoTemporalmente(2f);
        }

        cambioVida.Invoke(vidasRestantes);  // Actualiza el evento con las vidas restantes
    }


    IEnumerator DesactivarMovimiento(float duracion)
    {
        movimientoJugador.sePuedeMover = false; // Desactiva el movimiento en Plamov
        yield return new WaitForSeconds(duracion);
        movimientoJugador.sePuedeMover = true; // Reactiva el movimiento
    }

    public void Muerte()
    {
        Destroy(gameObject);
    }

    public int ObtenerVidasRestantes()
    {
        return vidasRestantes;
    }
}
