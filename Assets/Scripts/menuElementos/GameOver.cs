using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    private CombateJugador combateJugador;

    private void Start()
    {
        combateJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<CombateJugador>();
        combateJugador.MuerteJugador += ActivarMenu;
    }

    private void ActivarMenu(object sender, EventArgs e)
    {
        gameOver.SetActive(true);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuInicial(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
