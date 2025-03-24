#nullable enable

using System.Collections.Generic;
using Godot;

namespace QuickCalc;

public partial class RootScene : Control
{
    [Export] 
    public Label ResultLabel { get; set; } = null!;

    private int _currentResult = 0;
    
    public override void _Ready()
    {
        var numpad = GetNode<Numpad>("VBoxContainer/Numpad");
        numpad.NumberButtonInput += AddToResult;
        numpad.ClearButtonInput += ClearCalculation;
        
        SetDisplayTo(_currentResult.ToString());
    }
    
    private void SetDisplayTo(string content)
    {
        ResultLabel.Text = content;
    }

    private void AddToResult(int value)
    {
        _currentResult += value;
        SetDisplayTo(_currentResult.ToString());
    }
    
    private void ClearCalculation()
    {
        _currentResult = 0;
        SetDisplayTo(_currentResult.ToString());
    }
}