using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class CombateJugador : MonoBehaviour
{
    [SerializeField] int vida;
    [SerializeField] int maxVida;
    [SerializeField] int vidasRestantes = 3; // Tres vidas iniciales
    public UnityEvent<int> cambioVida;
    public event EventHandler MuerteJugador;

    [Header("Retroceso")]
    public float fuerzaRetroceso = 5f; // Controla la fuerza del retroceso
    private Rigidbody2D rb;
    private Plamov movimientoJugador; // Referencia al script de movimiento
    private DisparoP disparoJugador;  // Referencia al script de disparo

    private bool puedeMoverse = true; // Controla si el jugador puede moverse
    private bool puedeRecibirDa�o = true; // Controla si el jugador puede recibir da�o

    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer del objeto hijo

    private void Start()
    {
        vida = maxVida;
        cambioVida.Invoke(vidasRestantes);  // Invoca el cambio con las vidas restantes
        rb = GetComponent<Rigidbody2D>();  // Obtiene el componente Rigidbody2D
        movimientoJugador = GetComponent<Plamov>(); // Obtiene el script de movimiento
        disparoJugador = GetComponent<DisparoP>();  // Obtiene el script de disparo
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // Obtiene el SpriteRenderer del hijo

        // Verifica si el SpriteRenderer est� presente
        if (spriteRenderer == null)
        {
            Debug.LogError("No hay un SpriteRenderer en el hijo del jugador. Aseg�rate de que exista.");
        }
    }

    public void TomarDa�o(int da�o)
    {
        if (!puedeRecibirDa�o || !puedeMoverse) return; // Si no puede recibir da�o o moverse, no toma m�s da�o

        vida -= da�o;
        if (vida <= 0)
        {
            vidasRestantes--;
            if (vidasRestantes > 0)
            {
                vida = maxVida; // Restaura la vida al m�ximo cuando pierde una vida
                StartCoroutine(InvulnerabilidadTemporal()); // Inicia la invulnerabilidad temporal
            }
            else
            {
                Muerte();
            }
        }

        // Calcula la direcci�n de retroceso basada en hacia d�nde mira el jugador
        Vector2 direccionRetroceso = movimientoJugador.mirandoDerecha ? Vector2.right : Vector2.left;

        // Obtiene la velocidad actual del jugador
        Vector2 velocidadJugador = rb.velocity;

        // Si la velocidad del jugador es alta, limita el retroceso m�ximo
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

    IEnumerator InvulnerabilidadTemporal()
    {
        puedeRecibirDa�o = false;

        if (spriteRenderer != null)
        {
            for (int i = 0; i < 14; i++) // Parpadea durante 7 segundos
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                yield return new WaitForSeconds(0.25f);
            }
            spriteRenderer.enabled = true; // Asegura que el sprite se muestre al final
        }

        puedeRecibirDa�o = true;
    }

    public void Muerte()
    {
        MuerteJugador?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    public int ObtenerVidasRestantes()
    {
        return vidasRestantes;
    }
}
