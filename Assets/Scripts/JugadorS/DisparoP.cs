using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisparoP : MonoBehaviour
{
    [SerializeField] private Transform Cdisparo, disLeft, disArriba;
    [SerializeField] private GameObject balaBase;  // Prefab de la bala base (infinita)
    [SerializeField] private GameObject balaEspecial;  // Prefab de la bala especial (limitada)
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AudioSource music; // Música que se reproducirá solo al disparar balas especiales
    [SerializeField] private float fireRate = 0.2f; // Tiempo de espera entre disparos

    private int municion;
    private bool usandoBalaEspecial = false; // Controla si el jugador está usando la bala especial
    private float fireCooldown = 0f;
    private float timeSinceLastBullet = 0f;
    private bool isMusicPlaying = false;

    // Variable de control para permitir o no disparar
    public bool puedeDisparar = true;

    // Referencia al Animator
    private Animator animator;

    private void Start()
    {
        // Usamos la bala base al inicio con munición infinita
        CambiarABalaBase();
        animator = GetComponent<Animator>(); // Obtiene el componente Animator
    }

    private void Update()
    {
        // Control del cooldown de disparo
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }

        // Permitir movimiento del personaje si el jugador se mueve
        MoverPersonaje();

        // Solo permite disparar si puedeDisparar es true
        if (puedeDisparar)
        {
            Disparar();
        }

        // Control de la UI de munición
        if (usandoBalaEspecial)
        {
            text.text = municion.ToString();
        }
        else
        {
            text.text = "∞";  // Munición infinita
        }

        // Control del tiempo sin balas en escena
        if (!IsBulletInScene())
        {
            timeSinceLastBullet += Time.deltaTime;
            if (timeSinceLastBullet >= 3f && isMusicPlaying)
            {
                StopMusic();
            }
        }
        else
        {
            timeSinceLastBullet = 0f; // Resetea el tiempo si hay una bala en escena
        }

        // Activar la animación de emote con la tecla "b"
        if (Input.GetKeyDown(KeyCode.B))
        {
            ActivarEmote();
        }
    }

    private void MoverPersonaje()
    {
        // Lógica para mover el personaje. Puedes modificar esto según tu implementación de movimiento
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (moveX != 0 || moveY != 0)
        {
            var playerMovement = GetComponent<Plamov>(); // Cambiado a Plamov
            if (playerMovement != null)
            {
                playerMovement.enabled = true; // Asegura que el movimiento esté habilitado
            }
        }
        else
        {
            var playerMovement = GetComponent<Plamov>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false; // Deshabilita el movimiento si no hay entrada
            }
        }
    }

    private void Disparar()
    {
        // Disparo desde el pivote central
        if (Input.GetKeyDown(KeyCode.R) && fireCooldown <= 0f)
        {
            InstanciarBala(Cdisparo.position, Cdisparo.rotation);
            fireCooldown = fireRate; // Reiniciar el cooldown
        }

        // Disparo desde el pivote izquierdo con "q"
        if (Input.GetKeyDown(KeyCode.Q) && fireCooldown <= 0f)
        {
            InstanciarBala(disLeft.position, disLeft.rotation); // Usando disLeft como ejemplo
            ActivarAnimacion("DisparoIzquierda"); // Activar la animación correspondiente
            fireCooldown = fireRate; // Reiniciar el cooldown
        }

        // Disparo hacia arriba con "w"
        if (Input.GetKeyDown(KeyCode.E) && fireCooldown <= 0f)
        {
            InstanciarBala(disArriba.position, disArriba.rotation);
            ActivarAnimacion("DisparoArriba"); // Activar animación con trigger "DisparoArriba"
            fireCooldown = fireRate; // Reiniciar el cooldown
        }
    }

    private void InstanciarBala(Vector3 posicion, Quaternion rotacion)
    {
        if (usandoBalaEspecial)
        {
            Instantiate(balaEspecial, posicion, rotacion);
        }
        else
        {
            Instantiate(balaBase, posicion, rotacion);
        }
    }

    private bool IsBulletInScene()
    {
        // Verifica si hay alguna bala en la escena con el tag "Bala1"
        return GameObject.FindWithTag("Bala1") != null;
    }

    private void PlayMusic()
    {
        music.Play();
        isMusicPlaying = true;
    }

    private void StopMusic()
    {
        music.Stop();
        isMusicPlaying = false;
    }

    // Método para cambiar a la bala base (infinita)
    public void CambiarABalaBase()
    {
        usandoBalaEspecial = false;
        text.text = "<size=9450%>∞</size>";
    }

    // Método para cambiar a la bala especial con munición limitada
    public void CambiarABalaEspecial(GameObject nuevaBalaEspecial)
    {
        balaEspecial = nuevaBalaEspecial;
        BalaSen balaComponent = nuevaBalaEspecial.GetComponent<BalaSen>();
        if (balaComponent != null)
        {
            municion = balaComponent.GetMunicionInicial();
            usandoBalaEspecial = true;
        }
    }

    // Método para bloquear disparo durante el retroceso
    public void BloquearDisparoTemporalmente(float tiempoBloqueo)
    {
        StartCoroutine(BloquearDisparo(tiempoBloqueo));
    }

    private IEnumerator BloquearDisparo(float tiempoBloqueo)
    {
        puedeDisparar = false;
        yield return new WaitForSeconds(tiempoBloqueo);
        puedeDisparar = true;
    }

    private void ActivarAnimacion(string nombreAnimacion)
    {
        animator.SetTrigger(nombreAnimacion); // Activa el trigger correspondiente
    }

    private void ActivarEmote()
    {
        // Activar el trigger del emote
        animator.SetTrigger("Emote");

        // Deshabilitar temporalmente el disparo y movimiento
        puedeDisparar = false;

        // Iniciar una corrutina para restaurar el estado normal después de la animación
        StartCoroutine(DesactivarEmote());
    }

    private IEnumerator DesactivarEmote()
    {
        // Espera a que termine la animación del emote (ajusta el tiempo según la duración de la animación)
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Volver al estado normal
        puedeDisparar = true;
    }
}