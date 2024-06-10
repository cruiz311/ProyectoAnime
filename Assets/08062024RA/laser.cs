using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    [SerializeField] private float tiempoEntreDa�o;
    private float tiempoDa�o;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tiempoDa�o -= Time.deltaTime;
            if (tiempoDa�o <= 0)
            {
                other.GetComponent<CombateJugador>().TomarDa�o(8);
                tiempoDa�o = tiempoEntreDa�o;
            }
        }
    }
}
