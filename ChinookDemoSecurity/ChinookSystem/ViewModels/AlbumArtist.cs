using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.ViewModels
{
    public class AlbumArtist
    {
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string ReleaseLabel { get; set; }
        public int AlbumId { get; set; }

        //this property is only in this view to allow for ODS review
        //   on filling a DropDownList on a GridView
        public int ArtistId { get; set; }

    }
}
