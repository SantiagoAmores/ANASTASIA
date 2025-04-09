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

            // Agregamos el script de movimiento directamente si no está en el prefab
            BoomerangMover mover = boomerang.AddComponent<BoomerangMover>();
            mover.Initialize(transform, boomerangSpeed);
        }
    }

    public class BoomerangMover : MonoBehaviour
    {
        private Transform player;
        private Transform target;
        private float speed;
        private bool returning = false;

        public void Initialize(Transform playerTransform, float speed)
        {
            this.player = playerTransform;
            this.speed = speed;
            this.target = FindFarthestEnemy();
        }

        void Update()
        {
            if (target == null && !returning)
            {
                target = player;
                returning = true;
            }

            if (target != null)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;

                float distance = Vector3.Distance(transform.position, target.position);
                if (distance < 0.5f)
                {
                    if (!returning)
                    {
                        target = player;
                        returning = true;
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }

        Transform FindFarthestEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0) return null;

            return enemies
                .OrderByDescending(e => Vector3.Distance(e.transform.position, player.position))
                .First().transform;
        }
    }
}

