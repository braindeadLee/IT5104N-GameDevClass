using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Rnadom Wander", menuName = "Enemy/Idle/Random Wander")]
public class EnenyIdleRandomWander : EnemyIdleSOBase
{
    [SerializeField] private float RandomMovementRange = 5f;
    [SerializeField] private float RandomMovementSpeed = 1f;

    private Vector3 _targetPos;
    private Vector3 _direction;
    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }
    override public void DoEnterLogic()
    {
        base.DoEnterLogic();

        _targetPos = GetRandomPointInCircle();
    }

    override public void DoExitLogic()
    {
        base.DoExitLogic();
    }

    override public void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if(enemy.IsAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }

        _direction = (_targetPos - enemy.transform.position).normalized;
        enemy.MoveEnemy(_direction * RandomMovementSpeed);

        if((enemy.transform.position - _targetPos).sqrMagnitude < 0.01f)
        {
            _targetPos = GetRandomPointInCircle();
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    private Vector3 GetRandomPointInCircle()
    {
        return enemy.transform.position + (Vector3)(Random.insideUnitCircle * RandomMovementRange);
    }
}

