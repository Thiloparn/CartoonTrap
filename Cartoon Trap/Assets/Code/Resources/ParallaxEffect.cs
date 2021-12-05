using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 previousCameraPos;
    private float spriteWidth;
    private float startPosition;
    public float parallaxMultiplier;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPos = cameraTransform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = transform.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - previousCameraPos.x) * parallaxMultiplier;
        float moveAmount = cameraTransform.position.x - (1 - parallaxMultiplier);
        transform.Translate(new Vector3(deltaX, 0, 0));
        previousCameraPos = cameraTransform.position;

        if (moveAmount > startPosition + spriteWidth)
        {
            transform.Translate(new Vector3(spriteWidth, 0, 0));
            startPosition = transform.position.x;
        }
        else if (moveAmount < startPosition - spriteWidth)
        {
            print(spriteWidth);
            transform.Translate(new Vector3(-spriteWidth, 0, 0));
            startPosition = transform.position.x;
        }
    }
}
