using UnityEngine;
using UnityEngine.UI;

public class Connection : MonoBehaviour
{
    public Clue clue1;
    public Clue clue2;
    public RectTransform rectTransform;
    public Image image;


    public void UpdateConnections()
    {
        foreach (Connection connection in CluesManager.Instance.Connections)
        {
            UpdateLine(connection.clue1, connection.clue2);
        }
    }

    // overloads to make it easier to use sometimes. from vector to vector, clue to clue, vector to clue, it all works now.
    public void UpdateLine(Vector3 from, Vector3 to)
    {
        UpdateLinePrivate(from, to);
    }
    public void UpdateLine(Clue from, Vector3 to)
    {
        UpdateLinePrivate(from.transform.localPosition, to);
    }
    public void UpdateLine(Vector3 from, Clue to)
    {
        UpdateLinePrivate(from, to.transform.localPosition);
    }
    public void UpdateLine(Clue from, Clue to)
    {
        UpdateLinePrivate(from.transform.localPosition, to.transform.localPosition);
    }

    private void UpdateLinePrivate(Vector3 from, Vector3 to)
    {
        image.transform.localPosition = from;

        Vector3 diff = to - from;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0, 0, angle);
        image.transform.localRotation = rot;


        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Vector3.Distance(from, to));
    }
}

