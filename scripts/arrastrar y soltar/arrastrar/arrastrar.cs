using UnityEngine;
using UnityEngine.EventSystems;

public class DragImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    private RectTransform rectTransform;
    private Transform parentOriginal;
    private CanvasGroup canvasGroup;
     private Canvas canvas;

     

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
         canvasGroup.blocksRaycasts = false;
      
        //obtiene el padre del gameobject dentro de la jerarquía
        parentOriginal = transform.parent;
       

        transform.SetParent(canvas.transform);
    
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //convierte la posición del ratón a coordenadas locales del canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle
        (
            canvas.transform as RectTransform,

            //posición del mouse
            eventData.position,
            //la cámara utilizada por el canvas
            canvas.worldCamera,

            //aquí se guarda el resultado de la conversión
            out Vector2 localPoint);

            //Hace que el objeto se coloque exactamente en esa posición dentro del Canvas
        rectTransform.localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
         canvasGroup.blocksRaycasts = true;

        if (transform.parent == canvas.transform)
        {
            transform.SetParent(parentOriginal);
        }
    }
    public void VolverAlPadreOriginal()
{
    transform.SetParent(parentOriginal);
    transform.localPosition = Vector3.zero;
}
}