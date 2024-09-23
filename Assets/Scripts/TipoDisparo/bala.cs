using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int daño;
    [SerializeField] private float timeD;
    [SerializeField] private int municionInicial = 50;  // Cantidad de munición para esta bala

    public int GetMunicionInicial()
    {
        return municionInicial;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        Invoke("ObjDestroy", timeD);
    }

    private void ObjDestroy()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si colisiona con un enemigo, le hace daño y destruye la bala
        if (other.CompareTag("Enemy"))
        {
            Enemy enemigo = other.GetComponent<Enemy>();
            if (enemigo != null)
            {
                enemigo.TomarDaño(daño);
            }
            ObjDestroy(); // Destruir la bala después de hacer daño
        }
        else
        {
            // Destruir la bala si colisiona con cualquier otro objeto
            ObjDestroy();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con el suelo o cualquier otro objeto, destruir la bala
        ObjDestroy();
    }
}

