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
    [Table("Albums")]
    internal class Album
    {
        private string _ReleaseLabel;

        [Key]
        public int AlbumId { get; set; }

        [Required(ErrorMessage ="Album Title is a required field")]
        [StringLength(160, ErrorMessage = "Album Title is limit to 160 characters")]
        public string Title { get; set; }

        //[ForeignKey] DO NOT USE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public int ArtistId { get; set; }

        public int ReleaseYear { get; set; }

        [StringLength(50, ErrorMessage = "Album Release Label is limit to 50 characters")]
        public string ReleaseLabel
        {
            get { return _ReleaseLabel; }
            set { _ReleaseLabel = string.IsNullOrEmpty(value) ? null : value; }
        }

        //navigational properties
        //An album can have zero, one or more Tracks
        //parent(singlar) to children (collection)
        public virtual ICollection<Track> Tracks { get; set; }
        //an album has 1 Artist
        public virtual Artist Artist { get; set; }
    }
}
