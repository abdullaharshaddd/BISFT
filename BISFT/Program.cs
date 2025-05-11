using System;
using System.Windows.Forms;

namespace BISFT
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Customer segmentation model loading
            CustomerSegmentationTrainer.TrainModel();
            CustomerSegmentationTrainer.PredictClusters();

            // ✅ Removed unnecessary forecasting stuff from startup

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Dashboard());


            //chatbot test
            //ChatbotTest.RunTests().Wait();
        }
    }
}
