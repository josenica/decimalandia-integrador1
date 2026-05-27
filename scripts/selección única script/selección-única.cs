using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class selección_única : MonoBehaviour
{
    [Header("Botón")]
     private GameObject button;
    private TextMeshProUGUI txt;
    [SerializeField] private TextMeshProUGUI acertar;
    [SerializeField] private TextMeshProUGUI ejercicio_txt;
    [SerializeField] public Button[] boton;

    [Header("puntos")]
    int fallos1 = 0;
    int correctos = 0;
    int posición = 0;

    [Header("respuestas y preguntas")]
     private double[] respuestascorrectas = {8.3,8.5,2.5};
    [SerializeField] private string[] preguntas = { "3,5 + 4,8", "2,5 + 1,8", "4,5 + 5,5" };
     private string[] opciones =
      {
    "8.3", "5.2", "1.4",
    "4.3", "7.1", "8.5",
    "10", "2.5", "6.4"
      };
      [Header("cambio de escena")]
      [SerializeField] private GameObject boton_siguiente;

    void Start()
    {
        acertar.text =" ";
        boton_siguiente.SetActive(false);

        Debug.Log("posición actual"+posición);
        presentarejercicio();
        clickbotón();
    }

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

    void VerificarRespuesta()
    {
        
        button = EventSystem.current.currentSelectedGameObject;
       
        txt = button.GetComponentInChildren<TextMeshProUGUI>();
       

        double indice = respuestascorrectas[posición];

       
        Debug.Log("Respuesta correcta: " + indice);

        if (txt.text == indice.ToString())
        {
            Debug.Log("entro al if correcto");
            correctos++;

            Debug.Log("¡Ganaste! 🎉 La respuesta es correcta.");

            acertar.text = "correcto";

            Cont_puntos.instance.AgregarPuntos(correctos);
            Debug.Log("posición actual"+posición);
        }
        else
        {
            Debug.Log("entro al else");
            fallos1++;

            Debug.Log("Respuesta Incorrecta");

            acertar.text = "incorrecto";
        }

        Invoke("limpiartexto", 0.8f);

         posición++;
        Debug.Log("posición actual"+posición);

        if (posición < preguntas.Length)
        {
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