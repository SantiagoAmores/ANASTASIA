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
            if (firstShot) { yield return new WaitForSeconds(stats.arma2Cadencia); firstShot = false; };

            GameObject contenedor = new GameObject("PortaLapices");
            contenedor.transform.position = player.transform.position;
            contenedor.transform.rotation = Quaternion.identity;

            GameObject instanciaLapiz = Instantiate(proyectil, (player.transform.position + new Vector3(0, 0, 2)), Quaternion.Euler(0, -90, 90));
            Arma2_Adicional lapizScript = instanciaLapiz.GetComponent<Arma2_Adicional>();
            if (lapizScript != null) { lapizScript.golpe = (int)stats.arma2Ataque; }
                instanciaLapiz.transform.parent = contenedor.transform;

            float tiempoTranscurrido = 0f;

            float tiempoRotacionTotal = 0.5f + ((stats.arma2Ataque / 10) - 0.2f);
            
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
