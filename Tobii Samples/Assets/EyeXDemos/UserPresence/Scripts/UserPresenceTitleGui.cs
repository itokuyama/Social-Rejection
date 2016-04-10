//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using UnityEngine;

[RequireComponent(typeof(UserPresenceComponent))]
public class UserPresenceTitleGui : MonoBehaviour
{
	private UserPresenceComponent _userPresenceComponent;

	void Start()
	{
		_userPresenceComponent = GetComponent<UserPresenceComponent>();
	}

	void OnGUI()
	{
		// Check whether or not the user is present.
		var hasUserPresence = _userPresenceComponent.IsUserPresent ? "Yes" : "No";
		GuiHelpers.DrawText("Is user present?", new Vector2 (10, 10), 18, Color.gray);
		GuiHelpers.DrawText(hasUserPresence, new Vector2 (160, 10), 18, Color.black);

		// Check whether or not the EyeX Engine is tracking the user's gaze.
		GuiHelpers.DrawText("Is tracking gaze?", new Vector2 (10, 40), 18, Color.gray);
		if(_userPresenceComponent.GazeTracking == EyeXGazeTracking.NotSupported)
		{
			// This functionality is not available for the user.
			GuiHelpers.DrawText("Requires EyeX Engine 1.4", new Vector2 (160, 40), 18, Color.red);
		}
		else
		{
			var isTrackingGaze = _userPresenceComponent.GazeTracking == EyeXGazeTracking.GazeTracked;
			GuiHelpers.DrawText(isTrackingGaze ? "Yes" : "No", new Vector2 (160, 40), 18, Color.black);
		}
	}
}
