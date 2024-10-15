using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private GameObject efectoMuerte;
    [SerializeField] private LootSplash lootSplash;

    // Nuevo campo para establecer la posici�n de generaci�n
    [SerializeField] private Transform posicionGeneracion; // Asignar en el Inspector
    [SerializeField] private bool invertirEfectoMuerte; // Opci�n para invertir el GameObject

    private void Update()
    {
        if (vida <= 0)
        {
            Muerte();
        }
    }

    public void TomarDa�o(float da�o)
    {
        vida -= da�o;
        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        // Instanciar el efecto de muerte en la posici�n del enemigo o en la posici�n personalizada
        if (efectoMuerte != null)
        {
            GameObject efecto = Instantiate(efectoMuerte, posicionGeneracion.position, Quaternion.identity);

            // Rotar el efecto de muerte si es necesario
            if (invertirEfectoMuerte)
            {
                // Gira el efecto 180 grados en el eje Z
                efecto.transform.Rotate(0f, 0f, 180f);
            }
        }

        // Llamar al m�todo para soltar loot
        if (lootSplash != null)
        {
            lootSplash.SpawnLoot();
        }

        // Destruir el enemigo
        Destroy(gameObject);
    }
}