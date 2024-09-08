using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private GameObject efectoMuerte;
    [SerializeField] private LootSplash lootSplash;

    private void Update()
    {
        if (vida <= 0)
        {
            Muerte();
        }
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        // Instanciar el efecto de muerte en la posición del enemigo
        if (efectoMuerte != null)
        {
            Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        }

        // Llamar al método para soltar loot
        if (lootSplash != null)
        {
            lootSplash.SpawnLoot();
        }

        // Destruir el enemigo
        Destroy(gameObject);
    }
}
