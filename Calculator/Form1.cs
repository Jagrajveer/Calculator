namespace Calculator {
    public partial class Form1 : Form {
        private string storedOperand = "";
        private string storedOperation = "";
        private string displayType = "DEC";

        public Form1() {
            InitializeComponent();
        }

        private void InputNumber(object sender, EventArgs e) {
            Button input = (Button)sender;
            if(displayType == "DEC") {
                if (textBox1.Text == "0" && input.Text == ".") {
                    textBox1.Text = "0.";
                } else {
                    if (input.Text == "." && !textBox1.Text.Contains(".")) {
                        textBox1.Text += input.Text;
                    } else if(input.Text != ".") {
                        if (textBox1.Text == "0") {
                            textBox1.Text = input.Text;
                        } else {
                            textBox1.Text += input.Text;
                        }
                    }
                }
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e) {
            if(((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 46) {
                if (displayType == "DEC") {
                    if (textBox1.Text == "0" && (int)e.KeyChar == 46) {
                        textBox1.Text = "0.";
                    } else {
                        if ((int)e.KeyChar == 46 && !textBox1.Text.Contains(".")) {
                            textBox1.Text += e.KeyChar;
                        } else if ((int)e.KeyChar != 46) {
                            if (textBox1.Text == "0") {
                                textBox1.Text = e.KeyChar.ToString();
                            } else {
                                textBox1.Text += e.KeyChar;
                            }
                        }
                    }
                }
            }

            if((int)e.KeyChar == 42
                || (int)e.KeyChar == 43 
                || (int)e.KeyChar == 45 
                || (int)e.KeyChar == 46
            ) {
                if (displayType == "DEC") {
                    if (e.KeyChar.ToString() != storedOperation && storedOperation != "") {
                        storedOperation = e.KeyChar.ToString();
                    }

                    if (storedOperation != "" && storedOperand != "" && textBox1.Text != "0") {
                        storedOperand = Calculate(storedOperand, storedOperation, textBox1.Text).ToString();
                        textBox1.Text = storedOperand;
                    }

                    storedOperand = textBox1.Text;
                    storedOperation = e.KeyChar.ToString();

                    label1.Text = $"{storedOperand} {storedOperation}";

                    textBox1.Text = "0";
                }
            }
        }

        private void Operation(object sender, EventArgs e) {
            Button operation = (Button)sender;
            if(displayType == "DEC") {
                if (operation.Text != storedOperation && storedOperation != "") {
                    storedOperation = operation.Text;
                }

                if (storedOperation != "" && storedOperand != "" && textBox1.Text != "0") {
                    storedOperand = Calculate(storedOperand, storedOperation, textBox1.Text).ToString();
                    textBox1.Text = storedOperand;
                }

                storedOperand = textBox1.Text;
                storedOperation = operation.Text;

                label1.Text = $"{storedOperand} {storedOperation}";

                textBox1.Text = "0";
            }
        }

        private void Result(object sender, EventArgs e) {
            if (displayType == "DEC") {
                if (storedOperation != "" && storedOperand != "") {
                    storedOperand = Calculate(storedOperand, storedOperation, textBox1.Text);
                    textBox1.Text = storedOperand;
                    storedOperation = "";
                    label1.Text = $"{storedOperand} {storedOperation}";
                }
            }
        }

        private string Calculate(string firstOperand, string operation, string secondOperand) {
            switch (operation) {
                case "+":
                    return (decimal.Parse(firstOperand) + decimal.Parse(secondOperand)).ToString();
                case "-":
                    return (decimal.Parse(firstOperand) - decimal.Parse(secondOperand)).ToString();
                case "*":
                    return (decimal.Parse(firstOperand) * decimal.Parse(secondOperand)).ToString();
                case "/":
                    return (decimal.Parse(firstOperand) / decimal.Parse(secondOperand)).ToString();
                default:
                    return "Invalid";
            }
        }

        private void ConvertType(object sender, EventArgs e) {
            Button type = (Button)sender;
            try {
                if (displayType == "DEC") {
                    if (type.Text == "BIN") {
                        Int64 number = Int64.Parse(textBox1.Text);

                        string Result = string.Empty;
                        for (int i = 0; number > 0; i++) {
                            Result = number % 2 + Result;
                            number = number / 2;
                        }
                        textBox1.Text = Result;
                        displayType = "BIN";
                    } else if (type.Text == "LOC") {

                        displayType = "LOC";
                    }
                } else if (displayType == "BIN") {
                    if (type.Text == "DEC") {
                        int decimalNum = Convert.ToInt32(textBox1.Text, 2);
                        textBox1.Text = decimalNum.ToString();
                        displayType = "DEC";
                    }
                } else if (displayType == "LOC") {
                    if (type.Text == "DEC") {
                        textBox1.Text = ConvertToLOCOrDEC(textBox1.Text, "LOC");
                        displayType = "DEC";
                    }
                }
            } catch {
                textBox1.Text = "ERROR";
            }
        }

        private void Clear(object sender, EventArgs e) {
            textBox1.Text = "0";
        }

        private void Allclear(object sender, EventArgs e) {
            storedOperand = "";
            storedOperation = "";
            displayType = "DEC";
            textBox1.Text = "0";
        }

        private string ConvertToLOCOrDEC(string num, string type) {
            if (num.Contains(".")) {
                return "ERROR";
            }

            Dictionary<char, int> alphaNum = new Dictionary<char, int> {
                { 'a', 0 },
                { 'b', 1 },
                { 'c', 2 },
                { 'd', 3 },
                { 'e', 4 },
                { 'f', 5 },
                { 'g', 6 },
                { 'h', 7 },
                { 'i', 8 },
                { 'j', 9 },
                { 'k', 10 },
                { 'l', 11 },
                { 'm', 12 },
                { 'n', 13 },
                { 'o', 14 },
                { 'p', 15 },
                { 'q', 16 },
                { 'r', 17 },
                { 's', 18 },
                { 't', 19 },
                { 'u', 20 },
                { 'v', 21 },
                { 'w', 22 },
                { 'x', 23 },
                { 'y', 24 },
                { 'z', 25 },
            };

            if(type == "LOC") {
                double number = 0;
                foreach(var c in num){
                    number += Math.Pow(2, alphaNum[c]);
                }
                return number.ToString();
            }

            return "";
        }
    }
}
