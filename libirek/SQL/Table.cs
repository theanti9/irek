using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libirek.SQL
{
	public class Table
	{
		public List<Column> TblColumns;

		public Table(string tblname, Db database, List<Column> columns)
		{
			TblColumns = columns;

		}

		public Table(string tblname, Db database)
		{

		}
	}
}
