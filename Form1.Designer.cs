namespace ServerTimeSyncMaster
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            dgvServers = new DataGridView();
            colServerName = new DataGridViewTextBoxColumn();
            colCurrentTime = new DataGridViewTextBoxColumn();
            colTimeDiff = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            grpInput = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            txtAdminPass = new TextBox();
            txtAdminUser = new TextBox();
            btnScanAll = new Button();
            btnSyncSelected = new Button();
            rtbLog = new RichTextBox();
            label3 = new Label();
            groupBox1 = new GroupBox();
            grpLog = new GroupBox();
            btnScanSelected = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvServers).BeginInit();
            grpInput.SuspendLayout();
            groupBox1.SuspendLayout();
            grpLog.SuspendLayout();
            SuspendLayout();
            // 
            // dgvServers
            // 
            dgvServers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvServers.Columns.AddRange(new DataGridViewColumn[] { colServerName, colCurrentTime, colTimeDiff, colStatus });
            dgvServers.Location = new Point(42, 168);
            dgvServers.Name = "dgvServers";
            dgvServers.Size = new Size(597, 257);
            dgvServers.TabIndex = 0;
            // 
            // colServerName
            // 
            colServerName.HeaderText = "端末名 / IP";
            colServerName.Name = "colServerName";
            colServerName.Width = 125;
            // 
            // colCurrentTime
            // 
            colCurrentTime.HeaderText = "現在の時刻";
            colCurrentTime.Name = "colCurrentTime";
            colCurrentTime.Width = 150;
            // 
            // colTimeDiff
            // 
            colTimeDiff.HeaderText = "ズレ(秒)";
            colTimeDiff.Name = "colTimeDiff";
            // 
            // colStatus
            // 
            colStatus.HeaderText = "状態";
            colStatus.Name = "colStatus";
            // 
            // grpInput
            // 
            grpInput.Controls.Add(label2);
            grpInput.Controls.Add(label1);
            grpInput.Controls.Add(txtAdminPass);
            grpInput.Controls.Add(txtAdminUser);
            grpInput.Location = new Point(42, 26);
            grpInput.Name = "grpInput";
            grpInput.Size = new Size(210, 118);
            grpInput.TabIndex = 1;
            grpInput.TabStop = false;
            grpInput.Text = "管理者情報";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 74);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 3;
            label2.Text = "パスワード:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 36);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 2;
            label1.Text = "ユーザー名:";
            // 
            // txtAdminPass
            // 
            txtAdminPass.Location = new Point(70, 71);
            txtAdminPass.Name = "txtAdminPass";
            txtAdminPass.PasswordChar = '*';
            txtAdminPass.Size = new Size(124, 23);
            txtAdminPass.TabIndex = 1;
            // 
            // txtAdminUser
            // 
            txtAdminUser.Location = new Point(70, 33);
            txtAdminUser.Name = "txtAdminUser";
            txtAdminUser.Size = new Size(124, 23);
            txtAdminUser.TabIndex = 0;
            // 
            // btnScanAll
            // 
            btnScanAll.Location = new Point(301, 59);
            btnScanAll.Name = "btnScanAll";
            btnScanAll.Size = new Size(141, 23);
            btnScanAll.TabIndex = 2;
            btnScanAll.Text = "全端末をスキャン";
            btnScanAll.UseVisualStyleBackColor = true;
            btnScanAll.Click += btnScanAll_Click;
            // 
            // btnSyncSelected
            // 
            btnSyncSelected.Location = new Point(469, 100);
            btnSyncSelected.Name = "btnSyncSelected";
            btnSyncSelected.Size = new Size(137, 23);
            btnSyncSelected.TabIndex = 3;
            btnSyncSelected.Text = "選択した端末を同期";
            btnSyncSelected.UseVisualStyleBackColor = true;
            btnSyncSelected.Click += btnSyncSelected_Click;
            // 
            // rtbLog
            // 
            rtbLog.Location = new Point(6, 22);
            rtbLog.Name = "rtbLog";
            rtbLog.Size = new Size(585, 165);
            rtbLog.TabIndex = 4;
            rtbLog.Text = "";
            // 
            // label3
            // 
            label3.BackColor = SystemColors.Control;
            label3.Font = new Font("メイリオ", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label3.Location = new Point(6, 21);
            label3.Name = "label3";
            label3.Size = new Size(227, 436);
            label3.TabIndex = 6;
            label3.Text = resources.GetString("label3.Text");
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label3);
            groupBox1.Font = new Font("メイリオ", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            groupBox1.Location = new Point(662, 37);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(237, 461);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "【操作ガイド】";
            // 
            // grpLog
            // 
            grpLog.Controls.Add(rtbLog);
            grpLog.Location = new Point(42, 447);
            grpLog.Name = "grpLog";
            grpLog.Size = new Size(597, 193);
            grpLog.TabIndex = 8;
            grpLog.TabStop = false;
            grpLog.Text = "ログ";
            // 
            // btnScanSelected
            // 
            btnScanSelected.Location = new Point(301, 100);
            btnScanSelected.Name = "btnScanSelected";
            btnScanSelected.Size = new Size(141, 23);
            btnScanSelected.TabIndex = 9;
            btnScanSelected.Text = "選択した端末をスキャン";
            btnScanSelected.UseVisualStyleBackColor = true;
            btnScanSelected.Click += btnScanSelected_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(929, 652);
            Controls.Add(btnScanSelected);
            Controls.Add(grpLog);
            Controls.Add(groupBox1);
            Controls.Add(btnSyncSelected);
            Controls.Add(btnScanAll);
            Controls.Add(grpInput);
            Controls.Add(dgvServers);
            Name = "Form1";
            Text = "時刻管理ツール";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvServers).EndInit();
            grpInput.ResumeLayout(false);
            grpInput.PerformLayout();
            groupBox1.ResumeLayout(false);
            grpLog.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvServers;
        private GroupBox grpInput;
        private TextBox txtAdminPass;
        private TextBox txtAdminUser;
        private Button btnScanAll;
        private Button btnSyncSelected;
        private RichTextBox rtbLog;
        private Label label2;
        private Label label1;
        private Label label3;
        private GroupBox groupBox1;
        private GroupBox grpLog;
        private Button btnScanSelected;
        private DataGridViewTextBoxColumn colServerName;
        private DataGridViewTextBoxColumn colCurrentTime;
        private DataGridViewTextBoxColumn colTimeDiff;
        private DataGridViewTextBoxColumn colStatus;
    }
}
