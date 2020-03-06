using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/**
 * In this Class you can activate your Feed patterns in the following way: 
 * --Duplicate the template code for each pattern you want to use, replacing "PATTERN" with the boolean variable and "feed.CUSTOMPATTERN" with feed.NAMEOFFEEDLIST
 *                                                                              Happy Coding!
 **/
public class FeedManager : MonoBehaviour
{
    public FeedLibrary feed = new FeedLibrary();
    public int counter = 0;
    public SteamVR_Action_Vibration ControllerVibration;
    public SteamVR_Input_Sources activeHand;

    private IEnumerator coroutineHaptic;
    private IEnumerator coroutineHapticGenerator;
    private IEnumerator currentVibrationPattern;
    [Range(64, 8192)] public int numberOfSamples = 64;
    public FFTWindow fftWindow;

    /*
     * This IEnumerator is executed as a coroutine
     * it manages all the haptic events inside the while
     * if you need an haptic feedback just set the specific boolean to true (create it in the dedicated section above)
     * You can add more haptic feedbacks by copying the template below and inserting it inside the while scope
     * 
     * 
     * if (feed.PATTERN)
            {
                counter++;
                currentVibrationPattern = Vibrate(feed.CUSTOMPATTERN);      
                StartCoroutine(currentVibrationPattern);
                feed.PATTERN = false;
            }
     */
    private IEnumerator FeedbackManager()
    {
        while (true)
        {
            if (feed.success)
            {
                counter++;
                currentVibrationPattern = Vibrate(feed.feedSuccess);       
                StartCoroutine(currentVibrationPattern);
                feed.success = false;
            }
            if (feed.warning)
            {
                counter++;
                currentVibrationPattern = Vibrate(feed.feedWarning);       
                StartCoroutine(currentVibrationPattern);
                feed.warning = false;
            }
            if (feed.failure)
            {
                counter++;
                currentVibrationPattern = Vibrate(feed.feedFailure);      
                StartCoroutine(currentVibrationPattern);
                feed.failure = false;
            }
            if (feed.audio)
            {
                counter++;
                currentVibrationPattern = Vibrate(feed.feedAudio);      
                StartCoroutine(currentVibrationPattern);
                feed.audio = false;
            }

            //vvvvvvv  ) ADD AND MODIFY THE TEMPLATE CODE FOR YOUR PATTERNS HERE vvvvvvv
            /** 
            if (PATTERN)
            {
                counter++;
                currentVibrationPattern = Vibrate(feed.CUSTOMPATTERN);       
                StartCoroutine(currentVibrationPattern);
                PATTERN = false;
            }
            **/

            //^^^^^^^               END OF CUSTOM PATTERNS SECTION                ^^^^^^^


            yield return null;
        }
    }

    IEnumerator HapticGenerator(int attuale, List<Feed> tremor)
    {
        AudioSource audioS = GetComponent<AudioSource>();
        bool playing = true;
        while (playing)
        {
            if (counter > attuale)
            {
                yield break;
            }
            //generate haptic from audio
            float[] spectrum = new float[numberOfSamples];
            audioS.GetSpectrumData(spectrum, 0, fftWindow);
            float audioSum = 0;
            for (int i = 0; i < numberOfSamples; i++)
            {
                audioSum += spectrum[i];
            }
            audioSum = audioSum / numberOfSamples;
            if (audioSum > tremor[0].Treshold)
            {
                ControllerVibration.Execute(0f, 0.05f, 320, 1, activeHand);
            }
            playing = audioS.isPlaying;
            yield return null;
        }
    }

    IEnumerator Vibrate(List<Feed> list)                          
    {   
        counter++;
        int attuale = counter;
        if (list[0].Audio != null)
        {
            AudioSource audio = GetComponent<AudioSource>();
            AudioClip clip = Resources.Load(list[0].Audio) as AudioClip;
            audio.clip = clip;
            audio.Play();
            if (list[0].Treshold == 0)
            {
                foreach (Feed p in list)
                {
                    if (counter > attuale)
                    {
                        break;
                    }
                    else
                    {
                        if (p.Source == SteamVR_Input_Sources.Any)
                    {
                        ControllerVibration.Execute(0, p.Duration, p.Frequency, p.Amplitude, activeHand);
                    }
                    else
                    {
                        ControllerVibration.Execute(0, p.Duration, p.Frequency, p.Amplitude, p.Source);
                    }
                        yield return new WaitForSeconds(p.Duration);
                    }
                }
            }
            else
            {
                coroutineHapticGenerator = HapticGenerator(attuale, list);
                yield return StartCoroutine(coroutineHapticGenerator);
            }
        }
        else
        {
            foreach (Feed p in list)
            {
                if (counter > attuale)
                {
                    break;
                }
                else
                {
                    if (p.Source == SteamVR_Input_Sources.Any)
                    {
                        ControllerVibration.Execute(0, p.Duration, p.Frequency, p.Amplitude, activeHand);
                    }
                    else
                    {
                        ControllerVibration.Execute(0, p.Duration, p.Frequency, p.Amplitude, p.Source);
                    }

                    yield return new WaitForSeconds(p.Duration);
                }
            }
        }
    }

    void Start()
    {
        coroutineHaptic = FeedbackManager();
        StartCoroutine(coroutineHaptic);
    }

}
