using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent navMeshAgent;

    //Animation
    private Animator _animator = null;
    private int runningParam = Animator.StringToHash("isRunning");
    private int shootingParam = Animator.StringToHash("isShooting");

    //Targeting
    public Transform target;
    public bool isChasing = false;
    public Vector3 offset = new Vector3(-2f, -2f, 0f);

    //Projectible
    public GameObject projectible;
    public Transform projectiblePoint;

    //Patrol
    public Transform[] waypoints;
    private int destPoint = 0;

    public ParticleSystem tiro;
    public AudioSource somTiro;


    private float shotDelay = 0;



    void Start()
    {
        _animator = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        GotoNextPoint();
    }

    void Update()
    {
        //controle animação
        if(navMeshAgent.isStopped == false)
        {
            _animator.SetBool(runningParam, true);
        } else
        {
            _animator.SetBool(runningParam, false);
        }
        

        float distance = Vector3.Distance(navMeshAgent.transform.position, target.position);
        shotDelay += Time.deltaTime;

        //ve a distancia entre o alvo e ele
        if (distance > 6 && distance < 20 )
        {
            navMeshAgent.isStopped = false;
            isChasing = true;
            navMeshAgent.SetDestination(target.position - offset);
        } else if(distance <= 6)
        {
            //quando esta perto o suficiente começa a atirar
            isChasing = false;
            navMeshAgent.isStopped = true;
            if (shotDelay > 2)//atira a cada 2 segundos
            {
                _animator.transform.LookAt(target);
                _animator.SetBool(shootingParam, true);
                Rigidbody rb = Instantiate(projectible, projectiblePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 60f, ForceMode.Impulse);
                rb.AddForce(transform.up, ForceMode.Impulse);
                tiro.Play();
                somTiro.Play();
                shotDelay = 0;
                _animator.SetBool(shootingParam, false);
            }

        } else if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f) 
        {
            isChasing = false;
            //se nao estiver perseguindo e estiver proximo do waypoint move para o proximo
            GotoNextPoint();
        }
    }

    void GotoNextPoint()
    {
        //se nao tiver pontos nao faz nada
        if(waypoints.Length == 0)
        {
            return;
        }

        //coloca o destino para o proximo ponto
        navMeshAgent.destination = waypoints[destPoint].position;

        //coloca o proximo ponto do array, com o condicional de voltar para o 0 quando passar por todos
        destPoint = (destPoint + 1) % waypoints.Length;
    }

}
