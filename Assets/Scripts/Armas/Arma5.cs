using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma5 : MonoBehaviour
{
    public GameObject trailPrefab;
    public float spawnInterval = 2f;

    private float timer = 0f;
    private Vector3 lastPosition;

    void Update()
    {
        timer += Time.deltaTime;

        if (Vector3.Distance(transform.position, lastPosition) > 0.1f && timer >= spawnInterval)
        {
            SpawnTrail();
            timer = 0f;
        }

        lastPosition = transform.position;
    }

    void SpawnTrail()
    {
        Instantiate(trailPrefab, transform.position, Quaternion.identity);
    }
}
