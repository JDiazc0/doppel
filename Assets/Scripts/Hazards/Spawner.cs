using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject hazard;
    public Transform[] spawnPoints;
    public float spawnInterval = 3.5f;
    public float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnHazards();
            timer = 0;
        }
    }

    void SpawnHazards()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(hazard, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
