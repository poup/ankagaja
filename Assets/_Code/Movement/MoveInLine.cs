using Assets._Code.Movement;
using UnityEngine;

[RequireComponent(typeof(Directionable))]
public class MoveInLine : MonoBehaviour
{
    private Vector2 m_direction;
    public float m_speed;

	void Start()
	{
		m_direction = GetComponent<Directionable>().Direction;
	}

    void FixedUpdate()
    {
	    var move = m_direction.normalized * m_speed * Time.deltaTime;
	    transform.position = transform.position + new Vector3(move.x, move.y, 0);
    }
}