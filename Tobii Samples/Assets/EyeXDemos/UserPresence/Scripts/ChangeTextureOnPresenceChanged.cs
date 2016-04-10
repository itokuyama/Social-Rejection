//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using UnityEngine;

[RequireComponent(typeof(UserPresenceComponent), typeof(Renderer))]
public class ChangeTextureOnPresenceChanged : MonoBehaviour
{
    public Texture textureUsedWhenUserIsPresent;

    private Texture _savedTexture;
    private UserPresenceComponent _userPresenceComponent;
    private Renderer _rendererComponent;

    void Start()
    {
        _userPresenceComponent = GetComponent<UserPresenceComponent>();
        _rendererComponent = GetComponent<Renderer>();
    }

    void Update()
    {
        if (_userPresenceComponent.IsValid &&
            _userPresenceComponent.IsUserPresent)
        {
            OnUserPresent();
        }
        else
        {
            OnUserGone();
        }
    }

    private void OnUserPresent()
    {
        if (_savedTexture == null)
        {
            _savedTexture = _rendererComponent.material.mainTexture;
            _rendererComponent.material.mainTexture = textureUsedWhenUserIsPresent;
        }
    }

    private void OnUserGone()
    {
        if (_savedTexture != null)
        {
            _rendererComponent.material.mainTexture = _savedTexture;
            _savedTexture = null;
        }
    }
}
