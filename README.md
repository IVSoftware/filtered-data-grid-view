# filtered-data-grid-view

What you could do is load up a `BindingList<Wrestler>` similar to how you did in your other [question](https://stackoverflow.com/a/77520144). But instead of binding this list of `AllWrestlers` directly to the `DataGridView`, make a second `BindingList<Wrestler>` called `FilteredWresters` and bind _it_ to the `DataGridView`.

When the `checkBoxFilterChampions` filter changes, run the `ApplyFilter()` method:

```csharp
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
```

___
**Wrestlers Class**

This class has been modified slightly to include a placeholder for the `Show Titles` column, and to hold an array of actual titles.

```csharp
class Wrestler
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // This will be a button column. Cell will be visible if wrestler has > 0 titles.
    public string? ShowTitles => null;

    public string[] Titles { get; set; } = new string[0];
    .
    .
    .
}
```

**MainForm.Load event**

- Swap out the auto-generated `ShowTitles` column for a button column.
- Handle `CellPainting` event to show the button only if a wrestler has one or more titles
- Handle the button clicked to pop up a list of titles.

```csharp
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
    checkBoxFilterChampions.CheckedChanged += (sender, e) =>
    {
        ApplyFilter();
    };
    ApplyFilter();
}
```
