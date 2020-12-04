using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FigmaToCanvas : MonoBehaviour
{
    public Canvas canvas;
    public ImportFigmaJSON figma;
    public Renderer mRenderer;
    public Button buttonPrefab;

    // Store width and height of figma and Unity canvas dimensions
    private float cWidth;
    private float cHeight;

    private List<FrameData> frames;


    // Start is called before the first frame update
    void Start()
    {
        // Store dimensions of Unity canvas
        cWidth = canvas.GetComponent<RectTransform>().rect.width;
        cHeight = canvas.GetComponent<RectTransform>().rect.height;

        frames = figma.GetFrames();

        foreach (var frame in frames)
        {
            if (frame.name == figma.GetStartingFrame())
            {
                RenderFrame(frame.id);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RenderFrame(string id)
    {

        // test - get frame texture on there and button as well
        FrameData frame = GetFrame(id);

        if (frame == null)
        {
            Debug.Log("Frame " + id + " not found!");
            return;
        }


        // set texture to Assets/figma_pictures/Test/[framename].png
        string path = "Assets/figma_pictures/Test/" + frame.name + ".png";
        byte[] data = System.IO.File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(data);

        mRenderer.material.SetTexture("_MainTex", tex);

        // delete all current children buttons of the canvas
        foreach (Transform child in canvas.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // loop through hotspots and populate canvas with invButtons
        foreach (var hotspot in frame.hotspots)
        {
            float xFrac = hotspot.x / (float)frame.width;
            float yFrac = hotspot.y / (float)frame.height;
            float wFrac = hotspot.w / (float)frame.width;
            float hFrac = hotspot.h / (float)frame.height;

            

            float bX = xFrac * cWidth;
            float bY = yFrac * cHeight;
            float bW = wFrac * cWidth;
            float bH = hFrac * cHeight;

            Button button = Instantiate(buttonPrefab);
            button.gameObject.name = hotspot.name;
            RectTransform rect = button.GetComponent<RectTransform>();

            rect.SetParent(canvas.transform, false);
            rect.anchoredPosition = new Vector3(bX, -bY, 0);
            rect.sizeDelta = new Vector2(bW, bH);


            string transitionID = hotspot.transition.parameters[0];

            button.onClick.AddListener(delegate { RenderFrame(transitionID); });
            //button.onClick.AddListener(delegate { printButtonPressed(transitionID); });

            //print(string.Format("Frame: {4}, hotspot: {5}, x: {0}, y: {1}, w: {2}, h: {3}", hotspot.x, hotspot.y, hotspot.w, hotspot.h, frame.name, hotspot.name));
        }

        // For each invButton, set onclick to renderframe of id of frame transition goes to
    }

    private FrameData GetFrame(string id)
    {
        foreach(FrameData frame in this.frames)
        {
            if (frame.id == id)
            {
                return frame;
            }
        }
        return null;
    }

    void sayhi()
    {
        Debug.Log("HIIII");
    }

    public void printButtonPressed(string button)
    {
        Debug.Log(button + " pressed!");
    }
}
