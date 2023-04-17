using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Vector3D
{
    public double X { get; private set; }
    public double Y { get; private set; }
    public double Z { get; private set; }

    public Vector3D(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    [JsonConstructor]
    private Vector3D() { }

    // Updated methods to use properties instead of fields
    public Vector3D Add(Vector3D other)
        => new Vector3D(X + other.X, Y + other.Y, Z + other.Z);

    public Vector3D Subtract(Vector3D other)
        => new Vector3D(X - other.X, Y - other.Y, Z - other.Z);

    public double DotProduct(Vector3D other)
        => X * other.X + Y * other.Y + Z * other.Z;

    public double Magnitude()
        => Math.Sqrt(X * X + Y * Y + Z * Z);

    public double CosineAngle(Vector3D other)
        => DotProduct(other) / (Magnitude() * other.Magnitude());

    public override string ToString()
        => $"({X}, {Y}, {Z})";

    // Метод 1: Зберігає створений об’єкт класу з Завдання 1 у JSON файл
    public void SaveToJson(string filePath)
    {
        string jsonString = JsonSerializer.Serialize(this);
        File.WriteAllText(filePath, jsonString);
    }

    // Метод 2: Відкриває JSON файл з даними та створює об’єкт класу з цими даними для виконання Завдання 1
    public static Vector3D LoadFromJson(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Vector3D>(jsonString);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the coordinates for the vector:");
        Console.Write("X: ");
        double x = double.Parse(Console.ReadLine());
        Console.Write("Y: ");
        double y = double.Parse(Console.ReadLine());
        Console.Write("Z: ");
        double z = double.Parse(Console.ReadLine());

        Vector3D v1 = new Vector3D(x, y, z);
        Console.WriteLine("Original Vector: " + v1);

        string jsonFilePath = "vector.json";

        // Save the vector to a JSON file (Метод 1)
        v1.SaveToJson(jsonFilePath);

        // Load the vector from a JSON file and create an object (Метод 2)
        Vector3D v2 = Vector3D.LoadFromJson(jsonFilePath);
        Console.WriteLine("Loaded Vector: " + v2);
    }
}
