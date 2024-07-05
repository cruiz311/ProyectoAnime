using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    public static PlayerDetect Instance { get; private set; }
    public GameObject _player;
    void Awake()
    {
        Instance = this;

        if (_player == null)
        {
            GameObject[] objects = FindObjectsOfType<GameObject>();

            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].CompareTag("Player"))
                {
                    _player = objects[i];
                    return;
                }
            }
        }
    }
}