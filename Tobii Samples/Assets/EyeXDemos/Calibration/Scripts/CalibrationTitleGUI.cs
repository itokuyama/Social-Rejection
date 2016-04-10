//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using UnityEngine;

/// <summary>
/// Script that displays GUI buttons for recalibration and to test calibration.
/// <para>
/// Note that the Application.runInBackground property is set to true
/// while waiting for the recalibration to finish. If you use this script 
/// in a game, make sure that the background running does not cause any 
/// unpleasant side effects. 
/// </para>
/// </summary>
public class CalibrationTitleGUI : MonoBehaviour
{
    private EyeXHost _host;
    private WaitingState _waitingState = WaitingState.NotWaiting;
    private bool _originalRunInBackgroundState;

    private enum WaitingState
    {
        NotWaiting = 0,
        WaitingForCalibrationToStart,
        WaitingForCalibrationToFinish
    }

    private void Start()
    {
        _host = EyeXHost.GetInstance();
        _host.Start();

        _originalRunInBackgroundState = Application.runInBackground;
    }

    private void OnDisable()
    {
        StopWaitingForCalibration();
    }

    private void OnGUI()
    {
        // Draw the title.
        GuiHelpers.DrawText("CALIBRATION", new Vector2(10, 10), 36, GuiHelpers.Magenta);

        if (IsSupportedEngineVersion())
        {
            // Draw the "Recalibrate" button.
            if (GUI.Button(new Rect(10, 70, 150, 30), "Recalibrate"))
            {
                StartWaitingForCalibration();

                _host.LaunchRecalibration();
            }

            // Draw the "Test calibration" button.
            if (GUI.Button(new Rect(10, 110, 150, 30), "Test calibration"))
            {
                _host.LaunchCalibrationTesting();
            }
        }
        else
        {
            // The current engine is not supported.
            GuiHelpers.DrawRequiredEngineVersionError("1.1");
        }
    }

    private void Update()
    {
        if (_waitingState == WaitingState.WaitingForCalibrationToStart
            && _host.EyeTrackingDeviceStatus == EyeXDeviceStatus.Pending)
        {
            print("Waiting for calibration to finish");
            _waitingState = WaitingState.WaitingForCalibrationToFinish;
        }
        else if (_waitingState == WaitingState.WaitingForCalibrationToFinish
                 && _host.EyeTrackingDeviceStatus == EyeXDeviceStatus.Tracking)
        {
            print("Calibration finished. Bring back focus to application");
            WindowHelpers.ShowCurrentWindow();

            StopWaitingForCalibration();
        }
    }

    private void StartWaitingForCalibration()
    {
        _originalRunInBackgroundState = Application.runInBackground;

        // Set runInBackground to true to be able to wait for calibration to finish.
        Application.runInBackground = true;

        print("Waiting for calibration to start");
        _waitingState = WaitingState.WaitingForCalibrationToStart;
    }

    private void StopWaitingForCalibration()
    {
        // Reset runInBackground to its original value when the waiting is over.
        Application.runInBackground = _originalRunInBackgroundState;

        _waitingState = WaitingState.NotWaiting;
    }

    private bool IsSupportedEngineVersion()
    {
        // The EyeX Engine version need to be equal to or higher than 1.1.
        var version = _host.EngineVersion;
        return version != null && version.Major >= 1 && version.Minor >= 1;
    }
}
