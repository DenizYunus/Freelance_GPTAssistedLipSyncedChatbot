namespace FL_GPTChatBotController
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
            customSpeakRadioButton = new RadioButton();
            activateSTTRadioButton = new RadioButton();
            groupBox1 = new GroupBox();
            button1 = new Button();
            textBox1 = new TextBox();
            languageSelectComboBox = new ComboBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // customSpeakRadioButton
            // 
            customSpeakRadioButton.AutoSize = true;
            customSpeakRadioButton.Checked = true;
            customSpeakRadioButton.Location = new Point(12, 12);
            customSpeakRadioButton.Name = "customSpeakRadioButton";
            customSpeakRadioButton.Size = new Size(108, 19);
            customSpeakRadioButton.TabIndex = 0;
            customSpeakRadioButton.TabStop = true;
            customSpeakRadioButton.Text = "Custom Speech";
            customSpeakRadioButton.UseVisualStyleBackColor = true;
            customSpeakRadioButton.CheckedChanged += customSpeakRadioButton_CheckedChanged;
            // 
            // activateSTTRadioButton
            // 
            activateSTTRadioButton.AutoSize = true;
            activateSTTRadioButton.Location = new Point(320, 12);
            activateSTTRadioButton.Name = "activateSTTRadioButton";
            activateSTTRadioButton.Size = new Size(95, 19);
            activateSTTRadioButton.TabIndex = 1;
            activateSTTRadioButton.TabStop = true;
            activateSTTRadioButton.Text = "Conversation";
            activateSTTRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(12, 53);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(302, 385);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(6, 356);
            button1.Name = "button1";
            button1.Size = new Size(290, 23);
            button1.TabIndex = 1;
            button1.Text = "Speak";
            button1.UseVisualStyleBackColor = true;
            button1.Click += SendTextToVoice;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(8, 14);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(288, 336);
            textBox1.TabIndex = 0;
            // 
            // languageSelectComboBox
            // 
            languageSelectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            languageSelectComboBox.Enabled = false;
            languageSelectComboBox.FormattingEnabled = true;
            languageSelectComboBox.Items.AddRange(new object[] { "en", "ro" });
            languageSelectComboBox.Location = new Point(320, 67);
            languageSelectComboBox.Name = "languageSelectComboBox";
            languageSelectComboBox.Size = new Size(137, 23);
            languageSelectComboBox.TabIndex = 3;
            languageSelectComboBox.SelectedIndexChanged += SetConversationLanguage;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(469, 450);
            Controls.Add(languageSelectComboBox);
            Controls.Add(groupBox1);
            Controls.Add(activateSTTRadioButton);
            Controls.Add(customSpeakRadioButton);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton customSpeakRadioButton;
        private RadioButton activateSTTRadioButton;
        private GroupBox groupBox1;
        private TextBox textBox1;
        private Button button1;
        private ComboBox languageSelectComboBox;
    }
}
