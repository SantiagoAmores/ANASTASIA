using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Arma4 : MonoBehaviour
{
    public GameObject boomerangPrefab;
    //public float shootInterval = 2f;
    public float boomerangSpeed = 10f;

    public StatsAnastasia stats;


    private void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<StatsAnastasia>();
        StartCoroutine(RutinaBumerang());
    }

    IEnumerator RutinaBumerang()
    {
        while (true)
        {
            yield return new WaitForSeconds(stats.arma4Cadencia);

            GameObject boomerang = Instantiate(boomerangPrefab, transform.position, Quaternion.identity);
            BumerangDestruible bumerangScript = boomerang.GetComponent<BumerangDestruible>();
            
            if( bumerangScript != null )
            {
                bumerangScript.golpe = (int)stats.arma4Ataque;
            }

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

        GameObject[] enemiesInRange = enemies
            .Where(e => Vector3.Distance(e.transform.position, transform.position) <= 9f)
            .ToArray();

        if(enemiesInRange.Length == 0) return null;

        return enemies
            .OrderByDescending(e => Vector3.Distance(e.transform.position, transform.position))
            .First().transform;
    }
}
