﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mood.Models.FaceApiData
{

    public class Rootobject
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public string faceId { get; set; }
        public Facerectangle faceRectangle { get; set; }
        public Faceattributes faceAttributes { get; set; }
    }

    public class Facerectangle
    {
        public int top { get; set; }
        public int left { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Faceattributes
    {
        public double smile { get; set; }
        public Headpose headPose { get; set; }
        public string gender { get; set; }
        public double age { get; set; }
        public Facialhair facialHair { get; set; }
        public string glasses { get; set; }
        public Emotion emotion { get; set; }
        public Blur blur { get; set; }
        public Exposure exposure { get; set; }
        public Noise noise { get; set; }
        public Makeup makeup { get; set; }
        public object[] accessories { get; set; }
        public Occlusion occlusion { get; set; }
        public Hair hair { get; set; }
    }

    public class Headpose
    {
        public double pitch { get; set; }
        public float roll { get; set; }
        public float yaw { get; set; }
    }

    public class Facialhair
    {
        public double moustache { get; set; }
        public double beard { get; set; }
        public double sideburns { get; set; }
    }

    public class Emotion
    {
        public double anger { get; set; }
        public double contempt { get; set; }
        public double disgust { get; set; }
        public double fear { get; set; }
        public double happiness { get; set; }
        public double neutral { get; set; }
        public double sadness { get; set; }
        public double surprise { get; set; }
    }

    public class Blur
    {
        public string blurLevel { get; set; }
        public float value { get; set; }
    }

    public class Exposure
    {
        public string exposureLevel { get; set; }
        public float value { get; set; }
    }

    public class Noise
    {
        public string noiseLevel { get; set; }
        public float value { get; set; }
    }

    public class Makeup
    {
        public bool eyeMakeup { get; set; }
        public bool lipMakeup { get; set; }
    }

    public class Occlusion
    {
        public bool foreheadOccluded { get; set; }
        public bool eyeOccluded { get; set; }
        public bool mouthOccluded { get; set; }
    }

    public class Hair
    {
        public float bald { get; set; }
        public bool invisible { get; set; }
        public Haircolor[] hairColor { get; set; }
    }

    public class Haircolor
    {
        public string color { get; set; }
        public float confidence { get; set; }
    }

}
