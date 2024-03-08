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
    public Button customProfileBtn;
    public Button resumeGameBtn;
    public Button quitGameBtn;

    // Give the UI Manager access to the Accessibility Manager to call colour switch functions.
    private static AccessibilityManager AccessibilityManager;
    private static GameManager GameManager;

    private void Awake()
    {
        AccessibilityManager = FindObjectOfType<AccessibilityManager>();
        GameManager = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        defaultClrBtn.onClick.AddListener(() => OnColorProfileChanged("Default"));
        protanopiaClrBtn.onClick.AddListener(() => OnColorProfileChanged("Protanopia"));
        deuteranopiaClrBtn.onClick.AddListener(() => OnColorProfileChanged("Deuteranopia"));
        tritanopiaClrBtn.onClick.AddListener(() => OnColorProfileChanged("Tritanopia"));
        highContrastClrBtn.onClick.AddListener(() => OnColorProfileChanged("HighContrast"));
        greyscaleClrBtn.onClick.AddListener(() => OnColorProfileChanged("Greyscale"));
        customProfileBtn.onClick.AddListener(() => CustomProfileColorSelection());
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

    void CustomProfileColorSelection()
    {
        // Load Custom Profile Colour Manager - Displays all colours for all current game objects.
        SceneManager.LoadScene("CustomProfileColorManager", LoadSceneMode.Additive);
    }
}

