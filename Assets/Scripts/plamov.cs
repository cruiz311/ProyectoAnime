using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plamov : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 input;
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


    [Header("escalar")]
    [SerializeField] private float velocidadEscalar;
    private BoxCollider2D boxCollider2D;

    private float gravedadInicial;
    private bool escalando;

    private bool enSuelo;

    private bool salto = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        gravedadInicial = rb2d.gravityScale;
    }

    private void Update()
    {
        
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        movimientoHorizontal = input.x * velocidadMovimiento;


        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            salto = true;
        }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(Csuelo.position, dimensionesCaja, 0f, esSuelo);
        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
        Escalar();
        salto = false;
    }

    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, velocidadObjetivo, ref velocidad, suavizadoMovimiento);

        if (mover > 0 && mirandoDerecha)
        {
            Girar();
        }
        else if (mover < 0 && !mirandoDerecha)
        {
            Girar();
        }

        if (enSuelo && saltar)
        {
            rb2d.AddForce(new Vector2(0f, fuerzaSalto));
            enSuelo = false;
        }
    }

    private void Escalar()  
    {
        if ((input.y !=0 || escalando) && (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("escaleras")))) 
        {
            Vector2 velocidadSubida = new Vector2(rb2d.velocity.x, input.y * velocidadEscalar);
            rb2d.velocity = velocidadSubida;
            rb2d.gravityScale = 0;
            escalando = true;
        }
        else
        {
            rb2d.gravityScale = gravedadInicial;
            escalando = false;
        }

        if (enSuelo)
        {
            escalando = false;
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
