using UnityEngine;

public class InteractionTest : MonoBehaviour
{
    public void btn_addclue()
    {
        CluesManager.Instance.NewClue("Test Clue", "This is a test!");
        CluesManager.Instance.NewClue("Hello World!", "");
        CluesManager.Instance.NewClue("A bit of a longer one!", "Also quite a long description, yeah sure i couldve used lorem ipsum but typing this out isnt really takeing that long. just writing untill i go, wait what am i talking about, y'know? anyway thats enough.");
    }
}

