using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorPickerManager : MonoBehaviour
{
    public Image colorBlock;
    public TMP_InputField redInput;
    public TMP_InputField greenInput;
    public TMP_InputField blueInput;

    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public Button saveColours;
    public Button backBtn;
    public TextMeshProUGUI colourSelected;

    public ColorProperty currentColorProperty;

    private static AccessibilityManager AccessibilityManager;
    private static GameManager GameManager;
    private static CustomColorManager CustomColorManager;
    private Dictionary<ColorProperty, string> colourStrings = new Dictionary<ColorProperty, string>
    {
        {ColorProperty.GameWall, "Game Wall"},
        {ColorProperty.StartPoint, "Start Point"},
        {ColorProperty.EndPoint, "End Point"},
        {ColorProperty.EnemyRoute, "Enemy Route"},
        {ColorProperty.Enemy, "Enemy"},
        {ColorProperty.Turret, "Turret"},
        {ColorProperty.TurretBullet, "Turret Bullet"},
        {ColorProperty.HighHealth, "High Health"},
        {ColorProperty.LowHealth, "Low Health"},
    };

    private void Awake()
    {
        AccessibilityManager = FindObjectOfType<AccessibilityManager>();
        GameManager = FindObjectOfType<GameManager>();
        CustomColorManager = FindObjectOfType<CustomColorManager>();
    }

    void Start()
    {
        redInput.onSubmit.AddListener(delegate { UpdateColorsViaInput(); });
        greenInput.onSubmit.AddListener(delegate { UpdateColorsViaInput(); });
        blueInput.onSubmit.AddListener(delegate { UpdateColorsViaInput(); });
        redSlider.onValueChanged.AddListener(delegate { UpdateColorsViaSliders(); });
        greenSlider.onValueChanged.AddListener(delegate { UpdateColorsViaSliders(); });
        blueSlider.onValueChanged.AddListener(delegate { UpdateColorsViaSliders(); });

        saveColours.onClick.AddListener(delegate { SaveColours(); });

        backBtn.onClick.AddListener(delegate { SceneManager.UnloadSceneAsync("ColorPicker"); });
    } 

    void Update()
    {
        UpdateSelectedColourText();
    }

    private void UpdateColorsViaInput()
    {
        if (colorBlock != null)
        {
            float redValue = 0;
            float greenValue = 0;
            float blueValue = 0;

            if (float.TryParse(redInput.text, out redValue))
            {
                redValue = Mathf.Clamp(redValue, 0, 255) / 255;
            }

            if (float.TryParse(greenInput.text, out greenValue))
            {
                greenValue = Mathf.Clamp(greenValue, 0, 255) / 255;
            }

            if (float.TryParse(blueInput.text, out blueValue))
            {
                blueValue = Mathf.Clamp(blueValue, 0, 255) / 255;
            }

            colorBlock.color = new Color((float)redValue, (float)greenValue, (float)blueValue); 

            redSlider.value = redValue;
            greenSlider.value = greenValue;
            blueSlider.value = blueValue;
        }
    }

    private void UpdateColorsViaSliders()
    {
        if (colorBlock != null)
        {
            colorBlock.color = new Color(redSlider.value, greenSlider.value, blueSlider.value);
            redInput.text = Mathf.Floor((redSlider.value * 255)).ToString();
            greenInput.text = Mathf.Floor((greenSlider.value * 255)).ToString();
            blueInput.text = Mathf.Floor((blueSlider.value * 255)).ToString();
        }
    }

    public void InitializeValues(ColorProperty colorProperty)
    {
        Color initialColor = new Color();
        if (colorProperty == ColorProperty.GameWall)
        {
            initialColor = CustomColorManager.gameWallClr.color;
        }
        else if (colorProperty == ColorProperty.StartPoint)
        {
            initialColor = CustomColorManager.startPointClr.color;
        }
        else if (colorProperty == ColorProperty.EndPoint)
        {
            initialColor = CustomColorManager.endPointClr.color;
        }
        else if (colorProperty == ColorProperty.EnemyRoute)
        {
            initialColor = CustomColorManager.enemyRouteClr.color;
        }
        else if (colorProperty == ColorProperty.Enemy)
        {
            initialColor = CustomColorManager.enemyClr.color;
        }
        else if (colorProperty == ColorProperty.Turret)
        {
            initialColor = CustomColorManager.turretClr.color;
        }
        else if (colorProperty == ColorProperty.TurretBullet)
        {
            initialColor = CustomColorManager.turretBulletClr.color;
        }
        else if (colorProperty == ColorProperty.HighHealth)
        {
            initialColor = CustomColorManager.highHealthClr.color;
        }
        else if (colorProperty == ColorProperty.LowHealth)
        {
            initialColor = CustomColorManager.lowHealthClr.color;
        }

        float redValue = initialColor.r * 255;
        float greenValue = initialColor.g * 255;
        float blueValue = initialColor.b * 255;

        redInput.text = redValue.ToString();
        greenInput.text = greenValue.ToString();
        blueInput.text = blueValue.ToString();

        redSlider.value = initialColor.r;
        greenSlider.value = initialColor.g;
        blueSlider.value = initialColor.b;
    }

    private void SaveColours()
    {
        Color newColor = colorBlock.color;
        AccessibilityManager.UpdateCustomProfile(currentColorProperty, newColor);
        CustomColorManager.UpdateColourBlocks(currentColorProperty);
        Debug.Log("Color saved: " + newColor);
        SceneManager.UnloadSceneAsync("ColorPicker");
    }

    private void UpdateSelectedColourText()
    { 
        CustomColorManager.GetCurrentColorPicked(currentColorProperty);
        colourSelected.text = colourStrings[currentColorProperty] + " Colour";
    }
}