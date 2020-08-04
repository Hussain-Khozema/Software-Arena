using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// entity to keep user's game progress
/// </summary>
public class GameProgress
{
	/// <value>
	/// user's current world for story mode
	/// </value>
	[JsonProperty("cur_world")]
	public int curWorld;
	/// <value>
	/// user's current stage for story mode
	/// </value>
	[JsonProperty("cur_stage")]
	public int curStage;
	/// <value>
	/// user's average attempts on all stage he/she had went through
	/// </value>
	[JsonProperty("avg_num_attempts")]
	public double avgAttempts;
	/// <value>
	/// user's stage stats collected when he/she playing the game (for report)
	/// </value>
	[JsonProperty("stage_stats")]
	public Dictionary<string, StageStats> stats;
	// key should be combination of word-stage like "1-5"

	/// <summary>
	/// constructor for initilising
	/// </summary>
	public GameProgress()
	{
		this.curWorld = 0;
		this.curStage = 0;
		this.avgAttempts = 0;
		this.stats = new Dictionary<string, StageStats>();
	}
}