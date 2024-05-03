using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoP : MonoBehaviour
{
    [SerializeField] private Transform Cdisparo;
    [SerializeField] private GameObject bala;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Disparar();
        }
    }
    private void Disparar()
    {
        Instantiate(bala, Cdisparo.position, Cdisparo.rotation);
    }
}
