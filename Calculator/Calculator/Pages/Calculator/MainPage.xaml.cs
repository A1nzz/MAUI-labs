
using CommunityToolkit.Maui.Views;
using MauiToolkitPopupSample;

namespace Calculator;

public partial class MainPage : ContentPage
{

    private string currentOperator;

    private double firstNumber, secondNumber;

    private int state = 0;
    
    private string currentEntry = "";

    private bool isNullDeviding = false;

    private bool isCommaUsed = false;

    private double memory;

    public MainPage()
	{
		InitializeComponent();
	}

    async private void animateButton(Button btn)
	{
        btn.Scale = 0.9;
        await Task.Delay(100);
        btn.Scale = 1;
    }

	public void OnNumberClicked(object sender, EventArgs args)
	{
        var button = (Button)sender;
        string btnText = button.Text;
        if (state == 0)
        {
            if(btnText == ",")
            {
                this.result.Text = "0" + btnText;
                currentEntry = "0" + btnText;
                state = 1;
                isCommaUsed = true;
            } 
            else
            {
                this.result.Text = btnText;
                currentEntry = btnText;
                if (btnText != "0")
                {
                    state = 1;
                }
            }    
        } else if (state == 1)
        {
            if(btnText == ",")
            {
                if (!isCommaUsed)
                {
                    this.result.Text += btnText;
                    currentEntry += btnText;
                    isCommaUsed = true;
                }
            } 
            else
            {
                this.result.Text += btnText;
                currentEntry += btnText;
            }
      
        }
        else if (state == 2)
        {
            isCommaUsed = false;
            if (btnText == ",")
            {
                this.result.Text = "0" + btnText;
                currentEntry = "0" + btnText;
                isCommaUsed = true;
                state = 3;
            } else
            {
                this.result.Text = btnText;
                currentEntry = btnText;
                state = 3;
            }
            
        }
        else if (state == 3)
        {
            if(this.result.Text == "0")
            {
                if (btnText == ",")
                {
                    if (!isCommaUsed)
                    {
                        this.result.Text += btnText;
                        currentEntry += btnText;
                        isCommaUsed = true;
                    }
                } else
                {
                    this.result.Text = btnText;
                    currentEntry = btnText;
                }
               
         
            } else
            {
                if (btnText == ",") {
                    if(!isCommaUsed)
                    {
                        isCommaUsed = true;
                        this.result.Text += btnText;
                        currentEntry += btnText;
                    }  
                } else
                {
                    this.result.Text += btnText;
                    currentEntry += btnText;
                }
                
            }
            
        }
        else if (state == 4)
        {
            this.currentCalculation.Text = "";
            if(btnText == ",")
            {
                this.result.Text = "0" + btnText;
                currentEntry = "0" + btnText;
            } else
            {
                this.result.Text = btnText;
                currentEntry = btnText;
            }
           ;
            state = 1;
        }

        animateButton(button);
        
    }
    public void OnOperationClicked(object sender, EventArgs args)
    {
        
        Button button = (Button)sender;
        if (state != 3) currentOperator = button.Text;
        if (state == 0)
        {
            firstNumber = 0;
            state = 2;
            this.currentCalculation.Text = firstNumber + " " + currentOperator;   
        }
        else if (state == 1)
        {
            firstNumber = Double.Parse(currentEntry);
            this.currentCalculation.Text = firstNumber + " " + currentOperator;
            state = 2;
        }
        else if (state == 2)
        {
            currentOperator = button.Text;
            this.currentCalculation.Text = firstNumber + " " + currentOperator;
            //state = 2
        }
        else if (state == 3)
        {
            secondNumber = Double.Parse(currentEntry);
            this.currentCalculation.Text = Calculate(firstNumber, secondNumber, currentOperator).ToString() + " " + button.Text;
            this.result.Text = Calculate(firstNumber, secondNumber, currentOperator).ToString();
            if (isNullDeviding)
            {
                firstNumber = 0;
                state = 0;
                isNullDeviding = false;
                this.result.Text = "0";
                this.currentCalculation.Text = "";
                this.ShowPopup(new PopupNull());
                return;
            }
            firstNumber = Double.Parse(this.result.Text);
            currentOperator = button.Text;
            state = 2;
            isCommaUsed = false;
        } else if (state == 4)
        {
            this.currentCalculation.Text = firstNumber + " " + currentOperator;
            state = 2;
        }
        animateButton(button);
    }

    public void OnDel(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        if (state == 1 || state == 3)
        {
            if (currentEntry.Length > 1)
            {
                if (currentEntry[currentEntry.Length - 1] == ',')
                {
                    isCommaUsed = false;
                    if(currentEntry == "0,")
                    {
                        if(state == 1)
                        {
                            state = 0;
                        } 
                    }
                }
                currentEntry = currentEntry.Substring(0, currentEntry.Length - 1);
                this.result.Text = currentEntry;
            }
            else
            {
                currentEntry = "0";
                this.result.Text = "0";
                if (state == 1)
                {
                    state = 0;
                }
            }
        }
        animateButton(button);
    }

    public void OnCe(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        if (state == 1)
        {
            currentEntry = "0";
            this.result.Text = "0";
            state = 0;
        }
        else if (state == 3)
        {
            currentEntry = "0";
            this.result.Text = "0";
            state = 3;
        } if (state == 4)
        {
            currentEntry = "0";
            this.result.Text = "0";
            this.currentCalculation.Text = "";
            state = 0;
        }
        isCommaUsed = false;
        animateButton(button);
    }

    public void OnClear(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        isCommaUsed = false;
        firstNumber = 0;
        secondNumber = 0;
        state = 0;       
        this.result.Text = "0";
        this.currentCalculation.Text = "";
        currentEntry = string.Empty;
        animateButton(button);
    }

    public void OnChangeSign(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        if (state == 1 || state == 3)
        {
            string text = this.result.Text;
            if (text == "0" || text == "0,") return;
            if (text[0] == '-')
            {
                text = this.result.Text.Substring(1, currentEntry.Length - 1);
                currentEntry = text;
                this.result.Text = currentEntry;
            }
            else
            {
                currentEntry = "-" + text;
                this.result.Text = currentEntry;
            }
        }
        animateButton(button);
    }

    public void OnSquare(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        double number = Double.Parse(this.result.Text);
        currentEntry = Math.Pow(number, 2).ToString();
        if(state == 1)
        {
            this.currentCalculation.Text = "sqr(" + number.ToString() + ")";
        } if(state == 2)
        {
            this.currentCalculation.Text = number.ToString() + currentOperator + "sqr(" + number.ToString() + ")";
            state = 3;
        }
        this.result.Text = currentEntry;

        animateButton(button);
    }

    public void OnSqrt(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        double number = Double.Parse(this.result.Text);
        currentEntry = Math.Sqrt(number).ToString();
        if (state == 1)
        {
            this.currentCalculation.Text = "sqrt(" + number.ToString() + ")";
        }
        if (state == 2)
        {
            this.currentCalculation.Text = number.ToString() + currentOperator + "sqrt(" + number.ToString() + ")";
            state = 3;
        }
        this.result.Text = currentEntry;

        animateButton(button);
    }

    public void OnTwoPow(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        double number = Double.Parse(this.result.Text);
        currentEntry = Math.Pow(2, number).ToString();
        if (state == 1)
        {
            this.currentCalculation.Text = "2^(" + number.ToString() + ")";
        }
        if (state == 2)
        {
            this.currentCalculation.Text = number.ToString() + currentOperator + "2^(" + number.ToString() + ")";
            state = 3;
        }
        this.result.Text = currentEntry;

        animateButton(button);

    }

    public void OnOpposite(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        if (state == 0) return;
        double number = Double.Parse(this.result.Text);
        currentEntry =(1/number).ToString();
        if (state == 1)
        {
            this.currentCalculation.Text = "1/(" + number.ToString() + ")";
        }
        if (state == 2)
        {
            this.currentCalculation.Text = number.ToString() + currentOperator + "1/(" + number.ToString() + ")";
            state = 3;
        }
        this.result.Text = currentEntry;

        animateButton(button);
    }


    public void OnCalculate(object sender, EventArgs e)
    {
        if (state == 3)
        {
            secondNumber = Double.Parse(currentEntry);
            this.currentCalculation.Text = firstNumber + " " + currentOperator + " " + secondNumber + " = ";
            this.result.Text = Calculate(firstNumber, secondNumber, currentOperator).ToString();
            if(isNullDeviding)
            {
                firstNumber = 0;
                state = 0;
                isNullDeviding = false;
                this.result.Text = "0";
                this.currentCalculation.Text = "";
                this.ShowPopup(new PopupNull());
                return;
            }
            firstNumber = Double.Parse(this.result.Text);
            state = 4;
            isCommaUsed = false;
        }
    }
    public double Calculate(double val1, double val2, string op)
    {
        double result = 0;
        switch (op)
        {
            case "+":
                result = val1 + val2;
                break;
            case "-":
                result = val1 - val2;
                break;
            case "×":
                result = val1 * val2;
                break;
            case "÷":
                if (val2 == 0)
                {
                    isNullDeviding = true;
                    break;
                }
                result = val1 / val2;
                break;
        }
        return result;
    }

    public void OnMC(object sender, EventArgs e)
    {
        memory = 0;
        Button button = (Button)sender;
        animateButton(button);

    }
    public void OnMR(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        animateButton(button);
        currentEntry = memory.ToString();
        this.result.Text = currentEntry;
    }
    public void OnMPlus(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        animateButton(button);
        memory = memory + Double.Parse(this.result.Text);
        state = 4;
    }
    public void OnMMinus(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        animateButton(button);
        memory = memory - Double.Parse(this.result.Text);
        state = 4;
    }
    public void OnMS(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        animateButton(button);
        memory = Double.Parse(this.result.Text);
        state = 4;
    }
    public void OnM(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        animateButton(button);
        this.ShowPopup(new PopupNull(memory));
    }
}

