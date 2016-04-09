using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Bounce.Core;

namespace BounceWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _timer;
        private CanvasDrawingArea _canvasDrawingArea;
        private World _world;
        private readonly BrushConverter _brushConverter;

        public MainWindow()
        {
            this._brushConverter = new BrushConverter();
            this._timer = new DispatcherTimer(DispatcherPriority.Render, this.Dispatcher)
            {
                Interval = TimeSpan.FromMilliseconds(10),
                IsEnabled = true
            };
            this._timer.Tick += _timer_Tick;
            InitializeComponent();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            this._world.OnTick();
        }

        private void CanvasMain_OnLoaded(object sender, RoutedEventArgs e)
        {
            // Minimal radius
            this.IntegerUpDownRadius.Minimum = 5;
            // Maximal radius
            this.IntegerUpDownRadius.Maximum = (int?)(this.CanvasMain.ActualWidth / 4);

            this._canvasDrawingArea = new CanvasDrawingArea(this.CanvasMain);
            this._world = new World(this._canvasDrawingArea);
            this._world.CollectionChangedHandler += CollectionChangedHandler;
            this._timer.Start();
        }

        private void CollectionChangedHandler(object sender,
                                              NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            foreach (BallWpf changedObject in notifyCollectionChangedEventArgs.NewItems.Cast<BallWpf>())
            {
                switch (notifyCollectionChangedEventArgs.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        this.CanvasMain.Children.Add(changedObject.WpfShape);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        this.CanvasMain.Children.Remove(changedObject.WpfShape);
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        break;
                    case NotifyCollectionChangedAction.Move:
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void ButtonAddBall_Click(object sender, RoutedEventArgs e)
        {
            Color? color = this.ColorPickerColor.SelectedColor;
            if (color == null)
            {
                MessageBox.Show("Please select color.");
                return;
            }
            int? radius = this.IntegerUpDownRadius.Value;
            if (radius != null)
            {
                PointD position = this._world.GetUnoccupiedPosition((double) radius);
                Tuple<double, double> speed = this._world.GetRandomSpeed(-1, 1);

                this._world.AddObject(new BallWpf(position, speed.Item1, speed.Item2, (double) radius,
                    GetBrushFromColor((Color) color)));
            }
            else
            {
                MessageBox.Show("Pelase enter radius.");
            }
        }


        private Brush GetBrushFromColor(Color color)
        {
            return (Brush) this._brushConverter.ConvertFromString(color.ToString());
        }
    }
}