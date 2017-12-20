using System.Collections.Generic;
using UnityEngine;

public class TrapSpawnZoneCells : TrapSpawnZone
{
    [SerializeField] private float m_width;
    [SerializeField] private float m_height;
    
    private readonly List<Vector2> m_remains = new List<Vector2>();

    private void Awake()
    {
        Reset();
    }

    public void OnDisable()
    {
        Reset();
    }
    
    private void OnDrawGizmos()
    {
        var size = new Vector3(m_width, m_height, 0);
        Gizmos.DrawWireCube(transform.position, size);
    }

    public override Vector2 GetPosition()
    {
        return m_remains.PickAndRemoveRandom();
    }
    
    public void Reset()
    {
        m_remains.Clear();

        var minX = -Mathf.CeilToInt(m_width*0.5f);
        var minY = -Mathf.CeilToInt(m_height*0.5f);
        
        for (int x = minX; x < minX + m_width; ++x)
        for (int y = minY; y < minY + m_height; ++y)
        {
            m_remains.Add(new Vector2(x+0.5f, y+0.5f) + transform.position.ToVector2());
        }

        m_remains.Shuffle();
    }
}