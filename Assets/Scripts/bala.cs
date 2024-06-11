using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    [SerializeField] private float velocidad;

    [SerializeField] private int daño;

    private void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        DestroyBala();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Debug.Log("aaa");
            other.GetComponent<Enemy>().TomarDaño(daño);
            Destroy(gameObject);
        }
    }
    void DestroyBala()
    {
        Destroy(gameObject, 1f);
    }
}
