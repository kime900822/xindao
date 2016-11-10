using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CashBorrowINFO.CS
{
    public class ExcelHelp
    {
         public static bool OutToExcelFromDataGridView(string title, DataGridView dgv, bool isShowExcel)
         {
             int titleColumnSpan = 0;//标题的跨列数
             string fileName = "";//保存的excel文件名
             int columnIndex = 1;//列索引
             if (dgv.Rows.Count == 0)
                 return false;
             /*保存对话框*/
             SaveFileDialog sfd = new SaveFileDialog();
             sfd.Filter = "导出Excel(*.xls)|*.xls";
             sfd.FileName = title + DateTime.Now.ToString("yyyyMMddhhmmss");
             
             if (sfd.ShowDialog() == DialogResult.OK)
             {
                 fileName = sfd.FileName;
                 /*建立Excel对象*/
                 Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                 if (excel == null)
                 {
                     MessageBox.Show("无法创建Excel对象,可能您的计算机未安装Excel!");
                     return false;
                 }
                 try
                 {
                     excel.Application.Workbooks.Add(true);
                     excel.Visible = isShowExcel;
                     /*分析标题的跨列数*/
                     foreach (DataGridViewColumn column in dgv.Columns)
                     {
                         if (column.Visible == true)
                             titleColumnSpan++;
                     }
                     /*合并标题单元格*/
                     Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.ActiveSheet;
                     //worksheet.get_Range("A1", "C10").Merge();            
                     worksheet.get_Range(worksheet.Cells[1, 1] as Range, worksheet.Cells[1, titleColumnSpan] as Range).Merge();
                     /*生成标题*/
                     excel.Cells[1, 1] = title;
                     (excel.Cells[1, 1] as Range).HorizontalAlignment = XlHAlign.xlHAlignCenter;//标题居中
 //生成字段名称
                     columnIndex = 1;
                     for (int i = 0; i < dgv.ColumnCount; i++)
                     {
                         if (dgv.Columns[i].Visible == true)
                         {
                             excel.Cells[2, columnIndex] = dgv.Columns[i].HeaderText;
                             (excel.Cells[2, columnIndex] as Range).HorizontalAlignment = XlHAlign.xlHAlignCenter;//字段居中
                             columnIndex++;
                         }
                     }
                     //填充数据              
                     for (int i = 0; i < dgv.RowCount; i++)
                     {
                         columnIndex = 1;
                         for (int j = 0; j < dgv.ColumnCount; j++)
                         {
                             if (dgv.Columns[j].Visible == true)
                             {
                                 if (dgv[j, i].ValueType == typeof(string))
                                 {
                                     excel.Cells[i + 3, columnIndex] = "'" + dgv[j, i].Value.ToString();
                                 }
                                 else
                                 {
                                     excel.Cells[i + 3, columnIndex] = dgv[j, i].Value.ToString();
                                 }
                                 (excel.Cells[i + 3, columnIndex] as Range).HorizontalAlignment = XlHAlign.xlHAlignLeft;//字段居中
                                 columnIndex++;
                             }
                         }
                     }
                     worksheet.SaveAs(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);  
                 }
                 catch { }
                 finally
                 {  
                     excel.Quit(); 
                     excel = null;
                     GC.Collect();
                 }
                 //KillProcess("Excel");
                 return true;
             }
             else
             {
                 return false;
             }
         }
         private static void KillProcess(string processName)//杀死与Excel相关的进程
         {
             System.Diagnostics.Process myproc = new System.Diagnostics.Process();//得到所有打开的进程
             try
             {
                 foreach (System.Diagnostics.Process thisproc in System.Diagnostics.Process.GetProcessesByName(processName))
                 {
                     if (!thisproc.CloseMainWindow())
                     {
                         thisproc.Kill();
                     }
                 }
             }
             catch (Exception Exc)
             {
                 throw new Exception("", Exc);
             }
         }



        public string ExportExcel(System.Data.DataTable dt,string file,ProgressBar bar)
        {

            if (dt.Rows.Count == 0)
                return "无数据";
            try
            {
                bar.Value = 0;
                ApplicationClass xlsapp = new ApplicationClass();
                xlsapp.Visible = false;
                xlsapp.DisplayAlerts = false;
                if (xlsapp == null) {
                    return "未安装excel";
                }

                System.Globalization.CultureInfo currentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Workbooks workbooks = xlsapp.Workbooks;
                Workbook workbook = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Worksheet worksheet = (Worksheet)workbook.Worksheets[1];
                Range rang;
                for (int i = 0; i < dt.Columns.Count; i++) {
                    worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                    rang = (Range)worksheet.Cells[1, i + 1];
                    rang.Font.Bold = true;
                    rang.Interior.ColorIndex = 16;
                }

                for (int j = 0; j < dt.Rows.Count; j++) {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        worksheet.Cells[j + 2, k + 1] = dt.Rows[j][k].ToString();
                        rang = (Range)worksheet.Cells[j + 2, k + 1];
                        rang.Font.Size = 10;
                        rang.HorizontalAlignment =XlHAlign.xlHAlignLeft;
                        if (k == 0) {
                            rang.NumberFormat = "0";
                        }
                        if (k == 3)
                        {
                            rang.NumberFormat = "0";
                        }
                        if (k == 28) {
                            rang.NumberFormat = "yyyy-MM-dd";
                        }
                        if (k == 29)
                        {
                            rang.NumberFormat = "yyyy-MM-dd hh:mm:ss";
                        }
                    }
                    bar.Value = (j + 1) * 100 / dt.Rows.Count;
                    System.Windows.Forms.Application.DoEvents();
                }



                worksheet.Columns.AutoFit();
                workbook.Saved = true;
                workbook.SaveCopyAs(file);
                workbook.Close(true, Type.Missing, Type.Missing);
                workbook = null;
                xlsapp.Quit();
                xlsapp = null;
                return "导出成功！";

            }
            catch (Exception e1)
            {
                return e1.Message;
            }
            finally {
                GC.Collect();
            }
        }
    }
}
