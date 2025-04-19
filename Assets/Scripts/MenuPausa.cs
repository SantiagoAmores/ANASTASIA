using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;

    //variables para comprobar si hay otros paneles abiertos y asi que no te deje pausar
    public GameObject panelVictoria;
    public GameObject panelDerrota;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!panelVictoria.activeSelf && !panelDerrota.activeSelf)
            {
                if (menuPausa.activeSelf)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Pause()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
    }
}
