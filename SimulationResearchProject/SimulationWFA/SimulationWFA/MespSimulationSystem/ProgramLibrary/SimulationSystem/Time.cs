﻿using System.Diagnostics;

namespace SimulationSystem.Timer
{
    public static class Time
    {
        public static float deltaTime;
        public static float lastFrame;
        public static float currentFrame;

        static Stopwatch sw;

        public static void StartTimer()
        {
            deltaTime = 0;
            lastFrame = 0;
            currentFrame = 0;
            sw = new Stopwatch();
            sw.Start();
        }

        public static void StopTimer()
        {
            sw.Stop();
        }

        public static void UpdateTimer()
        {
            currentFrame = sw.ElapsedMilliseconds / 1000f;
            deltaTime = currentFrame - lastFrame;
            lastFrame = currentFrame;
        }
    }
}