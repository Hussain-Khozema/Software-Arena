using Newtonsoft.Json;
using System;

/// <summary>
/// entity to keep each stage's stats for user
/// </summary>
public class StageStats
{
    /// <value>
    /// number of attemps user 
    /// </value>
    [JsonProperty("num_attempts")]
    public int numAttempts;

    /// <value>
    /// datetime when user clear the stage
    /// </value>
    [JsonProperty("datetime_stage_clear")]
    public DateTime timeClear;

    /// <summary>
    /// default constructor
    /// </summary>
    public StageStats()
    {
    }

    /// <summary>
    /// overloading constructor
    /// </summary>
    /// <param name="numAttempts">number that user attempt to clear the stage</param>
    /// <param name="timeClear">datetime user clear the stage</param>
    public StageStats(int numAttempts, DateTime timeClear)
    {
        this.numAttempts = numAttempts;
        this.timeClear = timeClear;
    }
}