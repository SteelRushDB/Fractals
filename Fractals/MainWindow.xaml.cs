using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using ColorPickerWPF;

namespace Fractals;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    
    private double[] _scales = { 1, 2, 3, 5 }; // Возможные варианты масштаба
    private int _currentScaleIndex = 0;        // Индекс текущего масштаба
    
    
    private Point _startMousePos;     // Начальная позиция мыши
    private Point _startTranslatePos; // Начальная позиция трансляции

    private Color _startColor;
    private Color _endColor;
    public MainWindow()
    {
        InitializeComponent();

       // FractalCanvas.RenderTransform = st;
    }

    private void OnGenerateFractal(object sender, RoutedEventArgs e)
    {
        var selectedFractal = (FractalSelector.SelectedItem as ComboBoxItem)?.Content.ToString();
        FractalCanvas.Children.Clear();
        
        if (selectedFractal == "Дерево Пифагора")
        {
            int recursionCount = int.Parse(RecursionLevel_Pyth.Text);
            double rightAngle = double.Parse(RightBranchAngle.Text);
            double leftAngle = double.Parse(LeftBranchAngle.Text);
            double iterationCompression = double.Parse(IterationCompression.Text);
            
            FractalPythagoras fractal = new FractalPythagoras(FractalCanvas, recursionCount, iterationCompression, leftAngle, rightAngle, 
                _startColor, _endColor);
            fractal.DrawFractal();
        }
        else if (selectedFractal == "Кривая Коха")
        {
            int recursionCount = int.Parse(RecursionLevel_Koch.Text);
            
            FractalKoch fractal = new FractalKoch(FractalCanvas, recursionCount, _startColor, _endColor);
            fractal.DrawFractal();
        }
        else if (selectedFractal == "Ковер Серпинского")
        {
            int recursionCount = int.Parse(RecursionLevel_SerpinskyCarpet.Text);
            
            FractalSerpinskyCarpet fractal = new FractalSerpinskyCarpet(FractalCanvas, recursionCount, _startColor, _endColor);
            fractal.DrawFractal();
        }
        else if (selectedFractal == "Треугольник Серпинского")
        {
            int recursionCount = int.Parse(RecursionLevel_SerpinskyTriangle.Text);
            
            FractalSerpinskyTriangle fractal = new FractalSerpinskyTriangle(FractalCanvas, recursionCount, _startColor, _endColor);
            fractal.DrawFractal();
        }
        else if (selectedFractal == "Множество Кантора")
        {
            int recursionCount = int.Parse(RecursionLevel_Cantor.Text);
            double recursionDistance = int.Parse(RecursionDistance.Text);
            
            FractalCantor fractal = new FractalCantor(FractalCanvas, recursionCount, recursionDistance, _startColor, _endColor);
            fractal.DrawFractal();
        }
    }

    private void FractalSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedFractal = (FractalSelector.SelectedItem as ComboBoxItem)?.Content.ToString();

        PythagorasPanel.Visibility = selectedFractal == "Дерево Пифагора" ? Visibility.Visible : Visibility.Collapsed;
        KochPanel.Visibility = selectedFractal == "Кривая Коха" ? Visibility.Visible : Visibility.Collapsed;
        SerpinskyCarpetPanel.Visibility = selectedFractal == "Ковер Серпинского" ? Visibility.Visible : Visibility.Collapsed;
        SerpinskyTrianglePanel.Visibility = selectedFractal == "Треугольник Серпинского" ? Visibility.Visible : Visibility.Collapsed;
        CantorPanel.Visibility = selectedFractal == "Множество Кантора" ? Visibility.Visible : Visibility.Collapsed;
    }
    
    
    private void ApplyScale()
    {
        double scale = _scales[_currentScaleIndex];
        st.ScaleX = scale;
        st.ScaleY = scale;
    }
    private void ScalePlus_Click(object sender, RoutedEventArgs e)
    {
        // Увеличиваем индекс масштаба, если не достигли конца массива
        if (_currentScaleIndex < _scales.Length - 1)
        {
            _currentScaleIndex++;
        }

        ApplyScale(); // Применяем новый масштаб
    }
    private void ScaleMinus_OnClick(object sender, RoutedEventArgs e)
    {
        // Уменьшаем индекс масштаба, если не достигли начала массива
        if (_currentScaleIndex > 0)
        {
            _currentScaleIndex--;
        }

        ApplyScale(); // Применяем новый масштаб
    }
    
    
    private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        // Сохраняем начальные позиции мыши и текущей трансляции
        _startMousePos = e.GetPosition(GridMain);
        _startTranslatePos = new Point(translate.X, translate.Y);

        // Захватываем мышь
        FractalCanvas.CaptureMouse();
    }
    private void Canvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (FractalCanvas.IsMouseCaptured)
        {
            // Текущая позиция мыши
            Point currentMousePos = e.GetPosition(GridMain);

            // Вычисляем смещение мыши относительно начальной позиции
            double offsetX = currentMousePos.X - _startMousePos.X;
            double offsetY = currentMousePos.Y - _startMousePos.Y;

            // Применяем смещение к трансляции
            translate.X = _startTranslatePos.X + offsetX;
            translate.Y = _startTranslatePos.Y + offsetY;
        }
    }
    private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
    {
        // Освобождаем захват мыши
        FractalCanvas.ReleaseMouseCapture();
    }

    
    private void SaveFractalAs_Click(object sender, RoutedEventArgs e)
    {
        Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
        saveFileDialog.Filter = "PNG Image|*.png";
        saveFileDialog.Title = "Сохранение файла";

        if (saveFileDialog.ShowDialog() == true)
        {
            var rtb = new RenderTargetBitmap((int)FractalCanvas.ActualWidth, (int)FractalCanvas.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
            rtb.Render(FractalCanvas);
                    
            PngBitmapEncoder BufferSave = new PngBitmapEncoder();
            BufferSave.Frames.Add((BitmapFrame.Create(rtb)));
            using(var fs=System.IO.File.OpenWrite(saveFileDialog.FileName))
            { 
                BufferSave.Save(fs);
            }
        }
    }


    private void StartColor_OnClick(object sender, RoutedEventArgs e)
    {
        ColorPickerWindow.ShowDialog(out _startColor);
        
        StartColorButton.Background = backgroundBrushMaker(_startColor);
    }

    private void EndColor_OnClick(object sender, RoutedEventArgs e)
    {
        ColorPickerWindow.ShowDialog(out _endColor);
        
        EndColorButton.Background = backgroundBrushMaker(_endColor);
    }

    private Brush backgroundBrushMaker(Color color)
    {
        byte r = (byte)(color.R);
        byte g = (byte)(color.G);
        byte b = (byte)(color.B);
        return new SolidColorBrush(Color.FromRgb(r, g, b));
    }
}
