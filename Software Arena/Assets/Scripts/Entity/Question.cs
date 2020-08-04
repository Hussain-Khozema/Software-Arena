
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

/// <summary>
/// entity object to hold MCQ question as general
/// </summary>
public class Question 
{
    /// <value>
    /// id of the question
    /// </value>
    [JsonIgnore]
    public string id;
    /// <value>
    /// question's description
    /// </value>
    [JsonProperty("question_text")]
    public string description;

    /// <value>
    /// list of answer options for the question
    /// </value>
    public List<string> choices;
    /// <summary>
    /// index of correct answer from list of options 
    /// </summary>
    [JsonProperty("correct_choice")]
    public int idxAnswer;


    /// <summary>
    /// constructor to initilised a question
    /// </summary>
    /// <param name="description">question desc</param>
    /// <param name="choices">list of options avaiable for the question</param>
    /// <param name="idxAnswer">index of correct answer</param>
    public Question(string description, List<string> choices, int idxAnswer)
    {
        // assume no null passed
        if (choices.Count != 4)
        {
            throw new ArgumentException("choices number not equal to 4");
        }
        if (idxAnswer < 0 || idxAnswer > 3)
        {
            throw new ArgumentOutOfRangeException("invalid choice given");
        }
        this.description = description;
        this.choices = choices;
        this.idxAnswer = idxAnswer;
    }


    /// <summary>
    /// performing swaping of options to produce randomness 
    /// </summary>
    public void swapChoices()
    {
        string tempCorrect = choices[idxAnswer];
        choices.RemoveAt(idxAnswer);

        Random random = new Random();
        String temp;
        int idx;
        for (int i=0; i<10; i++)
        {
            idx = random.Next(choices.Count);
            temp = choices[0];
            choices.RemoveAt(0);
            choices.Insert(idx, temp);
        }

        idxAnswer = random.Next(choices.Count+1);
        choices.Insert(idxAnswer, tempCorrect);
    }

    /// <summary>
    /// to validate if user answer the question correctly
    /// </summary>
    /// <param name="idxChosen">index of option user selected</param>
    /// <returns>true if user answered correctly</returns>
    public virtual bool answerQestion(int idxChosen)
    {
        return idxChosen == idxAnswer;
    }


    /// <summary>
    /// converting object to string (for debugging)
    /// </summary>
    /// <returns>formatted question</returns>
    public override string ToString()
    {
        string options = "";
        foreach(string choice in choices)
        {
            options += choice+", ";
        }
        
        return "qn: "+description + ", choices: " + options  + "ans: "+idxAnswer;
    }


}
