using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparoE : MonoBehaviour
{
    public Transform controladorDisparo;
    public float distanciaLinea;
    public LayerMask capaJugador;
    public bool JugadorEnRango;
    public GameObject BalaEnemigo;
    public float tiempoEntreD;
    public float tiempoUltimoD;
    public float tiempoEspera;

    private void Update()
    {
        JugadorEnRango = Physics2D.Raycast(controladorDisparo.position, transform.right, distanciaLinea, capaJugador);

        if (JugadorEnRango)
        {
            if (Time.time > tiempoEntreD + tiempoUltimoD)
            {
                tiempoUltimoD = Time.time;
                Invoke(nameof(Disparar), tiempoEspera);
            }
        }
    }
    private void Disparar()
    {
        Instantiate(BalaEnemigo, controladorDisparo.position, controladorDisparo.rotation);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorDisparo.position, controladorDisparo.position + transform.right * distanciaLinea);
        if (controladorDisparo != null)
        {
          
        }
        else
        {
            Debug.LogWarning("controladorDisparo no está asignado.");
        }
    }
}
