using UnityEngine;

public class BalaBhvLIN : MonoBehaviour
{

    private Vector3 targetPos;
    [Tooltip("Factor de randomizacion de las balas")]
    public float Randomize = 0;
    [Tooltip("Velocidad Horizontal")]
    public float Vel = 10;
    [Tooltip("Tiempo Para el despawn")]
    public float Decal = 10;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2((PlayerDetect.Instance._player.transform.position.x > transform.position.x) ? Vel : -Vel, Random.Range(-.25f, Randomize));

        Invoke(nameof(Delete), Decal);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == PlayerDetect.Instance._player)
        {
            //hacer daÃ±o al jugador /* -------------------------------------------------------------------------------------------------------- */
        }

        Destroy(this.gameObject);
    }


    private void Delete()
    {
        Destroy(this.gameObject);
    }
}