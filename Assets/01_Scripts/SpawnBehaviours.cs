using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehaviours : MonoBehaviour
{

    [SerializeField] List<int> foodAmount;
    // Start is called before the first frame update
    [SerializeField] List<GameObject> spawnee;
    [SerializeField] List<Transform> spawnPosition;
    [SerializeField] bool stopSpawning = false;
    [SerializeField] float spawnTime;
    [SerializeField] float spawnDelay;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

/*    IEnumerator spawningManager(float delay)
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            Debug.Log("current speed is " + delay);
            yield return new WaitForSeconds(delay);
        }
    }*/
// avec invoke repeating on peut pas augmenter le delay entre deux spawn, je changerais en une coroutine plus tard
    public void SpawnObject()
    {
        var amount = foodAmount[Random.Range(0, foodAmount.Count)];
        //Debug.Log(amount);
        for (int i = 0; i < amount; i++)
        {
            Instantiate(spawnee[Random.Range(0, spawnee.Count)], spawnPosition[Random.Range(0, spawnPosition.Count)].position, transform.rotation);
        }

        if (stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }
}
