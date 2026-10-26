using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CluesCanvasManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public enum DragMode
    {
        Dragging,
        Connecting
    }

    public DragMode mode = DragMode.Dragging;

    public Canvas canvas;
    public RectTransform canvasRect;
    public Image dragBtnImage;
    public Image connectBtnImage;

    public GameObject objectsGroup;


    GameObject dragTestObj;
    Vector3 dragOffset;


    private Connection connecting;

    public static CluesCanvasManager Instance;

    void Start()
    {
        if (Instance == null)
            Instance = this;

        mode = DragMode.Dragging;
        dragBtnImage.color = Color.green;
        connectBtnImage.color = Color.white;
    }

    public void ClickTest()
    {
        print("Hello World!");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RaycastResult obj = eventData.pointerCurrentRaycast;

        if (mode == DragMode.Dragging)
        {
            if (obj.gameObject.CompareTag("Draggable"))
            {
                dragTestObj = obj.gameObject;

                Vector3 pos = canvas.transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);
                pos.z = 0;

                dragOffset = dragTestObj.transform.localPosition - pos;
                dragTestObj.transform.SetAsLastSibling();
            }
        } else if (mode == DragMode.Connecting)
        {
            if (obj.gameObject.TryGetComponent(out Clue clue))
            {
                connecting = CluesManager.Instance.NewConnection();
                connecting.clue1 = clue;

                connecting.transform.SetAsFirstSibling();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = canvas.transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);
        pos.z = 0;

        if (mode == DragMode.Dragging)
        {
            if (dragTestObj != null && eventData.pointerCurrentRaycast.isValid)
            {
                pos.x = Mathf.Clamp(pos.x + dragOffset.x, canvasRect.rect.xMin, canvasRect.rect.xMax);
                pos.y = Mathf.Clamp(pos.y + dragOffset.y, canvasRect.rect.yMin, canvasRect.rect.yMax);

                dragTestObj.transform.localPosition = pos;

                if (dragTestObj.TryGetComponent(out Clue clue))
                {
                    List<Connection> connections = CluesManager.Instance.GetConnections(clue.ID);
                    if (connections.Count > 0)
                    {
                        foreach (Connection connection in connections)
                        {
                            connection.UpdateLine(connection.clue1, connection.clue2);
                        }
                    }
                }
            }
        } else if (mode == DragMode.Connecting)
        {
            if (connecting != null && connecting.clue1 != null && eventData.pointerCurrentRaycast.isValid)
            {
                connecting.UpdateLine(connecting.clue1, pos);
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (mode == DragMode.Dragging)
        {
            dragTestObj = null;
        } else if (mode == DragMode.Connecting)
        {
            if (connecting == null)
                return;

            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out Clue clue))
            {
                bool AreConnected(Clue a, Clue b) =>
                    CluesManager.Instance.Connections.Exists(con =>
                        ( con.clue1 == a && con.clue2 == b ) ||
                        ( con.clue1 == b && con.clue2 == a ));

                if (connecting.clue1 != clue && !AreConnected(connecting.clue1, clue))
                {
                    connecting.clue2 = clue;

                    connecting.UpdateLine(connecting.clue1, clue);
                    CluesManager.Instance.Connections.Add(connecting);


                    connecting = null;
                    return;
                }
            }

            Destroy(connecting.gameObject);
            connecting = null;
        }
    }
    public void BtnModeConnect()
    {
        mode = DragMode.Connecting;
        connectBtnImage.color = Color.green;
        dragBtnImage.color = Color.white;
    }
    public void BtnModeDrag()
    {
        mode = DragMode.Dragging;
        dragBtnImage.color = Color.green;
        connectBtnImage.color = Color.white;
    }
}
