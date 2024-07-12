/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootburbuja : MonoBehaviour
{
    [SerializeField] private bool playerDetected;

    [SerializeField, Header("Variables"), Tooltip("Número de balas que se dispara en cada ráfaga")] private int Balas;
    [SerializeField, Tooltip("Tiempo de espera entre ráfagas")] private float Cd;
    [SerializeField, Tooltip("Tiempo de espera entre balas en cada ráfaga")] private float CdIndv;
    [SerializeField, Tooltip("El GO de la bala")] private GameObject BalaGO;

    [SerializeField, Tooltip("Añade todos los colliders de este enemigo")] private List<Collider2D> EnemyColliders;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == PlayerDetect.Instance._player && !playerDetected)
        {
            playerDetected = true;
            StartCoroutine(nameof(ShootPlayer));
        }
    }

    IEnumerator ShootPlayer()
    {
        yield return new WaitForSeconds(Cd / 2);

        while (playerDetected)
        {
            yield return null;

            List<GameObject> balas = new List<GameObject>();

            for (int i = 0; i < Balas; i++)
            {
                yield return new WaitForSeconds(CdIndv);

                GameObject aBala = Instantiate(BalaGO, this.transform.position, Quaternion.identity);
                balas.Add(aBala);

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

            yield return new WaitForSeconds(Cd);

            foreach (GameObject bala in balas)
            {
                bala.GetComponent<BalaBhvFALL>().StartFalling();
                yield return new WaitForSeconds(CdIndv);
            }
        }
    }
}
*/