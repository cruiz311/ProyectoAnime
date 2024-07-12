using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dañoBalaBur : MonoBehaviour
{

    public int daño;

    private void Update()
    {
        transform.Translate(Time.deltaTime * Vector2.right);
        Destroybalaburbuja();
    }

/*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CombateJugador combatejugador))
        {
            
            Destroy(gameObject);
        }
    }
*/
    void Destroybalaburbuja()
    {
        Destroy(gameObject, 1f);
    }
}
