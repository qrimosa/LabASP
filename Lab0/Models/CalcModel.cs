namespace Lab0.Models;

public class CalcModel
{
    public double? x { get; set; }
    public double? y { get; set; }
    public Operators Operator { get; set; }

    public bool isValid()
    {
        return x is not null && y is not null && Operator != Operators.Undefined;
    }
    
    public string Result
    {
        get
        {
            switch (Operator)
            {
                case Operators.Add:
                    return $"{x} + {y} = {x + y}";
                case Operators.Sub:
                    return $"{x} - {y} = {x - y}";
                case Operators.Mul:
                    return $"{x} * {y} = {x * y}";
                case Operators.Div:
                    if (y == 0)
                        return "Division by zero is not allowed.";
                    return $"{x} / {y} = {x / y}";
                default:
                    return "Invalid operator.";
            }
        }
    }
}