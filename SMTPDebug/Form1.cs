using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;

using DotNetOpenMail;
using DotNetOpenMail.Utils;

namespace SMTPDebug

{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private int _testNumber=1;

		#region Variables
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblSmtpServer;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtSubject;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ComboBox comboSmtpServer;
		private SmtpServerAddressCollection _smtpservers=new SmtpServerAddressCollection();
		private AddressBook _fromaddressbook;
		private System.Windows.Forms.ComboBox comboFromAddresses;
		private System.Windows.Forms.ComboBox comboToAddresses;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtHtmlContent;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtTextContent;
		private System.Windows.Forms.TextBox txtTestMessage;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TextBox txtRawEmail;
		private System.Windows.Forms.TextBox txtEnvelopeFrom;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnFromAddressBook;
		private System.Windows.Forms.Button btnToAddressBook;
		private System.Windows.Forms.Button btnSmtpServers;
		private AddressBook _toaddressbook;
		#endregion

		#region Form1
		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            try
            {
                UseDefaultData();
                txtTestMessage.Text = GetTestTextMessage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.StackTrace);
                
            }
		}
		#endregion

		#region Dispose
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSmtpServer = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.comboSmtpServer = new System.Windows.Forms.ComboBox();
            this.comboFromAddresses = new System.Windows.Forms.ComboBox();
            this.comboToAddresses = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtTestMessage = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHtmlContent = new System.Windows.Forms.TextBox();
            this.txtTextContent = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtRawEmail = new System.Windows.Forms.TextBox();
            this.btnFromAddressBook = new System.Windows.Forms.Button();
            this.txtEnvelopeFrom = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnToAddressBook = new System.Windows.Forms.Button();
            this.btnSmtpServers = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "From:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "To:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(-8, -72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Html Content:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblSmtpServer
            // 
            this.lblSmtpServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSmtpServer.Location = new System.Drawing.Point(280, 8);
            this.lblSmtpServer.Name = "lblSmtpServer";
            this.lblSmtpServer.Size = new System.Drawing.Size(80, 23);
            this.lblSmtpServer.TabIndex = 7;
            this.lblSmtpServer.Text = "SMTP Server:";
            this.lblSmtpServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSmtpServer.UseMnemonic = false;
            this.lblSmtpServer.Click += new System.EventHandler(this.lblSmtpServer_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOk.Location = new System.Drawing.Point(212, 528);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "SEND";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.Location = new System.Drawing.Point(292, 528);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Exit";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 23);
            this.label5.TabIndex = 12;
            this.label5.Text = "Subject:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(56, 56);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(504, 20);
            this.txtSubject.TabIndex = 13;
            this.txtSubject.TextChanged += new System.EventHandler(this.txtSubject_TextChanged);
            // 
            // comboSmtpServer
            // 
            this.comboSmtpServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboSmtpServer.Location = new System.Drawing.Point(360, 8);
            this.comboSmtpServer.Name = "comboSmtpServer";
            this.comboSmtpServer.Size = new System.Drawing.Size(152, 21);
            this.comboSmtpServer.TabIndex = 14;
            this.comboSmtpServer.SelectedIndexChanged += new System.EventHandler(this.comboSmtpServer_SelectedIndexChanged);
            // 
            // comboFromAddresses
            // 
            this.comboFromAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboFromAddresses.Location = new System.Drawing.Point(56, 8);
            this.comboFromAddresses.Name = "comboFromAddresses";
            this.comboFromAddresses.Size = new System.Drawing.Size(184, 21);
            this.comboFromAddresses.TabIndex = 15;
            this.comboFromAddresses.SelectedIndexChanged += new System.EventHandler(this.comboFromAddresses_SelectedIndexChanged);
            // 
            // comboToAddresses
            // 
            this.comboToAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboToAddresses.Location = new System.Drawing.Point(56, 32);
            this.comboToAddresses.Name = "comboToAddresses";
            this.comboToAddresses.Size = new System.Drawing.Size(184, 21);
            this.comboToAddresses.TabIndex = 16;
            this.comboToAddresses.SelectedIndexChanged += new System.EventHandler(this.comboToAddresses_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(8, 128);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(552, 392);
            this.tabControl1.TabIndex = 17;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtTestMessage);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(544, 366);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Test Message";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // txtTestMessage
            // 
            this.txtTestMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTestMessage.CausesValidation = false;
            this.txtTestMessage.Location = new System.Drawing.Point(0, 0);
            this.txtTestMessage.Multiline = true;
            this.txtTestMessage.Name = "txtTestMessage";
            this.txtTestMessage.ReadOnly = true;
            this.txtTestMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTestMessage.Size = new System.Drawing.Size(544, 368);
            this.txtTestMessage.TabIndex = 6;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.txtHtmlContent);
            this.tabPage2.Controls.Add(this.txtTextContent);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(544, 366);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "HTML/Text";
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "HTML Content:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.Location = new System.Drawing.Point(32, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 24);
            this.label4.TabIndex = 15;
            this.label4.Text = "Text Content:";
            // 
            // txtHtmlContent
            // 
            this.txtHtmlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHtmlContent.Location = new System.Drawing.Point(8, 32);
            this.txtHtmlContent.Multiline = true;
            this.txtHtmlContent.Name = "txtHtmlContent";
            this.txtHtmlContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtHtmlContent.Size = new System.Drawing.Size(528, 128);
            this.txtHtmlContent.TabIndex = 13;
            // 
            // txtTextContent
            // 
            this.txtTextContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTextContent.Location = new System.Drawing.Point(8, 192);
            this.txtTextContent.Multiline = true;
            this.txtTextContent.Name = "txtTextContent";
            this.txtTextContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTextContent.Size = new System.Drawing.Size(528, 168);
            this.txtTextContent.TabIndex = 14;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtRawEmail);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(544, 366);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Raw";
            // 
            // txtRawEmail
            // 
            this.txtRawEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRawEmail.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRawEmail.Location = new System.Drawing.Point(0, 0);
            this.txtRawEmail.MaxLength = 200000;
            this.txtRawEmail.Multiline = true;
            this.txtRawEmail.Name = "txtRawEmail";
            this.txtRawEmail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRawEmail.Size = new System.Drawing.Size(544, 368);
            this.txtRawEmail.TabIndex = 0;
            this.txtRawEmail.WordWrap = false;
            this.txtRawEmail.TextChanged += new System.EventHandler(this.txtRawEmail_TextChanged);
            // 
            // btnFromAddressBook
            // 
            this.btnFromAddressBook.Location = new System.Drawing.Point(240, 8);
            this.btnFromAddressBook.Name = "btnFromAddressBook";
            this.btnFromAddressBook.Size = new System.Drawing.Size(32, 23);
            this.btnFromAddressBook.TabIndex = 18;
            this.btnFromAddressBook.Text = "...";
            this.btnFromAddressBook.Click += new System.EventHandler(this.btnFromAddresses_Click);
            // 
            // txtEnvelopeFrom
            // 
            this.txtEnvelopeFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEnvelopeFrom.Location = new System.Drawing.Point(96, 96);
            this.txtEnvelopeFrom.Name = "txtEnvelopeFrom";
            this.txtEnvelopeFrom.Size = new System.Drawing.Size(464, 20);
            this.txtEnvelopeFrom.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Envelope From";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnToAddressBook
            // 
            this.btnToAddressBook.Location = new System.Drawing.Point(240, 32);
            this.btnToAddressBook.Name = "btnToAddressBook";
            this.btnToAddressBook.Size = new System.Drawing.Size(32, 23);
            this.btnToAddressBook.TabIndex = 19;
            this.btnToAddressBook.Text = "...";
            this.btnToAddressBook.Click += new System.EventHandler(this.btnToAddresses_Click);
            // 
            // btnSmtpServers
            // 
            this.btnSmtpServers.Location = new System.Drawing.Point(512, 8);
            this.btnSmtpServers.Name = "btnSmtpServers";
            this.btnSmtpServers.Size = new System.Drawing.Size(32, 24);
            this.btnSmtpServers.TabIndex = 20;
            this.btnSmtpServers.Text = "...";
            this.btnSmtpServers.Click += new System.EventHandler(this.btnSmtpServers_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(568, 558);
            this.Controls.Add(this.btnSmtpServers);
            this.Controls.Add(this.btnToAddressBook);
            this.Controls.Add(this.btnFromAddressBook);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.comboToAddresses);
            this.Controls.Add(this.comboFromAddresses);
            this.Controls.Add(this.comboSmtpServer);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblSmtpServer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEnvelopeFrom);
            this.Controls.Add(this.label7);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Main
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
		#endregion

		#region btnOk_Click
		private void btnOk_Click(object sender, System.EventArgs e)
		{
			//btnOk.Enabled=true;
			SendEmailInThread();
			//btnOk.Enabled=false;
		}
		#endregion

		#region UseDefaultData
		private void UseDefaultData() 
		{

			InitializeSmtpServers();
			InitializeAddressBooks();
			txtSubject.Text="TEST #${TEST_NUMBER}: ${SENDER_HOST} -> ${SMTP_HOST}";
			txtHtmlContent.Text="<html><body><p><b>Test</b> Server</p></body></html>";
			txtTextContent.Text="Testing Server";

		}
		#endregion

		private void InitializeSmtpServers() 
		{
			LoadSmtpServers();
			BindSmtpServers();
		}

		private void InitializeAddressBooks() 
		{
			LoadFromEmailAddresses();
			BindFromEmailAddresses();
			LoadToEmailAddresses();
			BindToEmailAddresses();
		}


		#region LoadFromEmailAddresses
		private void LoadFromEmailAddresses() 
		{	
			_fromaddressbook=AddressBook.Load();
			
			/*
			_fromaddresses=new EmailAddress[4];
			_fromaddresses[0]=new EmailAddress("mike@mymailout.com", "Mike Bridge");
			_fromaddresses[1]=new EmailAddress("mbridge@industrymailout.com", "Mike Bridge");
			_fromaddresses[2]=new EmailAddress("mike@advisormailout.com", "Mike Bridge");
			_fromaddresses[3]=new EmailAddress("mike@bridgecanada.com", "Mike Bridge");
			*/
		}
		#endregion

		#region LoadToEmailAddresses
		private void LoadToEmailAddresses() 
		{
			_toaddressbook=AddressBook.Load();
			
//			_toaddresses=new EmailAddress[5];
//			_toaddresses[0]=new EmailAddress("mike@mymailout.com", "Mike Bridge");
//			_toaddresses[1]=new EmailAddress("mbridge@industrymailout.com", "Mike Bridge");
//			_toaddresses[2]=new EmailAddress("mike@advisormailout.com", "Mike Bridge");
//			_toaddresses[3]=new EmailAddress("mike@bridgecanada.com", "Mike Bridge");
//			_toaddresses[4]=new EmailAddress("jonlars@shaw.ca", "Jon Larson");
		}
		#endregion

		#region BindFromEmailAddresses
		private void BindFromEmailAddresses() 
		{	
			comboFromAddresses.DataSource=_fromaddressbook;
			//comboFromAddresses.DisplayMember=
		}
		#endregion

		#region BindToEmailAddresses
		private void BindToEmailAddresses() 
		{	
			comboToAddresses.DataSource=_toaddressbook;
		}
		#endregion

		#region LoadSmtpServers
		private void LoadSmtpServers() 
		{
			//_smtpservers=new SmtpServerAddressCollection();
			//_smtpservers.Load(GetSmtpServerConfigFile());
			_smtpservers=SmtpServerAddressCollection.Load();

		}
		#endregion



		#region GetAddressBookConfigFile

        private String GetDefaultAddressBookConfigFile()
        {
            //return System.IO.Path.Combine(Application.StartupPath,"AddressBook.xml");
            return System.IO.Path.Combine(Application.StartupPath, "AddressBook.xml.orig");
        }

		private String GetAddressBookConfigFile() 
		{
			//return System.IO.Path.Combine(Application.StartupPath,"AddressBook.xml");
            return System.IO.Path.Combine(Application.UserAppDataPath, "AddressBook.xml");
		}
		#endregion

		#region BindSmtpServers
		private void BindSmtpServers() 
		{
			comboSmtpServer.DataSource=this._smtpservers;
		}
		#endregion

		#region GetCurrentlySelectedServer
		private SmtpServer GetCurrentlySelectedServer() 
		{			
			SmtpServerAddress smtpserveraddress=(SmtpServerAddress) comboSmtpServer.SelectedItem;
			if (smtpserveraddress==null) 
			{
				smtpserveraddress=SmtpServerAddress.Parse(comboSmtpServer.Text.Trim());
			}
			if (smtpserveraddress==null) 
			{
				SetError("The SMTP Server is not set.");
			}

			// Create a new instance (not a recycled smtpserver instance);
			SmtpServer smtpserver=smtpserveraddress.CreateSmtpServer();
			if (smtpserver==null) 
			{
				SetError("Invalid SMTP Server");
			}
			return smtpserver;
		}
		#endregion

		#region SendEmailInThread
		private void SendEmailInThread() 
		{
			EmailAddress fromaddress=GetFromEmailAddress();
			if (fromaddress==null && tabControl1.SelectedTab!=tabPage3) 
			{
				ShowErrorDialog("Can't parse the From Address");
				return;
			}
			EmailAddress[] toaddresses=GetToEmailAddresses();
			if (toaddresses.Length==0) 
			{
				ShowErrorDialog("Can't parse the To Address");
				return;
			}
			EmailAddress envelopefromaddress=GetEnvelopeFromAddress();

			try 
			{

				SmtpServer smtpserver=GetCurrentlySelectedServer();
				if (smtpserver==null) 
				{
					return;
				}
				LogWindow logwindow=new LogWindow();
				
				logwindow.Show();
				String subject=SubstituteVariables(txtSubject.Text, smtpserver);

				if (tabControl1.SelectedTab==tabPage2) 
				{

					SendThread thread=new SendThread(fromaddress, toaddresses, envelopefromaddress, subject, txtHtmlContent.Text, txtTextContent.Text, smtpserver, logwindow);
					_testNumber++;
					thread.Run();
				} 
				else if (tabControl1.SelectedTab==tabPage1)
				{

					String msg=SubstituteVariables(txtTestMessage.Text, smtpserver);
					SendThread thread=new SendThread(fromaddress, toaddresses, envelopefromaddress, subject, null, msg, smtpserver, logwindow);
					_testNumber++;
					thread.Run();

				} 
				else if (tabControl1.SelectedTab==tabPage3)
				{

					if (envelopefromaddress==null) 
					{
						ShowErrorDialog("Can't parse the Envelope-From Address");
						return;
					}

					//String msg=SubstituteVariables(txtTestMessage.Text, smtpserver);
					String msg=txtRawEmail.Text;
					SendRawThread thread=new SendRawThread(envelopefromaddress, toaddresses, msg, smtpserver, logwindow);
					_testNumber++;
					thread.Run();
				}


				
			}
			catch (Exception ex) 
			{
				SetError(ex.Message);
			}

		}
		#endregion

		#region GetTestTextMessage
		private String GetTestTextMessage() 
		{
			StringBuilder sb=new StringBuilder();

			sb.Append("========================================\r\n\r\n");
			sb.Append("This is test # ${TEST_NUMBER}\r\n");
			sb.Append("Origin:        ${SENDER_HOST}\r\n");
			sb.Append("SmtpServer:    ${SMTP_HOST}\r\n");
			sb.Append("Sent:          ${LOCAL_TIME}\r\n\r\n");			
			sb.Append("========================================\r\n\r\n");

			return sb.ToString();
															
		}
		#endregion

		#region SubstituteVariables
		private String SubstituteVariables(String str, SmtpServer smtpserver) 
		{
			str=str.Replace("${TEST_NUMBER}", _testNumber.ToString());
			str=str.Replace("${SENDER_HOST}", Dns.GetHostName());
			str=str.Replace("${SMTP_HOST}", smtpserver.ToString());
			str=str.Replace("${LOCAL_TIME}", DateTime.Now.ToString());

			return str;

		}
		#endregion

		#region SetError
		internal void SetError(String errormessage) 
		{
			MessageBox.Show (errormessage, "Error",
				MessageBoxButtons.OK, MessageBoxIcon.Error);

		}
		#endregion

		#region GetFromEmailAddress
		/// <summary>
		/// Return null if invalid.
		/// </summary>
		/// <returns></returns>
		private EmailAddress GetFromEmailAddress() 
		{
			Address address=(Address) comboFromAddresses.SelectedItem;
			if (address==null) 
			{
				EmailAddress emailaddress=(new EmailAddressParser()).ParseRawEmailAddress(comboFromAddresses.Text);
				return emailaddress;

			} 
			else 
			{
				return address.CreateEmailAddress();
			}
		}
		#endregion

		#region GetToEmailAddress
		/// <summary>
		/// Return null if invalid.
		/// </summary>
		/// <returns></returns>
		private EmailAddress GetToEmailAddress() 
		{
			Address address=(Address) comboToAddresses.SelectedItem;
			if (address==null) 
			{
				EmailAddress emailaddress=(new EmailAddressParser()).ParseRawEmailAddress(comboToAddresses.Text);
				return emailaddress;
			} 
			else 
			{
				return address.CreateEmailAddress();
			}
		}
		#endregion

		#region GetEnvelopeFromAddress
		/// <summary>
		/// return email address or null if none.
		/// </summary>
		/// <returns></returns>
		private EmailAddress GetEnvelopeFromAddress() 
		{
			if (txtEnvelopeFrom!=null && txtEnvelopeFrom.Text.Trim()!="") 
			{
				return ParseEmail(txtEnvelopeFrom.Text);
			} 
			else 
			{
				return null;
			}
			
		}
		#endregion

		#region ParseEmail
		private EmailAddress ParseEmail(String email) 
		{
			EmailAddressParser emailaddressparser=new EmailAddressParser();
			EmailAddress emailaddress=emailaddressparser.ParseRawEmailAddress(email);
			if (emailaddress==null) 
			{				
				return null;
			}
			else 
			{
				return emailaddress;
			}
		}
		#endregion

		#region ShowErrorDialog
		private void ShowErrorDialog(String errorMessage) 
		{
			MessageBox.Show(this, errorMessage, "SMTPDebug", MessageBoxButtons.OK, MessageBoxIcon.Error);
			
		}
		#endregion

		#region GetToEmailAddresses
		private EmailAddress[] GetToEmailAddresses() 
		{
			EmailAddress toaddress=GetToEmailAddress();
			if (toaddress==null) 
			{
				return new EmailAddress[0];
			}
			
			return new EmailAddress[1] {toaddress};
		}
		#endregion

		#region btnClose_Click
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}
		#endregion

		#region SaveSmtpServers
		private void SaveSmtpServers() 
		{
			this._smtpservers.Save();
		}
		#endregion

		#region Event Handlers
		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (tabControl1.SelectedTab==tabPage3) 
			{
				comboFromAddresses.Enabled=false;
				//comboToAddresses.Enabled=false;
				txtSubject.Enabled=false;
			} 
			else 
			{
				comboFromAddresses.Enabled=true;
				//comboToAddresses.Enabled=true;
				txtSubject.Enabled=true;
			}
		}

		private void tabPage2_Click(object sender, System.EventArgs e)
		{
		
		}

		private void txtTextContent_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void label4_Click(object sender, System.EventArgs e)
		{
		
		}

		private void label5_Click(object sender, System.EventArgs e)
		{
		
		}

		private void tabPage1_Click(object sender, System.EventArgs e)
		{
		
		}

		private void txtSubject_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void label2_Click(object sender, System.EventArgs e)
		{
		
		}

		private void comboSmtpServer_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void comboFromAddresses_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void comboToAddresses_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void label1_Click(object sender, System.EventArgs e)
		{
		
		}

		private void lblSmtpServer_Click(object sender, System.EventArgs e)
		{
		
		}

		private void txtHtmlContent_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void label3_Click(object sender, System.EventArgs e)
		{
		
		}


		private void txtRawEmail_TextChanged(object sender, System.EventArgs e)
		{
		
		}
		#endregion

		#region btnFromAddresses_Click
		private void btnFromAddresses_Click(object sender, System.EventArgs e)
		{
			AddressBookEditor addressbookeditor=new AddressBookEditor(GetAddressBookConfigFile());
			addressbookeditor.AddressSelected+=new SMTPDebug.AddressBookEditor.AddressSelectedHandler(addressbookeditor_FromAddressSelected);
			addressbookeditor.ShowDialog(this);

			InitializeAddressBooks();
		}
		#endregion

		private void btnToAddresses_Click(object sender, System.EventArgs e)
		{
			AddressBookEditor addressbookeditor=new AddressBookEditor(GetAddressBookConfigFile());
			addressbookeditor.AddressSelected+=new SMTPDebug.AddressBookEditor.AddressSelectedHandler(addressbookeditor_ToAddressSelected);
			addressbookeditor.ShowDialog(this);
			InitializeAddressBooks();

		}

		private void btnSmtpServers_Click(object sender, System.EventArgs e)
		{
			SmtpServerAddressCollectionEditor smtpservereditor=new SmtpServerAddressCollectionEditor(SmtpServerAddressCollection.GetSmtpServerConfigFile());
			smtpservereditor.ShowDialog(this);
			InitializeSmtpServers();
		}

		private void addressbookeditor_FromAddressSelected(Address address, bool needreload)
		{
			if (needreload) 
			{
				this.InitializeAddressBooks();
			}
			if (address!=null) 
			{
				comboFromAddresses.SelectedItem=address;
			}
			
		}

		private void addressbookeditor_ToAddressSelected(Address address, bool needreload)
		{
			if (needreload) 
			{
				this.InitializeAddressBooks();
			}
			if (address!=null) 
			{
				comboToAddresses.SelectedItem=address;
			}
		}
	}
}

