using System;
using System.Windows;
using NAudio.Wave;

namespace RadioApp
{
    public partial class MainWindow : Window
    {
        private IWavePlayer outputDevice;
        private MediaFoundationReader audioReader;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StopRadio();

                string url = UrlTextBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(url))
                {
                    MessageBox.Show("Введите ссылку на радиопоток");
                    return;
                }

                audioReader = new MediaFoundationReader(url);
                outputDevice = new WaveOutEvent();
                outputDevice.Init(audioReader);
                outputDevice.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка запуска потока:\n" + ex.Message);
                StopRadio();
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StopRadio();
        }

        private void StopRadio()
        {
            if (outputDevice != null)
            {
                outputDevice.Stop();
                outputDevice.Dispose();
                outputDevice = null;
            }

            if (audioReader != null)
            {
                audioReader.Dispose();
                audioReader = null;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            StopRadio();
            base.OnClosed(e);
        }
    }
}