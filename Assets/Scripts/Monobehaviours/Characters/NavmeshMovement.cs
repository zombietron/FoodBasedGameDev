using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(MeleAttack))]
public class NavmeshMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField]
    private float agentSpeed;

    MeleAttack atk;
    
    [SerializeField] 
    private GameObject selectedTarget;

    public UnityEvent<bool> inAttackRange;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = agentSpeed;
        atk = GetComponent<MeleAttack>();
    }

    private void OnEnable()
    {
        selectedTarget = SelectTargetPlayer();
        atk.Target = selectedTarget;
    }

    private GameObject SelectTargetPlayer()
    {
        GameObject[] target;
        target = GameObject.FindGameObjectsWithTag("Player");
        return target[Random.Range(0, target.Length)];
    }

    #region highlyImportant
    /*                                                   
                 ...::::::.                   
             .:^~!7777??777!^.                
           :^!7?YYYYJ???????7!:.              
         .^!7J55J7!!!!!!777???777!~^.         
        :~!?5PJ!!!!!!!!!77777???JJJ?!^:       
       ^!!?55?7!!!!!!!!77?JJJJJJJJJYJ7!^      
      ^7?YPG5P55J7!77??JYPPPPPPPPPP55J?7:     
    .^?P55Y??7?JY?BPPGYG5YJJJJJJY5G&&#PJ!.    
    :!G5557?7!!!?JP?JYY5J?7?JJYYYYYBBPGPJ~    
    ^7PPGPYYYY5555?7YG55Y5YPGGGPYJYB55BPPJ^   
   .~7YG5J7???JP577?Y5GBP5JJJJJJJYPY55GG55!   
   .~!?55JJYY55J!!7?Y55PP55YJJY555JJYYJY55J:  
   .^!?Y?!777?????J5P555Y??JJJJJ?JJYYYYYY5J~  
    ^!7J??77JJ?JJJ5PPP55YJ?777??JJYYY555YYY!. 
    ^~!????JJ?7!!!77??JJJJJJJJJYY55555GPYYJ!. 
   .^~!!77?J????????JJYYYYYYY55555Y555G5YJ?^. 
   :^~~77777?YGYYY5555GGBG55YYYYYYY55555YJ?^. 
  .^^~~7??77?JB&&&#####&&&5YJJJJYY5PBB5YYJ?^  
  .^^~~!?YJ777Y##BYJYG##GYJJJJYYY5PBBPYYYY?:  
..::^^~~^^~!?5P?77?JY5J?JY5YYJJJJY555PGBG5YYYJ?:  
!?J7!!~~~^~!7YG57!77???JJJYYJJJJJ555GBBGPYYJYJ?:  
PBBBP7~~~^~~!JPGY!77777???JJJJYY55PB#BBG5YYJJY5J: 
GBGGPY!~~~~~!?PPG?777777??JJJYPPPGB#&##G555YYPB##J
#&BG#B!!~~~~??PPGGJ??????JJYPGGGGB##&##GPGG5G&##@@
#BB#@B!!!!~!J?PBPGBPYYY55PGGGGGGB#&@@&#B##BB#&&&@@
GGB##J!7!!!7JJ5BBGGBG55P5PPGGGGB#&@@@@&@@&&&&&&&@&
BB&&G7?7!7!7JJ5PBBGBBGGGGGGGGGB#@@@@@@@@@&&&&@&@@&
 */
    #endregion


    void Update()
    {
        agent.SetDestination(selectedTarget.transform.position);
        
        if(agent.destination != null) {
            if (Vector3.Distance(agent.gameObject.transform.position,agent.destination) <=1f)
            {
                Debug.Log(Vector3.Distance(agent.gameObject.transform.position, agent.destination));
                inAttackRange.Invoke(true);
            }
        } 
    }
}
