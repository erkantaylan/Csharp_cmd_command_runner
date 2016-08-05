using System.Diagnostics;

namespace AEHelper {

    internal class CmdHelper {

        private readonly string command;
        /*
         
string output = proc.StandardOutput.ReadToEnd();
         
         
string cmd = "/c dir" ; 
System.Diagnostics.Process proc = new System.Diagnostics.Process(); 
proc.StartInfo.FileName = "cmd.exe"
proc.StartInfo.Arguments = cmd; 
proc.StartInfo.UseShellExecute = false;
proc.StartInfo.RedirectStandardOutput = true; 
proc.Start();
         */


        public CmdHelper(string command) {
            this.command = command;
        }

        public string Run() {
            var proc = new Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.Arguments = this.command;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            return proc.StandardOutput.ReadToEnd();
        }

    }

}