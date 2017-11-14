using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using DataGridViewExample.DataObjects;
using DataGridViewExample.Images;
using Newtonsoft.Json;

namespace DataGridViewExample.Business
{
    public static class DataManager
    {
        /// <summary>
        /// Contains the path of the data file
        /// </summary>
        private static readonly string DataFilePath = Path.Combine(Functions.GetBaseFolder(), "DataList.json");
        /// <summary>
        /// Loads the movies
        /// </summary>
        /// <returns>The list of movies</returns>
        public static List<MovieModel> LoadMovies()
        {
            if (!File.Exists(DataFilePath))
                return new List<MovieModel>();

            var jsonString = File.ReadAllText(DataFilePath);

            var movieList = JsonConvert.DeserializeObject<List<MovieModel>>(jsonString);

            movieList.ForEach(entry => entry.RatingImage = GetRatingImage(entry.Rating));

            return movieList;
        }
        /// <summary>
        /// Gets the according rating image
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>The rating image</returns>
        public static Image GetRatingImage(int value)
        {
            switch (value)
            {
                case 1:
                    return ImageManager._1_5;
                case 2:
                    return ImageManager._2_5;
                case 3:
                    return ImageManager._3_5;
                case 4:
                    return ImageManager._4_5;
                case 5:
                    return ImageManager._5_5;
                default:
                    return ImageManager._3_5;
            }
        }
        /// <summary>
        /// Saves a list of <see cref="MovieModel"/> as JSON string into a file
        /// </summary>
        /// <param name="movieList">The movie list</param>
        /// <returns>true if successful, otherwise false</returns>
        public static bool SaveMovies(List<MovieModel> movieList)
        {
            var jsonString = JsonConvert.SerializeObject(movieList, Formatting.Indented);

            File.WriteAllText(DataFilePath, jsonString);

            return File.Exists(DataFilePath);
        }
    }
}
