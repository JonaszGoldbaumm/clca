namespace clacF
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer? components = null;

        private MenuStrip menuStripMain = null!;
        private ToolStripMenuItem fileToolStripMenuItem = null!;
        private ToolStripMenuItem newCalculationToolStripMenuItem = null!;
        private ToolStripMenuItem exitToolStripMenuItem = null!;
        private ToolStripMenuItem helpToolStripMenuItem = null!;
        private ToolStripMenuItem aboutToolStripMenuItem = null!;

        private Label lblDimension = null!;
        private ComboBox cmbDimensions = null!;

        // 0D mode containers (managed by ZeroDimensionController; toggled when other dimensions are active)
        private GroupBox grpOperations = null!;
        private GroupBox grpInputsAndResult = null!;
        private Label lblFirstNumber = null!;
        private TextBox txtFirstNumber = null!;
        private Label lblSecondNumber = null!;
        private TextBox txtSecondNumber = null!;
        private Button btnCalculate = null!;
        private Label lblResult = null!;
        private TextBox txtResult = null!;

        // Reserved area for future dimension modes (e.g., 1D vectors/arrays or 2D matrices)
        private Panel pnlHigherDimensions = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            menuStripMain = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newCalculationToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            lblDimension = new Label();
            cmbDimensions = new ComboBox();
            grpOperations = new GroupBox();
            grpInputsAndResult = new GroupBox();
            lblFirstNumber = new Label();
            txtFirstNumber = new TextBox();
            lblSecondNumber = new Label();
            txtSecondNumber = new TextBox();
            btnCalculate = new Button();
            lblResult = new Label();
            txtResult = new TextBox();
            pnlHigherDimensions = new Panel();
            menuStripMain.SuspendLayout();
            grpInputsAndResult.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(521, 24);
            menuStripMain.TabIndex = 0;
            menuStripMain.Text = "menuStripMain";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newCalculationToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newCalculationToolStripMenuItem
            // 
            newCalculationToolStripMenuItem.Name = "newCalculationToolStripMenuItem";
            newCalculationToolStripMenuItem.Size = new Size(161, 22);
            newCalculationToolStripMenuItem.Text = "New Calculation";
            newCalculationToolStripMenuItem.Click += newCalculationToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(161, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(107, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // lblDimension
            // 
            lblDimension.AutoSize = true;
            lblDimension.Location = new Point(20, 42);
            lblDimension.Name = "lblDimension";
            lblDimension.Size = new Size(86, 15);
            lblDimension.TabIndex = 1;
            lblDimension.Text = "Dimension (D):";
            // 
            // cmbDimensions
            // 
            cmbDimensions.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDimensions.FormattingEnabled = true;
            cmbDimensions.Location = new Point(110, 38);
            cmbDimensions.Name = "cmbDimensions";
            cmbDimensions.Size = new Size(110, 23);
            cmbDimensions.TabIndex = 2;
            // 
            // grpOperations
            // 
            grpOperations.Location = new Point(20, 76);
            grpOperations.Name = "grpOperations";
            grpOperations.Size = new Size(200, 434);
            grpOperations.TabIndex = 3;
            grpOperations.TabStop = false;
            grpOperations.Text = "0D Operations";
            // 
            // grpInputsAndResult
            // 
            grpInputsAndResult.Controls.Add(lblFirstNumber);
            grpInputsAndResult.Controls.Add(txtFirstNumber);
            grpInputsAndResult.Controls.Add(lblSecondNumber);
            grpInputsAndResult.Controls.Add(txtSecondNumber);
            grpInputsAndResult.Controls.Add(btnCalculate);
            grpInputsAndResult.Controls.Add(lblResult);
            grpInputsAndResult.Controls.Add(txtResult);
            grpInputsAndResult.Location = new Point(240, 76);
            grpInputsAndResult.Name = "grpInputsAndResult";
            grpInputsAndResult.Size = new Size(269, 434);
            grpInputsAndResult.TabIndex = 4;
            grpInputsAndResult.TabStop = false;
            grpInputsAndResult.Text = "0D Values & Result";
            // 
            // lblFirstNumber
            // 
            lblFirstNumber.AutoSize = true;
            lblFirstNumber.Location = new Point(16, 30);
            lblFirstNumber.Name = "lblFirstNumber";
            lblFirstNumber.Size = new Size(63, 15);
            lblFirstNumber.TabIndex = 0;
            lblFirstNumber.Text = "Number 1:";
            // 
            // txtFirstNumber
            // 
            txtFirstNumber.Location = new Point(105, 27);
            txtFirstNumber.Name = "txtFirstNumber";
            txtFirstNumber.Size = new Size(150, 23);
            txtFirstNumber.TabIndex = 0;
            // 
            // lblSecondNumber
            // 
            lblSecondNumber.AutoSize = true;
            lblSecondNumber.Location = new Point(16, 64);
            lblSecondNumber.Name = "lblSecondNumber";
            lblSecondNumber.Size = new Size(63, 15);
            lblSecondNumber.TabIndex = 1;
            lblSecondNumber.Text = "Number 2:";
            // 
            // txtSecondNumber
            // 
            txtSecondNumber.Location = new Point(105, 61);
            txtSecondNumber.Name = "txtSecondNumber";
            txtSecondNumber.Size = new Size(150, 23);
            txtSecondNumber.TabIndex = 1;
            // 
            // btnCalculate
            // 
            btnCalculate.Location = new Point(105, 96);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(150, 28);
            btnCalculate.TabIndex = 2;
            btnCalculate.Text = "Calculate";
            btnCalculate.UseVisualStyleBackColor = true;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Location = new Point(16, 144);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(42, 15);
            lblResult.TabIndex = 3;
            lblResult.Text = "Result:";
            // 
            // txtResult
            // 
            txtResult.Location = new Point(105, 141);
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.Size = new Size(150, 23);
            txtResult.TabIndex = 3;
            txtResult.TabStop = false;
            // 
            // pnlHigherDimensions
            // 
            pnlHigherDimensions.Location = new Point(421, 462);
            pnlHigherDimensions.Name = "pnlHigherDimensions";
            pnlHigherDimensions.Size = new Size(42, 48);
            pnlHigherDimensions.TabIndex = 5;
            pnlHigherDimensions.Visible = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(521, 527);
            Controls.Add(pnlHigherDimensions);
            Controls.Add(grpInputsAndResult);
            Controls.Add(grpOperations);
            Controls.Add(cmbDimensions);
            Controls.Add(lblDimension);
            Controls.Add(menuStripMain);
            MainMenuStrip = menuStripMain;
            Name = "MainForm";
            Text = "Calculator";
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            grpInputsAndResult.ResumeLayout(false);
            grpInputsAndResult.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
