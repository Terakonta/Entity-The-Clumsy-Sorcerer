using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MoveRightBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private PlayerController pc;
    private bool isMouseDown = false;

    void Start()
    {
        pc = GameObject.Find("Entity").GetComponent<PlayerController>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        isMouseDown = true;
        pc.moveRight = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        isMouseDown = false;
        pc.moveRight = false;
    }

}