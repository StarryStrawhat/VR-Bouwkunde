using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clue : MonoBehaviour
{
    public TextMeshProUGUI tmpTitle;
    public TextMeshProUGUI tmpDescription;
    public Image img;

    public int ID;

    public string Title
    {
        set { tmpTitle.text = value; }
        get { return tmpTitle.text; }
    }
    public string Description
    {
        set { tmpDescription.text = value; }
        get { return tmpDescription.text; }
    }
    public Sprite Image
    {
        set { img.sprite = value; }
        get { return img.sprite; }
    }

}

