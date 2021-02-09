using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemController : MonoBehaviour
{
    [System.Serializable]
    public class DropItem
    {
        public PickableObject item;
        public int chance;
    }

    public DropItem[] allItems;

    int randomRange;

    private void Start()
    {
        randomRange = 0;
        foreach (var dropItem in allItems)
        {
            randomRange += dropItem.chance;
        }
    }

    public void TriggerDrop(Vector3 dropPosition)
    {
        int randomNum = Random.Range(1, randomRange + 1);

        int currentChance = 0;

        foreach (var dropItem in allItems)
        {
            currentChance += dropItem.chance;
            if (randomNum <= currentChance)
            {
                if (dropItem.item != null)
                    Instantiate(dropItem.item, dropPosition, Quaternion.identity);
                break;
            }
        }
    }

}
