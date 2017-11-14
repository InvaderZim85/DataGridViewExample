using System.Drawing;
using DataGridViewExample.Images;
using Newtonsoft.Json;

namespace DataGridViewExample.DataObjects
{
    public class MovieModel
    {
        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Gets or sets the rating
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets the rating image
        /// </summary>
        [JsonIgnore]
        public Image RatingImage { get; set; } = ImageManager._3_5;

        /// <summary>
        /// Gets or sets the media type
        /// </summary>
        public string Type { get; set; } = "";

        /// <summary>
        /// Gets or sets the genre
        /// </summary>
        public string Genre { get; set; } = "";
    }
}
