using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public PlayerWealth wealth;
    public PlayerStatus pStat => player.pStat;

    // Start is called before the first frame update
    void Awake()
    {
        wealth.Init();
        //pStat.Init();
    }
}
