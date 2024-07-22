using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{
    private NavMeshAgent _agent;
    private List<PointsForHuman> _pointsForHuman;
    public HumanHouse _humanHouse;
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _pointsForHuman = new List<PointsForHuman>(FindObjectsOfType<PointsForHuman>());
        SetNextPointToMove();
        StartCoroutine(CheckIfReachedDestination());
    }
    private void SetNextPointToMove()
    {
        int rand=Random.Range(0, _pointsForHuman.Count);
        _agent.SetDestination(_pointsForHuman[rand].transform.position);
    }


    private IEnumerator CheckIfReachedDestination()
    {
        while (true)
        {
            if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                OnDestinationReached();
                yield break; 
            }
            yield return new WaitForSeconds(0.1f); // Чекаємо трохи перед наступною перевіркою
        }
    }

    private void OnDestinationReached()
    {
        SetNextPointToMove();
        StartCoroutine(CheckIfReachedDestination());
    }

}

