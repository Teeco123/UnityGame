using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Position")]
    public float amount = 0.02f;
    public float maxAmount = 0.06f;
    public float smoothAmount = 6f;

    [Header("Rotation")]
    public float rotationAmount = 4f;
    public float maxRotationAmount = 5f;
    public float smoothRotation = 12f;

    [Header("Walking Sway")]
    public float walkSway = 0.1f;
    public float swaySpeed = 1;
    public float smoothWalkSway = 10f;

    [Space]
    public bool rotationX = true;
    public bool rotationY = true;
    public bool rotationZ = true;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private float inputX,
        inputY,
        inputHorizontal,
        inputVertical;

    private void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            swaySpeed = 2;
        }
        else
        {
            swaySpeed = 1;
        }
        CalculateSway();
        MoveSway();
        TiltSway();
        WalkingSwayVertical();
        WalkingSwayHorizontal();
    }

    private void CalculateSway()
    {
        inputX = Input.GetAxis("Mouse X");
        inputY = Input.GetAxis("Mouse Y");

        inputHorizontal = Mathf.Abs(Input.GetAxis("Horizontal"));
        inputVertical = Mathf.Abs(Input.GetAxis("Vertical"));
    }

    private void MoveSway()
    {
        float moveX = Mathf.Clamp(inputX * amount, -maxAmount, maxAmount);
        float moveY = Mathf.Clamp(inputY * amount, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(moveX, moveY, 0);

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            finalPosition + initialPosition,
            Time.deltaTime * smoothAmount
        );
    }

    private void TiltSway()
    {
        float tiltY = Mathf.Clamp(inputX * rotationAmount, -maxRotationAmount, maxRotationAmount);
        float tiltX = Mathf.Clamp(inputY * rotationAmount, -maxRotationAmount, maxRotationAmount);

        Quaternion finalRotation = Quaternion.Euler(
            new Vector3(rotationX ? -tiltX : 0f, rotationY ? tiltY : 0f, rotationZ ? tiltY : 0f)
        );

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            finalRotation * initialRotation,
            smoothRotation * Time.deltaTime
        );
    }

    private void WalkingSwayVertical()
    {
        float swayVertical1 = Mathf.PingPong(
            Time.time * swaySpeed / 6,
            inputVertical * walkSway + 0.0001f
        );
        float swayVertical2 = Mathf.PingPong(
            Time.time * swaySpeed / 3,
            inputVertical * walkSway + 0.0001f
        );
        Vector3 finalSway = new Vector3(swayVertical1, swayVertical2, 0);

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            finalSway + initialPosition,
            Time.deltaTime * smoothWalkSway
        );
    }

    private void WalkingSwayHorizontal()
    {
        float swayHorizontal = Mathf.PingPong(
            Time.time / 6,
            inputHorizontal * walkSway / 2 + 0.0001f
        );
        float swayHorizonta2 = Mathf.PingPong(
            Time.time / 3,
            inputHorizontal * walkSway / 2 + 0.0001f
        );
        Vector3 finalSway = new Vector3(swayHorizontal, swayHorizonta2, 0);

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            finalSway + initialPosition,
            Time.deltaTime * smoothWalkSway
        );
    }
}
