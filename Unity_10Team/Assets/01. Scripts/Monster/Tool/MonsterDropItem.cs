using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Progress;

public class MonsterDropItem : Singleton<MonsterDropItem>
{
    public Item experienceItemPrefab;
    public Item goldItemPrefab;

    private ObjectPool<Item> experienceItemPool;
    private ObjectPool<Item> goldItemPool;

    void Awake()
    {
        experienceItemPool = new ObjectPool<Item>(experienceItemPrefab, 10, transform);
        goldItemPool = new ObjectPool<Item>(goldItemPrefab, 10, transform);
    }

    public Item GetExperienceItem()
    {
        return experienceItemPool.Get();
    }

    public Item GetGoldItem()
    {
        return goldItemPool.Get();
    }

    public void DropItems(Monster monster, int experienceGained, int goldGained)
    {
        if (Random.value <= 0.5f)
        {
            Item droppedExperience = GetExperienceItem();
            Vector3 experienceDropPosition = monster.transform.position + new Vector3(-1, 0, 0);
            droppedExperience.transform.position = experienceDropPosition;
            droppedExperience.gameObject.SetActive(true);
            droppedExperience.Initialize(ItemType.Experience, experienceGained);
        }

        if (Random.value <= 0.5f)
        {
            Item droppedGold = GetGoldItem();
            Vector3 goldDropPosition = monster.transform.position + new Vector3(1, 0, 0);
            droppedGold.transform.position = goldDropPosition;
            droppedGold.gameObject.SetActive(true);
            droppedGold.Initialize(ItemType.Gold, goldGained);
        }
    }

    public void ReturnItem(Item item)
    {
        if (item.itemType == ItemType.Experience)
        {
            experienceItemPool.Release(item);
        }
        else if (item.itemType == ItemType.Gold)
        {
            goldItemPool.Release(item);
        }
    }
}
