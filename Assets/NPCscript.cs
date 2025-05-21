using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCscript : MonoBehaviour
{
    public bool desbloqueado;

    void Start()
    {
        if (!desbloqueado)
        {
            gameObject.SetActive(false);
        }
    }
}
