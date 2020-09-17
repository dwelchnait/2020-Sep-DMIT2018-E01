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
    [Table("Tracks")]
    internal class Track
    {
        private string _Composer;
        [Key]
        public int TrackId { get; set; }

        [Required(ErrorMessage ="Track Name is required")]
        [StringLength(200, ErrorMessage ="Track Name is limited to 200 characters")]
        public string Name { get; set; }

        public int? AlbumId { get; set; }

        public int MediaTypeId { get; set; }

        public int? GenreId { get; set; }

        [StringLength(220, ErrorMessage = "Track Composer is limited to 220 characters")]
        public string Composer
        {
            get { return _Composer; }
            set { _Composer = string.IsNullOrEmpty(value) ? null : value; }
        }

        public int Milliseconds { get; set; }

        public int? Bytes { get; set; }

        public decimal UnitPrice { get; set; }

        //navigational properties
        //virtually map the relationship of Table A to Table B
        //use to overlay a model of the database ERD Relationships
        //Tacks has a relationship to Albums, MediaTypes, Genres, InvoiceLines, PlaylistTracks
        //A Track has one parent (Album)
        //An Album has zero, one or more children (Tracks)
        //an entity may has bothe virtual properties for parent relationships and children relationships
        //Track and MediaTypes (child to parent)
        //Track and Genres (child to parent)
        //Track and InvoiceLines (parent to children)
        //Track and PlaylistTracks (parent to children)
        //
        public virtual Album Album { get; set; }
        public virtual MediaType MediaType { get; set; }

    }
}
