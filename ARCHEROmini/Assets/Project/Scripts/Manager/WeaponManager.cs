using System.Collections.Generic;
using UnityEngine;

public class WeaponManager 
{
    public List<BaseMonster> Monsters;

    public WeaponManager()
    {
        Monsters = new List<BaseMonster>();
    }

    public BaseMonster FindTargetMonster(Vector3 position)
    {
        BaseMonster monster = new BaseMonster();
        float minDistance = 999f;
        foreach (var obj in Monsters)
        {
            float distance = Vector3.Distance(position, obj.transform.position);
            if (distance < minDistance)
            {
                monster = obj;
                minDistance = distance;
            }
        }
        return monster;
    }
    
    public bool Check()
    {
        return true;
    }
}
