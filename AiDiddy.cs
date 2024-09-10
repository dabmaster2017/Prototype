using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiDiddy : MonoBehaviour
{
    public List<Transform> wayPoints;
    private NavMeshAgent Diddy;
    public GameObject Kong;
    public GameObject FadeScreen;
    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        Diddy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (i < wayPoints.Count)
        {
            Diddy.SetDestination(wayPoints[i].position);
        }

        if (wayPoints.Count == 0)
        {
            return;
        }

        float distance = Vector3.Distance(wayPoints[i].position, transform.position);
        float KongPos = Vector3.Distance(Kong.transform.position, transform.position);

        if (distance <= 3f)
        {
            i++;

            if (i >= wayPoints.Count)
            {
                i = wayPoints.Count - 1; 
                Diddy.isStopped = true;
            }
        }

        if (KongPos <= 3f)
        {
            FadeScreen.GetComponent<Animation>().Play("FadeOutAnim");
        }
    }
}
