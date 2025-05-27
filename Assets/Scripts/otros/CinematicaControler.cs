using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CinematicaControler : MonoBehaviour
{
    public VideoClip videoCastellano;
    public VideoClip videoIngles;
    public VideoClip videoValenciano;
    public VideoPlayer videoPlayer;
    public string escenaJuego = "Scene_Museo"; // Cambia esto si tu escena del juego tiene otro nombre

    void Start()
    {
        int idiomaID = PlayerPrefs.GetInt("LocaleKey", 0); // 0 = Español, 1 = Inglés (o según tu configuración)

        // Asigna el video según el idioma
        if (idiomaID == 0)
            videoPlayer.clip = videoValenciano;
        else if (idiomaID == 1)
            videoPlayer.clip = videoIngles;
        else if (idiomaID == 2)
            videoPlayer.clip = videoCastellano;

        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        PlayerPrefs.SetInt("PrimeraVez", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(escenaJuego);
    }

}
