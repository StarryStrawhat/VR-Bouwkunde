using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CluesManager : MonoBehaviour
{

    [SerializeField] private GameObject CluePrefab;
    [SerializeField] private Sprite defaultImage;

    private int _idTracker = 0;
    public List<Clue> clues = new();
    public List<Tuple<int, int>> Connections = new();

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

    public Clue NewClue(string title, string description)
    {
        GameObject clueObj = Instantiate(CluePrefab, CluesCanvasManager.Instance.canvas.gameObject.transform);
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

    public void ConnectClues(int clue1, int clue2)
    {
        Instance.Connections.Add(new(clue1, clue2));
    }

    public Clue[] GetConnections(int clueID)
    {
        print(Instance.Connections.FindAll(x => x.Item1 == clueID));

        return null;
    }
}

