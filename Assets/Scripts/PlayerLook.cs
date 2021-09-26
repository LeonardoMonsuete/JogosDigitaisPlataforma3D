using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;
    float _verticalAngle, _horizontalAngle;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _verticalAngle = 0.0f;
        _horizontalAngle = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {


        // Rotacao do jogador
        float turnPlayer = Input.GetAxis("Mouse X") * mouseSensitivity;
        _horizontalAngle = _horizontalAngle + turnPlayer;

        if (_horizontalAngle > 360) _horizontalAngle -= 360.0f;
        if (_horizontalAngle < 0) _horizontalAngle += 360.0f;

        Vector3 currentAngles = transform.localEulerAngles;
        currentAngles.y = _horizontalAngle;
        transform.localEulerAngles = currentAngles;



        // olhar para cima e para baixo
        var turnCam = -Input.GetAxis("Mouse Y");
        turnCam = turnCam * mouseSensitivity;
        _verticalAngle = Mathf.Clamp(turnCam + _verticalAngle, -89.0f, 89.0f);
        currentAngles = transform.localEulerAngles;
        currentAngles.x = _verticalAngle;
        transform.localEulerAngles = currentAngles;




        // //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        // float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        // xRotation -= mouseY;
        
        // xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // //transform.Rotate(Vector3.up * mouseX);




    }
}
