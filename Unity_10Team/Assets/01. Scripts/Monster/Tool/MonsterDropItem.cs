using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Progress;

public class MonsterDropItem : Singleton<MonsterDropItem>
{
    public Item experienceItemPrefab;
    public Item goldItemPrefab;
    public Item healthItemPrefab;

    public int expDropCount;
    public int goldDropCount;
    public int healDropCount;
    public int maxDropCount;

    private ObjectPool<Item> experienceItemPool;
    private ObjectPool<Item> goldItemPool;
    private ObjectPool<Item> healItemPool;

    void Awake()
    {
        experienceItemPool = new ObjectPool<Item>(experienceItemPrefab, maxDropCount, transform);
        goldItemPool = new ObjectPool<Item>(goldItemPrefab, maxDropCount, transform);
        healItemPool = new ObjectPool<Item>(healthItemPrefab, maxDropCount, transform);

        expDropCount = 0;
        goldDropCount = 0;
        healDropCount = 0;
    }

    public Item GetExperienceItem()
    {
        return experienceItemPool.Get();
    }

    public Item GetGoldItem()
    {
        return goldItemPool.Get();
    }

    public Item GetHealItem()
    {
        return healItemPool.Get();
    }

    public void DropItems(Monster monster, int experienceGained, int goldGained, int healGained)
    {
        if (monster == null)
        {
            Debug.LogError("DropItems 호출 Monster가 null입니다.");
            return;
        }

        if (expDropCount <= maxDropCount && Random.value <= 0.5f)
        {
            Item droppedExperience = GetExperienceItem();
            Vector3 experienceDropPosition = monster.transform.position + new Vector3(-1, 0.25f, 0);
            droppedExperience.transform.position = experienceDropPosition;
            droppedExperience.gameObject.SetActive(true);
            droppedExperience.Initialize(ItemType.Experience, experienceGained);
            expDropCount++;
            Debug.Log(expDropCount);
        }

        if (goldDropCount <= maxDropCount && Random.value <= 0.5f)
        {
            Item droppedGold = GetGoldItem();
            Vector3 goldDropPosition = monster.transform.position + new Vector3(1, 0.25f, 0);
            droppedGold.transform.position = goldDropPosition;
            droppedGold.gameObject.SetActive(true);
            droppedGold.Initialize(ItemType.Gold, goldGained);
            goldDropCount++;
            Debug.Log(goldDropCount);
        }

        if (healDropCount <= maxDropCount && Random.value <= 0.1f)
        {
            Item droppedHeal = GetHealItem();
            Vector3 healDropPosition = monster.transform.position + new Vector3(1, 0.25f, 1);
            droppedHeal.transform.position = healDropPosition;
            droppedHeal.gameObject.SetActive(true);
            droppedHeal.Initialize(ItemType.Heal, healGained);
            healDropCount++;
            Debug.Log(healDropCount);
        }
    }

    public void ReturnItem(Item item)
    {
        if (item.itemType == ItemType.Experience)
        {
            expDropCount--;
            Debug.Log(expDropCount);
            experienceItemPool.Release(item);
        }
        else if (item.itemType == ItemType.Gold)
        {
            goldDropCount--;
            Debug.Log(goldDropCount);
            goldItemPool.Release(item);
        }
        else if(item.itemType == ItemType.Heal)
        {
            healDropCount--;
            Debug.Log(healDropCount);
            healItemPool.Release(item);
        }
    }
}
