using System.Collections;
using UnityEngine;

public class Arma2 : MonoBehaviour
{
    public GameObject player;
    public GameObject proyectil;

    void Start()
    {
        player = GameObject.Find("Jugador");
        StartCoroutine(InstanciarProyectil());
    }

    private IEnumerator InstanciarProyectil()
    {
        while (true)
        {
            GameObject instanciaProyectil = Instantiate(proyectil, (player.transform.position + new Vector3(0,0,2)), Quaternion.Euler(0, -90, 90));
            float tiempoTranscurrido = 0f;
            
            while (tiempoTranscurrido < 1)
            {
                instanciaProyectil.transform.RotateAround(player.transform.position, Vector3.up, 360f * Time.deltaTime);
                tiempoTranscurrido += Time.deltaTime;
                yield return null;
            }

            Destroy(instanciaProyectil);
            yield return new WaitForSeconds(2f);
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
