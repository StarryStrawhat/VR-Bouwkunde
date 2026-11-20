using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CluesManager : MonoBehaviour
{
    // holding and making clues + connections

    [SerializeField] private GameObject CluePrefab;
    [SerializeField] private GameObject ConnectionPrefab;

    [SerializeField] private Sprite defaultImage;

    private int _idTracker = 0;
    public List<Clue> clues = new();
    public List<Connection> Connections = new();

    public static CluesManager Instance;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public int NextID()
    {
        return _idTracker++;
    }
    public Connection NewConnection()
    {
        // creating a connection from a prefab

        GameObject connectionObj = Instantiate(ConnectionPrefab, CluesCanvasManager.Instance.objectsGroup.transform);
        Connection connection = connectionObj.AddComponent<Connection>();

        connection.rectTransform = connection.GetComponent<RectTransform>();
        connection.image = connection.GetComponent<Image>();


        return connection;
    }
    public Clue NewClue(string title, string description)
    {
        // creating a new clue from a prefab using a title and description (mostly used for temp testing)
        GameObject clueObj = Instantiate(CluePrefab, CluesCanvasManager.Instance.objectsGroup.transform);
        Clue clue = clueObj.AddComponent<Clue>();

        Transform titleObj = clueObj.transform.Find("tmpTitle");
        Transform descObj = clueObj.transform.Find("tmpDescription");

        clue.tmpTitle = titleObj.GetComponent<TextMeshProUGUI>();
        clue.tmpDescription = descObj.GetComponent<TextMeshProUGUI>();

        clue.img = clueObj.GetComponent<Image>();

        clue.ID = NextID();
        clue.Title = title;
        clue.Description = description;
        clue.Image = defaultImage;


        clues.Add(clue);
        return clue;
    }

    // can probably go since it's not used anywhere as far as i know.
    public void ConnectClues(Clue clue1, Clue clue2)
    {
        Instance.Connections.Add(new() { clue1 = clue1, clue2 = clue2 });
    }


    public List<Connection> GetConnections(int clueID)
    {
        List<Connection> connections = Instance.Connections.FindAll(x => x.clue1.ID == clueID || x.clue2.ID == clueID);

        return connections;
    }

    public Clue GetClue(int id)
    {
        return clues.Find(clue => clue.ID == id);
    }
    public Clue GetClue(string title)
    {
        return clues.Find(clue => clue.Title == title);
    }

}