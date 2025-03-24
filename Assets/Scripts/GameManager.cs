using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Estadisticas
    public int experienciaTotal = 0;
    public int nivel = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubirNivel()
    {
        experienciaTotal++;

        if (experienciaTotal % 5 == 0)
        {
            nivel++;
        }
    }
}
