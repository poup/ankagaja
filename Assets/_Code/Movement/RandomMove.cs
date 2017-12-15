using System.Collections;
using UnityEngine;

    public class RandomMove : MonoBehaviour
    {
        public float m_timeBeforeChangeDirectionMin;
        public float m_timeBeforeChangeDirectionMax;
        
        public float m_speedMin;
        public float m_speedMax;
        
        public float m_timeMovingMin;
        public float m_timeMovingMax;

        private Rigidbody2D m_rigidbody2D;


        void Start()
        {
            StartMove();
        }

        void StartMove()
        {
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            var waiting = Randomizer.Next(m_timeBeforeChangeDirectionMin, m_timeBeforeChangeDirectionMax);
            yield return new WaitForSeconds(waiting);
            var direction = Random.insideUnitCircle.normalized;

            var speed = Randomizer.Next(m_speedMin, m_speedMax);
            m_rigidbody2D.velocity = direction * speed;
            
            var movingTime = Randomizer.Next(m_timeMovingMin, m_timeMovingMax);
            yield return new WaitForSeconds(movingTime);
            
            m_rigidbody2D.velocity = Vector2.zero;
            
        }
    }