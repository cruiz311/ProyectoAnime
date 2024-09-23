using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    public float velocidad;
    public int daño;
    public float radioGiro = 1f; // Radio del giro en el aire
    public float velocidadGiro = 2f; // Velocidad del giro (qué tan rápido hace el 360°)
    private float tiempoVida = 2f; // Tiempo antes de estrellarse

    private float tiempo = 0f; // Contador de tiempo para el giro

    private Vector3 posicionInicial;

    private void Start()
    {
        posicionInicial = transform.position;
    }

    private void Update()
    {
        // Incrementamos el contador de tiempo para hacer el giro
        tiempo += Time.deltaTime;

        // Movimiento hacia adelante en el eje X
        float movimientoX = velocidad * Time.deltaTime;

        // Movimiento circular en el aire (giro de 360°)
        float movimientoY = Mathf.Cos(tiempo * velocidadGiro) * radioGiro; // Movimiento en el eje Y (sube y baja)
        float movimientoZ = Mathf.Sin(tiempo * velocidadGiro) * radioGiro; // Movimiento en el eje Z (giro en 360°)

        // Actualizar la posición de la bala con un giro completo
        transform.position += new Vector3(movimientoX, movimientoY, movimientoZ);

        // Destruir la bala después de cierto tiempo o cuando toque el suelo
        DestroyBalaEnemigo();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CombateJugador combatejugador))
        {
            combatejugador.TomarDaño(daño);
            Destroy(gameObject);
        }
    }

    void DestroyBalaEnemigo()
    {
        // Destruir la bala después del tiempo establecido
        Destroy(gameObject, tiempoVida);
    }
}
