using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace libirek.SQL
{
	public class Db
	{
		public SqlConnection Connection;
		public string Name;
		public Db(string username, string password, string serverurl, string database)
		{
			Name = database;
			Connection = new SqlConnection("user id=" + username + 
				";password=" + password + 
				";server=" + serverurl + 
				";Trusted_Connection=yes;Database=" + database + 
				";connection timeout=30");

		}
	}
}
