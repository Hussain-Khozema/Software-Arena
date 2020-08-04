using Newtonsoft.Json;


/// <summary>
/// entity to keep user progile information
/// </summary>
[System.Serializable]
public class User
{
    /// <value>
    /// uid of user
    /// </value>
    [JsonIgnore]
    public string id;
    /// <value>
    /// user's username (email)
    /// </value>
    public string username;
    /// <value>
    /// user's display name
    /// </value>
    public string name;
    /// <value>
    /// user's role (student/teacher)
    /// </value>
    public string role;

    /// <summary>
    /// default constructor
    /// </summary>
    public User()
    {
    }

    /// <summary>
    /// overloading constructor
    /// </summary>
    /// <param name="username">user's email</param>
    /// <param name="name">user's display name</param>
    /// <param name="role">user's role</param>
    public User(string username, string name, string role)
    {
        this.username = username;
        this.name = name;
        this.role = role;
    }

    /// <summary>
    /// formatting object to string (for debugging)
    /// </summary>
    /// <returns>formatted string</returns>
    public override string ToString()
    {
        return username + ", " + name + ", " + role;
    }

}