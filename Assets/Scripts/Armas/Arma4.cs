using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Arma4 : MonoBehaviour
{
    public GameObject boomerangPrefab;
    public float shootInterval = 2f;
    public float boomerangSpeed = 10f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            timer = 0f;

            GameObject boomerang = Instantiate(boomerangPrefab, transform.position, Quaternion.identity);
            StartCoroutine(BoomerangMovement(boomerang.transform));
        }
    }

    IEnumerator BoomerangMovement(Transform boomerang)
    {
        Transform player = transform;
        Transform target = FindFarthestEnemy();
        bool returning = false;

        while (true)
        {
            if (target == null && !returning)
            {
                target = player;
                returning = true;
            }

            if (target != null)
            {
                Vector3 direction = (target.position - boomerang.position).normalized;
                boomerang.position += direction * boomerangSpeed * Time.deltaTime;

                float distance = Vector3.Distance(boomerang.position, target.position);
                if (distance < 0.5f)
                {
                    if (!returning)
                    {
                        target = player;
                        returning = true;
                    }
                    else
                    {
                        Destroy(boomerang.gameObject);
                        yield break;
                    }
                }
            }

            yield return null;
        }
    }

    Transform FindFarthestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return null;

        return enemies
            .OrderByDescending(e => Vector3.Distance(e.transform.position, transform.position))
            .First().transform;
    }
}
