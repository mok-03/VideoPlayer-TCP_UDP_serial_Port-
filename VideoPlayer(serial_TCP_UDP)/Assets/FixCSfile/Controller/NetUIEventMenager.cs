using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetUIEventMenager
{
    public delegate void DelEvent();

    private Queue<DelEvent> delQueue = null;
    private DelEvent delEvent = null;
    public void UpdateQueue()
    {
        delEvent = delQueue.Dequeue();
        delEvent();
    }
    public void SetFuncs(DelEvent del)
    {
        delQueue.Enqueue(del);

    }
    public bool checkQueueStack()
    {
        if (delQueue.Count == 0)
            return false;

        return true;
    }

    public void Begin()
    {
        delQueue = new Queue<DelEvent>();
    }

}