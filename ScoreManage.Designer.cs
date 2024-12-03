using System.Windows.Forms;

namespace StudentManage
{
    partial class ScoreManage
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgrScoreManage = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOke = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgrScoreManage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "Quản lý điểm";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgrScoreManage
            // 
            this.dgrScoreManage.AllowUserToAddRows = false;
            this.dgrScoreManage.AllowUserToDeleteRows = false;
            this.dgrScoreManage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrScoreManage.Location = new System.Drawing.Point(12, 56);
            this.dgrScoreManage.Name = "dgrScoreManage";
            this.dgrScoreManage.RowHeadersVisible = false;
            this.dgrScoreManage.Size = new System.Drawing.Size(466, 231);
            this.dgrScoreManage.TabIndex = 2;
            this.dgrScoreManage.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgrScoreManage_CellValidating);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(386, 314);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 34);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOke
            // 
            this.btnOke.Location = new System.Drawing.Point(290, 314);
            this.btnOke.Margin = new System.Windows.Forms.Padding(2);
            this.btnOke.Name = "btnOke";
            this.btnOke.Size = new System.Drawing.Size(92, 34);
            this.btnOke.TabIndex = 28;
            this.btnOke.Text = "Đồng ý";
            this.btnOke.UseVisualStyleBackColor = true;
            this.btnOke.Click += new System.EventHandler(this.btnOke_Click);
            // 
            // ScoreManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 359);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOke);
            this.Controls.Add(this.dgrScoreManage);
            this.Controls.Add(this.label1);
            this.Name = "ScoreManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ScoreManage";
            this.Load += new System.EventHandler(this.ScoreManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrScoreManage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgrScoreManage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOke;
    }
}