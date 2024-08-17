using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int daño; 
    [SerializeField] private float timeD;


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
        if (other.CompareTag("Enemy"))
        {
            Enemy enemigo = other.GetComponent<Enemy>();
            if (enemigo != null)
            {
                enemigo.TomarDaño(daño);
            }
            Invoke("ObjDestroy", timeD);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("piso"))
        {
            Destroy(this.gameObject);
        }
    }
}
