using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;


public class toggle_ejercicio : MonoBehaviour
{

    [Header("UI")]
    public Toggle[] mytoggle;
    public TextMeshProUGUI text;

    [Header("Mostrar ecuación")]
    public TextMeshProUGUI ecuacion;

    [Header("Ejercicios")]
    private string[] ecuaciones =
    {
        "¿Cuáles frutas debes seleccionar para obtener un total de 7.0 pesos?",
        "¿Cuáles frutas debes seleccionar para obtener un total de 10.0 pesos?",
        "¿Cuáles frutas debes seleccionar para obtener un total de 6.0 pesos?",
        "¿Cuáles frutas debes seleccionar para obtener un total de 5.5 pesos?"

    };
    private double[] respuestasCorrectas =
    {
        7,10,6,5.5
    };

    [Header("indice")]
    private int ejercicioActual = 0;

    [Header("Botón siguiente")]
    public GameObject siguiente;

    [Header("Guardar aciertos")]
    public int fallos1 = 0;
    public int correcto = 0;

    [Header("Guardar datos")]
    public TextMeshProUGUI izquierda;
    public TextMeshProUGUI derecha;
    public TextMeshProUGUI resultado;

    private bool respondido = false;

    [Header("animación")]
    [SerializeField] private Animator anim;

    void Start()
    {
        CargarEjercicio();
        foreach (Toggle t in mytoggle)
        {
            t.onValueChanged.AddListener(delegate { ActualizarSuma(); });
        }

        
    }

    void CargarEjercicio()
    {
        if (ejercicioActual >= ecuaciones.Length)
            return;

        ecuacion.text = ecuaciones[ejercicioActual];

        izquierda.text = "";
        derecha.text = "";
        resultado.text = "";
        text.text = "";

        foreach (Toggle t in mytoggle)
        {
            t.isOn = false;
            t.interactable = true;
        }

        respondido = false;
    }

    void ActualizarSuma()
    {
        double suma = 0;
        int seleccionados = 0;

        izquierda.text = "";
        derecha.text = "";
        resultado.text = "";
        text.text = "";

        foreach (Toggle t in mytoggle)
        {
            if (t.isOn)
            {
                seleccionados++;

                TextMeshProUGUI label = t.GetComponentInChildren<TextMeshProUGUI>();
              
                if (label != null)
                {
                    double numero = double.Parse(label.text);
                    suma += numero;

                    if (izquierda.text == "")
                        izquierda.text = numero.ToString();
                    else
                        derecha.text = numero.ToString();
                }
            }
        }

        resultado.text = suma.ToString("F1");

        block(seleccionados);

        if (seleccionados < 2)
        {
            respondido = false;
        }

        if (seleccionados == 2 && !respondido)
        {
            respondido = true;

            if (Math.Abs(suma - respuestasCorrectas[ejercicioActual]) < 0.01)
            {
                JuegoTerminado();
            }
            else
            {
                anim.SetTrigger("false_paj");
                fallos1++;
                text.text = "Incorrecto";
                Debug.Log("fallos = "+ fallos1);
            }
        }
    }

    void block(int seleccionados)
    {
        if (seleccionados >= 2)
        {
            foreach (Toggle t in mytoggle)
            {
                if (!t.isOn)
                    t.interactable = false;
            }
        }
        else
        {
            foreach (Toggle t in mytoggle)
            {
                t.interactable = true;
            }
        }
    }

    void JuegoTerminado()
    {
        text.text = "¡Correcto!";
        anim.SetTrigger("correct_paj");
        correcto++;
        Debug.Log("correcto ="+ correcto);

        ejercicioActual++;

        if (ejercicioActual < respuestasCorrectas.Length)
        {
            Invoke(nameof(CargarEjercicio), 1f);
        }
        else
        {
            correcto = respuestasCorrectas.Length - fallos1;

            Cont_puntos.instance.acierto_act_multiple = correcto;
            
            if (correcto < 0)
                correcto = 0;

            siguiente.SetActive(true);

            text.text = "¡Actividad completada!";
        }
    }
}
