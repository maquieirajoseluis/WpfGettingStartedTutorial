using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace WpfGettingStartedTutorial
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static bool createdNew;
        private bool isExit;
        private static Mutex mutex = new Mutex(true, "E033614A-4651-4EAD-BE6C-155D337AD381", out createdNew);
        private System.Windows.Forms.NotifyIcon notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                Current.Shutdown();
                return;
            }

            base.OnStartup(e);
            MainWindow = new MainWindow();
            MainWindow.Closing += MainWindow_Closing;

            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Icon = WpfGettingStartedTutorial.Properties.Resources.app;
            notifyIcon.Visible = true;

            CreateContextMenuStrip();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (createdNew)
            {
                mutex.ReleaseMutex();
            }
            base.OnExit(e);
        }

        private void CreateContextMenuStrip()
        {
            notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("Main").Click += (s, e) => ShowMainWindow();
            notifyIcon.ContextMenuStrip.Items.Add("Sign In").Click += (s, e) => ShowSignInView();
            notifyIcon.ContextMenuStrip.Items.Add("Help").Click += (s, e) => ShowHelpView();
            notifyIcon.ContextMenuStrip.Items.Add("Send Feedback").Click += (s, e) => ShowSendFeedbackView();
            notifyIcon.ContextMenuStrip.Items.Add("About").Click += (s, e) => ShowAboutView();
            notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
        }

        private void ShowSignInView()
        {
            throw new NotImplementedException();
        }

        private void ShowAboutView()
        {
            throw new NotImplementedException();
        }

        private void ShowSendFeedbackView()
        {
            throw new NotImplementedException();
        }

        private void ShowHelpView()
        {
            throw new NotImplementedException();
        }

        private void ExitApplication()
        {
            isExit = true;
            MainWindow.Close();
            notifyIcon.Dispose();
            notifyIcon = null;
        }

        private void ShowMainWindow()
        {
            if (MainWindow.IsVisible)
            {
                if (MainWindow.WindowState == WindowState.Minimized)
                {
                    MainWindow.WindowState = WindowState.Normal;
                }
                MainWindow.Activate();
            }
            else
            {
                MainWindow.Show();
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!isExit)
            {
                e.Cancel = true;
                MainWindow.Hide();
            }
        }
    }
}
