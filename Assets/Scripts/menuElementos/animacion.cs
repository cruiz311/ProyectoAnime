using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animacion : MonoBehaviour
{
    public Image image;
    public List<Sprite> sprites;
    public float animSpeedFirst3 = 0.5f;  // Velocidad de animación para las primeras 3 imágenes
    public float animSpeedRest = 1f;      // Velocidad de animación para el resto de las imágenes
    public float shakeAmount = 10f;       // Intensidad de la vibración
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        while (index < sprites.Count)
        {
            // Si estamos en las primeras 3 imágenes, añadimos el efecto de vibración y usamos una velocidad específica
            if (index < 3)
            {
                StartCoroutine(Shake());
                yield return new WaitForSeconds(animSpeedFirst3); // Usamos la velocidad para las primeras 3 imágenes
            }
            else
            {
                yield return new WaitForSeconds(animSpeedRest); // Usamos la velocidad para el resto de las imágenes
            }

            image.sprite = sprites[index];
            index++;

            // Si llegamos a la última imagen, terminamos la animación
            if (index == sprites.Count)
            {
                // Congelar la última imagen
                break;
            }
        }
    }

    // Efecto de vibración para las primeras 3 imágenes
    IEnumerator Shake()
    {
        Vector3 originalPos = image.transform.localPosition;

        // Vibrar durante un pequeño período
        float shakeDuration = animSpeedFirst3; // La duración de la vibración coincide con la velocidad de las primeras imágenes
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-shakeAmount, shakeAmount);
            float offsetY = Random.Range(-shakeAmount, shakeAmount);
            image.transform.localPosition = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Restaurar la posición original
        image.transform.localPosition = originalPos;
    }
}
