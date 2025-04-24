using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo07_animaciones : MonoBehaviour
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
        animator.SetFloat("velocidadActual", enemigoScript.enemigo.speed);
    }
}
