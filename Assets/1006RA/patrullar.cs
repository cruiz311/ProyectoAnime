using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform[] ptsMov;
    [SerializeField] private float distanciaMin;

    private int numeroAleatorio;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        numeroAleatorio = Random.Range(0, ptsMov.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, ptsMov[numeroAleatorio].position, velocidadMovimiento * Time.deltaTime);

        if (Vector2.Distance(transform.position, ptsMov[numeroAleatorio].position) < distanciaMin)
        {
            numeroAleatorio = Random.Range(0, ptsMov.Length);
            Girar();
        }
    }

    private void Girar()
    {
        if (transform.position.x < ptsMov[numeroAleatorio].position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}
