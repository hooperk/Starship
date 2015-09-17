using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace StarshipSheet
{
    public static class PrintSetup
    {
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
        <DocumentViewer Name='dv1'/>
     </Window>";

        public static void DoPreview(string title, FlowDocumentScrollViewer visual)
        {
            string fileName = System.IO.Path.GetTempFileName();
            try
            {
                // write the XPS document
                using (XpsDocument doc = new XpsDocument(fileName, FileAccess.ReadWrite))
                {
                    XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
                    writer.Write(visual);
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
