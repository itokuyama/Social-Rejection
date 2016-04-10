//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using UnityEngine;

/// <summary>
/// Represents a part of the goal.
/// </summary>
[RequireComponent(typeof(GazeAwareComponent))]
public class GoalZone : MonoBehaviour
{
    private GazeAwareComponent _gazeAwareComponent;

    /// <summary>
    /// Determines whether or not this part of the 
    /// goal is being looked at.
    /// </summary>
    public bool IsBeingLookedAt { get; private set; }

    /// <summary>
    /// Determines whether or not this part of the
    /// goal was clicked.
    /// </summary>
    public bool WasClicked { get; private set; }

    protected void Start()
    {
        _gazeAwareComponent = GetComponent<GazeAwareComponent>();
    }

    protected void Update()
	{
		// Are we looking at the game object?
		IsBeingLookedAt = _gazeAwareComponent.HasGaze;
		WasClicked = Input.GetMouseButtonDown (0) && PerformHitTest ();
	}
	
    /// <summary>
    /// Tests if the current mouse position was contained within the zone.
    /// </summary>
    /// <returns><c>true</c> if the mouse position was contained; otherwise <c>false</c>.</returns>
	private bool PerformHitTest()
	{
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) 
		{
			if (hit.collider.name.Equals(gameObject.name, System.StringComparison.Ordinal)) 
			{
				return true;
			}
		}
		return false;
	}
}
