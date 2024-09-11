using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractals;

public class FractalBase
{
    protected Canvas _canvas;         
    protected double _recursionLevel;
    protected Color _startColor;
    protected Color _endColor;
    
    public FractalBase(Canvas canvas, double recursionLevel, Color startColor, Color endColor)
    {
        _canvas = canvas;
        _recursionLevel = recursionLevel;
        _startColor = startColor;
        _endColor = endColor;
    }

    protected Brush GetGradientBrush(double iteration)
    {
        double factor = iteration / _recursionLevel;

        // Линейная интерполяция по каждому компоненту цвета
        byte r = (byte)(_startColor.R + (_endColor.R - _startColor.R) * factor);
        byte g = (byte)(_startColor.G + (_endColor.G - _startColor.G) * factor);
        byte b = (byte)(_startColor.B + (_endColor.B - _startColor.B) * factor);

        return new SolidColorBrush(Color.FromRgb(r, g, b));
    }
    
    public virtual void DrawFractal()
    {
        
    }
    
    // Линия
    public void DrawSimpleLine(double x1, double x2, double y1, double y2, double thinkess, double recursionLevel)
    {
        Line oneLine = new Line
        {
            X1 = x1,
            Y1 = y1,
            X2 = x2,
            Y2 = y2,
            Stroke = GetGradientBrush(recursionLevel),
            StrokeThickness = thinkess
        };
        
        _canvas.Children.Add(oneLine);
    }
    
    // Квадрат
    public void DrawFilledSquare(double x, double y, double sideLength, double recursionLevel)
    {
        // Создаем объект Rectangle (прямоугольник)
        Rectangle square = new Rectangle
        {
            Width = sideLength,
            Height = sideLength,
            Fill = GetGradientBrush(recursionLevel)      // Задаем цвет заливки
        };

        // Устанавливаем координаты квадрата на Canvas
        Canvas.SetLeft(square, x);
        Canvas.SetTop(square, y);

        // Добавляем квадрат на Canvas
        _canvas.Children.Add(square);
    }
    
    // Треугольник
    public void DrawEmptyTriangle(double x1, double y1, double x2, double y2, double x3, double y3, double recursionLevel)
    {
        
        // Создаем объект Polygon
        Polygon triangle = new Polygon
        {
            Stroke = GetGradientBrush(recursionLevel),    // Задаем цвет границы
            StrokeThickness = 2,       // Толщина границы
            Fill = Brushes.Transparent // Задаем прозрачную заливку
        };

        // Задаем вершины треугольника
        PointCollection points = new PointCollection
        {
            new Point(x1, y1), // Первая вершина
            new Point(x2, y2), // Вторая вершина
            new Point(x3, y3)  // Третья вершина
        };
    
        triangle.Points = points;

        // Добавляем треугольник на Canvas
        _canvas.Children.Insert(0, triangle);
    }
    
    
    
    
}