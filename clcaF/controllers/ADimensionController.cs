using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace clacF.controllers
{
    internal enum Operation0DTag
    {
        Add, Sub, Mul, Div, Mod, Modulo, Sqrt
    }
    internal enum Operation1DTag 
    {
        Add_nobase, Sub_nobase, Mul_nobase, Div_nobase, Abs, Antiabs, Min, Max, SqrtOfSum, NthrtOfSum, Floor, Ceiling
    }

    internal abstract class ADimensionController
    {
              
        protected readonly Panel _pnlOperationsList;
        protected readonly GroupBox _grpOperations;
        protected readonly GroupBox _inputsGroup;
        protected readonly Label _firstLabel;
        protected readonly TextBox _firstInput;
        protected readonly Label _secondLabel;
        protected readonly TextBox _secondInput;
        protected readonly TextBox _result;
        protected readonly Button _calculateBtn;
        public bool IsActive { get; protected set; }
        public abstract string DimensionName { get; }
        protected ADimensionController(Panel pnlOperationsList, GroupBox grpOperations, GroupBox inputsGroup, Label firstLabel, TextBox firstInput, Label secondLabel, TextBox secondInput, TextBox result, Button calculateBtn)
        {
            _pnlOperationsList = pnlOperationsList;
            _grpOperations = grpOperations;
            _inputsGroup = inputsGroup;
            _firstLabel = firstLabel;
            _firstInput = firstInput;
            _secondLabel = secondLabel;
            _secondInput = secondInput;
            _result = result;
            _calculateBtn = calculateBtn;
        }

        public abstract void SetActive(bool active);

        public abstract void Reset();
        public abstract void Initialize();
        protected abstract void ClearSelection();
        public abstract void RefreshAvailability();
        public abstract void UpdateInputSlotsForSelectedOperation();
        protected abstract void PopulateOperationsList();
        public abstract bool TryExecute(out string? error);
        protected static void FilterSingleNumberKeyPress(object? sender, KeyPressEventArgs e)
        {
            if (sender is not TextBox textBox)
            {
                return;
            }

            char ch = e.KeyChar;

            if (char.IsControl(ch) || char.IsDigit(ch))
            {
                return;
            }

            if (ch == '.')
            {
                bool selectionHasDot = !string.IsNullOrEmpty(textBox.SelectedText) &&
                                       textBox.SelectedText.Contains('.');

                if (textBox.Text.Contains('.') && !selectionHasDot)
                {
                    e.Handled = true;
                }
                return;
            }

            if (ch == '-')
            {
                bool selectionHasMinus = !string.IsNullOrEmpty(textBox.SelectedText) &&
                                         textBox.SelectedText.Contains('-');
                bool atStart = textBox.SelectionStart == 0;

                if (!atStart || (textBox.Text.Contains('-') && !selectionHasMinus))
                {
                    e.Handled = true;
                }
                return;
            }

            e.Handled = true;
        }
        protected static void FilterSimpleArrayKeyPress(object? sender, KeyPressEventArgs e)
        {
            if (sender is not TextBox textBox)
            {
                return;
            }

            char ch = e.KeyChar;

            if (char.IsControl(ch) || char.IsDigit(ch))
            {
                return;
            }

            if (ch is ';' or ' ')
            {
                return;
            }

            if (ch == '.')
            {
                if (HasDuplicateDotInCurrentElement(textBox))
                {
                    e.Handled = true;
                }
                return;
            }

            if (ch == '-')
            {
                if (!CanPlaceMinusInCurrentElement(textBox))
                {
                    e.Handled = true;
                }
                return;
            }

            e.Handled = true;
        }
        private static bool HasDuplicateDotInCurrentElement(TextBox textBox)
        {
            if (!string.IsNullOrEmpty(textBox.SelectedText) && textBox.SelectedText.Contains('.'))
            {
                return false;
            }

            int caret = Math.Clamp(textBox.SelectionStart, 0, textBox.Text.Length);
            string text = textBox.Text;

            int start = text.LastIndexOf(';', Math.Max(0, caret - 1));
            start = start < 0 ? 0 : start + 1;

            int end = text.IndexOf(';', caret);
            end = end < 0 ? text.Length : end;

            if (end <= start)
            {
                return false;
            }

            return text[start..end].Contains('.');
        }

        private static bool CanPlaceMinusInCurrentElement(TextBox textBox)
        {
            if (!string.IsNullOrEmpty(textBox.SelectedText) && textBox.SelectedText.Contains('-'))
            {
                return true;
            }

            int caret = Math.Clamp(textBox.SelectionStart, 0, textBox.Text.Length);
            string text = textBox.Text;

            int start = text.LastIndexOf(';', Math.Max(0, caret - 1));
            start = start < 0 ? 0 : start + 1;

            string prefix = text[start..caret];
            return prefix.Trim().Length == 0 && !prefix.Contains('-');
        }
    }

    internal abstract class ADimensionController<TOperationTag>(
        Panel pnlOperationsList,
        GroupBox grpOperations,
        GroupBox inputsGroup,
        Label firstLabel,
        TextBox firstInput,
        Label secondLabel,
        TextBox secondInput,
        TextBox result,
        Button calculateBtn) : ADimensionController(
        pnlOperationsList,
        grpOperations,
        inputsGroup,
        firstLabel,
        firstInput,
        secondLabel,
        secondInput,
        result,
        calculateBtn) where TOperationTag : struct, Enum {

        protected sealed record OperationR(string Name, TOperationTag Tag, int InputCount = 2);
        protected static readonly IReadOnlyList<OperationR> ops = [];
        protected OperationR? SelectedOperation { get; set; }

        override public void SetActive(bool active)
        {
            IsActive = active;
            _grpOperations.Enabled = active;
            _pnlOperationsList.Enabled = active;
            _inputsGroup.Enabled = active;
            if (active)
            {
                AttachInputHandlers();
            }
            else
            {
                DetachInputHandlers();
                ClearSelection();
            }

            RefreshAvailability();
        }
        override public void Reset()
        {
            SetActive(true);
            ClearSelection();
            _firstInput.Clear();
            _secondInput.Clear();
            _result.Clear();
            RefreshAvailability();
        }
        override protected void ClearSelection()
        {
            SelectedOperation = null;
            foreach (RadioButton rdo in _pnlOperationsList.Controls.OfType<RadioButton>())
            {
                rdo.Checked = false;
            }
            UpdateInputSlotsForSelectedOperation();
        }
        protected virtual void AttachInputHandlers()
        {
            DetachInputHandlers();
            _firstInput.KeyPress += OnFirstInputKeyPress;
            _secondInput.KeyPress += OnSecondInputKeyPress;
            _firstInput.TextChanged += OnInputTextChanged;
            _secondInput.TextChanged += OnInputTextChanged;
        }

        protected virtual void DetachInputHandlers()
        {
            _firstInput.KeyPress -= OnFirstInputKeyPress;
            _secondInput.KeyPress -= OnSecondInputKeyPress;
            _firstInput.TextChanged -= OnInputTextChanged;
            _secondInput.TextChanged -= OnInputTextChanged;
        }

        protected abstract void OnFirstInputKeyPress(object? sender, KeyPressEventArgs e);

        protected virtual void OnSecondInputKeyPress(object? sender, KeyPressEventArgs e)
        {
            FilterSingleNumberKeyPress(sender, e);
        }

        private void OnInputTextChanged(object? sender, EventArgs e)
        {
            if (IsActive)
            {
                RefreshAvailability();
            }
        }
    }
}
