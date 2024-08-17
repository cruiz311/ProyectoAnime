using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisparoP : MonoBehaviour
{
    [SerializeField] private Transform Cdisparo, disLeft, disArriba;//,disRight;
    [SerializeField] private GameObject bala;
    float posDisY,posDisX;
    [SerializeField] private int municionMax;
    [SerializeField] private TextMeshProUGUI text;
    int municion;
    [SerializeField] private bool infinito;
    private void Start()
    {
        municion = municionMax;
    }
    private void Update()
    {
        if (municion > 0)
        {
            Disparar();
        }
        if (infinito)
        {
            municion = 999;
        }
        if (municion >= municionMax)
            municion = municionMax;
        text.text = municion.ToString();
        
    }
    private void Disparar()
    {

        posDisY = Input.GetAxisRaw("Vertical");
        posDisX = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Fire1") && posDisY <= 0)
        {
            Instantiate(bala, Cdisparo.position, Cdisparo.rotation);
            municion -= 1;
        }

        if (Input.GetButtonDown("Fire1") && posDisY > 0 && posDisX == 0)
        {
            Instantiate(bala, disArriba.position, disArriba.rotation);
            municion -= 1;
        }
        if (Input.GetButtonDown("Fire1") && posDisY > 0 && posDisX < 0 | posDisX > 0)
        {

            Instantiate(bala, disLeft.position, disLeft.rotation);
            municion -= 1;
        }/*
        if (Input.GetButtonDown("Fire1") && posDisY > 0 && posDisX >0)
        {

            Instantiate(bala, disRight.position, disRight.rotation);
        }*/
        // Instantiate(bala, Cdisparo.position, Cdisparo.rotation);
    }
}
