using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    [Header("Componentes principales")]
    public NavMeshAgent enemigo;
    private Collider enemigoCollider;
    private MovimientoJugador moverJugador;
    private CanvasManager canvasManager;
    private Jefe01 jefeScript;
    private StatsAnastasia statsAnastasia;

    [Header("Objetos y prefabs")]
    public GameObject jugador;
    public GameObject puntoExperienciaPrefab;
    public GameObject corazonPrefab;
    public GameObject textoDanoPrefab;
    public GameObject spawnEfectoPrefab;
    public GameObject regaloPrefab;

    [Header("Estadisticas")]
    public StatsEnemigos estadisticas;
    public int enemigoVidaTotal;
    public float enemigoVelocidad;
    public int enemigoAtaque;
    public int enemigoExperiencia;
    public int enemigoVidaActual;

    [Header("Bools")]
    public bool golpeable = true;
    public bool seguirJugador = true;

    // Animaciones
    //private Animator animator;

    private void Awake()
    {
        enemigo = GetComponent<NavMeshAgent>();
        enemigoCollider = GetComponent<Collider>();
    }

    void Start()
    {
        InstanciarEfectoDeSpawn();

        jugador = GameObject.FindGameObjectWithTag("Player");
        moverJugador = jugador.GetComponent<MovimientoJugador>();
        canvasManager = FindObjectOfType<CanvasManager>();
        estadisticas = GetComponent<StatsEnemigos>();
        jefeScript = GetComponent<Jefe01>();
        statsAnastasia = jugador.GetComponent<StatsAnastasia>();

        // Busca al enemigo a partir del nombre de su prefab y le otorga sus respectivas estadisticas
        estadisticas.revisarEnemigo();

        if (estadisticas.esUnJefe)
        {
            // Asigna como fase por defecto del jefe la fase 1
            if (estadisticas.faseDeJefe == 0) { estadisticas.faseDeJefe = 1; }
            // Asigna los stats del jefe dependiendo de su fase
            if (estadisticas.faseDeJefe == 1 || estadisticas.faseDeJefe == 2) { estadisticas.revisarEnemigo(); }
        }

        enemigoVidaTotal = estadisticas.enemigoVida;
        enemigoAtaque = estadisticas.enemigoAtaque;
        enemigoVelocidad = estadisticas.enemigoVelocidad;
        enemigoExperiencia = estadisticas.enemigoExperiencia;

        // Asigna su salud actual = su vida total
        enemigoVidaActual   = enemigoVidaTotal;

        // Asigna la velocidad del navMeshAgent = su velocidad
        enemigo.speed       = enemigoVelocidad;

        MirarAnastasia();

        StartCoroutine(AlSpawnear());

        /*
        animator = GetComponent<Animator>();  // Obtener el Animator del enemigo

        animator.Play("Walking"); //Animacion

        // Iniciar la animación de caminar
        if (animator != null)
        {
            animator.SetBool("Walking", true);
        }
        */
    }

    void Update()
    {
        if (seguirJugador)
        {
            enemigo.SetDestination(jugador.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            moverJugador.herirAnastasia(enemigoAtaque);
            AnimacionHerida();
        }
    }

    public void RecibirGolpe(int cantidadDeGolpe, GameObject atacante)
    {
        
        if (!golpeable)
        {
            return;
        }
        if (atacante.CompareTag("ProjectileDPS"))
        {
            StartCoroutine(HeridaPausa());
        }

        MostrarTextoDano(cantidadDeGolpe);

        // Le resta al enemigo de su salud el daño que hace el golpe recibido
        enemigoVidaActual -= cantidadDeGolpe;

        // Si la salud del enemigo es igual o baja de 0
        if (enemigoVidaActual <= 0)
        {
            string nombreEnemigo = gameObject.name.Replace("(Clone)", "").Trim();
            if (nombreEnemigo == "Jarron")
            {
                DropDeJarron();
            }
            else
            {
                DropNormal();
            }
            
            if (estadisticas.esUnJefe && estadisticas.faseDeJefe == 1) { DropDeJefe(); }
            if (jefeScript != null) { jefeScript.EliminarTodosLosProyectiles(); }

            // Y despues destruye al enemigo
            Destroy(this.gameObject);
        }
    }
    public IEnumerator HeridaPausa()
    {
        golpeable = false;
        yield return new WaitForSeconds(statsAnastasia.ticsPorSegundo);
        golpeable = true;
    }
    public IEnumerator AnimacionHerida()
    {
        //SkinnedMeshRenderer [] anastasiaPiezas;

        //anastasiaPiezas = jugador.GetComponentsInChildren<SkinnedMeshRenderer>();

        //foreach (var pieza in anastasiaPiezas)
        //{
        //    pieza.material.color = Color.red;
        //}

        yield return new WaitForSeconds(0.5f);

        //foreach (var pieza in anastasiaPiezas)
        //{
        //    pieza.material.color = Color.white;
        //}
    }

    // SPAWN
    public IEnumerator AlSpawnear()
    {
        // Inhabilita temporalmente el collider y el movimiento del enemigo para que no le hagan daño a Anastasia
        // nada mas aparecer. Despues vuelve a activar el collider y le asigna su velocidad correspondiente
        enemigoCollider.enabled = false;
        enemigo.speed = 0f;

        yield return new WaitForSeconds(0.33f);

        enemigoCollider.enabled = true;
        enemigo.speed = enemigoVelocidad;
    }
    void InstanciarEfectoDeSpawn()
    {
        if (spawnEfectoPrefab != null)
        {
            GameObject efecto = Instantiate(spawnEfectoPrefab, transform.position, Quaternion.identity);
            Destroy(efecto, 1f);
        }
    }
    public void MirarAnastasia()
    {
        // Hace que el enemigo mire hacia Anastasia al ser instanciado
        Vector3 miraAnastasia = jugador.transform.position - transform.position;
        miraAnastasia.y = 0f;
        if (miraAnastasia != Vector3.zero) { transform.rotation = Quaternion.LookRotation(miraAnastasia); }
    }

    public void DropNormal()
    {
        //Debug.Log("Experiencia instanciada");
        // Genera un area pequeña con una altura fija
        float radioDrop = 1f;
        float alturaFija = 0.5f;
        // Y dependiendo del int enemigoExperiencia
        for (int i = 0; i < enemigoExperiencia; i++)
        {
            // Instanciara en lugares aleatorios del area su respectiva cantidad de puntos de experiencia
            Vector2 offset2D = Random.insideUnitCircle * radioDrop;
            Vector3 dropPosicion = new Vector3(
                transform.position.x + offset2D.x,
                alturaFija,
                transform.position.z + offset2D.y
                );
            Instantiate(puntoExperienciaPrefab, dropPosicion, Quaternion.identity);
        }

        // Dropeo de vida
        int aleatorio = Random.Range(0, 9);
        if (aleatorio == 0)
        {
            Vector2 offsetVida2D = Random.insideUnitCircle * radioDrop;
            Vector3 dropVidaPosicion = new Vector3(
                transform.position.x + offsetVida2D.x,
                alturaFija,
                transform.position.z + offsetVida2D.y
                );
            Instantiate(corazonPrefab, dropVidaPosicion, Quaternion.identity);
        }
    }

    public void DropDeJefe()
    {
        GameObject inventarioDeAnastasia = GameObject.Find("Inventario");

        string nombreJefe = gameObject.name.Replace("(Clone)", "").Trim();

        switch (nombreJefe)
        {
            case "Enemigo 3":
                inventarioDeAnastasia.GetComponent<Arma2>().enabled = true;
                break;
            case "Enemigo 6":
                inventarioDeAnastasia.GetComponent<Arma3>().enabled = true;
                break;
            case "Enemigo 9":
                inventarioDeAnastasia.GetComponent<Arma5>().enabled = true;
                break;
            default:
                break;
        }
    }

    public void DropDeJarron()
    {
        Instantiate(regaloPrefab, transform.position, Quaternion.identity);
    }

    void MostrarTextoDano(int cantidad)
    {
        if (textoDanoPrefab != null)
        {
            float alturaOffset = 0.2f + (transform.localScale.y * 0.5f);
            Vector3 posicionTexto = transform.position + new Vector3(0, alturaOffset, 0);
            GameObject textoInstancia = Instantiate(textoDanoPrefab, posicionTexto, Quaternion.identity);
            TextMeshProUGUI texto = textoInstancia.GetComponentInChildren<TextMeshProUGUI>();
            if (texto != null)
            {
                texto.text = cantidad.ToString();
            }
        }
    }
}
