using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject newBulletPrefab;  // Prefab de la nueva bala especial

    private void OnTriggerEnter2D(Collider2D other)
    {
        DisparoP playerShooting = other.GetComponent<DisparoP>();

        if (playerShooting != null)
        {
            // Cambia el prefab de la bala del jugador a la bala especial
            playerShooting.CambiarABalaEspecial(newBulletPrefab);

            // Destruye el ítem después de recogerlo
            Destroy(gameObject);
        }
    }
}
