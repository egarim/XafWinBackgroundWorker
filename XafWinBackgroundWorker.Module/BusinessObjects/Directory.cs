using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace XafWinBackgroundWorker.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Directory : BaseObject, IReportProgress
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Directory(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.filesToGenerate = 100;
            this.name = "C:\\MyFakeDirectory";
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        int progress;
        int filesToGenerate;
        string name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }
        [Association("Directory-DirectoryFiles")]
        public XPCollection<DirectoryFile> DirectoryFiles
        {
            get
            {
                return GetCollection<DirectoryFile>(nameof(DirectoryFiles));
            }
        }

        public int FilesToGenerate
        {
            get => filesToGenerate;
            set => SetPropertyValue(nameof(FilesToGenerate), ref filesToGenerate, value);
        }
        [ModelDefault(nameof(IModelCommonMemberViewItem.PropertyEditorType), "XafWinBackgroundWorker.Module.Win.Editors.ProgressBarPropertyEditor")]
        public int Progress
        {
            get => progress;
            set => SetPropertyValue(nameof(Progress), ref progress, value);
        }

        [Browsable(false)]
        public int Max => this.FilesToGenerate;
       
    }
}