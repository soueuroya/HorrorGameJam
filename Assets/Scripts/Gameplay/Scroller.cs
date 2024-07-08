using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scroller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] List<GameObject> objects;
    int current = 0;
    bool isMouseOver = false;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            objects[current].SetActive(false);
            current--;
            if (current < 0)
            {
                current = objects.Count - 1;
            }
            objects[current].SetActive(true);
        }
        else if (scroll < 0f)
        {
            objects[current].SetActive(false);
            current++;
            if (current > objects.Count - 1)
            {
                current = 0;
            }
            objects[current].SetActive(true);
        }
    }

    public void SetCurrent(int newCurrent)
    {
        if (current == newCurrent)
        {
            return;
        }

        objects[current].SetActive(false);
        current = newCurrent;
        objects[current].SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }
}
