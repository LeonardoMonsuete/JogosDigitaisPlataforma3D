using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController = null;
    private Animator _animator = null;

    //arma
    public float weaponRange = 100.0f;
    public Text munition;
    private int _munition = 10;
    public float shootTime = 0.9f;
    public float reloadTime = 1.5f;

    //movimentacao
    private Vector2 _direction = Vector2.zero;
    public float speed = 3.0f;
    public float maxSpeed = 10.0f;
    [Range(0.0f, 1.0f)]
    public float normalizedSpeed = 0.0f;
    public float maxTeste;

    //status acoes
    public bool IsShooting = false;
    public bool IsRunning = false;
    public bool IsReloading = false;
    public bool IsTesting = false;

    //parametros de animacao
    private int shootingParam = Animator.StringToHash("isShooting");
    private int runningParam = Animator.StringToHash("isRunning");
    private int reloadingParam = Animator.StringToHash("isReloading");

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Start()
    {

        _animator = GetComponent<Animator>();

        // GUI Municao
        munition = GameObject.Find("Municao/Text").GetComponent<Text>();
        munition.text = _munition.ToString();
    }

    void Update()
    {
        //Movimento do Jogador
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


        //Rotinas de acao do Jogador

        //Somente pode atirar se nao tiver recarregando e não atirou nos ultimos "shootTime" segundos
        Debug.Log(IsShooting);
        if (Input.GetButtonDown("Fire1") && IsReloading == false && IsShooting == false)
        {
            StartCoroutine(Shoot());
        }

        //Ação Correr
        Run();

        //Somente pode recarregar se não tiver recarregando e não estiver atirando
        if (Input.GetKey(KeyCode.R) && IsShooting == false && IsReloading == false)
        {
            StartCoroutine(Reload());
        }

    }

    IEnumerator Shoot()
    {
        
            if(_munition <= 0) {
                 StartCoroutine(Reload()); //se tentar atirar sem munição recarrega
            } else {
                IsShooting = true;
            _animator.SetBool(shootingParam, IsShooting);
            _munition = _munition - 1;
                Debug.Log(_munition);
                munition.text = _munition.ToString();
                RaycastHit hit;
                Physics.Raycast(transform.position,
                                transform.forward,
                                out hit, weaponRange);
                yield return new WaitForSecondsRealtime(shootTime); //delay de "shootTime" segundos entre tiros
            IsShooting = false;
            _animator.SetBool(shootingParam, IsShooting);
        }
        

        


    }

    private void Run()
    {
        //enquanto estiver com left shift pressionado ele tem o status de "isRunning"
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

    IEnumerator Reload() 
    {

            IsReloading = true;
            _animator.SetBool(reloadingParam, IsReloading);
            yield return new WaitForSecondsRealtime(reloadTime); //demora "reloadTime" segundos para recarregar
        _munition = 10;
            munition.text = _munition.ToString();
            IsReloading = false;
            _animator.SetBool(reloadingParam, IsReloading);


    }
}
