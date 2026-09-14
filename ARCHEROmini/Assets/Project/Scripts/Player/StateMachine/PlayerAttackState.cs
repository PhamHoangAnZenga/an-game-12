using UnityEngine;

public class PlayerAttackState : BaseState
{
    PlayerStats _stats;
    float _timer = 0;
    Vector3 _direction;
    BaseMonster _target;

    float _findTimer = 0;
    readonly float FINDDELAY = 0.36f;

    public PlayerAttackState(PlayerStats stats)
    {
        _stats = stats;
    }

    public override bool ConditionCheck()
    {
        return _stats.MonsterManager.HasMonster();
    }

    public override void EnterState()
    {
        FindTarget();

        _stats.Animator.SetFloat(PlayerStats.ATTACKSPEED, 2f / 3f / _stats.AttackTime);

        _stats.Animator.SetBool(PlayerStats.ISATTACK, true);

        _timer = Time.time + _stats.AttackTime;
    }

    public override void Update()
    {
        if (_findTimer < FINDDELAY || _target == null || _target.IsDeath)
        {
            FindTarget();
        }

        _direction = _target.transform.position - _stats.Transform.position;
        _direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(_direction);
        _stats.Transform.rotation = Quaternion.Slerp(_stats.Transform.rotation, targetRotation, _stats.RotateSpeed * Time.deltaTime);

        if (_timer < Time.time)
            Attack();
    }

    public override void ExitState()
    {
        _stats.Animator.SetBool(PlayerStats.ISATTACK, false);
        // Debug.Log("exit attack state");
    }

    public override string GetName()
    {
        return "attack";
    }

    void Attack()
    {
        Bullet bullet = Object.Instantiate(_stats.Weapon.BulletPrefab);

        bullet.transform.position = _stats.Transform.position;
        bullet.Fired(_stats.Transform.forward);

        _timer = Time.time + _stats.AttackTime;
    }

    // đoạn code ở đây có thể gặp vấn đề khi mục tiêu chết trước mà chưa bị thu gom thì hàm tìm kiếm vẫn có thể gọi ra được
    void FindTarget()
    {
        _target = _stats.MonsterManager.FindTarget(_stats.Transform.position);
        _findTimer = Time.time + FINDDELAY;
    }

    void OnTargetDie(BaseMonster monster)
    {
    }
}
