using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HP : MonoBehaviour
{
    [SerializeField]
    private int totalHP;

    [SerializeField]
    private int currentHP;

    public bool isEnemy;

    [SerializeField]
    private GameObject hitParticleSystemPrefab;

    [SerializeField] AudioSource deathSound;

    void OnEnable()
    {
        currentHP = totalHP;
    }

    public void ReduceHP(int damageAmount)
    {
        currentHP -= damageAmount;

        if (currentHP <= 0)
        {
            Dead();
        }
    }

    #region VITALINFORMATION
    /*
     *                                                                                                    




                                        .:^~!!77JJ!~:                                               
                                      .^~~~~!77?J5Y?7!^                                             
                                     .^^:^^~~7????7????7:                                           
                                    .:::::^~!!!!!!77?????:                                          
                                    ^~~~~~~!!7777!7?YYYYYJ~.                                        
                                   .~777?!!7?JYYYYYPGGP5JYY                                         
                                   :77JYJ?5YJPP5YPJJG5Y?7?!                                         
                                   :777Y?!7?!7J??Y!!?YJJ??!.                                   ..   
                                   ^7?!?7!~~!7??J7!!7?J?777~.                          :^^.:!??!^   
                                  .^!!~!!!!~~~!77!7?????77?7~.                      .^!~:^7JJJ?!.   
                                  :~!!~77J!!~~!7!!7??J????J7!~:                  .:^!?7~?YJ7~:      
                                 .^!7!!775!!!!!7!!77?JJ??YJ?7!!~:            .::^~7???JJ!^.         
                                 ^^!!!7???J~!!777!7?JJ?JY5J?7!~!~:::       .^~~~!JYYY?~.            
                                 ^~~~!7?YYY!~~!!77JJJ??JYY?77!~~!!YBY:     ^~~!?Y5J~:               
                                 :~~!!!?JYYJ!!!!!77?777????777!!!?J#&B~   :~~!?J7.                  
     :::...                   ..::!77!!!77???77!777?777!7?7!77?77?P#&&#?  ^!!7?Y?^                  
     ..:~!~~^^::::::.......!5PGY?777!7?7!!!!!YGPPPGP5?7!77?7!777J5P#&&#&J P######Y                  
          ::^~!!!!~~^~~~!!7JB&PJY??7777?77??!7B&&&BGYJ?77????????BGB&&&&#J?JYYPGG~                  
               .:^!7!!~~~!!7?#GPPYJ?77??77?J5?J&&#BPY5J?YJ?Y5JYJJG#B#&&&&G~~~!77!                   
                    .:^~~~!77P#BGP5Y??J5?77YBYP&&#BGB5PYY?JY55YY5G&##&&&&P~~!7777.                  
                       !~^~77Y@&&#P5Y?JYP??JGB####&#BPGYY?YYPPY5P#&#&&&&&Y~~!!7??~                  
                      :5~^~!7J&&&@#G57?YPPJY5B#&&#&&BPG55JYPG5YGB#&&#&&&&J~~!!7??7                  
                      7P~^~!77B@&&&&#J?YPGGPG##&&&&##GGPG55BG5G#&####&&&B?~~!77???:                 
                      ^Y~~~!775@&&&&&5YPGBB&&&####&&&&#######BB###&&#&BPPJ!!!77???~                 
                      :7~~~!7?J&&&&&&BYB##&&###########################G5Y?7!77???7                 
                      .~~~~!7??B@@&&##BB###&#####BBBBB#&##########&##&&GYYJ?77?JJJ?                 
                      :~~~~!7??P&@&&#####GG###BB#############&##&&&&&&#BYYYY???JJJ!                 
                      ~~~~~!7??JP#@&###&#PGBBB#&&&&&&&&&&&&&&&&&&&&&&&#B5YYYJ?JJJ?.                 
                     ^!~~~~!77?J5B@&###&#BBB#&&&&&&&&######&&&&&&&&&&&7JGP5YJ??JY!                  
                    .!!!~~!!77?J5P#@##&BBBB#&&###&#&&&&&#&&&&&&&&&&&&&~ ^JPP5JJJ!.                  
                    ^!!!~!!!77JJYPG@&&&#BB#&&#######&&#&&&#&&&&&&&&&&@!   .:^^:.                    
                    ^!!!!!!!7?JYY5G&@@#####&########&&#&&&#&&&&&&&@&&@7                             
                    .!7!!!!!7?JYY5B@@@###&#########&&&#&&&#&&&&&&&@@&@!                             
                     .!!!!!77JYY5G&@@&#BGBG###&&&&##&&&##&&&&&&&&&&@&&^                             
                       ~!!!7?JYYP&@@@&#BGBB####&##&###&&##&&&&&&&&&@@G                              
                       .~777?J5G&@@&&&&&###BBB##&&##&&&&&&&&&&&&&&&&@J                              
                         YGPGB#&@&&&&&&&&&##BBB#####&&&&##&&&&&&&&&&@7                              
                       .5&&&&&&&&&&&&&&&&&&&##BBB###&&&&&##&&&&&#&&&@!                              
                       7@&&&&&&&&&&&&&&&&&&&&&####B##&&&&&&##&&&&&&&@!                              
                       Y@&&&&&&&&&&&&&&&&&&&&&&&&&&#&&&&&&&&&&&&&&&&@7                              
                      7&&&&&&&&&&&&&&&&&&&&&&#&&&###&&&&&&&&&&&&&&&&&!                              
                     :#&&&&&&&&&&&&&&&&##############&&&&&&&&&&&&&&&B.  
     * \
     */
    #endregion
   
    
    public string GetCurrentHP()
{
    return currentHP.ToString();
}
private void Dead()
{
    if(isEnemy)
    {
        Instantiate(hitParticleSystemPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSound.clip, transform.position);
        gameObject.GetComponent<PooledObjectBehaviour>().ReturnToPool();


    }

    else
    {
        Monobehaviours.Characters.PlayerController pc = GetComponent<Monobehaviours.Characters.PlayerController>();
        pc.isDead = true;
        GameManager.Instance.ChangeGameState(GameManager.GameState.gameEnding);
    }
}
}
