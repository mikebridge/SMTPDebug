using System;
using System.Xml.Serialization;
using DotNetOpenMail;

namespace SMTPDebug
{
	/// <summary>
	/// An AddressBook Entry
	/// </summary>
	[Serializable]
	[System.Xml.Serialization.XmlType("emailaddress")]
	public class Address
	{
		private String _email;
		private String _name;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Address()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public Address(String email, String name)
		{
			this._email=email;
			this._name=name;
		}

		/// <summary>
		/// The Email part
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("email")]
		public String Email
		{
			get {return _email;}
			set {_email=value;}
		}

		/// <summary>
		/// The Name part
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("name")]
		public String Name
		{
			get {return _name;}
			set {_name=value;}
		}

		public override string ToString()
		{
			if (_name==null || _name.Trim()=="") 
			{
				return _email;
			}
			else 
			{
				return _name+" <"+_email.Trim()+">";
			}
		}

		public EmailAddress CreateEmailAddress() 
		{
			if (_name==null || _name.Trim()=="") 
			{
				return new EmailAddress(_email);
			}
			else 
			{
				return new EmailAddress(_email, _name);
			}
		}

		/*
		public override bool Equals(object obj)
		{
			if (obj is Address) 
			{
				Address addr=(Address) obj;
				return addr.Name==this.Name && addr.Email==this.Email;
			}
			return false;
		}
		*/



	}
}
