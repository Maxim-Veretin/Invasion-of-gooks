using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Invasion_of_Gooks.View;
using System.Data.SQLite;
using Invasion_of_Gooks.Model;
using Invasion_of_Gooks.ViewModel;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Invasion_of_Gooks
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Переопределение закрытия окна и деактивация крестика закрытия
        /// Код взят из https://stackoverflow.com/questions/743906/how-to-hide-close-button-in-wpf-window/1623991#1623991

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;

            if (hwndSource != null)
            {
                hwndSource.AddHook(HwndSourceHook);
            }
        }

        /// <summary>Разрешение на закрытие окна</summary>
        private bool allowClosing = false;

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        private const uint MF_BYCOMMAND = 0x00000000;
        private const uint MF_GRAYED = 0x00000001;

        private const uint SC_CLOSE = 0xF060;

        private const int WM_SHOWWINDOW = 0x00000018;
        private const int WM_CLOSE = 0x10;

        private IntPtr HwndSourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_SHOWWINDOW:
                    {
                        IntPtr hMenu = GetSystemMenu(hwnd, false);
                        if (hMenu != IntPtr.Zero)
                        {
                            EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
                        }
                    }
                    break;
                case WM_CLOSE:
                    if (!allowClosing)
                    {
                        handled = true;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        /// <summary>Новый метод программного закрытия с учётом переопределения 
        /// внешних методов закрытия</summary>
        public new void Close()
        {
            allowClosing = true;
            base.Close();
        }
        #endregion

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            MediaPlayerExtensions.AllContinue();
        }
        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            MediaPlayerExtensions.AllPause();
        }
    }
}
