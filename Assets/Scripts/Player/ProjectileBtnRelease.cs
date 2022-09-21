using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ProjectileBtnRelease : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
        pc.FireBtn();
    }

    public void OnPointerUp(PointerEventData data)
    {
        isMouseDown = false;

    }

}
