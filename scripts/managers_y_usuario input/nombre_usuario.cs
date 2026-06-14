using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class nombre_usuario : MonoBehaviour
{
    public TMP_InputField nombre_ingresado;
    public Button verificarnombre;

    private void Start()
    {
        // Al iniciar, el botón estará deshabilitado si no hay texto
        verificarnombre.interactable = false;
    }

    public void VerificarNombre()
    {
        verificarnombre.interactable =
            !string.IsNullOrWhiteSpace(nombre_ingresado.text);
    }

    public void MostrarNombre()
    {
        string nombre = nombre_ingresado.text;

        Debug.Log("Nombre ingresado: " + nombre);

        Cont_puntos.instance.EstablecerNombre(nombre);
    }
}