using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                for(int index = 0; index < childs.Count; index++)
                {
                    Grid grid = childs[index];
                    grid.MinHeight = cellSize.y;
                    grid.MinWidth = cellSize.x;
                }
                for(int index = 0; index < panels.Count; index++)
                {
                    StackPanel panel = panels[index];
                    panel.MinHeight = cellSize.y;
                }
            }
        }
        public Vector2 Spacing
        {
            get => spacing;
            set
            {
                spacing = value;
                for (int index = 0; index < childs.Count; index++)
                {
                    Grid grid = childs[index];
                    grid.Margin = new System.Windows.Thickness(spacing.x, 0, 0, 0);
                }
                for (int index = 0; index < panels.Count; index++)
                {
                    StackPanel panel = panels[index];
                    panel.Margin = new System.Windows.Thickness(0, spacing.y, 0, 0);
                }
            }
        }
        public int Constraint
        {
            get => constraint;
            set
            {
                constraint = value;
                
                int count = Convert.ToInt32(Math.Ceiling(childs.Count / constraint * 1f));
                
                if(count > panels.Count)
                {
                    for (int index = 0; index < count - panels.Count; index++)
                        CreatePanel();
                }
                else
                {
                    for (int index = 0; index < panels.Count - count; index++)
                        panel.Children.Remove(panels[index]);
                }

                for(int index = 0; index < panels.Count; index++)
                {
                    StackPanel panel = panels[index];
                    panel.Children.Clear();
                }
                
                for (int index = 0; index < childs.Count; index++)
                {
                    StackPanel panel = panels[index / constraint];
                    Grid grid = childs[index];
                    panel.Children.Add(grid);
                }
            }
        }

        private Vector2 cellSize = Vector2.Zero;
        private Vector2 spacing = Vector2.Zero;

        private int constraint = 1;
        
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
            
            if(selected.Children.Count > 0)
                grid.Margin = new System.Windows.Thickness(spacing.x, 0, 0, 0);

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
    }
}
