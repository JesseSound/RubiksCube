using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBigCube : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public GameObject target;
    bool swiping = false;
    float speed = 200.0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            swiping = true;
        }

        if (swiping && Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();

            if (LeftSwipe(currentSwipe))
            {
                target.transform.Rotate(0, 90, 0, Space.World);
            }
            else if (RightSwipe(currentSwipe))
            {
                target.transform.Rotate(0, -90, 0, Space.World);
            }
            else if (UpSwipe(currentSwipe))
            {
                target.transform.Rotate(-90, 0, 0, Space.World);
            }
            else if (DownSwipe(currentSwipe))
            {
                target.transform.Rotate(90, 0, 0, Space.World);
            }

            swiping = false;
        }

        // Calculate the angular difference between the current rotation and the target rotation
        float angleDifference = Quaternion.Angle(transform.rotation, target.transform.rotation);
        if (angleDifference > 0.1f)
        {
            float step = speed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
        }
    }

    bool LeftSwipe(Vector2 swipe)
    {
        return currentSwipe.x < 0 && Mathf.Abs(currentSwipe.x) > Mathf.Abs(currentSwipe.y);
    }

    bool RightSwipe(Vector2 swipe)
    {
        return currentSwipe.x > 0 && Mathf.Abs(currentSwipe.x) > Mathf.Abs(currentSwipe.y);
    }

    bool DownSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0 && Mathf.Abs(currentSwipe.y) > Mathf.Abs(currentSwipe.x);
    }

    bool UpSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0 && Mathf.Abs(currentSwipe.y) > Mathf.Abs(currentSwipe.x);
    }
}
