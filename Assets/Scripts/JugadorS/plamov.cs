using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plamov : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 input;
    private Animator animator; // Referencia al Animator

    public bool sePuedeMover = true;
    [SerializeField] private Vector2 velocidadPvtazo;

    [Header("Configuración de Movimiento")]
    [SerializeField] private float velocidadMovimiento = 10f;
    [SerializeField] private float suavizadoMovimiento = 0.05f;
    [SerializeField] private float velocidadSubida = 5f;
    private float movimientoHorizontal = 0f;
    private Vector3 velocidad = Vector3.zero;
    public bool mirandoDerecha = true;

    [Header("Configuración de Salto")]
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private LayerMask esSuelo; // Define qué capas se consideran "suelo"
    [SerializeField] private Transform Csuelo;  // Punto de chequeo del suelo
    [SerializeField] private Vector3 dimensionesCaja;  // Dimensiones de la caja que chequea el suelo
    BoxCollider2D boxCollider;

    [Header("Rebote")]
    [SerializeField] private float velocidadRebote;

    private bool enSuelo;  // Variable para indicar si está en el suelo
    private bool salto = false;
    bool checkForLadders;  // Variable para detectar si está tocando una escalera

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>(); // Inicializamos el Animator
        rb2d.gravityScale = 1; // Asegura que la gravedad esté activada correctamente al inicio
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");

        // Solo permite el input vertical si el personaje está en una escalera
        if (checkForLadders)
        {
            input.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            input.y = 0; // Previene el movimiento vertical no deseado
        }

        movimientoHorizontal = input.x * velocidadMovimiento;
        Climb();
        CheckForLadders();

        // Parámetro isRunning: Activa la animación de correr
        animator.SetBool("isRunning", input.x != 0);

        // Parámetro isJumping: Activa la animación de salto
        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            salto = true;
            animator.SetBool("isJumping", true); // Activar salto en el Animator
        }

        // Parámetro velocityY: Controla la velocidad vertical para la transición
        animator.SetFloat("velocityY", rb2d.velocity.y);

        // Detecta si se presiona la tecla "B" para activar una acción especial
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.SetBool("isSpecialAction", true); // Activa la animación especial
        }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(Csuelo.position, dimensionesCaja, 0f, esSuelo);

        if (sePuedeMover)
        {
            Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
        }

        if (enSuelo)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isOnGround", true);
        }
        else
        {
            animator.SetBool("isOnGround", false);
        }

        salto = false;
    }

    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, velocidadObjetivo, ref velocidad, suavizadoMovimiento);

        if (mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            Girar();
        }

        if (enSuelo && saltar)
        {
            rb2d.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse); // Salta con impulso
            enSuelo = false;
        }
    }

    private void Climb()
    {
        // Si no está en una escalera, la gravedad vuelve a la normalidad
        if (!checkForLadders)
        {
            rb2d.gravityScale = 1; // Asegura que la gravedad esté activada cuando no hay escalera
            boxCollider.isTrigger = false;
            animator.SetBool("isClimbing", false); // Desactiva la animación de escalar
            return;
        }

        // Movimiento en la escalera
        var getDirection = Input.GetAxis("Vertical");

        if (checkForLadders && getDirection != 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, velocidadSubida * getDirection);
            rb2d.gravityScale = 0; // Desactiva la gravedad para permitir la subida
            boxCollider.isTrigger = true;
            animator.SetBool("isClimbing", true); // Activa la animación de escalar
        }
    }

    private void CheckForLadders()
    {
        // Detecta si el personaje está tocando una escalera
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("escaleras")))
        {
            checkForLadders = true;
        }
        else
        {
            checkForLadders = false;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Csuelo.position, dimensionesCaja);
    }
}