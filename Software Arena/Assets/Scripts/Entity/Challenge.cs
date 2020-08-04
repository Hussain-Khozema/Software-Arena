[System.Serializable]
public class Challenge
{
    public string question;
    public string answer;
    public string[] bait = new string[3];

    public Challenge()
    {

    }

    public Challenge(string[] t, int num_of_bait)
    {
        this.question = t[0];
        this.answer = t[1];

        for (int i = 0; i < num_of_bait; i++)
        {
            bait[i] = t[i + 2];
        }
    }


    public override string ToString()
    {
        return base.ToString();
        //   return username + ", " + name + ", " + role;
    }

}