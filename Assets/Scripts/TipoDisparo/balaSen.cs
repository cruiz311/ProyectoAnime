using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaSen : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int da�o;
    [Tooltip("La frecuencia de la onda seno")]
    [SerializeField] private float frecuencia = 1f;
    [Tooltip("La amplitud de la onda seno")]
    [SerializeField] private float amplitud = 1f;
    [Tooltip("Factor de randomizaci�n de las balas")]
    public float Randomize = 0;

    [SerializeField] private int municionInicial = 30;  // Cantidad de munici�n para esta bala

    private float startX; // Posici�n inicial en X
    private bool invertirDireccion; // Determina si la direcci�n sinusoidal est� invertida

    // Obtener la cantidad de munici�n inicial
    public int GetMunicionInicial()
    {
        return municionInicial;
    }

    private void Start()
    {
        startX = transform.position.x; // Guardar la posici�n inicial en X

        // Determinar si se invertir� la direcci�n sinusoidal
        invertirDireccion = Random.Range(0f, 1f) > 0.5f;
    }

    private void Update()
    {
        // Mover en la direcci�n horizontal
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);

        // Calcular la nueva posici�n en Y usando una funci�n seno
        float y = Mathf.Sin((transform.position.x - startX) * frecuencia) * amplitud;

        // Invertir la direcci�n si es necesario
        if (invertirDireccion)
        {
            y = -y;
        }

        // Actualizar la posici�n en Y
        transform.position = new Vector2(transform.position.x, y + transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemigo = other.GetComponent<Enemy>();
            if (enemigo != null)
            {
                enemigo.TomarDa�o(da�o);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("suelo"))
        {
            Destroy(gameObject);
        }
    }
}
