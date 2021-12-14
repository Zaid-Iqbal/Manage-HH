namespace Manage_HH
{
    partial class UI
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
            this.FullUpdateBtn = new System.Windows.Forms.Button();
            this.Label = new System.Windows.Forms.Label();
            this.UpdatePostsBtn = new System.Windows.Forms.Button();
            this.CleanPicsBtn = new System.Windows.Forms.Button();
            this.CleanLibraryBtn = new System.Windows.Forms.Button();
            this.AddBtn = new System.Windows.Forms.Button();
            this.PopulateSQLFromExcelBtn = new System.Windows.Forms.Button();
            this.PopulateSQLBtn = new System.Windows.Forms.Button();
            this.RemoveDuplicatesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FullUpdateBtn
            // 
            this.FullUpdateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FullUpdateBtn.Location = new System.Drawing.Point(12, 58);
            this.FullUpdateBtn.Name = "FullUpdateBtn";
            this.FullUpdateBtn.Size = new System.Drawing.Size(146, 35);
            this.FullUpdateBtn.TabIndex = 8;
            this.FullUpdateBtn.Text = "Full Update";
            this.FullUpdateBtn.UseVisualStyleBackColor = true;
            this.FullUpdateBtn.Click += new System.EventHandler(this.FullUpdateBtn_Click);
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label.Location = new System.Drawing.Point(12, 9);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(499, 46);
            this.Label.TabIndex = 9;
            this.Label.Text = "What would you like to do?\r\n";
            // 
            // UpdatePostsBtn
            // 
            this.UpdatePostsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdatePostsBtn.Location = new System.Drawing.Point(164, 58);
            this.UpdatePostsBtn.Name = "UpdatePostsBtn";
            this.UpdatePostsBtn.Size = new System.Drawing.Size(236, 35);
            this.UpdatePostsBtn.TabIndex = 10;
            this.UpdatePostsBtn.Text = "Update Existing Posts";
            this.UpdatePostsBtn.UseVisualStyleBackColor = true;
            this.UpdatePostsBtn.Click += new System.EventHandler(this.UpdatePostsBtn_Click);
            // 
            // CleanPicsBtn
            // 
            this.CleanPicsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CleanPicsBtn.Location = new System.Drawing.Point(12, 99);
            this.CleanPicsBtn.Name = "CleanPicsBtn";
            this.CleanPicsBtn.Size = new System.Drawing.Size(168, 35);
            this.CleanPicsBtn.TabIndex = 11;
            this.CleanPicsBtn.Text = "Clean Pic Folder";
            this.CleanPicsBtn.UseVisualStyleBackColor = true;
            this.CleanPicsBtn.Click += new System.EventHandler(this.CleanPicsBtn_Click);
            // 
            // CleanLibraryBtn
            // 
            this.CleanLibraryBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CleanLibraryBtn.Location = new System.Drawing.Point(186, 99);
            this.CleanLibraryBtn.Name = "CleanLibraryBtn";
            this.CleanLibraryBtn.Size = new System.Drawing.Size(156, 35);
            this.CleanLibraryBtn.TabIndex = 12;
            this.CleanLibraryBtn.Text = "Clean Media Library";
            this.CleanLibraryBtn.UseVisualStyleBackColor = true;
            this.CleanLibraryBtn.Click += new System.EventHandler(this.CleanLibraryBtn_Click);
            // 
            // AddBtn
            // 
            this.AddBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddBtn.Location = new System.Drawing.Point(406, 58);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(148, 35);
            this.AddBtn.TabIndex = 13;
            this.AddBtn.Text = "Add Posts";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // PopulateSQLFromExcelBtn
            // 
            this.PopulateSQLFromExcelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PopulateSQLFromExcelBtn.Location = new System.Drawing.Point(12, 140);
            this.PopulateSQLFromExcelBtn.Name = "PopulateSQLFromExcelBtn";
            this.PopulateSQLFromExcelBtn.Size = new System.Drawing.Size(291, 35);
            this.PopulateSQLFromExcelBtn.TabIndex = 14;
            this.PopulateSQLFromExcelBtn.Text = "Populate SQL NewProducts";
            this.PopulateSQLFromExcelBtn.UseVisualStyleBackColor = true;
            this.PopulateSQLFromExcelBtn.Click += new System.EventHandler(this.PopulateSQLFromExcelBtn_Click);
            // 
            // PopulateSQLBtn
            // 
            this.PopulateSQLBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PopulateSQLBtn.Location = new System.Drawing.Point(309, 139);
            this.PopulateSQLBtn.Name = "PopulateSQLBtn";
            this.PopulateSQLBtn.Size = new System.Drawing.Size(245, 35);
            this.PopulateSQLBtn.TabIndex = 15;
            this.PopulateSQLBtn.Text = "Populate SQL Products";
            this.PopulateSQLBtn.UseVisualStyleBackColor = true;
            this.PopulateSQLBtn.Click += new System.EventHandler(this.PopulateSQLBtn_Click);
            // 
            // RemoveDuplicatesButton
            // 
            this.RemoveDuplicatesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemoveDuplicatesButton.Location = new System.Drawing.Point(348, 99);
            this.RemoveDuplicatesButton.Name = "RemoveDuplicatesButton";
            this.RemoveDuplicatesButton.Size = new System.Drawing.Size(206, 35);
            this.RemoveDuplicatesButton.TabIndex = 16;
            this.RemoveDuplicatesButton.Text = "Remove Duplicates";
            this.RemoveDuplicatesButton.UseVisualStyleBackColor = true;
            this.RemoveDuplicatesButton.Click += new System.EventHandler(this.RemoveDuplicatesButton_Click);
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 186);
            this.Controls.Add(this.RemoveDuplicatesButton);
            this.Controls.Add(this.PopulateSQLBtn);
            this.Controls.Add(this.PopulateSQLFromExcelBtn);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.CleanLibraryBtn);
            this.Controls.Add(this.CleanPicsBtn);
            this.Controls.Add(this.UpdatePostsBtn);
            this.Controls.Add(this.Label);
            this.Controls.Add(this.FullUpdateBtn);
            this.Name = "UI";
            this.Text = "Hardware Hub Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FullUpdateBtn;
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.Button UpdatePostsBtn;
        private System.Windows.Forms.Button CleanPicsBtn;
        private System.Windows.Forms.Button CleanLibraryBtn;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.Button PopulateSQLFromExcelBtn;
        private System.Windows.Forms.Button PopulateSQLBtn;
        private System.Windows.Forms.Button RemoveDuplicatesButton;
    }
}