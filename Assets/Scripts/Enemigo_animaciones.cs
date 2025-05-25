using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_animaciones : MonoBehaviour
{
    public Enemigo enemigoScript;
    public Animator animator;
    public float velocidadActual;

    void Start()
    {
        enemigoScript = GetComponent<Enemigo>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        velocidadActual = enemigoScript.enemigo.speed;
        if (enemigoScript != null)
        {
            animator.SetFloat("velocidadActual", enemigoScript.enemigo.speed);
        }
        else
        {
            animator.SetFloat("velocidadActual", 0f);
        }
    }
}
