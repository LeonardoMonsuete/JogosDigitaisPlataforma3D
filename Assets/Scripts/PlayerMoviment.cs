using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{

    [Header("Perspective Settings")]
    public Camera mainCamera;
    public Transform body;
    [Space]
    [Header("Control Settings")]
    public float mouseSensitivity = 2.4f;
    public float playerSpeed = 5.0f;
    [Space]

    public float speed = 0.0f;

    CharacterController _characterController;

    public float weaponRange = 100.0f;
    public float fireRatio = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mainCamera = Camera.main;
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        Movement();

    }



    private void Movement()
    {
        speed = 0;
        Vector3 move = Vector3.zero;

        // Calculo do movimento do personagem
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (move.sqrMagnitude > 1.0f)
            move.Normalize();

        move = move * playerSpeed * Time.deltaTime;

        move = transform.TransformDirection(move);
        _characterController.Move(move);

     
    }
}
