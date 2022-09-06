using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Persistent.BaseImpl;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XafWinBackgroundWorker.Module.BusinessObjects;

namespace XafWinBackgroundWorker.Module.Win.Editors
{

    [PropertyEditor(typeof(Int32), false)]
    public class ProgressBarPropertyEditor : PropertyEditor, IComplexViewItem
    {
        private ProgressBarControl control = null;
        IReportProgress currentObject;
        protected override void ReadValueCore()
        {
            if (control != null)
            {
                if (CurrentObject != null)
                {
                    control.ReadOnly = false;
                    control.Position = (int)PropertyValue;
                }
                else
                {
                    control.ReadOnly = true;
                    control.Position = 0;
                }
            }
        }
        private void control_ValueChanged(object sender, EventArgs e)
        {
            if (!IsValueReading)
            {
                OnControlValueChanged();
                WriteValueCore();
            }
        }
        protected override object CreateControlCore()
        {
            control=new ProgressBarControl();
            control.Properties.Step = 1;
            control.Properties.PercentView = true;
            control.Properties.Maximum = 100;
            control.Properties.Minimum = 0;

            //control = new NumericUpDown();
            //control.Minimum = 0;
            //control.Maximum = 5;
            //control.ValueChanged += control_ValueChanged;
            return control;
        }
        protected override void OnControlCreated()
        {
            base.OnControlCreated();
            ReadValue();
        }
        public ProgressBarPropertyEditor(Type objectType, IModelMemberViewItem info)
            : base(objectType, info)
        {
        }
        protected override void Dispose(bool disposing)
        {
            if (control != null)
            {
                //control.ValueChanged -= control_ValueChanged;
                control = null;
            }
            base.Dispose(disposing);
        }
   
        protected override object GetControlValueCore()
        {
            if (control != null)
            {
                return control.Position;
            }
            return null;
        }

        public void Setup(IObjectSpace objectSpace, XafApplication application)
        {
          
        }
        protected override void OnCurrentObjectChanged()
        {
            base.OnCurrentObjectChanged();
            if (this.CurrentObject != null)
            {
                currentObject = this.View.CurrentObject as IReportProgress;
                currentObject.PropertyChanged += CurrentObject_PropertyChanged;
            }
        }


        private void CurrentObject_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            control.Properties.Maximum = currentObject.Max;
        }
    }
}
