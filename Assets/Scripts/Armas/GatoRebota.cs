using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoRebota : MonoBehaviour
{
    public int golpe;
    public Vector3 direccion;
    public float velocidad = 10f;
    public float multiplicadorVelocidad = 1.5f;
    public float radioExplosion = 5f;

    private Rigidbody rb;
    private bool haChocado = false;

    public GameObject particulasExplosion;

    public Collider childCollider;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        childCollider = GetComponentInChildren<Collider>();

        if (childCollider == null)
        {
            Debug.Log("no tiene collider");
        }

        StartCoroutine(ExplotarDespuesDeTiempo());
    }

    private void Update()
    {
        if (!haChocado)
        {
            rb.velocity = direccion * velocidad;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Solo procesar colisiones con enemigos
        if (other == childCollider || other.CompareTag("Enemy"))
        {
            Enemigo enemigo = other.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                enemigo.RecibirGolpe(golpe, this.gameObject);
                if (!haChocado)
                {
                    velocidad *= multiplicadorVelocidad;
                    haChocado = true;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si choca con una pared (o cualquier objeto que no sea enemigo)
        if (!collision.gameObject.CompareTag("Enemy") || !collision.gameObject.CompareTag("Player"))
        {
            // Calcular dirección de rebote
            Vector3 normal = collision.contacts[0].normal;
            direccion = Vector3.Reflect(direccion, normal).normalized;

            // Aplicar la nueva dirección
            if (!haChocado)
            {
                rb.velocity = direccion * velocidad;
            }
            else
            {
                rb.velocity = direccion * velocidad * multiplicadorVelocidad;
            }
        }
    }

    IEnumerator ExplotarDespuesDeTiempo()
    {
        yield return new WaitForSeconds(1.75f);
        Explotar();
        Destroy(gameObject);
    }

    void Explotar()
    {
        if (particulasExplosion != null)
        {
            GameObject particulas = Instantiate(particulasExplosion, transform.position, Quaternion.identity);
            Destroy(particulas, 2f); // se destruyen despues de 2 segundos
        }

        Collider[] enemigos = Physics.OverlapSphere(transform.position, (radioExplosion + (float)StatsAnastasia.arma6Ataque/10));
        foreach (Collider enemigo in enemigos)
        {
            if (enemigo.CompareTag("Enemy"))
            {
                enemigo.GetComponent<Enemigo>().RecibirGolpe((int)golpe * 2, this.gameObject);
            }
        }
        Destroy(gameObject);
    }
}
