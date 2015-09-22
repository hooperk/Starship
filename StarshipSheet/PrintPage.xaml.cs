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
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.IO;

namespace StarshipSheet
{
    /// <summary>
    /// Interaction logic for PrintPage.xaml
    /// </summary>
    public partial class PrintPage : Window
    {
        public PrintPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Print Preview a flow document - From Stack Exchange
        /// </summary>
        /// <author>Cheeso</author>
        /// <see cref="http://stackoverflow.com/questions/2322064/how-can-i-produce-a-print-preview-of-a-flowdocument-in-a-wpf-application"/>
        #region FromSource
        private static string _previewWindowXaml =
    @"<Window
        xmlns                 ='http://schemas.microsoft.com/netfx/2007/xaml/presentation'
        xmlns:x               ='http://schemas.microsoft.com/winfx/2006/xaml'
        Title                 ='Print Preview - @@TITLE'
        Height                ='500'
        Width                 ='700'
        WindowStartupLocation ='CenterOwner'>
        <DocumentViewer Name='dv1'>
            <DocumentViewer.Resources>
                <Style TargetType=""ContentControl"">
                  <Style.Triggers>
                    <Trigger Property=""Name"" Value=""PART_FindToolBarHost"">
                      <Setter Property=""Visibility"" Value=""Collapsed"" />
                    </Trigger>
                  </Style.Triggers>
                </Style>
            </DocumentViewer.Resources>
        </DocumentViewer>
     </Window>";

        public void DoPreview(string title)
        {
            string fileName = System.IO.Path.GetTempFileName();
            try
            {
                // write the XPS document
                using (XpsDocument doc = new XpsDocument(fileName, FileAccess.ReadWrite))
                {
                    XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
                    writer.Write(this);
                }

                // Read the XPS document into a dynamically generated
                // preview Window 
                using (XpsDocument doc = new XpsDocument(fileName, FileAccess.Read))
                {
                    FixedDocumentSequence fds = doc.GetFixedDocumentSequence();

                    string s = _previewWindowXaml;
                    s = s.Replace("@@TITLE", title.Replace("'", "&apos;"));

                    using (var reader = new System.Xml.XmlTextReader(new StringReader(s)))
                    {
                        Window preview = System.Windows.Markup.XamlReader.Load(reader) as Window;

                        DocumentViewer dv1 = LogicalTreeHelper.FindLogicalNode(preview, "dv1") as DocumentViewer;
                        dv1.Document = fds as IDocumentPaginatorSource;
                        
                        preview.ShowDialog();
                    }
                }
            }
            finally
            {
                if (File.Exists(fileName))
                {
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch//temporary file anyway so doesn't matter if can't delete
                    {
                    }
                }
            }
        }
        #endregion
    }
}
