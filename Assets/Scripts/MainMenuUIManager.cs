using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    // Buttons For Each Colourblind Mode
    public Button startGameBtn;
    public Button quitGameBtn;

    void Start()
    {
        startGameBtn.onClick.AddListener(() => SceneManager.LoadScene("Level_01"));
        quitGameBtn.onClick.AddListener(() => Application.Quit());
    }

}

