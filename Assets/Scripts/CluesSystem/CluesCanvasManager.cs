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

    // the clues canvas
    public Canvas canvas;
    public RectTransform canvasRect;
    // for setting colors
    public Image dragBtnImage;
    public Image connectBtnImage;

    // for where the clues and lines go
    public GameObject objectsGroup;

    // drag variables
    GameObject dragTestObj;
    Vector3 dragOffset;


    private Connection connecting;

    public static CluesCanvasManager Instance;

    void Start()
    {
        // make sure theres only one instance that gets used. (in case there's another canvasmanager somewhere)
        if (Instance == null)
            Instance = this;

        // defaults
        mode = DragMode.Dragging;
        dragBtnImage.color = Color.green;
        connectBtnImage.color = Color.white;
    }

    public void ClickTest()
    {
        print("Hello World!");
    }

    // called when mousedown and movement on canvas, the start of a drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        RaycastResult obj = eventData.pointerCurrentRaycast;

        if (mode == DragMode.Dragging)
        {
            //  check if targeted object has draggable tag
            if (obj.gameObject.CompareTag("Draggable"))
            {
                // start dragging
                dragTestObj = obj.gameObject;

                // get mouse position and translate it to canvas position. (due to the canvas being scaled)
                Vector3 pos = canvas.transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);
                pos.z = 0;

                // sav ean offset so the dragging object doesnt go to the center of the cursor.
                dragOffset = dragTestObj.transform.localPosition - pos;
                // op top of the rest visually
                dragTestObj.transform.SetAsLastSibling();
            }
        } else if (mode == DragMode.Connecting)
        {
            // if clicking/dragging on a clue object
            if (obj.gameObject.TryGetComponent(out Clue clue))
            {
                // start a new connection
                connecting = CluesManager.Instance.NewConnection();
                connecting.clue1 = clue;

                // connection line at the very bottom.
                connecting.transform.SetAsFirstSibling();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // while already dragging.

        // get mouse position and translate it to canvas position. (due to the canvas being scaled)
        Vector3 pos = canvas.transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);
        pos.z = 0;

        if (mode == DragMode.Dragging)
        {
            // if we are dragging a clue (and cursor is on the canvas)
            if (dragTestObj != null && eventData.pointerCurrentRaycast.isValid)
            {
                // keep the clue inside the canvas based on its center.
                // (this can still allow the note to get off screen, i'd need to use it's rect as well to keep it all inside the canvas)

                pos.x = Mathf.Clamp(pos.x + dragOffset.x, canvasRect.rect.xMin, canvasRect.rect.xMax);
                pos.y = Mathf.Clamp(pos.y + dragOffset.y, canvasRect.rect.yMin, canvasRect.rect.yMax);

                // move the clue getting dragged to the mouse position (with that previous offset)
                dragTestObj.transform.localPosition = pos;

                // update all connections lines
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
            // if we are connecting clues (and cursor is on the canvas)
            if (connecting != null && connecting.clue1 != null && eventData.pointerCurrentRaycast.isValid)
            {
                // update the positioning
                connecting.UpdateLine(connecting.clue1, pos);
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // stop dragging 

        if (mode == DragMode.Dragging) // if we're dragging
        {
            dragTestObj = null;
        } else if (mode == DragMode.Connecting) // if we're connecting
        {
            // if there is no connection happening, stop.
            if (connecting == null)
                return;

            // try to get the clue under where we stopped dragging
            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out Clue clue))
            {
                // quick funciton to check if the connection we're trying to make already exists.
                bool AreConnected(Clue a, Clue b) =>
                    CluesManager.Instance.Connections.Exists(con =>
                        ( con.clue1 == a && con.clue2 == b ) ||
                        ( con.clue1 == b && con.clue2 == a ));

                // check if we're not connecting to self and the custom function above
                if (connecting.clue1 != clue && !AreConnected(connecting.clue1, clue))
                {
                    // set the other clue in th econnection, then update the line and add the connection to a list.
                    connecting.clue2 = clue;

                    connecting.UpdateLine(connecting.clue1, clue);
                    CluesManager.Instance.Connections.Add(connecting);


                    connecting = null;
                    return;
                }
            }

            // if the line wasnt connected to anything else or to self then we just destroy the connection since we cant connect to nothing (or self)

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
