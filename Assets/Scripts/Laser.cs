using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Tooltip("Iniciar automaticamente? (Si no, se necesita que otro script inicie la Coroutine)")] public bool Init;

    [Tooltip("Cuanto tiempo se mantiene encendido")] public float OnTime;
    [Tooltip("Cuanto tiempo se mantiene apagado")] public float OffTime;

    private SpriteRenderer spriteRenderer;
    private Collider2D laserCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Asume que hay un SpriteRenderer en el objeto del láser
        laserCollider = GetComponent<Collider2D>(); // Usamos cualquier tipo de Collider2D (Box, Circle, etc.)

        if (Init) StartCoroutine(nameof(LaserCycle)); // Inicia el ciclo de encendido/apagado
    }

    IEnumerator LaserCycle()
    {
        while (true)
        {
            // Apagar láser
            spriteRenderer.enabled = false;
            laserCollider.enabled = false;
            yield return new WaitForSeconds(OffTime);

            // Encender láser
            spriteRenderer.enabled = true;
            laserCollider.enabled = true;
            yield return new WaitForSeconds(OnTime);
        }
    }

    // Detectar colisiones con el jugador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CombateJugador combateJugador = collision.GetComponent<CombateJugador>();
            if (combateJugador != null)
            {
                combateJugador.TomarDaño(10); // Ajusta el daño según sea necesario
            }
        }
    }
}
