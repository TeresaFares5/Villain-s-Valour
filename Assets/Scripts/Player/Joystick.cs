using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;

    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    private RectTransform joystickRect;
    private RectTransform joystickBGRect;

    void Start()
    {
        joystickRect = joystick.GetComponent<RectTransform>();
        joystickBGRect = joystickBG.GetComponent<RectTransform>();

        joystickOriginalPos = joystickBGRect.anchoredPosition;
        joystickRadius = joystickBGRect.sizeDelta.y / 2;
    }

    private void OnDisable()
    {
        ResetJoystick();
    }

    public void PointerDown()
    {
        joystickBG.transform.position = Input.mousePosition;
        joystick.transform.position = Input.mousePosition;
        joystickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;

        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if (joystickDist < joystickRadius)
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
        }
        else
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
        }
    }

    public void PointerUp()
    {
        ResetJoystick();
    }

    public void ResetJoystick()
    {
        joystickVec = Vector2.zero;

        if (joystickRect != null && joystickBGRect != null)
        {
            joystickRect.anchoredPosition = joystickOriginalPos;
            joystickBGRect.anchoredPosition = joystickOriginalPos;
        }
    }
}