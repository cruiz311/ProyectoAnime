using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class EnemyShootUp : MonoBehaviour
{
    [SerializeField] private bool playerDetected;

    [SerializeField, Header("Variables"), Tooltip("Número de balas que se dispara en cada ráfaga")] private int Balas;
    [SerializeField, Tooltip("Tiempo de espera entre ráfagas")] private float Cd;
    [SerializeField, Tooltip("Tiempo de espera entre balas en cada ráfaga")] private float CdIndv;
    [SerializeField, Tooltip("El GO de la bala")] private GameObject BalaGO;

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

            for (int i = 0; i < Balas; i++)
            {
                yield return new WaitForSeconds(CdIndv);

                GameObject aBala = Instantiate(BalaGO, this.transform.position, Quaternion.identity);


                Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), aBala.GetComponent<Collider2D>());
            }

            yield return new WaitForSeconds(Cd);
        }


    }
}