using UnityEngine;
using UnityEngine.UI;

public class ColorblindMode : MonoBehaviour
{
    public enum ColorblindType
    {
        Normal,
        Protanopia,
        Deuteranopia,
        Tritanopia
    }

    public ColorblindType colorblindMode = ColorblindType.Normal;
    private ColorBlock[] originalColors;
    private Selectable[] selectables;

    private void Start()
    {
        selectables = GetComponentsInChildren<Selectable>();
        originalColors = new ColorBlock[selectables.Length];

        for (int i = 0; i < selectables.Length; i++)
        {
            originalColors[i] = selectables[i].colors;
        }
    }

    public void ToggleColorblindMode()
    {
        switch (colorblindMode)
        {
            case ColorblindType.Normal:
                ApplyColorblindMode(ColorConverter.RGBToProtanopia);
                colorblindMode = ColorblindType.Protanopia;
                break;
            case ColorblindType.Protanopia:
                ApplyColorblindMode(ColorConverter.RGBToDeuteranopia);
                colorblindMode = ColorblindType.Deuteranopia;
                break;
            case ColorblindType.Deuteranopia:
                ApplyColorblindMode(ColorConverter.RGBToTritanopia);
                colorblindMode = ColorblindType.Tritanopia;
                break;
            case ColorblindType.Tritanopia:
                ResetColorblindMode();
                colorblindMode = ColorblindType.Normal;
                break;
        }
    }

    private void ApplyColorblindMode(System.Func<Color, Color> converter)
    {
        for (int i = 0; i < selectables.Length; i++)
        {
            ColorBlock newColors = originalColors[i];
            newColors.normalColor = converter(originalColors[i].normalColor);
            selectables[i].colors = newColors;
        }
    }

    private void ResetColorblindMode()
    {
        for (int i = 0; i < selectables.Length; i++)
        {
            selectables[i].colors = originalColors[i];
        }
    }
}

public static class ColorConverter
{
    public static Color RGBToProtanopia(Color originalColor)
    {
        float newR = 0.567f * originalColor.g + 0.433f * originalColor.r;
        float newG = 0.558f * originalColor.g + 0.442f * originalColor.r;
        float newB = originalColor.b;

        return new Color(originalColor.g, originalColor.b, originalColor.b, originalColor.a);
    }

    public static Color RGBToDeuteranopia(Color originalColor)
    {
        float newR = 0.625f * originalColor.g + 0.375f * originalColor.r;
        float newG = 0.7f * originalColor.g + 0.3f * originalColor.r;
        float newB = originalColor.b;

        return new Color(originalColor.r, originalColor.b, originalColor.r, originalColor.a);
    }

    public static Color RGBToTritanopia(Color originalColor)
    {
        float newR = originalColor.r;
        float newG = 0.95f * originalColor.g + 0.05f * originalColor.b;
        float newB = 0.433f * originalColor.g + 0.567f * originalColor.b;

        return new Color(originalColor.r, originalColor.g, originalColor.r, originalColor.a);
    }
}