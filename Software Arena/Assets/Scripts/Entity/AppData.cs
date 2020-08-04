using Newtonsoft.Json;

/// <summary>
/// Entity class to store user's game data
/// </summary>
public class AppData
{
	/// <value>
	/// string to keep user's name
	/// </value>
	public string name;
	/// <value>
	/// int value to keep user's character points (currency)
	/// </value>
	[JsonProperty("character_points")]
	public int charPoints;
	/// <value>
	/// int value to keep user's point system for multiplayer mode
	/// </value>
	[JsonProperty("multi_player_points")]
	public int multiPlayerPoints;
	/// <value>
	/// int value to keep user's point system for challenge mode
	/// </value>
	[JsonProperty("challenge_points")]
	public int challengePoints;
	/// <value>
	/// strcuture to keep user's acatar information and shop state
	/// </value>
	public AvatarData avatar;

	/// <value>
	/// structure to keep user's gaming progrss and stats related to it
	/// </value>
	[JsonProperty("user_progress")]
	public GameProgress progress;
	
	/// <summary>
	/// constructor to initilise object
	/// </summary>
	public AppData(){

	}

	
}