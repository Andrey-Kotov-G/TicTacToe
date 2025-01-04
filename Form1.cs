namespace TicTacToe
{
    public partial class Form1 : Form
    {
        private int difficultModeBot = 0;
        private bool botPart = false;

        private int userMoveType = 0;
        private int botMoveType = 1;

        private readonly int?[,] listOfFieldValues = new int?[3, 3];

        public Form1()
        {
            InitializeComponent();
        }

        private List<CellInfo> GetCells()
        {
            List<CellInfo> cells = Enumerable.Range(0, listOfFieldValues.GetLength(0))
                .SelectMany(row => Enumerable.Range(0, listOfFieldValues.GetLength(1))
                    .Select(index => new CellInfo { Row = row, Index = index, Value = listOfFieldValues[row, index] }))
                .ToList();

            return cells;
        }

        private List<CellInfo> GetNullCells()
        {
            List<CellInfo> nullCells = GetCells().Where(cell => cell.Value == null).ToList();

            return nullCells;
        }

        private int? CheckForWinner()
        {
            for (int rowY = 0; rowY < listOfFieldValues.GetLength(0); rowY++)
            {
                if (listOfFieldValues[rowY, 0] == userMoveType && listOfFieldValues[rowY, 1] == userMoveType && listOfFieldValues[rowY, 2] == userMoveType) return 0;
                if (listOfFieldValues[rowY, 0] == botMoveType && listOfFieldValues[rowY, 1] == botMoveType && listOfFieldValues[rowY, 2] == botMoveType) return 1;
            }

            for (int rowX = 0; rowX < listOfFieldValues.GetLength(1); rowX++)
            {
                if (listOfFieldValues[0, rowX] == userMoveType && listOfFieldValues[1, rowX] == userMoveType && listOfFieldValues[2, rowX] == userMoveType) return 0;
                if (listOfFieldValues[0, rowX] == botMoveType && listOfFieldValues[1, rowX] == botMoveType && listOfFieldValues[2, rowX] == botMoveType) return 1;
            }

            if (listOfFieldValues[0, 0] == userMoveType && listOfFieldValues[1, 1] == userMoveType && listOfFieldValues[2, 2] == userMoveType) return 0;
            if (listOfFieldValues[0, 0] == botMoveType && listOfFieldValues[1, 1] == botMoveType && listOfFieldValues[2, 2] == botMoveType) return 1;

            if (listOfFieldValues[2, 0] == userMoveType && listOfFieldValues[1, 1] == userMoveType && listOfFieldValues[0, 2] == userMoveType) return 0;
            if (listOfFieldValues[2, 0] == botMoveType && listOfFieldValues[1, 1] == botMoveType && listOfFieldValues[0, 2] == botMoveType) return 1;

            if (GetNullCells().Count == 0)
                return -1;

            return null;
        }

        private PresumedWinner? CheckForWinner(CellInfo cellInfo)
        {
            for (int rowY = 0; rowY < listOfFieldValues.GetLength(0); rowY++)
            {
                if (listOfFieldValues[rowY, 0] == null && listOfFieldValues[rowY, 1] == cellInfo.Value && listOfFieldValues[rowY, 2] == cellInfo.Value) return new PresumedWinner { Row = rowY, Index = 0 };
                if (listOfFieldValues[rowY, 0] == cellInfo.Value && listOfFieldValues[rowY, 1] == null && listOfFieldValues[rowY, 2] == cellInfo.Value) return new PresumedWinner { Row = rowY, Index = 1 };
                if (listOfFieldValues[rowY, 0] == cellInfo.Value && listOfFieldValues[rowY, 1] == cellInfo.Value && listOfFieldValues[rowY, 2] == null) return new PresumedWinner { Row = rowY, Index = 2 };
            }

            for (int rowX = 0; rowX < listOfFieldValues.GetLength(1); rowX++)
            {
                if (listOfFieldValues[0, rowX] == null && listOfFieldValues[1, rowX] == cellInfo.Value && listOfFieldValues[2, rowX] == cellInfo.Value) return new PresumedWinner { Row = 0, Index = rowX };
                if (listOfFieldValues[0, rowX] == cellInfo.Value && listOfFieldValues[1, rowX] == null && listOfFieldValues[2, rowX] == cellInfo.Value) return new PresumedWinner { Row = 1, Index = rowX };
                if (listOfFieldValues[0, rowX] == cellInfo.Value && listOfFieldValues[1, rowX] == cellInfo.Value && listOfFieldValues[2, rowX] == null) return new PresumedWinner { Row = 2, Index = rowX };
            }

            if (listOfFieldValues[0, 0] == null && listOfFieldValues[1, 1] == cellInfo.Value && listOfFieldValues[2, 2] == cellInfo.Value) return new PresumedWinner { Row = 0, Index = 0 };
            if (listOfFieldValues[0, 0] == cellInfo.Value && listOfFieldValues[1, 1] == null && listOfFieldValues[2, 2] == cellInfo.Value) return new PresumedWinner { Row = 1, Index = 1 };
            if (listOfFieldValues[0, 0] == cellInfo.Value && listOfFieldValues[1, 1] == cellInfo.Value && listOfFieldValues[2, 2] == null) return new PresumedWinner { Row = 2, Index = 2 };

            if (listOfFieldValues[2, 0] == null && listOfFieldValues[1, 1] == cellInfo.Value && listOfFieldValues[0, 2] == cellInfo.Value) return new PresumedWinner { Row = 2, Index = 0 };
            if (listOfFieldValues[2, 0] == cellInfo.Value && listOfFieldValues[1, 1] == null && listOfFieldValues[0, 2] == cellInfo.Value) return new PresumedWinner { Row = 1, Index = 1 };
            if (listOfFieldValues[2, 0] == cellInfo.Value && listOfFieldValues[1, 1] == cellInfo.Value && listOfFieldValues[0, 2] == null) return new PresumedWinner { Row = 0, Index = 2 };

            return null;
        }

        private CellInfo? GetPreMoveForWin(CellInfo cellInfo)
        {
            Random random = new();

            for (int rowY = 0; rowY < listOfFieldValues.GetLength(0); rowY++)
            {
                if (listOfFieldValues[rowY, 0] == cellInfo.Value && listOfFieldValues[rowY, 1] == null && listOfFieldValues[rowY, 2] == null) return new CellInfo { Row = rowY, Index = random.Next(1, 2) };
                if (listOfFieldValues[rowY, 0] == null && listOfFieldValues[rowY, 1] == cellInfo.Value && listOfFieldValues[rowY, 2] == null) return new CellInfo { Row = rowY, Index = Math.Floor(random.NextDouble()) == 0 ? 0 : 2 };
                if (listOfFieldValues[rowY, 0] == null && listOfFieldValues[rowY, 1] == null && listOfFieldValues[rowY, 2] == cellInfo.Value) return new CellInfo { Row = rowY, Index = random.Next(0, 1) };
            }

            for (int rowX = 0; rowX < listOfFieldValues.GetLength(1); rowX++)
            {
                if (listOfFieldValues[0, rowX] == cellInfo.Value && listOfFieldValues[1, rowX] == null && listOfFieldValues[2, rowX] == null) return new CellInfo { Row = random.Next(1, 2), Index = rowX };
                if (listOfFieldValues[0, rowX] == null && listOfFieldValues[1, rowX] == cellInfo.Value && listOfFieldValues[2, rowX] == null) return new CellInfo { Row = Math.Floor(random.NextDouble()) == 0 ? 0 : 2, Index = rowX };
                if (listOfFieldValues[0, rowX] == null && listOfFieldValues[1, rowX] == null && listOfFieldValues[2, rowX] == cellInfo.Value) return new CellInfo { Row = random.Next(0, 1), Index = rowX };
            }

            if (listOfFieldValues[0, 0] == cellInfo.Value && listOfFieldValues[1, 1] == null && listOfFieldValues[2, 2] == null) return Math.Floor(random.NextDouble()) == 0 ? new CellInfo { Row = 1, Index = 1 } : new CellInfo { Row = 2, Index = 2 };
            if (listOfFieldValues[0, 0] == null && listOfFieldValues[1, 1] == cellInfo.Value && listOfFieldValues[2, 2] == null) return Math.Floor(random.NextDouble()) == 0 ? new CellInfo { Row = 0, Index = 0 } : new CellInfo { Row = 2, Index = 2 };
            if (listOfFieldValues[0, 0] == null && listOfFieldValues[1, 1] == null && listOfFieldValues[2, 2] == cellInfo.Value) return Math.Floor(random.NextDouble()) == 0 ? new CellInfo { Row = 0, Index = 0 } : new CellInfo { Row = 1, Index = 1 };

            if (listOfFieldValues[2, 0] == cellInfo.Value && listOfFieldValues[1, 1] == null && listOfFieldValues[0, 2] == null) return Math.Floor(random.NextDouble()) == 0 ? new CellInfo { Row = 1, Index = 1 } : new CellInfo { Row = 0, Index = 2 };
            if (listOfFieldValues[2, 0] == null && listOfFieldValues[1, 1] == cellInfo.Value && listOfFieldValues[0, 2] == null) return Math.Floor(random.NextDouble()) == 0 ? new CellInfo { Row = 2, Index = 0 } : new CellInfo { Row = 0, Index = 2 };
            if (listOfFieldValues[2, 0] == null && listOfFieldValues[1, 1] == null && listOfFieldValues[0, 2] == cellInfo.Value) return Math.Floor(random.NextDouble()) == 0 ? new CellInfo { Row = 2, Index = 0 } : new CellInfo { Row = 1, Index = 1 };

            return null;
        }

        private bool CheckForWinnerAndSendMessageBox()
        {
            string difficultBot = difficultModeBot switch
            {
                0 => "Легкий",
                1 => "Средний",
                2 => "Сложный",
                _ => "Неизвестно"
            };

            switch (CheckForWinner())
            {
                case 0:
                    MessageBox.Show($"Вы выиграли бота с режимом сложности «{difficultBot}»!", "Крестики-нолики");

                    ResetGame();
                    return true;
                case 1:
                    MessageBox.Show($"Вы проиграли боту с режимом сложности «{difficultBot}». Это было сложно, да?", "Крестики-нолики");

                    ResetGame();
                    return true;
                case -1:
                    MessageBox.Show("Ничья!", "Крестики-нолики");

                    ResetGame();
                    return true;
                default:
                    return false;
            };
        }

        private CellInfo GetMoveForEasyDifficult(Random random)
        {
            List<CellInfo> nullCells = GetNullCells();
            List<CellInfo> filledCellsByUsers = GetCells().FindAll(cellInfo => cellInfo.Value.Equals(userMoveType));

            CellInfo randomCell = nullCells[random.Next(0, nullCells.Count)];

            if (filledCellsByUsers.Count != 0)
            {
                List<CellInfo> cellsForFill = [];
                foreach (CellInfo filledCellInfo in filledCellsByUsers)
                {
                    PresumedWinner? presumedWinner = CheckForWinner(filledCellInfo);

                    if (presumedWinner != null && random.Next(0, 10) < 7)
                    {
                        cellsForFill.AddRange(nullCells.FindAll(cellInfo => cellInfo.Row != presumedWinner.Row || cellInfo.Index != presumedWinner.Index));
                        break;
                    }
                }

                if (cellsForFill.Count != 0) randomCell = cellsForFill[random.Next(0, cellsForFill.Count)];
            }

            return randomCell;
        }

        private CellInfo GetMoveForMediumDifficult(Random random)
        {
            List<CellInfo> nullCells = GetNullCells();
            List<CellInfo> filledCellsByUsers = GetCells().FindAll(cellInfo => cellInfo.Value.Equals(userMoveType));

            if (nullCells.Count >= 8 && random.Next(0, 10) < 3 && nullCells.Find(nullCell => nullCell.Row == 1 && nullCell.Index == 1) is { } centerCell)
                return centerCell;

            CellInfo randomCell = nullCells[random.Next(0, nullCells.Count)];

            List<CellInfo> cellsForFill = [];
            foreach (CellInfo filledByUserCellInfo in filledCellsByUsers)
            {
                PresumedWinner? presumedWinner = CheckForWinner(filledByUserCellInfo);

                if (presumedWinner != null && random.Next(0, 10) < 5)
                {
                    cellsForFill.AddRange(nullCells.FindAll(cellInfo => cellInfo.Row == presumedWinner.Row && cellInfo.Index == presumedWinner.Index));
                    break;
                }

                List<CellInfo> filledCellsByBot = GetCells().FindAll(cellInfo => cellInfo.Value.Equals(botMoveType));
                foreach (CellInfo filledByBotCellInfo in filledCellsByBot)
                {
                    PresumedWinner? presumedWinnerAsBot = CheckForWinner(filledByBotCellInfo);

                    if (presumedWinnerAsBot != null && random.Next(0, 10) < 7)
                    {
                        cellsForFill.AddRange(nullCells.FindAll(cellInfo => cellInfo.Row == presumedWinnerAsBot.Row && cellInfo.Index == presumedWinnerAsBot.Index));
                        break;
                    }

                    CellInfo? cellPreWin = GetPreMoveForWin(filledByBotCellInfo);
                    if (cellPreWin != null) cellsForFill.Add(cellPreWin);
                }
            }

            if (cellsForFill.Count != 0) randomCell = cellsForFill[random.Next(0, cellsForFill.Count)];

            return randomCell;
        }

        private CellInfo GetMoveForHardDifficult(Random random)
        {
            List<CellInfo> nullCells = GetNullCells();
            List<CellInfo> filledCellsByUsers = GetCells().FindAll(cellInfo => cellInfo.Value.Equals(userMoveType));

            if (nullCells.Find(nullCell => nullCell.Row == 1 && nullCell.Index == 1) is { } centerCell)
                return centerCell;

            CellInfo randomCell = nullCells[random.Next(0, nullCells.Count)];

            List<CellInfo> cellsForFill = [];
            foreach (CellInfo filledByUserCellInfo in filledCellsByUsers)
            {
                PresumedWinner? presumedWinner = CheckForWinner(filledByUserCellInfo);

                List<CellInfo> filledCellsByBot = GetCells().FindAll(cellInfo => cellInfo.Value.Equals(botMoveType));
                foreach (CellInfo filledByBotCellInfo in filledCellsByBot)
                {
                    PresumedWinner? presumedWinnerAsBot = CheckForWinner(filledByBotCellInfo);

                    if (presumedWinnerAsBot != null)
                    {
                        CellInfo? cellForWin = nullCells.Find(cellInfo => cellInfo.Row == presumedWinnerAsBot.Row && cellInfo.Index == presumedWinnerAsBot.Index);
                        if (cellForWin != null)
                        {
                            if (random.Next(0, 10) < 5) return cellForWin;

                            cellsForFill.Add(cellForWin);
                            break;
                        }
                    }

                    if (presumedWinner != null) break;

                    CellInfo? cellPreWin = GetPreMoveForWin(filledByBotCellInfo);
                    if (cellPreWin != null) cellsForFill.Add(cellPreWin);
                }

                if (presumedWinner != null)
                {
                    cellsForFill.AddRange(nullCells.FindAll(cellInfo => cellInfo.Row == presumedWinner.Row && cellInfo.Index == presumedWinner.Index));
                    break;
                }
            }

            if (cellsForFill.Count != 0) randomCell = cellsForFill[random.Next(0, cellsForFill.Count)];

            return randomCell;
        }

        private void BotPartMove(CellInfo cell)
        {
            listOfFieldValues[cell.Row, cell.Index] = botMoveType;
            Control control = panel2.Controls.Cast<Control>().Where(element => element.Tag?.Equals($"{cell.Row} {cell.Index}") ?? false).First();

            control.Invoke((MethodInvoker)delegate
            {
                control.Text = botMoveType == 0 ? "X" : "O";
            });
        }

        private void BotPartMove()
        {
            Random random = new();

            switch (difficultModeBot)
            {
                case 0:
                    CellInfo cellByEasyMode = GetMoveForEasyDifficult(random);

                    BotPartMove(cellByEasyMode);
                    break;
                case 1:
                    CellInfo cellByMediumMode = GetMoveForMediumDifficult(random);

                    BotPartMove(cellByMediumMode);
                    break;
                case 2:
                    CellInfo cellByHardMode = GetMoveForHardDifficult(random);

                    BotPartMove(cellByHardMode);
                    break;
                default:
                    break;
            }

            if (CheckForWinnerAndSendMessageBox()) return;

            botPart = false;
        }

        private void ResetGame()
        {
            for (int i = 0; i < listOfFieldValues.GetLength(0); i++)
            {
                for (int j = 0; j < listOfFieldValues.GetLength(1); j++)
                {
                    listOfFieldValues[i, j] = null;
                }
            }

            foreach (Button element in panel2.Controls.Cast<Control>().Where(element => element is Button buttonElement).Cast<Button>())
            {
                element.Invoke((MethodInvoker)delegate
                {
                    element.Text = "";
                });
            }

            if (userMoveType == 1)
            {
                botPart = true;

                if (difficultModeBot == 2)
                {
                    BotPartMove(new CellInfo { Row = 1, Index = 1 });

                    botPart = false;
                    return;
                }

                BotPartMove();

                botPart = false;
                return;
            }

            botPart = false;
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (botPart) return;
            if (((Button)sender).Text != "") return;

            if ((string?)((Button)sender).Tag is not string buttonIndex) return;
            string[] values = buttonIndex.Split(' ');

            int row = int.Parse(values[0]);
            int index = int.Parse(values[1]);

            listOfFieldValues[row, index] = userMoveType;
            ((Button)sender).Text = userMoveType == 0 ? "X" : "O";
            botPart = true;

            if (CheckForWinnerAndSendMessageBox()) return;

            Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(_ =>
            {
                BotPartMove();
            });
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            userMoveType = 0;
            botMoveType = 1;

            if (listOfFieldValues.Length > 0) ResetGame();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            userMoveType = 1;
            botMoveType = 0;

            if (listOfFieldValues.Length > 0) ResetGame();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            difficultModeBot = 0;

            if (listOfFieldValues.Length > 0) ResetGame();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            difficultModeBot = 1;

            if (listOfFieldValues.Length > 0) ResetGame();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            difficultModeBot = 2;

            if (listOfFieldValues.Length > 0) ResetGame();
        }
    }
}
