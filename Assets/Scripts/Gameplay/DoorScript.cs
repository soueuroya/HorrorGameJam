using System;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] bool isOpen = true;
    [SerializeField] Vector2 initialSize;
    

    // Start is called before the first frame update
    void Start()
    {
        initialSize = transform.localScale;
        Close();
    }


    public void Close()
    {
        if (isOpen)
        {
            isOpen = false;
            transform.localScale = transform.localScale.y * Vector2.up + Vector2.right * 0.03f;
        }
    }

    public void Open()
    {
        if (!isOpen)
        {
            isOpen = true;
            transform.localScale = initialSize;
        }
    }
}
