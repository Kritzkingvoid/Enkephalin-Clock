using EnkephalinClock;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace EnkephalinClock
{
    public class MarqueeController
    {
        private readonly Grid _announceContainer;
        private readonly TextBlock _marqueeTextBlock;
        private readonly FrameworkElement _marqueeContainer;
        private readonly TranslateTransform _marqueeTransform;

        private bool _isMarqueePlaying = false;



        private class ScheduledMarquee
        {
            public TimeSpan Time { get; set; }
            public string Message { get; set; }
            public string Wav { get; set; }
        }

        private readonly List<ScheduledMarquee> _schedule = new List<ScheduledMarquee>();
        private readonly DispatcherTimer _scheduleTimer = new DispatcherTimer();
        private string _lastTriggeredMinute = "";

        public MarqueeController(Grid announceContainer, TextBlock marqueeTextBlock, FrameworkElement marqueeContainer, TranslateTransform marqueeTransform)
        {
            _announceContainer = announceContainer;
            _marqueeTextBlock = marqueeTextBlock;
            _marqueeContainer = marqueeContainer;
            _marqueeTransform = marqueeTransform;
        }

        public void PlayMarquee(string message, int loopCount, string wav = "")
        {
            if (_isMarqueePlaying)
            {
                if (!Config.AllowInterruptMarquee)
                {
                    return; 
                }
            }

            _isMarqueePlaying = true;
            _announceContainer.Visibility = Visibility.Visible;

            var showMarginAnim = new ThicknessAnimation
            {
                From = new Thickness(40, 120, 0, 0),
                To = new Thickness(40, 180, 0, 0),
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new QuadraticEase
                {
                    EasingMode = EasingMode.EaseOut
                }
            };

            showMarginAnim.Completed += (s, e) =>
            {
                _marqueeTextBlock.Text = message;
                StartMarqueeAnimation(message, loopCount);
            };

            PlayWav(wav);

            _announceContainer.BeginAnimation(Grid.MarginProperty,showMarginAnim);
        }

       

        private void StartMarqueeAnimation(string message, int loopCount)
        {
            _marqueeTextBlock.Text = message;
            _marqueeTextBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            _marqueeTextBlock.Arrange(new Rect(_marqueeTextBlock.DesiredSize));

            _marqueeContainer.UpdateLayout();

            double textWidth = _marqueeTextBlock.DesiredSize.Width;
            double containerWidth = _marqueeContainer.ActualWidth;

            double fromValue = containerWidth;
            double toValue = -textWidth;

            double speed = Config.MarqueeSpeed;
            double distance = containerWidth + textWidth;
            double durationSeconds = distance / speed;

            _marqueeTransform.BeginAnimation(TranslateTransform.XProperty, null);

            var marqueeAnim = new DoubleAnimation
            {
                From = fromValue,
                To = toValue,
                Duration = TimeSpan.FromSeconds(durationSeconds),
            };

            if (loopCount < 0)
            {
                marqueeAnim.RepeatBehavior = RepeatBehavior.Forever;
            }
            else
            {
                marqueeAnim.RepeatBehavior = new RepeatBehavior(loopCount);
            }

            if (loopCount > 0)
            {
                marqueeAnim.Completed += (s, e) =>
                {
                    var hideMarginAnim = new ThicknessAnimation
                    {
                        From = new Thickness(40, 180, 0, 0),
                        To = new Thickness(40, 70, 0, 0),
                        Duration = TimeSpan.FromSeconds(0.5),
                        EasingFunction = new QuadraticEase
                        {
                            EasingMode = EasingMode.EaseIn
                        }
                    };
                    hideMarginAnim.Completed += (_, __) =>
                    {
                        _announceContainer.Visibility = Visibility.Collapsed;
                        _isMarqueePlaying = false;
                    };
                    _announceContainer.BeginAnimation(Grid.MarginProperty, hideMarginAnim);
                };
            }
            _marqueeTransform.BeginAnimation(TranslateTransform.XProperty, marqueeAnim);
        }
        public void PlayWav(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || !Config.EnableSounds)
            {
                return;
            }
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", fileName);
                new SoundPlayer(path).Play();
            }
            catch
            {
            }
        }
        public void PlayDateTimeMarquee()
        {
            DateTime now = DateTime.Now;

            string dayName = now.DayOfWeek.ToString();
            string monthName = now.ToString("MMMM");
            int day = now.Day;
            int year = now.Year;

            string time = now.ToString("hh:mm:ss tt");

            string message = $"Current Date: {dayName}, {monthName} {day}, {year} | {time}";

            PlayMarquee(message, 1, "scifi.wav");
        }

        public void LoadMarqueeSchedule(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            _schedule.Clear();

            string[] lines = File.ReadAllLines(filePath);

            foreach (string raw in lines)
            {
                if (string.IsNullOrWhiteSpace(raw))
                {
                    continue;
                }

                try
                {

                    string line = raw.Trim();

                    int closeBracket = line.IndexOf(']');

                    if (closeBracket < 0)
                    {
                        continue;
                    }

                    string timeText = line.Substring(1, closeBracket - 1);

                    string remainder = line.Substring(closeBracket + 1).Trim();

                    if (remainder.StartsWith(":"))
                    {
                        remainder = remainder.Substring(1).Trim();
                    }

                    string[] parts = remainder.Split(new[] { ':' }, 2);

                    string message = parts[0].Trim();

                    string wav = "";

                    if (parts.Length > 1)
                    {
                        wav = parts[1].Trim();
                    }

                    if (TimeSpan.TryParse(timeText, out TimeSpan time))
                    {
                        _schedule.Add(new ScheduledMarquee
                        {
                            Time = time,
                            Message = message,
                            Wav = wav
                        });
                    }
                }
                catch
                {
                }
            }
        }

        public void StartScheduleWatcher()
        {
            _scheduleTimer.Interval =
                TimeSpan.FromSeconds(1);

            _scheduleTimer.Tick -= ScheduleTick;
            _scheduleTimer.Tick += ScheduleTick;

            _scheduleTimer.Start();
        }

        private void ScheduleTick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            string currentMinute = now.ToString("HH:mm");

            if (_lastTriggeredMinute == currentMinute)
            {
                return;
            }

            foreach (var item in _schedule)
            {
                if (item.Time.Hours == now.Hour && item.Time.Minutes == now.Minute)
                {
                    _lastTriggeredMinute = currentMinute;

                    PlayMarquee(item.Message, 1, item.Wav);
                    break;
                }
            }
        }
    }
}