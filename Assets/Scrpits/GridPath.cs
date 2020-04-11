﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridPath : MonoBehaviour
{
    public Transform player;
    public bool Scan;
    public LayerMask unWalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;
    public Vector3 teste;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }
    private void Update()
    {
        if (Scan)
        {
            Start();
            Scan = false;
        }
    }
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        //teste = worldBottomLeft;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                if (x==0 && y==0)
                {

                teste = worldPoint;
                }
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unWalkableMask));  //2D
                //bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius)); //3D
                grid[x, y] = new Node(walkable, worldPoint,x,y);
            }
        }
    }
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x ==0 && y == 0)
                {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX >= 0 && checkX< gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }
    public Node NodeFromWorldPoint(Vector3 worldPosition) //
    {
        float percentX = ((worldPosition.x - transform.position.x) / gridWorldSize.x) + .5f;
        float percentY = ((worldPosition.y - transform.position.y) + (gridWorldSize.y / 2)) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.FloorToInt(Mathf.Clamp((gridSizeX) * percentX, 0, gridSizeX - 1));
        int y = Mathf.FloorToInt(Mathf.Clamp((gridSizeY) * percentY, 0, gridSizeY - 1));
        //int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
        
    }
    public List<Node> path;
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
        if (grid != null)
        {
            //Node playerNode = NodeFromWorldPoint(player.position);
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable)?Color.green:Color.red;
                if (path != null)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                   //Gizmos.color = Color.blue;
                }
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-0.05f));
            }
        }
    }

}
