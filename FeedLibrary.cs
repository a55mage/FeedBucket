using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public struct Feed //DO NOT TOUCH THIS STRUCTURE!
{
    public SteamVR_Input_Sources Source;
    public float Frequency; //0-320
    public float Amplitude; //0-1
    public float Duration;
    public string Audio;    //Path to audio file, used for auditory feedback patterns
    public float Treshold; //Usually 0.04 is fine, to be used only if audio is used as well

    //HAPTIC ONLY FEEDBACK CONSTRUCTOR
    public Feed(SteamVR_Input_Sources source, float frequency, float amplitude,  float duration)
    {
        Source = source;
        Frequency = frequency;
        Amplitude = amplitude;
        Duration = duration;
        Audio = null;
        Treshold = 0;
    }

    //HAPTIC AND AUDITORY FEEDBACK CONSTRUCTOR
    public Feed(SteamVR_Input_Sources source, float frequency, float amplitude, float duration, string audio)
    {
        Source = source;
        Frequency = frequency;
        Amplitude = amplitude;
        Duration = duration;
        Audio = audio;
        Treshold = 0;
    }

    //AUDITORY FEEDBACK WITH AUTOMATICALLY GENERATED HAPTIC CONSTRUCTOR
    public Feed(SteamVR_Input_Sources source, float frequency, float amplitude, string audio, float treshold)
    {
        Source = source;
        Frequency = frequency;
        Amplitude = amplitude;
        Duration = 0;
        Audio = audio;
        Treshold = treshold;
    }
}

//HOW TO CREATE AN EXECUTABLE PATTERN
/* 1)   Declare a public List<Feed>
 *          public List<Feed> yourFeed = new List<Feed>();
 *          
 * 2)   Declare a public boolean
 *          public bool yourBool = false;
 *          
 * 3)   Create one or more feed elements for your pattern
 *          Feed feedback1 = new Feed(SteamVR_Input_Sources.Any, frequency, amplitude, duration); //guide below
 *          
 * 4)   Inside this class' constructor add the feeds to your list of feed
 *          yourFeed.Add(feedback1);
 *          
 * 5)   Go to the "FeedManager" script and follow the guide under the "FeedbackManager" IEnumerator
 */


/**
     Feed creation guide:
     SteamVR_Input_Sources source   = [REQUIRED] Can be SteamVR_Input_Sources.Any, SteamVR_Input_Sources.LeftHand, SteamVR_Input_Sources.RightHand for mainstream projects
     float frequency                = [REQUIRED] Value between 0 and 320, where 0 is quiet (useful for pauses) and 320 is full motor force
     float amplitude                = [REQUIRED] Value between 0f and 1.0f, it represents the intensity of the vibration
     float duration                 = [REQUIRED] If threshold isn't used, float variable that tells for how much time the controller vibrates (in seconds)
                                      [UNUSED]   If threshold is used
     public string Audio            = [OPTIONAL] Path to audio file, used for auditory feedback patterns
     public float Treshold          = [REQUIRED] if Audio Path is set, Usually 0.04 is fine, the value which if the audio intensity than this value, an haptic is triggered
                                      [UNUSED]   If Audio Path isn't set
     */

public class FeedLibrary
{
    //public SteamVR_Input_Sources activeHand = SteamVR_Input_Sources.Any;
    //FEED NAMING GUIDE
    /* [duration_amplitude_type]
     * duration:    short / medium / long --> [s = 0.1s ; m = 0.15s ; l = 0.2s]
     * amplitude:   weak / normal / strong --> [w = 0.4 ; n = 0.7 ; s = 1.0]
     * type:        vibration / pause
     */

    //BASIC FEEDS HERE
    Feed s_pause = new Feed(SteamVR_Input_Sources.Any, 320, 0f, 0.1f);
    Feed m_pause = new Feed(SteamVR_Input_Sources.Any, 320, 0f, 0.15f);
    Feed l_pause = new Feed(SteamVR_Input_Sources.Any, 320, 0f, 0.2f);

    Feed s_w_vib = new Feed(SteamVR_Input_Sources.Any, 320, 0.4f, 0.1f);
    Feed s_n_vib = new Feed(SteamVR_Input_Sources.Any, 320, 0.7f, 0.1f);
    Feed s_s_vib = new Feed(SteamVR_Input_Sources.Any, 320, 1.0f, 0.1f);

    Feed m_w_vib = new Feed(SteamVR_Input_Sources.Any, 320, 0.4f, 0.15f);
    Feed m_n_vib = new Feed(SteamVR_Input_Sources.Any, 320, 0.7f, 0.15f);
    Feed m_s_vib = new Feed(SteamVR_Input_Sources.Any, 320, 1.0f, 0.15f);

    Feed l_w_vib = new Feed(SteamVR_Input_Sources.Any, 320, 0.4f, 0.2f);
    Feed l_n_vib = new Feed(SteamVR_Input_Sources.Any, 320, 0.7f, 0.2f);
    Feed l_s_vib = new Feed(SteamVR_Input_Sources.Any, 320, 1.0f, 0.2f);
    //END OF BASIC FEEDS

    //FEED FOR PRESET PATTERNS HERE
    public List<Feed> feedSuccess = new List<Feed>();
    public bool success = false;
    Feed succ1 = new Feed(SteamVR_Input_Sources.Any, 320, 0.6f, 0.1f);
    Feed succ2 = new Feed(SteamVR_Input_Sources.Any, 320, 0f, 0.1f);
    Feed succ3 = new Feed(SteamVR_Input_Sources.Any, 320, 1, 0.1f);

    public List<Feed> feedWarning = new List<Feed>();
    public bool warning = false;
    Feed warn1 = new Feed(SteamVR_Input_Sources.Any, 320, 1f, 0.1f);
    Feed warn2 = new Feed(SteamVR_Input_Sources.Any, 320, 0f, 0.2f);
    Feed warn3 = new Feed(SteamVR_Input_Sources.Any, 320, 1, 0.1f);

    public List<Feed> feedFailure = new List<Feed>();
    public bool failure = false;
    Feed fail1 = new Feed(SteamVR_Input_Sources.Any, 320, 0.8f, 0.1f);
    Feed fail2 = new Feed(SteamVR_Input_Sources.Any, 320, 0f, 0.1f);
    Feed fail3 = new Feed(SteamVR_Input_Sources.Any, 320, 0.8f, 0.1f);
    Feed fail4 = new Feed(SteamVR_Input_Sources.Any, 320, 0f, 0.1f);
    Feed fail5 = new Feed(SteamVR_Input_Sources.Any, 320, 1f, 0.1f);
    Feed fail6 = new Feed(SteamVR_Input_Sources.Any, 320, 0f, 0.1f);
    Feed fail7 = new Feed(SteamVR_Input_Sources.Any, 320, 0.6f, 0.2f);

    public List<Feed> feedTest = new List<Feed>();
    public bool audio = false;
    Feed test1 = new Feed(SteamVR_Input_Sources.Any, 320, 0.6f, 0.1f);
    Feed test2 = new Feed(SteamVR_Input_Sources.Any, 320, 0f, 0.1f);
    Feed test3 = new Feed(SteamVR_Input_Sources.Any, 320, 1, 0.1f);

    public List<Feed> feedAudio = new List<Feed>();
    static string clipSucc = "Sounds/Ghetto";
    Feed audio1 = new Feed(SteamVR_Input_Sources.Any, 320, 0.6f, clipSucc, 0.04f);


    public FeedLibrary()
    {
        feedSuccess.Add(succ1);
        feedSuccess.Add(succ2);
        feedSuccess.Add(succ3);

        feedWarning.Add(warn1);
        feedWarning.Add(warn2);
        feedWarning.Add(warn3);

        feedFailure.Add(fail1);
        feedFailure.Add(fail2);
        feedFailure.Add(fail3);
        feedFailure.Add(fail4);
        feedFailure.Add(fail5);
        feedFailure.Add(fail6);
        feedFailure.Add(fail7);
    }


}
