using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovEnPlat : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Transform Csuelo;
    [SerializeField] private float distancia;
    [SerializeField] private bool moviendoIzquierda;

    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D informacionSuelo = Physics2D.Raycast(Csuelo.position, Vector2.down, distancia);

        rb2d.velocity = new Vector2(velocidad, rb2d.velocity.y);

        if (informacionSuelo.collider == null)
        {
            Girar();
        }
    }

    private void Girar()
    {
        moviendoIzquierda = !moviendoIzquierda;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Csuelo.transform.position, Csuelo.transform.position + Vector3.down * distancia);
    }
}
