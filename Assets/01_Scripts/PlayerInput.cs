using UnityEngine;

public static class PlayerInput
{
    private static float lookAngle = 0f;
    private static float tiltAngle = 0f;

    public static Vector3 GetMovementInput(Camera relativeCamera)
    {
        Vector3 moveVector;
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        if (relativeCamera != null)
        {
            // Calculate the move vector relative to camera rotation
            Vector3 scalerVector = new Vector3(1f, 0f, 1f);
            Vector3 cameraForward = Vector3.Scale(relativeCamera.transform.forward, scalerVector).normalized;
            Vector3 cameraRight = Vector3.Scale(relativeCamera.transform.right, scalerVector).normalized;

            moveVector = (cameraForward * verticalAxis + cameraRight * horizontalAxis);
        }
        else
        {
            // Use world relative directions
            moveVector = (Vector3.forward * verticalAxis + Vector3.right * horizontalAxis);
        }

        if (moveVector.magnitude > 1f)
        {
            moveVector.Normalize();
        }

        return moveVector;
    }

    public static Quaternion GetMouseRotationInput(float mouseSensitivity = 3f, float minTiltAngle = -75f, float maxTiltAngle = 45f)
    {
        //if (!Input.GetMouseButton(1))
        //{
        //    return;
        //}

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Adjust the look angle (Y Rotation)
        lookAngle += mouseX * mouseSensitivity;
        lookAngle %= 360f;

        // Adjust the tilt angle (X Rotation)
        tiltAngle += mouseY * mouseSensitivity;
        tiltAngle %= 360f;
        tiltAngle = MathfExtensions.ClampAngle(tiltAngle, minTiltAngle, maxTiltAngle);

        var controlRotation = Quaternion.Euler(-tiltAngle, lookAngle, 0f);
        return controlRotation;
    }

    public static bool GetSprintInput()
    {
        return Input.GetButton("Sprint");
    }

    public static bool GetJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }

    /*public static bool GetToggleWalkInput()
    {
        return Input.GetButtonDown("Toggle Walk");
    }*/

    public static Vector2 GetSlidingDirection(float startPosition)
    {
        if (Input.GetMouseButton(0))
        {
            return PlayerInput.GetSlideDirection(startPosition, 0.1f);
        }
        return Vector2.zero;
    }

    public static Vector2 GetSlideDirection(float originalXPos, float deadZoneInFloat = 0f)
    {
        float currentXPos = Input.mousePosition.x;

        if (originalXPos - deadZoneInFloat > currentXPos)
            return Vector2.left;
        else if (originalXPos + deadZoneInFloat < currentXPos)
            return Vector2.right;
        else
            return Vector2.zero;
    }

    public static Vector2 GetSwipeDirection(Vector2 originalXPos, float minDetectionDistance = 0f, float directionthreshold = 0.9f)
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 newPos = Input.mousePosition;

            if (Vector2.Distance(originalXPos, newPos) > minDetectionDistance)
            {
                Vector2 direction = VectorsMethods.GetDistanceFromAtoB(newPos, originalXPos).normalized;

                if (Vector2.Dot(Vector2.up, direction) > directionthreshold)
                {
                    Debug.Log("Up");
                    return Vector2.up;
                }
                else if (Vector2.Dot(Vector2.down, direction) > directionthreshold)
                {
                    Debug.Log("down");
                    return Vector2.down;
                }
                else if (Vector2.Dot(Vector2.left, direction) > directionthreshold)
                {
                    Debug.Log("left");
                    return Vector2.left;
                }
                else if (Vector2.Dot(Vector2.right, direction) > directionthreshold)
                {
                    Debug.Log("right");
                    return Vector2.right;
                }
                else
                    return Vector2.zero;
            }
        }
        return Vector2.zero;
    }
}
