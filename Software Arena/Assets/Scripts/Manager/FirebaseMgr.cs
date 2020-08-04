using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Auth;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using System;
using Firebase.Extensions;
using System.Threading.Tasks;
using Newtonsoft.Json;

/// <summary>
/// manager to handle firebase authentication and database related functions
/// </summary>
public class FirebaseManager
{
    /// <value>
    /// singleton instance for firebase manager itself
    /// </value>
    static FirebaseManager instance;
    /// <value>
    /// firebase database object reference
    /// </value>
    private FirebaseDatabase _db;

    /// <value>
    /// database key reference for saving user data
    /// </value>
    const string ROOT_USER = "user/";
    /// <value>
    /// database key reference for saving user's game data
    /// </value>
    const string ROOT_GAME_DATA = "game_data/";
    /// <value>
    /// database key reference for saving challenge questions submitted by users
    /// </value>
    const string ROOT_CHALLENGE_QUESTION = "challenge_mode_qn_bank/";
    /// <value>
    /// database key reference for saving report
    /// </value>
    const string ROOT_REPORT = "report/";

    /// <summary>
    /// constructor for initilising firebase manager 
    /// </summary>
    private FirebaseManager()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://ssad-software-arena.firebaseio.com/");
        _db = FirebaseDatabase.DefaultInstance;
    }

    /// <summary>
    /// getter method to retrieve singlton instance of firebase manager
    /// </summary>
    /// <returns>firebase manager instance</returns>
    static public FirebaseManager getInstance()
    {
        if (instance == null)
        {
            instance = new FirebaseManager();
        }

        return instance;
    }

    // AUTH
    /// <summary>
    /// getter method to retrieve firebase authentication user instance
    /// </summary>
    /// <returns>firebase user</returns>
    ///  <exception cref="System.Exception">Thrown when called before user login</exception>
    public FirebaseUser getFirebaseUser()
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        FirebaseUser user = auth.CurrentUser;
        if (user == null)
        {
            throw new Exception("User is not login");
        }
        return user;
    }

    /// <summary>
    /// perform registeration for new user using email, and password
    /// </summary>
    /// <param name="email">user's email in string</param>
    /// <param name="password">user's password in string</param>
    /// <param name="callback">callback to notify caller when task is finished</param>
    ///  <exception cref="System.ArgumentNullException">Thrown when callback not provided</exception>
    public void doRegister(string email, string password, Action<bool, string> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException("callback is not provided");
        }
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            string message = readError(task);
            if (message != null)
            {
                callback(false, message);
                return;
            }
            Firebase.Auth.FirebaseUser newUser = task.Result;
            message = string.Format("Firebase user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            callback(true, message);
        });
    }

    /// <summary>
    /// perform updating of firebase user's profile information like display name
    /// </summary>
    /// <param name="displayName">user's display name</param>
    public void updateUserProfile(string displayName)
    {
        FirebaseUser user = getFirebaseUser();
        UserProfile profile = new Firebase.Auth.UserProfile
        {
            DisplayName = displayName,
        };
        user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
        {
            string message = readError(task);
            if (message != null)
            {
                return;
            }

            Debug.Log("updateProfile: successfully.");
        });
    }

    /// <summary>
    /// perform sending verification email to user's registerated email account
    /// </summary>
    /// <param name="callback">callback to notify user when task is finsihed</param>
    /// <exception cref="System.ArgumentNullException">Thrown when callback not provided</exception>
    public void sendVerificaitionEmail(Action<bool, string> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException("callback is not provided");
        }

        FirebaseUser user = getFirebaseUser();
        user.SendEmailVerificationAsync().ContinueWithOnMainThread(task =>
        {
            string message = readError(task);
            if (message != null)
            {
                callback(false, message);
                return;
            }

            message = "Please verify you account from your email";
            callback(true, message);
        });
    }

    /// <summary>
    /// perform authentication to allow user login to our system
    /// </summary>
    /// <param name="email">user's email</param>
    /// <param name="password">user's password</param>
    /// <param name="callback">callback to notify caller when task is finished</param>
    /// <remarks>
    /// user is not allow to login if his email is not verified
    /// </remarks>
    /// <exception cref="System.ArgumentNullException">Thrown when callback not provided</exception>
    public void doLogin(string email, string password, Action<bool, string> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException("callback is not provided");
        }
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            string message = readError(task);
            if (message != null)
            {
                callback(false, message);
                return;
            }

            Firebase.Auth.FirebaseUser user = task.Result;
            Debug.LogFormat("user found: {0} ({1})", user.DisplayName, user.UserId);
            if (!user.IsEmailVerified)
            {
                message = "Please verify your email";
                callback(false, message);
                return;
            }
            message = "Authenticate successfully";
            callback(true, message);
        });
    }

    /// <summary>
    /// alllow user to reset their password by sending en reset password email to their registered email
    /// </summary>
    /// <param name="emailaddress">user's email</param>
    public void forgetPassword(string emailaddress)
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SendPasswordResetEmailAsync(emailaddress).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                return;
            }

            Debug.Log("Password reset email sent successfully.");
        });
    }

    /// <summary>
    /// perform logout and clear user's informatiton from firebase authentication
    /// </summary>
    public void doLogout()
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        auth.SignOut();
    }








    // DATABASE
    /// <summary>
    /// save user's profile information
    /// </summary>
    /// <param name="user">user's profile</param>
    /// <remarks>
    /// this user profile is different from firebase authentication user profile
    /// </remarks>
    public void saveUser(User user)
    {
        string uid = getFirebaseUser().UserId;
        var refUser = _db.RootReference.Child(ROOT_USER).Child(uid);
        refUser.SetRawJsonValueAsync(JsonConvert.SerializeObject(user));
    }

    /// <summary>
    /// retrieve user's progile information
    /// </summary>
    /// <param name="callback"></param>
    /// <exception cref="System.ArgumentNullException">Thrown when callback not provided</exception>
    public void getUserInfo(Action<User, string> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException("callback is not provided");
        }


        string uid = getFirebaseUser().UserId;
        _db.RootReference.Child(ROOT_USER).Child(uid).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            string message = readError(task);
            if (message != null)
            {
                callback(null, message);
                return;
            }
            if (task.IsCompleted)
            {
                message = "task is success";
                DataSnapshot snapshot = task.Result;
                User user = JsonConvert.DeserializeObject<User>(snapshot.GetRawJsonValue());
                user.id = uid;
                Debug.Log(user.ToString());
                callback(user, message);
            }
        });

    }


    // DATABASE: Challenge Question
    /// <summary>
    /// saving challenge question information to database
    /// </summary>
    /// <param name="question">challenge questions to be saved</param>
    /// <param name="callback">callback to notifiy caller when task is finished</param>
    /// <exception cref="System.ArgumentNullException">Thrown when callback not provided</exception>
    public void saveQuestion(ChallengeQuestion question, Action<bool, string> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException("callback is not provided");
        }
        var refQestion = _db.RootReference.Child(ROOT_CHALLENGE_QUESTION);
        string json = JsonConvert.SerializeObject(question);
        string key = question.id != null ? question.id : refQestion.Push().Key;
        refQestion.Child(key).SetRawJsonValueAsync(json).ContinueWithOnMainThread((task) =>
        {
            string message = readError(task);
            if (message != null)
            {
                Debug.Log("saveQuestion.e: " + message);
                callback(false, message);
                return;
            }
            message = "task is success";
            Debug.Log("saveQuestion: " + message);
            callback(true, message);
        });
    }

    /// <summary>
    /// saving a newly submitted challenge question to firebase database
    /// </summary>
    /// <param name="question">newly submitted challenge questions</param>
    /// <param name="callback">callback to notify user when task is finished</param>
    public void addQuestion(ChallengeQuestion question, Action<bool, string> callback)
    {
        question.createrId = getFirebaseUser().UserId;
        saveQuestion(question, callback);
    }

    /// <summary>
    /// approve or reject a given challenge question
    /// </summary>
    /// <param name="question">approving challenge question</param>
    /// <param name="isApproved">state to approve or reject</param>
    /// <param name="callback">callback to notify caller when task is finished</param>
    public void doApproveQuestion(ChallengeQuestion question, bool isApproved, Action<bool, string> callback)
    {
        question.reviewer = getFirebaseUser().DisplayName;
        question.status = isApproved ? ChallengeQuestion.Status.Approved : ChallengeQuestion.Status.Rejected;
        question.dateApproved = DateTime.Now;
        saveQuestion(question, callback);
    }


    /// <summary>
    /// publishing a set of questions
    /// </summary>
    /// <param name="questions">list of questions to be publish</param>
    /// <param name="callback">callback to notify caller when task is finished</param>
    public void doPublishChallenge(List<ChallengeQuestion> questions, Action<bool, string> callback)
    {
        foreach (ChallengeQuestion question in questions)
        {

            question.status = ChallengeQuestion.Status.Submitted;

            //question.reviewer = getFirebaseUser().DisplayName;
            //question.dateApproved = DateTime.Now;
            saveQuestion(question, callback);
        }

    }


    /// <summary>
    /// retrieving list of questions from firebase database
    /// </summary>
    /// <param name="callback">callback to notify caller when task is finished</param>
    public void getQuestionList(Action<List<ChallengeQuestion>, string> callback)
    {
        var refQestion = _db.RootReference.Child(ROOT_CHALLENGE_QUESTION);
        refQestion.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            string message = readError(task);
            if (message != null)
            {
                callback(null, message);
                return;
            }
            if (task.IsCompleted)
            {
                message = "task is success";
                var snapshot = task.Result;
                var resultDict = JsonConvert.DeserializeObject<Dictionary<string, ChallengeQuestion>>(snapshot.GetRawJsonValue());
                var list = new List<ChallengeQuestion>();
                foreach (string key in resultDict.Keys)
                {
                    ChallengeQuestion question = resultDict[key];
                    question.id = key;
                    list.Add(question);
                }
                callback(list, message);
            }
        });
    }


    // DATABASE: Game data
    /// <summary>
    /// saving user's game app to firebase database
    /// </summary>
    /// <param name="data">user's data to be save</param>
    /// <param name="callback">callback to notify caller when task is finished</param>
    /// <remarks>
    /// <para>this method should only be called by DataManager/para>
    /// <para>caller should not initialise the AppData instance by any mean</para>
    /// </remarks>
    /// <exception cref="System.ArgumentNullException">Thrown when callback not provided</exception>
    public void saveGameData(AppData data, Action<bool, string> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException("callback is not provided");
        }
        string uid = getFirebaseUser().UserId;
        var refGameData = _db.RootReference.Child(ROOT_GAME_DATA).Child(uid);
        refGameData.SetRawJsonValueAsync(JsonConvert.SerializeObject(data)).ContinueWithOnMainThread((task) =>
        {
            string message = readError(task);
            if (message != null)
            {
                Debug.Log("saveGameData.e: " + message);
                callback(false, message);
                return;
            }
            message = "task is success";
            Debug.Log("saveGameData: " + message);
            callback(true, message);
        }); ;
    }

    /// <summary>
    /// retriving user's game data from firebase database
    /// </summary>
    /// <param name="callback">callback to notify caller when task is finished</param>
    /// <exception cref="System.ArgumentNullException">Thrown when callback not provided</exception>
    public void getGameData(Action<AppData, string> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException("callback is not provided");
        }
        var user = getFirebaseUser();
        string uid = user.UserId;
        var refGameData = _db.RootReference.Child(ROOT_GAME_DATA).Child(uid);
        refGameData.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            string message = readError(task);
            if (message != null)
            {
                callback(null, message);
                return;
            }
            if (task.IsCompleted)
            {
                message = "task is success";
                DataSnapshot snapshot = task.Result;
                string json = snapshot.GetRawJsonValue();
                AppData newAppData;
                if (json == null)
                {
                    message = "no game data found in backend, create default data";
                    newAppData = new AppData();
                    newAppData.name = user.DisplayName;
                    newAppData.avatar = AvatarData.getDefault();
                    newAppData.progress = new GameProgress();
                }
                newAppData = JsonConvert.DeserializeObject<AppData>(json);
                callback(newAppData, message);
            }
        });
    }


    // OTHERS 
    /// <summary>
    /// from a given task, extracing the message of exception thrown
    /// </summary>
    /// <param name="task">completed task</param>
    /// <returns>error message</returns>
    /// <remarks>null if no exception thrown</remarks>
    private string readError(Task task)
    {
        if (task.IsCanceled)
        {
            Debug.LogError("task.canceled");
            return "task was cancelled";
        }
        if (task.IsFaulted)
        {
            Debug.LogError("task.error: " + task.Exception);
            return task.Exception.InnerExceptions[0].GetBaseException().Message;
        }
        return null;
    }


    
    /// <summary>
    /// saving report to database
    /// </summary>
    /// <param name="report">saving report</param>
    /// <param name="callback">callback to notify caller when task is finished</param>
    /// <exception cref="System.ArgumentNullException">Thrown when callback not provided</exception>
    public void saveReport(jReport report, Action<bool, string> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException("callback is not provided");
        }
        //define the node we are going to save to as ROOT_REPORT
        var refReport = _db.RootReference.Child(ROOT_REPORT);

        //convert report that is passed in to json format
        string json = JsonConvert.SerializeObject(report);

        //create a key under ROOT_REPORT
        string key = refReport.Push().Key;

        //save the report to firebase
        refReport.Child(key).SetRawJsonValueAsync(json).ContinueWithOnMainThread((task) =>
        {
            string message = readError(task);
            if (message != null)
            {
                Debug.Log("saveReport.e: " + message);
                callback(false, message);
                return;
            }
            message = "task is success";
            Debug.Log("saveReport: " + message);
            callback(true, message);
        });
    }

    /// <summary>
    /// retrive all saved report from database
    /// </summary>
    /// <param name="callback">callback to notify caller when task is finished</param>
    public void getAllReport(Action<List<jReport>, string> callback)
    {
        //define the node we are going to retrieve information from as ROOT_REPORT
        var refAllReport = _db.RootReference.Child(ROOT_REPORT);

        //retrieve all reports found under ROOT_REPORT
        refAllReport.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            string message = readError(task);
            if (message != null)
            {
                callback(null, message);
                return;
            }

            if (task.IsCompleted)
            {
                message = "task is success";
                var snapshot = task.Result;
                var resultDict = JsonConvert.DeserializeObject<Dictionary<string, jReport>>(snapshot.GetRawJsonValue());
                var list = new List<jReport>();
                foreach (string key in resultDict.Keys)
                {
                    jReport data = resultDict[key];
                    //if its not a init node
                    if (data.date != "01/01/0001")
                    {
                        list.Add(data);
                    }
                    else
                    {
                        //if it is an init node, remove it from the database
                        DatabaseReference rmvthisdata = _db.GetReference(ROOT_REPORT).Child(key);
                        rmvthisdata.RemoveValueAsync();
                    }

                }
                callback(list, message);
            }

        });
    }


    /// <summary>
    /// retrieve all player's game data as list
    /// </summary>
    /// <param name="callback">callback to notify caller when task is finsihed</param>
    public void getAllGameData(Action<List<AppData>, string> callback)
    {
        //define the node we are going to retrieve information from as ROOT_GAME_DATA
        var refAllGameData = _db.RootReference.Child(ROOT_GAME_DATA);

        //retrieve all AppData found under ROOT_GAME_DATA
        refAllGameData.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            string message = readError(task);
            if (message != null)
            {
                callback(null, message);
                return;
            }
            if (task.IsCompleted)
            {
                message = "task is success";
                var snapshot = task.Result;
                var resultDict = JsonConvert.DeserializeObject<Dictionary<string, AppData>>(snapshot.GetRawJsonValue());
                var list = new List<AppData>();
                foreach (string key in resultDict.Keys)
                {
                    AppData data = resultDict[key];
                    list.Add(data);
                }
                callback(list, message);
            }
        });
    }



}