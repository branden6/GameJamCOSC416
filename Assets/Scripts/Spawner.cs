using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnTime = 3f;
    [SerializeField] private GameObject barrelObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnBarrel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnBarrel()
    {
    while (true)
    {
        yield return new WaitForSeconds(spawnTime);
        
        Quaternion spawnRotation = Quaternion.Euler(90, 0, 0); // Set X rotation to 90 degrees
        Instantiate(barrelObj, transform.position, spawnRotation);
    }
    }

}
