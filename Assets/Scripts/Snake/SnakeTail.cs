using System;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    [SerializeField] private Transform SnakeHead;
    [SerializeField] private float CircleDiameter;

    private List<Transform> snakeCircles = new List<Transform>();
    private List<Vector2> positions = new List<Vector2>();

    public Action Die;

    private void Awake()
    {
        positions.Add(SnakeHead.position);
    }

    private void Update()
    {
        float distance = ((Vector2) SnakeHead.position - positions[0]).magnitude;

        if (distance > CircleDiameter)
        {
            // Направление от старого положения головы, к новому
            Vector2 direction = ((Vector2) SnakeHead.position - positions[0]).normalized;

            positions.Insert(0, positions[0] + direction * CircleDiameter);
            positions.RemoveAt(positions.Count - 1);

            distance -= CircleDiameter;
        }

        for (int i = 0; i < snakeCircles.Count; i++)
        {
            snakeCircles[i].position = Vector2.Lerp(positions[i + 1], positions[i], distance / CircleDiameter);
        }
    }

    public void AddCircle(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Transform circle = Instantiate(SnakeHead, positions[positions.Count - 1], Quaternion.identity, transform);
            snakeCircles.Add(circle);
            positions.Add(circle.position);
        }
    }

    public void RemoveCircle()
    {
        int index = snakeCircles.Count - 1;
        if(index < 0)
        {
            Die?.Invoke();
            return;
        }
        Destroy(snakeCircles[index].gameObject);
        snakeCircles.RemoveAt(index);
        positions.RemoveAt(index + 1);
    }

    private void OnDie()
    {
        Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        Die += OnDie;
    }

    private void OnDisable()
    {
        Die -= OnDie;
    }
}
