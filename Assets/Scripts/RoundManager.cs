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

    public NivelManager nivelManager;
    public int numeroNivelActual = 1; // se mira a ver que nivel es

    public MovimientoJugador movimientoJugador;

    // Start is called before the first frame update
    void Start()
    {
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();

        spawner = GetComponent<SpawnEnemigos>();
        if (spawner != null)
        {
            spawner.seguir = true;
        }

        movimientoJugador = GameObject.FindWithTag("Player")?.GetComponent<MovimientoJugador>();

        IniciarSiguienteFase(); 
    }

    public void IniciarSiguienteFase()
    {
        ActualizarIntefazRonda();

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

        if (movimientoJugador != null)
        {
            bool mostrarFlechaBool = (ronda == 1 || ronda == 3);
            movimientoJugador.mostrarFlecha(mostrarFlechaBool, mostrarFlechaBool ? jefeActual?.transform : null);
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
        */ //esto era para instanciar al jefe desde aqui en un punto exacto

        GameObject jefeGameObject = spawner.SpawnBoss((ronda == 1) ? 1 : 2);
        jefeActual = jefeGameObject.GetComponent<Enemigo>();
        GameManager.instancia.jefeActual = jefeActual;

        if (jefeActual != null)
        {
            canvasManager.sliderBossObject.SetActive(true);
            canvasManager.sliderBoss.gameObject.SetActive(true);
            canvasManager.sliderBoss.maxValue = jefeActual.enemigoVidaTotal;
            canvasManager.sliderBoss.value = jefeActual.enemigoVidaActual;

            StartCoroutine(EsperarMuerteJefe());
        }
    }

    //esto lo llamaremos desde el script de cada boss cuando la vida llegue a 0
    public void BossDerrotado()
    {
        canvasManager.sliderBossObject.SetActive(false);
        canvasManager.sliderBoss.gameObject.SetActive(false);
        GameManager.instancia.jefeActual = null;
        ronda++;
        IniciarSiguienteFase();
    }

    void PantallaVictoria()
    {
        Debug.Log("Has Ganado");
        ActivarSpawner(false);

        //Aqui pondremos las animaciones y demas cosas
        canvasManager.Victoria();
        // Desbloquear entradas del bestiario correspondientes a este nivel
        nivelManager.NivelCompletado(numeroNivelActual);
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

    void ActualizarIntefazRonda()
    {
        string texto = "";

        switch (ronda)
        {
            case 0:
                texto = "ROUND\n1";
                canvasManager.ReiniciarCuentaAtras();
                break;
            case 1:
                texto = "BOSS\n1";
                canvasManager.InfinitoCuentaAtras();
                break;
            case 2:
                texto = "ROUND\n2";
                canvasManager.ReiniciarCuentaAtras();
                break;
            case 3:
                texto = "BOSS\n2";
                canvasManager.InfinitoCuentaAtras();
                break;
        }
        canvasManager.ActualizarTextoRonda(texto);
    }
}
