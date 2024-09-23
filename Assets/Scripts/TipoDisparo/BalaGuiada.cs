using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaGuiada : MonoBehaviour
{
    public Transform transformJugador;
    public float velocidadMovimiento;
    public float anguloInicial;
    public float velocidadRotacion;
    public int daño;
    

    private void Start()
    {
        GameObject jugadorGameObject = GameObject.FindGameObjectWithTag("Player");

        if (jugadorGameObject == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transformJugador = jugadorGameObject.transform;
        }
    }

    private void Update()
    {
        transform.Translate(velocidadMovimiento * Time.deltaTime * Vector2.right, Space.Self);

        if (transformJugador == null)
        {
            return;
        }

        float anguloRadianes = Mathf.Atan2(transformJugador.position.y - transform.position.y, transformJugador.position.x - transform.position.x);
        float anguloGrados = anguloRadianes * Mathf.Rad2Deg - anguloInicial;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, anguloGrados), Time.deltaTime * velocidadRotacion);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CombateJugador combateJugador))
        {
            combateJugador.TomarDaño(daño);
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("piso"))
        {
            
            Destroy(gameObject);
        }
    }
}
