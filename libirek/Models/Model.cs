using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace libirek.Models
{
	public class Model : DynamicObject
	{
		public Dictionary<string, object> dictionary = new Dictionary<string, object>();

		public int Count
		{
			get
			{
				return dictionary.Count;
			}
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			string name = binder.Name.ToLower();
			return dictionary.TryGetValue(name, out result);
		}

		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			dictionary[binder.Name.ToLower()] = value;

			return true;
		}
	}
}
