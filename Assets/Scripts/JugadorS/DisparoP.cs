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

    private void Start()
    {
        // Usamos la bala base al inicio con munición infinita
        CambiarABalaBase();
    }

    private void Update()
    {
        // Control del cooldown de disparo
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }

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
    }

    private void Disparar()
    {
        float posDisY = Input.GetAxisRaw("Vertical");
        float posDisX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Fire1") && fireCooldown <= 0f)
        {
            if (posDisY <= 0)
            {
                InstanciarBala(Cdisparo.position, Cdisparo.rotation);
            }
            else if (posDisY > 0 && posDisX == 0)
            {
                InstanciarBala(disArriba.position, disArriba.rotation);
            }
            else if (posDisY > 0 && (posDisX < 0 || posDisX > 0))
            {
                InstanciarBala(disLeft.position, disLeft.rotation);
            }

            // Si está usando bala especial, reducir la munición
            if (usandoBalaEspecial)
            {
                municion--;
                if (municion <= 0)
                {
                    CambiarABalaBase();  // Cambiar a bala base cuando se acabe la munición especial
                }

                // Reproducir la música solo al disparar balas especiales
                if (!isMusicPlaying)
                {
                    PlayMusic();
                }
            }

            // Reiniciar el cooldown de disparo
            fireCooldown = fireRate;
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
}
