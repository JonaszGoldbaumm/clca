using clacF.controllers;
using System.Globalization;

namespace clacF
{
    public partial class MainForm : Form
    {

        private static readonly string[] dim = ["0D"];
        private readonly Dictionary<string, controllers.IDimensionController> _controllers = new(StringComparer.OrdinalIgnoreCase);
        private IDimensionController _activeController;
        public MainForm()
        {
            InitializeComponent();

            var zeroDimension = new controllers.ZeroDimensionController(
                pnlOperationsList,
                grpOperations,
                grpInputsAndResult,
                txtFirstNumber,
                lblSecondNumber,
                txtSecondNumber,
                txtResult,
                btnCalculate);
            _controllers[zeroDimension.DimensionName] = zeroDimension;
            
            foreach(IDimensionController controller in _controllers.Values)

            cmbDimensions.SelectedIndexChanged += cmbDimensions_SelectedIndexChanged;
            txtFirstNumber.TextChanged += InputTextBox_TextChanged;
            txtSecondNumber.TextChanged += InputTextBox_TextChanged;
            txtFirstNumber.KeyPress += NumericTextBox_KeyPress;
            txtSecondNumber.KeyPress += NumericTextBox_KeyPress;
            btnCalculate.Click += btnCalculate_Click;
            AcceptButton = btnCalculate;

            PopulateDimensionsSelector();
            zeroDimension.Initialize();
        }

        private void PopulateDimensionsSelector()
        {
            cmbDimensions.Items.Clear();
            foreach(string dim in _controllers.Keys)
            {
                cmbDimensions.Items.Add(dim);
            }
            if(cmbDimensions.Items.Count > 0)
            {
                cmbDimensions.SelectedIndex = 0;
            }
        }
        private void cmbDimensions_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string? selectedDim = cmbDimensions.SelectedItem?.ToString();
            ActivateDimension(selectedDim);
        }
        private void ActivateDimension(string? dim)
        {
            _controllers.TryGetValue(dim ?? string.Empty, out IDimensionController? nextC);
            foreach(IDimensionController controller in _controllers.Values)
            {
                bool isTarget = ReferenceEquals(controller, nextC);
                controller.SetActive(isTarget);
            }
            _activeController = nextC;
            bool isHigherDim = _activeController is not null &&
                !string.Equals(_activeController.DimensionName, "0D", StringComparison.OrdinalIgnoreCase);
            pnlHigherDimensions.Visible = isHigherDim;
            ApplyDimensionLayout(_activeController?.DimensionName);
        }
        private void ApplyDimensionLayout(string? dim)
        {
            switch (dim)
            {
                case "0D":
                    ClientSize = new System.Drawing.Size(534, 543);
                    break;
                default:
                    ClientSize = new Size(534, 543);
                    break;
            }
        }
        private void InputTextBox_TextChanged(object? sender, EventArgs e)
        {
            _activeController.RefreshAvailability();
        }
        private void NumericTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (sender is not TextBox textBox)
            {
                return;
            }
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                return;
            }
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (e.KeyChar.ToString() == decimalSeparator || e.KeyChar == '.')
            {
                if (textBox.Text.Contains(decimalSeparator) || textBox.Text.Contains('.'))
                {
                    e.Handled = true;
                }
                return;
            }
            if (e.KeyChar == '-')
            {
                if (textBox.SelectionStart != 0 || textBox.Text.Contains('-'))
                {
                    e.Handled = true;
                }
                return;
            }
            e.Handled = true;
        }
        private void btnCalculate_Click(object? sender, EventArgs e)
        {
            if (!_activeController.TryExecute(out string? error))
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
        private void newCalculationToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (cmbDimensions.Items.Count > 0)
            {
                cmbDimensions.SelectedIndex = 0;
            }
            _activeController.Reset();
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
    }
}
