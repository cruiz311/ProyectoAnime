using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaSen : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int daño;
    [Tooltip("La frecuencia de la onda seno")]
    [SerializeField] private float frecuencia = 1f;
    [Tooltip(" La amplitud de la onda seno")]
    [SerializeField] private float amplitud = 1f;
    [Tooltip("Factor de randomización de las balas")]
    public float Randomize = 0;

    private float startX; // Posición inicial en X
    private int invertirDireccion; // Factor para invertir la dirección en Y

    private void Start()
    {
        startX = transform.position.x; // Guardar la posición inicial en X

        // Determinar si se invierte la dirección en Y con una probabilidad de 1/2
        invertirDireccion = Random.Range(0, 2) == 0 ? 1 : -1;
    }

    private void Update()
    {
        // Mover en la dirección horizontal
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);

        // Calcular la nueva posición en Y usando una función seno
        float y = Mathf.Sin((transform.position.x - startX) * frecuencia) * amplitud * invertirDireccion;

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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("piso"))
        {
            Destroy(gameObject);
        }
    }
}
