using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;

// Clase que contiene toda la información de cada entrada del bestiario
[System.Serializable]
public class EntradaBestiario
{
    public Sprite imagen;       // Imagen del elemento
    public float escalaImagen = 1f; // Valor por defecto
    public LocalizedString nombre;       // Nombre a mostrar
    public LocalizedString descripcion;  // Descripción detallada
    public bool desbloqueado;   // Si está disponible para ver

    [Header("Enlace a itch.io")]
    public string urlItchIO; // Añade este campo para guardar la URL
}

public class BestiarioManager : MonoBehaviour
{
    [Header("UI Principal")]
    public GameObject bestiarioCanvas;
    public Image imagenDisplay;
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoDescripcion;

    [Header("Nombres Localizados de Categorías")]
    public LocalizedString categoriaEnemigos;
    public LocalizedString categoriaArmas;
    public LocalizedString categoriaJefes;
    public LocalizedString categoriaColeccionables;

    [Header("Contenido Organizado")]
    public EntradaBestiario[] enemigos;
    public EntradaBestiario[] armas;
    public EntradaBestiario[] jefes;
    public EntradaBestiario[] coleccionables;

    [Header("Botones Categorías")] 
    public Button botonEnemigos;
    public Button botonArmas;
    public Button botonJefes;
    public Button botonColeccionables;

    [Header("Botones Navegación")]
    public Button botonAnterior;
    public Button botonSiguiente;
    public TextMeshProUGUI textoPagina;
    public TextMeshProUGUI textoCategoria; // Muestra la categoría actual

    [Header("Configuración de Bloqueado")]
    public Sprite imagenBloqueado; // Arrastra tu imagen de "bloqueado" desde el Inspector

    [Header("Botón Itch.io")]
    public Button botonItchIO;
    private string urlActual; // Para guardar la URL del coleccionable actual

    // Variables de control
    private EntradaBestiario[] entradasActuales;
    private int indiceActual = 0;
    private string categoriaActual = "";

    void Start()
    {
        ConfigurarBotones();
        CerrarBestiario();
        CargarDesbloqueos(); // Nueva línea añadida

        // Configura el botón existente
        botonItchIO.onClick.AddListener(AbrirEnlaceItch);
        botonItchIO.gameObject.SetActive(false); // Ocultar inicialmente
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
        // Aquí puedes cargar datos guardados o inicializar desbloqueos
        // Ejemplo: desbloquear el primer elemento de cada categoría
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

        // Usar LocalizedString en lugar de texto hardcoded
        switch (categoria.ToLower())
        {
            case "enemigos":
                categoriaEnemigos.StringChanged += (valor) => textoCategoria.text = valor;
                categoriaEnemigos.RefreshString(); // Llama a la actualización
                break;

            case "armas":
                categoriaArmas.StringChanged += (valor) => textoCategoria.text = valor;
                categoriaArmas.RefreshString();
                break;

            case "jefes":
                categoriaJefes.StringChanged += (valor) => textoCategoria.text = valor;
                categoriaJefes.RefreshString();
                break;

            case "coleccionables":
                categoriaColeccionables.StringChanged += (valor) => textoCategoria.text = valor;
                categoriaColeccionables.RefreshString();
                break;
        }

        MostrarEntradaActual();
    }

    void CambiarEntrada(int cambio)
    {
        if (entradasActuales == null || entradasActuales.Length == 0) return;

        // Buscar próxima entrada desbloqueada
        int intentos = 0;
        do
        {
            indiceActual += cambio;

            // Navegación circular
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

        // Mostrar/ocultar botón solo para coleccionables
        bool esColeccionable = categoriaActual.Equals("COLECCIONABLES", System.StringComparison.OrdinalIgnoreCase);
        botonItchIO.gameObject.SetActive(entrada.desbloqueado && esColeccionable && !string.IsNullOrEmpty(entrada.urlItchIO));

        if (entrada.desbloqueado)
        {
            imagenDisplay.sprite = entrada.imagen;

            // Aplicar escala diferente para coleccionables
            float escala = entrada.escalaImagen;
            imagenDisplay.transform.localScale = new Vector3(escala, escala, escala);

            textoNombre.text = entrada.nombre.GetLocalizedString();
            textoDescripcion.text = entrada.descripcion.GetLocalizedString();
            textoPagina.text = $"{indiceActual + 1}/{entradasActuales.Length}";

            // Guardar URL si es coleccionable
            if (esColeccionable)
            {
                urlActual = entrada.urlItchIO;
            }
        }
        else
        {
            imagenDisplay.transform.localScale = Vector3.one;
            MostrarBloqueado();
        }
    }

    void MostrarBloqueado()
    {
        imagenDisplay.sprite = imagenBloqueado; // Necesitarás una imagen "bloqueado"
        textoNombre.text = "???";
        textoDescripcion.text = "???";
    }

    // Método para desbloquear entradas desde otros scripts
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

            // Si es la categoría actual, actualizar visual
            if (categoria.ToLower() == categoriaActual)
                MostrarEntradaActual();
        }
    }
    public void CargarDesbloqueos()
    {
        // Para cada categoría, verificar qué entradas están desbloqueadas
        ProcesarCategoria(enemigos, "enemigos");
        ProcesarCategoria(armas, "armas");
        ProcesarCategoria(jefes, "jefes");
        ProcesarCategoria(coleccionables, "coleccionables");

        // Si estamos mostrando una categoría, actualizar la vista
        if (!string.IsNullOrEmpty(categoriaActual))
        {
            MostrarEntradaActual();
        }
    }

    void ProcesarCategoria(EntradaBestiario[] entradas, string categoria)
    {
        for (int i = 0; i < entradas.Length; i++)
        {
            bool estaDesbloqueado = NivelManager.EstaDesbloqueado(categoria, i);
            Debug.Log($"Verificando: {categoria}[{i}] = {entradas[i].nombre} → Desbloqueado: {estaDesbloqueado}");

            if (estaDesbloqueado)
            {
                entradas[i].desbloqueado = true;
            }
        }
    }

    void AbrirEnlaceItch()
    {
        if (!string.IsNullOrEmpty(urlActual))
        {
            // Abre el enlace en el navegador
            Application.OpenURL(urlActual);
        }
        else
        {
            Debug.LogWarning("No hay URL configurada para este coleccionable");
        }
    }
}