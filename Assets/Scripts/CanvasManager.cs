using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{

    //GameManager
    GameManager gameManager;

    //Estadisticas
    public TextMeshProUGUI textoExperiencia;
    public TextMeshProUGUI textoNivel;


    //Cuenta atras
    public TextMeshProUGUI cuentaAtras;
    public float startTime = 120f;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        textoExperiencia = GameObject.Find("TextoExperiencia").GetComponent<TextMeshProUGUI>();
        textoNivel = GameObject.Find("TextoNivel").GetComponent<TextMeshProUGUI>();

        timeLeft = startTime;
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        textoExperiencia.text = gameManager.experienciaTotal.ToString();
        textoNivel.text = gameManager.nivel.ToString();
    }

    IEnumerator Countdown()
    {
        while (timeLeft > 0)
        {
            cuentaAtras.text = timeLeft.ToString("0");
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }

        cuentaAtras.text = "Te has quedado sin tiempo :(";
    }
}
