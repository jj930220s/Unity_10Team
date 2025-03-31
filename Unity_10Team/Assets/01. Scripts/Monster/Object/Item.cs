using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Experience,
    Gold
}

public class Item : MonoBehaviour
{
    public ItemType itemType;
    public int experienceValue;
    public int goldValue;

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
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("¿ÂÆ®°Å ½ÇÇà");
        if (other.CompareTag("Player"))
        {
            Debug.Log("CompareTag ½ÇÇà");
            Character player = other.GetComponent<Character>();
            if (player != null)
            {
                Debug.Log("OnPickUp ½ÇÇà");
                OnPickUp(player);
            }
        }
    }

    public void OnPickUp(Character player)
    {
        if (itemType == ItemType.Experience)
        {
            player.AddExperience(experienceValue);
        }
        else if (itemType == ItemType.Gold)
        {
            player.AddGold(goldValue);
        }
        Debug.Log($"{experienceValue} °æÇèÄ¡ È¹µæ    {goldValue} °ñµå È¹µæ");
        MonsterDropItem.Instance.ReturnItem(this);
    }
}
