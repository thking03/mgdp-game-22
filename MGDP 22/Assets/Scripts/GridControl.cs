using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridControl : MonoBehaviour
{
    public bool moveable;
    public bool fallable;
    public bool button;
    public bool isNpc;
    public bool isInteractable;
    public UnityEvent buttonAction;
    public UnityEvent interactAction;
    public Vector3 position;
    private GameObject player;
    public GameObject interactSymbol;

    private void Start()
    {
        UpdatePosition();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (player.transform.position == transform.position + new Vector3(0, 1, 0) && fallable)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public bool checkMoveable(Vector3 loc, GameObject[] world)
    {
        foreach (GameObject obj in world)
        {
            if (obj.GetComponent<GridControl>().position == loc + transform.position)
            {
                return false;
            }
        }



        return OnGround();
    }

    public bool OnGround()
    {
        //ADDED IN BUFFER 8-11 (the +- 0.05 thing is what to change if you don't like it)
        return (Mathf.RoundToInt(transform.position.y) <= transform.position.y + 0.05 && Mathf.RoundToInt(transform.position.y) >= transform.position.y - 0.05);

    }

    public void UpdatePosition()
    {
        position = transform.position;
    }

    public void ButtonClick()
    {
        buttonAction.Invoke();
    }

    public void InteractClick()
    {
        interactAction.Invoke();
    }

    public void TestButton()
    {
        Debug.Log(" Button has been clicked!");
    }
}
