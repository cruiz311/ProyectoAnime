using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShootUp : MonoBehaviour
{
    [SerializeField] private bool playerDetected;

    [SerializeField, Header("Variables"), Tooltip("Número de balas que se dispara en cada ráfaga")] private int Balas;
    [SerializeField, Tooltip("Tiempo de espera entre ráfagas")] private float Cd;
    [SerializeField, Tooltip("Tiempo de espera entre balas en cada ráfaga")] private float CdIndv;
    [SerializeField, Tooltip("El GO de la bala")] private GameObject BalaGO;
    public SpriteRenderer spriteRenderer;
    [SerializeField, Tooltip("Añade todos los colliders de este enemigo")] private List<Collider2D> EnemyColliders;
    [SerializeField, Tooltip("El Animator para manejar las animaciones")] private Animator animator; // Se añade el Animator

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerDetected)
        {
            playerDetected = true;
            StartCoroutine(nameof(ShootPlayer));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerDetected)
        {
            playerDetected = false;
        }
    }

    IEnumerator ShootPlayer()
    {
        yield return new WaitForSeconds(Cd / 2);

        while (playerDetected)
        {
            yield return null;

            // Activa la animación de disparo al comenzar a disparar
            animator.SetBool("isShooting", true);

            for (int i = 0; i < Balas; i++)
            {
                yield return new WaitForSeconds(CdIndv);

                GameObject aBala = Instantiate(BalaGO, this.transform.position, Quaternion.identity);
                if (!spriteRenderer.flipX)
                {
                    transform.position -= new Vector3(0.18f, 0, 0);
                }
                else
                {
                    transform.position += new Vector3(0.18f, 0, 0);
                }

                foreach (Collider2D collider_ in GetComponents<Collider2D>())
                {
                    EnemyColliders.Add(collider_);
                }

                foreach (Collider2D collider_ in GetComponentsInChildren<Collider2D>())
                {
                    EnemyColliders.Add(collider_);
                }

                foreach (Collider2D collider_ in EnemyColliders)
                {
                    Physics2D.IgnoreCollision(collider_, aBala.GetComponent<Collider2D>());
                }
            }

            // Desactiva la animación de disparo después de cada ráfaga
            animator.SetBool("isShooting", false);

            yield return new WaitForSeconds(Cd);
        }
    }
}