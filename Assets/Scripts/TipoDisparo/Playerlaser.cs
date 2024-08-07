using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserScript : MonoBehaviour
{


    [Tooltip("Hacia que dirección va el lase. Se puede poner directamente la coordenada de destino")] public Vector2 Direction;

    [Tooltip("Contra que colisina el Laser")] public LayerMask layer;

    [Tooltip("Cuantas partes tiene el Laser ---- ATENCION: Un valor mayor a 5 causa mucho lag")] public float resolution = 1;
    [Tooltip("Velocidad a la que se mueve la animación del Laser ---- ATENCION: Cuanto mas largo sea el laser más rapido se mueve")] public float AnimVel;
    [Tooltip("Como de alto/bajo llega el laser en comparación al punto inicial")] public float AnimAmp;
    [Tooltip("Como de ondulado es el laser")] public float Frequency;

    [Tooltip("Distancia máxima que puede alcanzar el laser ---- ATENCION: valores altos generan lag (combinado con la resolucion)")] public float MaxDistance;

    private LineRenderer lR;
    private EdgeCollider2D eC;
    void Start()
    {
        lR = GetComponent<LineRenderer>();
        eC = GetComponent<EdgeCollider2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) StartCounter();
        else
        {
            lR.enabled = false;
            eC.enabled = false;
        }

        if (lR.enabled == true)
        {
            for (int i = 1; i < lR.positionCount - 1; i++)
            {
                lR.useWorldSpace = true;

                lR.SetPosition(i, new Vector2(lR.GetPosition(i).x, lR.GetPosition(i).y + Mathf.Sin(Time.time * AnimVel * 2 / lR.positionCount + (float)i / Frequency) * AnimAmp));
            }
        }

    }

    void StartCounter()
    {

        lR.enabled = true;
        eC.enabled = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction.normalized, MaxDistance, layer);

        Vector2 TargetPoint;

        if (hit.collider != null)
        {
            TargetPoint = hit.point;

        }
        else
        {
            TargetPoint = (Vector2)transform.position + Direction * MaxDistance;
        }


        lR.useWorldSpace = true;
        lR.SetPosition(0, transform.position);

        lR.SetPosition(lR.positionCount - 1, TargetPoint);


        lR.positionCount = Mathf.RoundToInt((TargetPoint.x - transform.position.x) * resolution) + 2;

        Debug.Log(lR.positionCount);

        for (int i = 0; i < lR.positionCount - 1; i++)
        {
            Debug.Log(i / lR.positionCount);
            lR.SetPosition(i, new Vector2(Mathf.Lerp(transform.position.x, TargetPoint.x, (float)i / (float)lR.positionCount), transform.position.y));
        }


        List<Vector2> a = new()
        {
            Vector2.zero,
            TargetPoint - (Vector2)transform.position
        };



        eC.SetPoints(a);
    }
}