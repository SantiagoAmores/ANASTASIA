using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_animaciones : MonoBehaviour
{
    Enemigo enemigoScript;
    Animator animator;

    void Start()
    {
        enemigoScript = GetComponent<Enemigo>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
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
