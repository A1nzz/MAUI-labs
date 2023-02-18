namespace Calculator;

public partial class ProgressDemo : ContentPage
{
    public ProgressDemo()
    {
        InitializeComponent();
    }

    private CancellationTokenSource cancelTokenSource = null;

    private Task<double> calcSinTask = null;

    async private void animateButton(Button btn)
    {
        btn.Scale = 0.9;
        await Task.Delay(100);
        btn.Scale = 1;
    }    

    private double CalculateIntegral()
    {
        double result = 0;
        MainThread.BeginInvokeOnMainThread(() =>
        {
            this.SateLabel.Text = "Calculating";
        });
        int percent = 0;
        int lastPercent = 0;
        double step = 0.00000001;
        for (double i = 0; i < 1 + step; i += step)
        {
            if (cancelTokenSource.IsCancellationRequested)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    this.SateLabel.Text = "Cancelled";
                });
                return double.NaN;
            }
            result += Math.Sin(i) * step;
            percent = (int)(i * 100);
            int a = 1;

            for (int j = 0; j < 10; j++)
            {
                a = a * a;
            }

            if (percent != lastPercent)
            {
                lastPercent = percent;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    progressBar.Progress = percent / 100.0;
                    this.progressLabel.Text = percent + "%";
                });
            }
        }
        return result;
    }

    public async void OnStart(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        animateButton(btn);
        if (calcSinTask?.Status != TaskStatus.Running)
        {
            cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            calcSinTask = Task.Run(() => CalculateIntegral(), token);
            var result = await calcSinTask;
            if (cancelTokenSource.IsCancellationRequested)
            {
                return;
            }
            this.SateLabel.Text = "Result: " + result.ToString();
        }  
    }

    public void OnCancel(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        animateButton(btn);        
        if (calcSinTask?.Status == TaskStatus.Running)
        {
            cancelTokenSource.Cancel();
            cancelTokenSource.Dispose();
        }
    }
}