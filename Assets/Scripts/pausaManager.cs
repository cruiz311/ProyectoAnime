using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausaManager : MonoBehaviour
{
    public GameObject cuadroPausa;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pausaBoton()
    {
        cuadroPausa.SetActive(true);
    }


    public void Continuar()
    {
        cuadroPausa.SetActive(false);
    }
}
