using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int _speedID = Animator.StringToHash("Speed");
    private CharacterController _characterController = null;
    private Animator _animator = null;

    private Vector2 _direction = Vector2.zero;

    public float speed = 5.0f;

    public float maxSpeed = 10.0f;

    [Range(0.0f, 1.0f)]
    public float normalizedSpeed = 0.0f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _direction.Normalize();

        Vector3 moviment = new Vector3(_direction.x, transform.position.y, _direction.y) * speed * Time.deltaTime;
        moviment = transform.TransformDirection(moviment);
        _characterController.Move(moviment);


        normalizedSpeed = _direction.magnitude;
    }
}
