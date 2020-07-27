using System.Collections.Generic;
using System.Resources;
using System.Windows.Forms;

namespace MultiLanguage
{
    class LanguageSetting
    {
        public object ClassObject { get; set; }
        public List<object> Targets { get; set; }

        public void ChangeLanguage()
        {
            var type = this.ClassObject.GetType();
            var resource = new ResourceManager(type.ToString(), type.Assembly);
            this.Targets.ForEach(item =>
            {
                var column = (item as DataGridViewColumn);
                if (column != null)
                {
                    column.HeaderText = resource.GetString(string.Join(".", column.Name, "HeaderText"));
                }
                else
                {
                    var menuItem = (item as ToolStripMenuItem);
                    if (menuItem != null)
                    {
                        menuItem.Text = resource.GetString(string.Join(".", menuItem.Name, "Text"));
                    }
                    else
                    {
                        var form = (item as Form);
                        if (form != null)
                        {
                            form.Text = resource.GetString(string.Join(".", @"$this", "Text"));
                        }
                        else
                        {
                            var control = (item as Control);
                            control.Text = resource.GetString(string.Join(".", control.Name, "Text"));
                        }
                    }
                }
            });
        }
    }
}
