using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController = null;
    private Animator _animator = null;
    public Camera fpsCam; 

    //arma
    public float damage = 10f;
    public float weaponRange = 100.0f;
    public TMPro.TextMeshProUGUI munition;
    private int _munition = 10;
    public float shootTime = 0.9f;
    public float reloadTime = 1.5f;
    public ParticleSystem tiro;
    public AudioSource somTiro;

    //movimentacao
    private Vector3 _direction = Vector3.zero;
    public float speed = 0.0f;
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
        munition.text = _munition.ToString();
    }

    void Update()
    {
        //Rotinas de acao do Jogador
        Movement();

        //Somente pode atirar se nao tiver recarregando e n�o atirou nos ultimos "shootTime" segundos
        if (Input.GetButtonDown("Fire1") && IsReloading == false && IsShooting == false)
        {
            StartCoroutine(Shoot());
        }

        //A��o Correr
        Run();

        //Somente pode recarregar se n�o tiver recarregando e n�o estiver atirando
        if (Input.GetKey(KeyCode.R) && IsShooting == false && IsReloading == false)
        {
            StartCoroutine(Reload());
        }

    }

    IEnumerator Shoot()
    {
        
            if(_munition <= 0 && _munition != 10) {
                 StartCoroutine(Reload()); //se tentar atirar sem muni��o recarrega
            } else {
                IsShooting = true;
            tiro.Play();
            somTiro.Play();
            _animator.SetBool(shootingParam, IsShooting);
            _munition = _munition - 1;
                
                munition.text = _munition.ToString();
                RaycastHit hit;
                if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, weaponRange))
                {
                  Debug.Log(hit.transform.name);
                  Target target = hit.transform.GetComponent<Target>();
                  if(target != null)
                    {
                        target.TakeDamage(damage);
                    }
                }
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

    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if(IsRunning)
        {
            Vector3 movement = transform.forward * vertical + transform.right * horizontal;
            _characterController.Move(movement * Time.deltaTime * (speed + 3));
        }
        else
        {
            Vector3 movement = transform.forward * vertical + transform.right * horizontal;
            _characterController.Move(movement * Time.deltaTime * speed);
        }

        
    }


}
