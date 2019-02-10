using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WASDMode.Properties;

namespace WASDMode {
  public class WASDMode : ApplicationContext {
    private NotifyIcon trayIcon;

    public WASDMode() {
      trayIcon = new NotifyIcon() {
        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath),
        ContextMenu = new ContextMenu(new MenuItem[] {
          new MenuItem("Exit", this.Exit)
        }),
        Visible = true
      };
    }

    void Exit(object sender, EventArgs e) {
      trayIcon.Visible = false;
      Application.Exit();
    }
  }
}
