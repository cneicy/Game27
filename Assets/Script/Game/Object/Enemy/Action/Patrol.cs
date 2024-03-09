using UnityEngine;

namespace Script.Game.Object.Enemy.Action
{
    public class Patrol : MonoBehaviour
    {
        public Transform[] patrolPoints; // 巡逻路径点数组
        public float moveSpeed = 3f; // 移动速度
        public float chaseSpeed = 5f; // 追逐速度
        public float detectionRange = 5f; // 玩家检测范围
        public float returnThreshold = 0.1f; // 返回原点的阈值

        private Transform target; // 玩家的位置
        private int currentPointIndex = 0; // 当前巡逻路径点索引
        private Vector3 initialPosition; // 初始位置

        void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform; // 根据标签获取玩家对象
            initialPosition = transform.position; // 记录初始位置
        }

        void Update()
        {
            // 计算与玩家的距离
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            // 如果玩家在检测范围内
            if (distanceToPlayer < detectionRange)
            {
                // 切换到追逐状态
                transform.position =
                    Vector2.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
            }
            else
            {
                // 返回到初始位置
                ReturnToInitialPosition();
            }
        }

        void ReturnToInitialPosition()
        {
            // 如果已经返回到初始位置
            if (Vector2.Distance(transform.position, initialPosition) < returnThreshold)
            {
                // 在巡逻路径点之间移动
                Enemy();
            }
            else
            {
                // 返回到初始位置
                transform.position =
                    Vector2.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);
            }
        }

        void Enemy()
        {
            // 到达当前路径点
            if (Vector2.Distance(transform.position, patrolPoints[currentPointIndex].position) < 0.1f)
            {
                // 移动到下一个路径点
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            }

            // 移向当前路径点
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position,
                moveSpeed * Time.deltaTime);
        }

        // 绘制巡逻路径点
        private void OnDrawGizmos()
        {
            if (patrolPoints == null || patrolPoints.Length < 2)
                return;

            Gizmos.color = Color.red;
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                Gizmos.DrawSphere(patrolPoints[i].position, 0.1f);
                if (i < patrolPoints.Length - 1)
                {
                    Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
                }
                else
                {
                    Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[0].position);
                }
            }
        }
    }
}