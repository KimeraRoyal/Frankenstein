using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Bodybuilder.GFX
{
    public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent OnHover;
        public UnityEvent OnEndHover;

        public void OnPointerEnter(PointerEventData eventData)
            => OnHover?.Invoke();

        public void OnPointerExit(PointerEventData eventData)
            => OnEndHover?.Invoke();
    }
}