using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

//Custom 8
public partial class Wit3D : MonoBehaviour
{
    public Text myHandleTextBox;
    private bool actionFound = false;

    void Handle(string jsonString)
    {

        if (jsonString != null)
        {

            RootObject theAction = new RootObject();
            JsonConvert.PopulateObject(jsonString, theAction);

            HandleActionObject(theAction);
        }//END OF IF
    }

    void HandleActionObject(RootObject rootObject)
    {
        if (rootObject == null || rootObject.intents == null || rootObject.intents.Count < 1)
        {
            Debug.Log("No intent found");
            return;
        }

        var intent = rootObject.intents.First().name;
        TriggerActions(intent, rootObject.entities);
        DisplayIntent(intent, rootObject.entities);
        actionFound = true;

        if (!actionFound)
        {
            myHandleTextBox.text = "Request unknown, please ask a different way.";
        }
        else
        {
            actionFound = false;
        }
    }

    void TriggerActions(string intent, Entities entities)
    {
        if (intent.Contains("engine"))
        {
            if (intent.Contains("start"))
            {
                carController.instance.playSound();
                return;
            }
            else if (intent.Contains("stop"))
            {
                carController.instance.stopSound();
                return;
            }
        }

        if (intent == "change_colour")
        {
            if (entities.colour == null || entities.colour.Count < 1)
            {
                return;
            }
            colourSwitcher.instance.colours(entities.colour.First().value);
            return;
        }

        var command = GetActionCommand(intent);
        carController.instance.triggerAnimation(command);
    }

    void DisplayIntent(string intent, Entities entities)
    {
        var sentence = intent.Replace('_', ' ');
        if (intent == "change_colour")
        {
            if (entities.colour == null || entities.colour.Count < 1)
            {
                myHandleTextBox.text = "Colour could not be obtained";
                return;
            }
            sentence += " " + entities.colour.First().value;
        }
        myHandleTextBox.text = sentence;
    }

    string GetActionCommand(string input)
    {
        var parts = input.Split('_');
        var sb = new StringBuilder(parts[0]);
        for (int i = 1; i < parts.Length; i++)
        {
            var capitalised = parts[i].First().ToString().ToUpper() + parts[i].Substring(1);
            sb.Append(capitalised);
        }

        return sb.ToString();
    }
    //END OF HANDLE VOID

}//END OF CLASS


//Custom 9
public class PotentialEntity
{
    public string body { get; set; }
    public double confidence { get; set; }
    public int end { get; set; }
    public List<object> entities { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public string role { get; set; }
    public int start { get; set; }
    public string type { get; set; }
    public string value { get; set; }
}

public class Entities
{
    [JsonProperty("open:open")]
    public List<PotentialEntity> open { get; set; }

    [JsonProperty("close:close")]
    public List<PotentialEntity> close { get; set; }

    [JsonProperty("colour:colour")]
    public List<PotentialEntity> colour { get; set; }
}

public class Intent
{
    public double confidence { get; set; }
    public string id { get; set; }
    public string name { get; set; }
}

public class RootObject
{
    public string text { get; set; }
    public Entities entities { get; set; }
    public List<Intent> intents { get; set; }
    public object traits { get; set; }
}