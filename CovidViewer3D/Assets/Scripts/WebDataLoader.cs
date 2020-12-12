using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class WebDataLoader : MonoBehaviour
{
    // UI elements
    [SerializeField] private TMP_Text _Text;
    [SerializeField] private TMP_Dropdown _CountryMenu;
    [SerializeField] private TMP_Dropdown _YMenu;
    [SerializeField] private TMP_Dropdown _SizeMenu;

    private string _displaytext;
    public int dispLength = 1200;

    // web data things
    private string _webtext;
    public string url = "https://covid.ourworldindata.org/data/owid-covid-data.csv";

    
    // stored data
    public List<string> categories;
    public List<string> countries;
    public List<List<List<float>>> DataList;

    // Start is called before the first frame update
    void Start()
    {
        _webtext = "downloading data......\n\nloading covid database from https://covid.ourworldindata.org/data/owid-covid-data.csv......  \n\nplease wait......  ";
        _Text.text = _webtext;

        DataList = new List<List<List<float>>>(); 

        StartCoroutine(Get(url));

    }

    private void DoThingsWithWebData()
    {
        TextUpdate("Done Loading!... Parsing Data");

        ParseData();

        UIUpdate();

        TextUpdate("Data ready for exploring: ");

        this.GetComponent<DataSpheres>().DataList = DataList; 
    }

    private void ParseData()
    {
        string[] lines = _webtext.Split('\n');
        string[] temp = lines[0].Split(',');

        categories = new List<string>(temp);
        categories.RemoveRange(0, 4);

        int rows = lines.Length - 1;
        int cols = temp.Length;

        List<List<float>> tempList = new List<List<float>>();       // it seems really stupid to do it this way.... but maybe it works
        for (int i = 0; i < cols-4; i++)
        {
            tempList.Add(new List<float>());
        }

        string tempId = lines[1].Split(',')[2];

        for (int i = 1; i < rows; i++)
        {
            string[] r = lines[i].Split(',');

            if (String.Equals(r[2], tempId, StringComparison.OrdinalIgnoreCase))
            {
                for (int k = 4; k < cols; k++)
                {
                    float a = 0; 
                    float.TryParse(r[k], out a);
                    tempList[k-4].Add(a);
                }
            }
            else
            {
                // done with last country --> add to the lists
                // passing this as a new list will add a clone of tempList to
                // DataList instead of a reference which will continue to change
                // as the list as tempList is cleared and updated
                // this took me so grrrr long to figure out....

                DataList.Add(new List<List<float>>(tempList));  
                countries.Add(tempId);


                tempId = r[2];

                for (int k = 4; k < cols; k++)
                {
                    float a = 0;
                    float.TryParse(r[k], out a);

                    tempList[k - 4] = new List<float>();
                    tempList[k-4].Add(a);
                }
            }
        }

        DataList.Add(tempList);
        countries.Add(tempId);
    }

    private IEnumerator Get(string url)
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(url))
        {
            yield return uwr.SendWebRequest();

            _webtext = uwr.downloadHandler.text;

            TextUpdate("Recieved: " + _webtext.Substring(0, dispLength));

            DoThingsWithWebData();
        }
    }

    private void UIUpdate()
    {
        _CountryMenu.AddOptions(countries);
        _YMenu.AddOptions(categories);
        _SizeMenu.AddOptions(categories);
    }

    private void TextUpdate(string t)
    {
        _Text.text = t;
    }
}
