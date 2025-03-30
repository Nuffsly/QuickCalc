using Godot;

namespace QuickCalc;

public partial class Numpad : GridContainer
{
    private int? _mouseOverValue = null;
    private string _currentValue = "";
    
    [Signal]
    public delegate void NumberButtonInputEventHandler(int value);
    
    [Signal]
    public delegate void ClearButtonInputEventHandler();
    
    public override void _Ready()
    {
        foreach (var child in GetChildren())
        {
            if (child is EventButton eventButton)
            {
                eventButton.MouseEnteredValueButton += MouseEnteredValueButton;
                eventButton.MouseExitedValueButton += MouseExitedValueButton;
                eventButton.ValueButtonsInput += MouseClickedValueButton;
            }
            if (child is Button button && button.Name == "ClearButton")
                button.Pressed += EmitClearButtonInputEvent;
        }
    }

    private void MouseClickedValueButton(int value)
    {
        _mouseOverValue = value;
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is not InputEventMouseButton mouseEvent) return;

        if (mouseEvent.Pressed)
        {
            // When pressed, add whatever number is currently moused over.
            _currentValue += _mouseOverValue?.ToString() ?? "";
            // Set to null so user has to enter a new number before releasing to get double-digit mode.
            _mouseOverValue = null;
        }
        else
        {
            if (_mouseOverValue != null)
                _currentValue += _mouseOverValue.ToString();
            
            _ = int.TryParse(_currentValue, out var parsedValue);
            EmitNumberButtonInputEvent(parsedValue);
        }
    }
    
    private void MouseEnteredValueButton(int value)
    {
        _mouseOverValue = value;
    }

    private void MouseExitedValueButton(int value)
    {
        if (_mouseOverValue == value)
            _mouseOverValue = null;
    }
    
    private void EmitNumberButtonInputEvent(int value)
    {
        EmitSignal(SignalName.NumberButtonInput, value);
        _currentValue = "";
    }

    private void EmitClearButtonInputEvent()
    {
        EmitSignal(SignalName.ClearButtonInput);
    }
}