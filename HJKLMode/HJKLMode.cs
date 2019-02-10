using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SnagFree.TrayApp.Core;

namespace HJKLMode
{
  public class HJKLMode : ApplicationContext {
    private NotifyIcon _trayIcon;
    private bool _capture = false;
    private bool _win = false;
    private bool _shift = false;
    private MenuItem _toggleMenu;
    private GlobalKeyboardHook _hook;

    public HJKLMode() {
      _trayIcon = new NotifyIcon() {
        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath),
        ContextMenu = new ContextMenu(new MenuItem[] {
          _toggleMenu = new MenuItem("Enabled", Toggle),
          new MenuItem("Exit", Exit)
        }),
        Visible = true
      };

      _hook = new GlobalKeyboardHook();
      _hook.KeyboardPressed += OnKeyPressed;
    }

    ~HJKLMode() {
      _hook.Dispose();
    }

    private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e) {
      if (e.KeyboardData.VirtualCode == GlobalKeyboardHook.VkLwin) {
        if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown) {
          _win = true;
          if (_capture) {
            e.Handled = true;
          }
        } else if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp) {
          _win = false;
        }
      }

      if (e.KeyboardData.VirtualCode == GlobalKeyboardHook.vkLShift) {
        if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown) {
          _shift = true;
        } else if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp) {
          _shift = false;
        }
      }

      if (e.KeyboardData.VirtualCode == GlobalKeyboardHook.H) {
        if (_win && e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp) {
          Toggle(sender, e);
          e.Handled = true;
        } else if (_capture) {
          SendKeys.Send("{LEFT}");
          e.Handled = true;
        }
      }

      if (!_capture) {
        return;
      }

      if (e.KeyboardData.VirtualCode == GlobalKeyboardHook.NUM_4 && _shift) {
        SendKeys.Send("{HOME}");
        e.Handled = true;
      }

      if (e.KeyboardData.VirtualCode == GlobalKeyboardHook.NUM_4 && _shift) {
        SendKeys.Send("{END}");
        e.Handled = true;
      }

      if (e.KeyboardData.VirtualCode == GlobalKeyboardHook.J) {
        SendKeys.Send("{DOWN}");
        e.Handled = true;
      }

      if (e.KeyboardData.VirtualCode == GlobalKeyboardHook.K) {
        SendKeys.Send("{UP}");
        e.Handled = true;
      }

      if (e.KeyboardData.VirtualCode == GlobalKeyboardHook.L) {
        SendKeys.Send("{RIGHT}");
        e.Handled = true;
      }

      return;
    }

    void Toggle(object sender, EventArgs e) {
      _toggleMenu.Checked = _capture = !_capture;
    }

    void Exit(object sender, EventArgs e) {
      _trayIcon.Visible = false;
      Application.Exit();
    }
  }
}
