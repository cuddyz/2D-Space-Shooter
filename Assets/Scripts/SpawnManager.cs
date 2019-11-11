using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyContainer;
    public GameObject[] powerups;
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerups());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerDeath() {
        _stopSpawning = true;
    }

    IEnumerator SpawnEnemies() {
        while(!_stopSpawning) {
            float ran = Random.Range(-9f, 9f);
            GameObject newEnemy = Instantiate(enemyPrefab, new Vector3 (ran, 7f, 0), Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;

            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnPowerups() {
        while(!_stopSpawning) {
            float ranX = Random.Range(-9f, 9f);
            float ranTime = Random.Range(3f, 8f);
            int ranPowerUp = Random.Range(0, 2);
            Instantiate(powerups[ranPowerUp], new Vector3 (ranX, 7f, 0), Quaternion.identity);

            yield return new WaitForSeconds(ranTime);
        }
    }
}
