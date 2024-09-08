using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaSen : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int daño;
    [Tooltip("La frecuencia de la onda seno")]
    [SerializeField] private float frecuencia = 1f;
    [Tooltip("La amplitud de la onda seno")]
    [SerializeField] private float amplitud = 1f;
    [Tooltip("Factor de randomización de las balas")]
    public float Randomize = 0;

    [SerializeField] private int municionInicial = 30;  // Cantidad de munición para esta bala

    private float startX; // Posición inicial en X
    private bool invertirDireccion; // Determina si la dirección sinusoidal está invertida

    // Obtener la cantidad de munición inicial
    public int GetMunicionInicial()
    {
        return municionInicial;
    }

    private void Start()
    {
        startX = transform.position.x; // Guardar la posición inicial en X

        // Determinar si se invertirá la dirección sinusoidal
        invertirDireccion = Random.Range(0f, 1f) > 0.5f;
    }

    private void Update()
    {
        // Mover en la dirección horizontal
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);

        // Calcular la nueva posición en Y usando una función seno
        float y = Mathf.Sin((transform.position.x - startX) * frecuencia) * amplitud;

        // Invertir la dirección si es necesario
        if (invertirDireccion)
        {
            y = -y;
        }

        // Actualizar la posición en Y
        transform.position = new Vector2(transform.position.x, y + transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemigo = other.GetComponent<Enemy>();
            if (enemigo != null)
            {
                enemigo.TomarDaño(daño);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("suelo"))
        {
            Destroy(gameObject);
        }
    }
}
