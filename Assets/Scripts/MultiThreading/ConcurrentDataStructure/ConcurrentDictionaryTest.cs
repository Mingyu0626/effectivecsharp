using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ConcurrentDictionaryTest : MonoBehaviour
{
    private const int ThreadCount = 10;
    private const int ItemsPerThread = 10000;
    private const int ExpectedTotal = ThreadCount * ItemsPerThread;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(1610, 10, 1000, 500), "Test Normal Dictionary"))
        {
            TestNormalDictionary();
        }

        if (GUI.Button(new Rect(1610, 520, 1000, 500), "Test Concurrent Dictionary"))
        {
            TestConcurrentDictionary();
        }
    }

    private async void TestNormalDictionary()
    {
        Dictionary<int, int> dict = new Dictionary<int, int>();
        Task[] tasks = new Task[ThreadCount];

        Debug.Log($"[Unsafe] Start Dictionary Add... (Expected: {ExpectedTotal})");

        try
        {
            for (int i = 0; i < ThreadCount; i++)
            {
                int threadIndex = i;
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < ItemsPerThread; j++)
                    {
                        int key = (threadIndex * ItemsPerThread) + j;
                        dict[key] = j;
                    }
                });
            }

            await Task.WhenAll(tasks);

            if (dict.Count != ExpectedTotal)
            {
                Debug.LogError($"[Unsafe] Data Lost or Corrupted! Result: {dict.Count} / {ExpectedTotal}");
            }
            else
            {
                Debug.Log($"[Unsafe] Lucky! Result: {dict.Count} / {ExpectedTotal}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[Unsafe] Exception occurred: {e.GetType().Name} - {e.Message}");
        }
    }

    private async void TestConcurrentDictionary()
    {
        ConcurrentDictionary<int, int> concurrentDict = new ConcurrentDictionary<int, int>();
        Task[] tasks = new Task[ThreadCount];

        Debug.Log($"[Safe] Start ConcurrentDictionary Add... (Expected: {ExpectedTotal})");

        for (int i = 0; i < ThreadCount; i++)
        {
            int threadIndex = i;
            tasks[i] = Task.Run(() =>
            {
                for (int j = 0; j < ItemsPerThread; j++)
                {
                    int key = (threadIndex * ItemsPerThread) + j;
                    concurrentDict.TryAdd(key, j);
                }
            });
        }

        await Task.WhenAll(tasks);

        if (concurrentDict.Count == ExpectedTotal)
        {
            Debug.Log($"[Safe] Success! Result: {concurrentDict.Count} / {ExpectedTotal}");
        }
        else
        {
            Debug.LogError($"[Safe] Something went wrong. Result: {concurrentDict.Count} / {ExpectedTotal}");
        }
    }
}