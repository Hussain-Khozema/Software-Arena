using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.PerformanceTesting;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;

// define custom variable for test test
[TestFixture(1)]
public class PerformanceTest
{
    private int _value;

    public PerformanceTest(int value)
    {
        _value = value;
    }

    [UnityTest, Performance]
    public IEnumerator SampleTest()
    {
        // It goes to the group name "Time"
        // Measure the execution time of a method (measure 10 times, usage an average of 5 measurements)
        Measure.Method(() =>
        {
            for (int i = 0; i < 1000000; i++)
            {
            }
        })
            .MeasurementCount(10)
            .IterationsPerMeasurement(5)
            .Run();


        // It goes to the group name "Time"
        // Measure the execution time of 10 frames, wait for 5 frames to warm up
        yield return Measure.Frames()
            .WarmupCount(5)
            //.ProfilerMarkers(groupDefinitions) // if we just want to measure profiler without record frames
            //.DontRecordFrametime()
            .MeasurementCount(11)
            .Run();

        // Measure the execution time of all frames
        using (Measure.Frames().Scope(new SampleGroupDefinition("Group 1")))
        {
            // your code
            yield return null;
            yield return null;
        }

        // measure profiler data
        SampleGroupDefinition[] groupDefinitions =
        {
            new SampleGroupDefinition("PlayerLoop"),
            new SampleGroupDefinition("Camera.Render"),
        };
        using (Measure.ProfilerMarkers(groupDefinitions))
        {
            for (var i = 0; i < 60; i++)
            {
                yield return null;
            }
        }

        // measure memory
        var allocated = new SampleGroupDefinition("TotalAllocatedMemory", SampleUnit.Megabyte);
        var reserved = new SampleGroupDefinition("TotalReservedMemory", SampleUnit.Megabyte);
        for (var i = 0; i < 60; i++)
        {
            Measure.Custom(allocated, Profiler.GetTotalAllocatedMemoryLong() / 1048576f);
            Measure.Custom(reserved, Profiler.GetTotalReservedMemoryLong() / 1048576f);
            yield return null;
        }

        // load scene
        using (Measure.Scope(new SampleGroupDefinition("Group 3")))
        {
            SceneManager.LoadScene("Assets/Scenes/WaitingRoom.unity");
        }

        // measure instantiate of objects
        using (Measure.Scope(new SampleGroupDefinition("Group 4")))
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            for (var i = 0; i < 5000; i++)
            {
                UnityEngine.Object.Instantiate(cube);
            }
        }

        yield return null;
    }
}