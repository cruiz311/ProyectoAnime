using UnityEngine;
using UnityEngine.SceneManagement; 

public class CambioDeEscena : MonoBehaviour
{
    private string nombreEscena = "Bobosaurio";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscena);
        }
    }
}