namespace MicroCoG
{
    partial class DemoForm
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
            this.SelectedAgent = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectedAction = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Results = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.query = new System.Windows.Forms.Label();
            this.SelectedTarget = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ShowPredicatesButton = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SelectedAgent
            // 
            this.SelectedAgent.FormattingEnabled = true;
            this.SelectedAgent.ItemHeight = 32;
            this.SelectedAgent.Location = new System.Drawing.Point(26, 137);
            this.SelectedAgent.Name = "SelectedAgent";
            this.SelectedAgent.Size = new System.Drawing.Size(240, 164);
            this.SelectedAgent.TabIndex = 0;
            this.SelectedAgent.SelectedIndexChanged += new System.EventHandler(this.SelectedCharacter_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Agent";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 320);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Action";
            // 
            // SelectedAction
            // 
            this.SelectedAction.FormattingEnabled = true;
            this.SelectedAction.ItemHeight = 32;
            this.SelectedAction.Location = new System.Drawing.Point(26, 355);
            this.SelectedAction.Name = "SelectedAction";
            this.SelectedAction.Size = new System.Drawing.Size(240, 164);
            this.SelectedAction.TabIndex = 3;
            this.SelectedAction.SelectedIndexChanged += new System.EventHandler(this.SelectedAction_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(325, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "Results";
            // 
            // Results
            // 
            this.Results.FormattingEnabled = true;
            this.Results.ItemHeight = 32;
            this.Results.Location = new System.Drawing.Point(325, 137);
            this.Results.Name = "Results";
            this.Results.Size = new System.Drawing.Size(1111, 612);
            this.Results.TabIndex = 5;
            this.Results.SelectedIndexChanged += new System.EventHandler(this.Results_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(26, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1410, 39);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // query
            // 
            this.query.AutoSize = true;
            this.query.Location = new System.Drawing.Point(26, 5);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(79, 32);
            this.query.TabIndex = 7;
            this.query.Text = "Query";
            // 
            // SelectedTarget
            // 
            this.SelectedTarget.FormattingEnabled = true;
            this.SelectedTarget.ItemHeight = 32;
            this.SelectedTarget.Location = new System.Drawing.Point(26, 574);
            this.SelectedTarget.Name = "SelectedTarget";
            this.SelectedTarget.Size = new System.Drawing.Size(240, 164);
            this.SelectedTarget.TabIndex = 9;
            this.SelectedTarget.SelectedIndexChanged += new System.EventHandler(this.SelectedTarget_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 539);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 32);
            this.label4.TabIndex = 8;
            this.label4.Text = "Target";
            // 
            // ShowPredicatesButton
            // 
            this.ShowPredicatesButton.Location = new System.Drawing.Point(26, 777);
            this.ShowPredicatesButton.Name = "ShowPredicatesButton";
            this.ShowPredicatesButton.Size = new System.Drawing.Size(240, 46);
            this.ShowPredicatesButton.TabIndex = 10;
            this.ShowPredicatesButton.Text = "Show predicates";
            this.ShowPredicatesButton.UseVisualStyleBackColor = true;
            this.ShowPredicatesButton.Click += new System.EventHandler(this.ShowPredicatesButton_Click);
            // 
            // helpButton
            // 
            this.helpButton.Location = new System.Drawing.Point(1286, 777);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(150, 46);
            this.helpButton.TabIndex = 11;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1478, 868);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.ShowPredicatesButton);
            this.Controls.Add(this.SelectedTarget);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.query);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Results);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SelectedAction);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SelectedAgent);
            this.Name = "DemoForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox SelectedAgent;
        private Label label1;
        private Label label2;
        private ListBox SelectedAction;
        private Label label3;
        private ListBox Results;
        private TextBox textBox1;
        private Label query;
        private ListBox SelectedTarget;
        private Label label4;
        private Button ShowPredicatesButton;
        private Button helpButton;
    }
}