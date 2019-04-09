using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KataTraining
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer MainTimer = new DispatcherTimer();
        DispatcherTimer innerTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            MainTimer = LoadMainTimer();
            ClearScreenAfterTimeout();

            LoadArray();
        }

        private DispatcherTimer LoadMainTimer()
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0, 0, random.Next(10, 20));
            dt.Tick += Dt_Tick;
            dt.Start(); 
            return dt; 
            //return new DispatcherTimer(new TimeSpan(0, 0, random.Next(10, 20)),
            //     DispatcherPriority.Normal,
            //     delegate
            //     {
            //         UpdateScreen();

            //     }, Application.Current.Dispatcher);
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            UpdateScreen(); 
        }

        private void UpdateScreen()
        {

            //System.Threading.Thread.Sleep(random.Next(10, 20) * 1000);
            int AttackIdx = random.Next(0, Attacks.Count);
            string word = Attacks[AttackIdx].description;
            label.Content = word;
            var idxImage = random.Next(0, Attacks[AttackIdx].image.Length);
            AttackImage.Source = Attacks[AttackIdx].image[idxImage];
            lblInfoUrl.Content = Attacks[AttackIdx].defenseVideo != null ? Attacks[AttackIdx].defenseVideo : ""; 
            ClearScreenAfterTimeout();
        }

        private void ClearScreenAfterTimeout()
        {
            innerTimer = new DispatcherTimer(new TimeSpan(0, 0, 3),
                             DispatcherPriority.Normal,
                             delegate
                             {
                                 label.Content = "...";
                                 innerTimer.Stop();
                                 AttackImage.Source = blank;
                             }, Application.Current.Dispatcher);

        }

        BitmapImage blank = new BitmapImage(new Uri("pack://application:,,,/images/blank.png"));

        void LoadArray()
        {
            Attacks = new List<AttackDesc>(){
        new AttackDesc{ description="haymaker swing",
            image=new BitmapImage[]{
                new BitmapImage(new Uri("pack://application:,,,/images/HaymakerPunchLeft.png")),
                new BitmapImage(new Uri("pack://application:,,,/images/HaymakerPunchRight.png")),
            }, defenseVideo="https://www.youtube.com/watch?v=HvK7aaKlJFE"
        },
        new AttackDesc{ description="overhead bottle/stick",
            image=new BitmapImage[]{
               new BitmapImage(new Uri("pack://application:,,,/images/OverheadStick2.png")),
               new BitmapImage(new Uri("pack://application:,,,/images/OverheadStickLeft.jpg")),
               new BitmapImage(new Uri("pack://application:,,,/images/OverheadStickLeft2.jpg")),
               new BitmapImage(new Uri("pack://application:,,,/images/OverheadStickRight.jpg")),
               new BitmapImage(new Uri("pack://application:,,,/images/OverheadStickRight2.jpg")),
                }, defenseVideo="https://www.youtube.com/watch?v=ilvz6WABCbM"
            },
        new AttackDesc{ description="front kick", image=new BitmapImage[]{ new BitmapImage(new Uri("pack://application:,,,/images/Kick1.png")) }  },
        new AttackDesc{ description="grab and punch",
            image =new BitmapImage[]{
                new BitmapImage(new Uri("pack://application:,,,/images/ShirtGrabWithLeft.png")),
                new BitmapImage(new Uri("pack://application:,,,/images/ShirtGrabWithRight.png"))
                }  },
        };
            return;
        }

        private List<AttackDesc> Attacks = new List<AttackDesc>();

        Random random = new Random();

        private class AttackDesc
        {
            public string description { get; set; }
            public BitmapImage[] image { get; set; }
            public string defenseVideo { get; set; } = null;
        };

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)PauseResume.Content == "Pause")
            {
                PauseResume.Content = "Resume";
                MainTimer.Stop();
                Debug.Write("stopped main timer, set to word resume ");
            }
            else
            {
                PauseResume.Content = "Pause";
                MainTimer = LoadMainTimer();
                Debug.Write("loaded main timer, set to word pause "); 
            }
        }
    }

}


