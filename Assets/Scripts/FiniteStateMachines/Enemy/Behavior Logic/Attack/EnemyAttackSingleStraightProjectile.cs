using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackSingleStraightProjectile", menuName = "Enemy/Attack/Single Straight Projectile")]
public class EnemyAttackSingleStraightProjectile : EnemyAttackSOBase
{
    [SerializeField] private Rigidbody2D BulletPrefab;
    [SerializeField] private float _timer;
    [SerializeField] private float _timeBetweenShots = 2f;

    [SerializeField] private float _exitTimer;
    [SerializeField] private float _timeTillExit = 2f;
    [SerializeField] private float _distanceToCountExit = 3f;
    [SerializeField] private float _bulletSpeed = 10f;

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }
    override public void DoEnterLogic()
    {
        base.DoEnterLogic();

    }

    override public void DoExitLogic()
    {
        base.DoExitLogic();
    }

    override public void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        enemy.MoveEnemy(Vector2.zero);

        if(_timer > _timeBetweenShots)
        {
            _timer = 0f;

            Vector2 dir = (playerTransform.position - enemy.transform.position).normalized;

            Rigidbody2D bullet = Object.Instantiate(BulletPrefab, enemy.transform.position, Quaternion.identity);
            bullet.linearVelocity = dir * _bulletSpeed;
        }

        if (Vector2.Distance(playerTransform.position, enemy.transform.position) > _distanceToCountExit)
        {
            _exitTimer += Time.deltaTime;

            if(_exitTimer > _timeTillExit)
            {
                enemy.StateMachine.ChangeState(enemy.ChaseState);
            }
        }
        else
        {
            _exitTimer = 0f;
        }

        _timer += Time.deltaTime;

    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }
}
