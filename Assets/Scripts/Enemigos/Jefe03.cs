using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe03 : MonoBehaviour
{
    Enemigo enemigoScript;
    StatsEnemigos statsScript;
    Animator animator;
    public GameObject jugador;

    // Variables de movimiento
    private bool isAttacking = false;
    private RoundManager roundManager;

    // Variables de zigzag (fase 2)
    private float zigzagTimer = 0f;
    private float zigzagSpeed = 15f; // Aumentada
    private float zigzagWidth = 12f;  // Aumentada
    private Vector3 moveDirection;

    // Variables de aceleración
    private float currentSpeed = 0f;
    private float acceleration = 5f;
    private float deceleration = 8f;
    private float maxSpeedPhase1 = 7f;
    private float maxSpeedPhase2 = 10f;

    void Start()
    {
        enemigoScript = GetComponent<Enemigo>();
        statsScript = GetComponent<StatsEnemigos>();
        animator = GetComponentInChildren<Animator>();
        jugador = GameObject.FindGameObjectWithTag("Player");
        roundManager = FindObjectOfType<RoundManager>();

        // Configuración inicial basada en la ronda
        if (roundManager.ronda == 1) // Primera ronda de jefe
        {
            statsScript.faseDeJefe = 1; // Solo fase 1
            transform.localScale = Vector3.one; // Tamaño normal
        }
        else if (roundManager.ronda == 3) // Segunda ronda de jefe
        {
            statsScript.faseDeJefe = 1; // Comienza en fase 1
            transform.localScale = Vector3.one; // Tamaño normal inicial
        }

        statsScript.revisarEnemigo();
        StartCoroutine(ComportamientoJefe());
    }

    void Update()
    {
        animator.SetFloat("velocidadActual", enemigoScript.enemigo.speed);
        enemigoScript.MirarAnastasia();

        // Solo permitir cambio de fase en la ronda 3
        if (roundManager.ronda == 3 && statsScript.faseDeJefe == 1 &&
            statsScript.enemigoVida <= statsScript.enemigoVida / 2f)
        {
            CambiarAFase2();
        }
    }

    void CambiarAFase2()
    {
        statsScript.faseDeJefe = 2;
        statsScript.revisarEnemigo(); // Actualizar stats con los de fase 2
        transform.localScale *= 1.5f; // Aumentar tamaño
        animator.SetTrigger("CambioFase");

        // Opcional: efecto visual al cambiar de fase
        //Instantiate(particulasCambioFase, transform.position, Quaternion.identity);
    }

    public IEnumerator ComportamientoJefe()
    {
        while (true)
        {
            statsScript.revisarEnemigo();

            if (statsScript.faseDeJefe == 1)
            {
                // Comportamiento fase 1 (movimiento lineal)
                yield return StartCoroutine(MovimientoLineal());
                yield return new WaitForSeconds(1.5f); // Pausa entre ataques
            }
            else if (statsScript.faseDeJefe == 2 && roundManager.ronda == 3)
            {
                // Comportamiento fase 2 (zigzag) solo en ronda 3
                yield return StartCoroutine(MovimientoZigZag());
                yield return new WaitForSeconds(0.8f); // Pausa más corta
            }
        }
    }

    private IEnumerator MovimientoLineal()
    {
        isAttacking = true;
        animator.SetBool("atacando", true);

        float attackDuration = roundManager.ronda == 1 ? 3f : 2.5f; // Ajuste por ronda
        float timer = 0f;
        Vector3 direction = (jugador.transform.position - transform.position).normalized;

        while (timer < attackDuration)
        {
            timer += Time.deltaTime;

            // Aceleración gradual
            if (currentSpeed < maxSpeedPhase1)
            {
                currentSpeed += acceleration * Time.deltaTime;
                currentSpeed = Mathf.Min(currentSpeed, maxSpeedPhase1);
            }

            // Actualizar dirección
            direction = (jugador.transform.position - transform.position).normalized;
            transform.position += direction * currentSpeed * Time.deltaTime;

            yield return null;
        }

        // Frenado gradual
        while (currentSpeed > 0.1f)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            transform.position += direction * currentSpeed * Time.deltaTime;
            yield return null;
        }

        currentSpeed = 0f;
        isAttacking = false;
        animator.SetBool("atacando", false);
    }

    private IEnumerator MovimientoZigZag()
    {
        isAttacking = true;
        animator.SetBool("atacando", true);

        float attackDuration = 3.5f;
        float timer = 0f;
        zigzagTimer = 0f;

        moveDirection = (jugador.transform.position - transform.position).normalized;

        while (timer < attackDuration)
        {
            timer += Time.deltaTime;
            zigzagTimer += Time.deltaTime * zigzagSpeed;

            // Aceleración más rápida en fase 2
            if (currentSpeed < maxSpeedPhase2)
            {
                currentSpeed += acceleration * 1.5f * Time.deltaTime;
                currentSpeed = Mathf.Min(currentSpeed, maxSpeedPhase2);
            }

            // Movimiento en zigzag más pronunciado
            Vector3 baseMovement = moveDirection * currentSpeed * Time.deltaTime;
            Vector3 perpendicular = new Vector3(-moveDirection.z, 0, moveDirection.x);
            Vector3 zigzagOffset = perpendicular * Mathf.Sin(zigzagTimer) * zigzagWidth * Time.deltaTime;

            transform.position += baseMovement + zigzagOffset;

            // Actualizar dirección más frecuentemente para cambios más bruscos
            if (timer % 0.3f < Time.deltaTime)
            {
                moveDirection = (jugador.transform.position - transform.position).normalized;
            }

            yield return null;
        }

        // Frenado gradual
        while (currentSpeed > 0.1f)
        {
            currentSpeed -= deceleration * 1.5f * Time.deltaTime;
            transform.position += moveDirection * currentSpeed * Time.deltaTime;
            yield return null;
        }

        currentSpeed = 0f;
        isAttacking = false;
        animator.SetBool("atacando", false);
    }

    // Llamar este método cuando el jefe sea derrotado
    public void Derrotado()
    {
        roundManager.BossDerrotado();
        Destroy(gameObject);
    }
}
