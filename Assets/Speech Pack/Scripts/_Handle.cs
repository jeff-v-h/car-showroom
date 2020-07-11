using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
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

            GetIntent(theAction);

            if (!actionFound)
            {
                myHandleTextBox.text = "Request unknown, please ask a different way.";
            }
            else
            {
                actionFound = false;
            }

        }//END OF IF

    }

    void GetIntent(RootObject rootObject)
    {
        if (rootObject == null || rootObject.intents == null || rootObject.intents.Count < 1)
        {
            Debug.Log("No intent found");
            return;
        }

        myHandleTextBox.text = rootObject.intents.First().name.Replace('_', ' ');
        actionFound = true;
    }//END OF HANDLE VOID

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