using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] public float _mouseSensetivity = 350f;
    [SerializeField] private float _minCameraView = -80f, _maxCameraView = 0f;

    private CharacterController _characterController;
    private Camera _camera;
    private float xRotation = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement();

        float mouseX = Input.GetAxis("Mouse X") * _mouseSensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensetivity * Time.deltaTime;

        xRotation -= mouseY- 20f;

        xRotation = Mathf.Clamp(xRotation, _minCameraView, _maxCameraView);
        // Debug.Log("mouse x" + mouseX);
        //_camera.transform.localRotation = Quaternion.Euler(xRotation,0,0);
   
     
        transform.Rotate(Vector3.up * mouseX * 6);

        Movement(xRotation, mouseY);

    }

    void Movement(float horizontal, float vertical)
    {
        Vector3 movement = transform.forward * vertical + transform.right * horizontal;
        _characterController.Move(movement * Time.deltaTime * _speed);
    }

}

