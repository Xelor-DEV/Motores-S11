using System.Collections;
using UnityEngine;
using DG.Tweening;
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float translateTime;
    [SerializeField] private float waitTime;
    [SerializeField] private Ease animationType;
    [SerializeField] private int currentPoint = 0;
    [SerializeField] private int damage;
    public int Damage
    {
        get
        {
            return damage;
        }
    }

    void Start()
    {
        MoveToNextPatrolPoint();
    }

    void MoveToNextPatrolPoint()
    {
        transform.DOMove(patrolPoints[currentPoint].position, translateTime).SetEase(animationType).OnComplete(() => 
        {
                StartCoroutine(WaitAndMove());
        });
    }

    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(waitTime);
        if (currentPoint + 1 < patrolPoints.Length)
        {
            currentPoint = currentPoint + 1;
        }
        else
        {
            currentPoint = 0;
        }
        MoveToNextPatrolPoint();
    }
}
