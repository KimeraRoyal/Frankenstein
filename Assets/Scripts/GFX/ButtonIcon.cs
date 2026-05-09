using UnityEngine;
using UnityEngine.UI;

namespace Bodybuilder.GFX
{
    public class ButtonIcon : MonoBehaviour
    {
        private Button _button;
        private HoverUI _hover;

        private Image _image;

        [SerializeField] private Sprite _normalSprite, _activeSprite;

        private bool _clicked;

        private void Awake()
        {
            _button = GetComponentInParent<Button>();
            _button.onClick.AddListener(Click);

            _hover = GetComponentInParent<HoverUI>();
            _hover.OnHover.AddListener(Hover);
            _hover.OnEndHover.AddListener(EndHover);
        
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            _image.sprite = _normalSprite;
        }

        private void Hover()
        {
            if(_clicked) { return; }
            _image.sprite = _activeSprite;
        }

        private void EndHover()
        {
            if(_clicked) { return; }
            _image.sprite = _normalSprite;
        }

        private void Click()
        {
            _clicked = true;
            _image.sprite = _activeSprite;
        }
    }
}
