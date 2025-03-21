using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    // Para el movimiento del personaje usaremos el Input Manager de Unity que nos va a permitir exportarlo a diferentes dispositivos sin modificar el script,
    // personalizar los controles y mantener odenado el código

    //Controles del jugador
    private float speed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();      
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime;
        rb.MovePosition(rb.position + move);
    }

}
