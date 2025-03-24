using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    GameManager gameManager;

    //Controles del jugador
    private float speed = 5f;
    public float rotationSpeed = 10f;
    private CharacterController characterController;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        characterController = GetComponent<CharacterController>();

        // Asegurarse de que no haya movimiento al iniciar el juego
        characterController.Move(Vector3.zero);

    }

    void Update()
    {

        // Para el movimiento del personaje usaremos el Input Manager de Unity que nos va a permitir exportarlo a diferentes dispositivos sin modificar el script,
        // personalizar los controles y mantener ordenado el script trabajando desde los Axes
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Solo permitir el movimiento si hay entrada del jugador
        if (moveX != 0 || moveZ != 0)
        {
            Vector3 movement = new Vector3(moveX, 0, moveZ).normalized;
            float movementSpeed = movement.magnitude;

            // Mover al personaje
            characterController.Move(movement * speed * Time.deltaTime);

            // Rotación suave hacia la dirección del movimiento
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        else
        {
            // Detener al personaje cuando no hay movimiento (sin entrada)
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
    }

    // Control de animación
    //animator.SetFloat("Speed", movementSpeed);
}
