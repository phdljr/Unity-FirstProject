using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ChasePlayer : MonoBehaviour
{
    //public GameObject gameover;
    GameObject[] players;
    Transform[] PlayerTrs;
    float[,] PlayerAndGhostDist;
    int ghostnum;
    private Transform ghostTr;
    private Transform targetTr;
    private int targetInt = 0;

    private NavMeshAgent nvAgent;

    // Start is called before the first frame update
    void Start()
    {
        string ThisGhost;
        ThisGhost = this.gameObject.name;
        if (ThisGhost == "JangSanBum")
        {
            ghostnum = 0;
        }
        if (ThisGhost == "VirginGhost")
        {
            ghostnum = 1;
        }
        if (ThisGhost == "Auduksini")
        {
            ghostnum = 2;
        }
        nvAgent = GetComponent<NavMeshAgent>();

        players = GameObject.FindGameObjectsWithTag("Player");
        PlayerTrs = new Transform[players.Length];
        PlayerAndGhostDist = new float[3, players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            PlayerTrs[i] = players[i].GetComponent<Transform>();
        }

        targetTr = PlayerTrs[0];
        StartCoroutine(SearchTarget());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SearchTarget()
    {
        while (true)
        {
            PlayerAndGhostDist[ghostnum, 0] = Mathf.Abs(Vector3.Distance(PlayerTrs[0].position, transform.position));

            yield return new WaitForSeconds(0.1f);


            for (int i = 0; i < players.Length; i++)
            {
                PlayerAndGhostDist[ghostnum, i] = Mathf.Abs(Vector3.Distance(PlayerTrs[i].position, transform.position));
            }
            targetTr = PlayerTrs[0];

            if (players.Length == 1)
            {

            }
            else
            {
                for (int i = 0; i < players.Length - 1; i++)
                {
                    if (PlayerAndGhostDist[ghostnum, targetInt] <= PlayerAndGhostDist[ghostnum, i + 1])
                    {
                        targetTr = PlayerTrs[targetInt];
                    }
                    else
                    {
                        targetInt = i + 1;
                        targetTr = PlayerTrs[targetInt];
                    }
                }
            }
            nvAgent.destination = targetTr.position;
            targetInt = 0;
        }
    }
}