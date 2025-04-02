using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Progress;

public class MonsterDropItem : Singleton<MonsterDropItem>
{
    public Item experienceItemPrefab;
    public Item goldItemPrefab;

    public int ExpDropCount;
    public int GoldDropCount;
    public int maxDropCount;

    private ObjectPool<Item> experienceItemPool;
    private ObjectPool<Item> goldItemPool;

    void Awake()
    {
        experienceItemPool = new ObjectPool<Item>(experienceItemPrefab, maxDropCount, transform);
        goldItemPool = new ObjectPool<Item>(goldItemPrefab, maxDropCount, transform);

        ExpDropCount = 0;
        GoldDropCount = 0;
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
        if (monster == null)
        {
            Debug.LogError("DropItems 호출 Monster가 null입니다.");
            return;
        }

        if (ExpDropCount <= maxDropCount && Random.value <= 0.5f)
        {
            Item droppedExperience = GetExperienceItem();
            Vector3 experienceDropPosition = monster.transform.position + new Vector3(-1, 1, 0);
            droppedExperience.transform.position = experienceDropPosition;
            droppedExperience.gameObject.SetActive(true);
            droppedExperience.Initialize(ItemType.Experience, experienceGained);
            ExpDropCount++;
            Debug.Log(ExpDropCount);
        }

        if (GoldDropCount <= maxDropCount && Random.value <= 0.5f)
        {
            Item droppedGold = GetGoldItem();
            Vector3 goldDropPosition = monster.transform.position + new Vector3(1, 1, 0);
            droppedGold.transform.position = goldDropPosition;
            droppedGold.gameObject.SetActive(true);
            droppedGold.Initialize(ItemType.Gold, goldGained);
            GoldDropCount++;
            Debug.Log(GoldDropCount);
        }
    }

    public void ReturnItem(Item item)
    {
        if (item.itemType == ItemType.Experience)
        {
            ExpDropCount--;
            Debug.Log(ExpDropCount);
            experienceItemPool.Release(item);
        }
        else if (item.itemType == ItemType.Gold)
        {
            GoldDropCount--;
            Debug.Log(GoldDropCount);
            goldItemPool.Release(item);
        }
    }
}
