using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    GameManager gameManager;

    private CanvasManager canvasManager;

    //Controles del jugador
    //private float speed = 5f;
    public float rotationSpeed = 10f;
    private CharacterController characterController;

    private Animator animator;

    public StatsAnastasia stats;

    public int vidaTotal;
    public int vidaActual;

    public GameObject flechaDireccion;
    public Transform flechaObjetivo;

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

            vidaActual += 2;
            if (vidaActual > vidaTotal)
            {
                vidaActual = vidaTotal; // Para evitar que tenga mas vida actual que total
            }
            Destroy(other.gameObject);

        }

    }


    public void herirAnastasia(int cantidadHerida)
    {
        //Debug.Log("Daño recibido: " + cantidadHerida);
        vidaActual -= cantidadHerida;
        //Debug.Log("Vida actual: " + vidaActual);

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
}
