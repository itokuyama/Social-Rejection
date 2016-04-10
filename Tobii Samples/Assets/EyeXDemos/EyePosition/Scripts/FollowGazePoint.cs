using UnityEngine;

[RequireComponent(typeof(GazePointDataComponent), typeof(Renderer))]
public class FollowGazePoint : MonoBehaviour
{
    // Scale: 1 mm maps to 0.001 units in world space
    private const float Scale = 1 / 1000.0f;

    private EyeXHost _eyeXHost;
    private GazePointDataComponent _gazePointDataComponent;
    private Renderer _rendererComponent;

    void Start()
    {
        _eyeXHost = EyeXHost.GetInstance();
        _gazePointDataComponent = GetComponent<GazePointDataComponent>();
        _rendererComponent = GetComponent<Renderer>();
    }

    void Update()
    {
        var displaySize = _eyeXHost.DisplaySize;
        var screenBounds = _eyeXHost.ScreenBounds;
        var gazePoint = _gazePointDataComponent.LastGazePoint;

        var gazePointOnDisplayX = gazePoint.Display.x;
        var gazePointOnDisplayY = gazePoint.Display.y;

        if (displaySize.IsValid &&
            screenBounds.IsValid && screenBounds.Value.Width > 0 && screenBounds.Value.Height > 0 &&
            gazePoint.IsValid)
        {
            var normalizedGazePoint = new Vector2(
                (float)((gazePointOnDisplayX - screenBounds.Value.X) / screenBounds.Value.Width),
                (float)((gazePointOnDisplayY - screenBounds.Value.Y) / screenBounds.Value.Height));

            var gazePointOnDisplayPlaneMm = new Vector2(
                (float)((0.5 - normalizedGazePoint.x) * displaySize.Value.Width),
                (float)((0.5 - normalizedGazePoint.y) * displaySize.Value.Height));

            _rendererComponent.transform.position = new Vector3(
                gazePointOnDisplayPlaneMm.x * Scale,
                gazePointOnDisplayPlaneMm.y * Scale,
                0);

            _rendererComponent.enabled = true;
        }
        else
        {
            _rendererComponent.enabled = false;
        }
    }
}
