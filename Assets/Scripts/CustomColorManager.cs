using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomColorManager : MonoBehaviour
{
    // These need to be public so that the colour picker is able to access them
    public Image gameWallClr;
    public Image startPointClr;
    public Image endPointClr;
    public Image highHealthClr;
    public Image lowHealthClr;
    public Image turretClr;
    public Image turretBulletClr;
    public Image enemyRouteClr;
    public Image enemyClr;
    // Public so that colour picker buttons can be assigned
    public Button gameWallClrBtn;
    public Button startPointClrBtn;
    public Button endPointClrBtn;
    public Button highHealthClrBtn;
    public Button lowHealthClrBtn;
    public Button turretClrBtn;
    public Button turretBulletClrBtn;
    public Button enemyRouteClrBtn;
    public Button enemyClrBtn;

    public GameObject color;
    public ColorProperty currentColorProperty;

    public Button saveColorSelection;

    private static AccessibilityManager AccessibilityManager;
    private static GameManager GameManager;
    private static ColorPickerManager ColorPickerManager;

    private void Awake()
    {
        AccessibilityManager = FindObjectOfType<AccessibilityManager>();
        GameManager = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        gameWallClr.color = AccessibilityManager.GetCurrentProfile().gameWallClr;
        startPointClr.color = AccessibilityManager.GetCurrentProfile().startPointClr;
        endPointClr.color = AccessibilityManager.GetCurrentProfile().endPointClr;
        highHealthClr.color = AccessibilityManager.GetCurrentProfile().highHealthClr;
        lowHealthClr.color = AccessibilityManager.GetCurrentProfile().lowHealthClr;
        turretClr.color = AccessibilityManager.GetCurrentProfile().turretClr;
        turretBulletClr.color = AccessibilityManager.GetCurrentProfile().turretBulletClr;
        enemyRouteClr.color = AccessibilityManager.GetCurrentProfile().enemyRouteClr;
        enemyClr.color = AccessibilityManager.GetCurrentProfile().enemyClr;

        gameWallClrBtn.onClick.AddListener(() => ToggleColorPicker(ColorProperty.GameWall));
        startPointClrBtn.onClick.AddListener(() => ToggleColorPicker(ColorProperty.StartPoint));
        endPointClrBtn.onClick.AddListener(() => ToggleColorPicker(ColorProperty.EndPoint));
        highHealthClrBtn.onClick.AddListener(() => ToggleColorPicker(ColorProperty.HighHealth));
        lowHealthClrBtn.onClick.AddListener(() => ToggleColorPicker(ColorProperty.LowHealth));
        turretClrBtn.onClick.AddListener(() => ToggleColorPicker(ColorProperty.Turret));
        turretBulletClrBtn.onClick.AddListener(() => ToggleColorPicker(ColorProperty.TurretBullet));
        enemyRouteClrBtn.onClick.AddListener(() => ToggleColorPicker(ColorProperty.EnemyRoute));
        enemyClrBtn.onClick.AddListener(() => ToggleColorPicker(ColorProperty.Enemy));

        saveColorSelection.onClick.AddListener(() => SaveColorSelection());

    }

    public void ToggleColorPicker(ColorProperty colorProperty)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("ColorPicker", LoadSceneMode.Additive);

        // Start the coroutine to wait for the scene to fully load
        StartCoroutine(WaitForSceneLoad(asyncLoad, colorProperty));
        ColorPickerManager = FindObjectOfType<ColorPickerManager>();
    }

    public ColorProperty GetSelectedColour(ColorProperty currentColorProperty)
    {
        return currentColorProperty;
    }


    private IEnumerator WaitForSceneLoad(AsyncOperation asyncLoad, ColorProperty colorProperty)
    {
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Now the scene is fully loaded, find the ColorPickerManager in the newly loaded scene
        ColorPickerManager colorPickerManager = FindObjectOfType<ColorPickerManager>();

        if (colorPickerManager != null)
        {
            // Set the color property to be edited
            colorPickerManager.currentColorProperty = colorProperty;
            colorPickerManager.InitializeValues(colorProperty);
        }
        else
        {
            Debug.LogError("ColorPickerManager not found in the loaded scene.");
        }
    }


    public void SaveColorSelection()
    {
        GameManager.UpdateGameTiles();
        SceneManager.UnloadSceneAsync("CustomProfileColorManager");
    }

    public void UpdateColourBlocks(ColorProperty currentColorProperty)
    {
        Debug.Log("Current Color Property: " + currentColorProperty);
        switch (currentColorProperty)
        { 
            case ColorProperty.GameWall:
                gameWallClr.color = AccessibilityManager.GetCurrentProfile().gameWallClr;
                break;
            case ColorProperty.StartPoint:
                startPointClr.color = AccessibilityManager.GetCurrentProfile().startPointClr;
                break;
            case ColorProperty.EndPoint:
                endPointClr.color = AccessibilityManager.GetCurrentProfile().endPointClr;
                break;
            case ColorProperty.HighHealth:
                highHealthClr.color = AccessibilityManager.GetCurrentProfile().highHealthClr;
                break;
            case ColorProperty.LowHealth:
                lowHealthClr.color = AccessibilityManager.GetCurrentProfile().lowHealthClr;
                break;
            case ColorProperty.Turret:
                turretClr.color = AccessibilityManager.GetCurrentProfile().turretClr;
                break;
            case ColorProperty.TurretBullet:
                turretBulletClr.color = AccessibilityManager.GetCurrentProfile().turretBulletClr;
                break;
            case ColorProperty.EnemyRoute:
                enemyRouteClr.color = AccessibilityManager.GetCurrentProfile().enemyRouteClr;
                break;
            case ColorProperty.Enemy:
                enemyClr.color = AccessibilityManager.GetCurrentProfile().enemyClr;
                break;
        }
    }

    public void GetCurrentColorPicked(ColorProperty currentColorProperty)
    {
        this.currentColorProperty = currentColorProperty;
    }
}
