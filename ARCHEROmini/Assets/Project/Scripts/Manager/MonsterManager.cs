using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MonsterManager
{
    public event Action OnClearMonster;

    List<BaseMonster> _monsters;

    public MonsterManager()
    {
        _monsters = new List<BaseMonster>();
    }

    public void Add(BaseMonster monster)
    {
        monster.ID = _monsters.Count;
        _monsters.Add(monster);

        monster.OnDeath += OnMonsterDeath;
    }

    public bool FindTarget(Vector3 position, out BaseMonster monster)
    {
        bool result = false;
        BaseMonster temp = null;

        float minDistance = float.MaxValue;
        int groundMask = LayerMask.GetMask("Ground");

        for (int i = 0; i < _monsters.Count; ++i)
        {
            BaseMonster obj = _monsters[i];

            Vector3 start = position;
            Vector3 direction = obj.transform.position - position;
            float distance = direction.sqrMagnitude;

            if (distance < minDistance && !Physics.Raycast(start, direction, 1f, groundMask))
            {
                result = true;
                temp = obj;
                minDistance = distance;
            }
        }

        monster = temp;
        return result;
    }

    void OnMonsterDeath(BaseMonster monster)
    {
        monster.OnDeath -= OnMonsterDeath;

        _monsters[_monsters.Count - 1].ID = monster.ID;

        _monsters[monster.ID] = _monsters[_monsters.Count - 1];
        _monsters.RemoveAt(_monsters.Count - 1);

        if (_monsters.Count <= 0)
        {
            OnClearMonster.Invoke();
        }
    }
}
