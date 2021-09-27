using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int _speedID = Animator.StringToHash("Speed");
    private CharacterController _characterController = null;
    private Animator _animator = null;
    public float weaponRange = 100.0f;
    
    public Text munition;
    private int _munition = 10;

    private Vector2 _direction = Vector2.zero;
    private int shootingParam = Animator.StringToHash("isShooting");
    private int runningParam = Animator.StringToHash("isRunning");
    private int reloadingParam = Animator.StringToHash("isReloading");
    public float speed = 3.0f;

    public float maxSpeed = 10.0f;

    public float maxTeste;

    [Range(0.0f, 1.0f)]
    public float normalizedSpeed = 0.0f;
    public bool IsShooting = false;
    public bool IsRunning = false;
    public bool IsReloading = false;
    public bool IsTesting = false;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        munition = GameObject.Find("Municao/Text").GetComponent<Text>();
        munition.text = _munition.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _direction.Normalize();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector3 moviment = new Vector3(_direction.x, 0.0f, _direction.y) * (speed + 5) * Time.deltaTime;
            moviment = transform.TransformDirection(moviment);
            _characterController.Move(moviment);
            normalizedSpeed = _direction.magnitude;
        }
        else 
        {
            Vector3 moviment = new Vector3(_direction.x, 0.0f, _direction.y) * speed * Time.deltaTime;
            moviment = transform.TransformDirection(moviment);
            _characterController.Move(moviment);
            normalizedSpeed = _direction.magnitude;
        }
        
      
        Shoot();
        Run();

        if(Input.GetKey(KeyCode.R))
        {
            Reload();
        }
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(_munition <= 0) {
                 Reload();
            } else {
                IsShooting = true;
                _munition = _munition - 1;
                Debug.Log(_munition);
                munition.text = _munition.ToString();
                RaycastHit hit;
                Physics.Raycast(transform.position,
                                transform.forward,
                                out hit, weaponRange);

            }
        }
        else 
        {
            IsShooting = false;
        }

        

        _animator.SetBool(shootingParam, IsShooting);


    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            IsRunning = true;
        }
        else
        {
            IsRunning = false;
        }

        _animator.SetBool(runningParam, IsRunning);

    }

    private void Reload() 
    {

            IsReloading = true;
            _munition = 10;
            munition.text = _munition.ToString();
            IsReloading = false;

        _animator.SetBool(reloadingParam, IsReloading);
    }
}
