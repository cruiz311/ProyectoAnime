using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    [Tooltip("Iniciar automaticamente? (Si no, se necesita que otro script inicie la Coroutine)")] public bool Init;

    [Tooltip("Cuanto tiempo se mantiene encendido")] public float OnTime;
    [Tooltip("Cuanto tiempo se mantiene apagado")] public float OffTime;
    [Tooltip("Hacia que dirección va el lase. Se puede poner directamente la coordenada de destino")] public Vector2 Direction;

    [Tooltip("Contra que colisina el Laser")] public LayerMask layer;

    private LineRenderer lR;
    void Start()
    {
        lR = GetComponent<LineRenderer>();

        if (Init) StartCoroutine(nameof(StartCounter));
    }

    IEnumerator StartCounter()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction.normalized, 100f, layer);
        EdgeCollider2D eC = GetComponent<EdgeCollider2D>();

        if (hit.collider != null)
        {
            lR.useWorldSpace = true;
            lR.SetPosition(0, transform.position);

            lR.SetPosition(1, hit.point);

            List<Vector2> a = new()
            {
                Vector2.zero,
                hit.point - (Vector2)transform.position
            };


            eC.SetPoints(a);

        }
        else yield break;

        while (true)
        {
            lR.enabled = false;
            eC.enabled = false;
            yield return new WaitForSeconds(OffTime);
            lR.enabled = true;
            eC.enabled = true;
            yield return new WaitForSeconds(OnTime);
        }
    }
}