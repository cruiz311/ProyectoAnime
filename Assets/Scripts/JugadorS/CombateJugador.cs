using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateJugador : MonoBehaviour
{
    [SerializeField] int vida;
    [SerializeField] int maxVida;
    

    private void Start()
    {
        vida = maxVida;
    }

    public void TomarDa�o(int da�o)
    {
        vida -= da�o;
        if (vida <= 0)
        {
            Muerte();
        }

    }
    public void Muerte()
    {  
        Destroy(gameObject);
    }

}
