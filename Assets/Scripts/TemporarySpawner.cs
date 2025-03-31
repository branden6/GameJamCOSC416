using System.Collections;
using UnityEngine;

public class TemporarySpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime = 3f;
    [SerializeField] private float lifetime = 10f;
    [SerializeField] private GameObject barrelObj;

    private Coroutine spawnCoroutine;

    void Start()
    {
        Quaternion spawnRotation = Quaternion.Euler(90, 0, 0);
        Instantiate(barrelObj, transform.position, spawnRotation);

        spawnCoroutine = StartCoroutine(SpawnBarrel());

        Destroy(gameObject, lifetime);
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

    private void OnDestroy()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }
}
