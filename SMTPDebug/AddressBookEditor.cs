using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using System.Data;

namespace SMTPDebug
{
	/// <summary>
	/// Summary description for AddressBookEditor.
	/// </summary>
	public class AddressBookEditor : System.Windows.Forms.Form
	{
		public delegate void AddressSelectedHandler(Address address, bool bookchanged); 
		
		public event AddressSelectedHandler AddressSelected;

		//public event AddressSelected(
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private String _addressbookfile;
		private System.Windows.Forms.DataGrid dataGrid1;

		#region AddressBookEditor
		public AddressBookEditor(String addressbookfile)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			_addressbookfile=addressbookfile;
			//LoadAddressesFromFile();

			//BindAddresses();
			LoadAddressesInDataSet();

			dataGrid1.DoubleClick+=new EventHandler(dataGrid1_DoubleClick);
			//dataGrid1.E
		}
		#endregion

		#region LoadAddressesInDataSet
		private void LoadAddressesInDataSet() 
		{

			DataSet ds=new DataSet();			
			ds.ReadXml(_addressbookfile);
			//ds.Tables[0].Columns["email"].ColumnName="Email";
			//ds.Tables[0].Columns["name"].ColumnName="Name";
			DataGridTableStyle tablestyle=new DataGridTableStyle();
			tablestyle.MappingName="emailaddress";
			
			
			DataGridColumnStyle columnstyle1=new DataGridTextBoxColumn();			
			columnstyle1.MappingName="email";
			columnstyle1.Width=200;
			columnstyle1.HeaderText="Email";

			DataGridColumnStyle columnstyle2=new DataGridTextBoxColumn();			
			columnstyle2.MappingName="name";
			columnstyle2.Width=200;
			columnstyle2.HeaderText="Name";

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

		#region SaveAddressesToFile
		private bool SaveAddressesToFile() 
		{
			DataTable dt=(DataTable) dataGrid1.DataSource;
			AddressBook addressbook=new AddressBook();

			try 
			{

				foreach (DataRow row in dt.Rows) 
				{
					addressbook.Add(new Address((String) row["email"], (String) row["name"]));
				}

				addressbook.Save(AddressBookFile);
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

		#region AddressBookFile
		public String AddressBookFile 
		{
			get 
			{
				return _addressbookfile;
			}
			set 
			{
				_addressbookfile=value;
			}
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Address Book";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(184, 408);
			this.button1.Name = "button1";
			this.button1.TabIndex = 3;
			this.button1.Text = "Save";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(264, 408);
			this.button2.Name = "button2";
			this.button2.TabIndex = 4;
			this.button2.Text = "Cancel";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// dataGrid1
			// 
			this.dataGrid1.AlternatingBackColor = System.Drawing.SystemColors.Window;
			this.dataGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGrid1.BackgroundColor = System.Drawing.Color.LightGray;
			this.dataGrid1.DataMember = "";
			this.dataGrid1.GridLineColor = System.Drawing.SystemColors.Control;
			this.dataGrid1.HeaderBackColor = System.Drawing.SystemColors.Control;
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.dataGrid1.Location = new System.Drawing.Point(8, 40);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.dataGrid1.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dataGrid1.Size = new System.Drawing.Size(512, 360);
			this.dataGrid1.TabIndex = 2;
			// 
			// AddressBookEditor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(528, 438);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.label1);
			this.Name = "AddressBookEditor";
			this.Text = "AddressBookEditor";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			bool success=SaveAddressesToFile();
			if (success) 
			{
				this.Close();
			}
		}

		private void dataGrid1_DoubleClick(object sender, EventArgs e)
		{
			OnAddressSelected(this, "Clicked");
		}

		#region OnAddressSelected
		internal void OnAddressSelected(Object sender, String message) 
		{
			if (AddressSelected!=null) 
			{
				int index=dataGrid1.CurrentRowIndex;
				if (index >= 0) 
				{
					DataTable dt=(DataTable) dataGrid1.DataSource;
					DataRow datarow=dt.Rows[index];
					String name=null;
					if (!datarow.IsNull("name")) 
					{
						name=(String) datarow["name"];
					}
					AddressSelected(new Address((String) datarow["email"], name), true);
					this.Close();
				}
			}
			
		}
		#endregion

	}
}
