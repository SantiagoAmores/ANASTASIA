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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemigo>().RecibirGolpe(golpe);
            if (!haChocado)
            {
                velocidad *= multiplicadorVelocidad;
                haChocado = true;
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
        yield return new WaitForSeconds(6f);
        Explotar();
        Destroy(gameObject);
    }

    void Explotar()
    {
        if (particulasExplosion != null)
        {
            Instantiate(particulasExplosion, transform.position, Quaternion.identity);
        }

        Collider[] enemigos = Physics.OverlapSphere(transform.position, radioExplosion);
        foreach (Collider enemigo in enemigos)
        {
            if (enemigo.CompareTag("Enemy"))
            {
                enemigo.GetComponent<Enemigo>().RecibirGolpe((int)golpe * 2);
            }
        }
        Destroy(gameObject);
    }
}
