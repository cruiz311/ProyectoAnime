using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fondomov : MonoBehaviour
{
    [SerializeField] private Vector2 velocidadMovimiento;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D jugadorRB;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        jugadorRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // El offset se ajusta según la velocidad del jugador
        offset = jugadorRB.velocity.x * velocidadMovimiento * 0.1f; // Elimina Time.deltaTime aquí para evitar un cálculo incorrecto
        material.mainTextureOffset += offset * Time.deltaTime; // Usa Time.deltaTime al aplicar el offset, no al calcularlo
    }
}
