using UnityEngine;

public class TrapSpawnZoneCircle : TrapSpawnZone
{
    [SerializeField] private float m_radius;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }

    public override Vector2 GetPosition()
    {
        return Random.insideUnitCircle * m_radius + transform.position.ToVector2();
    }
}