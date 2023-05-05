using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnController : MonoBehaviour
{
    [SerializeField] int spawnGap = 3;

    public int SpawnGap
    {
        set { spawnGap = value; }
        get { return spawnGap; }
    }

    [SerializeField] List<GameObject> enemyPrefabs;

    

    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] WaveManager waveMgr;
    [SerializeField] List<Transform> enemiesInScene;

    bool waveComplete = false;
    GameObject spawnedEnemies;
    int spawnLocationIndex = 0;



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
        enemiesInScene = new List<Transform>();
    }



    IEnumerator SpawnMonstersWithGap(int gap)
    {
        Debug.Log("SpawnMonstersWithGap Coroutine Started at: " + Time.time);
        
        while(GameManager.Instance.gameState == GameManager.GameState.gameRunning)
        {
            
            yield return new WaitForSeconds(gap);

            if (enemiesInScene.Count < waveMgr.wave.maxEnemiesSpawnedDuringWave && !waveMgr.wave.waveComplete)
            {
                var spawnedEnemy = Instantiate<GameObject>(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)],spawnedEnemies.transform);
                enemiesInScene.Add(spawnedEnemy.transform);
                waveMgr.wave.AddEnemyToWaveCount();
                //This is a solution for the spawned enemy not hitting the correct point due to a navmesh conflict
                // we need to clean this up and make it an event based system.
                spawnedEnemy.GetComponent<NavMeshAgent>().enabled = false;
                spawnedEnemy.transform.position = spawnPoints[spawnLocationIndex].position;
                //same here
                spawnedEnemy.GetComponent<NavMeshAgent>().enabled = true;

                spawnLocationIndex = spawnLocationIndex >= spawnPoints.Count - 1 ? 0 : spawnLocationIndex + 1;
                //spawnedEnemy.transform.parent = spawnedEnemies.transform;
                yield return new WaitForSeconds(gap);
            }
            else
                yield return new WaitUntil(() => enemiesInScene.Count < waveMgr.wave.maxEnemiesSpawnedDuringWave);

            
        }

        yield break;

    }

    public void StartMonsterWithGapCoRoutine(int gap)
    {
        StartCoroutine(SpawnMonstersWithGap(gap));
    }

    public void StopSpawning()
    {
        StopCoroutine(SpawnMonstersWithGap(spawnGap));
    }
}
