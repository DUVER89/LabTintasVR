using UnityEngine;
public class panoramaCapture : MonoBehaviour
{
    public Camera targetCamera;
    public RenderTexture cubeMapleft;
    public RenderTexture equirecRTR;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Capture();

    }
    public void Capture()
    {
        targetCamera.RenderToCubemap(cubeMapleft);
        cubeMapleft.ConvertToEquirect(equirecRTR);
        Save(equirecRTR);
    }

    public void Save(RenderTexture rt)
    {
        Texture2D tex = new Texture2D(rt.width, rt.height);
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        RenderTexture.active = null;

        byte[] bytes = tex.EncodeToJPG();
        string path = Application.dataPath + "/panorama" + "jpg";

        System.IO.File.WriteAllBytes(path, bytes);
    }
}
