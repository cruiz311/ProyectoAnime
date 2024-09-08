using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    [Tooltip("Iniciar automaticamente? (Si no, se necesita que otro script inicie la Coroutine)")] public bool Init;

    [Tooltip("Cuanto tiempo se mantiene encendido")] public float OnTime;
    [Tooltip("Cuanto tiempo se mantiene apagado")] public float OffTime;
    [Tooltip("Hacia que dirección va el lase. Se puede poner directamente la coordenada de destino")] public Vector2 Direction;

    [Tooltip("Contra que colisina el Laser")] public LayerMask layer;

    private LineRenderer lR;
    private EdgeCollider2D eC;

    void Start()
    {
        lR = GetComponent<LineRenderer>();

        // Ajusta el grosor del láser
        lR.startWidth = 0.2f; // Grosor al inicio del láser
        lR.endWidth = 0.2f;   // Grosor al final del láser

        // Añade una textura al LineRenderer
        lR.material = new Material(Shader.Find("Unlit/Texture"));
        lR.material.mainTexture = Resources.Load<Texture2D>("Textures/LaserTexture"); // Usa una textura personalizada

        if (Init) StartCoroutine(nameof(StartCounter));
    }


    IEnumerator StartCounter()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction.normalized, 100f, layer);

        if (hit.collider != null)
        {
            lR.useWorldSpace = true;
            lR.SetPosition(0, transform.position);
            lR.SetPosition(1, hit.point);

            List<Vector2> a = new()
            {
                Vector2.zero,
                hit.point - (Vector2)transform.position
            };

            eC.SetPoints(a);
        }
        else yield break;

        while (true)
        {
            lR.enabled = false;
            eC.enabled = false;
            yield return new WaitForSeconds(OffTime);

            lR.enabled = true;
            eC.enabled = true;
            yield return new WaitForSeconds(OnTime);
        }
    }

    // Detectar colisiones con el jugador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Usamos el script CombateJugador para hacer daño al jugador
            CombateJugador combateJugador = collision.GetComponent<CombateJugador>();
            if (combateJugador != null)
            {
                combateJugador.TomarDaño(10); // Ajusta el valor del daño según sea necesario
            }
        }
    }
}
