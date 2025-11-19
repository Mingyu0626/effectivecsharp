using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ConcurrentQueueTest : MonoBehaviour
{
    private const int ThreadCount = 10;
    private const int ItemsPerThread = 10000;
    private const int ExpectedTotal = ThreadCount * ItemsPerThread;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 1000, 500), "Test Normal Queue"))
        {
            TestNormalQueue();
        }

        if (GUI.Button(new Rect(10, 520, 1000, 500), "Test Concurrent Queue"))
        {
            TestConcurrentQueue();
        }
    }

    private async void TestNormalQueue()
    {
        Queue<int> q = new Queue<int>();
        Task[] tasks = new Task[ThreadCount];

        Debug.Log($"[Unsafe] Start Enqueue... (Expected: {ExpectedTotal})");

        try
        {
            for (int i = 0; i < ThreadCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < ItemsPerThread; j++)
                    {
                        q.Enqueue(j);
                    }
                });
            }

            await Task.WhenAll(tasks);

            if (q.Count != ExpectedTotal)
            {
                Debug.LogError($"[Unsafe] Data Lost! Result: {q.Count} / {ExpectedTotal}");
            }
            else
            {
                Debug.Log($"[Unsafe] Lucky! Result: {q.Count} / {ExpectedTotal}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[Unsafe] Exception occurred: {e.GetType().Name} - {e.Message}");
        }
    }

    private async void TestConcurrentQueue()
    {
        ConcurrentQueue<int> cq = new ConcurrentQueue<int>();
        Task[] tasks = new Task[ThreadCount];

        Debug.Log($"[Safe] Start Enqueue... (Expected: {ExpectedTotal})");

        for (int i = 0; i < ThreadCount; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                for (int j = 0; j < ItemsPerThread; j++)
                {
                    cq.Enqueue(j);
                }
            });
        }

        await Task.WhenAll(tasks);

        if (cq.Count == ExpectedTotal)
        {
            Debug.Log($"[Safe] Success! Result: {cq.Count} / {ExpectedTotal}");
        }
        else
        {
            Debug.LogError($"[Safe] Something went wrong. Result: {cq.Count} / {ExpectedTotal}");
        }
    }
}