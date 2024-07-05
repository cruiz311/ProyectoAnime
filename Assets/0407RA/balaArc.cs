
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaBhvARC : MonoBehaviour
{

    private Vector3 targetPos;
    private bool _done;
    [Tooltip("VelocidadHorizontal")]
    public float Vel = 10;

    [Tooltip("Altura del arco")]
    public float Arc = 1;

    [Tooltip("Las capas en la que se puede fijar el objetivo (seleccionar la capa del suelo)")]
    public LayerMask layer;

    Vector3 startPos, nextPosS, pt0;

    void Start()
    {
        startPos = transform.position;

        RaycastHit2D ray = Physics2D.Raycast(PlayerDetect.Instance._player.transform.position, Vector2.down, 15f, layer);

        targetPos = ray.point;
    }

    void Update()
    {

        if (_done)
        {

        }
        else
        {
            float x0 = startPos.x;
            float x1 = targetPos.x;
            float dist = x1 - x0;
            float nextX = Mathf.MoveTowards(transform.position.x, x1, Vel * Time.deltaTime);
            float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
            float arc = Arc * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
            Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

            transform.rotation = LookAt2D(nextPos - transform.position);
            transform.position = nextPos;

            if (nextPos == targetPos)
            {
                _done = true;
            }
        }

    }

    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == PlayerDetect.Instance._player)
        {
            //hacer daño al jugador /* -------------------------------------------------------------------------------------------------------- */
        }

        Destroy(this.gameObject);
    }
}
