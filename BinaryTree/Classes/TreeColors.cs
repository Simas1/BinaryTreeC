using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree {
    class TreeColors {
        public List<TreeColor> PathColors { get; set; }
        private Random Number { get; set; }

        public TreeColors() {
            List<TreeColor> pathColors = new List<TreeColor>();
            var colors = Enum.GetValues(typeof(ConsoleColor));
            foreach (ConsoleColor bgColor in colors) {
                foreach (ConsoleColor fgColor in colors) {
                    if (bgColor != fgColor) {
                        pathColors.Add(new TreeColor(fgColor, bgColor));
                    }
                }
            }
            PathColors = pathColors;
            Number = new Random();
        }

        public TreeColor GetRandom() {
            int i = Number.Next(PathColors.Count);
            return PathColors[i];
        }
    }
}
