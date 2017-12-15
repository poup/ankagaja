using UnityEngine;

public class TrapSpawnZoneRect : TrapSpawnZone
{
    [SerializeField] private float m_width;
    [SerializeField] private float m_height;

    private void OnDrawGizmos()
    {
        var size = new Vector3(m_width, m_height, 0);
        Gizmos.DrawWireCube(transform.position, size);
    }

    public override Vector2 GetPosition()
    {
        var x = m_width * (Random.value - 0.5f);
        var y = m_height * (Random.value - 0.5f);
        return new Vector2(x, y) + transform.position.ToVector2();
    }
}