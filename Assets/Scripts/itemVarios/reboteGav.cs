using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mrebote : MonoBehaviour
{
    public Animator anim;
    private bool animExecuted = false; // Variable para controlar si la animaci�n ya se ejecut�

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Plamov player = other.gameObject.GetComponent<Plamov>();
            if (player != null)
            {
                foreach (ContactPoint2D punto in other.contacts)
                {
                    if (punto.normal.y <= -0.9f) // Usar 'f' para indicar un float literal
                    {
                        if (anim != null && !animExecuted)
                        {
                            anim.SetBool("gaveta", true);
                            animExecuted = true; // Marcar que la animaci�n se ejecut�
                        }
                        player.Rebote();
                    }
                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Restablecer el estado para permitir que la animaci�n se ejecute de nuevo en el pr�ximo salto
            animExecuted = false;
            anim.SetBool("gaveta", false); // Reiniciar la animaci�n
        }
    }
}
