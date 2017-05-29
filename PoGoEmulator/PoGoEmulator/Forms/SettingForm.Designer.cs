namespace RocketBot2.Forms
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.cancelBtn = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabAuth = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPosition = new System.Windows.Forms.TabPage();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.AdressBox = new System.Windows.Forms.TextBox();
            this.FindAdressBtn = new System.Windows.Forms.Button();
            this.ResetLocationBtn = new System.Windows.Forms.Button();
            this.gMapCtrl = new GMap.NET.WindowsForms.GMapControl();
            this.GoogleApiBox = new System.Windows.Forms.TextBox();
            this.GoogleApiLabel = new System.Windows.Forms.Label();
            this.tbWalkingSpeed = new System.Windows.Forms.TextBox();
            this.TravelSpeedText = new System.Windows.Forms.Label();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.proxyGb = new System.Windows.Forms.GroupBox();
            this.proxyPortTb = new System.Windows.Forms.TextBox();
            this.proxyUserTb = new System.Windows.Forms.TextBox();
            this.proxyPwTb = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.proxyHostTb = new System.Windows.Forms.TextBox();
            this.useProxyAuthCb = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.useProxyCb = new System.Windows.Forms.CheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.UserLoginBox = new System.Windows.Forms.TextBox();
            this.UserPasswordBox = new System.Windows.Forms.TextBox();
            this.tbLatitude = new System.Windows.Forms.TextBox();
            this.tbLongitude = new System.Windows.Forms.TextBox();
            this.authTypeLabel = new System.Windows.Forms.Label();
            this.longiLabel = new System.Windows.Forms.Label();
            this.authTypeCb = new System.Windows.Forms.ComboBox();
            this.latLabel = new System.Windows.Forms.Label();
            this.UserLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabAuth.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.proxyGb.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.Location = new System.Drawing.Point(863, 535);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(104, 30);
            this.cancelBtn.TabIndex = 31;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabAuth);
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(977, 509);
            this.tabControl.TabIndex = 30;
            // 
            // tabAuth
            // 
            this.tabAuth.BackColor = System.Drawing.SystemColors.Control;
            this.tabAuth.Controls.Add(this.tabControl1);
            this.tabAuth.Controls.Add(this.GoogleApiBox);
            this.tabAuth.Controls.Add(this.GoogleApiLabel);
            this.tabAuth.Controls.Add(this.tbWalkingSpeed);
            this.tabAuth.Controls.Add(this.TravelSpeedText);
            this.tabAuth.Controls.Add(this.cbLanguage);
            this.tabAuth.Controls.Add(this.label26);
            this.tabAuth.Controls.Add(this.proxyGb);
            this.tabAuth.Controls.Add(this.UserLoginBox);
            this.tabAuth.Controls.Add(this.UserPasswordBox);
            this.tabAuth.Controls.Add(this.tbLatitude);
            this.tabAuth.Controls.Add(this.tbLongitude);
            this.tabAuth.Controls.Add(this.authTypeLabel);
            this.tabAuth.Controls.Add(this.longiLabel);
            this.tabAuth.Controls.Add(this.authTypeCb);
            this.tabAuth.Controls.Add(this.latLabel);
            this.tabAuth.Controls.Add(this.UserLabel);
            this.tabAuth.Controls.Add(this.PasswordLabel);
            this.tabAuth.Location = new System.Drawing.Point(4, 29);
            this.tabAuth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabAuth.Name = "tabAuth";
            this.tabAuth.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabAuth.Size = new System.Drawing.Size(969, 476);
            this.tabAuth.TabIndex = 0;
            this.tabAuth.Text = "Auth";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPosition);
            this.tabControl1.Location = new System.Drawing.Point(330, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(639, 467);
            this.tabControl1.TabIndex = 34;
            // 
            // tabPosition
            // 
            this.tabPosition.BackColor = System.Drawing.SystemColors.Control;
            this.tabPosition.Controls.Add(this.trackBar);
            this.tabPosition.Controls.Add(this.AdressBox);
            this.tabPosition.Controls.Add(this.FindAdressBtn);
            this.tabPosition.Controls.Add(this.ResetLocationBtn);
            this.tabPosition.Controls.Add(this.gMapCtrl);
            this.tabPosition.Location = new System.Drawing.Point(4, 29);
            this.tabPosition.Name = "tabPosition";
            this.tabPosition.Padding = new System.Windows.Forms.Padding(3);
            this.tabPosition.Size = new System.Drawing.Size(631, 434);
            this.tabPosition.TabIndex = 0;
            this.tabPosition.Text = "Position";
            // 
            // trackBar
            // 
            this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar.BackColor = System.Drawing.SystemColors.Info;
            this.trackBar.Location = new System.Drawing.Point(594, -8);
            this.trackBar.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.trackBar.Maximum = 18;
            this.trackBar.Minimum = 2;
            this.trackBar.Name = "trackBar";
            this.trackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar.Size = new System.Drawing.Size(56, 150);
            this.trackBar.TabIndex = 25;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar.Value = 2;
            // 
            // AdressBox
            // 
            this.AdressBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AdressBox.ForeColor = System.Drawing.Color.Gray;
            this.AdressBox.Location = new System.Drawing.Point(9, 402);
            this.AdressBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.AdressBox.Name = "AdressBox";
            this.AdressBox.Size = new System.Drawing.Size(369, 27);
            this.AdressBox.TabIndex = 25;
            this.AdressBox.Text = "Enter an address or a coordinate";
            // 
            // FindAdressBtn
            // 
            this.FindAdressBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FindAdressBtn.Location = new System.Drawing.Point(384, 400);
            this.FindAdressBtn.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.FindAdressBtn.Name = "FindAdressBtn";
            this.FindAdressBtn.Size = new System.Drawing.Size(110, 30);
            this.FindAdressBtn.TabIndex = 25;
            this.FindAdressBtn.Text = "Find Location";
            this.FindAdressBtn.UseVisualStyleBackColor = true;
            // 
            // ResetLocationBtn
            // 
            this.ResetLocationBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetLocationBtn.Location = new System.Drawing.Point(500, 400);
            this.ResetLocationBtn.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ResetLocationBtn.Name = "ResetLocationBtn";
            this.ResetLocationBtn.Size = new System.Drawing.Size(125, 30);
            this.ResetLocationBtn.TabIndex = 26;
            this.ResetLocationBtn.Text = "Reset Location";
            this.ResetLocationBtn.UseVisualStyleBackColor = true;
            // 
            // gMapCtrl
            // 
            this.gMapCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gMapCtrl.BackColor = System.Drawing.SystemColors.Info;
            this.gMapCtrl.Bearing = 0F;
            this.gMapCtrl.CanDragMap = true;
            this.gMapCtrl.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapCtrl.GrayScaleMode = false;
            this.gMapCtrl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapCtrl.LevelsKeepInMemmory = 5;
            this.gMapCtrl.Location = new System.Drawing.Point(-4, 0);
            this.gMapCtrl.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gMapCtrl.MarkersEnabled = true;
            this.gMapCtrl.MaxZoom = 18;
            this.gMapCtrl.MinZoom = 2;
            this.gMapCtrl.MouseWheelZoomEnabled = true;
            this.gMapCtrl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            this.gMapCtrl.Name = "gMapCtrl";
            this.gMapCtrl.NegativeMode = false;
            this.gMapCtrl.PolygonsEnabled = true;
            this.gMapCtrl.RetryLoadTile = 0;
            this.gMapCtrl.RoutesEnabled = true;
            this.gMapCtrl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapCtrl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapCtrl.ShowTileGridLines = false;
            this.gMapCtrl.Size = new System.Drawing.Size(641, 434);
            this.gMapCtrl.TabIndex = 22;
            this.gMapCtrl.Zoom = 0D;
            // 
            // GoogleApiBox
            // 
            this.GoogleApiBox.Location = new System.Drawing.Point(140, 142);
            this.GoogleApiBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.GoogleApiBox.Name = "GoogleApiBox";
            this.GoogleApiBox.Size = new System.Drawing.Size(172, 27);
            this.GoogleApiBox.TabIndex = 33;
            // 
            // GoogleApiLabel
            // 
            this.GoogleApiLabel.AutoSize = true;
            this.GoogleApiLabel.Location = new System.Drawing.Point(6, 144);
            this.GoogleApiLabel.Name = "GoogleApiLabel";
            this.GoogleApiLabel.Size = new System.Drawing.Size(115, 20);
            this.GoogleApiLabel.TabIndex = 32;
            this.GoogleApiLabel.Text = "Google API Key:";
            // 
            // tbWalkingSpeed
            // 
            this.tbWalkingSpeed.Location = new System.Drawing.Point(176, 241);
            this.tbWalkingSpeed.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tbWalkingSpeed.Name = "tbWalkingSpeed";
            this.tbWalkingSpeed.Size = new System.Drawing.Size(136, 27);
            this.tbWalkingSpeed.TabIndex = 30;
            // 
            // TravelSpeedText
            // 
            this.TravelSpeedText.AutoSize = true;
            this.TravelSpeedText.Location = new System.Drawing.Point(6, 244);
            this.TravelSpeedText.Name = "TravelSpeedText";
            this.TravelSpeedText.Size = new System.Drawing.Size(164, 20);
            this.TravelSpeedText.TabIndex = 31;
            this.TravelSpeedText.Text = "Walking Speed (KM/H):";
            // 
            // cbLanguage
            // 
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Items.AddRange(new object[] {
            "Google",
            "PTC"});
            this.cbLanguage.Location = new System.Drawing.Point(140, 10);
            this.cbLanguage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(172, 28);
            this.cbLanguage.TabIndex = 29;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 12);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(77, 20);
            this.label26.TabIndex = 28;
            this.label26.Text = "Language:";
            // 
            // proxyGb
            // 
            this.proxyGb.Controls.Add(this.proxyPortTb);
            this.proxyGb.Controls.Add(this.proxyUserTb);
            this.proxyGb.Controls.Add(this.proxyPwTb);
            this.proxyGb.Controls.Add(this.label24);
            this.proxyGb.Controls.Add(this.label25);
            this.proxyGb.Controls.Add(this.proxyHostTb);
            this.proxyGb.Controls.Add(this.useProxyAuthCb);
            this.proxyGb.Controls.Add(this.label19);
            this.proxyGb.Controls.Add(this.useProxyCb);
            this.proxyGb.Controls.Add(this.label23);
            this.proxyGb.Location = new System.Drawing.Point(9, 272);
            this.proxyGb.Name = "proxyGb";
            this.proxyGb.Size = new System.Drawing.Size(315, 195);
            this.proxyGb.TabIndex = 27;
            this.proxyGb.TabStop = false;
            this.proxyGb.Text = "Proxy Setting";
            // 
            // proxyPortTb
            // 
            this.proxyPortTb.Location = new System.Drawing.Point(131, 76);
            this.proxyPortTb.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.proxyPortTb.Name = "proxyPortTb";
            this.proxyPortTb.Size = new System.Drawing.Size(172, 27);
            this.proxyPortTb.TabIndex = 36;
            // 
            // proxyUserTb
            // 
            this.proxyUserTb.Location = new System.Drawing.Point(131, 132);
            this.proxyUserTb.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.proxyUserTb.Name = "proxyUserTb";
            this.proxyUserTb.Size = new System.Drawing.Size(172, 27);
            this.proxyUserTb.TabIndex = 34;
            // 
            // proxyPwTb
            // 
            this.proxyPwTb.Location = new System.Drawing.Point(131, 163);
            this.proxyPwTb.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.proxyPwTb.Name = "proxyPwTb";
            this.proxyPwTb.PasswordChar = '*';
            this.proxyPwTb.Size = new System.Drawing.Size(172, 27);
            this.proxyPwTb.TabIndex = 35;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 135);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(78, 20);
            this.label24.TabIndex = 32;
            this.label24.Text = "Username:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 166);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(73, 20);
            this.label25.TabIndex = 33;
            this.label25.Text = "Password:";
            // 
            // proxyHostTb
            // 
            this.proxyHostTb.Location = new System.Drawing.Point(131, 45);
            this.proxyHostTb.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.proxyHostTb.Name = "proxyHostTb";
            this.proxyHostTb.Size = new System.Drawing.Size(172, 27);
            this.proxyHostTb.TabIndex = 30;
            // 
            // useProxyAuthCb
            // 
            this.useProxyAuthCb.AutoSize = true;
            this.useProxyAuthCb.Location = new System.Drawing.Point(6, 105);
            this.useProxyAuthCb.Name = "useProxyAuthCb";
            this.useProxyAuthCb.Size = new System.Drawing.Size(196, 24);
            this.useProxyAuthCb.TabIndex = 29;
            this.useProxyAuthCb.Text = "Use Proxy Authentication";
            this.useProxyAuthCb.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 48);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(43, 20);
            this.label19.TabIndex = 28;
            this.label19.Text = "Host:";
            // 
            // useProxyCb
            // 
            this.useProxyCb.AutoSize = true;
            this.useProxyCb.Location = new System.Drawing.Point(6, 22);
            this.useProxyCb.Name = "useProxyCb";
            this.useProxyCb.Size = new System.Drawing.Size(95, 24);
            this.useProxyCb.TabIndex = 30;
            this.useProxyCb.Text = "Use Proxy\r\n";
            this.useProxyCb.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 79);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(38, 20);
            this.label23.TabIndex = 29;
            this.label23.Text = "Port:";
            // 
            // UserLoginBox
            // 
            this.UserLoginBox.Location = new System.Drawing.Point(140, 76);
            this.UserLoginBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.UserLoginBox.Name = "UserLoginBox";
            this.UserLoginBox.Size = new System.Drawing.Size(172, 27);
            this.UserLoginBox.TabIndex = 11;
            // 
            // UserPasswordBox
            // 
            this.UserPasswordBox.Location = new System.Drawing.Point(140, 109);
            this.UserPasswordBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.UserPasswordBox.Name = "UserPasswordBox";
            this.UserPasswordBox.PasswordChar = '*';
            this.UserPasswordBox.Size = new System.Drawing.Size(172, 27);
            this.UserPasswordBox.TabIndex = 12;
            // 
            // tbLatitude
            // 
            this.tbLatitude.Location = new System.Drawing.Point(140, 174);
            this.tbLatitude.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tbLatitude.Name = "tbLatitude";
            this.tbLatitude.Size = new System.Drawing.Size(172, 27);
            this.tbLatitude.TabIndex = 13;
            // 
            // tbLongitude
            // 
            this.tbLongitude.Location = new System.Drawing.Point(140, 208);
            this.tbLongitude.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tbLongitude.Name = "tbLongitude";
            this.tbLongitude.Size = new System.Drawing.Size(172, 27);
            this.tbLongitude.TabIndex = 14;
            // 
            // authTypeLabel
            // 
            this.authTypeLabel.AutoSize = true;
            this.authTypeLabel.Location = new System.Drawing.Point(6, 45);
            this.authTypeLabel.Name = "authTypeLabel";
            this.authTypeLabel.Size = new System.Drawing.Size(84, 20);
            this.authTypeLabel.TabIndex = 0;
            this.authTypeLabel.Text = "Login Type:";
            // 
            // longiLabel
            // 
            this.longiLabel.AutoSize = true;
            this.longiLabel.Location = new System.Drawing.Point(6, 210);
            this.longiLabel.Name = "longiLabel";
            this.longiLabel.Size = new System.Drawing.Size(79, 20);
            this.longiLabel.TabIndex = 5;
            this.longiLabel.Text = "Longitude:";
            // 
            // authTypeCb
            // 
            this.authTypeCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.authTypeCb.FormattingEnabled = true;
            this.authTypeCb.Items.AddRange(new object[] {
            "Google",
            "PTC"});
            this.authTypeCb.Location = new System.Drawing.Point(140, 43);
            this.authTypeCb.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.authTypeCb.Name = "authTypeCb";
            this.authTypeCb.Size = new System.Drawing.Size(172, 28);
            this.authTypeCb.TabIndex = 1;
            // 
            // latLabel
            // 
            this.latLabel.AutoSize = true;
            this.latLabel.Location = new System.Drawing.Point(6, 177);
            this.latLabel.Name = "latLabel";
            this.latLabel.Size = new System.Drawing.Size(66, 20);
            this.latLabel.TabIndex = 4;
            this.latLabel.Text = "Latitude:";
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(6, 78);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(78, 20);
            this.UserLabel.TabIndex = 2;
            this.UserLabel.Text = "Username:";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(6, 111);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(73, 20);
            this.PasswordLabel.TabIndex = 3;
            this.PasswordLabel.Text = "Password:";
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBtn.Location = new System.Drawing.Point(561, 535);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(286, 30);
            this.saveBtn.TabIndex = 29;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 581);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.saveBtn);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PoGoEmulator Settings";
            this.tabControl.ResumeLayout(false);
            this.tabAuth.ResumeLayout(false);
            this.tabAuth.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPosition.ResumeLayout(false);
            this.tabPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.proxyGb.ResumeLayout(false);
            this.proxyGb.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabAuth;
        private System.Windows.Forms.TextBox tbWalkingSpeed;
        private System.Windows.Forms.Label TravelSpeedText;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.GroupBox proxyGb;
        private System.Windows.Forms.TextBox proxyPortTb;
        private System.Windows.Forms.TextBox proxyUserTb;
        private System.Windows.Forms.TextBox proxyPwTb;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox proxyHostTb;
        private System.Windows.Forms.CheckBox useProxyAuthCb;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox useProxyCb;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button ResetLocationBtn;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.TextBox AdressBox;
        private System.Windows.Forms.Button FindAdressBtn;
        private GMap.NET.WindowsForms.GMapControl gMapCtrl;
        private System.Windows.Forms.TextBox UserLoginBox;
        private System.Windows.Forms.TextBox UserPasswordBox;
        private System.Windows.Forms.TextBox tbLatitude;
        private System.Windows.Forms.TextBox tbLongitude;
        private System.Windows.Forms.Label authTypeLabel;
        private System.Windows.Forms.Label longiLabel;
        private System.Windows.Forms.ComboBox authTypeCb;
        private System.Windows.Forms.Label latLabel;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.TextBox GoogleApiBox;
        private System.Windows.Forms.Label GoogleApiLabel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPosition;
    }
}