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
            if (vidaActual < vidaTotal)
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
}
