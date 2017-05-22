using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Activities.Presentation;
using System.Activities.Presentation.Toolbox;
using System.Activities.Statements;
using System.Activities.Presentation.View;
using System.Activities.Core.Presentation;

namespace WFDesigner
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            (new DesignerMetadata()).Register();
            this.AddDesigner();
            this.AddToolBox();
            this.AddPropertyInspector();
        }

        WorkflowDesigner wd = null;
        private void AddDesigner()
        {
            this.wd = new WorkflowDesigner();
            this.workflowDesignerPanel.Content = wd.View;
        }
        private void AddToolBox()
        {
            ToolboxControl tc = GetToolboxControl();
            this.toolboxPanel.Content = tc;
        }
        private ToolboxControl GetToolboxControl()
        {
            ToolboxControl toolboxControl = new ToolboxControl();
            ToolboxCategory toolboxCategory = new ToolboxCategory("Activities");
            ToolboxItemWrapper sequence = new ToolboxItemWrapper(typeof(Sequence));
            ToolboxItemWrapper writeLine = new ToolboxItemWrapper(typeof(WriteLine));
            toolboxCategory.Add(sequence);
            toolboxCategory.Add(writeLine);
            toolboxControl.Categories.Add(toolboxCategory);
            return toolboxControl;
        }

        private void AddPropertyInspector()
        {
            if (wd == null)
                return;
            this.WorkflowPropertyPanel.Content = wd.PropertyInspectorView;
        }

        string workflowFilePathName="temp.xaml";

        private void LoadWorkflowFromFile(string fileName)
        {
            workflowFilePathName = fileName;
            workflowDesignerPanel.Content = null;
            WorkflowPropertyPanel.Content = null;
            wd = new WorkflowDesigner();
            wd.Load(workflowFilePathName);
            DesignerView designerView = wd.Context.Services.GetService<DesignerView>();
            designerView.WorkflowShellBarItemVisibility = ShellBarItemVisibility.Arguments |
                ShellBarItemVisibility.Imports |
                ShellBarItemVisibility.MiniMap |
                ShellBarItemVisibility.Variables |
                ShellBarItemVisibility.Zoom;
            workflowDesignerPanel.Content = wd.View;
            WorkflowPropertyPanel.Content = wd.PropertyInspectorView;

        }

        private void MenuItem_Click_NewWorkflow(object sender, RoutedEventArgs e)
        {
            workflowFilePathName = @"WFTemplate.xaml";
            LoadWorkflowFromFile(workflowFilePathName);
            workflowFilePathName = "temp.xaml";
        }

        private void MenuItem_Click_LoadWorkflow(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog(this).Value)
            {
                workflowFilePathName = openFileDialog.FileName;
                LoadWorkflowFromFile(workflowFilePathName);
            }
        }

        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_SaveAs(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_Run(object sender, RoutedEventArgs e)
        {

        }

        private void TabItem_GotFocus_RefreshXamlBox(object sender, RoutedEventArgs e)
        {

        }
    }
}
