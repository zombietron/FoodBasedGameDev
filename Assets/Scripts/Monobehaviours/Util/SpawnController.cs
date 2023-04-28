using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnController : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;

    [SerializeField] int spawnGap;

    [SerializeField] List<Transform> spawnPoints;

    GameObject spawnedEnemies;
    int spawnLocationIndex = 0;

    //temporary until GameManager exists for testing
    public bool gameActive=true;

    /*
     * To do
     * Hook Function up to GameManager to listen for game state
     * Register spawned enemies with game manager for wave progression
     * profit?
    */
    // Start is called before the first frame update
    private void Awake()
    {
        spawnedEnemies = new GameObject();
        spawnedEnemies.name = "Spawned Enemies";
    }
    void Start()
    {

        StartCoroutine(SpawnMonstersWithGap(3));
        //Debug.Log("SpawnPoints" + spawnPoints.Count);
    }


    IEnumerator SpawnMonstersWithGap(int gap)
    {
    
        
        while(gameActive)
        {
            yield return new WaitForSeconds(spawnGap);
            var spawnedEnemy = Instantiate<GameObject>(enemyPrefabs[Random.Range(0,enemyPrefabs.Count)]);
            //This is a solution for the spawned enemy not hitting the correct point due to a navmesh conflict
            // we need to clean this up and make it an event based system.
            spawnedEnemy.GetComponent<NavMeshAgent>().enabled = false;
            spawnedEnemy.transform.position = spawnPoints[spawnLocationIndex].position;
            //same here
            spawnedEnemy.GetComponent<NavMeshAgent>().enabled = true;

            spawnLocationIndex = spawnLocationIndex >= spawnPoints.Count - 1 ? 0: spawnLocationIndex + 1;
            //spawnedEnemy.transform.parent = spawnedEnemies.transform;

        }
        yield break;

    }

    public void StartMonsterWithGapCoRoutine(int gap)
    {
        StartCoroutine(SpawnMonstersWithGap(gap));
    }
}
