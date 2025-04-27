using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Clase que contiene toda la informaci�n de cada entrada del bestiario
[System.Serializable]
public class EntradaBestiario
{
    public Sprite imagen;       // Imagen del elemento
    public string nombre;       // Nombre a mostrar
    public string descripcion;  // Descripci�n detallada
    public bool desbloqueado;   // Si est� disponible para ver
}

public class BestiarioManager : MonoBehaviour
{
    [Header("UI Principal")]
    public GameObject bestiarioCanvas;
    public Image imagenDisplay;
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoDescripcion;

    [Header("Contenido Organizado")]
    public EntradaBestiario[] enemigos;
    public EntradaBestiario[] armas;
    public EntradaBestiario[] jefes;
    public EntradaBestiario[] coleccionables;

    [Header("Botones Categor�as")] 
    public Button botonEnemigos;
    public Button botonArmas;
    public Button botonJefes;
    public Button botonColeccionables;

    [Header("Botones Navegaci�n")]
    public Button botonAnterior;
    public Button botonSiguiente;
    public TextMeshProUGUI textoPagina;
    public TextMeshProUGUI textoCategoria; // Muestra la categor�a actual

    // Variables de control
    private EntradaBestiario[] entradasActuales;
    private int indiceActual = 0;
    private string categoriaActual = "";

    void Start()
    {
        ConfigurarBotones(); 
        CerrarBestiario();
    }

    void ConfigurarBotones()
    {
        // Asignacin directa desde Inspector
        botonEnemigos.onClick.AddListener(() => MostrarCategoria("ENEMIGOS", enemigos));
        botonArmas.onClick.AddListener(() => MostrarCategoria("ARMAS", armas));
        botonJefes.onClick.AddListener(() => MostrarCategoria("JEFES", jefes));
        botonColeccionables.onClick.AddListener(() => MostrarCategoria("COLECCIONABLES", coleccionables));

        botonAnterior.onClick.AddListener(() => CambiarEntrada(-1));
        botonSiguiente.onClick.AddListener(() => CambiarEntrada(1));
    }

    void InicializarEstados()
    {
        // Aqu� puedes cargar datos guardados o inicializar desbloqueos
        // Ejemplo: desbloquear el primer elemento de cada categor�a
        if (enemigos.Length > 0) enemigos[0].desbloqueado = true;
        if (armas.Length > 0) armas[0].desbloqueado = true;
        if (jefes.Length > 0) jefes[0].desbloqueado = true;
        if (coleccionables.Length > 0) coleccionables[0].desbloqueado = true;
    }

    public void AbrirBestiario()
    {
        bestiarioCanvas.SetActive(true);
        if (string.IsNullOrEmpty(categoriaActual))
            MostrarCategoria("enemigos", enemigos);
    }

    public void CerrarBestiario()
    {
        bestiarioCanvas.SetActive(false);
    }

    public void MostrarCategoria(string categoria, EntradaBestiario[] entradas)
    {
        categoriaActual = categoria;
        entradasActuales = entradas;
        indiceActual = 0;

        // Actualizar UI
        textoCategoria.text = categoria.ToUpper();
        MostrarEntradaActual();
    }

    void CambiarEntrada(int cambio)
    {
        if (entradasActuales == null || entradasActuales.Length == 0) return;

        // Buscar pr�xima entrada desbloqueada
        int intentos = 0;
        do
        {
            indiceActual += cambio;

            // Navegaci�n circular
            if (indiceActual < 0) indiceActual = entradasActuales.Length - 1;
            if (indiceActual >= entradasActuales.Length) indiceActual = 0;

            intentos++;
            if (intentos > entradasActuales.Length) break; // Evitar bucle infinito
        } while (!entradasActuales[indiceActual].desbloqueado && entradasActuales.Length > 1);

        MostrarEntradaActual();
    }

    void MostrarEntradaActual()
    {
        if (entradasActuales == null || indiceActual >= entradasActuales.Length) return;

        EntradaBestiario entrada = entradasActuales[indiceActual];

        if (entrada.desbloqueado)
        {
            imagenDisplay.sprite = entrada.imagen;
            textoNombre.text = entrada.nombre;
            textoDescripcion.text = entrada.descripcion;
        
            // Actualizar p�gina
            textoPagina.text = $"{indiceActual + 1}/{entradasActuales.Length}";
        }
        else
        {
            MostrarBloqueado();
        }
    }

    void MostrarBloqueado()
    {
        imagenDisplay.sprite = Resources.Load<Sprite>("bloqueado"); // Necesitar�s una imagen "bloqueado"
        textoNombre.text = "???";
        textoDescripcion.text = "A�n no has descubierto este elemento";
    }

    // M�todo para desbloquear entradas desde otros scripts
    public void DesbloquearEntrada(string categoria, int indice)
    {
        EntradaBestiario[] entradas = null;

        switch (categoria.ToLower())
        {
            case "enemigos": entradas = enemigos; break;
            case "armas": entradas = armas; break;
            case "jefes": entradas = jefes; break;
            case "coleccionables": entradas = coleccionables; break;
        }

        if (entradas != null && indice >= 0 && indice < entradas.Length)
        {
            entradas[indice].desbloqueado = true;

            // Si es la categor�a actual, actualizar visual
            if (categoria.ToLower() == categoriaActual)
                MostrarEntradaActual();
        }
    }
}