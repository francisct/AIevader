using UnityEngine;
using System.Collections;
using System;

public class NodeBinaryHeap {
	
	public PathfindingNode[] items;
	public int currentItemCount;
	
	public NodeBinaryHeap(int maxHeapSize) {
		items = new PathfindingNode[maxHeapSize];
        currentItemCount = 0;
    }

    public void Clear()
    {
      
        int size = items.Length;
        items = new PathfindingNode[size];
        currentItemCount = 0;
    }

    public void SortedAdd(PathfindingNode item)
    {
            for (int i = currentItemCount; i >= 0; i--)
            {
                if (i == 0)
                {
                    items[i] = item;
                    break;
                }
                if (item <= items[i - 1])
                {
                    items[i] = item;
                    break;
                }
                else ShiftItemRight(i-1);
            }

        this.currentItemCount++;
    }

    private void ShiftItemRight(int i)
    {
        items[i+1] = items[i];
        items[i] = null;
    }

    public PathfindingNode Pop()
    {
        this.currentItemCount--;
        PathfindingNode tmp = items[currentItemCount];
        items[currentItemCount] = null;
        return tmp;
    }
	
	
}
