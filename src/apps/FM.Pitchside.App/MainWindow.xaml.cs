using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FM.Pitchside.Core;

namespace FM.Pitchside.App
{
    public partial class MainWindow : Window
    {
        private FMCore? _core;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadButton.IsEnabled = false;
            StatusText.Text = "Loading...";

            try
            {
                var rows = await Task.Run(LoadPlayerRows);
                PlayersGrid.ItemsSource = rows;
                StatusText.Text = $"{rows.Count} player(s) loaded.";
            }
            catch (FMProcessNotFoundException ex)
            {
                StatusText.Text = ex.Message;
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error: {ex.Message}";
            }
            finally
            {
                LoadButton.IsEnabled = true;
            }
        }

        private List<PlayerRow> LoadPlayerRows()
        {
            _core ??= new FMCore();

            // Only attach/scan once - Fm27Game.FindPlayers() reuses its cached heap scan on its own,
            // so re-loading here would force an expensive full rescan on every click for nothing.
            if (_core.gameManager == null)
            {
                _core.LoadData();
            }

            var fm27 = _core.Fm27;
            if (fm27 != null)
            {
                return fm27.FindPlayers()
                    .Select(p => new PlayerRow
                    {
                        Name = p.DisplayName,
                        Club = string.IsNullOrEmpty(p.CurrentClubName) ? p.ClubName : p.CurrentClubName,
                        Position = string.Join("/", p.BestPositions),
                        Nation = p.Nation,
                        CurrentAbility = p.CurrentAbility,
                        PotentialAbility = p.PotentialAbility,
                    })
                    .ToList();
            }

            return _core.Players
                .Select(p => new PlayerRow
                {
                    Name = p.ActualPerson.VisibleName,
                    Club = p.Team?.Name ?? "",
                    Position = p.RoleRatings.PreferredPosition,
                    Nation = p.ActualPerson.Nation?.Name ?? "",
                    CurrentAbility = p.CA,
                    PotentialAbility = p.PA,
                })
                .ToList();
        }
    }

    public class PlayerRow
    {
        public string Name { get; set; } = "";
        public string Club { get; set; } = "";
        public string Position { get; set; } = "";
        public string Nation { get; set; } = "";
        public int CurrentAbility { get; set; }
        public int PotentialAbility { get; set; }
    }
}
