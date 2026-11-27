using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace clacF.controllers
{
    internal sealed class ZeroDimensionController : ADimensionController<Operation0DTag>
    {
        private static new readonly IReadOnlyList<OperationR> ops = [
            new("Add (+)", Operation0DTag.Add),
                new("Subtract (-)", Operation0DTag.Sub),
                new("Multiply (•)", Operation0DTag.Mul),
                new("Divide (/)", Operation0DTag.Div),
                new("MOD", Operation0DTag.Mod),
                new("Modulo",   Operation0DTag.Modulo),
                new("Sqrt (√)", Operation0DTag.Sqrt, 1)
            ];

        public ZeroDimensionController(Panel pnlOperationsList, GroupBox grpOperations, GroupBox inputsGroup, Label firstLabel, TextBox firstInput, Label secondLabel, TextBox secondInput, TextBox result, Button calculateBtn) : base(pnlOperationsList, grpOperations, inputsGroup, firstLabel, firstInput, secondLabel, secondInput, result, calculateBtn)
        {
        }

        override public string DimensionName => "0D";

        override public void Initialize()
        {
            _grpOperations.Text = "0D Operations";
            _firstLabel.Text = "1st Number";
            _secondLabel.Text = "2nd Number";
            _pnlOperationsList.Controls.Clear();
            for (int i = 0; i < ops.Count; i++)
            {
                OperationR operation = ops[i];
                var radioButton = new RadioButton
                {
                    Name = $"rdo0DOp{i}",
                    Text = operation.Name,
                    Tag = operation,
                    Left = 15,
                    Top = 28 + (i * 28),
                    AutoSize = true
                };

                radioButton.CheckedChanged += OnOperationCheckedChanged;
                _pnlOperationsList.Controls.Add(radioButton);
            }
            SetActive(true);
            UpdateInputSlotsForSelectedOperation();
            RefreshAvailability();
        }
        override public void RefreshAvailability()
        {
            if (!IsActive || SelectedOperation is null)
            {
                _firstInput.Enabled = false;
                _calculateBtn.Enabled = false;
                return;
            }
            if (SelectedOperation is not null)
            {
                _firstInput.Enabled = true;
            }
            bool hasValidInputs = SelectedOperation!.InputCount == 1
                ? TryParseScalar(_firstInput.Text, out _)
                : TryParseScalar(_firstInput.Text, out _)
                && TryParseScalar(_secondInput.Text, out _);
            _calculateBtn.Enabled = hasValidInputs;
        }
        override public bool TryExecute(out string? error)
        {
            error = null;
            if (!IsActive || SelectedOperation is null)
            {
                error = "Select an operation first.";
                return false;
            }
            if (!TryParseScalar(_firstInput.Text, out double firstVal))
            {
                error = "Enter valid first number.";
                return false;
            }
            double? secondVal = null;
            if (SelectedOperation.InputCount == 2)
            {
                if (!TryParseScalar(_secondInput.Text, out double parsedSecond))
                {
                    error = "Enter valid second number.";
                    return false;
                }
                secondVal = parsedSecond;
            }

            double res;
            short exitcode = -9;
            res = Execute(firstVal, secondVal != null ? (double)secondVal : 0, out exitcode);
            if (exitcode != 0)
            {
                error = $"An error has occurred: Operation exited with code {exitcode}.";
                return false;
            }
            _result.Text = res.ToString(CultureInfo.CurrentCulture);
            return true;
        }
        private double Execute(double a, double b, out short exitcode)
        {
            if (SelectedOperation is not null) switch (SelectedOperation.Tag)
                {
                    case Operation0DTag.Add: return library.ScalarCaller.Add(a, b, out exitcode);
                    case Operation0DTag.Sub: return library.ScalarCaller.Sub(a, b, out exitcode);
                    case Operation0DTag.Mul: return library.ScalarCaller.Mul(a, b, out exitcode);
                    case Operation0DTag.Div: return library.ScalarCaller.Div(a, b, out exitcode);
                    case Operation0DTag.Mod: return library.ScalarCaller.Mod(a, b, out exitcode);
                    case Operation0DTag.Modulo: return library.ScalarCaller.Modulo(a, b, out exitcode);
                    case Operation0DTag.Sqrt: return library.ScalarCaller.Sqrt(a, out exitcode);
                    default:
                        exitcode = -2;
                        return -1;
                }
            else
            {
                exitcode = -2;
                return -1;
            }
        }
        private void OnOperationCheckedChanged(object? sender, EventArgs e)
        {
            if (sender is RadioButton { Checked: true, Tag: OperationR operation })
            {
                SelectedOperation = operation;
                UpdateInputSlotsForSelectedOperation();
                RefreshAvailability();
            }
        }
        override public void UpdateInputSlotsForSelectedOperation()
        {
            bool needsSecondInput = SelectedOperation?.InputCount == 2;
            _secondInput.Visible = needsSecondInput;
            _secondLabel.Visible = needsSecondInput;
            if (!needsSecondInput)
            {
                _secondInput.Clear();
            }
        }
        private static bool TryParseScalar(string? rawText, out double val)
        {
            val = 0;
            if (string.IsNullOrWhiteSpace(rawText))
            {
                return false;
            }
            string trim = rawText.Trim();
            bool parsed = double.TryParse(trim, NumberStyles.Float, CultureInfo.CurrentCulture, out val) ||
                double.TryParse(trim, NumberStyles.Float, CultureInfo.InvariantCulture, out val);
            return parsed && !double.IsNaN(val) && !double.IsInfinity(val);
        }
        override protected void PopulateOperationsList()
        {
            SelectedOperation = null;
            _pnlOperationsList.Controls.Clear();

            for (int i = 0; i < ops.Count; i++)
            {
                OperationR operation = ops[i];
                var radioButton = new RadioButton
                {
                    Name = $"rdo0DOp{i}",
                    Text = operation.Name,
                    Tag = operation,
                    Left = 15,
                    Top = 6 + (i * 28),
                    AutoSize = true
                };

                radioButton.CheckedChanged += OnOperationCheckedChanged;
                _pnlOperationsList.Controls.Add(radioButton);
            }
        }
        public override void SetActive(bool active)
        {
            base.SetActive(active);
            if (active)
            {
                _grpOperations.Text = "0D Operations";
                PopulateOperationsList();
                UpdateInputSlotsForSelectedOperation();
                RefreshAvailability();
            }
        }

        protected override void OnFirstInputKeyPress(object? sender, KeyPressEventArgs e)
            => FilterSingleNumberKeyPress(sender, e);

    }
}
