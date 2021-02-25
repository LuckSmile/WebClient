using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WebClient.Layout
{
    public class GridLayoutGroup
    {
        public System.Windows.VerticalAlignment VerticalAlignment
        {
            get => this.panel.VerticalAlignment;
            set
            {
                this.panel.VerticalAlignment = value;
            }
        }
        public System.Windows.HorizontalAlignment HorizontalAlignment
        {
            get => horizontalAlignment;
            set
            {
                horizontalAlignment = value;
                for (int index = 0; index < panels.Count; index++)
                {
                    StackPanel panel = panels[index];
                    panel.HorizontalAlignment = horizontalAlignment;
                }
            }
        }
        public int Count => childs.Count;
        public Vector2 CellSize
        {
            get => cellSize;
            set
            {
                cellSize = value;
                for(int index = 0; index < panels.Count; index++)
                {
                    StackPanel panel = panels[index];
                    panel.Height = cellSize.y;
                    for (int indexChild = 0; indexChild < panel.Children.Count; indexChild++)
                    {
                        Grid grid = panel.Children[indexChild] as Grid;
                        grid.Height = cellSize.y;
                        grid.Width = cellSize.x;
                    }
                }
            }
        }
        public Vector2 Spacing
        {
            get => spacing;
            set
            {
                spacing = value;
                for (int index = 0; index < panels.Count; index++)
                {
                    StackPanel panel = panels[index];
                    panel.Margin = new Thickness(0, spacing.y, 0, 0);
                    for(int indexChild = 0; indexChild < panel.Children.Count; indexChild++)
                    {
                        (panel.Children[indexChild] as Grid).Margin = new Thickness(spacing.x / 2f, 0, spacing.x / 2f, 0);
                    }
                }
            }
        }
        public int Constraint
        {
            get => constraint;
            set
            {
                constraint = value;
           
                int count = Convert.ToInt32(Math.Ceiling(childs.Count / (float)constraint));
                panels.ForEach((x) => x.Children.Clear());

                if (count - panels.Count < 0)
                {

                    MessageBox.Show("Remove: " + (panels.Count - count));
                    for (int index = panels.Count - count; index > 0; index--)
                    {
                        this.RemovePanel();
                    }
                    MessageBox.Show("Panels: " + panels.Count + " " + panel.Children.Count);
                }
                else
                {
                    for(int index = count - panels.Count; index >= 0; index--)
                    {
                        this.CreatePanel();
                    }
                }


                StackPanel[] sort = panels.OrderBy(x => this.panel.Children.IndexOf(x)).ToArray();
                panels.Clear();
                panels.AddRange(sort);

                List<Grid> grids = new List<Grid>();
                grids.AddRange(childs);
                childs.Clear();

                for (int index = 0; index < grids.Count; index++)
                {
                    Grid grid = grids[index];
                    if (selected.Children.Count >= constraint)
                    {
                        for (int indexPanel = 0; indexPanel < panels.Count; indexPanel++)
                        {
                            StackPanel panel = panels[indexPanel];
                            if (panel.Children.Count < constraint)
                            {
                                selected = panel;
                                break;
                            }
                        }
                    }
                    this.Add(grid);
                }

                this.CellSize = cellSize;
                this.Spacing = spacing;


                MessageBox.Show(panels.Count + " " + panel.Children.Count);
            }
        }

        private Vector2 cellSize = Vector2.Zero;
        private Vector2 spacing = Vector2.Zero;

        private int constraint = 5;
        
        private System.Windows.HorizontalAlignment horizontalAlignment = System.Windows.HorizontalAlignment.Center;

        private StackPanel selected = null;

        private readonly StackPanel panel = null;
        private readonly List<Grid> childs = null;
        private readonly List<StackPanel> panels = null;
        public GridLayoutGroup(StackPanel panel, Vector2 cellSize, Vector2 spacing)
        {
            this.panel = panel;
            this.cellSize = cellSize;
            this.spacing = spacing;
            this.childs = new List<Grid>();
            this.panels = new List<StackPanel>();
            this.selected = CreatePanel();
        }
        public void Add(Grid grid)
        {
            if (selected.Children.Count >= constraint)
            {
                bool isSetSelected = false;
                for (int index = 0; index < panels.Count; index++)
                {
                    StackPanel panel = panels[index];
                    if (panel.Children.Count < constraint)
                    {
                        isSetSelected = true;
                        selected = panel;
                        break;
                    }
                }
                if (isSetSelected == false)
                    selected = CreatePanel();
            }

            grid.MinHeight = cellSize.y;
            grid.MinWidth = cellSize.x;

            grid.Margin = new System.Windows.Thickness(spacing.x / 2f, 0, spacing.x / 2f, 0);

            childs.Add(grid);
            selected.Children.Add(grid);
        }
        private StackPanel CreatePanel()
        {
            StackPanel answer = new StackPanel
            {
                Margin = new System.Windows.Thickness(0, spacing.y, 0, 0),
                Orientation = Orientation.Horizontal,
                MinHeight = cellSize.y,
                HorizontalAlignment = horizontalAlignment
            };
            
            panel.Children.Add(answer);
            panels.Add(answer);

            return answer;
        }
        private void RemovePanel()
        {
            this.panel.Children.RemoveAt(0);
            panels.RemoveAt(0);
        }
    }
}
