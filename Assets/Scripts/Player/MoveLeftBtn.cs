using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MoveLeftBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
        pc.moveLeft = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        isMouseDown = false;
        pc.moveLeft = false;
    }

}