using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetUIEventMenager
{
    public delegate void DelEvent();

    private Queue<DelEvent> delQueue = null;
    private DelEvent delEvent = null;
    private object lockObject = new object(); //lock
    public void UpdateQueue()
    {
        delEvent = delQueue.Dequeue();
        delEvent();
    }
    public void SetFuncs(DelEvent del)
    {
        lock (lockObject)
        { //other thread include this Queue so locking the queue 

            delQueue.Enqueue(del);
        }
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