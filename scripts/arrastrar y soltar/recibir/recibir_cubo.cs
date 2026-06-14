using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler
{
    [Header("UI valores cajas")]
    public TextMeshProUGUI valor_esperado;

    [Header("guardar datos")]
    public  static int correcto=0;
    public static int fallos1=0;

    [Header("botón")]
    public  static GameObject botón;
    [SerializeField]private GameObject botón_referencia;
 
    public void OnDrop(PointerEventData eventData)
    {
        botón=botón_referencia;
        Debug.Log("Drop detectado");
        DragImage imagen = eventData.pointerDrag.GetComponent<DragImage>();
        TextMeshProUGUI textoCubo =
        imagen.GetComponentInChildren<TextMeshProUGUI>();

        if (imagen != null)
        {
            imagen.transform.SetParent(transform);
             Debug.Log("Padre actual: " + transform.parent.name);
            imagen.transform.localPosition = Vector3.zero;
        }
        if (textoCubo.text == valor_esperado.text) {
            Debug.Log("correcto");
            correcto++;
            Debug.Log("acierto = "+ correcto);
            if (correcto == 2)
            {
                Juegoterminado();
            }
        }
        else
        {
            Debug.Log("no coincide");
            fallos1++;
            if (fallos1 == 2)
            {
                fallos1=0;
                correcto=0;
                Juegoterminado();
            }
            Debug.Log("fallos =" + fallos1);

            imagen.VolverAlPadreOriginal();
        }
    }
    void Juegoterminado()
    {
        int total=correcto-fallos1;

        Cont_puntos.instance.acierto_act_arrastre = total;
        

        botón.SetActive(true);
    }
}