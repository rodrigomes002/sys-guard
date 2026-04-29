using System.Diagnostics;

namespace Infra;

public class ProcessBlocker
{
    public void KillIfRunning(string processName)
    {
        var processes = Process.GetProcessesByName(processName);

        foreach (var process in processes)
        {
            try
            {
                process.Kill();
            }
            catch { }
        }
    }
}