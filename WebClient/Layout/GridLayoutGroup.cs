using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
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
                    panel.MinHeight = cellSize.y;
                    for (int indexChild = 0; indexChild < panel.Children.Count; indexChild++)
                    {
                        Grid grid = panel.Children[indexChild] as Grid;
                        grid.MinHeight = cellSize.y;
                        grid.MinWidth = cellSize.x;
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
                int count = Convert.ToInt32(Math.Ceiling(childs.Count / (constraint * 1f)));

                for (int index = 0; index < panels.Count; index++)
                {
                    StackPanel panel = panels[index];
                    for (int indexChild = 0; indexChild < panel.Children.Count; indexChild++)
                    {
                        Grid grid = panel.Children[indexChild] as Grid;
                        panel.Children.Remove(grid);
                        indexChild--;
                    }
                }
                if (count > panels.Count)
                {
                    for (int index = 0; index < count - panels.Count; index++)
                        CreatePanel();
                }
                else
                {
                    for (int index = panels.Count - 1; index < panels.Count - count; index--)
                    {
                        RemovePanel(panels[index]);
                        index--;
                    }
                }

                List<Grid> grids = new List<Grid>();
                grids.AddRange(this.childs.ToArray());
                for(int index = 0; index < panels.Count; index++)
                {
                    if (grids.Count == 0)
                        break;
                    StackPanel panel = panels[index];
                    for(int indexChild = 0; indexChild < constraint; indexChild++)
                    {
                        Grid grid = grids[0];
                        panel.Children.Add(grid);
                        grids.RemoveAt(0);
                        if (grids.Count == 0)
                            break;
                    }
                }
                this.CellSize = cellSize;
                this.Spacing = spacing;
            }
        }

        private Vector2 cellSize = Vector2.Zero;
        private Vector2 spacing = Vector2.Zero;

        private int constraint = 2;
        
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
            grid.MinHeight = cellSize.y;
            grid.MinWidth = cellSize.x;

            grid.Margin = new System.Windows.Thickness(spacing.x / 2f, 0, spacing.x / 2f, 0);

            childs.Add(grid);
            selected.Children.Add(grid);
            
            if(selected.Children.Count >= constraint)
            {
                selected = CreatePanel();
            }
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
        private void RemovePanel(StackPanel panel)
        {
            panel.Children.Remove(panel);
            panels.Remove(panel);
            MessageBox.Show("" + panel.Children.Count);
        }
    }
}
