using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterAssignment.Entities
{
	public class GeneralDBEntity			//A general class which contains all the common attributes of all the DB Entities	
	{
		[Key]
		public long Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime LastModifiedOn { get; set; }
	}
}
