using Input;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MouseGraphic : MonoBehaviour
{
    private Mouse _mouse;
    [SerializeField] private Camera _camera;

    private SpriteRenderer _renderer;

    [SerializeField] private Sprite _normalGraphic, _holdGraphic;
    [SerializeField] private Vector3 _offset;

    private void Awake()
    {
        _mouse = FindAnyObjectByType<Mouse>();
        _mouse.OnPressed.AddListener(Press);
        _mouse.OnReleased.AddListener(Release);

        if (!_camera) { _camera = FindAnyObjectByType<Camera>(); }

        _renderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        var worldPos = (Vector3) _mouse.Position;
        worldPos = _camera.ScreenToWorldPoint(worldPos);
        transform.position = worldPos + _offset;
    }

    private void Press(Vector2 position)
    {
        _renderer.sprite = _holdGraphic;
    }

    private void Release(Vector2 position)
    {
        _renderer.sprite = _normalGraphic;
    }
}
