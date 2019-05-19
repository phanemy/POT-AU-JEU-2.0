using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class DebugConsoleBuild : MonoBehaviour
{
    public List<Color> priorityColor;
    public Text debugTextPrefab;

    static List<Color> staticPriorityColor;
    static int fontSize = 10;
    static Text staticDebugTextPrefab;
    static private RectTransform debugImageTransform;
    static private Image debugImage;

    static bool isShow = false;
    static List<Text> debugText;
    static int nbMaxLine;
    static int maxPriority;
    static Vector3 offset;

    private void Start()
    {
        staticDebugTextPrefab = debugTextPrefab;
        staticPriorityColor = new List<Color>();
        foreach (Color col in priorityColor)
            staticPriorityColor.Add(col);

        debugText = new List<Text>();
        offset = new Vector3(0, -fontSize, 0);
        debugImageTransform = this.gameObject.GetComponent<RectTransform>();
        debugImage= this.gameObject.GetComponent<Image>();

        int height =  Screen.height;
        debugImageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        nbMaxLine = height - 20;
        nbMaxLine /= fontSize;
        maxPriority = priorityColor.Count;
        DebugConsoleBuild.Log(height.ToString(), 0);
        DebugConsoleBuild.Log(nbMaxLine.ToString(), 0);

    }

    public static void Log(string message,int priority)
    {
        Text t = Instantiate(staticDebugTextPrefab) as Text;
        t.rectTransform.SetParent(debugImageTransform,false);
        //t.rectTransform.localPosition = new Vector3(20, -20, 0);
        t.text = message;
        t.color = staticPriorityColor[Mathf.Clamp(priority, 0, staticPriorityColor.Count - 1)];
        t.enabled = true;
        if(priority == 0)
        {
            Debug.LogError(message);
        }else if (priority == 0)
        {
            Debug.LogWarning(message);
        }
        else
        {
            Debug.Log(message);
        }

        if (debugText.Count >= nbMaxLine)
        {
            Text temp = debugText[0];
            debugText.RemoveAt(0);
            Destroy(temp);
        }
        foreach (Text text in debugText)
        {
            text.rectTransform.localPosition += offset;
        }
        debugText.Add(t);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2))
        {
            isShow = !isShow;
            debugImage.enabled = isShow;
            foreach (Text text in debugText)
                text.enabled = isShow;
        }
    }
}
