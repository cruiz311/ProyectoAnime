using UnityEngine;

public class BalaBhvFALL : MonoBehaviour
{
    [Tooltip("Factor de randomización de las balas")]
    public float Randomize = 0;
    [Tooltip("Velocidad Horizontal")]
    public float Vel = 10;
    [Tooltip("Tiempo mínimo para que la bala caiga")]
    public float minFallTime = 5;
    [Tooltip("Tiempo máximo para que la bala caiga")]
    public float maxFallTime = 10;
    [Tooltip("Aceleración vertical cuando la bala cae")]
    public float fallAcceleration = -9.8f;
    [Tooltip("Duración del suavizado de la velocidad horizontal")]
    public float smoothingDuration = 1.0f;
    [Tooltip("Daño que inflige la bala al jugador")]
    public int damage = 8;

    private Rigidbody2D rb;
    private bool isFalling = false;
    private bool isSmoothing = false;
    private float smoothingStartTime;
    private float initialHorizontalSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float direction = (PlayerDetect.Instance._player.transform.position.x > transform.position.x) ? Vel : -Vel;
        rb.velocity = new Vector2(direction, Random.Range(-0.25f, Randomize));

        float fallTime = Random.Range(minFallTime, maxFallTime); // Genera un tiempo de caída aleatorio.
        Invoke(nameof(StartFalling), fallTime);
        Invoke(nameof(Delete), fallTime + 10); // Ajusta el tiempo para eliminar el objeto después de la caída, si es necesario.
    }

    private void StartFalling()
    {
        isFalling = true;
        isSmoothing = true;
        smoothingStartTime = Time.time;
        initialHorizontalSpeed = rb.velocity.x;
    }

    void Update()
    {
        if (isSmoothing)
        {
            float t = (Time.time - smoothingStartTime) / smoothingDuration;
            rb.velocity = new Vector2(Mathf.Lerp(initialHorizontalSpeed, 0, t), rb.velocity.y);

            if (t >= 1.0f)
            {
                isSmoothing = false;
                rb.velocity = new Vector2(0, rb.velocity.y); // Asegurarse de que la velocidad horizontal sea exactamente cero.
            }
        }

        if (isFalling)
        {
            rb.velocity += new Vector2(0, fallAcceleration * Time.deltaTime); // Aumenta gradualmente la velocidad vertical.
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CombateJugador>().TomarDaño(damage);
        }

        Destroy(gameObject);
    }

    private void Delete()
    {
        Destroy(gameObject);
    }
}
