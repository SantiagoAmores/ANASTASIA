using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public int ronda = 0;

    public float duracionRonda = 120f;
    public GameObject bossPrefab;
    public Transform bossSpawn;

    private Coroutine rondaActual;

    // Start is called before the first frame update
    void Start()
    {
        IniciarSiguienteFase(); 
    }

    public void IniciarSiguienteFase()
    {
        if (ronda == 0 || ronda == 2)
        {
            rondaActual = StartCoroutine(Ronda());
        }
        else if (ronda ==1 || ronda == 3)
        {
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
        GameObject boss = Instantiate(bossPrefab, bossSpawn.position, Quaternion.identity);

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

        //Aqui pondremos las animaciones y demas cosas
    }
}
