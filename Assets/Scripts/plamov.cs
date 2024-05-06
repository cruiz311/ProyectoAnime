    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plamov : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [Header("Configuración de Movimiento")]
    [SerializeField] private float velocidadMovimiento = 10f;
    [SerializeField] private float suavizadoMovimiento = 0.05f;
    private float movimientoHorizontal = 0f;
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;

    [Header("Configuración de Salto")]
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private LayerMask esSuelo;
    [SerializeField] private Transform Csuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    private bool enSuelo;

    private bool salto = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadMovimiento;

        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            salto = true;
        }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(Csuelo.position, dimensionesCaja, 0f, esSuelo);
        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
        salto = false;
    }

    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, velocidadObjetivo, ref velocidad, suavizadoMovimiento);

        if (mover < 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (mover > 0 && mirandoDerecha)
        {
            Girar();
        }

        if (enSuelo && saltar)
        {
            rb2d.AddForce(new Vector2(0f, fuerzaSalto));
            enSuelo = false;
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Csuelo.position, dimensionesCaja);
    }
}
