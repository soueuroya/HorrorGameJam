using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float speed = 0;

    private Vector2 cameraPrevPosition;

    private void Start()
    {
        cameraPrevPosition = Camera.main.transform.position;
    }

    private void Update()
    {
        Vector2 cameraPosition = Camera.main.transform.position;
        Vector2 difference = cameraPosition - cameraPrevPosition;

        if (difference != Vector2.zero)
        {
            transform.Translate(difference * speed);
        }

        cameraPrevPosition = cameraPosition;
    }
}
