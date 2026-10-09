namespace WindowsFormsApp1
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeaderSub2 = new System.Windows.Forms.Label();
            this.lblHeaderSub1 = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.picHeaderIcon = new System.Windows.Forms.PictureBox();
            this.panelTabs = new System.Windows.Forms.Panel();
            this.btnTabPassword = new System.Windows.Forms.Button();
            this.btnTabBiometric = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelBiometricCard = new System.Windows.Forms.Panel();
            this.lblBiometricHashNote = new System.Windows.Forms.Label();
            this.btnScanBiometric = new System.Windows.Forms.Button();
            this.lblScannerInstruction = new System.Windows.Forms.Label();
            this.lblScannerDevice = new System.Windows.Forms.Label();
            this.progressBarScanner = new System.Windows.Forms.ProgressBar();
            this.panelScannerBox = new System.Windows.Forms.Panel();
            this.picFPImg = new System.Windows.Forms.PictureBox();
            this.lblStaffInfo = new System.Windows.Forms.Label();
            this.cboStaff = new System.Windows.Forms.ComboBox();
            this.lblSelectStaff = new System.Windows.Forms.Label();
            this.panelPasswordCard = new System.Windows.Forms.Panel();
            this.btnLoginPassword = new System.Windows.Forms.Button();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPasswordSubtitle = new System.Windows.Forms.Label();
            this.lblPasswordTitle = new System.Windows.Forms.Label();
            this.btnNewStaff = new System.Windows.Forms.Button();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblFooterStatus = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeaderIcon)).BeginInit();
            this.panelTabs.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelBiometricCard.SuspendLayout();
            this.panelScannerBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFPImg)).BeginInit();
            this.panelPasswordCard.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.panelHeader.Controls.Add(this.lblHeaderSub2);
            this.panelHeader.Controls.Add(this.lblHeaderSub1);
            this.panelHeader.Controls.Add(this.lblHeaderTitle);
            this.panelHeader.Controls.Add(this.picHeaderIcon);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(464, 96);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeaderSub2
            // 
            this.lblHeaderSub2.AutoSize = true;
            this.lblHeaderSub2.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblHeaderSub2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(175)))), ((int)(((byte)(190)))));
            this.lblHeaderSub2.Location = new System.Drawing.Point(86, 68);
            this.lblHeaderSub2.Name = "lblHeaderSub2";
            this.lblHeaderSub2.Size = new System.Drawing.Size(264, 12);
            this.lblHeaderSub2.TabIndex = 3;
            this.lblHeaderSub2.Text = "Centralized Collection • High-Assurance Access Control";
            // 
            // lblHeaderSub1
            // 
            this.lblHeaderSub1.AutoSize = true;
            this.lblHeaderSub1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblHeaderSub1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(212)))), ((int)(((byte)(224)))));
            this.lblHeaderSub1.Location = new System.Drawing.Point(86, 48);
            this.lblHeaderSub1.Name = "lblHeaderSub1";
            this.lblHeaderSub1.Size = new System.Drawing.Size(236, 13);
            this.lblHeaderSub1.TabIndex = 2;
            this.lblHeaderSub1.Text = "Municipality of Subic • Office of the Treasurer";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(83, 14);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(107, 32);
            this.lblHeaderTitle.TabIndex = 1;
            this.lblHeaderTitle.Text = "Vaultify";
            // 
            // picHeaderIcon
            // 
            this.picHeaderIcon.Location = new System.Drawing.Point(22, 18);
            this.picHeaderIcon.Name = "picHeaderIcon";
            this.picHeaderIcon.Size = new System.Drawing.Size(52, 60);
            this.picHeaderIcon.TabIndex = 0;
            this.picHeaderIcon.TabStop = false;
            this.picHeaderIcon.Paint += new System.Windows.Forms.PaintEventHandler(this.picHeaderIcon_Paint);
            // 
            // panelTabs
            // 
            this.panelTabs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(235)))), ((int)(((byte)(240)))));
            this.panelTabs.Controls.Add(this.btnTabPassword);
            this.panelTabs.Controls.Add(this.btnTabBiometric);
            this.panelTabs.Location = new System.Drawing.Point(20, 110);
            this.panelTabs.Name = "panelTabs";
            this.panelTabs.Size = new System.Drawing.Size(424, 42);
            this.panelTabs.TabIndex = 1;
            // 
            // btnTabPassword
            // 
            this.btnTabPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(233)))), ((int)(((byte)(238)))));
            this.btnTabPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabPassword.FlatAppearance.BorderSize = 0;
            this.btnTabPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTabPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.btnTabPassword.Location = new System.Drawing.Point(212, 3);
            this.btnTabPassword.Name = "btnTabPassword";
            this.btnTabPassword.Size = new System.Drawing.Size(208, 36);
            this.btnTabPassword.TabIndex = 1;
            this.btnTabPassword.Text = "🔑 Password Login";
            this.btnTabPassword.UseVisualStyleBackColor = false;
            this.btnTabPassword.Click += new System.EventHandler(this.btnTabPassword_Click);
            // 
            // btnTabBiometric
            // 
            this.btnTabBiometric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(117)))), ((int)(((byte)(107)))));
            this.btnTabBiometric.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabBiometric.FlatAppearance.BorderSize = 0;
            this.btnTabBiometric.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabBiometric.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTabBiometric.ForeColor = System.Drawing.Color.White;
            this.btnTabBiometric.Location = new System.Drawing.Point(3, 3);
            this.btnTabBiometric.Name = "btnTabBiometric";
            this.btnTabBiometric.Size = new System.Drawing.Size(208, 36);
            this.btnTabBiometric.TabIndex = 0;
            this.btnTabBiometric.Text = "🔒 Biometric One-Touch Access";
            this.btnTabBiometric.UseVisualStyleBackColor = false;
            this.btnTabBiometric.Click += new System.EventHandler(this.btnTabBiometric_Click);
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.panelBiometricCard);
            this.panelContent.Controls.Add(this.panelPasswordCard);
            this.panelContent.Location = new System.Drawing.Point(20, 160);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(424, 400);
            this.panelContent.TabIndex = 2;
            // 
            // panelBiometricCard
            // 
            this.panelBiometricCard.BackColor = System.Drawing.Color.White;
            this.panelBiometricCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBiometricCard.Controls.Add(this.lblBiometricHashNote);
            this.panelBiometricCard.Controls.Add(this.btnScanBiometric);
            this.panelBiometricCard.Controls.Add(this.lblScannerInstruction);
            this.panelBiometricCard.Controls.Add(this.lblScannerDevice);
            this.panelBiometricCard.Controls.Add(this.progressBarScanner);
            this.panelBiometricCard.Controls.Add(this.panelScannerBox);
            this.panelBiometricCard.Controls.Add(this.lblStaffInfo);
            this.panelBiometricCard.Controls.Add(this.cboStaff);
            this.panelBiometricCard.Controls.Add(this.lblSelectStaff);
            this.panelBiometricCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBiometricCard.Location = new System.Drawing.Point(0, 0);
            this.panelBiometricCard.Name = "panelBiometricCard";
            this.panelBiometricCard.Size = new System.Drawing.Size(424, 400);
            this.panelBiometricCard.TabIndex = 0;
            // 
            // lblBiometricHashNote
            // 
            this.lblBiometricHashNote.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblBiometricHashNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(140)))), ((int)(((byte)(150)))));
            this.lblBiometricHashNote.Location = new System.Drawing.Point(20, 365);
            this.lblBiometricHashNote.Name = "lblBiometricHashNote";
            this.lblBiometricHashNote.Size = new System.Drawing.Size(380, 18);
            this.lblBiometricHashNote.TabIndex = 8;
            this.lblBiometricHashNote.Text = "🔒 One-touch zero-password authentication via municipal biometric hash";
            this.lblBiometricHashNote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnScanBiometric
            // 
            this.btnScanBiometric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(117)))), ((int)(((byte)(107)))));
            this.btnScanBiometric.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScanBiometric.FlatAppearance.BorderSize = 0;
            this.btnScanBiometric.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScanBiometric.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnScanBiometric.ForeColor = System.Drawing.Color.White;
            this.btnScanBiometric.Location = new System.Drawing.Point(24, 320);
            this.btnScanBiometric.Name = "btnScanBiometric";
            this.btnScanBiometric.Size = new System.Drawing.Size(374, 40);
            this.btnScanBiometric.TabIndex = 7;
            this.btnScanBiometric.Text = "⚡ Scan Fingerprint • Access Vaultify";
            this.btnScanBiometric.UseVisualStyleBackColor = false;
            this.btnScanBiometric.Click += new System.EventHandler(this.btnScanBiometric_Click);
            // 
            // lblScannerInstruction
            // 
            this.lblScannerInstruction.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblScannerInstruction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(120)))), ((int)(((byte)(130)))));
            this.lblScannerInstruction.Location = new System.Drawing.Point(20, 296);
            this.lblScannerInstruction.Name = "lblScannerInstruction";
            this.lblScannerInstruction.Size = new System.Drawing.Size(382, 16);
            this.lblScannerInstruction.TabIndex = 6;
            this.lblScannerInstruction.Text = "Click scanner or button below to authenticate.";
            this.lblScannerInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScannerDevice
            // 
            this.lblScannerDevice.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblScannerDevice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.lblScannerDevice.Location = new System.Drawing.Point(20, 278);
            this.lblScannerDevice.Name = "lblScannerDevice";
            this.lblScannerDevice.Size = new System.Drawing.Size(382, 16);
            this.lblScannerDevice.TabIndex = 5;
            this.lblScannerDevice.Text = "Optical Scanner Ready (ZKTeco Live20R)";
            this.lblScannerDevice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarScanner
            // 
            this.progressBarScanner.Location = new System.Drawing.Point(24, 266);
            this.progressBarScanner.Name = "progressBarScanner";
            this.progressBarScanner.Size = new System.Drawing.Size(374, 6);
            this.progressBarScanner.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarScanner.TabIndex = 4;
            // 
            // panelScannerBox
            // 
            this.panelScannerBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(253)))));
            this.panelScannerBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelScannerBox.Controls.Add(this.picFPImg);
            this.panelScannerBox.Location = new System.Drawing.Point(142, 115);
            this.panelScannerBox.Name = "panelScannerBox";
            this.panelScannerBox.Size = new System.Drawing.Size(140, 140);
            this.panelScannerBox.TabIndex = 3;
            // 
            // picFPImg
            // 
            this.picFPImg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(253)))));
            this.picFPImg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picFPImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picFPImg.Location = new System.Drawing.Point(0, 0);
            this.picFPImg.Name = "picFPImg";
            this.picFPImg.Size = new System.Drawing.Size(138, 138);
            this.picFPImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picFPImg.TabIndex = 0;
            this.picFPImg.TabStop = false;
            this.picFPImg.Click += new System.EventHandler(this.picFPImg_Click);
            this.picFPImg.Paint += new System.Windows.Forms.PaintEventHandler(this.picFPImg_Paint);
            // 
            // lblStaffInfo
            // 
            this.lblStaffInfo.AutoSize = true;
            this.lblStaffInfo.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Italic);
            this.lblStaffInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(95)))), ((int)(((byte)(115)))));
            this.lblStaffInfo.Location = new System.Drawing.Point(22, 70);
            this.lblStaffInfo.Name = "lblStaffInfo";
            this.lblStaffInfo.Size = new System.Drawing.Size(268, 12);
            this.lblStaffInfo.TabIndex = 2;
            this.lblStaffInfo.Text = "Officer: Auto-Detect • Role: All Registered Staff (Biometrics Enrolled)";
            // 
            // cboStaff
            // 
            this.cboStaff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStaff.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboStaff.FormattingEnabled = true;
            this.cboStaff.Location = new System.Drawing.Point(24, 42);
            this.cboStaff.Name = "cboStaff";
            this.cboStaff.Size = new System.Drawing.Size(374, 23);
            this.cboStaff.TabIndex = 1;
            this.cboStaff.SelectedIndexChanged += new System.EventHandler(this.cboStaff_SelectedIndexChanged);
            // 
            // lblSelectStaff
            // 
            this.lblSelectStaff.AutoSize = true;
            this.lblSelectStaff.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelectStaff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.lblSelectStaff.Location = new System.Drawing.Point(21, 20);
            this.lblSelectStaff.Name = "lblSelectStaff";
            this.lblSelectStaff.Size = new System.Drawing.Size(182, 15);
            this.lblSelectStaff.TabIndex = 0;
            this.lblSelectStaff.Text = "Select Registered Staff Account:";
            // 
            // panelPasswordCard
            // 
            this.panelPasswordCard.BackColor = System.Drawing.Color.White;
            this.panelPasswordCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPasswordCard.Controls.Add(this.btnLoginPassword);
            this.panelPasswordCard.Controls.Add(this.chkShowPassword);
            this.panelPasswordCard.Controls.Add(this.txtPassword);
            this.panelPasswordCard.Controls.Add(this.lblPassword);
            this.panelPasswordCard.Controls.Add(this.txtUsername);
            this.panelPasswordCard.Controls.Add(this.lblUsername);
            this.panelPasswordCard.Controls.Add(this.lblPasswordSubtitle);
            this.panelPasswordCard.Controls.Add(this.lblPasswordTitle);
            this.panelPasswordCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPasswordCard.Location = new System.Drawing.Point(0, 0);
            this.panelPasswordCard.Name = "panelPasswordCard";
            this.panelPasswordCard.Size = new System.Drawing.Size(424, 400);
            this.panelPasswordCard.TabIndex = 1;
            this.panelPasswordCard.Visible = false;
            // 
            // btnLoginPassword
            // 
            this.btnLoginPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(117)))), ((int)(((byte)(107)))));
            this.btnLoginPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoginPassword.FlatAppearance.BorderSize = 0;
            this.btnLoginPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoginPassword.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLoginPassword.ForeColor = System.Drawing.Color.White;
            this.btnLoginPassword.Location = new System.Drawing.Point(24, 275);
            this.btnLoginPassword.Name = "btnLoginPassword";
            this.btnLoginPassword.Size = new System.Drawing.Size(374, 42);
            this.btnLoginPassword.TabIndex = 7;
            this.btnLoginPassword.Text = "Login • Access Vaultify";
            this.btnLoginPassword.UseVisualStyleBackColor = false;
            this.btnLoginPassword.Click += new System.EventHandler(this.btnLoginPassword_Click);
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.chkShowPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(100)))), ((int)(((byte)(110)))));
            this.chkShowPassword.Location = new System.Drawing.Point(26, 218);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new System.Drawing.Size(107, 17);
            this.chkShowPassword.TabIndex = 6;
            this.chkShowPassword.Text = "Show passwords";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.Location = new System.Drawing.Point(24, 180);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(374, 25);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.lblPassword.Location = new System.Drawing.Point(21, 160);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(63, 15);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password:";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUsername.Location = new System.Drawing.Point(24, 115);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(374, 25);
            this.txtUsername.TabIndex = 3;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.lblUsername.Location = new System.Drawing.Point(21, 95);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(67, 15);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Username:";
            // 
            // lblPasswordSubtitle
            // 
            this.lblPasswordSubtitle.AutoSize = true;
            this.lblPasswordSubtitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblPasswordSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(120)))), ((int)(((byte)(130)))));
            this.lblPasswordSubtitle.Location = new System.Drawing.Point(21, 46);
            this.lblPasswordSubtitle.Name = "lblPasswordSubtitle";
            this.lblPasswordSubtitle.Size = new System.Drawing.Size(239, 13);
            this.lblPasswordSubtitle.TabIndex = 1;
            this.lblPasswordSubtitle.Text = "Enter personal and official treasury credentials";
            // 
            // lblPasswordTitle
            // 
            this.lblPasswordTitle.AutoSize = true;
            this.lblPasswordTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPasswordTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.lblPasswordTitle.Location = new System.Drawing.Point(20, 24);
            this.lblPasswordTitle.Name = "lblPasswordTitle";
            this.lblPasswordTitle.Size = new System.Drawing.Size(214, 20);
            this.lblPasswordTitle.TabIndex = 0;
            this.lblPasswordTitle.Text = "Officer Account Credentials";
            // 
            // btnNewStaff
            // 
            this.btnNewStaff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(242)))), ((int)(((byte)(247)))));
            this.btnNewStaff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewStaff.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(198)))), ((int)(((byte)(212)))));
            this.btnNewStaff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewStaff.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNewStaff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(72)))), ((int)(((byte)(88)))));
            this.btnNewStaff.Location = new System.Drawing.Point(20, 574);
            this.btnNewStaff.Name = "btnNewStaff";
            this.btnNewStaff.Size = new System.Drawing.Size(424, 38);
            this.btnNewStaff.TabIndex = 3;
            this.btnNewStaff.Text = "👤 New Staff Officer? Create Account • Enroll Biometrics";
            this.btnNewStaff.UseVisualStyleBackColor = false;
            this.btnNewStaff.Click += new System.EventHandler(this.btnNewStaff_Click);
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(233)))), ((int)(((byte)(238)))));
            this.panelFooter.Controls.Add(this.lblFooterStatus);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 630);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(464, 28);
            this.panelFooter.TabIndex = 4;
            // 
            // lblFooterStatus
            // 
            this.lblFooterStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFooterStatus.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblFooterStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(100)))), ((int)(((byte)(110)))));
            this.lblFooterStatus.Location = new System.Drawing.Point(0, 0);
            this.lblFooterStatus.Name = "lblFooterStatus";
            this.lblFooterStatus.Size = new System.Drawing.Size(464, 28);
            this.lblFooterStatus.TabIndex = 0;
            this.lblFooterStatus.Text = "● Hardware Sensor Ready • Subic Treasury OS v2.4";
            this.lblFooterStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(464, 658);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.btnNewStaff);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelTabs);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vaultify - Subic Municipality Treasury Authentication";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeaderIcon)).EndInit();
            this.panelTabs.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelBiometricCard.ResumeLayout(false);
            this.panelBiometricCard.PerformLayout();
            this.panelScannerBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFPImg)).EndInit();
            this.panelPasswordCard.ResumeLayout(false);
            this.panelPasswordCard.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.PictureBox picHeaderIcon;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSub1;
        private System.Windows.Forms.Label lblHeaderSub2;
        private System.Windows.Forms.Panel panelTabs;
        private System.Windows.Forms.Button btnTabBiometric;
        private System.Windows.Forms.Button btnTabPassword;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelBiometricCard;
        private System.Windows.Forms.Label lblSelectStaff;
        private System.Windows.Forms.ComboBox cboStaff;
        private System.Windows.Forms.Label lblStaffInfo;
        private System.Windows.Forms.Panel panelScannerBox;
        private System.Windows.Forms.PictureBox picFPImg;
        private System.Windows.Forms.ProgressBar progressBarScanner;
        private System.Windows.Forms.Label lblScannerDevice;
        private System.Windows.Forms.Label lblScannerInstruction;
        private System.Windows.Forms.Button btnScanBiometric;
        private System.Windows.Forms.Label lblBiometricHashNote;
        private System.Windows.Forms.Panel panelPasswordCard;
        private System.Windows.Forms.Label lblPasswordTitle;
        private System.Windows.Forms.Label lblPasswordSubtitle;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Button btnLoginPassword;
        private System.Windows.Forms.Button btnNewStaff;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label lblFooterStatus;
    }
}
