using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager
{
    public event Action OnClearMonster;

    List<BaseMonster> _monsters;

    int _monstersCount;

    public MonsterManager()
    {
        _monsters = new List<BaseMonster>();
        _monstersCount = 0;
    }

    public void Release()
    {
        foreach (BaseMonster monster in _monsters)
        {
            monster.Release();
        }
        _monsters = new List<BaseMonster>();
        _monstersCount = 0;
    }

    public void Add(BaseMonster monster)
    {
        monster.ID = _monsters.Count;
        _monsters.Add(monster);
        _monstersCount += 1;
        monster.OnDeath += OnMonsterDeath;

        monster.OnClear += OnMonsterRelease;
    }

    public void OnMonsterDeath()
    {        
        _monstersCount -= 1;
    }

    public bool HasMonster()
    {
        return _monstersCount > 0;
    }

    public BaseMonster FindTarget(Vector3 position)
    {
        BaseMonster monster = null;
        float minDistance = float.MaxValue;

        for (int i = 0; i < _monsters.Count; ++i)
        {
            BaseMonster obj = _monsters[i];
            if (obj.IsDeath) continue;

            Vector3 direction = obj.transform.position - position;
            float distance = direction.sqrMagnitude;

            if (distance < minDistance)
            {
                monster = obj;
                minDistance = distance;
            }
        }
        return monster;
    }

    void OnMonsterRelease(BaseMonster monster)
    {
        _monsters[_monsters.Count - 1].ID = monster.ID;
        _monsters[monster.ID] = _monsters[_monsters.Count - 1];
        _monsters.RemoveAt(_monsters.Count - 1);

        if (_monsters.Count <= 0)
        {
            OnClearMonster.Invoke();
        }
    }
}
