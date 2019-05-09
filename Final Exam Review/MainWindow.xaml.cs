//Marissa Munoz
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Final_Exam_Review
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ClearAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            List<Movie> movies = new List<Movie>();

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(@"http://pcbstuou.w27.wh-2.com/webservices/3033/api/Movies?number=100").Result;
                var content = response.Content.ReadAsStringAsync().Result;
                movies = JsonConvert.DeserializeObject<List<Movie>>(content);
            }
            //list the genres
            Dictionary<string, int> genres = new Dictionary<string, int>();
            foreach (var movie in movies)
            {
                if (movie.genres.Contains("|"))
                {
                    var gs = movie.genres.Split('|');
                    foreach (var g in gs)
                    {
                        if (genres.ContainsKey(g))
                        {
                            genres[g] = genres[g] + 1;
                        }
                        else
                        {
                            genres.Add(g, 1);
                        }
                        //lstGenres.Items.Add(g);
                    }
                }
                else
                {
                    if (genres.ContainsKey(movie.genres))
                    {
                        genres[movie.genres] = genres[movie.genres] + 1;
                    }
                    else
                    {
                        genres.Add(movie.genres, 1);
                    }
                    //lstGenres.Items.Add(movie.genres);
                }
            }

            foreach (var key in genres.Keys)
            {
                lstGenres.Items.Add($"{key}({genres[key].ToString("N0")})");
            }



            //list highest imdb score
            List<Movie> highestIMDBScores = new List<Movie>();
            foreach (var movie in movies)
            {
                if (highestIMDBScores.Count < 1)
                {
                    highestIMDBScores.Add(movie);
                    continue;
                }
                else
                {
                    if (highestIMDBScores[0].imdb_score < movie.imdb_score) // we have new highest imdb_score
                    {
                        highestIMDBScores.Clear();
                        highestIMDBScores.Add(movie);
                    }
                    else if (highestIMDBScores[0].imdb_score == movie.imdb_score) // have the same score, so add this movie to the list
                    {
                        highestIMDBScores.Add(movie);
                    }
                    else// the current instance of Movie (movie)
                    {
                        // don't need to add to the list, or clear the list
                    }
                }
            }
            if (highestIMDBScores.Count() > 1)
            {
                string content = "";
                foreach (var m in highestIMDBScores)
                {
                    content += m.movie_title + '\n';
                }
                txtHighestIMBDScore.Text = content;

            }
            else
            {
                Hyperlink h = new Hyperlink();
                h.NavigateUri = new Uri(highestIMDBScores[0].movie_imdb_link);
                h.Inlines.Add(highestIMDBScores[0].movie_title);
                h.RequestNavigate += LinkOnRequestNavigate;

                txtHighestIMBDScore.Inlines.Add(h);
            }

            //  txtHighestIMBDScore.Text = highestIMBDScores.Count().ToString("N0");

            //list all different movies with 350000
            foreach (var movie in movies)
            {
              if(movie.num_voted_users >= 350000)
                {
                    Hyperlink link = new Hyperlink();
                    link.NavigateUri = new Uri(movie.movie_imdb_link);
                    link.Inlines.Add(movie.movie_title);
                    link.RequestNavigate += LinkOnRequestNavigate;

                    lstVotedUsers.Items.Add(link);
                }
            }
            foreach (var movie in movies)
            {
                int counterRusso = 0;
                if(movie.director_name == "Anthony Russo")
                {
                    counterRusso++;
                    txtAnthonyRusso.Text = counterRusso.ToString("N0");
                }
                else
                {
                    //do nothing
                }
                
            }

            foreach (var movie in movies)
            {
                int counterDowney = 0;
                if (movie.actor_1_name == "Robert Downey Jr.")
                {
                    counterDowney++;
                    txtRobertDowney.Text = counterDowney.ToString("N0");
                }
                else
                {
                    //do nothing
                }
            }
        }

        private void LinkOnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        private void ClearAll()
        {
            txtAnthonyRusso.Inlines.Clear();
            txtHighestIMBDScore.Inlines.Clear();
            txtRobertDowney.Inlines.Clear();
            lstVotedUsers.Items.Clear();
            lstGenres.Items.Clear();
        }
    }
}
