using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CustomColorManager : MonoBehaviour
{
    public Image gameWallClr;
    public Image startPointClr;
    public Image endPointClr;
    public Image highHealthClr;
    public Image lowHealthClr;
    public Image turretClr;
    public Image turretBulletClr;
    public Image enemyRouteClr;
    public Image enemyClr;

    public Button gameWallClrBtn;
    public Button startPointClrBtn;
    public Button endPointClrBtn;
    public Button highHealthClrBtn;
    public Button lowHealthClrBtn;
    public Button turretClrBtn;
    public Button turretBulletClrBtn;
    public Button enemyRouteClrBtn;
    public Button enemyClrBtn;

    public GameObject colorPicker;

    private static AccessibilityManager AccessibilityManager;
    private static GameManager GameManager;
    private static ColorPickerManager ColorPickerManager;
    private Image currentEditingImage;

    private void Awake()
    {
        AccessibilityManager = FindObjectOfType<AccessibilityManager>();
        GameManager = FindObjectOfType<GameManager>();
        ColorPickerManager = FindObjectOfType<ColorPickerManager>();
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

        gameWallClrBtn.onClick.AddListener(() => ToggleColorPicker(gameWallClr));
        startPointClrBtn.onClick.AddListener(() => ToggleColorPicker(startPointClr));
        endPointClrBtn.onClick.AddListener(() => ToggleColorPicker(endPointClr));
        highHealthClrBtn.onClick.AddListener(() => ToggleColorPicker(highHealthClr));
        lowHealthClrBtn.onClick.AddListener(() => ToggleColorPicker(lowHealthClr));
        turretClrBtn.onClick.AddListener(() => ToggleColorPicker(turretClr));
        turretBulletClrBtn.onClick.AddListener(() => ToggleColorPicker(turretBulletClr));
        enemyRouteClrBtn.onClick.AddListener(() => ToggleColorPicker(enemyRouteClr));
        enemyClrBtn.onClick.AddListener(() => ToggleColorPicker(enemyClr));

    }

    public void ToggleColorPicker(Image imageToEdit)
    {
        currentEditingImage = imageToEdit; // Store the reference to the image being edited
        GameObject colorPickerInstance = Instantiate(colorPicker, FindObjectOfType<Canvas>().transform, false);
        ColorPickerManager pickerManager = colorPickerInstance.GetComponent<ColorPickerManager>();

        if (pickerManager != null)
        {
            pickerManager.colorBlock.color = imageToEdit.color; // Set the initial color of the color block
            pickerManager.InitializeValues(imageToEdit.color); // Initialize the color picker values with the current color
            colorPickerInstance.SetActive(true); // Activate the color picker instance
        }
        else
        {
            Debug.LogError("ColorPickerManager component is missing on the instantiated color picker prefab.");
        }
    }


}
