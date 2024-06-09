using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatMov : MonoBehaviour
{
    [SerializeField] private Transform[] ptsMov;
    [SerializeField] private float velocidadMovimiento;

    private int siguientePlat = 1;
    private bool ordenPlat = true;

    private void Update()
    {
        // Cambia la dirección si alcanza el último punto
        if (ordenPlat && siguientePlat + 1 >= ptsMov.Length)
        {
            ordenPlat = false;
        }

        // Cambia la dirección si alcanza el primer punto
        if (!ordenPlat && siguientePlat <= 0)
        {
            ordenPlat = true;
        }

        // Mueve la plataforma hacia el siguiente punto
        if (Vector2.Distance(transform.position, ptsMov[siguientePlat].position) < 0.1f)
        {
            if (ordenPlat)
            {
                siguientePlat += 1;
            }
            else
            {
                siguientePlat -= 1;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, ptsMov[siguientePlat].position, velocidadMovimiento * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
