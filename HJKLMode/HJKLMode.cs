using System;
using System.Drawing;
using System.Windows.Forms;

namespace HJKLMode
{
  public class HJKLMode : ApplicationContext {
    private NotifyIcon trayIcon;
    private bool capturing;

    public HJKLMode() {
      trayIcon = new NotifyIcon() {
        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath),
        ContextMenu = new ContextMenu(new MenuItem[] {
          new MenuItem("Enable", Enable),
          new MenuItem("Disable", Disable),
          new MenuItem("Exit", Exit)
        }),
        Visible = true
      };
    }

    void Enable(object sender, EventArgs e) {
      capturing = true;
    }

    void Disable(object sender, EventArgs e) {
      capturing = false;
    }

    void Exit(object sender, EventArgs e) {
      trayIcon.Visible = false;
      Application.Exit();
    }
  }
}
