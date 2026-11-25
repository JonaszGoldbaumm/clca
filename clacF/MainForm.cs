using System.Globalization;

namespace clacF
{
    public partial class MainForm : Form
    {
        //private static readonly string[] operations0D = [
        //    "Add (+)",
        //    "Subtract (-)",
        //    "Multiply (•)",
        //    "Divide (/)",
        //    "MOD",
        //    "Modulo"
        //];

        private static readonly string[] dim = ["0D"];
        private readonly ZeroDimensionController _zeroController;

        public MainForm()
        {
            InitializeComponent();

            _zeroController = new ZeroDimensionController(
                grpOperations,
                grpInputsAndResult,
                txtFirstNumber,
                lblSecondNumber,
                txtSecondNumber,
                txtResult,
                btnCalculate);

            cmbDimensions.SelectedIndexChanged += cmbDimensions_SelectedIndexChanged;
            txtFirstNumber.TextChanged += InputTextBox_TextChanged;
            txtSecondNumber.TextChanged += InputTextBox_TextChanged;
            txtFirstNumber.KeyPress += NumericTextBox_KeyPress;
            txtSecondNumber.KeyPress += NumericTextBox_KeyPress;
            btnCalculate.Click += btnCalculate_Click;

            PopulateDimensionsSelector();
            _zeroController.Initialize();
        }

        private void PopulateDimensionsSelector()
        {
            cmbDimensions.Items.Clear();
            cmbDimensions.Items.AddRange(dim);

            if (cmbDimensions.Items.Count > 0)
            {
                cmbDimensions.SelectedIndex = 0;
            }
        }
        private void cmbDimensions_SelectedIndexChanged(object? sender, EventArgs e)
        {
            bool is0DSelected = string.Equals(
                cmbDimensions.SelectedItem?.ToString(),
                "0D",
                StringComparison.OrdinalIgnoreCase
                );
            _zeroController.SetActive(is0DSelected);
        }
        private void InputTextBox_TextChanged(object? sender, EventArgs e)
        {
            _zeroController.RefreshAvailability();
        }
        private void NumericTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if(sender is not TextBox textBox)
            {
                return;
            }
            if(char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                return;
            }
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if(e.KeyChar.ToString() == decimalSeparator || e.KeyChar == '.')
            {
                if(textBox.Text.Contains(decimalSeparator) || textBox.Text.Contains('.'))
                {
                    e.Handled = true;
                }
                return;
            }
            if(e.KeyChar == '-')
            {
                if(textBox.SelectionStart != 0 || textBox.Text.Contains('-'))
                {
                    e.Handled = true;
                }
                return;
            }
            e.Handled = true;
        }
        private void btnCalculate_Click(object? sender, EventArgs e)
        {
            if(!_zeroController.TryExecute(out string? error))
            {
                MessageBox.Show(
                    this,
                    error ?? "Unable to calculate",
                    "Calculation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
            }
        }
        private void newCalculationToolStripMenuItem_Click(object?  sender, EventArgs e)
        {
            if(cmbDimensions.Items.Count > 0)
            {
                cmbDimensions.SelectedIndex = 0;
            }
            _zeroController.Reset();
        }
        private void exitToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(
                this,
                "Simple calculator\nPowered by MathBase.dll",
                "About Clcaulator",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
        }

        /// <summary>
        /// Sealed class responsible for basic scalar operations
        /// </summary>
        private sealed class ZeroDimensionController
        {
            private enum OperationTag
            {
                Add, Sub, Mul, Div, Mod, Modulo, Sqrt
            }
            private sealed record Operation0D(string Name, OperationTag tag,  int InputCount=2);
            private static readonly IReadOnlyList<Operation0D> ops = [
                new("Add (+)", OperationTag.Add),
                new("Subtract (-)", OperationTag.Sub),
                new("Multiply (•)", OperationTag.Mul),
                new("Divide (/)", OperationTag.Div),
                new("MOD", OperationTag.Mod),
                new("Modulo", OperationTag.Modulo),
                new("Sqrt (√)", OperationTag.Sqrt, 1)
                ];

            private readonly GroupBox _operationsGroup;
            private readonly GroupBox _inputsGroup;
            private readonly TextBox _firstInput;
            private readonly Label _secondLabel;
            private readonly TextBox _secondInput;
            private readonly TextBox _result;
            private readonly Button _calculateBtn;

            private Operation0D? _selectedOp;
            private bool _isActive;

            public ZeroDimensionController(
                GroupBox operationsGroup,
                GroupBox inputsGroup,
                TextBox firstInput,
                Label secondLabel,
                TextBox secondInput,
                TextBox result,
                Button calculateBtn
                )
            {
                _operationsGroup = operationsGroup;
                _inputsGroup = inputsGroup;
                _firstInput = firstInput;
                _secondLabel = secondLabel;
                _secondInput = secondInput;
                _result = result;
                _calculateBtn = calculateBtn;
            }
            public void Initialize()
            {
                _operationsGroup.Text = "0D Operations";
                _operationsGroup.Controls.Clear();
                for (int i = 0; i < ops.Count; i++)
                {
                    Operation0D operation = ops[i];
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
                    _operationsGroup.Controls.Add(radioButton);
                }

                SetActive(true);
                UpdateInputSlotsForSelectedOperation();
                RefreshAvailability();
            }
            public void SetActive(bool active)
            {
                _isActive = active;
                _operationsGroup.Enabled = active;
                _inputsGroup.Enabled = active;

                if (!active)
                {
                    ClearSelection();
                }
                RefreshAvailability();
            }
            public void Reset()
            {
                SetActive(true);
                ClearSelection();
                _firstInput.Clear();
                _secondInput.Clear();
                _result.Clear();
                UpdateInputSlotsForSelectedOperation();
                RefreshAvailability();
            }
            public void RefreshAvailability()
            {
                if(!_isActive || _selectedOp is null)
                {
                    _calculateBtn.Enabled = false;
                    return;
                }
                bool hasValidInputs = _selectedOp.InputCount == 1
                    ? TryParseScalar(_firstInput.Text, out _)
                    : TryParseScalar(_firstInput.Text, out _) 
                    && TryParseScalar(_secondInput.Text, out _);
                _calculateBtn.Enabled = hasValidInputs;
            }
            public bool TryExecute(out string? error)
            {
                error = null;
                if(!_isActive || _selectedOp is null)
                {
                    error = "Select an operation first.";
                    return false;
                }
                if(!TryParseScalar(_firstInput.Text, out double firstVal))
                {
                    error = "Enter valid first number.";
                    return false;
                }
                double? secondVal = null;
                if(_selectedOp.InputCount == 2)
                {
                    if(!TryParseScalar(_secondInput.Text, out double parsedSecond))
                    {
                        error = "Enter valid second number.";
                        return false;
                    }
                    secondVal = parsedSecond;
                }

                double res;
                short exitcode = 0;
                res = Execute(firstVal, secondVal != null ? (double)secondVal : 0, out exitcode);
                if (exitcode != 0) {
                    error = $"An error has occurred: Operation exited with code {exitcode}.";
                    return false;
                }
                _result.Text = res.ToString(CultureInfo.CurrentCulture);
                return true;
            }
            private double Execute(double a, double b, out short exitcode)
            {
                switch (_selectedOp.tag)
                {
                    case OperationTag.Add: return library.ScalarCaller.Add(a, b, out exitcode);
                    case OperationTag.Sub: return library.ScalarCaller.Sub(a, b, out exitcode);
                    case OperationTag.Mul: return library.ScalarCaller.Mul(a, b, out exitcode);
                    case OperationTag.Div: return library.ScalarCaller.Div(a, b, out exitcode);
                    case OperationTag.Mod: return library.ScalarCaller.Mod(a, b, out exitcode);
                    case OperationTag.Modulo: return library.ScalarCaller.Modulo(a, b, out exitcode);
                    case OperationTag.Sqrt: return library.ScalarCaller.Sqrt(a, out exitcode);
                    default:
                        exitcode = -2;
                        return -1;
                }
            }
            private void OnOperationCheckedChanged(object? sender, EventArgs e)
            {
                if(sender is RadioButton { Checked: true, Tag: Operation0D operation })
                {
                    _selectedOp = operation;
                    UpdateInputSlotsForSelectedOperation();
                    RefreshAvailability();
                }
            }
            private void UpdateInputSlotsForSelectedOperation() {
                bool needsSecondInput = _selectedOp?.InputCount == 2;
                _secondInput.Visible = needsSecondInput;
                _secondLabel.Visible = needsSecondInput;
                if (!needsSecondInput)
                {
                    _secondInput.Clear();
                }
            }
            private void ClearSelection()
            {
                _selectedOp = null;
                foreach (RadioButton rdo in _operationsGroup.Controls.OfType<RadioButton>())
                {
                    rdo.Checked = false;
                }
                UpdateInputSlotsForSelectedOperation();
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
        }
    }
}
