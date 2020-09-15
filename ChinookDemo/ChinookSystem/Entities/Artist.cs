using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace ChinookSystem.Entities
{
    //annotate your entity to
    //      link to the sql table
    //      indicate primary key and key type
    //      include validation on non-key fields

    [Table("Artists")]
    internal class Artist
    {
        //private data members
        private string _Name;

        //properties
        //each sql table attribute will be mapped within this entity definition
        //annotation may be needed so some of the properties
        //ANY property annotation MUST appear prior to the property

        //[Key] primary key
        //an additional option within this annotation is DatabaseGenerated()
        //by default if no DatabaseGenerate option is given, the primary key
        //  is assumed to be an Identiy sql primary key

        //Identity pKey: [Key] or
        //               [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        //user pkey: [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public string ISBN { get; set; }

        //there is a third option for DatabaseGenerated() and that is .Computed
        //this versionof the annotation is used for sql attribute which are computed fields.

        //Compound primary keys
        //the order of declared your primary compund key fields as properties MUST
        //  be in the order they exist on the sql table.
        //[Key, Column(Order=1)]
        //property
        //[Key, Column(Order=2)]
        //property
        //[Key, Column(Order=3)]
        //property

        [Key]
        public int ArtistId { get; set; }

        //[Required(ErrorMessage = "Name is require")]
        [StringLength(120,ErrorMessage ="Name is limited to 120 characters")]
        public string Name
        {
            get { return _Name; }
            set { _Name = string.IsNullOrEmpty(value) ? null : value; }
        }

        //constructors

        //behaviours
    }
}
