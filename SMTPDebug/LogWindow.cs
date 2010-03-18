using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using DotNetOpenMail.Logging;

namespace SMTPDebug
{
	/// <summary>
	/// Summary description for LogWindow.
	/// </summary>
	public class LogWindow : System.Windows.Forms.Form
	{
        delegate void AppendDetailsCallback(Color color, String text);

		private Object loggingLockObject=new Object();
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.RichTextBox richTextBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public LogWindow()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//textBoxLog.BackColor=Color.White;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnClose = new System.Windows.Forms.Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnClose.Location = new System.Drawing.Point(258, 468);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox1.CausesValidation = false;
			this.richTextBox1.DetectUrls = false;
			this.richTextBox1.Location = new System.Drawing.Point(8, 32);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(576, 424);
			this.richTextBox1.TabIndex = 2;
			this.richTextBox1.Text = "";
			this.richTextBox1.WordWrap = false;
			// 
			// LogWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(600, 510);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.btnClose);
			this.Name = "LogWindow";
			this.Text = "LogWindow";
			this.ResumeLayout(false);

		}
		#endregion

		#region btnClose_Click
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		#endregion


		public void LogSmtpWrite(LogMessage logmessage) 
		{
			LogMessage(Color.Green, "WRITE     >"+logmessage.Message);
		}

		public void LogSmtpReceive(LogMessage logmessage) 
		{
			LogMessage(Color.Blue, "RECEIVED  >"+logmessage.Message);
		}

		public void LogSmtpCompleted(LogMessage logmessage) 
		{
			LogMessage(Color.Purple, "COMPLETED >"+logmessage.Message);
		}

		public void LogInfo(String str) 
		{
			LogMessage(Color.Gray, str);
		}

		public void LogError(String str) 
		{
			LogMessage(Color.Red, "ERROR     >"+str);
		}

        #region LogMessage
        private void LogMessage(Color logcolor, string texttoappend)
        {

            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (richTextBox1.InvokeRequired)
            {

                AppendDetailsCallback d = LogMessage;
                this.Invoke(d, new object[] { logcolor, texttoappend });

            }
            else
            {

                lock (loggingLockObject)
                {
                    int requiredlength = richTextBox1.TextLength + texttoappend.Length;
                    if (requiredlength > richTextBox1.MaxLength)
                    {
                        if (texttoappend.Length > richTextBox1.MaxLength)
                        {
                            richTextBox1.ResetText();
                        }
                        else
                        {
                            while (texttoappend.Length + richTextBox1.Text.Length > richTextBox1.MaxLength && texttoappend.Length > 0)
                            {
                                int firstlineend = richTextBox1.Text.IndexOf("\n");
                                richTextBox1.Text = richTextBox1.Text.Substring(firstlineend + 1, richTextBox1.Text.Length - firstlineend - 1);
                            }
                        }
                    }
                    if (!richTextBox1.IsDisposed)
                    {
                        //richTextBox1.AppendText(texttoappend);
                        int currentSelectionStart = richTextBox1.SelectionStart;
                        int currentSelectionLength = richTextBox1.SelectionLength;
                        //richTextBox1.ForeColor=logcolor;
                        int start = richTextBox1.TextLength;
                        richTextBox1.AppendText(texttoappend);
                        int length = richTextBox1.TextLength;
                        richTextBox1.Select(start, length);
                        richTextBox1.SelectionColor = logcolor;

                        // select original text
                        if (currentSelectionStart >= 0)
                            richTextBox1.Select(currentSelectionStart,
                                currentSelectionLength);

                    }
                }
                //txtDetails.Text = texttoappend;

            }
        }
        #endregion





		#region LogMessageOLD
		private void LogMessageOLD(Color logcolor, String message) 
		{
			if (richTextBox1.IsDisposed) 
			{
				return;
			}
			
			String texttoappend=DateTime.Now.ToShortTimeString()+": "+message;
			
			lock( loggingLockObject ) 
			{

				int requiredlength= richTextBox1.TextLength +  texttoappend.Length;
				if (requiredlength > richTextBox1.MaxLength) 
				{
					if (texttoappend.Length > richTextBox1.MaxLength) 
					{
						richTextBox1.ResetText();
					} 
					else 
					{
						while (texttoappend.Length + richTextBox1.Text.Length > richTextBox1.MaxLength && texttoappend.Length>0) 
						{
							int firstlineend=richTextBox1.Text.IndexOf("\n");					
							richTextBox1.Text=richTextBox1.Text.Substring(firstlineend+1, richTextBox1.Text.Length-firstlineend-1);
						}
					}
				}
				if (!richTextBox1.IsDisposed) 
				{	

					int currentSelectionStart = richTextBox1.SelectionStart;
					int currentSelectionLength = richTextBox1.SelectionLength;
					//richTextBox1.ForeColor=logcolor;
					int start=richTextBox1.TextLength;
					richTextBox1.AppendText(texttoappend);
					int length=richTextBox1.TextLength;
					richTextBox1.Select(start, length);
					richTextBox1.SelectionColor=logcolor;
					
					// select original text
					if  (currentSelectionStart >=0)                
						richTextBox1.Select(currentSelectionStart,
							currentSelectionLength);

				}
			} // end critical section
		}
		#endregion

	

	}
}
