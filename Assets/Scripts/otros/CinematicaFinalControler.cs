using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CinematicaFinalControler : MonoBehaviour
{
    public VideoClip videoCastellano;
    public VideoClip videoIngles;
    public VideoClip videoValenciano;
    public VideoPlayer videoPlayer;
    public string escenaMuseo = "Scene_Museo"; // Nombre de la escena del museo

    void Start()
    {
        ConfigurarIdioma();

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
            videoPlayer.Play();
        }
    }

    void ConfigurarIdioma()
    {
        int idiomaID = PlayerPrefs.GetInt("LocaleKey", 0);

        if (idiomaID == 2 && videoCastellano != null)
            videoPlayer.clip = videoCastellano;
        else if (idiomaID == 1 && videoIngles != null)
            videoPlayer.clip = videoIngles;
        else if (idiomaID == 0 && videoValenciano != null)
            videoPlayer.clip = videoValenciano;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(escenaMuseo); // "Scene_Museo"
    }


    public void SaltarCinematica()
    {
        // Opción para saltar la cinemática si el jugador lo desea
        SceneManager.LoadScene(escenaMuseo);
    }
}
