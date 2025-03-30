#nullable enable

using System.Collections.Generic;
using Godot;

namespace QuickCalc;

public partial class RootScene : Control
{
    [Export] 
    public Label ResultLabel { get; set; } = null!;
    
    [Export]
    public RichTextLabel HistoryLabel { get; set; } = null!;

    private int _currentResult = 0;

    private readonly Stack<int> _history = [];
    
    public override void _Ready()
    {
        var numpad = GetNode<Numpad>("VBoxContainer/Numpad");
        numpad.NumberButtonInput += AddToResult;
        numpad.ClearButtonInput += ClearCalculation;
        numpad.UndoButtonInput += UndoLastChange;
        
        UpdateUi();
    }

    private void UpdateUi()
    {
        UpdateResultLabel(_currentResult.ToString());

        var historyContent = "";
        var historyArray = _history.ToArray();
        for (var i = historyArray.Length-1; i > -1; i--)
        {
            if (i == historyArray.Length - 1)
            {
                historyContent += historyArray[i].ToString();
            }
            else
            {
                historyContent += $"+{historyArray[i]}";
            }
        }
        UpdateHistoryLabel(historyContent);
    }
    
    private void UpdateResultLabel(string content)
    {
        ResultLabel.Text = content;
    }

    private void UpdateHistoryLabel(string content)
    {
        HistoryLabel.Text = $"[right]{content}[/right]";
    }

    private void AddToResult(int value)
    {
        if (value == 0)
            return;
        
        _currentResult += value;
        _history.Push(value);
        UpdateUi();
    }
    
    private void ClearCalculation()
    {
        _currentResult = 0;
        _history.Clear();
        UpdateUi();
    }

    private void UndoLastChange()
    {
        if (_history.Count < 1)
            return;
        
        var change = _history.Pop();
        _currentResult -= change;
        UpdateUi();
    }
}