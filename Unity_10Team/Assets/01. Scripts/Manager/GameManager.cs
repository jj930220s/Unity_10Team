using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public PlayerWealth wealth;
    public PlayerStatus pStat => player.pStat;

    public float gameStartTime;
    public int score;

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        Time.timeScale = 1f;
        PlayerWealth savedWealth = DataSave<PlayerWealth>.LoadData("wealthData.json");
        if (savedWealth != default(PlayerWealth))
            wealth = savedWealth;
        wealth.Init();
        //player.pStat.Init();

        gameStartTime = Time.time;
        score = 0;
    }

    public void AddScore(int add)
    {
        score += add;
    }
}
