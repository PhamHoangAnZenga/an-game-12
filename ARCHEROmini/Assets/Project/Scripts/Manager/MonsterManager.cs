using System.Collections.Generic;
using UnityEngine;

public class MonsterManager
{
    public List<BaseMonster> Monsters;

    public MonsterManager()
    {
        Monsters = new List<BaseMonster>();
    }

    public bool FindTarget(Vector3 position, out BaseMonster monster)
    {
        bool result = false;
        BaseMonster temp = new BaseMonster();

        float minDistance = 999f;

        foreach (var obj in Monsters)
        {
            float distance = Vector3.Distance(position, obj.transform.position);

            Vector3 start = position;
            Vector3 direction = obj.transform.position - position;

            if (distance < minDistance && !Physics.Raycast(start, direction, distance, LayerMask.GetMask("Ground")))
            {
                result = true;
                temp = obj;
                minDistance = distance;
            }
        }

        Debug.Log(result);
        monster = temp;
        return result;
    }
    }
