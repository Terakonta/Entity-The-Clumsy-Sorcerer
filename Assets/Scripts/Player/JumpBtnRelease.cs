using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class JumpBtnRelease : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
        pc.JumpBtn();
    }

    public void OnPointerUp(PointerEventData data)
    {
        isMouseDown = false;
        pc.JumpBtnRelease();
    }

}