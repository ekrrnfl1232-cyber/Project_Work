using UnityEngine;
using System.Collections.Generic;

public class NavigationArea : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private Vector2 worldSize = new Vector2(25, 10);
    [SerializeField, Min(0.05f)] private float cellSize = 0.5f;

    [Header("Obstacle")]
    [SerializeField, Min(0.01f)] private float agentRadius = 0.5f;
    [SerializeField, Min(0f)] private float padding = 0.15f;
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Target")]
    [SerializeField] private Transform target;

    public Transform Target => target;
    public LayerMask Obstacle => obstacleLayer;
    public NavigationGrid Grid { get; private set; }
    private FlowField flowField;
    private Vector2Int? lastTargetCell;

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        RefreshObstacles();
    }

    public void Initialize()
    {
        Grid = new NavigationGrid(transform.position, worldSize, cellSize);

        flowField = new FlowField(Grid);
        RefreshObstacles();
    }

    public void RefreshObstacles()
    {
        if (Grid == null)
            return;

        Physics2D.SyncTransforms();

        for(int x = 0; x < Grid.Width; ++x)
        {
            for(int y = 0; y < Grid.Height; ++y)
            {
                Vector2Int cel = new Vector2Int(x, y);
                Vector2 center = Grid.GetCellCenter(cel);

                Collider2D obstacle = Physics2D.OverlapCircle(center, agentRadius + padding, obstacleLayer);

                Grid.SetWalkable(cel, obstacle == null);
            }
        }
        RefreshFlowField(true);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        RefreshFlowField(true);
    }

    public Vector2 GetDirection(Vector2 pos)
    {
        return flowField == null ? Vector2.zero : flowField.GetDirection(pos);
    }

    private void RefreshFlowField(bool force = false)
    {
        if (Grid == null || flowField == null)
            return;

        Vector2Int? targetCell = null;

        if (target != null && Grid.TryWorldToCell(target.position, out Vector2Int cell))
        {
            targetCell = cell;
        }

        if (!force && targetCell == lastTargetCell)
            return;

        flowField.Build(targetCell ?? new Vector2Int(-1, -1));
        lastTargetCell = targetCell;
    }

    private void OnDrawGizmosSelected()
    {
        if(Grid == null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, worldSize.y, 0.1f));
            return;
        }

        for (int x = 0; x < Grid.Width; ++x)
        {
            for (int y = 0; y < Grid.Height; ++y)
            {
                Vector2Int cell = new Vector2Int(x, y);
                Vector2 center = Grid.GetCellCenter(cell);

                Gizmos.color = Grid.IsWalkable(cell) ? Color.green : Color.red;
                Gizmos.DrawWireCube(new Vector3(center.x, center.y, transform.position.z), new Vector3(Grid.CellSize, Grid.CellSize, 0.1f) * 0.9f);
            }
        }
    }
}
