using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using System.Data;

namespace SMTPDebug
{
	/// <summary>
	/// Summary description for SmtpServerAddressCollectionEditor.
	/// </summary>
	public class SmtpServerAddressCollectionEditor : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private String _smtpserverfile=null;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SmtpServerAddressCollectionEditor(String smtpserverfile)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			_smtpserverfile=smtpserverfile;
			LoadSmtpServersInDataSet();
			
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		#region LoadSmtpServersInDataSet
		private void LoadSmtpServersInDataSet() 
		{

			DataSet ds=new DataSet();			
			ds.ReadXml(_smtpserverfile);
			//ds.Tables[0].Columns["email"].ColumnName="Email";
			//ds.Tables[0].Columns["name"].ColumnName="Name";
			DataGridTableStyle tablestyle=new DataGridTableStyle();
			tablestyle.MappingName="smtpserver";
			
			
			DataGridColumnStyle columnstyle1=new DataGridTextBoxColumn();			
			columnstyle1.MappingName="hostname";
			columnstyle1.Width=200;
			columnstyle1.HeaderText="Host";

			DataGridColumnStyle columnstyle2=new DataGridTextBoxColumn();			
			columnstyle2.MappingName="port";
			columnstyle2.Width=100;
			columnstyle2.HeaderText="Port";

			tablestyle.GridColumnStyles.Add(columnstyle1);
			tablestyle.GridColumnStyles.Add(columnstyle2);
			
			dataGrid1.TableStyles.Add(tablestyle);
			dataGrid1.DataSource=ds.Tables[0];
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
				if(components != null)
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
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Smtp Servers";
			// 
			// dataGrid1
			// 
			this.dataGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(8, 32);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(472, 368);
			this.dataGrid1.TabIndex = 1;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(168, 408);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(248, 408);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// SmtpServerAddressCollectionEditor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(480, 438);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.label1);
			this.Name = "SmtpServerAddressCollectionEditor";
			this.Text = "SmtpServerAddressCollectionEditor";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region SaveSmtpServersToFile
		private bool SaveSmtpServersToFile() 
		{
			DataTable dt=(DataTable) dataGrid1.DataSource;
			SmtpServerAddressCollection coll=new SmtpServerAddressCollection();

			try
			{

				foreach (DataRow row in dt.Rows) 
				{
					int port=25;
					if (!row.IsNull("port")) 
					{
						port=Convert.ToInt32(row["port"]);
					}					
					coll.Add(new SmtpServerAddress((String) row["hostname"], port));
				}
				coll.Save(_smtpserverfile);				
				return true;
			}			
			catch (Exception ex) 
			{
				ShowErrorDialog(ex.Message);
				return false;
			}
		}
		#endregion

		#region ShowErrorDialog
		private void ShowErrorDialog(String errorMessage) 
		{
			MessageBox.Show(this, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			
		}
		#endregion

		#region btnSave_Click
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			bool success=SaveSmtpServersToFile();
			if (success) 
			{
				this.Close();
			}
		}
		#endregion

		#region SmtpServerFile
		public String SmtpServerFile 
		{
			get 
			{
				return _smtpserverfile;
			}
			set 
			{
				_smtpserverfile=value;
			}
		}
		#endregion

		#region btnCancel_Click
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		#endregion

	}
}
