using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathManager : MonoBehaviour
{
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;
    PathFinding pathfinding;

    Boolean isProcessing;
    static PathManager instance;

    private void Awake()
    {
        instance = this;
        pathfinding = GetComponent<PathFinding>();
    }

    // This is the method to request the path and set the path for minion to follow 
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], Boolean> callback){
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.tryProcessNext();
    }

    void tryProcessNext() {
        if (!isProcessing && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessing = true;
            pathfinding.startFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    } 

        struct PathRequest {
            public Vector3 pathStart;
            public Vector3 pathEnd;
            public Action<Vector3[], Boolean> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback) {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }
        }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessing = false;
        tryProcessNext();
    }
    
}
