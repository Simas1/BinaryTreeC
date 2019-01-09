using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree {
    class TreeColor {
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }

        public TreeColor(ConsoleColor fgColor, ConsoleColor bgColor) {
            ForegroundColor = fgColor;
            BackgroundColor = bgColor;
        }
    }
}
