using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DataGridViewExample.Business;
using DataGridViewExample.DataObjects;

namespace DataGridViewExample
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Contains the movie list
        /// </summary>
        private List<MovieModel> _movieList;
        /// <summary>
        /// Creates a new instance of the form
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            _movieList = DataManager.LoadMovies();
            SetBindingSource(_movieList);
        }
        /// <summary>
        /// Saves the data
        /// </summary>
        private void Save()
        {
            if (bindingSource.DataSource is List<MovieModel> movieList)
            {
                if (DataManager.SaveMovies(movieList))
                    MessageBox.Show("Daten gespeichert.", "Speichern", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                else
                    MessageBox.Show("Daten konnten nicht gespeichert werden.", "Speichern", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Deletes the selected entry
        /// </summary>
        private void DeleteEntry()
        {
            if (bindingSource.Current is MovieModel movie)
            {
                if (MessageBox.Show($"Soll der Eintrag \"{movie.Title}\" gelöscht werden?", "Löschen",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bindingSource.RemoveCurrent();
                }
            }
        }
        /// <summary>
        /// Searchs for the given string in the list
        /// </summary>
        /// <param name="search">The search string</param>
        private void Search(string search)
        {
            SetBindingSource(string.IsNullOrEmpty(search)
                ? _movieList
                : _movieList.Where(w =>
                    w.Title.ToLower().Contains(search) || w.Genre.ToLower().Contains(search) ||
                    w.Type.ToLower().Contains(search)).ToList());
        }

        /// <summary>
        /// Sets the binding source
        /// </summary>
        private void SetBindingSource(List<MovieModel> movieList)
        {
            bindingSource.DataSource = null;
            bindingSource.DataSource = movieList;
        }

        /// <summary>
        /// Occurs when the form is loading
        /// </summary>
        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        /// <summary>
        /// Occurs when the user hits the save icon in the binding navigator
        /// </summary>
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            Save();
        }
        /// <summary>
        /// Occurs when the user hits the delete icon in the binding navigator
        /// </summary>
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DeleteEntry();
        }
        /// <summary>
        /// Occurs when the user hits a key while the text box in the binding navigator has the focus
        /// </summary>
        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search(textBoxSearch.Text);
        }
        /// <summary>
        /// Occurs when the value of a cell was changed
        /// </summary>
        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Nur auslösen wenn die "Bewertungsspalte" geändert wurde
            if (e.RowIndex == -1)
                return;

            if (e.ColumnIndex == columnRating.Index && dataGridView.Rows[e.RowIndex].DataBoundItem is MovieModel movie)
            {
                if (movie.Rating > 5)
                    movie.Rating = 5;
                else if (movie.Rating < 1)
                    movie.Rating = 1;

                movie.RatingImage = DataManager.GetRatingImage(movie.Rating);
            }
        }
    }
}
