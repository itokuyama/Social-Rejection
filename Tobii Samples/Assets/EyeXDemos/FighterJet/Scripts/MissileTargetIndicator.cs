//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MissileTargetIndicator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Renderer _renderer;

    public Sprite TargetSprite;
    public Sprite TargetLockSprite;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _renderer = GetComponent<Renderer>();
    }

    public void Hide()
    {
        _renderer.enabled = false;
    }

    public void SetState(bool locked)
    {
        _spriteRenderer.enabled = true;
        _spriteRenderer.sprite = locked ? TargetLockSprite : TargetSprite;
    }
}
