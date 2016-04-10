using UnityEngine;
using System.Collections;

/// <summary>
/// Contains convenience methods for drawing GUI related things.
/// </summary>
public static class GuiHelpers
{
    public readonly static Color Black = new Color(0f, 0f, 0f, 1f);
    public readonly static Color Magenta = new Color(0.925f, 0f, 0.533f, 1f);
    public readonly static Color Red = new Color(1f, 0f, 0f, 1f);

    public static void DrawRequiredEngineVersionError(string version)
    {
        // Create the label.
        var text = string.Format("This functionality requires EyeX Engine version {0} or higher.", version);
        var content = new GUIContent(text);

        // Create the font style.
        var style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = 18;
        style.normal.textColor = new Color(0.953f, 0.569f, 0.039f, 1f);

        // Calculate the boundaries.
        var height = style.CalcHeight(content, 700) + 30;
        var bounds = new Rect((Screen.width - 700) / 2f, Screen.height / 2f - (height / 2), 700, height);

        // Draw the label.
        GUI.Label(bounds, content, style);
    }

    public static void DrawText(string text, Vector2 position, int fontSize, Color color)
    {
        DrawText(text, position, fontSize, color, FontStyle.Normal);
    }

    public static void DrawText(string text, Vector2 position, int fontSize, Color color, FontStyle fontStyle)
    {
        // Create the label.
        var content = new GUIContent(text);

        // Create the font style.
        var style = new GUIStyle();
        style.alignment = TextAnchor.MiddleLeft;
        style.fontSize = fontSize;
        style.normal.textColor = color;
        style.fontStyle = fontStyle;

        // Draw the text using a label.
        var size = style.CalcSize(content);
        GUI.Label(new Rect(position.x, position.y, size.x, size.y), content, style);
    }
}