namespace WindowsFormsApp1
{
    partial class AccountCreationForm
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
            this.lblHeaderSub = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.picHeaderIcon = new System.Windows.Forms.PictureBox();
            this.panelOfficerCard = new System.Windows.Forms.Panel();
            this.lblRoleNotice = new System.Windows.Forms.Label();
            this.lblPasswordNote = new System.Windows.Forms.Label();
            this.chkShowPasswords = new System.Windows.Forms.CheckBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblOfficerSub = new System.Windows.Forms.Label();
            this.lblOfficerTitle = new System.Windows.Forms.Label();
            this.panelBiometricCard = new System.Windows.Forms.Panel();
            this.lblFootnote = new System.Windows.Forms.Label();
            this.btnScanEnroll = new System.Windows.Forms.Button();
            this.lblFPTemplate = new System.Windows.Forms.Label();
            this.lblFPStatus = new System.Windows.Forms.Label();
            this.progressBarFP = new System.Windows.Forms.ProgressBar();
            this.lblSensorDevice = new System.Windows.Forms.Label();
            this.panelScannerBox = new System.Windows.Forms.Panel();
            this.picFPImg = new System.Windows.Forms.PictureBox();
            this.lblBiometricWarning = new System.Windows.Forms.Label();
            this.lblBiometricTitle = new System.Windows.Forms.Label();
            this.panelBottomBar = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeaderIcon)).BeginInit();
            this.panelOfficerCard.SuspendLayout();
            this.panelBiometricCard.SuspendLayout();
            this.panelScannerBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFPImg)).BeginInit();
            this.panelBottomBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.panelHeader.Controls.Add(this.lblHeaderSub);
            this.panelHeader.Controls.Add(this.lblHeaderTitle);
            this.panelHeader.Controls.Add(this.picHeaderIcon);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(774, 82);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeaderSub
            // 
            this.lblHeaderSub.AutoSize = true;
            this.lblHeaderSub.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblHeaderSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(205)))), ((int)(((byte)(218)))));
            this.lblHeaderSub.Location = new System.Drawing.Point(82, 48);
            this.lblHeaderSub.Name = "lblHeaderSub";
            this.lblHeaderSub.Size = new System.Drawing.Size(415, 13);
            this.lblHeaderSub.TabIndex = 2;
            this.lblHeaderSub.Text = "Municipality of Subic • Office of the Municipal Treasurer • High-Assurance Identity Portal";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(79, 14);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(567, 30);
            this.lblHeaderTitle.TabIndex = 1;
            this.lblHeaderTitle.Text = "Vaultify — Account Registration & Biometric Enrollment";
            // 
            // picHeaderIcon
            // 
            this.picHeaderIcon.Location = new System.Drawing.Point(18, 12);
            this.picHeaderIcon.Name = "picHeaderIcon";
            this.picHeaderIcon.Size = new System.Drawing.Size(50, 56);
            this.picHeaderIcon.TabIndex = 0;
            this.picHeaderIcon.TabStop = false;
            this.picHeaderIcon.Paint += new System.Windows.Forms.PaintEventHandler(this.picHeaderIcon_Paint);
            // 
            // panelOfficerCard
            // 
            this.panelOfficerCard.BackColor = System.Drawing.Color.White;
            this.panelOfficerCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOfficerCard.Controls.Add(this.lblRoleNotice);
            this.panelOfficerCard.Controls.Add(this.lblPasswordNote);
            this.panelOfficerCard.Controls.Add(this.chkShowPasswords);
            this.panelOfficerCard.Controls.Add(this.txtConfirmPassword);
            this.panelOfficerCard.Controls.Add(this.lblConfirmPassword);
            this.panelOfficerCard.Controls.Add(this.txtPassword);
            this.panelOfficerCard.Controls.Add(this.lblPassword);
            this.panelOfficerCard.Controls.Add(this.txtUsername);
            this.panelOfficerCard.Controls.Add(this.lblUsername);
            this.panelOfficerCard.Controls.Add(this.txtFullName);
            this.panelOfficerCard.Controls.Add(this.lblFullName);
            this.panelOfficerCard.Controls.Add(this.lblOfficerSub);
            this.panelOfficerCard.Controls.Add(this.lblOfficerTitle);
            this.panelOfficerCard.Location = new System.Drawing.Point(20, 96);
            this.panelOfficerCard.Name = "panelOfficerCard";
            this.panelOfficerCard.Size = new System.Drawing.Size(360, 416);
            this.panelOfficerCard.TabIndex = 1;
            // 
            // lblRoleNotice
            // 
            this.lblRoleNotice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.lblRoleNotice.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblRoleNotice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
            this.lblRoleNotice.Location = new System.Drawing.Point(20, 360);
            this.lblRoleNotice.Name = "lblRoleNotice";
            this.lblRoleNotice.Padding = new System.Windows.Forms.Padding(6);
            this.lblRoleNotice.Size = new System.Drawing.Size(318, 42);
            this.lblRoleNotice.TabIndex = 12;
            this.lblRoleNotice.Text = "ℹ Account role assignment is governed by the System Administrator and Supervisor.";
            // 
            // lblPasswordNote
            // 
            this.lblPasswordNote.AutoSize = true;
            this.lblPasswordNote.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Italic);
            this.lblPasswordNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(140)))), ((int)(((byte)(150)))));
            this.lblPasswordNote.Location = new System.Drawing.Point(20, 332);
            this.lblPasswordNote.Name = "lblPasswordNote";
            this.lblPasswordNote.Size = new System.Drawing.Size(182, 12);
            this.lblPasswordNote.TabIndex = 11;
            this.lblPasswordNote.Text = "Note: Passwords must be at least 4 characters.";
            // 
            // chkShowPasswords
            // 
            this.chkShowPasswords.AutoSize = true;
            this.chkShowPasswords.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.chkShowPasswords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(100)))), ((int)(((byte)(110)))));
            this.chkShowPasswords.Location = new System.Drawing.Point(22, 305);
            this.chkShowPasswords.Name = "chkShowPasswords";
            this.chkShowPasswords.Size = new System.Drawing.Size(107, 17);
            this.chkShowPasswords.TabIndex = 10;
            this.chkShowPasswords.Text = "Show passwords";
            this.chkShowPasswords.UseVisualStyleBackColor = true;
            this.chkShowPasswords.CheckedChanged += new System.EventHandler(this.chkShowPasswords_CheckedChanged);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtConfirmPassword.Location = new System.Drawing.Point(20, 270);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(318, 24);
            this.txtConfirmPassword.TabIndex = 9;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.lblConfirmPassword.Location = new System.Drawing.Point(18, 250);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(111, 15);
            this.lblConfirmPassword.TabIndex = 8;
            this.lblConfirmPassword.Text = "Confirm Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtPassword.Location = new System.Drawing.Point(20, 208);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(318, 24);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.lblPassword.Location = new System.Drawing.Point(18, 188);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(62, 15);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Password:";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtUsername.Location = new System.Drawing.Point(20, 148);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(318, 24);
            this.txtUsername.TabIndex = 5;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.lblUsername.Location = new System.Drawing.Point(18, 128);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(111, 15);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "Desired Username:";
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtFullName.Location = new System.Drawing.Point(20, 88);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(318, 24);
            this.txtFullName.TabIndex = 3;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.lblFullName.Location = new System.Drawing.Point(18, 68);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(147, 15);
            this.lblFullName.TabIndex = 2;
            this.lblFullName.Text = "Full Name / Official Title:";
            // 
            // lblOfficerSub
            // 
            this.lblOfficerSub.AutoSize = true;
            this.lblOfficerSub.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblOfficerSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(130)))), ((int)(((byte)(140)))));
            this.lblOfficerSub.Location = new System.Drawing.Point(18, 38);
            this.lblOfficerSub.Name = "lblOfficerSub";
            this.lblOfficerSub.Size = new System.Drawing.Size(252, 13);
            this.lblOfficerSub.TabIndex = 1;
            this.lblOfficerSub.Text = "Enter personal and official municipal treasury credentials";
            // 
            // lblOfficerTitle
            // 
            this.lblOfficerTitle.AutoSize = true;
            this.lblOfficerTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblOfficerTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.lblOfficerTitle.Location = new System.Drawing.Point(16, 16);
            this.lblOfficerTitle.Name = "lblOfficerTitle";
            this.lblOfficerTitle.Size = new System.Drawing.Size(161, 20);
            this.lblOfficerTitle.TabIndex = 0;
            this.lblOfficerTitle.Text = "👤 Officer Credentials";
            // 
            // panelBiometricCard
            // 
            this.panelBiometricCard.BackColor = System.Drawing.Color.White;
            this.panelBiometricCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBiometricCard.Controls.Add(this.lblFootnote);
            this.panelBiometricCard.Controls.Add(this.btnScanEnroll);
            this.panelBiometricCard.Controls.Add(this.lblFPTemplate);
            this.panelBiometricCard.Controls.Add(this.lblFPStatus);
            this.panelBiometricCard.Controls.Add(this.progressBarFP);
            this.panelBiometricCard.Controls.Add(this.lblSensorDevice);
            this.panelBiometricCard.Controls.Add(this.panelScannerBox);
            this.panelBiometricCard.Controls.Add(this.lblBiometricWarning);
            this.panelBiometricCard.Controls.Add(this.lblBiometricTitle);
            this.panelBiometricCard.Location = new System.Drawing.Point(395, 96);
            this.panelBiometricCard.Name = "panelBiometricCard";
            this.panelBiometricCard.Size = new System.Drawing.Size(360, 416);
            this.panelBiometricCard.TabIndex = 2;
            // 
            // lblFootnote
            // 
            this.lblFootnote.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblFootnote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(140)))), ((int)(((byte)(150)))));
            this.lblFootnote.Location = new System.Drawing.Point(16, 385);
            this.lblFootnote.Name = "lblFootnote";
            this.lblFootnote.Size = new System.Drawing.Size(326, 16);
            this.lblFootnote.TabIndex = 8;
            this.lblFootnote.Text = "🔒 Cryptographic Minutiae Protected (ZKTeco Biometric Engine)";
            this.lblFootnote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnScanEnroll
            // 
            this.btnScanEnroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(117)))), ((int)(((byte)(107)))));
            this.btnScanEnroll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScanEnroll.FlatAppearance.BorderSize = 0;
            this.btnScanEnroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScanEnroll.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnScanEnroll.ForeColor = System.Drawing.Color.White;
            this.btnScanEnroll.Location = new System.Drawing.Point(20, 342);
            this.btnScanEnroll.Name = "btnScanEnroll";
            this.btnScanEnroll.Size = new System.Drawing.Size(320, 36);
            this.btnScanEnroll.TabIndex = 7;
            this.btnScanEnroll.Text = "⚡ Scan / Enroll Fingerprint";
            this.btnScanEnroll.UseVisualStyleBackColor = false;
            this.btnScanEnroll.Click += new System.EventHandler(this.btnScanEnroll_Click);
            // 
            // lblFPTemplate
            // 
            this.lblFPTemplate.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblFPTemplate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(120)))), ((int)(((byte)(130)))));
            this.lblFPTemplate.Location = new System.Drawing.Point(16, 318);
            this.lblFPTemplate.Name = "lblFPTemplate";
            this.lblFPTemplate.Size = new System.Drawing.Size(328, 16);
            this.lblFPTemplate.TabIndex = 6;
            this.lblFPTemplate.Text = "Template: [Not Registered]";
            this.lblFPTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFPStatus
            // 
            this.lblFPStatus.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblFPStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(90)))));
            this.lblFPStatus.Location = new System.Drawing.Point(16, 282);
            this.lblFPStatus.Name = "lblFPStatus";
            this.lblFPStatus.Size = new System.Drawing.Size(328, 30);
            this.lblFPStatus.TabIndex = 5;
            this.lblFPStatus.Text = "Status: Hardware Online. Place finger firmly on sensor.";
            this.lblFPStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarFP
            // 
            this.progressBarFP.Location = new System.Drawing.Point(20, 268);
            this.progressBarFP.Maximum = 3;
            this.progressBarFP.Name = "progressBarFP";
            this.progressBarFP.Size = new System.Drawing.Size(320, 8);
            this.progressBarFP.TabIndex = 4;
            // 
            // lblSensorDevice
            // 
            this.lblSensorDevice.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblSensorDevice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.lblSensorDevice.Location = new System.Drawing.Point(16, 248);
            this.lblSensorDevice.Name = "lblSensorDevice";
            this.lblSensorDevice.Size = new System.Drawing.Size(328, 16);
            this.lblSensorDevice.TabIndex = 3;
            this.lblSensorDevice.Text = "Sensor: ZKTeco Live20R (500 DPI Optical)";
            this.lblSensorDevice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelScannerBox
            // 
            this.panelScannerBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(253)))));
            this.panelScannerBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelScannerBox.Controls.Add(this.picFPImg);
            this.panelScannerBox.Location = new System.Drawing.Point(115, 95);
            this.panelScannerBox.Name = "panelScannerBox";
            this.panelScannerBox.Size = new System.Drawing.Size(130, 140);
            this.panelScannerBox.TabIndex = 2;
            // 
            // picFPImg
            // 
            this.picFPImg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(253)))));
            this.picFPImg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picFPImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picFPImg.Location = new System.Drawing.Point(0, 0);
            this.picFPImg.Name = "picFPImg";
            this.picFPImg.Size = new System.Drawing.Size(128, 138);
            this.picFPImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picFPImg.TabIndex = 0;
            this.picFPImg.TabStop = false;
            this.picFPImg.Click += new System.EventHandler(this.btnScanEnroll_Click);
            this.picFPImg.Paint += new System.Windows.Forms.PaintEventHandler(this.picFPImg_Paint);
            // 
            // lblBiometricWarning
            // 
            this.lblBiometricWarning.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblBiometricWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(45)))), ((int)(((byte)(35)))));
            this.lblBiometricWarning.Location = new System.Drawing.Point(18, 38);
            this.lblBiometricWarning.Name = "lblBiometricWarning";
            this.lblBiometricWarning.Size = new System.Drawing.Size(324, 38);
            this.lblBiometricWarning.TabIndex = 1;
            this.lblBiometricWarning.Text = "Mandatory: A certified biometric fingerprint scan is required to create a municipal treasury account.";
            // 
            // lblBiometricTitle
            // 
            this.lblBiometricTitle.AutoSize = true;
            this.lblBiometricTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBiometricTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.lblBiometricTitle.Location = new System.Drawing.Point(16, 16);
            this.lblBiometricTitle.Name = "lblBiometricTitle";
            this.lblBiometricTitle.Size = new System.Drawing.Size(228, 20);
            this.lblBiometricTitle.TabIndex = 0;
            this.lblBiometricTitle.Text = "👆 Biometric Fingerprint Station";
            // 
            // panelBottomBar
            // 
            this.panelBottomBar.Controls.Add(this.btnCancel);
            this.panelBottomBar.Controls.Add(this.btnRegister);
            this.panelBottomBar.Location = new System.Drawing.Point(20, 520);
            this.panelBottomBar.Name = "panelBottomBar";
            this.panelBottomBar.Size = new System.Drawing.Size(735, 48);
            this.panelBottomBar.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(232)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.btnCancel.Location = new System.Drawing.Point(545, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel / Back";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(60, 4);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(465, 40);
            this.btnRegister.TabIndex = 0;
            this.btnRegister.Text = "✓ Complete Registration • Create Account";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // AccountCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(774, 578);
            this.Controls.Add(this.panelBottomBar);
            this.Controls.Add(this.panelBiometricCard);
            this.Controls.Add(this.panelOfficerCard);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AccountCreationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vaultify - Municipal Staff Registration & Biometric Enrollment";
            this.Load += new System.EventHandler(this.AccountCreationForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeaderIcon)).EndInit();
            this.panelOfficerCard.ResumeLayout(false);
            this.panelOfficerCard.PerformLayout();
            this.panelBiometricCard.ResumeLayout(false);
            this.panelBiometricCard.PerformLayout();
            this.panelScannerBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFPImg)).EndInit();
            this.panelBottomBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.PictureBox picHeaderIcon;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSub;
        private System.Windows.Forms.Panel panelOfficerCard;
        private System.Windows.Forms.Label lblOfficerTitle;
        private System.Windows.Forms.Label lblOfficerSub;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.CheckBox chkShowPasswords;
        private System.Windows.Forms.Label lblPasswordNote;
        private System.Windows.Forms.Label lblRoleNotice;
        private System.Windows.Forms.Panel panelBiometricCard;
        private System.Windows.Forms.Label lblBiometricTitle;
        private System.Windows.Forms.Label lblBiometricWarning;
        private System.Windows.Forms.Panel panelScannerBox;
        private System.Windows.Forms.PictureBox picFPImg;
        private System.Windows.Forms.Label lblSensorDevice;
        private System.Windows.Forms.ProgressBar progressBarFP;
        private System.Windows.Forms.Label lblFPStatus;
        private System.Windows.Forms.Label lblFPTemplate;
        private System.Windows.Forms.Button btnScanEnroll;
        private System.Windows.Forms.Label lblFootnote;
        private System.Windows.Forms.Panel panelBottomBar;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnCancel;
    }
}