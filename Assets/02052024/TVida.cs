using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVida : MonoBehaviour
{
    [SerializeField] private float duracion;

    private void Start()
    {
        Destroy(gameObject, duracion);
    }
}
