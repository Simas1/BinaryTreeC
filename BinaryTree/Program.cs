using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace BinaryTree {
    class Program {
        static List<List<dynamic>> validCombinations = new List<List<dynamic>>();
        private static int[][] BinaryTree { get; set; }
        static void Main(string[] args) {
            const string defaultTree = @"
                215
                192 124
                117 269 442
                218 836 347 235
                320 805 522 417 345
                229 601 728 835 133 124
                248 202 277 433 207 263 257
                359 464 504 528 516 716 871 182
                461 441 426 656 863 560 380 171 923
                381 348 573 533 448 632 387 176 975 449
                223 711 445 645 245 543 931 532 937 541 444
                330 131 333 928 376 733 017 778 839 168 197 197
                131 171 522 137 217 224 291 413 528 520 227 229 928
                223 626 034 683 839 052 627 310 713 999 629 817 410 121
                924 622 911 233 325 139 721 218 253 223 107 233 230 124 233";

            BinaryTree = BuildBinaryTree(defaultTree);

            FindPaths(0, 0, new string[] { "even", "odd" }, new List<dynamic>());
            var allPaths = CalculateAndSort();

            Console.SetBufferSize(Console.BufferWidth, 3000);
            Console.WriteLine($"Found valid path(s): {allPaths.Count}");
            Console.WriteLine($"Max Sum: {allPaths[0].MaxSum}");
            Console.WriteLine($"Max Path: {allPaths[0].Numbers}");

            TreeColors pathColors = new TreeColors();
            PrintTree(allPaths[0], pathColors);
            Console.WriteLine();

            Console.WriteLine("Show ALL paths: ");
            foreach (var variant in allPaths) {
                PrintTree(variant, pathColors);
                Console.WriteLine();
            }

            Console.SetCursorPosition(0, 0);
            Console.ReadKey();
        }
        private static int[][] BuildBinaryTree(string tree) {
            string[] arrRows = tree.Split('\n');
            arrRows = arrRows.Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray();
            int[][] binaryTree = new int[arrRows.Length][];
            for (int row = 0; row < arrRows.Length; row++) {
                var arrLine = arrRows[row].Trim().Split(' ');
                int[] arrLineNr = arrLine.Select(int.Parse).ToArray();
                binaryTree[row] = arrLineNr;
            }
            return binaryTree;
        }

        private static string IsEvenOrOdd(int number) {
            int result = number % 2;
            if (result == 0) {
                return "even";
            } else {
                return "odd";
            }
        }

        private static string ToggleEvenOrOdd(string evenOrOdd) {
            if (evenOrOdd == "even") {
                return "odd";
            } else {
                return "even";
            }
        }

        private static void FindPaths(int col, int row, string[] toggle, List<dynamic> sequence) {
            int rowMax = BinaryTree.GetLength(0) - 1;
            int count = 0;
            int step = (BinaryTree[row].Length - 1 >= col + 1) ? 2 : 1;
            int[] line = BinaryTree[row].ToList().GetRange(col, step).ToArray();
            foreach (var item in line) {
                string polarity = IsEvenOrOdd(item);
                if (toggle.Contains(polarity)) {
                    //Console.WriteLine($"{item} - Row:{row} Col:{col} - {polarity}");
                    dynamic obj = new ExpandoObject();
                    obj.Number = item;
                    obj.Col = col + count;
                    var newSeq = new List<dynamic>(sequence);
                    newSeq.Add(obj);
                    if (rowMax == row) {
                        validCombinations.Add(newSeq);
                    }
                    if (rowMax >= row + 1) {
                        FindPaths(col + count, row + 1, new string[] { ToggleEvenOrOdd(polarity) }, newSeq);
                    }
                }
                count++;
            }
        }
        private static List<dynamic> CalculateAndSort() {
            List<dynamic> calculated = new List<dynamic>();
            foreach (var combination in validCombinations) {
                List<int> numbers = new List<int>();
                List<int> cols = new List<int>();
                foreach (var cell in combination) {
                    numbers.Add(cell.Number);
                    cols.Add(cell.Col);
                }
                dynamic obj = new ExpandoObject();
                obj.Numbers = String.Join(", ", numbers.ToArray());
                obj.Cols = cols;
                obj.MaxSum = numbers.Sum();
                calculated.Add(obj);
            }
            List<dynamic> sortedList = calculated.OrderByDescending(o => o.MaxSum).ToList();
            return sortedList;
        }

        private static void PrintTree(dynamic combination, TreeColors pathColors) {
            double maxCharLen = 0;
            foreach (var line in BinaryTree) {
                foreach (var number in line) {
                    maxCharLen = Math.Max(maxCharLen, number.ToString().Length);
                }
            }
            int step = (int)Math.Ceiling(maxCharLen / 2);
            int linePad = step * (BinaryTree.Length - 1);
            TreeColor color = pathColors.GetRandom();
            for (int r = 0; r <= BinaryTree.Length - 1; r++) {
                Console.Write(new string(' ', linePad));
                linePad = linePad - step;
                for (int c = 0; c <= BinaryTree[r].Length - 1; c++) {
                    if (combination.Cols[r] == c) {
                        Console.ForegroundColor = color.ForegroundColor;
                        Console.BackgroundColor = color.BackgroundColor;
                        Console.Write($"{BinaryTree[r][c].ToString().PadLeft((int)maxCharLen, '0')}");
                        Console.ResetColor();
                        Console.Write(" ");
                    } else {
                        Console.Write($"{BinaryTree[r][c].ToString().PadLeft((int)maxCharLen, '0')} ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
