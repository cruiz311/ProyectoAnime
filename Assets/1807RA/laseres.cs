/**using UnityEngine;

public class laseres : MonoBehaviour
{
    public GameObject laserPrefab;
    private GameObject currentLaser;

    void Start()
    {
        GenerateLaser();
    }

    void GenerateLaser()
    {
        currentLaser = Instantiate(laserPrefab, transform.position, transform.rotation);
        currentLaser.GetComponent<laser>().SetBase(this);
    }

    public void DestroyLaser()
    {
        if (currentLaser != null)
        {
            Destroy(currentLaser);
        }
    }
}
**/