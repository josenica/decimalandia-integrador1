using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class selección_única : MonoBehaviour
{
    [Header("Botón")]
     private GameObject button;

     [Header("TextMeshProUGUI")]
    private TextMeshProUGUI txt;
    [SerializeField] private TextMeshProUGUI acertar;
    [SerializeField] private TextMeshProUGUI ejercicio_txt;
    [SerializeField] public Button[] boton;

    [Header("guadar puntos")]
    int fallos1 = 0;
    public int correctos = 0;
    int posición = 0;

    [Header("respuestas y preguntas")]
     private double[] respuestascorrectas = {8.3,8.5,2.5};
     private string[] preguntas = { "3,5 + 4,8 = ?", "5,2 + 3,3 = ?", "1,2 + 1,3 = ?" };
     private string[] opciones =
      {
    "8.3", "5.2", "1.4",
    "4.3", "7.1", "8.5",
    "10", "2.5", "6.4"
      };


      [Header("cambio de escena")]
      [SerializeField] private GameObject boton_siguiente;

      [Header("animación")]
      [SerializeField] private Animator anim;

       /// <summary>
      /// Inicializa la actividad cargando la primera pregunta,
     /// configurando los botones y ocultando el botón de siguiente.
    /// </summary>
    
    void Start()
    {
        
        acertar.text =" ";
        boton_siguiente.SetActive(false);
        presentarejercicio();
        clickbotón();
    }

       /// <summary>
      /// Muestra la pregunta actual y asigna las opciones
     /// correspondientes a cada botón.
    /// </summary>
    void presentarejercicio()
    {
        ejercicio_txt.text = preguntas[posición];
         for (int i = 0; i < boton.Length; i++)
    {
        TextMeshProUGUI textoBoton =
        boton[i].GetComponentInChildren<TextMeshProUGUI>();

        textoBoton.text = opciones[(posición * 3) + i];
    }
    }

    void clickbotón()
    {
        for (int i = 0; i < boton.Length; i++)
        {

            boton[i].onClick.RemoveAllListeners();

            boton[i].onClick.AddListener(VerificarRespuesta);
        }
    }

       /// <summary>
      /// Comprueba si la respuesta seleccionada por el jugador es correcta.
     /// Actualiza los contadores de aciertos y errores, muestra un mensaje
    /// y avanza a la siguiente pregunta.
   /// </summary>
    void VerificarRespuesta()
    {
        
        button = EventSystem.current.currentSelectedGameObject;
       
        txt = button.GetComponentInChildren<TextMeshProUGUI>();
       

        double indice = respuestascorrectas[posición];

       
        Debug.Log("Respuesta correcta: " + indice);

        if (txt.text == indice.ToString())
        {
            correctos++;
            
            anim.SetTrigger("correct_anim");
            acertar.text = "correcto";
            int correctototal=correctos-fallos1;

           Cont_puntos.instance.acierto_act_unica=correctototal;
            

            Debug.Log("correctototal es igual a "+ correctototal);
        }
        else
        {
            fallos1++;
            anim.SetTrigger("fallos_anim");
            Debug.Log("Respuesta Incorrecta");

            acertar.text = "incorrecto";
            
        }
       Invoke(nameof(SiguientePregunta),1.2f);
    }
    void SiguientePregunta()
    {
        Debug.Log("se ejecuta");
        limpiartexto();
        Debug.Log("limpio texto");

         posición++;
         Debug.Log("Nueva posición: " + posición);
        if (posición < preguntas.Length)
        {
            Debug.Log("presenta ejercicio");
            presentarejercicio();

        }
        else
        {
            Debug.Log("Actividad terminada");

            boton_siguiente.SetActive(true);
        } 
    }
    void limpiartexto()
        {
            acertar.text = "";
        }
    
}