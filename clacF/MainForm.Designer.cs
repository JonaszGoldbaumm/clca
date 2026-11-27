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

        private Panel pnlOperationsList = null!;
        private GroupBox grpOperations = null!;
        private GroupBox grpInputsAndResult = null!;
        private Label lblFirstNumber = null!;
        private TextBox txtFirstNumber = null!;
        private Label lblSecondNumber = null!;
        private TextBox txtSecondNumber = null!;
        private Button btnCalculate = null!;
        private Label lblResult = null!;
        private TextBox txtResult = null!;

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
            copyToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            lblDimension = new Label();
            cmbDimensions = new ComboBox();
            grpOperations = new GroupBox();
            pnlOperationsList = new Panel();
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
            grpOperations.SuspendLayout();
            grpInputsAndResult.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(534, 24);
            menuStripMain.TabIndex = 0;
            menuStripMain.Text = "menuStripMain";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newCalculationToolStripMenuItem, copyToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newCalculationToolStripMenuItem
            // 
            newCalculationToolStripMenuItem.Name = "newCalculationToolStripMenuItem";
            newCalculationToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newCalculationToolStripMenuItem.Size = new Size(204, 22);
            newCalculationToolStripMenuItem.Text = "New Calculation";
            newCalculationToolStripMenuItem.Click += newCalculationToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            copyToolStripMenuItem.Size = new Size(204, 22);
            copyToolStripMenuItem.Text = "Copy";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Q;
            exitToolStripMenuItem.Size = new Size(204, 22);
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
            aboutToolStripMenuItem.ShortcutKeys = Keys.F1;
            aboutToolStripMenuItem.Size = new Size(126, 22);
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
            grpOperations.Controls.Add(pnlOperationsList);
            grpOperations.Location = new Point(20, 76);
            grpOperations.Name = "grpOperations";
            grpOperations.Size = new Size(200, 434);
            grpOperations.TabIndex = 3;
            grpOperations.TabStop = false;
            grpOperations.Text = "0D Operations";
            // 
            // pnlOperationsList
            // 
            pnlOperationsList.AutoScroll = true;
            pnlOperationsList.Location = new Point(8, 22);
            pnlOperationsList.Name = "pnlOperationsList";
            pnlOperationsList.Size = new Size(184, 406);
            pnlOperationsList.TabIndex = 0;
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
            lblFirstNumber.Location = new Point(24, 286);
            lblFirstNumber.Name = "lblFirstNumber";
            lblFirstNumber.Size = new Size(63, 15);
            lblFirstNumber.TabIndex = 0;
            lblFirstNumber.Text = "Number 1:";
            // 
            // txtFirstNumber
            // 
            txtFirstNumber.Location = new Point(113, 283);
            txtFirstNumber.Name = "txtFirstNumber";
            txtFirstNumber.Size = new Size(150, 23);
            txtFirstNumber.TabIndex = 0;
            // 
            // lblSecondNumber
            // 
            lblSecondNumber.AutoSize = true;
            lblSecondNumber.Location = new Point(24, 320);
            lblSecondNumber.Name = "lblSecondNumber";
            lblSecondNumber.Size = new Size(63, 15);
            lblSecondNumber.TabIndex = 1;
            lblSecondNumber.Text = "Number 2:";
            // 
            // txtSecondNumber
            // 
            txtSecondNumber.Location = new Point(113, 317);
            txtSecondNumber.Name = "txtSecondNumber";
            txtSecondNumber.Size = new Size(150, 23);
            txtSecondNumber.TabIndex = 1;
            // 
            // btnCalculate
            // 
            btnCalculate.Location = new Point(113, 352);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(150, 28);
            btnCalculate.TabIndex = 2;
            btnCalculate.Text = "Calculate";
            btnCalculate.UseVisualStyleBackColor = true;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Location = new Point(24, 400);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(42, 15);
            lblResult.TabIndex = 3;
            lblResult.Text = "Result:";
            // 
            // txtResult
            // 
            txtResult.Location = new Point(113, 397);
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.Size = new Size(150, 23);
            txtResult.TabIndex = 3;
            txtResult.TabStop = false;
            // 
            // pnlHigherDimensions
            // 
            pnlHigherDimensions.Location = new Point(467, 38);
            pnlHigherDimensions.Name = "pnlHigherDimensions";
            pnlHigherDimensions.Size = new Size(42, 48);
            pnlHigherDimensions.TabIndex = 5;
            pnlHigherDimensions.Visible = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 543);
            Controls.Add(pnlHigherDimensions);
            Controls.Add(grpInputsAndResult);
            Controls.Add(grpOperations);
            Controls.Add(cmbDimensions);
            Controls.Add(lblDimension);
            Controls.Add(menuStripMain);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStripMain;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Calculator";
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            grpOperations.ResumeLayout(false);
            grpInputsAndResult.ResumeLayout(false);
            grpInputsAndResult.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private ToolStripMenuItem copyToolStripMenuItem;
    }
}
