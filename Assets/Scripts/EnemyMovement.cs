using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent navMeshAgent;

    //Animation
    private Animator _animator = null;
    private int speedParam = Animator.StringToHash("Speed");

    //Targeting
    public Transform target;
    public bool isChasing = false;
    public Vector3 offset = new Vector3(-2f, -2f, 0f);

    //Patrol
    public Transform[] waypoints;
    private int destPoint = 0;

    void Start()
    {
        _animator = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        GotoNextPoint();
    }

    void Update()
    {
        _animator.SetFloat(speedParam, navMeshAgent.speed);
        if (isChasing)
        {
            navMeshAgent.SetDestination(target.position - offset);
        } else if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f) 
        {
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
