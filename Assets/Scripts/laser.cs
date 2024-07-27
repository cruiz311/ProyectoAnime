using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    [SerializeField] private float tiempoEntreDaño;
    private float tiempoDaño;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tiempoDaño -= Time.deltaTime;
            if (tiempoDaño <= 0)
            {
                other.GetComponent<CombateJugador>().TomarDaño(8);
                tiempoDaño = tiempoEntreDaño;
            }
        }
    }
}
