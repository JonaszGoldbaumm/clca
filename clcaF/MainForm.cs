using clacF.controllers;
using library;
using System.Globalization;
using System.Runtime.InteropServices;

namespace clacF
{
    public partial class MainForm : Form
    {
        private readonly Dictionary<string, controllers.ADimensionController> _controllers = new(StringComparer.OrdinalIgnoreCase);
        private ADimensionController _activeController;
        public MainForm()
        {
            InitializeComponent();

            var zeroDimension = new controllers.ZeroDimensionController(
                pnlOperationsList,
                grpOperations,
                grpInputsAndResult,
                lblFirstNumber,
                txtFirstNumber,
                lblSecondNumber,
                txtSecondNumber,
                txtResult,
                btnCalculate);
            _controllers[zeroDimension.DimensionName] = zeroDimension;
            var array1DDimension = new OneDimensionController(
                pnlOperationsList,
                grpOperations,
                grpInputsAndResult,
                lblFirstNumber,
                txtFirstNumber,
                lblSecondNumber,
                txtSecondNumber,
                txtResult,
                btnCalculate
                );
            _controllers[array1DDimension.DimensionName] = array1DDimension;
            
            cmbDimensions.SelectedIndexChanged += cmbDimensions_SelectedIndexChanged;
            btnCalculate.Click += btnCalculate_Click;
            AcceptButton = btnCalculate;

            foreach(ADimensionController controller in _controllers.Values)
            {
                controller.Initialize();
            }
            PopulateDimensionsSelector();
            _activeController = _controllers[zeroDimension.DimensionName];
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
            _controllers.TryGetValue(dim ?? String.Empty, out var next);
            foreach(var controller in _controllers.Values)
            {
                if(!ReferenceEquals(controller, next))
                {
                    controller.SetActive(false);
                }
            }
            _activeController = next;
            if(_activeController is not null)
            {
                _activeController.SetActive(true);
                txtFirstNumber.Clear();
                txtSecondNumber.Clear();
                txtResult.Clear();
                _activeController.RefreshAvailability();
            }
            //possible 1+D stuff for file aggregation with the panel
            //pnlHigherDimensions<-
            ApplyDimensionLayout(_activeController?.DimensionName);
        }
        private void ApplyDimensionLayout(string? dim)
        {
            switch (dim)
            {
                case "0D":
                    ClientSize = new Size(534, 543);
                    break;
                default:
                    ClientSize = new Size(534, 543);
                    break;
            }
        }
        private void btnCalculate_Click(object? sender, EventArgs e)
        {
            if (_activeController is null) return;
            try
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
            } catch (DllNotFoundException ex)
            {
                UhOh.CrashWithNoSurvivorsFromDll(this, ex);
            } catch (BadImageFormatException ex)
            {
                UhOh.CrashWithNoSurvivorsFromDll(this, ex);
            }
            
        }
        private void newCalculationToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (cmbDimensions.Items.Count > 0)
            {
                cmbDimensions.SelectedIndex = 0;
            }
            _activeController?.Reset();
        }
        private void copyToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            CopyResToClipboard();
        }
        private void CopyResToClipboard()
        {
            string resTxt = txtResult.Text.Trim();
            if (string.IsNullOrEmpty(resTxt))
            {
                return;
            }
            else try
            {
                    Clipboard.SetText(resTxt, TextDataFormat.UnicodeText);
            } catch (ExternalException ex)
                {
                    MessageBox.Show(
                    text: $"Copy to clipboard unavailable. The following error has occured:\n{ex.ErrorCode}=>{ex.Message}",
                    caption: "Copy Failed",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error
                    );
                }
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
