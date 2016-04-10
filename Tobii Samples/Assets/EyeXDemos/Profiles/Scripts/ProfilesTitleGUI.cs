using UnityEngine;
using System.Collections;

/// <summary>
/// Script that displays all user profiles and highlights the current one.
/// </summary>
public class ProfilesTitleGUI : MonoBehaviour 
{
    private EyeXHost _host;

    private const float buttonWidth = 250;
    private const float buttonHeight = 30;
    private const float buttonMargin = 10;

    private void Start () 
    {
        _host = EyeXHost.GetInstance();
        _host.Start();
    }

    private void OnGUI()
    {
        // Draw the title.
        GuiHelpers.DrawText("PROFILES", new Vector2(10, 10), 36, GuiHelpers.Magenta);

        if (!IsSupportedEngineVersion())
        {
            // The current engine is not supported.
            GuiHelpers.DrawRequiredEngineVersionError("1.3");
            return;
        }

        UpdateProfiles(
            _host.UserProfileNames, 
            _host.UserProfileName);
    }

    private void UpdateProfiles(
        EyeXEngineStateValue<string[]> profiles,
        EyeXEngineStateValue<string> currentProfile)
    {
        if(profiles.IsValid && currentProfile.IsValid)
        {
            // Draw the title.
            GuiHelpers.DrawText("Click a profile button to activate it.", new Vector2(10, 60), 18, GuiHelpers.Black);

            for(int index = 0; index < profiles.Value.Length; index++)
            {
                // Get the profile name.
                var profileName = profiles.Value[index];

                // Should we disable the GUI?
                var isCurrentProfile = profileName == currentProfile.Value;
                if(isCurrentProfile)
                {
                    GUI.enabled = false;
                }

                // Draw the button.
                var buttonX = buttonMargin;
                var buttonY = 100 + (index * (buttonHeight + buttonMargin));
                var buttonRect = new Rect(buttonX, buttonY, buttonWidth, buttonHeight);
                if (GUI.Button(buttonRect, profileName))
                {
                    // Set the current user profile.
                    _host.SetCurrentProfile(profileName);
                }

                // Re-enable the GUI.
                GUI.enabled = true;
            }
        }
        else
        {
            // No valid profile name.
            GuiHelpers.DrawText("Error: Could not retrieve profile names.", new Vector2(10, 60), 18, GuiHelpers.Red);
        }
    }

    private bool IsSupportedEngineVersion()
    {
        // The EyeX Engine version need to be equal to or higher than 1.3.
        var version = _host.EngineVersion;
        return version != null && version.Major >= 1 && version.Minor >= 3;
    }
}
