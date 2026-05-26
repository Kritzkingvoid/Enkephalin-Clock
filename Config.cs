using System;
using System.Collections.Generic;
using System.IO;

namespace EnkephalinClock
{
    public static class Config
    {
        public static bool UseRoman = true;
        public static bool EnableMarqueeSchedule = true;
        public static bool EnableSounds = true;
        public static bool ShowInTaskBar = true;
        public static bool UseMilitaryTime = false;
        public static bool AllowInterruptMarquee = false;
        public static string WelcomeMessage = "Welcome back, Manager";
        public static string IntroSound = "intro.wav";
        public static double MarqueeSpeed = 80;

        public static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt");
        public static void Load()
        {
            if (!File.Exists(ConfigPath))
            {
                SaveDefaults();
                return;
            }

            string[] lines = File.ReadAllLines(ConfigPath);
            foreach (string raw in lines)
            {
                if (string.IsNullOrWhiteSpace(raw))
                {
                    continue;
                }

                if (raw.StartsWith("#"))
                {
                    continue;
                }

                int splitIndex = raw.IndexOf('=');

                if (splitIndex < 0)
                {
                    continue;
                }

                string key = raw.Substring(0, splitIndex).Trim();

                string value = raw.Substring(splitIndex + 1).Trim();
                ParseValue(key, value);
            }
        }
        private static void ParseValue(
            string key,
            string value)
        {
            switch (key.ToLower())
            {
                case "useroman":
                    UseRoman = ParseBool(value);
                    break;

                case "enablemarqueeschedule":
                    EnableMarqueeSchedule = ParseBool(value);
                    break;

                case "showintaskbar":
                    EnableMarqueeSchedule = ParseBool(value);
                    break;

                case "usemilitarytime":
                    UseMilitaryTime = ParseBool(value);
                    break;

                case "enablesounds":
                    EnableSounds = ParseBool(value);
                    break;

                case "welcomemessage":
                    WelcomeMessage = value;
                    break;

                case "introsound":
                    IntroSound = value;
                    break;

                case "marqueespeed":
                    if (double.TryParse(value, out double speed))
                    {
                        MarqueeSpeed = speed;
                    }
                    break;
            }
        }
        private static bool ParseBool(string value)
        {
            value = value.ToLower();

            return
                value == "true" ||
                value == "1" ||
                value == "yes" ||
                value == "on";
        }

        public static void SaveDefaults()
        {
            List<string> lines =
                new List<string>
                {
                    "@Kritzkingvoid | GitHub | EnkephalinClock",
                    "# Enkephalin Config | Delete this file and run to reset all settings to default",
                    "",
                    "UseRoman=true",
                    "EnableMarqueeSchedule=true",
                    "EnableSounds=true",
                    "ShowInTaskBar=true",
                    "UseMilitaryTime=false",

                    "# Currently if set to true, the notification will glitch and overlap be aware",
                    "AllowInterruptMarquee=false",  
                    "",
                    "WelcomeMessage=Welcome back, Manager",
                    "IntroSound=intro.wav",
                    "# Notification speed (80 -Default)",
                    "MarqueeSpeed=80"
                };
            File.WriteAllLines(ConfigPath, lines);
        }
    }
}