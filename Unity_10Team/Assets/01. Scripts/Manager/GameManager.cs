using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerWealth wealth;
    public PlayerState pStat;

    // Start is called before the first frame update
    void Awake()
    {
        pStat.Init();
    }
}
