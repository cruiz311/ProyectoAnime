using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    
    public string nombre;

    void Update()
    {
      
        if (Input.anyKeyDown)
        {
           
            SceneManager.LoadScene(nombre);
        }
    }
}
