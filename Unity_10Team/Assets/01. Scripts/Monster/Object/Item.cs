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
        Debug.Log("��Ʈ�� ����");
        if (other.CompareTag("Player"))
        {
            Debug.Log("CompareTag ����");
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("OnPickUp ����");
                OnPickUp(player);
            }
        }
    }

    public void OnPickUp(Player player)
    {
        if (itemType == ItemType.Experience)
        {
            //player.AddExperience(experienceValue);
        }
        else if (itemType == ItemType.Gold)
        {
            //player.AddGold(goldValue);
        }
        Debug.Log($"{experienceValue} ����ġ ȹ��    {goldValue} ��� ȹ��");
        MonsterDropItem.Instance.ReturnItem(this);
    }
}
