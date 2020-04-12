using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace SHETest
{
    public static class Utilities
    {

        static List<TableDatacollection> tableDatacollections = new List<TableDatacollection>();

        public static void ReadTable(IWebElement table)
        {
            //Get All Cloumns from the table
            var columns = table.FindElements(By.TagName("th"));
            var rows = table.FindElements(By.XPath("//tbody/tr"));

            //Create rowIndex
            int rowIndex = 0;
            foreach (var row in rows)
            {
               
                int columnIndex = 0;

                var columnData = row.FindElements(By.TagName("td"));

                foreach (var columnValue in columnData)
                {
                   tableDatacollections.Add(new TableDatacollection
                    {
                        RowNumber = rowIndex,
                        ColumnName = columns[columnIndex].Text,
                        ColumnValue = columnValue.Text
                    });

                    //Move to Next column
                    columnIndex++;
                }

                    //Move to next row
                    rowIndex++;
                

               
            }
        }

        public static String ReadCell(String columnName, int rowNumber)
        {
            var data = (from e in tableDatacollections
                        where e.ColumnName == columnName && e.RowNumber == rowNumber
                        select e.ColumnValue).SingleOrDefault();
            return data;
        }


        public static String GetSubStringBetweenStrings(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }
    }

    public class TableDatacollection
    {
        public int RowNumber { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
    }



  }



