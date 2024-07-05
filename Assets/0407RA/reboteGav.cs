using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mrebote : MonoBehaviour
{




    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D punto in other.contacts)
            {
                if (punto.normal.y <= -0.9)
                {
                    other.gameObject.GetComponent<Plamov>().Rebote();
                }
            }
        }
    }


}
