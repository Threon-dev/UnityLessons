using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeMonkey.Utils;
public class Grid
{
    public const int HEAT_MAP_MAX_VALUE = 100;
    public const int HEAT_MAP_MIN_VALUE = 0;
    private int width;
    private int height;

    private Vector3 originPosition;
    private float cellSize;

    private int[,] gridArray;
    private TextMesh[,] debugTextArray;

    public event EventHandler<OnGridValueChangedEventArgs> onGridValueChanged;
    public class OnGridValueChangedEventArgs: EventArgs
    {
        public int x;
        public int y;
    }
    public Grid(int width,int height,float cellSize,Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTextArray[x,y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null,GetWorldPosition(x,y) + new Vector3(cellSize,cellSize) * .5f , 30, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y),GetWorldPosition(x, y + 1),Color.white,100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y),Color.white,100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight() 
    {
        return height;
    }

    public void SetValue(int x,int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = Mathf.Clamp(value, HEAT_MAP_MIN_VALUE, HEAT_MAP_MAX_VALUE);
            debugTextArray[x, y].text = gridArray[x, y].ToString();
            //debugTextArray[x, y].color = Color.red;
            if (onGridValueChanged != null) onGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    private void GetXY(Vector3 position,out int x, out int y)
    {
        x = Mathf.FloorToInt((position - originPosition).x / cellSize);
        y = Mathf.FloorToInt((position - originPosition).y / cellSize);
    }

    public int GetValue(int x,int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    public void AddValue(int x, int y, int value)
    {
        SetValue(x, y, (GetValue(x, y) + value));
    }

    public void AddValue(Vector3 worldPosition,int value, int fullValueRange, int totalRange)
    {
        int lowerValueAmount = Mathf.RoundToInt((float)value / (totalRange - fullValueRange));
        GetXY(worldPosition, out int originX, out int originY);
        for (int x = 0; x < totalRange; x++)
        {
            for (int y = 0; y < totalRange - x; y++)
            {
                int radius = x + y;
                int addValueAmount = value;
                if(radius > fullValueRange)
                {
                    addValueAmount -= lowerValueAmount * (radius - fullValueRange);
                }
                AddValue(originX + x, originY + y, addValueAmount);
                if(x != 0)
                {
                    AddValue(originX - x, originY + y, addValueAmount);
                }

                if(y != 0)
                {
                    AddValue(originX - x, originY - y, addValueAmount);
                }
                if(y != 0 && x != 0)
                {
                    AddValue(originX + x, originY - y, addValueAmount);
                }
            }
        }
    }
}
