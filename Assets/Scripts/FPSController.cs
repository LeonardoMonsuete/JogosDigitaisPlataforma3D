// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FPSController : MonoBehaviour
// {
//     [Header("Perspective Settings")]
//     public Camera mainCamera;
//     public Transform body;
//     [Space]
//     [Header("Control Settings")]
//     public float mouseSensitivity = 2.4f;
//     public float playerSpeed = 5.0f;
//     [Space]
//     float _verticalAngle, _horizontalAngle;
//     public float speed = 0.0f;

//     CharacterController _characterController;

//     public float weaponRange = 100.0f;
//     public float fireRatio = 0.3f;

//     // Novo parte do script
//     private Animator animator;




//     // Start is called before the first frame update
//     void Start()
//     {
//         Cursor.lockState = CursorLockMode.Locked;
//         Cursor.visible = false;

//         mainCamera = Camera.main;
//         _characterController = GetComponent<CharacterController>();
//         _verticalAngle = 0.0f;
//         _horizontalAngle = transform.localEulerAngles.y;

//         animator = GetComponent<Animator>();

//     }

//     // Update is called once per frame
//     void Update()
//     {
//         Movement();
//         Shoot();

//     }


//     private void Movement()
//     {
//         speed = 0;
//         Vector3 move = Vector3.zero;

//         // Calculo do movimento do personagem
//         move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
//         if (move.sqrMagnitude > 1.0f)
//             move.Normalize();

//         move = move * playerSpeed * Time.deltaTime;

//         move = transform.TransformDirection(move);
//         _characterController.Move(move);

//         // Rotacao do jogador
//         float turnPlayer = Input.GetAxis("Mouse X") * mouseSensitivity;
//         _horizontalAngle = _horizontalAngle + turnPlayer;

//         if (_horizontalAngle > 360) _horizontalAngle -= 360.0f;
//         if (_horizontalAngle < 0) _horizontalAngle += 360.0f;

//         Vector3 currentAngles = transform.localEulerAngles;
//         currentAngles.y = _horizontalAngle;
//         transform.localEulerAngles = currentAngles;

//         // olhar para cima e para baixo
//         var turnCam = -Input.GetAxis("Mouse Y");
//         turnCam = turnCam * mouseSensitivity;
//         _verticalAngle = Mathf.Clamp(turnCam + _verticalAngle, -89.0f, 89.0f);
//         currentAngles = body.localEulerAngles;
//         currentAngles.x = _verticalAngle;
//         body.localEulerAngles = currentAngles;
//     }

//     private void Shoot()
//     {
//         if (Input.GetButtonDown("Fire1"))
//         {
//             RaycastHit hit;
//             Physics.Raycast(mainCamera.transform.position,
//                             mainCamera.transform.forward,
//                             out hit, weaponRange);

//             //Zombie zombie = hit.transform.GetComponent<Zombie>();
//             //if (zombie != null)
//             //    zombie.TakeHit(25.0f);
//         }
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
       [Header("Perspective Settings")]
    public Camera mainCamera;
    public Transform body;
    [Space]
    [Header("Control Settings")]
    public float mouseSensitivity = 2.4f;
    public float playerSpeed = 5.0f;
    [Space]
    float _verticalAngle, _horizontalAngle;
    public float speed = 0.0f;

    CharacterController _characterController;

    public float weaponRange = 100.0f;
    public float fireRatio = 0.3f;

    // Novo parte do script
    private Animator animator;




    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mainCamera = Camera.main;
        _characterController = GetComponent<CharacterController>();
        _verticalAngle = 0.0f;
        _horizontalAngle = transform.localEulerAngles.y;

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();

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

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Physics.Raycast(mainCamera.transform.position,
                            mainCamera.transform.forward,
                            out hit, weaponRange);

            //Zombie zombie = hit.transform.GetComponent<Zombie>();
            //if (zombie != null)
            //    zombie.TakeHit(25.0f);
        }
    }
}