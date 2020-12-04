using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportFigmaJSON : MonoBehaviour
{
    public TextAsset jsonFile;


    private List<FrameData> frames;
    private string startingFrame;

    void Awake()
    {
        FigmaData figmaData = JsonUtility.FromJson<FigmaData>(jsonFile.text);

        this.startingFrame = figmaData.startingFrame;

        frames = new List<FrameData>();
        
        // Access JSON data, example:
        foreach (var frame in figmaData.frames)
        {
            this.frames.Add(frame);

            foreach (var hotspot in frame.hotspots)
            {
                print(string.Format("Frame: {4}, hotspot: {5}, x: {0}, y: {1}, w: {2}, h: {3}", hotspot.x, hotspot.y, hotspot.w, hotspot.h, frame.name, hotspot.name));
            }
        }

    }

    public List<FrameData> GetFrames()
    {
        return frames;
    }

    public string GetStartingFrame()
    {
        return startingFrame;
    }
}