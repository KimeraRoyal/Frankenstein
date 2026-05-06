using System;
using UnityEngine;

public class PlaceRelativeToScreen : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private Vector2 _anchor = Vector2.one * 0.5f;
    [SerializeField] private Vector2 _offset;

    private Vector2Int _previousScreenSize;

    private void Awake()
    {
        _camera = FindAnyObjectByType<Camera>();
    }

    private void Start()
    {
        transform.position = _anchor + _offset;
        UpdateScreenSize();
    }

    private void Update()
    {
        if(Screen.width == _previousScreenSize.x && Screen.height == _previousScreenSize.y) { return; }
        UpdateScreenSize();
    }

    private void UpdateScreenSize()
    {
        var screenSize = new Vector2Int(Screen.width, Screen.height);
        
        var worldPos = new Vector3(screenSize.x * _anchor.x, screenSize.y * _anchor.y, 0.0f);
        worldPos = _camera.ScreenToWorldPoint(worldPos);
        worldPos.z = transform.position.z;
        transform.position = worldPos + (Vector3) _offset;
        
        _previousScreenSize = screenSize;
    }
}
