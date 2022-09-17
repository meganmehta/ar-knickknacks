using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class timeTeller2 : MonoBehaviour
{
    public GameObject timeObject2;

    string url = "http://worldtimeapi.org/api/timezone/America/New_York";
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTime", 2f, 10f);
    }

    // Update is called once per frame
    void UpdateTime()
    {
        StartCoroutine(GetRequest(url));
    }
     IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.result ==  UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);

                int startDateTime = webRequest.downloadHandler.text.IndexOf("datetime", 0);
                int startTime = webRequest.downloadHandler.text.IndexOf("T", startDateTime);
                int endTime = webRequest.downloadHandler.text.IndexOf(".", startTime);

                string time = webRequest.downloadHandler.text.Substring(startTime+1, 5);

                timeObject2.GetComponent<TextMeshPro>().text = time;
            }
        }
    }
}


