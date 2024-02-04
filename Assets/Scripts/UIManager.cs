using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Buttons For Each Colourblind Mode
    public Button defaultClrBtn;
    public Button protanopiaClrBtn;
    public Button deuteranopiaClrBtn;
    public Button tritanopiaClrBtn;
    public Button highContrastClrBtn;
    public Button greyscaleClrBtn;
    public Toggle useCustomProfile;
    public Button resumeGameBtn;
    public Button quitGameBtn;

    // Give the UI Manager access to the Accessibility Manager to call colour switch functions.
    private AccessibilityManager AccessibilityManager;
    private GameManager GameManager;

    private void Awake()
    {
        AccessibilityManager = GameObject.Find("_AccessibilityManager").GetComponent<AccessibilityManager>();
        GameManager = GameObject.Find("_GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
        defaultClrBtn.onClick.AddListener(() => OnColorProfileChanged("Default"));
        protanopiaClrBtn.onClick.AddListener(() => OnColorProfileChanged("Protanopia"));
        deuteranopiaClrBtn.onClick.AddListener(() => OnColorProfileChanged("Deuteranopia"));
        tritanopiaClrBtn.onClick.AddListener(() => OnColorProfileChanged("Tritanopia"));
        highContrastClrBtn.onClick.AddListener(() => OnColorProfileChanged("HighContrast"));
        greyscaleClrBtn.onClick.AddListener(() => OnColorProfileChanged("Greyscale"));
        useCustomProfile.onValueChanged.AddListener(delegate { ToggleCustomProfile(useCustomProfile.isOn); });
        resumeGameBtn.onClick.AddListener(() => GameManager.ResumeGame());
        quitGameBtn.onClick.AddListener(() => GameManager.QuitGame());
    }

    public void OnColorProfileChanged(string selectedProfile)
    {
        Debug.Log("Old Colour Profile: " + PlayerPrefs.GetString("SelectedColorProfile"));
        AccessibilityManager.SwitchColorProfile(selectedProfile);
        PlayerPrefs.SetString("SelectedColorProfile", selectedProfile);
        PlayerPrefs.Save();
        Debug.Log("New Colour Profile: " + selectedProfile);
        GameManager.UpdateGameTiles();
    }

    public bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == sceneName && scene.isLoaded)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsSceneActive(string sceneName)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        return activeScene.name == sceneName;
    }

    void ToggleCustomProfile(bool useCustomProfile)
    {
        if (useCustomProfile)
        {
            OnColorProfileChanged("Custom");
            AccessibilityManager.ApplyColorProfile(AccessibilityManager.customProfile);
        }
        else
        {
            OnColorProfileChanged("Default");
            AccessibilityManager.ApplyColorProfile(AccessibilityManager.defaultProfile);
        }
    }
}

