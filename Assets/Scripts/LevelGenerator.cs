using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject ballPrefab;

    private struct Point
    {
        private readonly float _x, _y;
        public Point(float x, float y) { _x = x; _y = y; }

        public float X => _x;
        public float Y => _y;

        public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
        public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);
        public static Point operator /(Point point, float f) => new Point(point.X / f, point.Y / f);

        public float Distance(Point point)
        {
            return Mathf.Sqrt(Mathf.Pow(X - point.X, 2) + Mathf.Pow(Y - point.Y, 2));
        }

        public Vector2 ToVector()
        {
            return new Vector2(X, Y);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnWalls(Random.Range(3, 8));
        GameObject ball = Instantiate(ballPrefab, new Vector3(), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private List<Point> GeneratePoints(int numPoints)
    {
        List<Point> points = new();
        for (int i = 0; i < numPoints; ++i)
        {
            float angle = 360 / numPoints * i * Mathf.Deg2Rad;
            Point point = new(Mathf.Cos(angle) * 10, Mathf.Sin(angle) * 20);
            points.Add(point);
        }

        return points;
    }

    private void SpawnWalls(int numPoints)
    {
        List<Point> points = GeneratePoints(numPoints);

        for (int i = 0; i < points.Count(); ++i)
        {
            Point point1 = points[i], point2 = points[(i + 1) % points.Count()];

            Instantiate(wallPrefab, point1.ToVector(), Quaternion.identity);

            Point midpoint = (point1 + point2) / 2f;
            GameObject wall = Instantiate(wallPrefab, midpoint.ToVector(), Quaternion.identity);
            wall.transform.localScale = new Vector3(point1.Distance(point2), 1);

            Vector2 diff = (point1 - point2).ToVector();
            wall.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg);
        }
    }
}