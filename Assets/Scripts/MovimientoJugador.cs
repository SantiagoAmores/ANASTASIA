using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoJugador : MonoBehaviour
{
    GameManager gameManager;

    private CanvasManager canvasManager;

    //Controles del jugador
    public float rotationSpeed = 10f;
    private CharacterController characterController;

    private Animator animator;

    public StatsAnastasia stats;

    public int vidaTotal;
    public int vidaActual;

    public GameObject flechaDireccion;
    public Transform flechaObjetivo;

    public GameObject textoCuracionPrefab;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        canvasManager = FindObjectOfType<CanvasManager>();

        characterController = GetComponent<CharacterController>();

        animator = GetComponentInChildren<Animator>();

        // Asegurarse de que no haya movimiento al iniciar el juego
        characterController.Move(Vector3.zero);

        stats = GameObject.FindWithTag("Player").GetComponent<StatsAnastasia>();

        // Declaramos la vida de Anastasia al comienzo del nivel
        vidaTotal = stats.vidaBase;
        vidaActual = vidaTotal;

        flechaDireccion = transform.Find("FlechaDireccion")?.gameObject;
        if (flechaDireccion != null)
        {
            flechaDireccion.SetActive(false);
        }
    }

    void Update()
    {
        // Actualizar vida Anastasia
        vidaTotal = stats.vida;

        // Para el movimiento del personaje usaremos el Input Manager de Unity que nos va a permitir exportarlo a diferentes dispositivos sin modificar el script,
        // personalizar los controles y mantener ordenado el script trabajando desde los Axes
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized;

        // Activamos o desactivamos el bool según haya movimiento o no
        animator.SetBool("isWalking", movement.magnitude > 0.05f);

        if (movement.magnitude > 0)
        {
            // Utiliza la estadistica de velocidad de StatsAnastasia
            float velocidad = stats.velocidadMovimiento;
            characterController.Move(movement * velocidad * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            characterController.Move(Vector3.zero);
        }

        if (flechaDireccion != null && flechaObjetivo != null)
        {
            actualizarDireccionFlecha();
        }
        if (SceneManager.GetActiveScene().name != "Scene_Museo")
        {
            if (Input.GetKeyDown(KeyCode.E) && canvasManager.objetoActivable.activeSelf)
            {
                Debug.Log("Objeto activado");

                if (canvasManager.objeto1.activeSelf)
                {
                    Debug.Log("Objeto 1 activado");
                    canvasManager.objeto1.SetActive(false);

                }
                if (canvasManager.objeto2.activeSelf)
                {
                    Debug.Log("Objeto 2 activado");
                    canvasManager.objeto2.SetActive(false);
                }
                if (canvasManager.objeto3.activeSelf)
                {
                    Debug.Log("Objeto 3 activado");
                    Curar(vidaTotal);
                    canvasManager.objeto3.SetActive(false);
                }
                if (canvasManager.objeto4.activeSelf)
                {
                    Debug.Log("Objeto 4 activado");
                    canvasManager.objeto4.SetActive(false);
                }
                if (canvasManager.objeto5.activeSelf)
                {
                    Debug.Log("Objeto 5 activado");
                    canvasManager.objeto5.SetActive(false);
                }

                canvasManager.objetoActivable.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Experiencia"))
        {
            gameManager.SubirNivel();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Corazon"))
        {
            int curacion = vidaTotal / 10;
            Curar(curacion);
            Destroy(other.gameObject);
        }
    }

    public void herirAnastasia(int cantidadHerida)
    {
        vidaActual -= cantidadHerida;

        if (vidaActual <= 0)
        {
            Time.timeScale = 0f;
            canvasManager.Derrota();
        }
    }

    public void mostrarFlecha(bool mostrar, Transform objetivo = null)
    {
        if (flechaDireccion != null)
        {
            flechaDireccion.SetActive(mostrar);
            flechaObjetivo = mostrar ? objetivo : null;
        }
    }

    private void actualizarDireccionFlecha()
    {
        float radio = 2f;

        Vector3 direccion = flechaObjetivo.position - transform.position;
        direccion.y = 0f;

        float angulo = Mathf.Atan2(direccion.z, direccion.x);

        float x = transform.position.x + radio * Mathf.Cos(angulo);
        float z = transform.position.z + radio * Mathf.Sin(angulo);

        flechaDireccion.transform.position = new Vector3(x, transform.position.y - 1f, z);

        float distancia = direccion.magnitude;

        if (distancia < 4f)
        {
            flechaDireccion.SetActive(false);
        }
        else
        {
            flechaDireccion.SetActive(true);
        }

        if (direccion.sqrMagnitude > 0.01f)
        {
            Quaternion rotacion = Quaternion.LookRotation(direccion);
            flechaDireccion.transform.rotation = Quaternion.Euler(-90f, rotacion.eulerAngles.y + 180f, 0f);
        }
    }

    public void Curar(int cantidad)
    {
        vidaActual += cantidad;
        if (vidaActual > vidaTotal)
        {
            vidaActual = vidaTotal;
        }
        MostrarTextoCuracion(cantidad);
    }

    void MostrarTextoCuracion(int cantidad)
    {
        if (textoCuracionPrefab != null)
        {
            float alturaOffset = 0.2f + (transform.localScale.y * 0.5f);
            Vector3 posicionTexto = transform.position + new Vector3(0, alturaOffset, 0);
            GameObject textoInstancia = Instantiate(textoCuracionPrefab, posicionTexto, Quaternion.identity);
            TextMeshProUGUI texto = textoInstancia.GetComponentInChildren<TextMeshProUGUI>();
            if (texto != null)
            {
                texto.text = cantidad.ToString();
            }
        }
    }
}
