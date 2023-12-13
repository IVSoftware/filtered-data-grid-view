using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace filtered_data_grid_view
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent(); 
        
        string _filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            Assembly.GetEntryAssembly().GetName().Name,
            "roster.json"
            );
        BindingList<Wrestler> AllWrestlers { get; set; }
        BindingList<Wrestler> FilteredWrestlers { get; } = new BindingList<Wrestler>();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
#if false || REGEN

            File.Delete( _filePath );
#endif
            if (!File.Exists(_filePath))
            {
                makeNewFile();
            }
            var json = File.ReadAllText(_filePath);
            AllWrestlers = JsonConvert.DeserializeObject<BindingList<Wrestler>>(json);

            // Here's the difference from the ListView answer
            dataGridView.AllowUserToAddRows = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.DataSource = FilteredWrestlers;
            DataGridViewColumn columnToSwap = dataGridView.Columns[nameof(Wrestler.ShowTitles)];
            int swapIndex =columnToSwap.Index;
            var buttonColumn = new DataGridViewButtonColumn
            {
                Name = columnToSwap.Name,
                HeaderText = columnToSwap.HeaderText,
            };
            buttonColumn.DefaultCellStyle.NullValue = "Show All";
            // Style the buttons when painted.
            dataGridView.CellPainting += (sender, e) =>
            {
                if ((e.ColumnIndex != -1) && (e.RowIndex != -1))
                {
                    if (dataGridView[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell buttonCell)
                    {
                        buttonCell.FlatStyle = FlatStyle.Flat;
                        if (FilteredWrestlers[e.RowIndex].Titles.Any())
                        {
                            buttonCell.Style.BackColor = Color.FromArgb(0, 0, 192);
                            buttonCell.Style.ForeColor = Color.White;
                        }
                        else
                        {
                            e.Handled = true;
                            e.PaintBackground(e.ClipBounds, false);
                        }
                    }
                }
            };
            // Show the titles when clicked
            dataGridView.CellContentClick += (sender, e) =>
            {
                if ((e.RowIndex != -1) &&(dataGridView.Columns[nameof(Wrestler.ShowTitles)].Index == e.ColumnIndex))
                {
                    MessageBox.Show(string.Join(Environment.NewLine, FilteredWrestlers[e.RowIndex].Titles));
                }
            };
            dataGridView.Columns.Remove(columnToSwap);
            dataGridView.Columns.Insert(swapIndex, buttonColumn);
            foreach (DataGridViewColumn col in dataGridView.Columns)
            {
                col.HeaderCell.Style.Font = new Font(dataGridView.Font.FontFamily, 8f);
                switch (col.Name)
                {
                    case nameof(Wrestler.Name):
                        col.ReadOnly = true;
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        break;
                    case nameof(Wrestler.Description):
                        // Allow editing for Description
                        col.ReadOnly = false;
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        break;
                    default:
                        col.ReadOnly = true;
                        col.Width = 100;
                        break;
                }
            }
            // If the checbox changes, reapply the filter.
            checkBoxFilterChampions.CheckedChanged += (sender, e) =>
            {
                ApplyFilter();
            };
            // If the underlying list of all wrestlers changes, reapply the filter.
            AllWrestlers.ListChanged += (sender, e) => ApplyFilter();

            ApplyFilter();
        }

        private void ApplyFilter()
        {
            FilteredWrestlers.Clear();
            if(checkBoxFilterChampions.Checked) 
            {
                foreach (var wrestler in AllWrestlers.Where(_=>_.Titles.Any()))
                {
                    FilteredWrestlers.Add(wrestler);
                }
            }
            else
            {
                foreach (var wrestler in AllWrestlers)
                {
                    FilteredWrestlers.Add(wrestler);
                }
            }
        }

        private void makeNewFile()
        {
            var personsList = new List<Wrestler>    
            {
                new Wrestler
                {
                    Name = "The Rock",
                    Description = "He is the most electrifying name in sports entertainment.",
                    Titles = new string[]{ "WWE Heavyweight Champion (8x)", "WWF World Heavyweight Champion (3x)" },
                    InRingAbility = 80,
                    Charisma = 90,
                    Gender = "Male",
                    RingStyle = "Basic",
                    DrawValue = 100,
                    Condition = 80,
                    Weight = 220,
                    Morale = 100,
                    MicSkills = 100,
                },
                new Wrestler
                {
                    Name = "MJF",
                    Description = "First-ever World Middleweight Champion at the Battle Riot special.",
                    Gender = "Male",
                    Titles = new string[]{ "MLW World Middleweight Championship", "Rockstar Pro Tag Team Championship" }
                },
                new Wrestler
                {
                    Name = "David Flair",
                    Gender = "Male",
                },
                new Wrestler
                {
                    Name = "Charlotte Flair",
                    Gender = "Female",
                },
                new Wrestler
                {
                    Name = "Braden Putt",
                    Description = "Rising star who everyone is watching.",
                    Gender = "Male",
                },
            };
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath));
            var json = JsonConvert.SerializeObject(personsList, Formatting.Indented);
            File.WriteAllText(_filePath, json);
            System.Diagnostics.Process.Start("notepad.exe", _filePath);
        }
    }
    class Wrestler
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // This will be a button column. Cell will be visible if wrestler has > 0 titles.
        public string? ShowTitles => null;
        public string[] Titles { get; set; } = new string[0];

        [Description("In Ring Ability")]
        public int InRingAbility { get; set; }

        [Description("Draw Value")]
        public int DrawValue { get; set; }
        public int Charisma { get; set; }
        public int Condition { get; set; }
        public int Weight { get; set; }
        public string Gender { get; set; } = string.Empty;
        public int Morale { get; set; }

        [Description("Ring Style")]
        public string RingStyle { get; set; } = string.Empty;

        [Description("Mic Skills")]
        public int MicSkills { get; set; }
    }
}
