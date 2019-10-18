using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies() {
        while(true) {
            yield return new WaitForSeconds(5);
            Instantiate(enemyPrefab, new Vector3(0,0,0), Quaternion.identity);
        }
    }
}
