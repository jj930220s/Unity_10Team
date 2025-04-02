using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Experience,
    Gold,
    Heal
}

public class Item : MonoBehaviour
{
    public ItemType itemType;
    public int experienceValue;
    public int goldValue;
    public int healValue;

    public void Initialize(ItemType type, int value)
    {
        itemType = type;

        switch (itemType)
        {
            case ItemType.Experience:
                experienceValue = value;
                break;
            case ItemType.Gold:
                goldValue = value;
                break;
            case ItemType.Heal:
                healValue = value;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("¿ÂÆ®°Å ½ÇÇà");
        if (other.CompareTag("Player"))
        {
            Debug.Log("CompareTag ½ÇÇà");
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("OnPickUp ½ÇÇà");
                OnPickUp(player);
            }
        }
    }

    public void OnPickUp(Player player)
    {
        if (itemType == ItemType.Experience)
        {
            player.pLevel.AddExp(experienceValue);
        }
        else if (itemType == ItemType.Gold)
        {
            //player.AddGold(goldValue);
            GameManager.Instance.wealth.AddWealth(WEALTHTYPE.Gold, goldValue);
        }
        else if(itemType == ItemType.Heal)
        {
            player.pStat.Heal(healValue);
        }
        Debug.Log($"{experienceValue} °æÇèÄ¡ È¹µæ    {goldValue} °ñµå È¹µæ");
        MonsterDropItem.Instance.ReturnItem(this);
    }
}
