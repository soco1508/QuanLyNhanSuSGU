namespace QLNS_SGU.View
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnHuyBo = new DevExpress.XtraEditors.SimpleButton();
            this.btnDangNhap = new DevExpress.XtraEditors.SimpleButton();
            this.chkBNhoMatKhau = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtMatKhau = new DevExpress.XtraEditors.TextEdit();
            this.txtTaiKhoan = new DevExpress.XtraEditors.TextEdit();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::QLNS_SGU.View.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkBNhoMatKhau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMatKhau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaiKhoan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2007 Blue";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnHuyBo);
            this.panelControl1.Controls.Add(this.btnDangNhap);
            this.panelControl1.Controls.Add(this.chkBNhoMatKhau);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtMatKhau);
            this.panelControl1.Controls.Add(this.txtTaiKhoan);
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(458, 211);
            this.panelControl1.TabIndex = 5;
            // 
            // btnHuyBo
            // 
            this.btnHuyBo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnHuyBo.Appearance.Options.UseForeColor = true;
            this.btnHuyBo.Location = new System.Drawing.Point(359, 163);
            this.btnHuyBo.Name = "btnHuyBo";
            this.btnHuyBo.Size = new System.Drawing.Size(75, 23);
            this.btnHuyBo.TabIndex = 5;
            this.btnHuyBo.Text = "Hủy bỏ";
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDangNhap.Appearance.Options.UseForeColor = true;
            this.btnDangNhap.Location = new System.Drawing.Point(278, 163);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(75, 23);
            this.btnDangNhap.TabIndex = 4;
            this.btnDangNhap.Text = "Đăng nhập";
            // 
            // chkBNhoMatKhau
            // 
            this.chkBNhoMatKhau.Location = new System.Drawing.Point(184, 134);
            this.chkBNhoMatKhau.Name = "chkBNhoMatKhau";
            this.chkBNhoMatKhau.Properties.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.chkBNhoMatKhau.Properties.Appearance.Options.UseForeColor = true;
            this.chkBNhoMatKhau.Properties.Caption = "Nhớ mật khẩu";
            this.chkBNhoMatKhau.Size = new System.Drawing.Size(104, 19);
            this.chkBNhoMatKhau.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(187, 104);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(54, 14);
            this.labelControl3.TabIndex = 17;
            this.labelControl3.Text = "Mật khẩu:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(184, 73);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(57, 14);
            this.labelControl2.TabIndex = 18;
            this.labelControl2.Text = "Tài khoản:";
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(259, 102);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic);
            this.txtMatKhau.Properties.Appearance.Options.UseFont = true;
            this.txtMatKhau.Properties.NullValuePrompt = "Nhập mật khẩu của bạn";
            this.txtMatKhau.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtMatKhau.Properties.PasswordChar = '*';
            this.txtMatKhau.Properties.ShowNullValuePromptWhenFocused = true;
            this.txtMatKhau.Properties.UseSystemPasswordChar = true;
            this.txtMatKhau.Size = new System.Drawing.Size(175, 20);
            this.txtMatKhau.TabIndex = 2;
            // 
            // txtTaiKhoan
            // 
            this.txtTaiKhoan.EditValue = "";
            this.txtTaiKhoan.Location = new System.Drawing.Point(259, 71);
            this.txtTaiKhoan.Name = "txtTaiKhoan";
            this.txtTaiKhoan.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtTaiKhoan.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic);
            this.txtTaiKhoan.Properties.Appearance.Options.UseFont = true;
            this.txtTaiKhoan.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTaiKhoan.Properties.NullValuePrompt = "Nhập tài khoản của bạn";
            this.txtTaiKhoan.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtTaiKhoan.Properties.ShowNullValuePromptWhenFocused = true;
            this.txtTaiKhoan.Size = new System.Drawing.Size(175, 20);
            this.txtTaiKhoan.TabIndex = 1;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureEdit1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(24, 61);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit1.Properties.ZoomAccelerationFactor = 1D;
            this.pictureEdit1.Size = new System.Drawing.Size(133, 125);
            this.pictureEdit1.TabIndex = 19;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 18F);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.labelControl1.Location = new System.Drawing.Point(41, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(116, 29);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "Đăng nhập";
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 211);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý nhân sự";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkBNhoMatKhau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMatKhau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaiKhoan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnHuyBo;
        private DevExpress.XtraEditors.SimpleButton btnDangNhap;
        private DevExpress.XtraEditors.CheckEdit chkBNhoMatKhau;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtMatKhau;
        private DevExpress.XtraEditors.TextEdit txtTaiKhoan;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}