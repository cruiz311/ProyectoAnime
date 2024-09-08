using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class vidasUI : MonoBehaviour
{
    private CombateJugador combateJugador; // Referencia al script de CombateJugador
    private TextMeshProUGUI textMesh;

    private void Start()
    {
        combateJugador = FindObjectOfType<CombateJugador>(); // Encuentra al jugador
        textMesh = GetComponent<TextMeshProUGUI>();
        ActualizarVidas(combateJugador.ObtenerVidasRestantes());
    }

    public void ActualizarVidas(int vidas)
    {
        textMesh.text = "X " + vidas.ToString(); // Muestra "X" seguido del número de vidas
    }
}
