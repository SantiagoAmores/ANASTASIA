using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe01 : MonoBehaviour
{
    Enemigo enemigoScript;
    StatsEnemigos statsScript;
    Animator animator;
    public GameObject jugador;
    public GameObject proyectilJefe;
    public List<GameObject> proyectilListaJefe = new List<GameObject>();
    public Vector3 ojo;
    Transform ojoTransform;
    public GameObject particulasDisparo;

    void Start()
    {
        enemigoScript = GetComponent<Enemigo>();
        statsScript = GetComponent<StatsEnemigos>();
        animator = GetComponentInChildren<Animator>();
        jugador = GameObject.FindGameObjectWithTag("Player");

        ojoTransform = transform.Find("Ojo");
        if (ojoTransform != null)
        {
            ojo = ojoTransform.position;
        }
        if (statsScript.faseDeJefe == 2)
        {
            transform.localScale *= 2f;
        }

        StartCoroutine(Patron());
    }

    void Update()
    {
        animator.SetFloat("velocidadActual", enemigoScript.enemigo.speed);
        enemigoScript.MirarAnastasia();
        if (ojoTransform != null)
        {
            ojo = ojoTransform.position;
        }
    }

    public IEnumerator Patron()
    {
        while (true)
        {
            // Reasigna los stats y las animaciones del jefe al principio por si acaso
            statsScript.revisarEnemigo();
            enemigoScript.enemigo.speed = statsScript.enemigoVelocidad;
            animator.SetBool("atacando", false);
            yield return new WaitForSeconds(5f);

            // Deja quieto al jefe mientras ataca 
            enemigoScript.enemigo.speed = 0f;
            animator.SetBool("atacando", true);
            yield return new WaitForSeconds(1f);

            // Ataca durante 5 segundos, despues, se queda quieto durante un breve periodo de tiempo para ser atacado
            StartCoroutine(Metralleta());
            yield return new WaitForSeconds(5f);

            animator.SetBool("atacando", false);
            yield return new WaitForSeconds(2.5f);

        }
    }

    private IEnumerator Metralleta()
    {
        float tiempoFinal = Time.time + 5f;
        while (Time.time < tiempoFinal)
        {
            if (statsScript.faseDeJefe == 1)
            {
                Vector3 direccion = (jugador.transform.position - transform.position).normalized;

                if (particulasDisparo != null)
                {
                    GameObject particulas = Instantiate(particulasDisparo, ojo, Quaternion.LookRotation(direccion));
                    Destroy(particulas, 4f);
                }

                GameObject instanciaProyectilJefe   = Instantiate(proyectilJefe, ojo, Quaternion.LookRotation(direccion));

                proyectilListaJefe.Add(instanciaProyectilJefe);

                Jefe01_Proyectil scriptProyectil    = instanciaProyectilJefe.GetComponent<Jefe01_Proyectil>();
                scriptProyectil.statsScript         = this.statsScript;

                Rigidbody rb                        = instanciaProyectilJefe.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                rb.AddForce(direccion * 500f);

                StartCoroutine(DespawnProyectilRutina(instanciaProyectilJefe));
                yield return new WaitForSeconds(0.2f);
            } 
            else if (statsScript.faseDeJefe == 2)
            {
                Vector3 direccion = (jugador.transform.position - transform.position).normalized;

                if (particulasDisparo != null)
                {
                    GameObject particulas = Instantiate(particulasDisparo, ojo, Quaternion.LookRotation(direccion));
                    particulas.transform.localScale *= 2f;
                    Destroy(particulas, 4f);
                }

                // PROYECTIL PRINCIPAL DEL JEFE
                GameObject instanciaProyectilJefe   = Instantiate(proyectilJefe, ojo, Quaternion.LookRotation(direccion));

                instanciaProyectilJefe.transform.localScale *= 2f;
                proyectilListaJefe.Add(instanciaProyectilJefe);

                Jefe01_Proyectil scriptProyectil = instanciaProyectilJefe.GetComponent<Jefe01_Proyectil>();
                scriptProyectil.statsScript = this.statsScript;

                Rigidbody rb                        = instanciaProyectilJefe.GetComponent<Rigidbody>();
                rb.constraints                      = RigidbodyConstraints.FreezePositionY;
                rb.AddForce(direccion * 500f);

                StartCoroutine(DespawnProyectilRutina(instanciaProyectilJefe));

                // PROYECTIL SECUNDARIO DEL JEFE (45 ANGULOS POSITIVOS)
                Vector3 direccion45                 = Quaternion.Euler(0, 45, 0) * direccion;
                GameObject proyectilAdicional1      = Instantiate(proyectilJefe, ojo, Quaternion.LookRotation(direccion45));
                proyectilAdicional1.transform.localScale *= 2f;
                proyectilListaJefe.Add(proyectilAdicional1);

                Jefe01_Proyectil scriptProyectilAdicional1 = proyectilAdicional1.GetComponent<Jefe01_Proyectil>();
                scriptProyectilAdicional1.statsScript = this.statsScript;

                Rigidbody rb45                      = proyectilAdicional1.GetComponent<Rigidbody>();
                rb45.constraints                    = RigidbodyConstraints.FreezePositionY;
                rb45.AddForce(direccion45 * 500f);

                StartCoroutine(DespawnProyectilRutina(proyectilAdicional1));

                // PROYECTIL SECUNDARIO DEL JEFE (45 ANGULOS NEGATIVOS)
                Vector3 direccion45nega             = Quaternion.Euler(0, -45, 0) * direccion;
                GameObject proyectilAdicional2      = Instantiate(proyectilJefe, ojo, Quaternion.LookRotation(direccion45nega));
                proyectilAdicional2.transform.localScale *= 2f;
                proyectilListaJefe.Add(proyectilAdicional2);

                Jefe01_Proyectil scriptProyectilAdicional2 = proyectilAdicional2.GetComponent<Jefe01_Proyectil>();
                scriptProyectilAdicional2.statsScript = this.statsScript;

                Rigidbody rbMinus45                 = proyectilAdicional2.GetComponent<Rigidbody>();
                rbMinus45.constraints               = RigidbodyConstraints.FreezePositionY;
                rbMinus45.AddForce(direccion45nega * 500f);

                StartCoroutine(DespawnProyectilRutina(proyectilAdicional2));

                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private IEnumerator DespawnProyectilRutina(GameObject proyectilADespawnear)
    {
        yield return new WaitForSeconds(3.5f);
        if (proyectilListaJefe.Count > 0)
        {
            Destroy(proyectilListaJefe[0]);
            proyectilListaJefe.RemoveAt(0);
        }
    }
}
