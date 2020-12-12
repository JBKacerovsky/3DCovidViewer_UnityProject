using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class DataSpheres : MonoBehaviour
{

    public List<List<List<float>>> DataList;

    // UI things
    [SerializeField] private TMP_Dropdown _CountryMenu;
    [SerializeField] private TMP_Dropdown _YMenu;
    [SerializeField] private TMP_Dropdown _SizeMenu;
    [SerializeField] private InputField _yMax;
    [SerializeField] private InputField _sMax; 

    // Sphere things
    [SerializeField] private GameObject SpherePrefab;
    private Transform _parent;
    private  Gradient map;


    // Scale adjustments
    [SerializeField] private int _xAdjust = -50; 
    private int offset = 0;
    [SerializeField] private int _offsetAmount = 70;
    [SerializeField] private string yMax_Default = "100";
    [SerializeField] private string sMax_Default = "50";
    private List<float> _sphereY;
    private List<float> _sphereS;

    private float yMaxValue;
    private float sMaxValue; 

    private bool _scaleSet = false; 

    private void Start()
    {
        _parent = this.transform;
        _yMax.text = yMax_Default;
        _sMax.text = sMax_Default;

        map = ColorMap(); 
    }

    public void OnAddPress()
    {
        int c = _CountryMenu.value;
        int y = _YMenu.value;
        int s = _SizeMenu.value;

        if (!_scaleSet)
        {
            yMaxValue = DataList[c][y].Max();
            sMaxValue = DataList[c][s].Max();

            _scaleSet = true;
        }

        Color color = map.Evaluate(Random.Range(0.0f, 1.0f));

        float yAdjust = float.Parse(_yMax.text)/yMaxValue;
        float scaleAdjust = float.Parse(_sMax.text)/sMaxValue;

        _sphereY = DataList[c][y];
        _sphereS = DataList[c][s];


        for (int i = 0; i < _sphereS.Count; i++)
        {
            GameObject newDataSphere = Instantiate(SpherePrefab, _parent);
            newDataSphere.transform.position = new Vector3(i*_xAdjust, _sphereY[i] * yAdjust, offset);
            float locScale = _sphereS[i] * scaleAdjust; 
            newDataSphere.transform.localScale = new Vector3(locScale, locScale, locScale);
            newDataSphere.GetComponent<Renderer>().material.color = color;
        }

        offset += _offsetAmount; 
    }

    private UnityEngine.Gradient ColorMap()
    {
        UnityEngine.Gradient map = new UnityEngine.Gradient();

        GradientColorKey[] colorKey = new GradientColorKey[8];

        // manual entry of matlab jet(8)
        colorKey[0].color = new Color(0f, 0f, 1f);
        colorKey[0].time = (float)1 / 8;

        colorKey[1].color = new Color(0f, 0.5f, 1f);
        colorKey[1].time = (float)2 / 8;

        colorKey[2].color = new Color(0f, 1f, 1f);
        colorKey[2].time = (float)3 / 8;

        colorKey[3].color = new Color(0.5f, 1f, 0.5f);
        colorKey[3].time = (float)4 / 8;

        colorKey[4].color = new Color(1f, 1f, 0f);
        colorKey[4].time = (float)5 / 8;

        colorKey[5].color = new Color(1f, 0.5f, 0f);
        colorKey[5].time = (float)6 / 8;

        colorKey[6].color = new Color(1f, 0f, 0f);
        colorKey[6].time = (float)7 / 8;

        colorKey[7].color = new Color(0.5f, 0f, 0f);
        colorKey[7].time = 1f;

        GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        map.SetKeys(colorKey, alphaKey);

        return (map);
    }

    public void DestroyAllChildren()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        _scaleSet = false; 
        offset = 0; 
    }

}
