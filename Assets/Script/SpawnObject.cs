using System.Collections;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject objectPrefab;
    public Transform[] spawnPositions;

    public float spawnRate = 3f;
    float timePassSpawn = 7;
    public bool stopSpawn;

    private void Start()
    {
        StartCoroutine(WaitAndSpawn());
        InvokeRepeating("speedTimeSpawn",timePassSpawn,timePassSpawn);
    }

    void RandomSpawn()
    {
        // random a place to spawn a zombie within spawnPositions
        int randomIndex = Random.Range(0, spawnPositions.Length);
        GameManager.Instance.SpawnEnemyInScene(objectPrefab, spawnPositions[randomIndex].position);
    }

    void speedTimeSpawn(){
        if(spawnRate>1){
            spawnRate*=0.7f;
        }else{
            spawnRate = 0.5f;
        }

    }
    IEnumerator WaitAndSpawn()
    {

        do
        {
            RandomSpawn();
            yield return new WaitForSeconds(spawnRate);

        } while (!stopSpawn);
    }
}
