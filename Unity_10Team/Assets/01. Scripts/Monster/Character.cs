using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Singleton<Character>
{
    public void AddExperience(int amount)
    {
        Debug.Log("����ġ ȹ��: " + amount);
    }

    public void AddGold(int amount)
    {
        Debug.Log("��� ȹ��: " + amount);

    }
}
