using System.Collections;
using UnityEngine;

public class LootSplash : MonoBehaviour
{
    [SerializeField] private string lootObjectName = "LootObject";  // Nombre del objeto que deseas encontrar
    [SerializeField] private int lootCount = 5;      // Número de objetos de loot a generar
    [SerializeField] private float splashRadius = 2f; // Radio de la explosión
    [SerializeField] private float force = 5f;       // Fuerza de la explosión
    [SerializeField] private float destroyDelay = 5f; // Tiempo antes de destruir el loot
    [SerializeField] private float fadeDuration = 1f; // Duración del parpadeo antes de desaparecer

    // Llama a este método cuando el enemigo sea destruido
    public void SpawnLoot()
    {
        StartCoroutine(SpawnLootCoroutine());
    }

    private IEnumerator SpawnLootCoroutine()
    {
        for (int i = 0; i < lootCount; i++)
        {
            // Busca el objeto de loot en la escena
            GameObject loot = GameObject.Find(lootObjectName);

            if (loot != null)
            {
                // Clona el objeto para simular la instanciación
                GameObject clonedLoot = Instantiate(loot, transform.position, Quaternion.identity);
                Rigidbody2D rb = clonedLoot.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    // Calcula una dirección aleatoria para la fuerza de la explosión
                    Vector2 direction = Random.insideUnitCircle.normalized * splashRadius;
                    rb.AddForce(direction * force, ForceMode2D.Impulse);
                }

                // Inicia la rutina de parpadeo antes de destruir el objeto
                StartCoroutine(FadeOutAndDestroy(clonedLoot, destroyDelay, fadeDuration));
            }
            else
            {
                Debug.LogWarning("Loot object not found in the scene!");
            }

            yield return null; // Espera un frame antes de crear el siguiente objeto
        }
    }

    private IEnumerator FadeOutAndDestroy(GameObject loot, float delay, float fadeTime)
    {
        // Espera el tiempo definido antes de comenzar a parpadear
        yield return new WaitForSeconds(delay - fadeTime);

        SpriteRenderer spriteRenderer = loot.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            float timer = 0f;
            bool isVisible = true;

            // Comienza el parpadeo
            while (timer < fadeTime)
            {
                // Alterna la visibilidad del objeto
                isVisible = !isVisible;
                spriteRenderer.enabled = isVisible;

                // Tiempo entre cada parpadeo
                yield return new WaitForSeconds(0.1f);
                timer += 0.1f;
            }

            // Asegúrate de que el objeto esté visible al final
            spriteRenderer.enabled = true;
        }

        // Finalmente destruye el objeto
        Destroy(loot);
    }
}
