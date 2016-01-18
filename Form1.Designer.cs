namespace VideoTest
{
  partial class Form1
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
      this.find = new System.Windows.Forms.Button();
      this.deviceName = new System.Windows.Forms.TextBox();
      this.stream = new System.Windows.Forms.Button();
      this.notifications = new System.Windows.Forms.TextBox();
      this.previewBox = new OpenTK.GLControl();
      this.frameCount = new System.Windows.Forms.TextBox();
      this.previewCount = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.writeRaw = new System.Windows.Forms.CheckBox();
      this.writeEncoded = new System.Windows.Forms.CheckBox();
      this.label3 = new System.Windows.Forms.Label();
      this.burn = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // find
      // 
      this.find.Location = new System.Drawing.Point(12, 36);
      this.find.Name = "find";
      this.find.Size = new System.Drawing.Size(75, 23);
      this.find.TabIndex = 0;
      this.find.Text = "Find";
      this.find.UseVisualStyleBackColor = true;
      this.find.Click += new System.EventHandler(this.find_Click);
      // 
      // deviceName
      // 
      this.deviceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.deviceName.Location = new System.Drawing.Point(93, 38);
      this.deviceName.Name = "deviceName";
      this.deviceName.Size = new System.Drawing.Size(457, 20);
      this.deviceName.TabIndex = 1;
      // 
      // stream
      // 
      this.stream.Enabled = false;
      this.stream.Location = new System.Drawing.Point(12, 65);
      this.stream.Name = "stream";
      this.stream.Size = new System.Drawing.Size(75, 23);
      this.stream.TabIndex = 2;
      this.stream.Text = "Stream";
      this.stream.UseVisualStyleBackColor = true;
      this.stream.Click += new System.EventHandler(this.stream_Click);
      // 
      // notifications
      // 
      this.notifications.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.notifications.Location = new System.Drawing.Point(12, 12);
      this.notifications.Name = "notifications";
      this.notifications.Size = new System.Drawing.Size(538, 20);
      this.notifications.TabIndex = 3;
      // 
      // previewBox
      // 
      this.previewBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.previewBox.BackColor = System.Drawing.Color.Black;
      this.previewBox.Location = new System.Drawing.Point(12, 123);
      this.previewBox.Name = "previewBox";
      this.previewBox.Size = new System.Drawing.Size(538, 397);
      this.previewBox.TabIndex = 4;
      this.previewBox.VSync = false;
      // 
      // frameCount
      // 
      this.frameCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.frameCount.Location = new System.Drawing.Point(408, 67);
      this.frameCount.Name = "frameCount";
      this.frameCount.ReadOnly = true;
      this.frameCount.Size = new System.Drawing.Size(49, 20);
      this.frameCount.TabIndex = 5;
      // 
      // previewCount
      // 
      this.previewCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.previewCount.Location = new System.Drawing.Point(501, 67);
      this.previewCount.Name = "previewCount";
      this.previewCount.ReadOnly = true;
      this.previewCount.Size = new System.Drawing.Size(49, 20);
      this.previewCount.TabIndex = 6;
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(361, 70);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(41, 13);
      this.label1.TabIndex = 7;
      this.label1.Text = "Frames";
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(463, 70);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(32, 13);
      this.label2.TabIndex = 8;
      this.label2.Text = "Prev.";
      // 
      // writeRaw
      // 
      this.writeRaw.AutoSize = true;
      this.writeRaw.Location = new System.Drawing.Point(138, 69);
      this.writeRaw.Name = "writeRaw";
      this.writeRaw.Size = new System.Drawing.Size(48, 17);
      this.writeRaw.TabIndex = 9;
      this.writeRaw.Text = "Raw";
      this.writeRaw.UseVisualStyleBackColor = true;
      // 
      // writeEncoded
      // 
      this.writeEncoded.AutoSize = true;
      this.writeEncoded.Location = new System.Drawing.Point(192, 69);
      this.writeEncoded.Name = "writeEncoded";
      this.writeEncoded.Size = new System.Drawing.Size(52, 17);
      this.writeEncoded.TabIndex = 10;
      this.writeEncoded.Text = "H264";
      this.writeEncoded.UseVisualStyleBackColor = true;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(93, 70);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(39, 13);
      this.label3.TabIndex = 11;
      this.label3.Text = "Output";
      // 
      // burn
      // 
      this.burn.Location = new System.Drawing.Point(12, 94);
      this.burn.Name = "burn";
      this.burn.Size = new System.Drawing.Size(75, 23);
      this.burn.TabIndex = 12;
      this.burn.Text = "Burn";
      this.burn.UseVisualStyleBackColor = true;
      this.burn.Click += new System.EventHandler(this.burn_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(562, 532);
      this.Controls.Add(this.burn);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.writeEncoded);
      this.Controls.Add(this.writeRaw);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.previewCount);
      this.Controls.Add(this.frameCount);
      this.Controls.Add(this.previewBox);
      this.Controls.Add(this.notifications);
      this.Controls.Add(this.stream);
      this.Controls.Add(this.deviceName);
      this.Controls.Add(this.find);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "Form1";
      this.Text = "Video";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button find;
    private System.Windows.Forms.TextBox deviceName;
    private System.Windows.Forms.Button stream;
    private System.Windows.Forms.TextBox notifications;
    private OpenTK.GLControl previewBox;
    private System.Windows.Forms.TextBox frameCount;
    private System.Windows.Forms.TextBox previewCount;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox writeRaw;
    private System.Windows.Forms.CheckBox writeEncoded;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button burn;
  }
}

