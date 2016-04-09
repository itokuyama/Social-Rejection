Tobii EyeX Software Development Kit for Unity
=============================================

README

  This package contains everything you need to create games and
  applications using the Tobii EyeX Engine API and the Unity 3D engine:
  - a sample Unity project
  - documentation
  - libraries for accessing the EyeX Engine API

CONTACT

  If you have problems, questions, ideas, or suggestions, please use the forums
  on the Developer Zone (link below). That's what they are for!

WEB SITE

  Visit the Tobii Developer Zone web site for the latest news and downloads:
  
  http://developer.tobii.com/

COMPATIBILITY

  This version of the EyeX SDK requires EyeX Engine version 1.0.0 or later.
  Specific features will require newer versions of the EyeX Engine as listed
  in the revision history below.

REVISION HISTORY

  2016-03-14
  Version 1.7:
    - Bug fix: For newer versions of Unity 5, the behavior of the OnApplicationFocus
	  message was changed causing the user to have to alt-tab away and back to Unity 
	  or the game to get valid data from the EyeX data streams. The data streams now 
	  start correctly at application start.
	- Bug fix: Eye-gaze coordinates were incorrectly mapped when a game was run in a
	  non-native aspect ratio. Coordinates are now mapped correctly for aspects
	  ratios with black bars around the game view.
	- Known issue: Eye-gaze coordinates are not mapped correctly in the Unity Editor
	  for DPI settings above 125%. This is because the Unity Editor itself is not 
	  registered as a DPI-aware application with Windows. This issue is more commonly
	  seen on Windows 10 since Windows 10 automatically sets higher DPI settings on 
	  screens with high resolutions. Work-around: manually set the screen DPI to 125%
	  or lower.

  2015-11-19
  Version 1.6:
    - Removed all dependencies on the EyeXButton for activatable behavior. The
      demo scene using the activatable behavior now hooks its own keyboard key
      for activation, and sends action commands to trigger activation and
      activation mode on.
    - Removed all dependencies on the EyeXButton for pannable behavior. The 
      demo scene using the pannable behavior now hooks its own keyboard key
      for panning, and triggers panning begin and end commands through the
      PannableComponent.
    - Methods for triggering action commands have been added to the 
      ActivatableComponent, the PannableComponent, and the EyeXHost.
    - Rewrote and restructured parts of the Developer Guide, and added more 
      detailed inforation about action commands for the activatable and the
      pannable behaviors.
    - Bug fix: the 32 bit EyeX client dll is now correctly copied when building
      a game for 32 bit on a 64 bit system.
      
  2015-06-12
  Version 1.5:
    - Added new sample: Profiles. Requires EyeX Engine 1.3.0.
    - Added method: EyeXHost.SetCurrentProfile. Requires EyeX Engine 1.3.0.
    - Added property: EyeXHost.UserProfileNames. Requires EyeX Engine 1.3.0.
    - Added property: EyeXHost.IsGazeTracked. Requires EyeX Engine 1.4.0.
	- New semantic behavior for UserPresence state: the user will be detected as
	  present in more cases than before. The user's eyes do not have to be open.
    - The EyeXHost has been updated to use new states that replace deprecated 
      states with the same state paths. The new states where introduced in EyeX
      Engine 1.3.0.
      
      New state                                 | Replaces deprecated state
      ----------------------------------------- | --------------------------------
      StatePaths.EyeTrackingScreenBounds        | StatePaths.ScreenBounds           
      StatePaths.EyeTrackingDisplaySize         | StatePaths.DisplaySize            
      StatePaths.EyeTrackingConfigurationStatus | StatePaths.ConfigurationStatus    
	  
      Two new states with new state paths have also been added to the API, but are
      not updated yet in the EyeXHost because of backward compatibility with
      pre-1.3.0 EyeX Engines.
      
      New state                                 | Old state
      ----------------------------------------- | ---------------------------------
      StatePaths.EngineInfoVersion              | StatePaths.EngineVersion   
      StatePaths.EyeTrackingCurrentProfileName  | StatePaths.ProfileName 
	
  2015-04-14
  Version 1.4:
    - Added panning documentation and samples.
    - Added Unity 5 support.

  2015-01-15
  Version 1.3:
    - Fixed data stream coordinate offset in Unity 4.6.
    - Modified EyePosition demo to reduce flickering.
    - Modified Calibration demo so the game regains focus
      after calibration has been finished.

  2014-12-16
  Version 1.2:
    - Added minimal sample and support for launching recalibration and
      calibration testing programmatically. This function requires EyeX Engine 1.1.
    - Added a component for checking whether the EyeX Engine is available and a 
      visual indicator in the Activatable Buttons demo scene.
    - Added support for retrieving EyeX Engine version.
    - Improved projected rectangle calculations for cubes, by using the BoxCollider 
      corner points instead of the axis-aligned Renderer.bounds. Also made it possible
      to override the GetBoundingCornerPoints method to support any type of object.
    - The deprecated Unity API functions in EyeX Framework have been replaced to 
      be prepared for Unity 5.

  2014-11-20
  Version 1.1:
    - The EyeX Framework is distributed as a .unityPackage file, for easier import
      into existing games.
    - The most common API functions are encapsulated as Unity components that 
      can be attached to an existing game object.
    - Added a new sample, Spotlight, which demonstrates how to combine the
      components for gaze point data with the one for user presence to "shine a light"
      on what the user is looking at.
    - Added a new editor script for automatic deployment of EyeX client library files.
    - Updated samples and documentation.

  2014-10-27
  Version 1.0:
    - Client library compatible with EyeX Engine 1.0.
    - Improved handling of Windows screen scale settings to avoid offset problems
      in Windows 7 and 8.
    - Added normalized eye positions to EyeXEyePositionDataStream.
    - Hooked the Right Shift key to trigger activations in the ActivatableGUI scene.
    - Updated samples and documentation for the new direct click modes in 
      EyeX Interaction settings.

  2014-09-23
  Version 0.32:
    - Client library compatible with both EyeX Engine 0.10 and 1.0.
    - Added new samples: Goalkeeper and Fighter Jet.
    - EyeXHost initialization moved to the Awake method.
    - Fix for resolving correct renderer when calculating game object bounds.

  2014-09-05
  Version 0.31: Updated package for Tobii EyeX Engine 0.10.0:
    - EyeX Framework for Unity updated for EyeX Engine API changes. Most of the
      API changes will not be visible to framework users.

  2014-08-22
  Version 0.24: 
    - Restructured and renamed the project that contains the Unity samples 
      and framework to better match the Asset Store packaging guidelines. 
    - The framework classes have been moved to the StandardAssets directory.
    - Added support for engine states and two new samples to demonstrate 
      the user presence state and how to show the gaze point in 3D space. 
    - Made it possible to override the GetProjectedRect() and GetBounds() 
      methods on the EyeXGameObjectInteractorBase class.
    - Converting EyeX Engine coordinates correctly when running in windowed 
      full screen mode (new from Unity 4.5).

  2014-08-18
  Version 0.24: Restructured and renamed the project that contains the Unity 
  samples and framework to better match the Asset Store packaging guidelines. 
  The framework classes have been moved to the StandardAssets directory.
  Added support for engine states and a sample for the user presence state.

  2014-06-19
  Version 0.23: Refactored the EyeX Framework for Unity. Added samples for
  eye position and map navigation. Updated Developer's Guide.

  2014-05-21
  Version 0.22: Bug fixes for problems with freezing Unity editor. Removed 
  Mono samples -- refer to the new EyeX SDK for .NET instead.

  2014-05-07
  Version 0.21: Updated package for Tobii EyeX Engine 0.8.14:
    - Client libraries updated with some breaking API changes (see below).
    - All samples are updated with the new client DLLs.
    - Various bug fixes on the Unity samples.

  2014-04-08
  Version 0.20: Updated package for Tobii EyeX Engine 0.8.11:
    - Client libraries updated with some breaking API changes (see below).
    - All samples are updated with the new client DLLs.
    - The Unity GazeAware3DScene now demonstrates how to define
      non-rectangular interactors by using weighted stencil masks.
    - The Unity GazeAware3DScene and the Mono GazeAwarePanels
      sample now demonstrates how to define an interactor with inertia
      by using GazeAwareMode.Delayed.
    - The Developer's Guide is updated.

  2014-03-05
  Version 0.17: Adapted the EyeX client libraries for use with the Mono runtime
  and the Mono soft debugger.

  2014-02-28
  Version 0.16: Added color and size to GazePointVisualizer. Added experimental
  functions to visualize the fixation points.

  2014-02-26
  Version 0.15: Fixed an issue where there could be more than one instance of an
  object that was supposed to be a singleton, leading to a crash in Unity.

  2014-02-21
  Version 0.14.40: Added a new Unity sample demonstrating how to access the
  gaze data stream. Renamed the EyeXInteractorBase class to
  EyeXInteractionBehaviour. Fixed a bug that would sometimes cause the Unity
  editor to crash on exit.

  2014-02-12
  Version 0.13.39: Bug fixes: Settings retrieval bug fixed in client library,
  missing DPI flag and dispose of context and system added in GazeAwarePanels
  Mono sample.

  2014-02-06
  Version 0.13.38: Added samples licence agreement. Minor updates to samples:
  Added clarifying comments and made the visual feedback on buttons more
  consistent in the Unity sample, added query bounds checking in Mono sample.

  2014-01-03
  Version 0.13.37: This is the first official alpha release of the SDK. APIs
  may change and backward compatibility isn't guaranteed. As a rule of thumb,
  the APIs used in the samples are the most mature and less likely to change
  much.

KNOWN ISSUES

  Errors are reported to the console in the EyePositionScene sample scene:
  "!dest.m_MultiFrameGUIState.m_NamedKeyControlList". This seems to be a known 
  issue with the Unity editor. See 
  http://answers.unity3d.com/questions/447913/destm-multiframeguistatem-namedkeycontrollist.html 
  for more information.

  When building and running a project, some demo scenes cause the player to 
  crash on shutdown. This seems to be a known issue, see
  http://answers.unity3d.com/questions/467030/unity-builds-crash-when-i-exit-1.html

  If you are running in editor mode and have placed the game window on a different
  screen than the main editor window, the gaze data will be offset.

  If you are running the Unity 4.6 editor, the gaze data will be offset by the distance 
  between the top-left of the screen and the top-left of the game window (or the main 
  window if the game window is docked). This is caused by an internal change related 
  to the new Unity GUI system that is not yet fully supported by the EyeX SDK.
  
EYEX ENGINE API CHANGES

  2014-10-22
  EyeX Engine 1.0
  - No actual API changes, but functional changes related to the Activatable 
    behavior, direct click and key bindings: 
       - If EyeX Interaction is disabled, no default keys are mapped to direct click.
       - ActivationFocus and Activated events are sent simultaneously if EyeX Button 
         interaction is configured in EyeX Interaction settings.

  2014-09-05
  EyeX Engine Developer Preview 0.10.0
  - Name changes:
      InteractionSystem => Environment
      InteractionSnapshot => Snapshot
      InteractionQuery => Query
      InteractionBehaviorType => BehaviorType
      InteractionContext => Context
      InteractionBoundsType => BoundsType
      PresenceData => UserPresence
      Interactor.Set[X]Behavior => Interactor.Create[X]Behavior

  2014-05-07
  EyeX Engine Developer Preview 0.8.14
  - The AsyncData.Result property has been removed. Please refer to the method
    AsyncData.TryGetResultCode.
  - The EyeX Engine state API has been updated.
