using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaSen : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int da�o;
    [Tooltip("La frecuencia de la onda seno")]
    [SerializeField] private float frecuencia = 1f;
    [Tooltip(" La amplitud de la onda seno")]
    [SerializeField] private float amplitud = 1f;
    [Tooltip("Factor de randomizaci�n de las balas")]
    public float Randomize = 0;

    private float startX; // Posici�n inicial en X
    private int invertirDireccion; // Factor para invertir la direcci�n en Y

    private void Start()
    {
        startX = transform.position.x; // Guardar la posici�n inicial en X

        // Determinar si se invierte la direcci�n en Y con una probabilidad de 1/2
        invertirDireccion = Random.Range(0, 2) == 0 ? 1 : -1;
    }

    private void Update()
    {
        // Mover en la direcci�n horizontal
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);

        // Calcular la nueva posici�n en Y usando una funci�n seno
        float y = Mathf.Sin((transform.position.x - startX) * frecuencia) * amplitud * invertirDireccion;

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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("piso"))
        {
            Destroy(gameObject);
        }
    }
}
