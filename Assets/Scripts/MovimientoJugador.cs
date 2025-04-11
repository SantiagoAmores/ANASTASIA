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

    private Animator animator;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        characterController = GetComponent<CharacterController>();

        animator = GetComponentInChildren<Animator>();

        // Asegurarse de que no haya movimiento al iniciar el juego
        characterController.Move(Vector3.zero);

    }

    void Update()
    {

        // Para el movimiento del personaje usaremos el Input Manager de Unity que nos va a permitir exportarlo a diferentes dispositivos sin modificar el script,
        // personalizar los controles y mantener ordenado el script trabajando desde los Axes
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized;

        // 🟢 Activamos o desactivamos el bool según haya movimiento o no
        animator.SetBool("isWalking", movement.magnitude > 0.05f);

        if (movement.magnitude > 0)
        {
            characterController.Move(movement * speed * Time.deltaTime);

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
                 Debug.Log (other.name + "-" + this.name);
                 gameManager.SubirNivel();
                Destroy(other.gameObject);
  
            }
        
    }

    // Control de animación
    //animator.SetFloat("Speed", movementSpeed);
}
