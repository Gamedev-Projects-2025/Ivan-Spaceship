using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static comboSystem;

public class comboSystem : MonoBehaviour
{

    //creating a class for combos
    [System.Serializable]
    public class Combo
    {
        //combo name
        public string comboName;

        //keys for the combo
        public KeyCode[] keys;

        //the target game object with the script to run once the action is performed
        public MonoBehaviour targetScript;

        //the name of the meothed to run from the target game object
        public string methodName;

    }

    //The list of all of our combos
    public List<Combo> combos;

    //The input delay between key strokes
    public float inputTimeout = 1f;

    //Keys that we might want to ingore like the directional keys
    public List<KeyCode> ignore;

    //The current set of keys that have been pressed
    private List<KeyCode> currentInput = new List<KeyCode>();

    //Time since last input
    private float lastInputTime;

    void Update()
    {
        //gathering inputs from the keyboard
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                HandleInput(key);
                break;

            }
        }

        //couting the time between key presses
        if (Time.time - lastInputTime > inputTimeout)
        {
            //reset if out of time
            ResetInput();
        }
    }

    void HandleInput(KeyCode key)
    {
        //Remebering time since last press
        lastInputTime = Time.time;

        //ignoring inputs that are in the ignore list
        if (!ignore.Contains(key))
        {
            currentInput.Add(key);
        }

        //checking if we match any combo in the list
        foreach (Combo combo in combos)
        {
            if (IsComboMatched(combo.keys))
            {
                ExecuteCombo(combo);
                ResetInput();
                break;
            }
        }
    }

    //comparing the combo to the input
    bool IsComboMatched(KeyCode[] comboKeys)
    {
        if (comboKeys.Length != currentInput.Count) return false;

        for (int i = 0; i < comboKeys.Length; i++)
        {
            if (comboKeys[i] != currentInput[i])
            {

                return false;
            }
        }
        return true;
    }

    //executing the combo
    void ExecuteCombo(Combo combo)
    {
        if (combo.targetScript != null && !string.IsNullOrEmpty(combo.methodName))
        {
            combo.targetScript.Invoke(combo.methodName, 0f);
        }
    }

    //reseting input
    void ResetInput()
    {
        currentInput.Clear();
    }
}
