using UnityEngine;
using UnityEngine.EventSystems;

public class CluesCanvasManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas canvas;
    public RectTransform canvasRect;

    GameObject dragTestObj;
    Vector3 dragOffset;


    public static CluesCanvasManager Instance;


    void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ClickTest()
    {
        print("Hello World!");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RaycastResult obj = eventData.pointerCurrentRaycast;
        if (obj.gameObject.CompareTag("Draggable"))
        {
            dragTestObj = obj.gameObject;

            Vector3 pos = canvas.transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);
            pos.z = 0;

            dragOffset = dragTestObj.transform.localPosition - pos;
            dragTestObj.transform.SetAsLastSibling();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragTestObj = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragTestObj != null)
        {
            Vector3 pos = canvas.transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition) + dragOffset;
            pos.z = 0;

            pos.x = Mathf.Clamp(pos.x, canvasRect.rect.xMin, canvasRect.rect.xMax);
            pos.y = Mathf.Clamp(pos.y, canvasRect.rect.yMin, canvasRect.rect.yMax);

            dragTestObj.transform.localPosition = pos;
        }
    }
}
