using UnityEngine;


public class Cont_puntos:MonoBehaviour
{
    [Header("instancia")]
    public static Cont_puntos instance;

    [Header("Guardar datos usuario")]
    public string playerName;
    public int playerScore;

    [Header("Guardar aciertos")]
    public int acierto_act_multiple;
    public int acierto_act_unica;
    public int acierto_act_arrastre;

    [Header("Modo de juego")]
    public bool modoLibre;

 

    void Awake()
    {
        if (instance == null)
        {
            instance=this;
            DontDestroyOnLoad(gameObject);//no se destruye al cargar escena
        }
        else
        {
            Destroy(gameObject);
        }}
          // 🔹 Método para agregar puntos desde cualquier escena
    public void AgregarPuntos(int puntos)
    {
        Debug.Log("actividad unica = "+ acierto_act_unica);
        Debug.Log("actividad multiple = "+ acierto_act_multiple);
        Debug.Log("actividad arrastre = "+ acierto_act_arrastre);

        int sumaejericios = acierto_act_unica+acierto_act_multiple+acierto_act_arrastre;
        playerScore = sumaejericios * 5;
        Debug.Log("Puntos totales: " + playerScore);
    }

    // 🔹 Método para establecer el nombre desde otro script
    public void EstablecerNombre(string nombre)
    {
        playerName = nombre;
        Debug.Log("Nombre guardado: " + playerName);
    }

  
    }
