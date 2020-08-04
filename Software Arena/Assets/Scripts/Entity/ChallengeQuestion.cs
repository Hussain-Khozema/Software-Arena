using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// entity to hold information of challenge question
/// </summary>
public class ChallengeQuestion : Question
{
    /// <value>
    /// uid of creator
    /// </value>
    [JsonProperty("creator_id")]
    public string createrId;
    /// <value>
    /// reviewer's name
    /// </value>
    public string reviewer;
    /// <value>
    /// remarks related to question
    /// </value>
    public string remarks;

    /// <summary>
    /// enum used to presentate questions's state
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status { Approved, Rejected, Pending, Submitted };
    /// <value>
    /// state of question
    /// </value>
    public Status status;
    /// <value>
    /// datetime when question submitted
    /// </value>
    [JsonProperty("datetime_submitted")]
    public DateTime dateSubmitted;
    /// <value>
    /// datetime when question being reviewed
    /// </value>
    [JsonProperty("datetime_approved")]
    public DateTime dateApproved;
    /// <value>
    /// number of attempts by all user (for stats collection)
    /// </value>
    [JsonProperty("number_of_attempts")]
    public int numAttempts;
   

    /// <summary>
    /// constructor to initialise challenge question
    /// </summary>
    /// <param name="question_text">question descrption</param>
    /// <param name="choices">list of answer for question</param>
    /// <param name="correct_choice">index of correct answer</param>
    public ChallengeQuestion(string question_text, List<string> choices, int correct_choice) :base( question_text, choices, correct_choice)
    {
        dateSubmitted = DateTime.Now;
        status = Status.Pending;
    }


    /// <summary>
    /// valid if user answer question correctly
    /// </summary>
    /// <param name="idxChosen">index of user's selection on answer</param>
    /// <returns>true if user answer correctly</returns>
    public override bool answerQestion(int idxChosen)
    {
        numAttempts++;
        return base.answerQestion(idxChosen);
    }


}
