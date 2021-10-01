using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Animator _animator = null;
    private int speedParam = Animator.StringToHash("Speed");


    public Transform target;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    public bool isChasing = false;
    public Vector3 offset = new Vector3(-2f, -2f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isChasing)
        {
            _animator.SetFloat(speedParam, navMeshAgent.speed);
            navMeshAgent.SetDestination(target.position - offset);
        }
    }

}
