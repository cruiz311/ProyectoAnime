using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animacion : MonoBehaviour
{
    public Image image;
    public List<Sprite> sprites;
    public float animSpeedFirst3 = 0.5f;  // Velocidad de animaci�n para las primeras 3 im�genes
    public float animSpeedRest = 1f;      // Velocidad de animaci�n para el resto de las im�genes
    public float shakeAmount = 10f;       // Intensidad de la vibraci�n
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
            // Si estamos en las primeras 3 im�genes, a�adimos el efecto de vibraci�n y usamos una velocidad espec�fica
            if (index < 3)
            {
                StartCoroutine(Shake());
                yield return new WaitForSeconds(animSpeedFirst3); // Usamos la velocidad para las primeras 3 im�genes
            }
            else
            {
                yield return new WaitForSeconds(animSpeedRest); // Usamos la velocidad para el resto de las im�genes
            }

            image.sprite = sprites[index];
            index++;

            // Si llegamos a la �ltima imagen, terminamos la animaci�n
            if (index == sprites.Count)
            {
                // Congelar la �ltima imagen
                break;
            }
        }
    }

    // Efecto de vibraci�n para las primeras 3 im�genes
    IEnumerator Shake()
    {
        Vector3 originalPos = image.transform.localPosition;

        // Vibrar durante un peque�o per�odo
        float shakeDuration = animSpeedFirst3; // La duraci�n de la vibraci�n coincide con la velocidad de las primeras im�genes
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-shakeAmount, shakeAmount);
            float offsetY = Random.Range(-shakeAmount, shakeAmount);
            image.transform.localPosition = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Restaurar la posici�n original
        image.transform.localPosition = originalPos;
    }
}
