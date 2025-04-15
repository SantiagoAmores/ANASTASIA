using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma5 : MonoBehaviour
{
    public GameObject proyectilPrefab;
    public float spawnInterval = 2f;

    private float timer = 0f;
    private Vector3 lastPosition;

    void Update()
    {
        timer += Time.deltaTime;

        if (Vector3.Distance(transform.position, lastPosition) > 0.01f && timer >= spawnInterval)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, 0.5f, transform.position.z);
            Instantiate(proyectilPrefab, spawnPos, Quaternion.identity);
            timer = 0f;
        }

        lastPosition = transform.position;
    }
}
