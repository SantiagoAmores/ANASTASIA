using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public int ronda = 0;

    public float duracionRonda = 120f;
    /*public GameObject bossPrefab;
    public Transform bossSpawn;*/

    public SpawnEnemigos spawner;

    private Coroutine rondaActual;

    private Enemigo jefeActual;

    private CanvasManager canvasManager;

    // Start is called before the first frame update
    void Start()
    {
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();

        spawner = GetComponent<SpawnEnemigos>();
        if (spawner != null)
        {
            spawner.ronda = ronda;
            spawner.seguir = true;
        }

        IniciarSiguienteFase(); 

    }

    public void IniciarSiguienteFase()
    {
        if (spawner != null)
        {
            spawner.ronda = ronda;
        }


        if (ronda == 0 || ronda == 2)
        {
            ActivarSpawner(true);
            rondaActual = StartCoroutine(Ronda());
        }
        else if (ronda ==1 || ronda == 3)
        {
            ActivarSpawner(false);
            SpawnBoss();
        }
        else if (ronda >= 4)
        {
            PantallaVictoria();
        }
    }

    IEnumerator Ronda()
    {
        Debug.Log("Comienza ronda " + (ronda == 0 ? 1 : 2));
        yield return new WaitForSeconds(duracionRonda);

        ronda++;
        IniciarSiguienteFase();
    }

    void SpawnBoss()
    {
        Debug.Log("Aparece jefe " + (ronda == 1 ? "1" : "Final"));
        /*GameObject boss = Instantiate(bossPrefab, bossSpawn.position, Quaternion.identity);

        StatsEnemigos statsBoss = boss.GetComponent<StatsEnemigos>();

        //asignarle los stats que tocan al boss
        if(statsBoss != null)
        {
            if (ronda == 1)
            {
                statsBoss.faseDeJefe = 1;
            }
            else if (ronda == 3)
            {
                statsBoss.faseDeJefe = 2;
            }

            //actualizar stats llamando a revisarEnemigo
            statsBoss.revisarEnemigo();
        }
        if (spawner != null)
        {
            int fase = (ronda == 1) ? 1 : 2;
            spawner.SpawnBoss(fase);
        }
        */

        GameObject jefeGameObject = spawner.SpawnBoss((ronda == 1) ? 1 : 2);
        jefeActual = jefeGameObject.GetComponent<Enemigo>();

        if (jefeActual != null)
        {
            StartCoroutine(EsperarMuerteJefe());
        }
    }

    //esto lo llamaremos desde el script de cada boss cuando la vida llegue a 0
    public void BossDerrotado()
    {
        ronda++;
        IniciarSiguienteFase();
    }

    void PantallaVictoria()
    {
        Debug.Log("Has Ganado");
        ActivarSpawner(false);

        //Aqui pondremos las animaciones y demas cosas
        canvasManager.Victoria();
    }

    void ActivarSpawner(bool activo)
    {
        if (spawner != null)
        {
            spawner.seguir = activo;
        }
    }

    IEnumerator EsperarMuerteJefe()
    {
        while (jefeActual != null && jefeActual.enemigoVidaActual <= 0)
        {
            yield return null;
        }

        while (jefeActual != null && jefeActual.enemigoVidaActual > 0)
        {
            yield return null;
        }

        BossDerrotado();
    }
}
