using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using SharpDX.XInput;

namespace ScummVMSpeechBridge
{
    public partial class SpeechForm : Form
    {
        const string ScummVMProcessName = "scummvm";
        const int CheckDelayMs = 100;
        const int TypeDelayMs = 50;
        const Key SwitchKey = Key.Tab;

        const UInt32 WM_KEYDOWN = 0x0100;

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        static extern short VkKeyScan(char ch);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        Controller controller;
        public bool connected = false;

        public SpeechForm()
        {
            InitializeComponent();

            controller = new Controller(UserIndex.One);
            connected = controller?.IsConnected ?? false;
            Task.Run(CheckControllerLoop);
        }

        private void CheckControllerLoop()
        {
            while (true)
            {
                CheckController();
                System.Threading.Thread.Sleep(CheckDelayMs);
            }
        }

        private bool FindScummVM(out IntPtr windowHandle)
        {
            var proc = Process.GetProcessesByName(ScummVMProcessName).FirstOrDefault();
            if(proc != null)
            {
                windowHandle = proc.MainWindowHandle;
                return true;
            }
            windowHandle = IntPtr.Zero;
            return false;
        }

        private void CheckController()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(CheckController));
                return;
            }
            if (ContainsFocus)
            {
                var shouldSwitchBack = false;
                if (connected)
                {
                    var state = controller.GetState();
                    if (state.Gamepad.RightTrigger == 0)
                    {
                        shouldSwitchBack = true;
                    }
                }
                if(!shouldSwitchBack)
                {
                    if(!Keyboard.IsKeyDown(SwitchKey))
                    {
                        shouldSwitchBack = true;
                    }
                }

                if(shouldSwitchBack)
                {
                    if (FindScummVM(out var windowHandle))
                    {
                        SetForegroundWindow(windowHandle);
                        speechInputBox.Clear();
                    }
                }
            }
        }

        private void speechInputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (FindScummVM(out var windowHandle))
            {
                PostMessage(windowHandle, WM_KEYDOWN, VkKeyScan(e.KeyChar), 0);
                System.Threading.Thread.Sleep(TypeDelayMs);
            }
        }
    }
}
