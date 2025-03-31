using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnTime = 3f;
    [SerializeField] private GameObject barrelObj;

    void Start()
    {
        Quaternion spawnRotation = Quaternion.Euler(90, 0, 0);
        Instantiate(barrelObj, transform.position, spawnRotation);
        
        StartCoroutine(SpawnBarrel());
    }

    private IEnumerator SpawnBarrel()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            Quaternion spawnRotation = Quaternion.Euler(90, 0, 0);
            Instantiate(barrelObj, transform.position, spawnRotation);
        }
    }
}
