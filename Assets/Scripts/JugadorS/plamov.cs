using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plamov : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 input;

    public bool sePuedeMover = true;
    [SerializeField] private Vector2 velocidadPvtazo;

    [Header("Configuración de Movimiento")]
    [SerializeField] private float velocidadMovimiento = 10f;
    [SerializeField] private float suavizadoMovimiento = 0.05f;
    [SerializeField] private float velocidadSubida = 5f;
    private float movimientoHorizontal = 0f;
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;

    [Header("Configuración de Salto")]
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private LayerMask esSuelo;
    [SerializeField] private Transform Csuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    BoxCollider2D boxCollider;

    [Header("Rebote")]
    [SerializeField] private float velocidadRebote;

    private bool enSuelo;

    private bool salto = false;

    bool checkForLadders;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    
    }

    private void Update()
    {
        
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        movimientoHorizontal = input.x * velocidadMovimiento;
        Climb();
        CheckForLadders();

        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            salto = true;
        }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(Csuelo.position, dimensionesCaja, 0f, esSuelo);
         if (sePuedeMover)
        {
            Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
        }
        
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

    private void Climb()
    {
        if (!checkForLadders) {

            boxCollider.isTrigger = false;
            rb2d.gravityScale = 1;
            return;}
        var getDirection = Input.GetAxis("Vertical");
        if(checkForLadders && Input.GetAxis("Vertical") !=0 )
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, velocidadSubida*getDirection);
            boxCollider.isTrigger = true;
            rb2d.gravityScale = 0;
        }
    }

    public void Rebote()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, velocidadRebote);
    }

    public void Pvtazo(Vector2 puntoGolpe)
    {
        rb2d.velocity = new Vector2(-velocidadPvtazo.x * puntoGolpe.x, velocidadPvtazo.y);
    }



    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 180, 0);
    }

    private void CheckForLadders()
    {
        if(boxCollider.IsTouchingLayers(LayerMask.GetMask("escaleras")))
        {
            checkForLadders = true;
        }
        else
        {
            checkForLadders = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Csuelo.position, dimensionesCaja);
    }
}
