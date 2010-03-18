using System;
using System.Xml.Serialization;
using DotNetOpenMail;

namespace SMTPDebug
{
	/// <summary>
	/// Summary description for SmtpServerAddress.
	/// </summary>

	
	[Serializable]
	[System.Xml.Serialization.XmlType("smtpserver")]
	public class SmtpServerAddress
	{

		private String _hostname;
		private int _port;
				
		public SmtpServerAddress() 
		{

		}


		public SmtpServerAddress(String hostname, int port)
		{
			this._hostname=hostname;
			this._port=port;
		}

		[System.Xml.Serialization.XmlElementAttribute("hostname")]
		public String HostName 
		{
			get {return _hostname;}
			set {_hostname=value;}
		}

		[System.Xml.Serialization.XmlElementAttribute("port")]
		public int Port 
		{
			get {return _port;}
			set {_port=value;}
		}

		public override String ToString() 
		{
			return _hostname+":"+_port;
		}

		public SmtpServer CreateSmtpServer() 
		{
			return new SmtpServer(_hostname, _port);
		}

		public static SmtpServerAddress Parse(String str) 
		{
			if (str==null) 
			{
				throw new ApplicationException("Invalid Server: "+str);
			}
			int port=25;
			String host=str;
			int colonpos=str.IndexOf(":");
			if (colonpos > 0) 
			{	
				String portstr=str.Substring(colonpos+1);
				try 
				{
					port=Convert.ToInt32(portstr);
					host=str.Substring(0, colonpos);
				}
				catch 
				{
					throw new ApplicationException("Unable to parse the server string");
				}
			}
			return new SmtpServerAddress(host, port);
		}

	}
}
