using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

		string directoryPath = Path.Combine(Application.persistentDataPath, "Screenshots");
		string time = System.DateTime.Now.ToString("HH:mm:ss_dd-MMMM-yyyy");

		print(time);
	}

    public void TakeScreenshot()
	{
		StartCoroutine(TakeScreenshotAndShare());
	}

	private IEnumerator TakeScreenshotAndShare()
	{
		yield return new WaitForEndOfFrame();

		string directoryPath = Path.Combine(Application.persistentDataPath, "Screenshots");
		if (!Directory.Exists(directoryPath))
			Directory.CreateDirectory(directoryPath);

		string time = System.DateTime.Now.ToString("HH-mm-ss_dd-MMMM-yyyy");
		string fileName = "SimpleAR_" + time + ".png";

		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();

		string filePath = Path.Combine(directoryPath, fileName);
		File.WriteAllBytes(filePath, ss.EncodeToPNG());

		// To avoid memory leaks
		Destroy(ss);

		new NativeShare().AddFile(filePath)
			.SetSubject("Simple AR Screenshot").SetText("Hey look at this screenshot!")
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();
	}
}
