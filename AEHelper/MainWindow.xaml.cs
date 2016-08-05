using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Documents;

namespace AEHelper {

    public partial class MainWindow {

        /*
         
             "C:\Program Files\Adobe\Adobe After Effects CC 2014\Support Files\aerender.exe" 
-project 
"C:\Users\abdullahtasgoz\Desktop\My Templates\Wedding Book\Wedding Book.aep" 
-comp Final Comp -output 
"C:\Users\abdullahtasgoz\Desktop\vasfi\video.MOV"
             */


        public MainWindow() {
            InitializeComponent();
            InitializeProcess();
        }

        public StreamWriter CmdStreamWriter { get; set; }

        public Process CmdProcess { get; set; }

        public string CmdOutput { get; set; }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            var rendererLocation = this.txtRenderLocation.Text.Trim();
            var optionalCommand1 = this.txtOptionalCommand1.Text.Trim();
            var projectLocation = this.txtProjectLocation.Text.Trim();
            var optionalCommand2 = this.txtOpionalCommand2.Text.Trim();
            var outputLocation = this.txtOutputLocation.Text.Trim();

            var command = ConcatCommand(
                rendererLocation,
                optionalCommand1,
                projectLocation,
                optionalCommand2,
                outputLocation);

            AddParagraph(SendCommand(command));
        }

        /// <summary>
        ///     put commands together
        /// </summary>
        /// <returns>one line command</returns>
        private string ConcatCommand(
            string rendererLocation,
            string optionalCommand1,
            string projectLocation,
            string optionalCommand2,
            string outputLocation) {
            return $"\"{rendererLocation}\"" +
                   $" {optionalCommand1} \"{projectLocation}\"" +
                   $" {optionalCommand2} {outputLocation}";
        }

        private void InitializeProcess() {
            this.CmdProcess = new Process {
                StartInfo = {
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true
                }
            };

            this.CmdProcess.OutputDataReceived += CmdProcess_OutputDataReceived;

            this.CmdProcess.Start();

            this.CmdStreamWriter = this.CmdProcess.StandardInput;

            this.CmdProcess.BeginOutputReadLine();
        }


        private void AddParagraph(string text) {
            if ( text != null
                 && string.IsNullOrWhiteSpace(text) ) {
                this.Dispatcher.Invoke(
                    () => {
                        var p = new Paragraph();
                        p.Inlines.Add(text);
                        this.rtxtOutput.Document.Blocks.Add(p);
                    });
            }
        }

        private void CmdProcess_OutputDataReceived(object sender, DataReceivedEventArgs e) {
            if ( string.IsNullOrWhiteSpace(e.Data) ) {
                AddParagraph($"{e.Data}");
            }
        }

        private string SendCommand(string command) {
            this.CmdStreamWriter.WriteLine(command);
            return this.CmdOutput;
        }

    }

}