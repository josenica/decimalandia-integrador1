using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ejercicio_1 : MonoBehaviour
{
    [Header("UI")]
    public Toggle[] mytoggle;
    public TextMeshProUGUI text;

    [Header("respuesta")]
    public double respuestacorrecta = 6.0;

    [Header("cambio de canvas")]
    public GameObject siguiente;
    public GameObject Canvas_1ejercicio;
    public GameObject Canvas_2ejercicio;

    [Header("guardar aciertos")]
    int fallos1 = 0;
    int correcto = 0;

    [Header("guardar datos")]
    public TextMeshProUGUI izquierda;
    public TextMeshProUGUI derecha;
    public TextMeshProUGUI resultado;

    private bool respondido = false;

    void Start()
    {
        foreach (Toggle t in mytoggle)
        {
            t.onValueChanged.AddListener(delegate { ActualizarSuma(); });
        }

        ActualizarSuma();
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

            if (Math.Abs(suma - respuestacorrecta) < 0.01)
            {
                JuegoTerminado();
            }
            else
            {
                fallos1++;
                text.text = "Incorrecto";
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
        correcto = 5 - fallos1;

        if (correcto < 0)
            correcto = 0;

        text.text = "¡Correcto!";
        siguiente.SetActive(true);

        Cont_puntos.instance.AgregarPuntos(correcto);
    }
     public void siguiente1()
    {
        Canvas_1ejercicio.SetActive(false);
        Canvas_2ejercicio.SetActive(true);
    }
}