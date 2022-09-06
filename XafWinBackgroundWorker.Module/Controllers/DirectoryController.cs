using Bogus;
using DevExpress.Data.Filtering;
using DevExpress.DataAccess.Native.Sql;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.XtraReports.Templates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using XafWinBackgroundWorker.Module.BusinessObjects;

namespace XafWinBackgroundWorker.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class DirectoryController : ViewController
    {
        SimpleAction ReadFiles;
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public DirectoryController()
        {
            InitializeComponent();
            this.TargetObjectType = typeof(Directory);
            this.TargetViewType = ViewType.DetailView;
            ReadFiles = new SimpleAction(this, "ReadFiles", "View");
            ReadFiles.Execute += ReadFiles_Execute;
            
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        private void ReadFiles_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            var CurrentDirectory = this.View.CurrentObject as Directory;

            BackgroundWorker BWorker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            BWorker.DoWork += backgroundWorker_DoWork;
            BWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            void backgroundWorker_ProgressChanged(object RP_sender, ProgressChangedEventArgs RP_e)
            {
                //HACK here we are back to the main thread so we can use the object space to create objects
              
               (string FileName, byte[] Data) WorkerArgs = ((string FileName, byte[] Data))RP_e.UserState;
                var test = WorkerArgs.Data;

                //resultLabel.Text = (e.ProgressPercentage.ToString() + "%");
            }
            void backgroundWorker_DoWork(object BW_sender, DoWorkEventArgs BW_e)
            {
                BackgroundWorker worker = BW_sender as BackgroundWorker;
                (Faker Faker, int FilesToGenerate) WorkerArgs = ((Faker Faker, int FilesToGenerate))BW_e.Argument;


               

                for (int i = 1; i <= WorkerArgs.FilesToGenerate; i++)
                {
                    if (worker.CancellationPending == true)
                    {
                        BW_e.Cancel = true;
                        break;
                    }
                    else
                    {
                        //We generate a filename and a random text as bytes to return to the main thread
                        var DataForMainThread = ($"{WorkerArgs.Faker.Name}.txt", Encoding.ASCII.GetBytes(WorkerArgs.Faker.Lorem.Sentence(50)));
                        // Perform a time consuming operation and report progress.
                        System.Threading.Thread.Sleep(500);

                        worker.ReportProgress(i, DataForMainThread);
                    }
                }
            }

            
            //Using bogus to generate random data
            var faker = new Faker("en");

            //Using tuples to pass arguments to the backgrown worker
            var WokerArgs = (faker, CurrentDirectory.FilesToGenerate);

            BWorker.RunWorkerAsync(WokerArgs);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
