using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    public Transform[] spawns;

    public GameObject enemigoPrefab;

    public bool seguir = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawns());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator Spawns()
    {
        while (seguir)
        {
            yield return new WaitForSeconds(1f);

            int index = Random.Range(0, spawns.Length);
            Vector3 spawnPosition = spawns[index].position;

            Instantiate(enemigoPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
