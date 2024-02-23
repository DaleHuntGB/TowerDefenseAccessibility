using JetBrains.Annotations;
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

    public ColorProperty currentColorProperty;

    private static AccessibilityManager AccessibilityManager;
    private static GameManager GameManager;

    private void Awake()
    {
        AccessibilityManager = FindObjectOfType<AccessibilityManager>();
        GameManager = FindObjectOfType<GameManager>();
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

    public void InitializeValues(Color initialColor)
    {
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
        SceneManager.UnloadSceneAsync("ColorPicker");
    }

}
