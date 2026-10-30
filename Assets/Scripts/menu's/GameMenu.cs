using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName;
    [SerializeField] private GameObject SettingsUI;

    public void Play()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        //controleerd op SettingsUI bestaat, zo ja opent hij SettingsUI
        if (SettingsUI != null)
        {
            SettingsUI.SetActive(true);
        }
        Debug.Log("Settings opened");
    }
}
    