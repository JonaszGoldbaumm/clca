using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace clacF.controllers
{
    internal class OneDimensionController : ADimensionController<Operation1DTag>
    {
        private static new readonly IReadOnlyList<OperationR> ops = [
            new("Add to first value (+)", Operation1DTag.Add_nobase, 1),
                new("Subtract from first value (-)", Operation1DTag.Sub_nobase, 1),
                new("Multiply the first value (•)", Operation1DTag.Mul_nobase, 1),
                new("Divide the first value (/)", Operation1DTag.Div_nobase, 1)
            ];

        public OneDimensionController(Panel pnlOperationsList, GroupBox grpOperations, GroupBox inputsGroup, Label firstLabel, TextBox firstInput, Label secondLabel, TextBox secondInput, TextBox result, Button calculateBtn) : base(pnlOperationsList, grpOperations, inputsGroup, firstLabel, firstInput, secondLabel, secondInput, result, calculateBtn)
        {
        }

        public override string DimensionName => "1D Array";
        public override void Initialize()
        {
            _grpOperations.Text = "1D Array Operations";
            _firstLabel.Text = "Array: ';' sep.";
            _secondLabel.Text = "Scalar number";
            _pnlOperationsList.Controls.Clear();
            for (int i = 0; i < ops.Count; i++)
            {
                OperationR op = ops[i];
                var rdo = new RadioButton
                {
                    Name = $"rdo1DOp{i}",
                    Text = op.Name,
                    Tag = op,
                    Left = 15,
                    Top = 28 + (i * 28),
                    AutoSize = true
                };
                rdo.CheckedChanged += OnOperationCheckedChanged;
                _pnlOperationsList.Controls.Add(rdo);
            }
            SetActive(true);
            UpdateInputSlotsForSelectedOperation();
            RefreshAvailability();
        }
        public override void RefreshAvailability()
        {
            if(!IsActive || SelectedOperation is null)
            {
                _firstInput.Enabled = false;
                _calculateBtn.Enabled = false;
                return;
            }
            if(SelectedOperation is not null)
            {
                _firstInput.Enabled = true;
            }
            bool hasValidInputs = SelectedOperation!.InputCount == 1
                ? !_firstInput.Text.IsWhiteSpace()
                : !_firstInput.Text.IsWhiteSpace()
                && !_secondInput.Text.IsWhiteSpace();
            _calculateBtn.Enabled = hasValidInputs;
        }
        public override bool TryExecute(out string? error)
        {
            error = null;
            if (!IsActive || SelectedOperation is null)
            {
                error = "Select an operation first.";
                return false;
            }
            if(!TryParseArray(_firstInput.Text, out double[]? arr) || arr is null)
            {
                error = "Array not in valid format.";
                return false;
            }
            double? secondVal = null;
            if(SelectedOperation.InputCount == 2)
            {
                if(!TryParseScalar(_secondInput.Text, out double parsedSecond))
                {
                    error = "Enter valid scalar.";
                    return false;
                }
                secondVal = parsedSecond;
            }
            double[] res;
            short exitcode = -9;
            res = Execute(arr, secondVal != null ? (double)secondVal : 0.0, out exitcode);
            if(exitcode != 0)
            {
                error = $"An error has occurred: Operation exited with code {exitcode}.";
                return false;
            }
            _result.Text = "{" + string.Join("; ", res.Select(x => x.ToString(CultureInfo.InvariantCulture))) + "}";
            return true;
        }
        private double[] Execute(double[] arr, double b, out short exitcode)
        {
            if(SelectedOperation is not null) switch (SelectedOperation.Tag)
                {
                    case Operation1DTag.Add_nobase: return [library.Array1DCaller.SumNoBase(arr, arr.Length, out exitcode)] ;
                    case Operation1DTag.Sub_nobase: return [library.Array1DCaller.SubNoBase(arr, arr.Length, out exitcode)];
                    case Operation1DTag.Mul_nobase: return [library.Array1DCaller.MulNoBase(arr, arr.Length, out exitcode)];
                    case Operation1DTag.Div_nobase: return [library.Array1DCaller.DivNoBase(arr, arr.Length, out exitcode)];
                    default:
                        exitcode = -2;
                        return [-1.0];
                }
            else
            {
                exitcode = -2;
                return [-1.0]; 
            }
        }
        private void OnOperationCheckedChanged(object? sender, EventArgs e)
        {
            if(sender is RadioButton { Checked: true, Tag: OperationR operation })
            {
                SelectedOperation = operation;
                UpdateInputSlotsForSelectedOperation();
                RefreshAvailability();
            }
        }
        public override void UpdateInputSlotsForSelectedOperation()
        {
            bool needsScalarInput = SelectedOperation?.InputCount == 2;
            _secondInput.Visible = needsScalarInput;
            _secondLabel.Visible = needsScalarInput;
            if (!needsScalarInput)
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
        private static bool TryParseArray(string? rawText,out double[]? arr)
        {
            arr= null;
            if (string.IsNullOrWhiteSpace(rawText))
            {
                return false;
            }

            string trimmed = rawText.Trim();

            if (trimmed.StartsWith('[') || trimmed.EndsWith(']'))
            {
                if (!(trimmed.StartsWith('[') && trimmed.EndsWith(']')))
                {
                    return false;
                }
                trimmed = trimmed[1..^1];
            }
            else if (trimmed.StartsWith('{') || trimmed.EndsWith('}'))
            {
                if (!(trimmed.StartsWith('{') && trimmed.EndsWith('}')))
                {
                    return false;
                }
                trimmed = trimmed[1..^1];
            }

            string normalized = trimmed.Replace(',', '.');
            string[] parts = normalized.Split(';', StringSplitOptions.TrimEntries);

            if (parts.Length == 0 || (parts.Length == 1 && parts[0].Length == 0))
            {
                return false;
            }

            arr = new double[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                arr[i] = double.Parse(parts[i], NumberStyles.Float, CultureInfo.InvariantCulture);
            }
            
            return true;
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
                    Name = $"rdo1DOp{i}",
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
                _grpOperations.Text = "1D Array Operations";
                PopulateOperationsList();
                UpdateInputSlotsForSelectedOperation();
                RefreshAvailability();
            }
        }

        protected override void OnFirstInputKeyPress(object? sender, KeyPressEventArgs e)
            => FilterSimpleArrayKeyPress(sender, e);
    }

}
