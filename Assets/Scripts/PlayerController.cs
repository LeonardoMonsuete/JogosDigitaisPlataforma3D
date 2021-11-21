using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController = null;
    private Animator _animator = null;
    public Camera fpsCam;

    //index cena
    int _currScene;

    //arma
    public float damage = 10f;
    public float weaponRange = 100.0f;
    public TMPro.TextMeshProUGUI munition;
    private int _munition = 10;
    public float shootTime = 0.9f;
    public float reloadTime = 1.5f;
    public ParticleSystem tiro;
    public AudioSource somTiro;

    //vida
    private float health;
    public TMPro.TextMeshProUGUI healthTxt;

    //mapa
    public Image mapImg;
    private bool mapDelay = false;

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
        health = 100;
        _characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        _currScene = SceneManager.GetActiveScene().buildIndex;
        _animator = GetComponent<Animator>();

        // GUI Municao
        munition.text = _munition.ToString();
    }

    void Update()
    {
        if (health > 0)
        {
            Movement();

            //Somente pode atirar se nao tiver recarregando e n�o atirou nos ultimos "shootTime" e não está correndo segundos
            if (Input.GetButtonDown("Fire1") && IsReloading == false && IsShooting == false && IsRunning == false)
            {
                StartCoroutine(Shoot());
            }

            //A��o Correr
            Run();

            //Somente pode recarregar se n�o tiver recarregando e n�o estiver atirando e não está correndo
            if (Input.GetKey(KeyCode.R) && IsShooting == false && IsReloading == false && IsRunning == false)
            {
                StartCoroutine(Reload());
            }

            //Abre e fecha o mapa
            if (Input.GetKey(KeyCode.M) && mapDelay == false)
            {
                StartCoroutine(ToggleMap());
            }
        } else
        {
            Waves.enemies = 0;
            SceneManager.LoadScene(++_currScene);
        }
        //Rotinas de acao do Jogador

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
                  TargetController target = hit.transform.GetComponent<TargetController>();
                Debug.Log(target);
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
            GetComponent<NavMeshAgent>().Move(movement * Time.deltaTime * (speed + 3));
        }
        else
        {
            Vector3 movement = transform.forward * vertical + transform.right * horizontal;
            GetComponent<NavMeshAgent>().Move(movement * Time.deltaTime * speed);
        }

        
    }

    IEnumerator ToggleMap()
    {
        mapDelay = true;
        mapImg.enabled = !mapImg.enabled;
        yield return new WaitForSecondsRealtime(0.2f);
        mapDelay = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag == "Enemy")
        {
            health = health - 20;
            healthTxt.text = health.ToString();
        }


    }


}
