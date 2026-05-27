using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace EnkephalinClock
{
    public partial class MainWindow : Window
    {


        //@Kritzkingvoid C# | Visual Studio 2022 
        //Lobotomy Corp got trashedd, I hate working at Multicrack
        //⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⠛⣛⣛⣛⣛⡛⠛⠻⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
        //⣿⣿⣿⣿⣿⣿⣿⣿⠿⠋⢀⣴⣿⠿⠛⠛⠛⠿⣿⠶⠀⠀⠠⣈⡉⠙⠛⠿⣿⣿⣿⣿⣿⣿⣿⣿
        //⣿⣿⣿⣿⣿⠟⠋⣁⠐⠚⠛⢛⠁⠀⠲⣶⠤⠀⠠⠀⠲⣦⠀⠀⠙⠛⠂⠀⠀⠙⢿⣿⣿⣿⣿⣿
        //⣿⣿⠟⠛⠁⠠⠚⠛⠀⠿⠖⠈⠉⢉⣀⣤⣤⣶⣶⣦⣄⡈⠁⠘⠦⡈⠃⡀⢨⣷⣄⠈⠻⣿⣿⣿
        //⣿⡇⢠⣷⡀⠀⠀⢉⣀⣴⣶⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣦⣄⣀⠀⠀⠉⠉⠳⡄⠉⠉⢻
        //⣿⡇⠘⠟⠁⣰⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣄⠀⠀⠀⢀⡠⣧⠀
        //⣿⡿⠀⠀⠘⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⡀⠀⠉⠻⡿⠀
        //⣿⠃⠠⠀⠆⠀⠿⣿⡇⠉⠻⣿⣿⣿⣿⣿⣿⠛⠛⠿⢿⣿⣿⣿⡟⠉⠛⠿⠿⣟⠁⠀⠒⠀⠀⣴
        //⣿⠀⠀⠀⣀⡀⢠⣀⢁⣷⣤⡀⠉⠻⠿⣿⡿⠀⢲⣤⣀⠀⠙⠛⠷⠀⢳⣦⣄⡀⠀⠀⠀⠀⠀⢸
        //⣿⠀⢀⣿⣿⡇⠚⠛⠛⠛⠛⠛⠓⠂⢠⣈⣀⣀⠀⠛⣿⣿⡟⠒⠒⠒⠚⠛⠿⢿⡇⣤⣷⡄⢻⣿
        //⣿⠀⣿⣿⣿⡇⣿⡇⠘⠋⢀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣇⣘⣁⣠⣶⣾⡇⣿⣿⣧⠘⣿
        //⡏⠀⣿⣿⣿⡇⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠁⣿⣿⣿⠀⣿
        //⣷⠀⠀⣿⣿⣇⠈⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠏⢠⡿⢿⣿⠀⣿
        //⣿⣷⣦⠀⠉⠀⠀⠄⠈⠛⠿⣿⣿⣿⣿⣿⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠟⠁⠀⠙⠇⠀⠙⢀⣿
        //⣿⣿⣿⠀⠳⣶⣶⣶⣶⣶⣦⣄⡉⠉⠙⠛⠀⠒⠀⠘⠛⠛⠛⠋⢉⣁⣤⣶⡶⠂⢀⣴⣶⣶⣿⣿
        //⣿⣿⣿⣷⣤⡄⢹⣿⣿⡿⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢿⣿⣿⣿⡇⠰⠿⠛⢿⣿⣿⣿
        //⣿⣿⣿⡟⠛⣷⠀⢿⣿⠀⠐⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⡿⠦⠀⠀⢸⣿⣿⣿
        //⣿⣿⣿⣧⣀⠈⠀⠼⠿⠷⠶⠂⠀⠀⠀⣀⣀⣀⣀⠀⠀⠀⠠⠤⢴⣿⡟⠛⢛⠀⣶⣾⣿⣿⣿⣿
        //⣿⣿⣿⣿⣿⣿⣀⣴⣶⣶⣤⣀⣀⣤⣤⣤⣤⣤⣤⣄⡀⠀⠀⣤⣤⣈⣁⣀⣤⣿⣿⣿⣿⣿⣿⣿

        //Func mode is currently useless, earlier versions had a meter version that was scrapped,
        //but I might add it back in the future so I'm keeping the enum here for now
        enum FuncMode
        {
            Clock,
            Meter
        }
        private bool isDragging = false;
        private Point startMouseScreenPoint;
        private Point startWindowLocation;

        private BitmapImage _dayImage;
        private BitmapImage _nightImage;
        private bool _isNight;

        //Children of The City YEEEEEEEEEE, purely cosmetic, but I like it
        private bool _cityOpen = true;
        private bool _cityAnimating = false;

        private FuncMode func = FuncMode.Clock;

        private MarqueeController MarqueeController;

        private int lastSecond = -1;
        private enum ClockState
        {
            Live,
            Draining
        }


        private ClockState clockState = ClockState.Live;

        public MainWindow()
        {
            ShowInTaskbar = Config.ShowInTaskBar;
            InitializeComponent();
            LoadAllUserImages();
            Config.Load();
            if (Config.CanResizeWindow)
            {
                this.ResizeMode = ResizeMode.CanResizeWithGrip;
            }
            else
            {
                this.ResizeMode = ResizeMode.NoResize;
            }
            this.Width = Config.WindowWidth;
            this.Height = Config.WindowHeight;

            _dayImage = LoadBitmap("Day.png");
            _nightImage = LoadBitmap("Night.png");

            MarqueeController = new MarqueeController(AnnounceContainer, MarqueeTextBlock, MarqueeContainer, MarqueeTransform);

            DispatcherTimer clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromMilliseconds(50);
            clockTimer.Tick += (s, e) =>
            {
                RenderClock();
            };

            clockTimer.Start();

            AnimateQualiCounter(true);
            MarqueeController.PlayWav(Config.IntroSound);

            MarqueeController.LoadMarqueeSchedule("schedule.txt");
            MarqueeController.StartScheduleWatcher();

        }
        private BitmapImage LoadBitmap(string name)
        {
            string filePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Images",
                name);

            BitmapImage bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }
        private void LoadAllUserImages()
        {
            var imageTargets = new (Image Control, string FileName)[]
            {
                (ContainerImage, "Images/Notification_Back.png"),
                (ImageOuterImage, "Images/Notification_Outline.png"),
                (MeltDownImage, "Images/MeltDown.png"),
                (EnkephalinImage, "Images/Enkephalin.png"),
                (CityImage, "Images/City.png"),
                (StatusDay, "Images/Day.png"),
            };

            foreach (var target in imageTargets)
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, target.FileName);

                if (!File.Exists(filePath)) continue;

                try
                {
                    using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = stream;
                        bitmap.EndInit();

                        target.Control.Source = bitmap;
                    }
                }
                catch (Exception)
                {
                }
            }
        }
     

        void RenderClock()
        {
            DateTime now = DateTime.Now;

            int currentSecond = now.Second;

            double progress = currentSecond + now.Millisecond / 1000.0;
            if (lastSecond == 59 && currentSecond == 0)
            {
                StartDrain();
            }

            lastSecond = currentSecond;

            if (clockState == ClockState.Live)
            {
                EnkephalinBar.Value = progress;
            }

            SecondsCount.Text = now.Second.ToString("00") + " / 60";
            MinutesCount.Text = now.Minute.ToString("00");

            UpdateMinuteBar();
            UpdateDayNightImage(now);
            UpdateHourDisplay(now);
        }
        private void UpdateHourDisplay(DateTime now)
        {
            int displayHour;

            if (Config.UseMilitaryTime)
            {
                displayHour = now.Hour;
            }
            else
            {
                displayHour = now.Hour % 12;

                if (displayHour == 0)
                {
                    displayHour = 12;
                }
            }

            string formattedHour;

            if (Config.UseRoman)
            {
                formattedHour = ToRoman(displayHour);
            }
            else
            {
                formattedHour = displayHour.ToString("00");
            }

            if (Config.UseMilitaryTime)
            {
                RomanNumeral.Text = formattedHour;
            }
            else
            {
                RomanNumeral.Text = $"{formattedHour}";
            }
        }
        private void UpdateDayNightImage(DateTime now)
        {
            bool shouldNight = now.Hour >= 12;

            if (shouldNight == _isNight)
            {
                return;
            }

            _isNight = shouldNight;

            StatusDay.Source = shouldNight
                ? _nightImage
                : _dayImage;
        }
        void StartDrain()
        {
            if (clockState == ClockState.Draining)
            {
                return;
            }

            clockState = ClockState.Draining;

            double startValue = EnkephalinBar.Value;

            EnkephalinBar.BeginAnimation(ProgressBar.ValueProperty, null);

            DoubleAnimation anim = new DoubleAnimation();

            anim.From = startValue;
            anim.To = 0;

            anim.Duration = TimeSpan.FromSeconds(1.2);

            QuadraticEase ease = new QuadraticEase();
            ease.EasingMode = EasingMode.EaseIn;

            anim.EasingFunction = ease;

            anim.Completed += (s, e) =>
            {
                EnkephalinBar.BeginAnimation(ProgressBar.ValueProperty, null);
                EnkephalinBar.Value = 0;

                clockState = ClockState.Live;

                lastSecond = 0;
            };

            EnkephalinBar.BeginAnimation(ProgressBar.ValueProperty, anim);
        }

        void AnimateQualiCounter(bool show)
        {
            QualiCounter.Visibility = Visibility.Visible;

            Thickness from;
            Thickness to;

            if (show == true)
            {
                from = new Thickness(26, 0, 0, -10);
                to = new Thickness(26, 0, 0, -90);
            }
            else
            {
                from = new Thickness(26, 0, 0, -90);
                to = new Thickness(26, 0, 0, -10);
            }

            var anim = new ThicknessAnimation();

            anim.From = from;
            anim.To = to;
            anim.Duration = TimeSpan.FromSeconds(0.7);

            QuadraticEase ease = new QuadraticEase();

            if (show == true)
            {
                ease.EasingMode = EasingMode.EaseIn;
            }
            else
            {
                ease.EasingMode = EasingMode.EaseOut;
            }

            anim.EasingFunction = ease;

            anim.Completed += (s, e) =>
            {
                AnimateCity();
            };

            QualiCounter.BeginAnimation(Grid.MarginProperty, anim);
        }

        void AnimateCity()
        {
            var anim = new ThicknessAnimation();

            anim.From = new Thickness(22, 0, 0, 0);
            anim.To = new Thickness(22, -69, 0, 0);
            anim.Duration = TimeSpan.FromSeconds(0.7);

            anim.Completed += (s, e) =>
            {
                MarqueeController.PlayMarquee(Config.WelcomeMessage, 1, "");
            };
            CityImage.BeginAnimation(Grid.MarginProperty, anim);
        }
        private void ToggleCity()
        {
            if (_cityAnimating)
            {
                return; 
            }
            MarqueeController.PlayWav("scifi.wav");  
            _cityAnimating = true;

            var anim = new ThicknessAnimation
            {
                Duration = TimeSpan.FromSeconds(0.7),
                EasingFunction = new QuadraticEase
                {
                    EasingMode = EasingMode.EaseInOut

                }
            };
            if (!_cityOpen)
            {
                anim.From = new Thickness(22, 0, 0, 0);
                anim.To = new Thickness(22, -69, 0, 0);

                anim.Completed += (s, e) =>
                {
                    _cityAnimating = false;
                };
            }
            else
            {
                anim.From = new Thickness(22, -69, 0, 0);
                anim.To = new Thickness(22, 0, 0, 0);

                anim.Completed += (s, e) =>
                {
                    _cityAnimating = false;
                };
            }

            CityImage.BeginAnimation(Grid.MarginProperty, anim);

            _cityOpen = !_cityOpen;
        }
        public string ToRoman(int number)
        {
            string[] thousands = { "", "M", "MM", "MMM" };
            string[] hundreds = { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
            string[] tens = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
            string[] ones = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };

            return thousands[number / 1000] +
                   hundreds[(number % 1000) / 100] +
                   tens[(number % 100) / 10] +
                   ones[number % 10];
        }
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            startMouseScreenPoint = PointToScreen(e.GetPosition(this));
            startWindowLocation = new Point(this.Left, this.Top);

            TitleBar.CaptureMouse();
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentMouseScreenPoint = PointToScreen(e.GetPosition(this));

                double deltaX = currentMouseScreenPoint.X - startMouseScreenPoint.X;
                double deltaY = currentMouseScreenPoint.Y - startMouseScreenPoint.Y;

                this.Left = startWindowLocation.X + deltaX;
                this.Top = startWindowLocation.Y + deltaY;
            }
        }

        private void StatusDay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ToggleCity();
        }   
        private void MinutesCount_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MarqueeController.PlayDateTimeMarquee();
        }   

        private void TitleBar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                TitleBar.ReleaseMouseCapture();
            }
        }
        private void CityImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MarqueeController.PlayMarquee("ProjectMoon | Code by:Kritzkingvoid ", 1, "alert.wav");
        }   

        private void UpdateMinuteBar()
        {
            int minute = DateTime.Now.Minute;

            List<Brush> blocks = new List<Brush>();

            for (int i = 0; i < 10; i++)
            {
                if (i < minute / 6)
                {
                    blocks.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff3839")));
                }
                else
                {
                    blocks.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#130406")));
                }
            }
            MeltDownBar.ItemsSource = blocks;
        }

       
    }
}