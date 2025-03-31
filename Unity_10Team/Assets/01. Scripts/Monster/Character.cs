using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Singleton<Character>
{
    public void AddExperience(int amount)
    {
        Debug.Log("°æÇèÄ¡ È¹µæ: " + amount);
    }

    public void AddGold(int amount)
    {
        Debug.Log("°ñµå È¹µæ: " + amount);

    }
}
