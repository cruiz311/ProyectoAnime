using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    public float velocidad;
    public int da�o;

    private void Update()
    {
        transform.Translate(Time.deltaTime * velocidad * Vector2.right);
        DestroyBalaEnemigo();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CombateJugador combatejugador))
        {
            combatejugador.TomarDa�o(da�o);
            Destroy(gameObject);
        }
    }
    void DestroyBalaEnemigo()
    {
        Destroy(gameObject, 1f);
    }
}
