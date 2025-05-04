using System.Collections;
using UnityEngine;

public class Arma2 : MonoBehaviour
{
    public GameObject player;
    public GameObject proyectil;

    public StatsAnastasia stats;

    private bool firstShot = true;

    void Start()
    {
        player = GameObject.Find("Anastasia");
        stats = player.GetComponent<StatsAnastasia>();
        StartCoroutine(InstanciarProyectil());
    }

    private IEnumerator InstanciarProyectil()
    {
        while (true)
        {
            // Espera antes de instanciar el arma por primera vez al cargar el nivel
            if (firstShot) { yield return new WaitForSeconds(stats.arma2Cadencia); firstShot = false; };

            // Crea un objeto que seguira al jugador
            GameObject contenedor = new GameObject("PortaLapices");
            contenedor.transform.position = player.transform.position;
            contenedor.transform.rotation = Quaternion.identity;

            // Crea una instancia del lapiz y le otorga el daño
            GameObject instanciaLapiz = Instantiate(proyectil, (player.transform.position + new Vector3(0, 0, 2)), Quaternion.Euler(0, -90, 90));
            // Escala el tamaño del arma con las subidas de ataque
            float rango = 1.3f + ((float)stats.mejorasAtaque / 9f);
            //Debug.Log(rango);
            instanciaLapiz.transform.localScale = new Vector3(0.5f, rango, 0.5f);
            Arma2_Adicional lapizScript = instanciaLapiz.GetComponent<Arma2_Adicional>();
            if (lapizScript != null) { lapizScript.golpe = (int)stats.arma2Ataque; }

            // Transforma a la instancia del lapiz como hija del contenedor
            instanciaLapiz.transform.parent = contenedor.transform;

            // Calcula el tiempo de rotacion total, para que escale con el daño del arma
            float tiempoRotacionTotal = 1f /* + ((stats.arma2Ataque / 10) - 0.2f)*/;
            float tiempoTranscurrido = 0f;

            // Gira alrededor del contenedor
            // Si no se colocase el lapiz en el contenedor, este aceleraria/deceleraria cada vez que el jugador girase. El contenedor evita eso
            while (tiempoTranscurrido < tiempoRotacionTotal)
            {
                contenedor.transform.position = player.transform.position;

                contenedor.transform.RotateAround(player.transform.position, Vector3.up, 720f * Time.deltaTime);
                tiempoTranscurrido += Time.deltaTime;
                yield return null;
            }

            Destroy(contenedor);

            yield return new WaitForSeconds(stats.arma2Cadencia);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
