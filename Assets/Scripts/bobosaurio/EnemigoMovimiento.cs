using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoMovimiento : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 2f; // Puedes ajustar esta velocidad como prefieras

    private void Update()
    {
        // Mueve el enemigo hacia la derecha continuamente
        transform.position += Vector3.right * velocidadMovimiento * Time.deltaTime;
    }
}
