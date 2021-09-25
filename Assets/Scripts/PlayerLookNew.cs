using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookNew : MonoBehaviour
{

    float _verticalAngle, _horizontalAngle;
    public float mouseSensitivity = 2.4f;
    public Transform body;
    // Start is called before the first frame update
    void Start()
    {
        _verticalAngle = 0.0f;
        _horizontalAngle = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    void Look() 
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
        currentAngles = body.localEulerAngles;
        currentAngles.x = _verticalAngle;
        body.localEulerAngles = currentAngles;
    }
}
