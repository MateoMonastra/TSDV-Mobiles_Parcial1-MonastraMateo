using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform stick = null;

    public int player;
    public float limit = 80.0f;

    public void OnPointerDown(PointerEventData eventData)
    {
        stick.anchoredPosition = ConverToLocal(eventData);
    }

    private Vector2 ConverToLocal(PointerEventData eventData)
    {
        Vector2 newPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle
        (transform as RectTransform,
            eventData.position,
            eventData.enterEventCamera,
            out newPos);
        return newPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = ConverToLocal(eventData);
        if (pos.magnitude > limit)
            pos = pos.normalized * limit;

        stick.anchoredPosition = pos;

        float x = pos.x / limit;
        float y = pos.y / limit;

        SetHorizontal(x);
        SetVertical(y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stick.anchoredPosition = Vector2.zero;
        SetHorizontal(0f);
        SetVertical(0f);
    }

    private void OnDisable()
    {
        SetHorizontal(0f);
        SetVertical(0f);
    }

    private void SetHorizontal(float val)
    {
        InputManager.GetInstance().SetAxis($"Horizontal{player}", val);
    }

    private void SetVertical(float val)
    {
        InputManager.GetInstance().SetAxis($"Vertical{player}", val);
    }
}