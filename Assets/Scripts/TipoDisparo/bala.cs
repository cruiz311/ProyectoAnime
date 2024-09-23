using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int da�o;
    [SerializeField] private float timeD;
    [SerializeField] private int municionInicial = 50;  // Cantidad de munici�n para esta bala

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
        // Si colisiona con un enemigo, le hace da�o y destruye la bala
        if (other.CompareTag("Enemy"))
        {
            Enemy enemigo = other.GetComponent<Enemy>();
            if (enemigo != null)
            {
                enemigo.TomarDa�o(da�o);
            }
            ObjDestroy(); // Destruir la bala despu�s de hacer da�o
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

