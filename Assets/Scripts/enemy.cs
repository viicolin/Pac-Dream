using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum EnemyState { Chasing, Searching, Attacking }
    public EnemyState currentState;

    private NavMeshAgent _AIAgent;
    private Transform _playerTransform;
    private Vector3 _playerLastPosition;

    [Header("Rang de visió")]
    [SerializeField] private float _visionRange = 20;
    [SerializeField] private float _visionAngle = 120;

    [Header("Paràmetres de cerca")]
    [SerializeField] private float _searchWaitTime = 10;
    [SerializeField] private float _searchRadius = 10;

    private float _searchTimer;

    [Header("Objectes per activar/desactivar")]
    [SerializeField] private GameObject[] activarQuanPersegueix;
    [SerializeField] private GameObject[] desactivarQuanPersegueix;

    void Awake()
    {
        _AIAgent = GetComponent<NavMeshAgent>();
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
        currentState = EnemyState.Searching;
        ActualitzarObjectesPerEstat(false);
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Chasing: Chase(); break;
            case EnemyState.Searching: Search(); break;
            case EnemyState.Attacking: Attack(); break;
        }
    }

    void Chase()
    {
        ActualitzarObjectesPerEstat(true);

        if (!OnRange())
        {
            _playerLastPosition = _playerTransform.position;
            currentState = EnemyState.Searching;
            _searchTimer = 0;
            ActualitzarObjectesPerEstat(false);
            return;
        }

        if (Vector3.Distance(transform.position, _playerTransform.position) < 2.0f)
        {
            currentState = EnemyState.Attacking;
            return;
        }

        _AIAgent.destination = _playerTransform.position;
    }

    void Search()
    {
        if (OnRange())
        {
            currentState = EnemyState.Chasing;
            return;
        }

        _searchTimer += Time.deltaTime;

        if (!_AIAgent.hasPath || _AIAgent.remainingDistance < 0.5f)
        {
            Vector3 randomDir = Random.insideUnitSphere * _searchRadius;
            randomDir.y = 0;
            Vector3 destination = transform.position + randomDir;

            if (NavMesh.SamplePosition(destination, out NavMeshHit navHit, _searchRadius, NavMesh.AllAreas))
            {
                if (Vector3.Distance(navHit.position, _AIAgent.destination) > 1f)
                {
                    _AIAgent.SetDestination(navHit.position);
                }
            }
        }

        if (_searchTimer > _searchWaitTime)
        {
            _searchTimer = 0;
        }
    }

    void Attack()
    {
        Debug.Log("L'enemic ataca!");
        ActualitzarObjectesPerEstat(true);

        Player player = _playerTransform.GetComponent<Player>();
        if (player != null)
        {
            player.RebreDany();
        }

        currentState = EnemyState.Chasing;
    }

    bool OnRange()
    {
        Vector3 directionToPlayer = _playerTransform.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > _visionRange || angleToPlayer > _visionAngle / 2f)
            return false;

        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, distanceToPlayer) &&
            hit.collider.CompareTag("Player"))
        {
            _playerLastPosition = _playerTransform.position;
            return true;
        }

        return false;
    }

    void ActualitzarObjectesPerEstat(bool perseguint)
    {
        foreach (GameObject obj in activarQuanPersegueix)
        {
            if (obj != null) obj.SetActive(perseguint);
        }

        foreach (GameObject obj in desactivarQuanPersegueix)
        {
            if (obj != null) obj.SetActive(!perseguint);
        }
    }
}